namespace Okorodudu.Checkers.View.Providers.Win
{
   partial class CheckersForm
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
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckersForm));
         this.statusStrip = new System.Windows.Forms.StatusStrip();
         this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.turnIndicatorLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.boardPanel2D1 = new Okorodudu.Checkers.View.Providers.Win.Controls.BoardPanel2D();
         this.statusStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // statusStrip
         // 
         this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.turnIndicatorLabel});
         this.statusStrip.Location = new System.Drawing.Point(0, 400);
         this.statusStrip.Name = "statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(401, 22);
         this.statusStrip.SizingGrip = false;
         this.statusStrip.TabIndex = 1;
         this.statusStrip.Text = "statusStrip1";
         // 
         // statusLabel
         // 
         this.statusLabel.Name = "statusLabel";
         this.statusLabel.Size = new System.Drawing.Size(370, 17);
         this.statusLabel.Spring = true;
         this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // turnIndicatorLabel
         // 
         this.turnIndicatorLabel.Image = global::Okorodudu.Checkers.View.Providers.Win.Properties.Resources.BlackTurnIndicator;
         this.turnIndicatorLabel.Name = "turnIndicatorLabel";
         this.turnIndicatorLabel.Size = new System.Drawing.Size(16, 17);
         // 
         // boardPanel2D1
         // 
         this.boardPanel2D1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
         this.boardPanel2D1.Location = new System.Drawing.Point(0, -1);
         this.boardPanel2D1.MinimumSize = new System.Drawing.Size(100, 100);
         this.boardPanel2D1.Name = "boardPanel2D1";
         this.boardPanel2D1.Size = new System.Drawing.Size(400, 400);
         this.boardPanel2D1.TabIndex = 0;
         // 
         // CheckersForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(401, 422);
         this.Controls.Add(this.statusStrip);
         this.Controls.Add(this.boardPanel2D1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "CheckersForm";
         this.Text = "Sharp Checkers";
         this.statusStrip.ResumeLayout(false);
         this.statusStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private Okorodudu.Checkers.View.Providers.Win.Controls.BoardPanel2D boardPanel2D1;
      private System.Windows.Forms.StatusStrip statusStrip;
      private System.Windows.Forms.ToolStripStatusLabel statusLabel;
      private System.Windows.Forms.ToolStripStatusLabel turnIndicatorLabel;
   }
}