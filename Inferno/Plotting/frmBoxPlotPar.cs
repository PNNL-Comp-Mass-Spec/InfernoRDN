using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmBoxPlotPar : Form
    {
        private readonly clsBoxPlotPar mclsBoxPlotPar;
        private List<string> marrDatasets = new List<string>();
        string color;
        private bool mWarnedTooManyDatasets = false;
        private bool mPopulating = false;

        public frmBoxPlotPar(clsBoxPlotPar clsBoxPlotPar)
        {
            mclsBoxPlotPar = clsBoxPlotPar;
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count == 0)
            {
                MessageBox.Show("No datasets selected.", "Select datasets");
                DialogResult = DialogResult.None;
            }
            else
                DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonToggleAll_Click(object sender, EventArgs e)
        {
            var checkStateNew = clsUtilities.ToggleListViewCheckboxes(mlstViewDataSets,
                                                                      frmDAnTE.MAX_DATASETS_TO_SELECT * 3, true);

            if (mlstViewDataSets.Items.Count > frmDAnTE.MAX_DATASETS_TO_SELECT * 3 && !mPopulating &&
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


        private void mbtnColor_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                mlblColor.BackColor = hexColorDialog.Color;
                mlblColor.ForeColor = hexColorDialog.Color;
                color = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                Settings.Default.boxplotCol = color;
                Settings.Default.Save();
            }
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            color = "#CAFF70";
            Settings.Default.boxplotCol = color;
            Settings.Default.Save();
            mlblColor.BackColor = clsHexColorUtil.HexToColor(color);
            mlblColor.ForeColor = clsHexColorUtil.HexToColor(color);
            mnumUDFontSc.Value = 1.0M;
            mNumUDwidth.Value = 0.8M;
            mchkBoxOutl.Checked = true;
            mchkBoxTransparent.Checked = false;
            mchkBoxStamp.Checked = false;
        }

        private void FormLoad_event(object sender, EventArgs e)
        {
            color = mclsBoxPlotPar.color;
            mlblColor.BackColor = clsHexColorUtil.HexToColor(color);
            mlblColor.ForeColor = clsHexColorUtil.HexToColor(color);

            mnumUDFontSc.Value = mclsBoxPlotPar.fontScale;
            mNumUDwidth.Value = mclsBoxPlotPar.boxWidth;
            mchkBoxOutl.Checked = mclsBoxPlotPar.outliers;
            mchkBoxCount.Checked = mclsBoxPlotPar.showcount;
            mchkBoxTransparent.Checked = mclsBoxPlotPar.trBkground;
            PopulateListView = mclsBoxPlotPar.Datasets;
            PopulateFactorComboBox = mclsBoxPlotPar.Factors;
            SelectedDatasets = mclsBoxPlotPar.CheckedDatasets;
            mcmbBoxFactors.SelectedIndex = mclsBoxPlotPar.factorIdx;
            DataSetName = mclsBoxPlotPar.mstrDatasetName;
            mchkBoxStamp.Checked = mclsBoxPlotPar.stamp;
        }

        #region Properties

        public clsBoxPlotPar clsBoxPlotPar
        {
            get
            {
                mclsBoxPlotPar.datasubset = "c(" + Selected + ")";
                mclsBoxPlotPar.color = Color;
                mclsBoxPlotPar.trBkground = Background;
                mclsBoxPlotPar.boxWidth = BoxWidth;
                mclsBoxPlotPar.CheckedDatasets = SelectedDatasets;
                mclsBoxPlotPar.factor = Factor;
                mclsBoxPlotPar.fontScale = FontScale;
                mclsBoxPlotPar.outliers = Outliers;
                mclsBoxPlotPar.showcount = ShowCount;
                mclsBoxPlotPar.factorIdx = FactorIdx;
                mclsBoxPlotPar.stamp = mchkBoxStamp.Checked;
                return mclsBoxPlotPar;
            }
        }


        public string Color
        {
            get { return color; }
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

                    if (countChecked >= frmDAnTE.MAX_DATASETS_TO_SELECT * 3)
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
                if (indexes.Count == 0)
                    return null;

                var k = 0;
                foreach (int i in indexes)
                {
                    if (k == 0)
                        selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                    else
                        selected = selected + "," + Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                    k++;
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
                if (indexes.Count == 0)
                {
                    return selectedDS;
                }

                foreach (int i in indexes)
                {
                    selectedDS.Add(mlstViewDataSets.Items[i].ToString());
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

        public bool Outliers
        {
            get { return mchkBoxOutl.Checked; }
        }

        public bool ShowCount
        {
            get { return mchkBoxCount.Checked; }
        }

        public decimal BoxWidth
        {
            get { return mNumUDwidth.Value; }
        }

        public decimal FontScale
        {
            get { return mnumUDFontSc.Value; }
        }

        public bool Background
        {
            get
            {
                if (mchkBoxTransparent.Checked)
                    return true;
                else
                    return false;
            }
        }

        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public List<string> PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (value.Count == 0)
                        value.Add("<One Color>");
                    else if (!(value[value.Count - 1].Equals("<One Color>")))
                        value.Add("<One Color>");
                    mcmbBoxFactors.DataSource = value;
                }
                else
                    mcmbBoxFactors.Items.Add("<One Color>");
            }
        }

        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem.ToString().Equals("<One Color>"))
                    return "1";

                var idx = mcmbBoxFactors.SelectedIndex + 1;
                return "factors[" + idx.ToString() + ",]";
            }
        }

        public int FactorIdx
        {
            get { return mcmbBoxFactors.SelectedIndex; }
        }

        #endregion
    }
}