using System;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// Possible status of checker moves
   /// </summary>
   public enum MoveStatus
   {
      /// <summary>The move is legal</summary>
      Legal,

      /// <summary>The move is illegal</summary>
      Illegal,

      /// <summary>The move is legal but not completed because it hasn't been determined if there is a multiple jump</summary>
      Incomplete
   }
}
