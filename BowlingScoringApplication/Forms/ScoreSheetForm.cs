using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BowlingScoringApplication
{
    public partial class ScoreSheetForm : Form
    {
        #region Fields
        int TopStart = 20;
        int LeftBound = 0;
        List<RecordControl> recordControls = new List<RecordControl>();
        #endregion

        #region Constructors
        public ScoreSheetForm()
        {
            InitializeComponent();
            LoadRecords();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adjust instructions sets the positions of the Instructions so that it's in line with the active record.
        /// </summary>
        /// <param name="RecordControl"></param>
        public void AdjustInstructions(RecordControl RecordControl)
        {
            ucInputInstruction.Top = RecordControl.Top;
            ucInputInstruction.Left = RecordControl.Right;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// LoadRecords should be called after the component is initialized to programattically instantiate and position the Record Headers and generate a single record.
        /// </summary>
        private void LoadRecords()
        {
            LeftBound = (pnlBody.Width - (ThemeManager.FrameControlSize.Width * GameManager.FRAMESPERGAME)) / 2;
            pnlBody.BackColor = ThemeManager.BodyColor;
            pnlHead.BackColor = ThemeManager.HeaderColor;

            for (int i = 0; i < GameManager.FRAMESPERGAME; i++)
            {
                AddHeader(i);
            }

            AddRecord(0);
            ucInputInstruction.SetScoreSheetForm(this);
            ucInputInstruction.UpdateInstructions(recordControls[0], 0, 0, '-', true);
        }
        /// <summary>
        /// AddRecprd adds a new Player's Scoresheet record.
        /// </summary>
        /// <param name="RecordIndex"></param>
        private void AddRecord(int RecordIndex)
        {
            RecordControl recordControl = new RecordControl(RecordIndex, ucInputInstruction);
            recordControl.Top = (TopStart + ThemeManager.FrameHeaderSize.Height) + (recordControl.Height * RecordIndex);
            recordControl.Left = LeftBound;
            pnlBody.Controls.Add(recordControl);
            recordControls.Add(recordControl);

            AdjustAddRemoveButtons();
            recordControl.Focus();
            if (!ucInputInstruction.Visible)
            {
                ucInputInstruction.Visible = true;
            }
        }
        /// <summary>
        /// AdjustButtons repositions the add and remove buttons to the bottom of Records. Hide them if their functionality isn't applicable.
        /// </summary>
        private void AdjustAddRemoveButtons()
        {
            int RecordBottom = TopStart + ThemeManager.FrameHeaderSize.Height;

            if (recordControls.Count > 0)
            {
                RecordBottom = recordControls[recordControls.Count - 1].Bottom;
            }
            int Top = RecordBottom;
            btnAdd.Top = Top;
            btnAdd.Left = LeftBound;

            btnRemove.Top = Top;
            btnRemove.Left = LeftBound + (ThemeManager.FrameControlSize.Width * GameManager.FRAMESPERGAME) - btnRemove.Width;

            btnAdd.Visible = true;
            btnRemove.Visible = true;
            if (recordControls.Count >= GameManager.MAXPLAYERS)
            {
                btnAdd.Visible = false;
            }
            if (recordControls.Count == 0)
            {
                btnRemove.Visible = false;
            }
        }
        /// <summary>
        /// RemoveRecord removes a Player's record.
        /// </summary>
        private void RemoveRecord()
        {
            RecordControl recordControl = recordControls[recordControls.Count - 1];
            pnlBody.Controls.Remove(recordControl);
            recordControls.Remove(recordControl);

            AdjustAddRemoveButtons();
            if (recordControls.Count > 0)
            {
                AdjustInstructions(recordControls[recordControls.Count - 1]);
            }
            else
            {
                ucInputInstruction.Visible = false;
            }
            
        }

        /// <summary>
        /// Add header configures the record header for the scoresheet.
        /// </summary>
        /// <param name="RecordIndex"></param>
        private void AddHeader(int RecordIndex)
        {
            Label lblFrameNumber = new Label();
            lblFrameNumber.Text = (RecordIndex + 1).ToString();
            lblFrameNumber.AutoSize = false;
            lblFrameNumber.Size = ThemeManager.FrameHeaderSize;
            lblFrameNumber.Top = TopStart;
            lblFrameNumber.Left = LeftBound + (RecordIndex * lblFrameNumber.Width);
            lblFrameNumber.BorderStyle = BorderStyle.FixedSingle;
            lblFrameNumber.BackColor = ThemeManager.FrameHeaderColor;
            lblFrameNumber.ForeColor = Color.White;
            lblFrameNumber.Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            lblFrameNumber.TextAlign = ContentAlignment.MiddleCenter;
            pnlBody.Controls.Add(lblFrameNumber);
        }
        #endregion

        #region Events
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRecord(recordControls.Count);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveRecord();
        }
        #endregion


    }
}
