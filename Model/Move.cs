using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Okorodudu.Checkers.Model
{
   /// <summary>
   /// Encaspsulates a single checkers move.  This includes possible jumps via captures.
   /// </summary>
   public class Move
   {
      private readonly List<int> positions = new List<int>();


      /// <summary>
      /// Construct an empty move
      /// </summary>
      public Move()
      {
      }

      /// <summary>
      /// Construct a move based on the board postion notation
      /// </summary>
      /// <param name="first">The origin of the move</param>
      /// <param name="rest">The remaining moves.  Greater than one if this move contains jumps</param>
      public Move(int first, params int[] rest)
      {
         AddMoves(first, rest);
      }

      /// <summary>
      /// Construct a move with the given board locations
      /// </summary>
      /// <param name="first">The origin of the move</param>
      /// <param name="rest">The remaining moves.  Greater than one if this move contains jumps</param>
      public Move(Location first, params Location[] rest)
      {
         AddMoves(first, rest);
      }

      /// <summary>
      /// Get the board notation location at the given index.  This is zero-based.
      /// </summary>
      /// <param name="moveIndex">The index to get position for</param>
      /// <returns>The position at the given index</returns>
      public int this[int moveIndex]
      {
         get
         {
            if ((moveIndex < 0) || (moveIndex >= positions.Count))
            {
               throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
            }

            return positions[moveIndex];
         }
      }

      /// <summary>
      /// Get the location at the given index.  This is zero-based.
      /// </summary>
      /// <param name="moveIndex"></param>
      /// <returns></returns>
      public Location GetLocation(int moveIndex)
      {
         if ((moveIndex < 0) || (moveIndex >= positions.Count))
         {
            throw new ArgumentOutOfRangeException("moveIndex", "The move index is out of range");
         }

         return Location.FromPosition(positions[moveIndex]);
      }

      /// <summary>
      /// Add the given locations to this move
      /// </summary>
      /// <param name="first">The first location to add</param>
      /// <param name="rest">The remaining positions to add</param>
      public void AddMoves(Location first, params Location[] rest)
      {
         positions.Add(first.ToPosition());
         foreach (Location location in rest)
         {
            positions.Add(location.ToPosition());
         }
      }

      /// <summary>
      /// Add the given locations to this move using board notation
      /// </summary>
      /// <param name="first">The first location to add</param>
      /// <param name="rest">The remaining positions to add</param>
      public void AddMoves(int first, params int[] rest)
      {
         positions.Add(first);
         foreach (int position in rest)
         {
            positions.Add(position);
         }
      }

      /// <summary>
      /// Get the number of positions in this move
      /// </summary>
      public int Count
      {
         get { return positions.Count; }
      }

      /// <summary>
      /// Get the origin of this move in board notation.  This is the position the move originated at.
      /// </summary>
      public int? Origin
      {
         get
         {
            return (positions.Count > 0) ? new Nullable<int>(positions[0]) : null;
         }
      }

      /// <summary>
      /// Get the final position of this move in board notation.
      /// </summary>
      public int? Destination
      {
         get
         {
            return (positions.Count > 0) ? new Nullable<int>(positions[positions.Count - 1]) : null;
         }
      }

      /// <summary>
      /// Get the origin of this move.  This is the position the move originated at.
      /// </summary>
      public Location OriginLocation
      {
         get
         {
            return (positions.Count > 0) ? Location.FromPosition(positions[0]) : null;
         }
      }

      /// <summary>
      /// Get the final position of this move.
      /// </summary>
      public Location DestinationLocation
      {
         get
         {
            return (positions.Count > 0) ? Location.FromPosition(positions[positions.Count - 1]) : null;
         }
      }

      /// <summary>Is this a jumping move</summary>
      /// <returns><code>true</code> if this is a jumping move</returns>
      public bool IsJump()
      {
         if (positions.Count > 2)
         {
            return true;
         }
         else
         {
            const int INVALID_POSITION = -1;
            Location origin = Location.FromPosition(Origin ?? INVALID_POSITION);
            Location destination = Location.FromPosition(Destination ?? INVALID_POSITION);
            return (
               (Math.Abs(origin.Row - destination.Row) > 1) ||
               (Math.Abs(origin.Col - destination.Col) > 1)
            );
         }
      }

      /// <summary>Get the short notation formatted location</summary>
      /// <returns>the short notation formatted location</returns>
      public String ToShortNotationLocation()
      {
         if (positions.Count == 0)
         {
            const String BLANK_MOVE = "...";
            return BLANK_MOVE;
         }

         return Origin +
               ((!IsJump()) ? "-" : "x") +
               Destination;

      }

      /// <summary>String representation of move</summary>
      public override String ToString()
      {
         return ToShortNotationLocation();
      }
   }
}