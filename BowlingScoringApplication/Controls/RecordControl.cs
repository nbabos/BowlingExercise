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
    /// Record Control is a User Control that references and draws each of the ten frames per record.
    /// This control handles score calculation for completed frames due to Strike and Spare rules.It also sums Cumulative points.
    /// </summary>
    public partial class RecordControl : UserControl
    {
        #region Fields
        public int RecordID { get; private set; }
        private List<FrameControl> frameControls = new List<FrameControl>();
        InputInstructionControl ucInputInstructions;
        #endregion

        #region Constructors
        public RecordControl(int RecordID, InputInstructionControl InputInstructionControl)
        {
            this.RecordID = RecordID;
            ucInputInstructions = InputInstructionControl;
            InitializeComponent();
            LoadFrames();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// GetNextShots 
        /// </summary>
        /// <param name="FrameNumber">The current frame number of the shot.</param>
        /// <param name="ShotCount">The number of shots required for calculation</param>
        /// <param name="ShotIndex">The 0-based index indicating whether the shot is the first, second, or third in the frame.</param>
        /// <returns>A char array of any subsequent shots that count toward the number of bonus shots (ShotCount) required for scoring</returns>
        public char[] GetNextShots(int FrameNumber, int ShotCount, int ShotIndex)
        {
            List<char> outputList = new List<char>();
            int FrameStart = FrameNumber == GameManager.FRAMESPERGAME ? FrameNumber - 1 : FrameNumber;

            //Loop until the required number of shots reached or the number of frames have been exceeded. Whichever is first.
            for (int i = FrameStart; outputList.Count < ShotCount && i < frameControls.Count; i++)
            {
                char[] shotChars = frameControls[i].ShotChars;
                int ShotStart = 0;
                if (FrameNumber == GameManager.FRAMESPERGAME)
                {
                    ShotStart = ShotIndex + 1;
                }
                for (int j = ShotStart; j < shotChars.Length && outputList.Count < ShotCount; j++)
                {
                    outputList.Add(frameControls[i].ShotChars[j]);
                }
            }

            return outputList.ToArray();
        }
        /// <summary>
        /// SetFocusToNextFrame directs focus to the Next Frame in the list after entering all valid shots in the previous frame.
        /// </summary>
        /// <param name="FrameIndex"></param>
        public void SetFocusToNextFrame(int FrameIndex)
        {
            for (int i = FrameIndex; i < frameControls.Count; i++)
            {
                if (!frameControls[i].IsFrameCompleted())
                {
                    frameControls[i].Enabled = true;
                    frameControls[i].Focus();

                    HighlightActiveFrame(i);
                    return;
                }
            }
        }
        /// <summary>
        /// HighlightActiveFrame restores the color of the previous frame and highlights the next frame to indicate to the scorekeeper which frame requires entry.
        /// </summary>
        /// <param name="FrameIndex"></param>
        public void HighlightActiveFrame(int FrameIndex)
        {
            for (int i = 0; i < frameControls.Count; i++)
            {
                frameControls[i].BackColor = ThemeManager.RowColors[RecordID % ThemeManager.RowColors.Length];
            }

            frameControls[FrameIndex].BackColor = ControlPaint.LightLight(frameControls[FrameIndex].BackColor);

        }
        /// <summary>
        /// CalculateFramePoints is intended to calculate the previous two frames and current frame's points in order to handle Closed Frame (strike and spare) calculations.
        /// </summary>
        /// <param name="FrameStart">The first frame to calculate</param>
        /// <param name="FrameStop">The last frame to calculate (inclusive)</param>
        public void CalculateFramePoints(int FrameStart, int FrameStop)
        {
            if (FrameStart < 0)
            {
                FrameStart = 0;
            }
            if (FrameStop > GameManager.FRAMESPERGAME)
            {
                FrameStop = GameManager.FRAMESPERGAME;
            }

            for (int i = FrameStart; i <= FrameStop; i++)
            {
                char[] shotChars = frameControls[i].ShotChars;
                if (shotChars.Length > 0)
                {
                    frameControls[i].CalculatePoints(shotChars.Length - 1);
                }
            }
        }
        public void CalculateFramePoints(int FrameStart)
        {
            if (FrameStart < 0)
            {
                FrameStart = 0;
            }

            for (int i = FrameStart; i < frameControls.Count; i++)
            {
                char[] shotChars = frameControls[i].ShotChars;
                if (shotChars.Length > 0)
                {
                    frameControls[i].CalculatePoints(shotChars.Length - 1);
                }
            }
        }
        /// <summary>
        /// Points per frame are scored cumulatively. The Record must be references in order to accurately update points based on the preceding frames.
        /// </summary>
        /// <param name="FrameNumber"></param>
        /// <returns>Returns the number of points accumulated up to the FrameNumber provided.</returns>
        public int GetCumulativePoints(int FrameNumber)
        {
            int cumulativePoints = 0;
            for (int i = 0; i < FrameNumber; i++)
            {
                cumulativePoints += frameControls[i].Points;
            }
            return cumulativePoints;
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// LoadFrames should be called after the component is initialized to programattically instantiate and position each Frame of the record.
        /// </summary>
        private void LoadFrames()
        {
            for (int i = 0; i < GameManager.FRAMESPERGAME; i++)
            {
                FrameControl frameControl = new FrameControl(this, i + 1, ucInputInstructions);
                frameControl.Size = ThemeManager.FrameControlSize;
                frameControl.Left = frameControl.Width * i;
                frameControl.BackColor = ThemeManager.RowColors[RecordID % ThemeManager.RowColors.Length];
                if (i > 0)
                {
                    frameControl.Enabled = false;
                }
                this.Width = frameControl.Left + frameControl.Width;
                this.Height = frameControl.Height;
                Controls.Add(frameControl);
                frameControls.Add(frameControl);
            }
            HighlightActiveFrame(0);
        }
        #endregion
    }
}
