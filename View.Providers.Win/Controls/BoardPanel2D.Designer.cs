namespace Okorodudu.Checkers.View.Providers.Win.Controls
{
   partial class BoardPanel2D
   {
      /// <summary> 
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }

         boardPainter.Dispose();
         moveAnimator.Dispose();
         gameMessageFont.Dispose();
         gameMessageBrush.Dispose();

         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.SuspendLayout();
         // 
         // BoardPanel2D
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
         this.DoubleBuffered = true;
         this.MinimumSize = new System.Drawing.Size(100, 100);
         this.Name = "BoardPanel2D";
         this.Size = new System.Drawing.Size(400, 400);
         this.ResumeLayout(false);

      }

      #endregion
   }
}
