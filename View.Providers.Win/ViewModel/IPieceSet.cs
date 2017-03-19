using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Okorodudu.Checkers.View.Providers.Win.ViewModel
{
   /// <summary>
   /// The interface for a piece set
   /// </summary>
    public interface IPieceSet
    {
        /// <summary>
        /// Get the name of the piece set
        /// </summary>
        string Name { get;}

        /// <summary>
        /// Get black man image
        /// </summary>
        Image BlackMan { get;}

        /// <summary>
        /// Get white man image
        /// </summary>
        Image WhiteMan { get; }

        /// <summary>
        /// Get black king image
        /// </summary>
        Image BlackKing { get;}

        /// <summary>
        /// Get white king image
        /// </summary>
        Image WhiteKing { get; }

        /// <summary>
        /// Get dark square image
        /// </summary>
        Image DarkSquare { get;}

        /// <summary>
        /// Get light square image
        /// </summary>
        Image LightSquare { get; }
    }
}
