using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.View.Interfaces
{
   /// <summary>
   /// The interface for a game board
   /// </summary>
   public interface IBoardView
   {
      /// <summary>
      /// Show the start message for the game
      /// </summary>
      void ShowGameStart();

      /// <summary>
      /// Set the board state to the state of the specified board
      /// </summary>
      /// <param name="board">The board to copy the state of</param>
      void SetBoardState(IBoard board);

      /// <summary>
      /// Indicate to the user the player with the current turn
      /// </summary>
      /// <param name="turn"></param>
      void ShowPlayerChange(Player turn);

      /// <summary>
      /// Set the position that the player must begin the move from.  This usually
      /// indicates that the player must finish a move by completing a jump
      /// </summary>
      /// <param name="position">The position at which the current player must start the move</param>
      void SetMoveStartPosition(int? position);

      /// <summary>
      /// Allow or disallow the given player from moving
      /// </summary>
      /// <param name="player">The player</param>
      /// <param name="locked">If <c>true</c>, the player is prevented from moving.  If otherwise, the player is allowed to move</param>
      void LockPlayer(Player player, bool locked);

      /// <summary>
      /// Prompt the given player to make move
      /// </summary>
      /// <param name="player">The player to prompt move for</param>
      void PromptMove(Player player);

      /// <summary>
      /// Prompt the player to make a move at the specified position.  This indicates that a move must be completed.
      /// </summary>
      /// <param name="player">The player to prompt</param>
      /// <param name="position">The position at which the player should start the move from</param>
      void PromptMove(Player player, int position);

      /// <summary>
      /// Render the given move
      /// </summary>
      /// <param name="move">The move to render</param>
      /// <param name="after">The state the board should be in after the move is rendered</param>
      void RenderMove(Move move, IBoard after);

      /// <summary>
      /// Indicate to the user that an invalid move was made
      /// </summary>
      /// <param name="move">The invalid move that was played</param>
      /// <param name="player">The player that made the move</param>
      /// <param name="message">An message indicating the problem with the move</param>
      void ShowInvalidMove(Move move, Player player, string message);

      /// <summary>
      /// Indicate that the game is over
      /// </summary>
      /// <param name="winner">The winner of the match</param>
      /// <param name="loser">The loser of the match</param>
      void ShowGameOver(Player winner, Player loser);
   }
}
