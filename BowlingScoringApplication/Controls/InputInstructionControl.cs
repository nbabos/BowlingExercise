using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BowlingScoringApplication
{
    /// <summary>
    /// The InputInstructionControl exists to inform the scorekeeper of which inputs are valid for entry during a given shot or frame.
    /// </summary>
    public partial class InputInstructionControl : UserControl
    {
        #region Fields
        
        /// <summary>
        /// If the scorekeeper manually closed the InputControl, hide it until the scorekeeper is requires it.
        /// </summary>
        public bool ClosedManually { get; set; }
        ScoreSheetForm scoreSheetForm;
        int DefaultWidth = 128;
        int DefaultHeight = 160;
        #endregion
        #region Constructors
        public InputInstructionControl()
        {
            InitializeComponent();
            btnHelp.Size = btnClose.Size;
        }
        #endregion

        #region Public Methods
        public void UpdateInstructions(RecordControl RecordControl, int FrameNumber, int ShotIndex, char PrevShotChar, bool ForceOpen)
        {
            if (ForceOpen || !ClosedManually) //Only show if the scorekeeper did not close the instructions manually or an invalid shot was entered.
            {
                Open();
                char[] validChars = GameManager.GetValidScoreChars(FrameNumber, ShotIndex, PrevShotChar);
                string Instructions = "";
                for (int i = 0; i < validChars.Length; i++)
                {
                    Instructions += validChars[i].ToString() + "  ";
                }
                Instructions = Instructions.TrimEnd();
                lblInstruction.Text = Instructions;
            }
            scoreSheetForm.AdjustInstructions(RecordControl);
        }
        public void SetScoreSheetForm(ScoreSheetForm ScoreSheetForm)
        {
            scoreSheetForm = ScoreSheetForm;
        }
        #endregion
        #region Private Methods
        private void Open()
        {
            this.Size = new Size(DefaultWidth, DefaultHeight);
            ClosedManually = false;

            btnClose.Visible = true;
            lblHeader.Visible = true;
            lblInstruction.Visible = true;
            //pnlHead.Visible = true;
            btnHelp.Visible = false;
        }
        private void Close()
        {
            this.Size = btnHelp.Size;
            ClosedManually = true;

            btnClose.Visible = false;
            lblHeader.Visible = false;
            lblInstruction.Visible = false;
            //pnlHead.Visible = true;
            btnHelp.Visible = true;
        }
        #endregion

        #region Events
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Open();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        
    }
}
