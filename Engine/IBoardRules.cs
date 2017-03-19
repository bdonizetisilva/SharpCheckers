using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
   /// <summary>
   /// Interface for board rules
   /// </summary>
   public interface IBoardRules
   {
      /// <summary>
      /// Reset the board
      /// </summary>
      /// <param name="board">The board to reset</param>
      void ResetBoard(IBoard board);

      /// <summary>
      /// Check if the given move is valid for the given state
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="move">The move</param>
      /// <param name="player">The player that made the move</param>
      /// <returns>The status of the move</returns>
      MoveStatus IsValidMove(IBoard board, Move move, Player player);

      /// <summary>
      /// Is the game over
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="turn">The player with the current turn</param>
      /// <returns><c>true</c> if the game is over and <c>false</c> false if otherwise</returns>
      bool IsGameOver(IBoard board, Player turn);

      /// <summary>
      /// Get the winner of the game
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="turn">The player with the turn</param>
      /// <returns>The player that won the game if any</returns>
      Player GetWinner(IBoard board, Player turn);

      /// <summary>
      /// Apply the given move to the board
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="move">The move to apply to the board</param>
      /// <returns><c>true</c> if the move was applied successfully</returns>
      bool ApplyMove(IBoard board, Move move);

      /// <summary>
      /// Attempt to resolve ambiguous jump move.  The longest move matching the first 
      /// location and last location is selected.
      /// </summary>
      /// <param name="board">The board</param>
      /// <param name="move">The possibly ambiguos move</param>
      /// <returns><c>true</c> if move could be resolved</returns>
      Move ResolveAmbiguousMove(IBoard board, Move move);
   }
}
