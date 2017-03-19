using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Okorodudu.Checkers.View.Providers.Win
{
   /// <summary>
   /// The checkers UI
   /// </summary>
   public partial class CheckersForm : Form
   {
      /// <summary>
      /// Construct the UI
      /// </summary>
      public CheckersForm()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Invoked whenever the form is resized
      /// </summary>
      /// <param name="e"></param>
      protected override void OnResize(EventArgs e)
      {
         base.OnResize(e);
         int width = this.Width;
         int height = this.statusStrip.Top;
         this.boardPanel2D1.Width = width;
         this.boardPanel2D1.Height = height;
      }
   }
}