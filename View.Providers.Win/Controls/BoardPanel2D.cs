using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Interfaces;
using Okorodudu.Checkers.Presenter;
using Okorodudu.Checkers.View.Providers.Win.ViewModel;


namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   /// <summary>
   /// 2D board panel
   /// </summary>
   public partial class BoardPanel2D : UserControl, IBoardView
   {
      private delegate void RefreshDelegate();
      private delegate void RenderMoveDelegate(Move move, IBoard after);

      #region Fields
      private static readonly Cursor HandSelectCursor = CreateSelectCustomCursor();
      private static readonly Cursor GrabCursor = CreateGrabCustomCursor();
      private readonly BoardPresenter presenter;
      private readonly IBoard board;
      private readonly MoveAnimator moveAnimator;
      private readonly FloatingPiece floatingPiece = new FloatingPiece();
      private readonly BoardPainter boardPainter;
      private Font gameMessageFont = new Font(FontFamily.GenericSerif, 27, FontStyle.Bold);
      private Brush gameMessageBrush = new SolidBrush(Color.LightGoldenrodYellow);
      private int? startPosition;
      private bool blackLocked;
      private bool whiteLocked;
      private bool gameStarted;
      private string gameMessage;
      private int boardLength = 400;
      private IAudio audio = new Win32Audio();
      #endregion Fields


      /// <summary>
      /// Construct a board panel with the specified board
      /// </summary>
      public BoardPanel2D()
      {
         board = new Checkerboard();
         boardPainter = new BoardPainter(PieceSetFactory.CreateWoodGrainPieceSet());
         this.Cursor = HandSelectCursor;
         gameMessage = "Click Your Piece To Play";
         moveAnimator = new MoveAnimator(this, TimeSpan.FromMilliseconds(80), floatingPiece);
         moveAnimator.MoveCompleted += new MoveAnimator.MoveCompletedDelegate(moveAnimator_MoveCompleted);
         InitializeComponent();
         presenter = new BoardPresenter(this);
      }

      /// <summary>
      /// Get size of board square
      /// </summary>
      private int SquareSize
      {
         get
         {
            return this.boardLength / BoardConstants.Rows;
         }
      }

      private string GameMessage
      {
         set
         {
            gameMessage = value;
            this.RefreshBoard();
         }

         get { return gameMessage; }
      }


      #region Paint Board
      /// <summary>
      /// Raises the OnPaint event
      /// </summary>
      /// <param name="e">Paint event args</param>
      protected override void OnPaint(PaintEventArgs e)
      {
         base.OnPaint(e);
         Graphics g = e.Graphics;
         boardPainter.Paint(g, board, SquareSize, floatingPiece);

         string message = GameMessage;
         if (!string.IsNullOrEmpty(message))
         {
            SizeF messageSize = g.MeasureString(message, gameMessageFont);
            float messageX = this.boardLength / 2F - messageSize.Width / 2F;
            float messageY = this.boardLength / 2F - messageSize.Height / 2F;
            g.DrawString(message, gameMessageFont, Brushes.Gray, messageX - 2F, messageY - 2F);
            g.DrawString(message, gameMessageFont, gameMessageBrush, messageX, messageY);
         }
      }
      #endregion


      /// <summary>
      /// Raises Resize event
      /// </summary>
      /// <param name="e">The event args</param>
      protected override void OnResize(EventArgs e)
      {
         int length = Math.Min(this.Width, this.Height);

         // Make length divisible by eight (board length)
         for (int i = 0; i < BoardConstants.Cols - 1; i++)
         {
            if (length + i % BoardConstants.Cols == 0)
            {
               length += i;
               break;
            }
         }

         this.boardLength = length;

         // Clear board painter cache
         boardPainter.ClearCache();
         RefreshBoard();
         base.OnResize(e);
      }


      #region Mouse Event Handlers
      /// <summary>
      /// Raises mouse down event
      /// </summary>
      /// <param name="e">The mouse event args</param>
      protected override void OnMouseDown(MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            int position = GetPosition(e.Location);
            if (position > 0)
            {
               Piece piece = board[position];

               if (!gameStarted)
               {
                  Player humanPlayer = BoardUtilities.GetPlayer(piece);
                  Player computerPlayer = BoardUtilities.GetOpponent(humanPlayer);
                  presenter.SetComputer(humanPlayer, false);
                  presenter.SetComputer(computerPlayer, true);
                  presenter.StartGame();
               }

               bool locked = ((blackLocked && BoardUtilities.IsBlack(piece)) || (whiteLocked && BoardUtilities.IsWhite(piece)));

               if ((!locked) && (piece != Piece.None))
               {
                  int offset = SquareSize / 2;
                  floatingPiece.X = e.X - offset;
                  floatingPiece.Y = e.Y - offset;
                  floatingPiece.Position = position;
                  Cursor = GrabCursor;
                  this.RefreshBoard();
               }
               System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "clicked: {0}, piece={1}", position, piece));
            }
         }

         base.OnMouseClick(e);
      }

      /// <summary>
      /// Raises the MouseMove event
      /// </summary>
      /// <param name="e"></param>
      protected override void OnMouseMove(MouseEventArgs e)
      {
         if ((!moveAnimator.Running) && (floatingPiece.Active))
         {
            Piece piece = board[floatingPiece.Position];
            bool locked = ((blackLocked && BoardUtilities.IsBlack(piece)) || (whiteLocked && BoardUtilities.IsWhite(piece)));

            if (!locked)
            {
               int offset = SquareSize / 2;
               floatingPiece.X = e.X - offset;
               floatingPiece.Y = e.Y - offset;
               this.RefreshBoard();
            }
         }

         base.OnMouseMove(e);
      }

      /// <summary>
      /// Raises MouseUp event
      /// </summary>
      /// <param name="e">The mouse event</param>
      protected override void OnMouseUp(MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            if (floatingPiece.Active)
            {

               Piece piece = board[floatingPiece.Position];
               bool locked = ((blackLocked && BoardUtilities.IsBlack(piece)) || (whiteLocked && BoardUtilities.IsWhite(piece)));

               if (!locked)
               {
                  Move move = null;
                  int position = GetPosition(e.Location);
                  if ((position > 0) && (board[position] == Piece.None))
                  {
                     board[position] = board[floatingPiece.Position];
                     board[floatingPiece.Position] = Piece.None;
                     move = new Move(floatingPiece.Position, position);
                  }

                  floatingPiece.Position = FloatingPiece.INVALID_POSITION;

                  Cursor = HandSelectCursor;
                  this.RefreshBoard();

                  if (move != null)
                  {
                     OnMoveInput(move);
                  }
               }
            }
         }

         base.OnMouseUp(e);
      }

      /// <summary>
      /// Invoked when a move has been made
      /// </summary>
      /// <param name="move"></param>
      protected virtual void OnMoveInput(Move move)
      {
         presenter.MakeMove(move, startPosition);
         audio.PlayWavResourceYield("PieceDrop.wav");
      }
      #endregion


      #region Utility Methods


      /// <summary>
      /// Get the board position at the given coordinate
      /// </summary>
      /// <param name="coordinates">the coordinates</param>
      /// <returns>The board position for the given coordinate</returns>
      private int GetPosition(Point coordinates)
      {
         int squareSize = SquareSize;
         int col = BoardConstants.Cols - 1 - coordinates.X / squareSize;
         int row = BoardConstants.Rows - 1 - coordinates.Y / squareSize;
         return Okorodudu.Checkers.Model.Location.ToPosition(row, col);
      }
      #endregion


      #region Custom Cursors
      /// <summary>
      /// Set custom select cursor
      /// </summary>
      private static Cursor CreateSelectCustomCursor()
      {
         return new Cursor(Properties.Resources.HandSelectCursor.ToBitmap().GetHicon());
      }

      /// <summary>
      /// Set custom grab cursor
      /// </summary>
      private static Cursor CreateGrabCustomCursor()
      {
         return new Cursor(Properties.Resources.HandGrabCursor.ToBitmap().GetHicon());
      }
      #endregion


      private void moveAnimator_MoveCompleted(Move move)
      {
         presenter.MakeMove(move);
         audio.PlayWavResourceYield("PieceDrop.wav");
      }

      /// <summary>
      /// Refresh the board
      /// </summary>
      public void RefreshBoard()
      {
         if (this.InvokeRequired)
         {
            this.Invoke(new RefreshDelegate(RefreshBoard));
         }
         else
         {
            this.Refresh();
         }
      }


      #region IBoardView Members

      /// <summary>
      /// Show the start message for the game
      /// </summary>
      public void ShowGameStart()
      {
         gameStarted = true;
         GameMessage = string.Empty;
         System.Diagnostics.Trace.WriteLine("Game Started");
      }

      /// <summary>
      /// Set the board state to the state of the specified board
      /// </summary>
      /// <param name="board">The board to copy the state of</param>
      public void SetBoardState(IBoard board)
      {
         this.board.Copy(board);
         RefreshBoard();
      }

      /// <summary>
      /// Indicate to the user the player with the current turn
      /// </summary>
      /// <param name="turn"></param>
      public void ShowPlayerChange(Player turn)
      {
         System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}'s turn", turn.ToString()));
      }

      /// <summary>
      /// Set the position that the player must begin the move from.  This usually
      /// indicates that the player must finish a move by completing a jump
      /// </summary>
      /// <param name="position">The position at which the current player must start the move</param>
      public void SetMoveStartPosition(int? position)
      {
         startPosition = position;
      }

      /// <summary>
      /// Allow or disallow the given player from moving
      /// </summary>
      /// <param name="player">The player</param>
      /// <param name="locked">If <c>true</c>, the player is prevented from moving.  If otherwise, the player is allowed to move</param>
      public void LockPlayer(Player player, bool locked)
      {
         System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Locking {0}: {1}", player.ToString(), locked.ToString(CultureInfo.InvariantCulture)));

         if (player == Player.Black)
         {
            blackLocked = locked;
         }
         else if (player == Player.White)
         {
            whiteLocked = locked;
         }
      }

      /// <summary>
      /// Prompt the given player to make move
      /// </summary>
      /// <param name="player">The player to prompt move for</param>
      public void PromptMove(Player player)
      {
         System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Waiting for {0}'s move input.", player.ToString()));
      }

      /// <summary>
      /// Prompt the player to make a move at the specified position.  This indicates that a move must be completed.
      /// </summary>
      /// <param name="player">The player to prompt</param>
      /// <param name="position">The position at which the player should start the move from</param>
      public void PromptMove(Player player, int position)
      {
         System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} must complete jump at {1}", player.ToString(), position));
      }

      /// <summary>
      /// Render the given move
      /// </summary>
      /// <param name="move">The move to render</param>
      /// <param name="after">The state the board should be in after the move is rendered</param>
      public void RenderMove(Move move, IBoard after)
      {
         if (this.InvokeRequired)
         {
            this.Invoke(new RenderMoveDelegate(RenderMove), move, after);
         }
         else
         {
            moveAnimator.Start(move, SquareSize);
         }
      }

      /// <summary>
      /// Indicate to the user that an invalid move was made
      /// </summary>
      /// <param name="move">The invalid move that was played</param>
      /// <param name="player">The player that made the move</param>
      /// <param name="message">An message indicating the problem with the move</param>
      public void ShowInvalidMove(Move move, Player player, string message)
      {
         System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Invalid Move: {0}", message));
         //MessageBox.Show(this, message, Application.ProductName);
      }

      /// <summary>
      /// Indicate that the game is over
      /// </summary>
      /// <param name="winner">The winner of the match</param>
      /// <param name="loser">The loser of the match</param>
      public void ShowGameOver(Player winner, Player loser)
      {
         gameStarted = false;
         GameMessage = string.Format(CultureInfo.CurrentCulture, "{0} WINS!!!\n\nClick Board To Play", winner.ToString());
      }

      #endregion
   }
}
