using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.View.Providers.Console
{
   static class MoveReader
   {
      public static Move ReadMove(TextReader reader, TextWriter writer, bool retry)
      {
         String input = reader.ReadLine();
         bool validMove = false;
         Move move = null;

         do
         {
            String[] positions = input.Trim().Split(' ');
            move = new Move();

            foreach (String position in positions)
            {
               if (!String.IsNullOrEmpty(position))
               {
                  int numericPosition;
                  if (int.TryParse(position, out numericPosition))
                  {
                     move.AddMoves(numericPosition);
                  }
                  else
                  {
                     validMove = false;
                     writer.WriteLine("Error parsing move.  Moves should be numeric");
                     break;
                  }
               }
            }
            validMove = true;
         } while (!validMove && retry);

         return move;
      }
   }
}
