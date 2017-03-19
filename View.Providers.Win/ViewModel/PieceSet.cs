using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Okorodudu.Checkers.View.Providers.Win.ViewModel
{
    internal class PieceSet : IPieceSet
    {
        private readonly string name;
        private readonly Image blackMan;
        private readonly Image whiteMan;
        private readonly Image blackKing;
        private readonly Image whiteKing;
        private readonly Image darkSquare;
        private readonly Image lightSquare;

        public PieceSet(string name, Image blackMan, Image whiteMan, Image blackKing, Image whiteKing, Image darkSquare, Image lightSquare)
        {
            this.name = name;
            this.blackMan = blackMan;
            this.whiteMan = whiteMan;
            this.blackKing = blackKing;
            this.whiteKing = whiteKing;
            this.darkSquare = darkSquare;
            this.lightSquare = lightSquare;
        }

        /// <summary>
        /// Get the name of the piece set
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Get black man image
        /// </summary>
        public Image BlackMan { get { return blackMan; } }

        /// <summary>
        /// Get white man image
        /// </summary>
        public Image WhiteMan { get { return whiteMan; } }

        /// <summary>
        /// Get black king image
        /// </summary>
        public Image BlackKing { get { return blackKing; } }

        /// <summary>
        /// Get white king image
        /// </summary>
        public Image WhiteKing { get { return whiteKing; } }

        /// <summary>
        /// Get dark square image
        /// </summary>
        public Image DarkSquare { get { return darkSquare; } }

        /// <summary>
        /// Get light square image
        /// </summary>
        public Image LightSquare { get { return lightSquare; } }
    }
}
