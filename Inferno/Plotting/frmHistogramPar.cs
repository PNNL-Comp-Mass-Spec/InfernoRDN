using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmHistogramPar : Form
    {
        private const int SUGGESTED_MAX = frmDAnTE.SUGGESTED_DATASETS_TO_SELECT;
        private const int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT;

        private int numCol, bins;
        private List<string> marrDatasets = new List<string>();
        string foreC = "#FFC38A", borderC = "#5FAE27";
        private readonly clsHistogramPar mclsHistPar;
        private bool mWarnedTooManyDatasets = false;
        private bool mPopulating = false;

        public frmHistogramPar(clsHistogramPar clsHistPar)
        {
            mclsHistPar = clsHistPar;
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool valid = true;

            try
            {
                numCol = Convert.ToInt32(mtxtPlotCols.Text);
                bins = Convert.ToInt32(mtxtBoxBins.Text) + 1;
            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show("Error:" + ex.Message, "Wrong data type");
            }
            if (mlstViewDataSets.CheckedIndices.Count == 0)
            {
                MessageBox.Show("No datasets selected.", "Select datasets");
                this.DialogResult = DialogResult.None;
            }
            else if (valid)
                this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonToggleAll_Click(object sender, System.EventArgs e)
        {
            var checkStateNew = clsUtilities.ToggleListViewCheckboxes(mlstViewDataSets, SUGGESTED_MAX, true);

            if (mlstViewDataSets.Items.Count > SUGGESTED_MAX && !mPopulating &&
                (checkStateNew != clsUtilities.eCheckState.checkNone))
            {
                if (!mWarnedTooManyDatasets)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("This will select more datasets than is recommended to be plotted on one page.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mbtnForeC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                foreC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblFC.BackColor = hexColorDialog.Color;
                this.mlblFC.ForeColor = hexColorDialog.Color;
                Settings.Default.histFore = foreC;
                Settings.Default.Save();
                mclsHistPar.Fcol = foreC;
            }
        }

        private void mbtnBorderC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                borderC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblBC.BackColor = hexColorDialog.Color;
                this.mlblBC.ForeColor = hexColorDialog.Color;
                Settings.Default.histBrdr = borderC;
                Settings.Default.Save();
                mclsHistPar.Bcol = borderC;
            }
        }

        private void mlstViewDataSets_ItemChecked(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (mlstViewDataSets.CheckedIndices.Count > MAX && !mWarnedTooManyDatasets && !mPopulating)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("You are selecting too many datasets to be plotted on one page." +
                                    Environment.NewLine + "Maximum allowed is " + MAX + ".",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                double colN = Math.Ceiling(Math.Sqrt(mlstViewDataSets.CheckedIndices.Count));
                if (colN < 1)
                    colN = 1;
                mtxtPlotCols.Text = colN.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex);
            }
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            foreC = "#FFC38A";
            borderC = "#5FAE27";
            Settings.Default.histFore = foreC;
            Settings.Default.histBrdr = borderC;
            Settings.Default.Save();
            this.mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            this.mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
            mtxtPlotCols.Text = "2";
            mchkBoxRug.Checked = true;
            mchkBoxTransparent.Checked = false;
            mtxtBoxBins.Text = "10";
            mchkBoxAutoBin.Checked = true;
        }

        private void mchkBoxAutoBin_CheckedChanged(object sender, EventArgs e)
        {
            mtxtBoxBins.Enabled = !mchkBoxAutoBin.Checked;
        }

        private void FormLoad_event(object sender, EventArgs e)
        {
            var bins = mclsHistPar.numBins - 1;
            foreC = mclsHistPar.Fcol;
            borderC = mclsHistPar.Bcol;
            mtxtBoxBins.Text = bins.ToString();
            mtxtPlotCols.Text = mclsHistPar.ncolumns.ToString();
            this.mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            this.mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
            this.PopulateListView = mclsHistPar.Datasets;
            this.DataSetName = mclsHistPar.mstrDatasetName;
            this.SelectedDatasets = mclsHistPar.CheckedDatasets;
            mchkBoxAutoBin.Checked = mclsHistPar.autoBins;
            mchkBoxStamp.Checked = mclsHistPar.stamp;
        }

        #region Properties

        public clsHistogramPar clsHistPar
        {
            get
            {
                mclsHistPar.datasubset = "c(" + Selected + ")";
                mclsHistPar.ncolumns = NumPlotColumns;
                mclsHistPar.Fcol = ForeGColor;
                mclsHistPar.Bcol = BorderColor;
                mclsHistPar.bkground = Background;

                // When true, dd tick marks
                mclsHistPar.addrug = AddRug;

                mclsHistPar.CheckedDatasets = SelectedDatasets;
                mclsHistPar.Bins = strBins;
                mclsHistPar.numBins = numBins;
                mclsHistPar.autoBins = mchkBoxAutoBin.Checked;
                mclsHistPar.stamp = mchkBoxStamp.Checked;
                mclsHistPar.ncolumns = Convert.ToInt16(mtxtPlotCols.Text);

                return mclsHistPar;
            }
        }

        public string ForeGColor
        {
            get { return foreC; }
        }

        public string BorderColor
        {
            get { return borderC; }
        }

        public int NumPlotColumns
        {
            get
            {
                int Ncols = 2;
                try
                {
                    Ncols = Convert.ToInt16(mtxtPlotCols.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid type. Reverting to default. \nError: " + ex.Message, "Type error");
                    Ncols = 2;
                }
                return Ncols;
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
                    var lstVItem = new ListViewItem(marrDatasets[i].ToString())
                    {
                        Tag = i
                    };
                    lstVcolln[i] = lstVItem;

                    if (countChecked >= SUGGESTED_MAX)
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
                    var k = 0;
                    foreach (int i in indexes)
                    {
                        if (k == 0)
                            selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.
                                                                            Items[i].Tag) + 1);
                        else
                            selected = selected + "," + Convert.ToString(Convert.ToInt16(
                                mlstViewDataSets.Items[i].Tag) + 1);
                        k++;
                        if (k == MAX)
                            break;
                    }
                }
                return selected;
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


        public string Background
        {
            get
            {
                if (mchkBoxTransparent.Checked)
                    return "bkground=\"transparent\"";
                else
                    return "bkground=\"white\"";
            }
        }

        public string AddRug
        {
            get
            {
                if (mchkBoxRug.Checked)
                    return "addRug=TRUE";
                else
                    return "addRug=FALSE";
            }
        }

        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public string strBins
        {
            get
            {
                if (mchkBoxAutoBin.Checked)
                {
                    // Set the method for auto-computing histogram bins to use Sturges' formula:
                    // cells="Sturges"
                    return clsHistogramPar.GetHistogramBinMethodCode();
                }

                return "cells=" + bins;
            }
        }

        public int numBins
        {
            get { return bins; }
        }

        public bool Stamp
        {
            get { return mchkBoxStamp.Checked; }
        }

        #endregion
    }
}