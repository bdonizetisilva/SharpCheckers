using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Providers.Win.ViewModel;

namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   internal class MoveAnimator : IDisposable
   {
      public delegate void MoveCompletedDelegate(Move move);

      public event MoveCompletedDelegate MoveCompleted;
      private const int SLOPE = 20;
      private static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1);
      private static readonly TimeSpan ImmediateTimeout = TimeSpan.Zero;
      private readonly object SynchRoot = new object();
      private int step;
      private readonly System.Threading.Timer timer;
      private readonly TimeSpan interval;
      private readonly FloatingPiece floatingPiece;
      private BoardPanel2D owner;
      private Move move;
      private int squareSize;
      private Location currentLocation;
      private Location nextLocation;
      private int dx;
      private  int dy;
      private int destinationX;
      private bool running;

      public MoveAnimator(BoardPanel2D owner, TimeSpan interval, FloatingPiece floatingPiece)
      {
         this.owner = owner;
         this.interval = interval;
         this.floatingPiece = floatingPiece;
         timer = new System.Threading.Timer(TimerCallback);
      }


      public virtual void Dispose()
      {
         owner = null;
         timer.Dispose();
         System.GC.SuppressFinalize(this);
      }

      public bool Running
      {
         get
         {
            return running;
         }

         private set
         {
            running = value;
         }
      }

      private void SetCoordinates()
      {
         currentLocation = move.GetLocation(step);
         nextLocation = move.GetLocation(step + 1);
         dx = (nextLocation.Col < currentLocation.Col) ? SLOPE : -SLOPE;
         dy = (nextLocation.Row < currentLocation.Row) ? SLOPE : -SLOPE;
         destinationX = (BoardConstants.Rows - nextLocation.Col) * squareSize - squareSize;
         floatingPiece.X = (BoardConstants.Cols - currentLocation.Col) * squareSize - squareSize;
         floatingPiece.Y = (BoardConstants.Rows - currentLocation.Row) * squareSize - squareSize;
      }

      private void TimerCallback(object state)
      {
         lock (SynchRoot)
         {
            if (Math.Abs(floatingPiece.X - destinationX) >= SLOPE)
            {
               floatingPiece.X += dx;
               floatingPiece.Y += dy;
               owner.RefreshBoard();
            }
            else
            {
               step++;

               if (step < move.Count - 1)
               {
                  SetCoordinates();
               }
               else
               {
                  Stop();

                  if (MoveCompleted != null)
                  {
                     MoveCompleted(move);
                  }
               }
            }
         }
      }

      public void Start(Move move, int squareSize)
      {
         Stop();
         step = 0;
         
         if ((move != null) && (move.Count > 1))
         {
            Running = true;
            this.move = move;
            this.squareSize = squareSize;
            floatingPiece.Position = move[0];
            SetCoordinates();
            timer.Change(ImmediateTimeout, interval);
         }
      }

      public void Stop()
      {
         timer.Change(InfiniteTimeout, interval);
         floatingPiece.Position = FloatingPiece.INVALID_POSITION;
         Running = false;
      }
   }
}
