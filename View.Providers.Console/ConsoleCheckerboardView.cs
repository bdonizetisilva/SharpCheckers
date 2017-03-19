using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Interfaces;
using Okorodudu.Checkers.Presenter;
using System.Threading;
using System.Globalization;

namespace Okorodudu.Checkers.View.Providers.Console
{
   /// <summary>
   /// Checkers application in console mode
   /// </summary>
   public class ConsoleCheckerboardView : IBoardView, IDisposable
   {
      private readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
      private readonly BoardPresenter presenter;
      private readonly IBoard board = new Checkerboard();
      private readonly TextWriter writer;
      private readonly TextReader reader;
      private int? startPosition = null;

      /// <summary>
      /// Construct checkers game in console I/O mode
      /// </summary>
      public ConsoleCheckerboardView() : this(System.Console.Out, System.Console.In)
      {
      }

      /// <summary>
      /// Construct checkers game to use the given reader and writer for I/O
      /// </summary>
      /// <param name="writer">Used to write game play information to user</param>
      /// <param name="reader">Used to get input from user</param>
      public ConsoleCheckerboardView(TextWriter writer, TextReader reader)
      {
         this.writer = writer;
         this.reader = reader;
         this.presenter = new BoardPresenter(this);
      }

      /// <summary>
      /// Run the app
      /// </summary>
      private void run()
      {
         this.presenter.StartGame();

         // Keep application running even if there's only a background thread working
         autoResetEvent.WaitOne();
      }

      /// <summary>
      /// Dispose this object
      /// </summary>
      public void Dispose()
      {
         this.Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Dispose this object
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources</param>
      protected virtual void Dispose(bool disposing)
      {
         if (disposing)
         {
            IDisposable disposableAutoResetEvent = this.autoResetEvent as IDisposable;
            if (disposableAutoResetEvent != null)
            {
               disposableAutoResetEvent.Dispose();
            }
         }
      }

      /// <summary>
      /// Show the start message for the game
      /// </summary>
      public void ShowGameStart()
      {
         writer.WriteLine("Game started");
      }

      /// <summary>
      /// Set the board state to the state of the specified board
      /// </summary>
      /// <param name="board">The board to copy the state of</param>
      public void SetBoardState(IBoard board)
      {
         this.board.Copy(board);
      }

      /// <summary>
      /// Indicate to the user the player with the current turn
      /// </summary>
      /// <param name="turn"></param>
      public void ShowPlayerChange(Player turn)
      {
         writer.WriteLine("{0}'s turn", turn.ToString());
         BoardTrace.DrawBoard(board, this.writer);
      }

      /// <summary>
      /// Set the position that the player must begin the move from.  This usually
      /// indicates that the player must finish a move by completing a jump
      /// </summary>
      /// <param name="position">The position at which the current player must start the move</param>
      public void SetMoveStartPosition(int? position)
      {
         this.startPosition = position;
      }

      /// <summary>
      /// Allow or disallow the given player from moving
      /// </summary>
      /// <param name="player">The player</param>
      /// <param name="locked">If <c>true</c>, the player is prevented from moving.  If otherwise, the player is allowed to move</param>
      public void LockPlayer(Player player, bool locked)
      {
      }

      /// <summary>
      /// Prompt the given player to make move
      /// </summary>
      /// <param name="player">The player to prompt move for</param>
      public void PromptMove(Player player)
      {
         writer.WriteLine("Select move ({0}):", player.ToString());
         OnMoveInput(MoveReader.ReadMove(reader, writer, true));
      }

      /// <summary>
      /// Prompt the player to make a move at the specified position.  This indicates that a move must be completed.
      /// </summary>
      /// <param name="player">The player to prompt</param>
      /// <param name="position">The position at which the player should start the move from</param>
      public void PromptMove(Player player, int position)
      {
         writer.WriteLine("Finish move ({0}) for {1}:", player.ToString(), position.ToString(CultureInfo.CurrentCulture));
         OnMoveInput(MoveReader.ReadMove(reader, writer, true));
      }

      /// <summary>
      /// Render the given move
      /// </summary>
      /// <param name="move">The move to render</param>
      /// <param name="after">The state the board should be in after the move is rendered</param>
      public void RenderMove(Move move, IBoard after)
      {
         board.Copy(after);
         writer.WriteLine("Move made: {0}", move.ToString());
         presenter.MakeMove(move);
      }

      /// <summary>
      /// Indicate to the user that an invalid move was made
      /// </summary>
      /// <param name="move">The invalid move that was played</param>
      /// <param name="player">The player that made the move</param>
      /// <param name="message">An message indicating the problem with the move</param>
      public void ShowInvalidMove(Move move, Player player, string message)
      {
         writer.WriteLine("Invalid move: " + message);
      }

      /// <summary>
      /// Indicate that the game is over
      /// </summary>
      /// <param name="winner">The winner of the match</param>
      /// <param name="loser">The loser of the match</param>
      public void ShowGameOver(Player winner, Player loser)
      {
         writer.WriteLine("GAME OVER");
         writer.WriteLine("{0} Wins", winner.ToString());
      }

      /// <summary>
      /// Invoked when a move is made
      /// </summary>
      /// <param name="move"></param>
      protected virtual void OnMoveInput(Move move)
      {
         presenter.MakeMove(move, startPosition);
      }

      /// <summary>
      /// The main entry point into the application
      /// </summary>
      public static void Main()
      {
         ConsoleCheckerboardView app = new ConsoleCheckerboardView();
         app.run();
      }
   }
}
