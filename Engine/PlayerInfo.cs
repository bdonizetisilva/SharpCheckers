using System;
using System.Collections.Generic;
using System.Text;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.Engine
{
   /// <summary>
   /// Player details
   /// </summary>
   public class PlayerInfo
   {
      private readonly Player player;
      private String name;
      private bool isComputer;
      private int level = 7;
      private TimeSpan timeout = TimeSpan.FromMinutes(3);
      private IEngine engine;

      /// <summary>
      /// Construct the player details
      /// </summary>
      /// <param name="player">The player</param>
      public PlayerInfo(Player player) : this(player, false)
      {
      }

      /// <summary>
      /// Construct the player details
      /// </summary>
      /// <param name="player">The player</param>
      /// <param name="isComputer"><c>true</c> if the player is a computer and <c>false</c> if othewise</param>
      public PlayerInfo(Player player, bool isComputer)
      {
         this.player = player;
         this.isComputer = isComputer;
         this.engine = new SimpleEngine();
      }

      /// <summary>
      /// Get the underlying player
      /// </summary>
      public Player Player
      {
         get { return player; }
      }

      /// <summary>
      /// Get or set the name for the player
      /// </summary>
      public string Name
      {
         get { return name; }
         set { name = value; }
      }

      /// <summary>
      /// Get or set whether the player is computer controlled
      /// </summary>
      public bool IsComputer
      {
         get { return isComputer; }
         set { isComputer = value; }
      }

      /// <summary>
      /// Get or set the level of the player.  This is only relevant for computer controlled players
      /// </summary>
      public int Level
      {
         get { return level; }
         set { level = value; }
      }

      /// <summary>
      /// Get or set the move timeout of the player.  This is only relevant for computer controlled players
      /// </summary>
      public TimeSpan Timeout
      {
         get { return timeout; }
         set { timeout = value; }
      }

      /// <summary>
      /// Get or set the move engine of the player.  This is only relevant for computer controlled players
      /// </summary>
      public IEngine Engine
      {
         get { return engine; }
         set { engine = value; }
      }

      /// <summary>
      /// Generate a move for the player given the specified board state
      /// </summary>
      /// <param name="board">The board state</param>
      /// <returns>The generated move</returns>
      /// <exception cref="System.InvalidOperationException">If the player doesn't have an engine associated with it</exception>
      public Move GenerateMove(IBoard board)
      {
         if (engine == null)
         {
            throw new InvalidOperationException("Engine not available for player");
         }

         return engine.GenerateMove(board, player, level, timeout);
      }
   }
}
