
namespace BowlingScoringApplication
{
    partial class InputInstructionControl
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
            this.lblInstruction = new System.Windows.Forms.Label();
            this.pbExit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInstruction
            // 
            this.lblInstruction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(209)))), ((int)(((byte)(181)))));
            this.lblInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstruction.ForeColor = System.Drawing.Color.White;
            this.lblInstruction.Location = new System.Drawing.Point(0, 0);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(1200, 40);
            this.lblInstruction.TabIndex = 0;
            this.lblInstruction.Text = "Enter your shot:";
            this.lblInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbExit
            // 
            this.pbExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(209)))), ((int)(((byte)(181)))));
            this.pbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbExit.Image = global::BowlingScoringApplication.Properties.Resources.Icon_Exit;
            this.pbExit.Location = new System.Drawing.Point(1168, 0);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(32, 40);
            this.pbExit.TabIndex = 2;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            // 
            // InputInstructionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.lblInstruction);
            this.Name = "InputInstructionControl";
            this.Size = new System.Drawing.Size(1200, 40);
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.PictureBox pbExit;
    }
}
