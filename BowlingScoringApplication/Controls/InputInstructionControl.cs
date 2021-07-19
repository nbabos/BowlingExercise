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
        #endregion
        #region Constructors
        public InputInstructionControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        public void UpdateInstructions(RecordControl RecordControl, int FrameNumber, int ShotIndex, char PrevShotChar, bool ForceOpen)
        {
            if (ForceOpen || !ClosedManually) //Only show if the scorekeeper did not close the instructions manually or an invalid shot was entered.
            {
                this.Visible = true;
                ClosedManually = false;
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

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ClosedManually = true;
        }
        #endregion
    }
}
