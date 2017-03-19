using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using Okorodudu.Checkers.Model;
using Okorodudu.Checkers.View.Providers.Win.ViewModel;

namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   internal class BoardPainter : IDisposable
   {
      private Font positionFont = new Font(FontFamily.GenericSerif, 7);
      private Brush positionBrush = new SolidBrush(Color.White);
      private Brush shadowBrush = new SolidBrush(Color.FromArgb(80, 50, 50, 50));
      private Image boardImage;
      private int imageInset = 3;
      private IPieceSet pieceSet;

      
      public BoardPainter(IPieceSet pieceSet)
      {
         if (pieceSet == null)
         {
            throw new ArgumentNullException("pieceSet");
         }

         this.pieceSet = pieceSet;
      }

      
      public virtual void Dispose()
      {
         this.positionFont.Dispose();
         this.positionBrush.Dispose();
         this.shadowBrush.Dispose();
         this.ClearCache();
         System.GC.SuppressFinalize(this);
      }
      

      public void ClearCache()
      {
         if (this.boardImage != null)
         {
            this.boardImage.Dispose();
         }
         this.boardImage = null;
      }

      public void Paint(Graphics g, IBoard board, int squareSize, FloatingPiece floatingPiece)
      {
         Image boardImageCopy = this.boardImage;
         if (boardImageCopy == null)
         {
            boardImageCopy = this.boardImage = CreateBoardImage(board, squareSize, floatingPiece);
         }

         g.DrawImage(boardImageCopy, 0, 0);
         Paint(g, board, squareSize, floatingPiece, false, true);
      }

      private Image CreateBoardImage(IBoard board, int squareSize, FloatingPiece floatingPiece)
      {
         int boardWidth = GetBoardWidth(squareSize);
         int boardHeight = GetBoardHeight(squareSize);
         Bitmap image = new Bitmap(boardWidth, boardHeight);
         Graphics g = Graphics.FromImage(image);
         Paint(g, board, squareSize, floatingPiece, true, false);
         return image;
      }

      private static int GetBoardWidth(int squareSize)
      {
         return BoardConstants.Cols * squareSize;
      }

      private static int GetBoardHeight(int squareSize)
      {
         return BoardConstants.Rows * squareSize;
      }

      private void Paint(Graphics g, IBoard board, int squareSize, FloatingPiece floatingPiece, bool paintBoard, bool paintPiece)
      {
         int x = 0;
         int y = 0;
         int imageSize = squareSize - imageInset * 2;
         int position = BoardConstants.LightSquareCount;
         for (int row = 0; row < BoardConstants.Rows; row++)
         {
            x = 0;

            for (int col = 0; col < BoardConstants.Rows; col++)
            {
               bool isDarkSquare = (row % 2 != col % 2);
               //
               // Draw square
               //
               if (paintBoard)
               {
                  g.DrawImage(GetSquareImage(isDarkSquare), x, y, squareSize, squareSize);
               }

               if (isDarkSquare)
               {
                  //
                  // Draw square number
                  //
                  if (paintBoard)
                  {
                     g.DrawString(position.ToString(CultureInfo.CurrentCulture), positionFont, positionBrush, x, y);
                  }
                  //
                  // Draw piece on square
                  //
                  if (paintPiece)
                  {
                     Piece piece = board[position];
                     if ((piece != Piece.None) && (floatingPiece.Position != position))
                     {
                        g.DrawImage(GetPieceImage(piece), x + imageInset, y + imageInset, imageSize, imageSize);
                     }
                  }

                  position--;
               }
               x += squareSize;
            }

            y += squareSize;
         }
         //
         // draw floating piece
         //
         if (paintPiece)
         {
            if ((floatingPiece.Active) && (BoardUtilities.IsPiece(board[floatingPiece.Position])))
            {
               int floatingPieceSize = (int)(imageSize * 1.2);
               const int shadowOffset = 6;
               Image floatingPieceImage = GetPieceImage(board[floatingPiece.Position]);
               g.FillEllipse(shadowBrush, floatingPiece.X + shadowOffset, floatingPiece.Y + shadowOffset, floatingPieceSize, floatingPieceSize);
               g.DrawImage(floatingPieceImage, floatingPiece.X + imageInset, floatingPiece.Y + imageInset, floatingPieceSize, floatingPieceSize);

            }
         } 
      }



      /// <summary>
      /// Get the image for the specified piece
      /// </summary>
      /// <param name="piece">the piece to get image for</param>
      /// <returns>The image for the piece</returns>
      private Image GetPieceImage(Piece piece)
      {
         switch (piece)
         {
            case Piece.BlackMan:
               return pieceSet.BlackMan;
            case Piece.WhiteMan:
               return pieceSet.WhiteMan;
            case Piece.BlackKing:
               return pieceSet.BlackKing;
            case Piece.WhiteKing:
               return pieceSet.WhiteKing;
            default:
               return null;
         }
      }

      /// <summary>
      /// Get the image for a square
      /// </summary>
      /// <param name="darkSquare">Is this a dark square</param>
      /// <returns>Dark square image iff <c>true</c> and light square if otherwise</returns>
      private Image GetSquareImage(bool darkSquare)
      {
         return (darkSquare) ? pieceSet.DarkSquare : pieceSet.LightSquare;
      }
   }
}
