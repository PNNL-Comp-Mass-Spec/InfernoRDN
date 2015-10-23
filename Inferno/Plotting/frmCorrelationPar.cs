using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmCorrelationPar : Form
    {
        private readonly clsCorrelationPar mclsCorrPar;
        private int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT_CPU_INTENSIVE;
        private List<string> marrDatasets = new List<string>();
        private string ellipseC = null;
        private string mstrPaletteName = "Black-Body", customCol = null;
        private int mintPalette;
        private bool mWarnedTooManyDatasets = false;
        private bool mPopulating = false;

        public frmCorrelationPar(clsCorrelationPar clsCorrPar)
        {
            mclsCorrPar = clsCorrPar;
            InitializeComponent();
        }
                        
        private void rangeSelect_event(object sender, EventArgs e)
        {
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count == 0)
            {
                MessageBox.Show("No datasets selected.", "Select datasets");
                this.DialogResult = DialogResult.None;
            }
            else
                this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
                
        private void buttonToggleAll_Click(object sender, System.EventArgs e)
        {
            var N = mlstViewDataSets.Items.Count;

            if (mrBtnScatter.Checked)
            {
                N = mlstViewDataSets.Items.Count > MAX ? N = MAX : N = mlstViewDataSets.Items.Count;
            }

            var checkStateNew = mlstViewDataSets.Items.Cast<ListViewItem>().All(item => !item.Checked);

            for (var i = 0; i < N; i++)
            {
                mlstViewDataSets.Items[i].Checked = checkStateNew;
            }

            if (!checkStateNew)
            {
                for (var i = N; i < mlstViewDataSets.Items.Count; i++)
                {
                    mlstViewDataSets.Items[i].Checked = false;
                }
            }

            if (mrBtnScatter.Checked && (mlstViewDataSets.Items.Count > MAX) && checkStateNew)
            {
                if (!mWarnedTooManyDatasets)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("This will select too many datasets to be plotted on one page." +
                                    Environment.NewLine + "Therefore, total selected set to " +
                                    MAX + ".",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mlstViewDataSets_ItemChecked(object sender, ItemCheckEventArgs e)
        {
            if (mrBtnScatter.Checked && (mlstViewDataSets.CheckedIndices.Count > MAX) && !mPopulating)
            {
                if (!mWarnedTooManyDatasets)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("You are selecting too many datasets to be plotted on one page." +
                                    Environment.NewLine + "Maximum suggested is " + MAX +
                                    ".",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mrbtnHeatmap_CheckedChanged(object sender, EventArgs e)
        {
            mintPalette = 4;
            mstrPaletteName = ColorPaletteName;
            mchkBoxdHist.Enabled = mrBtnScatter.Checked;
            mbtnHeatmapC.Enabled = mrbtnHeatmap.Checked;
            mlblHeatPalette.Enabled = mrbtnHeatmap.Checked;
            mpanelCorr.Enabled = mrbtnHeatmap.Checked;
        }

        private void mrbtn2Dmat_CheckedChanged(object sender, EventArgs e)
        {
            mintPalette = 5;
            mstrPaletteName = ColorPaletteName;
            mchkBoxShowCorr.Enabled = mrbtn2Dmat.Checked;
            mpanelCorr.Enabled = mrbtn2Dmat.Checked;
            mbtn2Dpalette.Enabled = mrbtn2Dmat.Checked;
            mlbl2DPalette.Enabled = mrbtn2Dmat.Checked;
        }

        private void mrBtnScatter_CheckedChanged(object sender, EventArgs e)
        {
            mchkBoxdHist.Enabled = mrBtnScatter.Checked;
            mchkboxYXLine.Enabled = mrBtnScatter.Checked;
            mchkBoxPoints.Enabled = mrBtnScatter.Checked;
            mchkBoxLoess.Enabled = mrBtnScatter.Checked;
            mpanelCorr.Enabled = !mrBtnScatter.Checked;
        }
        
        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            this.mlblEcolor.ForeColor = System.Drawing.Color.FromKnownColor(KnownColor.Green);
            this.mlblEcolor.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Green);
            ellipseC = clsHexColorUtil.ColorToHex(mlblEcolor.BackColor);
            Settings.Default.ellipseCol = ellipseC;
            Settings.Default.Save();

            mlbl2DPalette.Text = "Blue-White-Red";
            mlblHeatPalette.Text = "Black-Body";
            mrbtnHeatmap.Checked = true;
            mintPalette = 4;

            mchkBoxdHist.Checked = true;
            mchkboxYXLine.Checked = false;
            mchkBoxShowCorr.Checked = false;
            mchkBoxTransparent.Checked = false;

            mRangeBar.RangeMaximum = 20;
            mRangeBar.RangeMinimum = 0;
            mlblMax.Text = "1.0";
            mlblMin.Text = "0.0";

            mnumUDFontSc.Value = 1.0M;
            mchkBoxStamp.Checked = false;
        }

        private void mrbtnEllipse_CheckedChanged(object sender, EventArgs e)
        {
            mbtnEcol.Enabled = mrbtnEllipse.Checked;
            mpanelCorr.Enabled = !mrbtnEllipse.Checked;
        }

        private void mbtnEcol_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                ellipseC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblEcolor.BackColor = hexColorDialog.Color;
                this.mlblEcolor.ForeColor = hexColorDialog.Color;
                Settings.Default.ellipseCol = ellipseC;
                Settings.Default.Save();
            }
        }
        
        private void mRangeBar_RangeChanging(object sender, EventArgs e)
        {
            double min = (double)mRangeBar.RangeMinimum / 20;
            double max = (double)mRangeBar.RangeMaximum / 20;

            mlblMin.Text = min.ToString(CultureInfo.InvariantCulture);
            mlblMax.Text = max.ToString(CultureInfo.InvariantCulture);
        }

        private void mbtnHeatmapPalette_Click(object sender, EventArgs e)
        {
            var mfrmColPalette = new frmColorPalette
            {
                SetDefaultPalette = mintPalette
            };

            if (mfrmColPalette.ShowDialog() == DialogResult.OK)
            {
                mintPalette = mfrmColPalette.ColorPalette;
                customCol = mfrmColPalette.CustomColors;
                mstrPaletteName = mfrmColPalette.ColorPaletteName;
                mlblHeatPalette.Text = mstrPaletteName;
                Settings.Default.colorMapType = mintPalette;
                Settings.Default.colorMap = mstrPaletteName;
                Settings.Default.Save();
            }
        }

        private void mbtn2Dpalette_Click(object sender, EventArgs e)
        {
            var mfrmColPalette = new frmColorPalette
            {
                SetDefaultPalette = mintPalette
            };

            if (mfrmColPalette.ShowDialog() == DialogResult.OK)
            {
                mintPalette = mfrmColPalette.ColorPalette;
                customCol = mfrmColPalette.CustomColors;
                mstrPaletteName = mfrmColPalette.ColorPaletteName;
                mlbl2DPalette.Text = mstrPaletteName;
                Settings.Default.colorMapType = mintPalette;
                Settings.Default.colorMap = mstrPaletteName;
                Settings.Default.Save();
            }
        }

        private void frmCorrelationPar_Load(object sender, EventArgs e)
        {
            ellipseC = Settings.Default.ellipseCol;
            if (ellipseC == "")
            {
                this.mlblEcolor.ForeColor = System.Drawing.Color.FromKnownColor(KnownColor.Green);
                this.mlblEcolor.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Green);
                ellipseC = clsHexColorUtil.ColorToHex(mlblEcolor.BackColor);
                Settings.Default.ellipseCol = ellipseC;
                Settings.Default.Save();
            }
            else
            {
                this.mlblEcolor.ForeColor = clsHexColorUtil.HexToColor(ellipseC);
                this.mlblEcolor.BackColor = clsHexColorUtil.HexToColor(ellipseC);
            }

            mbtnEcol.Enabled = mrbtnEllipse.Checked;
            mchkBoxShowCorr.Enabled = mrbtn2Dmat.Checked;
            mpanelCorr.Enabled = mrbtn2Dmat.Checked || mrbtnHeatmap.Checked;
            mbtn2Dpalette.Enabled = mrbtn2Dmat.Checked;
            mbtnHeatmapC.Enabled = mrbtnHeatmap.Checked;

            mRangeBar.RangeMaximum = (int)(20 * (mclsCorrPar.maxCorr));
            mRangeBar.RangeMinimum = (int)(20 * (mclsCorrPar.minCorr));
            mlblMax.Text = mclsCorrPar.maxCorr.ToString(CultureInfo.InvariantCulture);
            mlblMin.Text = mclsCorrPar.minCorr.ToString(CultureInfo.InvariantCulture);

            this.PopulateListView = mclsCorrPar.Datasets;
            this.DataSetName = mclsCorrPar.mstrDatasetName;
            this.SelectedDatasets = mclsCorrPar.CheckedDatasets;
            mintPalette = mclsCorrPar.paletteType;
            mstrPaletteName = mclsCorrPar.palettename;
            customCol = mclsCorrPar.customCol;
            mchkBoxShowCorr.Checked = mclsCorrPar.showCorr;
            mchkBoxdHist.Checked = mclsCorrPar.plotHist;
            mchkboxYXLine.Checked = mclsCorrPar.showYXLine;
            mchkBoxPoints.Checked = mclsCorrPar.mblShowOverlap;
            mchkBoxLoess.Checked = mclsCorrPar.mblShowLoess;
            mnumUDFontSc.Value = mclsCorrPar.fontScale;
            mchkBoxTransparent.Checked = mclsCorrPar.trBckgrnd;

            mlbl2DPalette.Text = mstrPaletteName;
            mlblHeatPalette.Text = mstrPaletteName;

            switch (mclsCorrPar.RplotType)
            {
                case 1: mrBtnScatter.Checked = true; break;
                case 2:
                    mrbtnHeatmap.Checked = true;
                    mintPalette = mclsCorrPar.paletteType;
                    mstrPaletteName = mclsCorrPar.palettename;
                    mlblHeatPalette.Text = mstrPaletteName;
                    break;
                case 3: mrbtnEllipse.Checked = true; break;
                case 4:
                    mrbtn2Dmat.Checked = true;
                    mintPalette = mclsCorrPar.paletteType;
                    mstrPaletteName = mclsCorrPar.palettename;
                    mlbl2DPalette.Text = mstrPaletteName;
                    break;
                default: mrbtnHeatmap.Checked = true; break;
            }

            mchkBoxStamp.Checked = mclsCorrPar.stamp;
        }

        #region Properties

        public clsCorrelationPar clsCorrPar
        {
            get
            {
                mclsCorrPar.paletteType = ColorPalette;
                mclsCorrPar.palettename = mstrPaletteName;
                mclsCorrPar.corrRange = CorrRange;
                mclsCorrPar.customCol = CustomColors;
                mclsCorrPar.datasubset = Selected;
                mclsCorrPar.fontScale = FontScale;
                mclsCorrPar.plotHist = PlotHist;
                mclsCorrPar.RplotType = plotType;
                mclsCorrPar.showCorr = ShowCorrVals;
                mclsCorrPar.showYXLine = ShowYXLine;
                mclsCorrPar.CheckedDatasets = SelectedDatasets;
                mclsCorrPar.eColor = EColor;
                mclsCorrPar.minCorr = (decimal)mRangeBar.RangeMinimum / 20;
                mclsCorrPar.maxCorr = (decimal)mRangeBar.RangeMaximum / 20;
                mclsCorrPar.trBckgrnd = Background;
                mclsCorrPar.stamp = mchkBoxStamp.Checked;
                mclsCorrPar.mblShowOverlap = mchkBoxPoints.Checked;
                mclsCorrPar.mblShowLoess = mchkBoxLoess.Checked;

                return mclsCorrPar;
            }
        }

        public int plotType
        {
            get
            {
                if (mrBtnScatter.Checked)
                    return 1;
                else if (mrbtnHeatmap.Checked)
                    return 2;
                else if (mrbtnEllipse.Checked)
                    return 3;
                else if (mrbtn2Dmat.Checked)
                    return 4;
                else return 1;
            }
        }

        public bool PlotHist
        {
            get
            {
                if (mrBtnScatter.Checked && mchkBoxdHist.Checked)
                    return true;
                else 
                    return false;
            }
        }

        public bool Scatter
        {
            get
            {
                return (mrBtnScatter.Checked);
            }
        }

        public bool ShowYXLine
        {
            get
            {
                return mchkboxYXLine.Checked;
            }
        }

        public List<string> PopulateListView
        {
            set
            {

                marrDatasets = value;
                var lstVcolln = new ListViewItem[marrDatasets.Count];
                var countChecked = 0;
                
                for (var i = 0; i < marrDatasets.Count; i++)
                {
                    var lstVItem = new ListViewItem(marrDatasets[i])
                    {
                        Tag = i
                    };
                    lstVcolln[i] = lstVItem;
                    if (countChecked >= MAX)
                    {
                        continue;
                    }
                    lstVcolln[i].Checked = true;
                    countChecked++;
                }
                mPopulating = true;
                mlstViewDataSets.Items.AddRange(lstVcolln);
                mPopulating = false;
            }
        }
        
        public string Selected
        {
            get
            {
                string selected = null;
                var indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    int k = 0;
                    foreach (int i in indexes)
                    {
                        if (k == 0)
                            selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.
                                Items[i].Tag) + 1);
                        else
                            selected = selected + "," + Convert.ToString(Convert.ToInt16(
                                mlstViewDataSets.Items[i].Tag) + 1);
                        k++;
                        if (mrBtnScatter.Checked && (k == MAX))
                            break;
                    }
                }
                return "c(" + selected + ")";
            }
        }
        public List<string> SelectedDatasets
        {
            get
            {
                var selectedDS = new List<string>();
                var indexes = mlstViewDataSets.CheckedIndices;
                if (indexes.Count != 0)
                {
                    foreach (int i in indexes)
                    {
                        selectedDS.Add(mlstViewDataSets.Items[i].ToString());
                    }
                }
                return selectedDS;
            }
            set
            {
                var selectedDS = value;
                for (var i = 0; i < mlstViewDataSets.Items.Count; i++)
                    foreach (var datasetName in selectedDS)
                    {
                        if (datasetName.Equals(mlstViewDataSets.Items[i].ToString()))
                            mlstViewDataSets.Items[i].Checked = true;
                    }
            }
        }
        
        public int ColorPalette
        {
            get
            {
                return mintPalette;
            }
        }

        public string ColorPaletteName
        {
            get
            {
                string cMap = null;
                if (mintPalette== 1)
                    cMap = "Green-Red"; //1
                if (mintPalette == 2)
                    cMap = "Heat-Palette"; //2
                if (mintPalette == 3)
                    cMap = "Custom"; //3
                if (mintPalette == 4)
                    cMap = "Black-Body"; //4
                if (mintPalette == 5)
                    cMap = "Blue-White-Red"; //5
                return cMap;
            }
        }

        public string CustomColors
        {
            get
            {
                return customCol;
            }
        }

        public string EColor
        {
            get
            {
                return ellipseC;
            }
        }

        public decimal FontScale
        {
            get
            {
                return mnumUDFontSc.Value;
            }
        }

        public bool Background
        {
            get
            {
                return mchkBoxTransparent.Checked;
            }
        }

        public bool ShowCorrVals
        {
            get
            {
                return mchkBoxShowCorr.Checked;
            }
        }

        public string CorrRange
        {
            get
            {
                if (mpanelCorr.Enabled)
                {
                    double min = (double)mRangeBar.RangeMinimum / 20;
                    double max = (double)mRangeBar.RangeMaximum / 20;
                    return "corRange=c(" + min.ToString(CultureInfo.InvariantCulture) + "," + max.ToString(CultureInfo.InvariantCulture) + ")";
                }
                else
                    return "corRange=c(0,1)";
            }
        }

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }
                
        public frmDAnTE DAnTEinstance
        {
            set
            { }
        }
        #endregion
                
        
    }
}