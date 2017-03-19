using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
   /// <summary>
   /// A basic checkers move generation engine.
   /// </summary>
   public class SimpleEngine : IEngine
   {
      private static readonly Random random = new Random();
      private readonly object SynchRoot = new object();
      private bool forceMove;


      /// <summary>
      /// Generate a move for the given board state
      /// </summary>
      /// <param name="board">The board state</param>
      /// <param name="player">The player to generate the move for</param>
      /// <param name="ply">The maximum ply</param>
      /// <param name="timeout">The maximum time allowed to generate teh move</param>
      /// <returns>The generated move</returns>
      public Move GenerateMove(IBoard board, Player player, int ply, TimeSpan timeout)
      {
         lock (SynchRoot)
         {
            forceMove = false;
            Move move = null;
            int score = MiniMax(ref move, board, player, ply, ply, timeout, DateTime.Now);

            if (forceMove)
            {
               System.Diagnostics.Trace.WriteLine("Forced move");
            }
            System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Selected move: {0}.  Score: {1}", move, score.ToString(CultureInfo.InvariantCulture)));
            return move;
         }
      }

      private int MiniMax(ref Move bestMove, IBoard board, Player player, int ply, int depth, TimeSpan timeout, DateTime startTime)
      {
         if (depth <= 0)
         {
            // reached ply level
            return Evaluate(board, player);
         }
         else if (forceMove)
         {
            return int.MinValue;
         }

         ICollection<Move> moves = CheckerMoveRules.GetAvailableMoves(board, player);
         if ((moves == null) || (moves.Count == 0))
         {
            // reached leaf
            return Evaluate(board, player);
         }


         int bestScore = int.MinValue;
         Player opponent = BoardUtilities.GetOpponent(player);
         IBoard boardCopy = board.Copy();
         foreach (Move move in moves)
         {
            CheckerMoveRules.UpdateBoard(boardCopy, move);
            int score = -MiniMax(ref bestMove, boardCopy, opponent, ply, depth - 1, timeout, startTime);
            boardCopy.Copy(board);// undo move

            if (depth == ply)
            {
               System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "Move: {0}.  Score: {1}", move, score.ToString(CultureInfo.InvariantCulture)));
               if ((bestMove == null) || (score > bestScore))
               {
                  bestMove = move;
               }
            }

            bestScore = Math.Max(bestScore, score);
         }

         return bestScore;
      }

      /// <summary>
      /// Notify the engine to hault move generation
      /// </summary>
      public void CancelProcessing()
      {
         forceMove = true;
      }

      /// <summary>
      /// Force the engine to move immediately
      /// </summary>
      public void ForceMove()
      {
         forceMove = true;
      }

      private static int Evaluate(IBoard board, Player player)
      {
         const int MAN_WEIGHT = 100;
         const int KING_WEIGHT = 130;
         const int BACKRANK_WEIGHT = 10;
         int score = 0;
         int whiteKings = 0, blackKings = 0, whiteMen = 0, blackMen = 0;

         for (int pos = 1; pos < BoardConstants.LightSquareCount; pos++)
         {
            Piece piece = board[pos];

            if (BoardUtilities.IsPiece(piece))
            {
               switch (piece)
               {
                  case Piece.BlackMan:
                     blackMen++;
                     break;
                  case Piece.WhiteMan:
                     whiteMen++;
                     break;
                  case Piece.BlackKing:
                     blackKings++;
                     break;
                  case Piece.WhiteKing:
                     whiteKings++;
                     break;
               }
            }
         }

         if (blackMen + blackKings == 0)
         {
            whiteKings += 100; ;
         }
         else if (whiteMen + whiteKings == 0)
         {
            blackKings += 100;
         }
         
         if (board[31] == Piece.WhiteMan && board[30] == Piece.WhiteMan && blackMen > 1)
            score -= BACKRANK_WEIGHT;

         if (board[1] == Piece.BlackMan && board[3] == Piece.BlackMan && whiteMen > 1)
            score += BACKRANK_WEIGHT;

         int blackPieceScore = blackKings * KING_WEIGHT + blackMen * MAN_WEIGHT;
         int whitePieceScore = whiteKings * KING_WEIGHT + whiteMen * MAN_WEIGHT;

         score += ((blackPieceScore - whitePieceScore) * 200) / (blackPieceScore + whitePieceScore);
         score += blackPieceScore - whitePieceScore;
         
         //int blackPieceScore = blackKings * 4 + blackMen * 1;
         //int whitePieceScore = whiteKings * 4 + whiteMen * 1;
         //score = blackPieceScore - whitePieceScore;
         const int RANDOMIZER_MIN = -10;
         const int RANDOMIZER_MAX = 10;
         return ((player == Player.Black) ? score : -score) + random.Next(RANDOMIZER_MIN, RANDOMIZER_MAX);
      }
   }
}
