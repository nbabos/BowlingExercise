
namespace BowlingScoringApplication
{
    partial class FrameControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPoints
            // 
            this.lblPoints.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPoints.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPoints.Location = new System.Drawing.Point(0, 70);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(120, 50);
            this.lblPoints.TabIndex = 0;
            this.lblPoints.Text = "0";
            this.lblPoints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblPoints);
            this.Name = "FrameControl";
            this.Size = new System.Drawing.Size(120, 120);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPoints;
    }
}
