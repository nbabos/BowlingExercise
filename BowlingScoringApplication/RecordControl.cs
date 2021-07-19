using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BowlingScoringApplication
{
    public partial class RecordControl : UserControl
    {
        #region Fields
        public int RecordID { get; private set; }
        private List<FrameControl> frameControls = new List<FrameControl>();
        #endregion

        #region Constructors
        public RecordControl(int RecordID)
        {
            this.RecordID = RecordID;
            InitializeComponent();
            LoadFrames();
        }
        #endregion

        #region Public Methods
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
                    ShotStart = ShotIndex + 1; //Double check this.
                }
                for (int j = ShotStart; j < shotChars.Length && outputList.Count < ShotCount; j++)
                {
                    outputList.Add(frameControls[i].ShotChars[j]);
                }
            }

            return outputList.ToArray();
        }

        public void SetFocusToNextFrame(int FrameIndex)
        {
            if (FrameIndex < GameManager.FRAMESPERGAME)
            {
                frameControls[FrameIndex].Enabled = true;
                frameControls[FrameIndex].Focus();
                //Highlight frame by setting its color to a little brighter.
                //Unhighlight the previous frame.
            }
            
        }

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
        private void LoadFrames()
        {
            for (int i = 0; i < GameManager.FRAMESPERGAME; i++)
            {
                FrameControl frameControl = new FrameControl(this, i + 1);
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
        }
        #endregion
    }
}
