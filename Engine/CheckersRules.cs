using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
   /// <summary>
   /// A checkers rules engine implementation
   /// </summary>
   public class CheckersRules : IBoardRules
   {
      /// <summary>
      /// Reset the board
      /// </summary>
      /// <param name="board">The board to reset</param>
      public void ResetBoard(IBoard board)
      {
         const int BLACK_BEGIN_POSITON = 1;
         const int BLACK_END_POSITION = 12;
         const int WHITE_BEGIN_POSITON = 21;
         const int WHITE_END_POSITION = 32;

         board.Clear();

         for (int position = BLACK_BEGIN_POSITON; position <= BLACK_END_POSITION; position++)
         {
            board[position] = Piece.BlackMan;
         }

         for (int position = WHITE_BEGIN_POSITON; position <= WHITE_END_POSITION; position++)
         {
            board[position] = Piece.WhiteMan;
         }
      }

      /// <summary>
      /// Check if the given move is valid for the given state
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="move">The move</param>
      /// <param name="player">The player that made the move</param>
      /// <returns>The status of the move</returns>
      public MoveStatus IsValidMove(IBoard board, Move move, Player player)
      {
         return CheckerMoveRules.IsMoveLegal(board, move, player);
      }

      /// <summary>
      /// Is the game over
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="turn">The player with the current turn</param>
      /// <returns><c>true</c> if the game is over and <c>false</c> false if otherwise</returns>
      public bool IsGameOver(IBoard board, Player turn)
      {
         return !CheckerMoveRules.HasMovesAvailable(board, turn);
      }

      /// <summary>
      /// Get the winner of the game
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="turn">The player with the turn</param>
      /// <returns>The player that won the game if any</returns>
      public Player GetWinner(IBoard board, Player turn)
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Apply the given move to the board
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="move">The move to apply to the board</param>
      /// <returns><c>true</c> if the move was applied successfully</returns>
      public bool ApplyMove(IBoard board, Move move)
      {
         CheckerMoveRules.UpdateBoard(board, move);
         return true;
      }

      /// <summary>
      /// Attempt to resolve ambiguous jump move.  The longest move matching the first 
      /// location and last location is selected.
      /// </summary>
      /// <param name="board">The board</param>
      /// <param name="move">The possibly ambiguos move</param>
      /// <returns><c>true</c> if move could be resolved</returns>
      public Move ResolveAmbiguousMove(IBoard board, Move move)
      {
         return CheckerMoveRules.ResolveAmbiguousMove(board, move);
      }
   }
}
