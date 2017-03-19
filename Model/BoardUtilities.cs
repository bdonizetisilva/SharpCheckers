using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// Various board utility methods
   /// </summary>
   public static class BoardUtilities
   {
      /// <summary>
      /// Is the given piece a man
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is a man and <c>false</c>  if otherwise</returns>
      public static bool IsMan(Piece piece)
      {
         return ((piece == Piece.BlackMan) || (piece == Piece.WhiteMan));
      }

      /// <summary>
      /// Is the given piece a king
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is a king and <c>false</c> if otherwise</returns>
      public static bool IsKing(Piece piece)
      {
         return ((piece == Piece.BlackKing) || (piece == Piece.WhiteKing));
      }

      /// <summary>
      /// Is the given piece black
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is a black and <c>false</c> if otherwise</returns>
      public static bool IsBlack(Piece piece)
      {
         return ((piece == Piece.BlackMan) || (piece == Piece.BlackKing));
      }

      /// <summary>
      /// Is the given piece white
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is a white and <c>false</c> if otherwise</returns>
      public static bool IsWhite(Piece piece)
      {
         return ((piece == Piece.WhiteMan) || (piece == Piece.WhiteKing));
      }

      /// <summary>
      /// Tests if the given piece is an actual piece as opposed to an an empty piece (no piece).
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is an actual piece and <c>false</c> if otherwise</returns>
      public static bool IsPiece(Piece piece)
      {
         return (piece != Piece.None);
      }

      /// <summary>
      /// Tests if the given piece is empty (no piece).
      /// </summary>
      /// <param name="piece">The piece to test</param>
      /// <returns><c>true</c> if the given piece is empty and <c>false</c> if otherwise</returns>
      public static bool IsEmpty(Piece piece)
      {
         return (piece == Piece.None);
      }

      /// <summary>
      /// Is the given player the owner of the specified piece
      /// </summary>
      /// <param name="player">The player to check ownership</param>
      /// <param name="piece">The piece to check ownership</param>
      /// <returns><c>true</c> if the player owns the given piece and <c>false</c> if otherwise</returns>
      public static bool IsOwner(Player player, Piece piece)
      {
         if (piece == Piece.None)
         {
            return false;
         }

         Player pieceOwner = GetPlayer(piece);
         return (player == pieceOwner);
      }

      /// <summary>
      /// Is the given player the opponent of the specified piece
      /// </summary>
      /// <param name="player">The player</param>
      /// <param name="piece">The piece to check if opponent's piece</param>
      /// <returns><c>true</c> if the player is the oppponent of the given piece and <c>false</c> if otherwise</returns>
      public static bool IsOpponentPiece(Player player, Piece piece)
      {
         if (piece == Piece.None)
         {
            return false;
         }

         return !IsOwner(player, piece);
      }

      /// <summary>
      /// Are the specified pieces opponents
      /// </summary>
      /// <param name="piece1">The first piece to compare</param>
      /// <param name="piece2">The second piece to compare</param>
      /// <returns><c>true</c> if the pieces are opponents of each other and false if otherwise.</returns>
      public static bool AreOpponents(Piece piece1, Piece piece2)
      {
         if ((piece1 == Piece.None) || (piece2 == Piece.None))
         {
            return false;
         }

         return (GetPlayer(piece1) != GetPlayer(piece2));
      }

      /// <summary>
      /// Get the player that is the opponent of the specified piece
      /// </summary>
      /// <param name="piece">The piece to get opponent for</param>
      /// <returns>The opponent of the given piece</returns>
      public static Player GetOpponent(Piece piece)
      {
         Player player = GetPlayer(piece);
         return GetOpponent(player);
      }

      /// <summary>
      /// Get the opponent of the given player
      /// </summary>
      /// <param name="player">The player to get the opponent for</param>
      /// <returns>The opponent of the given player</returns>
      public static Player GetOpponent(Player player)
      {
         if (player == Player.None)
         {
            return player;
         }

         return (player == Player.Black) ? Player.White : Player.Black;
      }

      /// <summary>
      /// Get the player that owns the given piece
      /// </summary>
      /// <param name="piece">The piece to get the owner for</param>
      /// <returns>The player that owns the given piece.  If the piece is empty/invalid, then neither player is returned.</returns>
      public static Player GetPlayer(Piece piece)
      {
         if (IsBlack(piece))
         {
            return Player.Black;
         }
         else if (IsWhite(piece))
         {
            return Player.White;
         }
         else
         {
            return Player.None;
         }
      }
   }
}
