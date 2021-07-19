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
    public partial class Form1 : Form
    {
        #region Fields
        int ColumnHeaderHeight = 60;
        int ColumnHeaderWidth = 120;
        int TopStart = 20;
        int LeftBound = 0;
        List<RecordControl> recordControls = new List<RecordControl>();
        #endregion

        #region Constructors
        public Form1()
        {
            InitializeComponent();
            LoadRecords();
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        private void LoadRecords()
        {
            LeftBound = (pnlBody.Width - (ColumnHeaderWidth * GameManager.FRAMESPERGAME)) / 2;
            pnlBody.BackColor = ThemeManager.BodyColor;
            pnlHead.BackColor = ThemeManager.HeaderColor;

            for (int i = 0; i < GameManager.FRAMESPERGAME; i++)
            {
                AddHeader(i);
            }

            AddRecord(0);
        }

        private void AddRecord(int RecordIndex)
        {
            RecordControl recordControl = new RecordControl(RecordIndex);
            recordControl.Top = (TopStart + ColumnHeaderHeight) + (recordControl.Height * RecordIndex);
            recordControl.Left = LeftBound;
            pnlBody.Controls.Add(recordControl);
            recordControls.Add(recordControl);

            AdjustButtons();
            recordControl.Focus();
        }

        private void AdjustButtons()
        {
            int RecordBottom = TopStart + ColumnHeaderHeight;

            if (recordControls.Count > 0)
            {
                RecordBottom = recordControls[recordControls.Count - 1].Bottom;
            }
            int Top = RecordBottom;
            btnAdd.Top = Top;
            btnAdd.Left = LeftBound;

            btnRemove.Top = Top;
            btnRemove.Left = LeftBound + (ColumnHeaderWidth * GameManager.FRAMESPERGAME) - btnRemove.Width;

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

        private void RemoveRecord()
        {
            RecordControl recordControl = recordControls[recordControls.Count - 1];
            pnlBody.Controls.Remove(recordControl);
            recordControls.Remove(recordControl);

            AdjustButtons();
        }

        private void AddHeader(int RecordIndex)
        {
            Label lblFrameNumber = new Label();
            lblFrameNumber.Text = (RecordIndex + 1).ToString();
            lblFrameNumber.AutoSize = false;
            lblFrameNumber.Size = new Size(ColumnHeaderWidth, ColumnHeaderHeight);
            lblFrameNumber.Top = TopStart;
            lblFrameNumber.Left = LeftBound + (RecordIndex * lblFrameNumber.Width);
            lblFrameNumber.BorderStyle = BorderStyle.FixedSingle;
            lblFrameNumber.BackColor = ThemeManager.ColumnHeadColor;
            lblFrameNumber.ForeColor = Color.White;
            lblFrameNumber.Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            lblFrameNumber.TextAlign = ContentAlignment.MiddleCenter;
            pnlBody.Controls.Add(lblFrameNumber);
        }

        public static void ShowInstructions()
        {
            
        }

        private void ShowInstruction(RecordControl RecordControl)
        {
            ucInputInstruction.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRecord(recordControls.Count);
        }
        #endregion

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveRecord();
        }
    }
}
