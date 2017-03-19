using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.View.Providers.Win.PieceSets;

namespace Okorodudu.Checkers.View.Providers.Win.ViewModel
{
   /// <summary>
   /// Factory for creating piece sets.
   /// </summary>
   public static class PieceSetFactory
   {
      private static IDictionary<string, IPieceSet> pieceSets = new Dictionary<string, IPieceSet>();

      /// <summary>
      /// Get names of keys
      /// </summary>
      /// <returns></returns>
      public static ICollection<string> PieceSetNames()
      {
         return pieceSets.Keys;
      }

      /// <summary>
      /// Create wood grain piece set
      /// </summary>
      /// <returns>wood grain piece set</returns>
      public static IPieceSet CreateWoodGrainPieceSet()
      {
         string pieceSetName = DefaultPieceSet.Name;

         if (!pieceSets.ContainsKey(pieceSetName))
         {
            pieceSets.Add(
                pieceSetName,
                new PieceSet(
                    DefaultPieceSet.Name,
                    DefaultPieceSet.BlackMan,
                    DefaultPieceSet.WhiteMan,
                    DefaultPieceSet.BlackKing,
                    DefaultPieceSet.WhiteKing,
                    DefaultPieceSet.DarkSquare,
                    DefaultPieceSet.LightSquare
                )
            );
         }

         return pieceSets[pieceSetName];
      }
   }
}
