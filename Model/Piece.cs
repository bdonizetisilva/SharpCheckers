using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// The various pieces
   /// </summary>
   public enum Piece
   {
      /// <summary>
      /// This indicates an invalid piece.  i.e. Invalid square
      /// </summary>
      Illegal,

      /// <summary>
      /// This indicates that the square is empty and has no piece
      /// </summary>
      None,

      /// <summary>
      /// Black man piece
      /// </summary>
      BlackMan,

      /// <summary>
      /// White man piece
      /// </summary>
      WhiteMan,

      /// <summary>
      /// Black king piece
      /// </summary>
      BlackKing,

      /// <summary>
      /// White king piece
      /// </summary>
      WhiteKing
   }
}
