using System;
using System.Drawing;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmColorPalette : Form
    {
        string lowC = null, midC = null, highC = null;
        int defaultPalette = 4;
        private System.Windows.Forms.ColorDialog hexColorDialog;

        public frmColorPalette()
        {
            this.hexColorDialog = new System.Windows.Forms.ColorDialog();
            InitializeComponent();
        }

        private void ToggleColorPalettes(bool toggle)
        {
            mrbtnHeat.Enabled = toggle;
            mrbtnRedGreen.Enabled = toggle;
            mrbtnCustom.Enabled = toggle;
            mrBtnBlackBody.Enabled = toggle;
            if (mrbtnCustom.Checked && toggle)
            {
                mbtnHigh.Enabled = toggle;
                mbtnLow.Enabled = toggle;
                mbtnMid.Enabled = toggle;
            }
            if (!toggle)
            {
                mbtnHigh.Enabled = toggle;
                mbtnLow.Enabled = toggle;
                mbtnMid.Enabled = toggle;
            }
        }

        private void mbtnLow_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                lowC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblLow.BackColor = hexColorDialog.Color;
                this.mlblLow.ForeColor = hexColorDialog.Color;
                Settings.Default.colCustLow = lowC;
                Settings.Default.Save();
            }
        }

        private void mbtnMid_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                midC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblMid.BackColor = hexColorDialog.Color;
                this.mlblMid.ForeColor = hexColorDialog.Color;
                Settings.Default.colCustMid = midC;
                Settings.Default.Save();
            }
        }

        private void mbtnHigh_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                highC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblHigh.BackColor = hexColorDialog.Color;
                this.mlblHigh.ForeColor = hexColorDialog.Color;
                Settings.Default.colCustHigh = highC;
                Settings.Default.Save();
            }
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmColorPalette_Load(object sender, EventArgs e)
        {
            lowC = Settings.Default.colCustLow;
            midC = Settings.Default.colCustMid;
            highC = Settings.Default.colCustHigh;

            if (lowC == "")
            {
                lowC = clsHexColorUtil.ColorToHex(System.Drawing.Color.FromKnownColor(KnownColor.Blue));
                Settings.Default.colCustLow = lowC;
                Settings.Default.Save();
            }
            if (midC == "")
            {
                midC = clsHexColorUtil.ColorToHex(System.Drawing.Color.FromKnownColor(KnownColor.White));
                Settings.Default.colCustMid = midC;
                Settings.Default.Save();
            }
            if (highC == "")
            {
                highC = clsHexColorUtil.ColorToHex(System.Drawing.Color.FromKnownColor(KnownColor.Red));
                Settings.Default.colCustHigh = highC;
                Settings.Default.Save();
            }
            this.mlblLow.BackColor = clsHexColorUtil.HexToColor(lowC);
            this.mlblLow.ForeColor = clsHexColorUtil.HexToColor(lowC);
            this.mlblMid.BackColor = clsHexColorUtil.HexToColor(midC);
            this.mlblMid.ForeColor = clsHexColorUtil.HexToColor(midC);
            this.mlblHigh.BackColor = clsHexColorUtil.HexToColor(highC);
            this.mlblHigh.ForeColor = clsHexColorUtil.HexToColor(highC);

            if (!mrbtnCustom.Checked)
            {
                mbtnLow.Enabled = false;
                mbtnMid.Enabled = false;
                mbtnHigh.Enabled = false;
            }
        }

        private void mrbtnCustom_CheckedChanged(object sender, EventArgs e)
        {
            mbtnLow.Enabled = mrbtnCustom.Checked;
            mbtnMid.Enabled = mrbtnCustom.Checked;
            mbtnHigh.Enabled = mrbtnCustom.Checked;
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            this.mlblLow.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Blue);
            this.mlblLow.ForeColor = System.Drawing.Color.FromKnownColor(KnownColor.Blue);
            this.mlblMid.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.White);
            this.mlblMid.ForeColor = System.Drawing.Color.FromKnownColor(KnownColor.White);
            this.mlblHigh.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Red);
            this.mlblHigh.ForeColor = System.Drawing.Color.FromKnownColor(KnownColor.Red);
            lowC = clsHexColorUtil.ColorToHex(mlblLow.BackColor);
            midC = clsHexColorUtil.ColorToHex(mlblMid.BackColor);
            highC = clsHexColorUtil.ColorToHex(mlblHigh.BackColor);
            Settings.Default.colCustLow = lowC;
            Settings.Default.colCustMid = midC;
            Settings.Default.colCustHigh = highC;
            Settings.Default.Save();

            if (!mrbtnCustom.Checked)
            {
                mbtnLow.Enabled = false;
                mbtnMid.Enabled = false;
                mbtnHigh.Enabled = false;
            }
            SetDefaultPalette = defaultPalette;
        }

        #region Properties

        public int ColorPalette
        {
            get
            {
                int cMap = 5;
                if (mrbtnRedGreen.Checked)
                    cMap = 1;
                if (mrbtnHeat.Checked)
                    cMap = 2;
                if (mrbtnCustom.Checked)
                    cMap = 3;
                if (mrBtnBlackBody.Checked)
                    cMap = 4;
                if (mrBtnBWR.Checked)
                    cMap = 5;
                return cMap;
            }
        }

        public string ColorPaletteName
        {
            get
            {
                string cMap = null;
                if (mrbtnRedGreen.Checked)
                    cMap = "Green-Red"; //1
                if (mrbtnHeat.Checked)
                    cMap = "Heat-Palette"; //2
                if (mrbtnCustom.Checked)
                    cMap = "Custom"; //3
                if (mrBtnBlackBody.Checked)
                    cMap = "Black-Body"; //4
                if (mrBtnBWR.Checked)
                    cMap = "Blue-White-Red"; //5
                return cMap;
            }
        }

        public string CustomColors
        {
            get
            {
                return @"customColors=c(""" + lowC + @""",""" + midC +
                       @""",""" + highC + @""")";
            }
        }

        public int SetDefaultPalette
        {
            set
            {
                defaultPalette = value;
                switch (value)
                {
                    case 1:
                        mrbtnRedGreen.Checked = true;
                        break;
                    case 2:
                        mrbtnHeat.Checked = true;
                        break;
                    case 3:
                        mrbtnCustom.Checked = true;
                        break;
                    case 4:
                        mrBtnBlackBody.Checked = true;
                        break;
                    case 5:
                        mrBtnBWR.Checked = true;
                        break;
                    default:
                        mrBtnBlackBody.Checked = true;
                        break;
                }
            }
        }

        #endregion
    }
}