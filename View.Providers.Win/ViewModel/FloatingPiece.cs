using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Okorodudu.Checkers.Model;

namespace Okorodudu.Checkers.View.Providers.Win.ViewModel
{
    /// <summary>
    /// A floating piece
    /// </summary>
    internal class FloatingPiece
    {
        public const int INVALID_POSITION = 0;
        private Point location;
        private int postion;


        /// <summary>
        /// Set the x-coordinate
        /// </summary>
        public int X
        {
            get { return location.X; }
            set { location.X = value; }
        }

        /// <summary>
        /// Set the y-coordinate
        /// </summary>
        public int Y
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        /// <summary>
        /// Get or set the position of the piece
        /// </summary>
        public int Position
        {
            get { return postion; }
            set { postion = value; }
        }

        /// <summary>
        /// Get whether the piece is active (piece has a valid position).
        /// </summary>
        public bool Active
        {
            get { return (postion > 0 && postion <= BoardConstants.LightSquareCount); }
        }

    }
}
