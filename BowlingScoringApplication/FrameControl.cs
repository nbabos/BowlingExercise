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
        #endregion

        #region Constructors
        public FrameControl(RecordControl RecordControl, int FrameNumber)
        {
            this.ParentRecordControl = RecordControl;
            this.FrameNumber = FrameNumber;
            InitializeComponent();
            lblPoints.Visible = false;
            ShotsPerFrame = FrameNumber < GameManager.FRAMESPERGAME ? 2 : 3;
            LoadControls();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void LoadControls()
        {
            //Textboxes

            int shotOffset = FrameNumber < GameManager.FRAMESPERGAME ? 1 : 0;
            int textBoxWidth = this.Width / 3;
            Font font = new Font(FontFamily.GenericSansSerif, 20);

            for (int i = 0; i < ShotsPerFrame; i++)
            {
                TextBox textBox = new TextBox();
                textBox.Width = textBoxWidth;
                textBox.Height = textBoxWidth;
                textBox.Left = textBox.Width * (i + shotOffset);
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = font;
                textBox.MaxLength = 1;
                textBox.TextAlign = HorizontalAlignment.Center;
                textBox.CharacterCasing = CharacterCasing.Upper;
                textBox.TextChanged += txtBox_TextChanged;

                Controls.Add(textBox);
                textBoxes.Add(textBox);
            }

            EnableTextBoxes();
        }

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
                CalculatePoints(shotIndex);
                ParentRecordControl.CalculateFramePoints(FrameNumber - 3, FrameNumber - 1);
            }
            else
            {
                txtBox.Clear();
            }
        }

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

        private bool ValidateEntry(TextBox TxtBox, int ShotIndex)
        {
            bool isValid = false;

            int pinsStanding = GameManager.PINSPERFRAME;
            char prevShotChar = '-';
            if (ShotIndex > 0)
            {
                prevShotChar = char.Parse(textBoxes[ShotIndex - 1].Text);
                if (FrameNumber < GameManager.FRAMESPERGAME && GameManager.ScoreLegendDict[prevShotChar] < GameManager.PINSPERFRAME)
                {
                    pinsStanding = pinsStanding - GameManager.ScoreLegendDict[prevShotChar];
                }
            }

            char[] validChars = GameManager.GetValidScoreChars(FrameNumber, ShotIndex, pinsStanding, prevShotChar);

            if (TxtBox.Text.ToUpper().IndexOfAny(validChars) >= 0)
            {
                isValid = true;

            }
            return isValid;
        }
        private void RevealScore()
        {
            lblPoints.Text = ParentRecordControl.GetCumulativePoints(FrameNumber).ToString();
            lblPoints.Visible = PointsCalculated;
        }

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
        #endregion

    }
}
