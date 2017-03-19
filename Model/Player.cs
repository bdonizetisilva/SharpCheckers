using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// The game player.  A checkers match is between two opponents.  One player
   /// controls the black pieces and the other controls the white pieces.
   /// </summary>
   public enum Player
   {
      /// <summary>
      /// Neither player
      /// </summary>
      None,

      /// <summary>
      /// The black piece player
      /// </summary>
      Black,

      /// <summary>
      /// The white piece player
      /// </summary>
      White
   }
}
