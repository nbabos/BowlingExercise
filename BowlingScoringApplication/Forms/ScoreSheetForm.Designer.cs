
namespace BowlingScoringApplication
{
    partial class ScoreSheetForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.ucInputInstruction = new BowlingScoringApplication.InputInstructionControl();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlHead.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.BackgroundImage = global::BowlingScoringApplication.Properties.Resources.ScorecardHeader;
            this.pnlHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHead.Controls.Add(this.pnlToolbar);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(1582, 240);
            this.pnlHead.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.Transparent;
            this.pnlToolbar.Controls.Add(this.btnMinimize);
            this.pnlToolbar.Controls.Add(this.btnExit);
            this.pnlToolbar.Location = new System.Drawing.Point(1499, 3);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(80, 44);
            this.pnlToolbar.TabIndex = 3;
            // 
            // btnMinimize
            // 
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(183)))), ((int)(((byte)(164)))));
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Image = global::BowlingScoringApplication.Properties.Resources.Icon_Minimize;
            this.btnMinimize.Location = new System.Drawing.Point(3, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(32, 32);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(183)))), ((int)(((byte)(164)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::BowlingScoringApplication.Properties.Resources.Icon_Exit;
            this.btnExit.Location = new System.Drawing.Point(45, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.ucInputInstruction);
            this.pnlBody.Controls.Add(this.btnRemove);
            this.pnlBody.Controls.Add(this.btnAdd);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 240);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1582, 713);
            this.pnlBody.TabIndex = 1;
            // 
            // ucInputInstruction
            // 
            this.ucInputInstruction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(142)))), ((int)(((byte)(166)))));
            this.ucInputInstruction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucInputInstruction.ClosedManually = false;
            this.ucInputInstruction.Location = new System.Drawing.Point(699, 471);
            this.ucInputInstruction.Name = "ucInputInstruction";
            this.ucInputInstruction.Size = new System.Drawing.Size(120, 180);
            this.ucInputInstruction.TabIndex = 2;
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Image = global::BowlingScoringApplication.Properties.Resources.Icon_Exit;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(1309, 607);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(198, 40);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove Player";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::BowlingScoringApplication.Properties.Resources.Icon_Plus;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(77, 607);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Player";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ScoreSheetForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(142)))), ((int)(((byte)(166)))));
            this.ClientSize = new System.Drawing.Size(1582, 953);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScoreSheetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scorecard";
            this.pnlHead.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnExit;
        private InputInstructionControl ucInputInstruction;
    }
}

