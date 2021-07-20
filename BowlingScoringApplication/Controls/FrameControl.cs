using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BowlingScoringApplication
{
    /// <summary>
    /// FrameControl is a user control that handles shot input validation, entry, and storage.
    /// </summary>
    public partial class FrameControl : UserControl
    {
        #region Fields
        public RecordControl ParentRecordControl { get; private set; }
        public int FrameNumber { get; private set; }
        public int Points { get; private set; }
        public bool PointsCalculated { get; private set; }
        public int ShotsPerFrame { get; private set; }
        List<TextBox> textBoxes = new List<TextBox>();
        public char[] ShotChars { get => GetShotChars(); }
        
        InputInstructionControl ucInputInstructions;
        #endregion

        #region Constructors
        public FrameControl(RecordControl RecordControl, int FrameNumber, InputInstructionControl InputInstructionControl)
        {
            this.ParentRecordControl = RecordControl;
            this.FrameNumber = FrameNumber;
            ucInputInstructions = InputInstructionControl;
            InitializeComponent();
            lblPoints.Visible = false;
            ShotsPerFrame = FrameNumber < GameManager.FRAMESPERGAME ? 2 : 3;
            LoadControls();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        /// <summary>
        /// LoadControls should be called after the component is initialized to programattically instantiate and position the textboxes in each frame.
        /// </summary>
        private void LoadControls()
        {
            //Textboxes

            int shotOffset = FrameNumber < GameManager.FRAMESPERGAME ? 1 : 0;
            Font font = new Font(FontFamily.GenericSansSerif, 20);

            for (int i = 0; i < ShotsPerFrame; i++)
            {
                TextBox textBox = new TextBox();
                textBox.Size = ThemeManager.FrameTextBoxSize;
                textBox.Left = textBox.Width * (i + shotOffset);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = font;
                textBox.MaxLength = 1;
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.CharacterCasing = CharacterCasing.Upper;
                textBox.TextChanged += txtBox_TextChanged;
                textBox.GotFocus += txtBox_GotFocus;
                Controls.Add(textBox);
                textBoxes.Add(textBox);
            }

            EnableTextBoxes();
        }

        
        /// <summary>
        /// ReplaceEntry ensure consistency for char representation of spares (/) and misses (-) while maintaing intuitive input of the number of pins hit or an entry of '0'.
        /// </summary>
        /// <param name="TxtBox"></param>
        /// <param name="ShotIndex"></param>
        private void ReplaceEntry(TextBox TxtBox, int ShotIndex)
        {
            TxtBox.Text = TxtBox.Text.Replace("0", "-");
            if (ShotIndex > 0)
            {
                char ShotChar = char.Parse(TxtBox.Text);
                char PrevShotChar = ShotChars[ShotIndex - 1];
                if (GameManager.ScoreLegendDict[ShotChar] + GameManager.ScoreLegendDict[PrevShotChar] == GameManager.PINSPERFRAME)
                {
                    TxtBox.Text = TxtBox.Text.Replace(ShotChar, '/');
                }
            }
        }
        /// <summary>
        /// Focus next forces focus to the next TextBox or Frame so that the scorekeeper can advance without additional clicks.
        /// </summary>
        /// <param name="shotIndex"></param>
        /// <param name="shotChar"></param>
        private void FocusNext(int shotIndex, char shotChar)
        {
            if (FrameNumber < GameManager.FRAMESPERGAME && (shotIndex == ShotsPerFrame - 1 || shotChar == 'X'))
            {
                ParentRecordControl.SetFocusToNextFrame(FrameNumber);
            }
            else
            {
                if (shotIndex + 1 < ShotsPerFrame)
                {
                    textBoxes[shotIndex + 1].Focus();
                }
            }
        }
        /// <summary>
        /// CalculatePoints is the method used to Calculated an individual frame's points.
        /// </summary>
        /// <param name="ShotIndex"></param>
        public void CalculatePoints(int ShotIndex)
        {
            char[] shotChars = ShotChars;
            char shotChar = shotChars[ShotIndex];
            int BonusShotCount = GameManager.GetBonusShotCountByChar(FrameNumber, ShotIndex, shotChar);
            char[] bonusShots = ParentRecordControl.GetNextShots(FrameNumber, BonusShotCount, ShotIndex);
            if (GameManager.CanCalculateFrameScore(FrameNumber, ShotIndex, shotChars, bonusShots))
            {
                Points = GameManager.CalculateScore(FrameNumber, ShotIndex, shotChars, bonusShots);
                PointsCalculated = true;
                RevealScore();
            }
        }
        /// <summary>
        /// EnableTextBoxes ensures that the textboxes are disabled if entry should not be possible due to an open 10th frame or a first frame strike in preceding frames.
        /// </summary>
        private void EnableTextBoxes()
        {
            char[] specialChars = { 'X', '/' };
            char[] shotChars = ShotChars;

            textBoxes[textBoxes.Count - 1].Enabled = true;

            if (shotChars.Length < 1)
            {
                textBoxes[textBoxes.Count - 1].Enabled = false;
            }

            if (FrameNumber < GameManager.FRAMESPERGAME && string.Concat(shotChars).IndexOf('X') == 0)
            {
                textBoxes[textBoxes.Count - 1].Enabled = false;
            }

            if (FrameNumber == GameManager.FRAMESPERGAME && shotChars.Length > 0)
            {
                if (string.Concat(shotChars).IndexOfAny(specialChars) == -1)
                {
                    textBoxes[textBoxes.Count - 1].Enabled = false;
                }
            }
        }
        /// <summary>
        /// ValidateEntry determines whether the input is a valid char for the frame and shot.
        /// </summary>
        /// <param name="TxtBox"></param>
        /// <param name="ShotIndex"></param>
        /// <returns></returns>
        private bool ValidateEntry(TextBox TxtBox, int ShotIndex)
        {
            bool isValid = false;


            char prevShotChar = GetPreviousShotChar(ShotIndex);

            char[] validChars = GameManager.GetValidScoreChars(FrameNumber, ShotIndex, prevShotChar);

            if (TxtBox.Text.ToUpper().IndexOfAny(validChars) >= 0)
            {
                isValid = true;

            }
            return isValid;
        }
        /// <summary>
        /// RevealScore keeps the score hidden until the score has been calculated for the Frame and its dependencies.
        /// </summary>
        private void RevealScore()
        {
            lblPoints.Text = ParentRecordControl.GetCumulativePoints(FrameNumber).ToString();
            lblPoints.Visible = PointsCalculated;
        }
        /// <summary>
        /// GetShotChars returns all shots taken in the frame. Attention: A strike in any frame other than the last will only have a single char for the strike.
        /// </summary>
        /// <returns>char[] of chars representing the shots taken.</returns>
        private char[] GetShotChars()
        {
            List<char> chars = new List<char>();

            for (int i = 0; i < textBoxes.Count; i++)
            {
                if (!string.IsNullOrEmpty(textBoxes[i].Text))
                {
                    chars.Add(textBoxes[i].Text.ToArray()[0]);
                }

            }

            return chars.ToArray();
        }
        /// <summary>
        /// ShowInputInstructions opens the InputInstructions using data from the current shot.
        /// </summary>
        /// <param name="ShotIndex"></param>
        /// <param name="ForceOpen"></param>
        private void ShowInputInstructions(int ShotIndex, bool ForceOpen)
        {
            ucInputInstructions.UpdateInstructions(ParentRecordControl, FrameNumber, ShotIndex, GetPreviousShotChar(ShotIndex), ForceOpen);
        }
        /// <summary>
        /// GetPreviousShotChar returns the char representing the previous shot.
        /// </summary>
        /// <param name="ShotIndex">Index of the current shot</param>
        /// <returns>char representing the previous shot</returns>
        private char GetPreviousShotChar(int ShotIndex)
        {
            char prevShotChar = '-';
            if (ShotIndex > 0)
            {
                prevShotChar = char.Parse(textBoxes[ShotIndex - 1].Text);
            }

            return prevShotChar;
        }
        #endregion

        #region Events
        private void txtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int shotIndex = textBoxes.IndexOf(txtBox);

            if (ValidateEntry(txtBox, shotIndex))
            {
                //Determine if score can be calculated.
                ReplaceEntry(txtBox, shotIndex);
                char shotChar = char.Parse(txtBox.Text);
                EnableTextBoxes();
                FocusNext(shotIndex, shotChar);
                ParentRecordControl.CalculateFramePoints(FrameNumber - 3, FrameNumber - 1);
            }
            else
            {
                txtBox.Clear();
                
                ShowInputInstructions(shotIndex, true);
            }
        }
        private void txtBox_GotFocus(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            int shotIndex = textBoxes.IndexOf(txtBox);

            ShowInputInstructions(shotIndex, false);
        }
        #endregion
    }
}
