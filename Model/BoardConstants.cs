using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// Board constants
   /// </summary>
   public static class BoardConstants
   {
      /// <summary>
      /// The number of rows on a checker board
      /// </summary>
      public static readonly int Rows = 8;

      /// <summary>
      /// The number of columns on a checker board
      /// </summary>
      public static readonly int Cols = 8;

      /// <summary>
      /// The number of legal squares.  These are the light colored squares on the board.
      /// </summary>
      public static readonly int LightSquareCount = 32;
   }
}
