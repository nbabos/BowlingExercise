using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BowlingScoringApplication
{
    public partial class InputInstructionControl : UserControl
    {
        public InputInstructionControl()
        {
            InitializeComponent();
            ShowValidShotInput();
        }

        public void ShowValidShotInput()
        {
            this.Visible = true;
            char[] validChars = GameManager.GetValidScoreChars(0, 0, 10, '-');
            string Instructions = "Please enter one of the following: ";
            for (int i = 0; i < validChars.Length; i++)
            {
                Instructions += validChars[i].ToString() + ", ";
            }
            Instructions = Instructions.TrimEnd(' ', ',');
            lblInstruction.Text = Instructions;
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
