using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmMAplotsPar : Form
    {
        private readonly clsMAplotsPar mclsMApar;

        private const int SUGGESTED_MAX = 20;
        private const int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT_CPU_INTENSIVE;

        private List<string> marrDatasets = new List<string>();
        string dataColor = "#00FF00", lColor = "#FF0000";
        private bool mWarnedTooManyDatasets;
        private bool mPopulating;

        public frmMAplotsPar(clsMAplotsPar clsMApar)
        {
            mclsMApar = clsMApar;
            InitializeComponent();
        }


        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count < 2)
            {
                MessageBox.Show("Select at least two datasets.", "Select datasets");
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

        private void mbtnDataC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                mlblDC.BackColor = hexColorDialog.Color;
                mlblDC.ForeColor = hexColorDialog.Color;
                dataColor = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                Settings.Default.maplotDcol = dataColor;
                Settings.Default.Save();
            }
        }

        private void mbtnLineC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                mlblLC.BackColor = hexColorDialog.Color;
                mlblLC.ForeColor = hexColorDialog.Color;
                lColor = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                Settings.Default.maplotLcol = lColor;
                Settings.Default.Save();
            }
        }

        private void mlstViewDataSets_ItemChecked(object sender, ItemCheckEventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count > MAX && !mWarnedTooManyDatasets && !mPopulating)
            {
                mWarnedTooManyDatasets = true;
                MessageBox.Show("You are selecting too many datasets to be plotted on one page." +
                                Environment.NewLine + "Maximum allowed is " + MAX + ".",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            dataColor = "#00FF00";
            Settings.Default.maplotDcol = dataColor;
            Settings.Default.Save();
            lColor = "#FF0000";
            Settings.Default.maplotLcol = lColor;
            Settings.Default.Save();
            mlblDC.BackColor = clsHexColorUtil.HexToColor(dataColor);
            mlblDC.ForeColor = clsHexColorUtil.HexToColor(dataColor);
            mlblLC.BackColor = clsHexColorUtil.HexToColor(lColor);
            mlblLC.ForeColor = clsHexColorUtil.HexToColor(lColor);

            mchkBoxTransparent.Checked = false;
            mchkBoxStamp.Checked = false;
        }

        private void FormLoad_event(object sender, EventArgs e)
        {
            dataColor = mclsMApar.dCol;
            lColor = mclsMApar.lCol;
            mlblDC.BackColor = clsHexColorUtil.HexToColor(dataColor);
            mlblDC.ForeColor = clsHexColorUtil.HexToColor(dataColor);
            mlblLC.BackColor = clsHexColorUtil.HexToColor(lColor);
            mlblLC.ForeColor = clsHexColorUtil.HexToColor(lColor);

            mchkBoxTransparent.Checked = mclsMApar.trBkground;
            PopulateListView = mclsMApar.Datasets;
            SelectedDatasets = mclsMApar.CheckedDatasets;
            DataSetName = mclsMApar.mstrDatasetName;
            mchkBoxStamp.Checked = mclsMApar.stamp;
        }

        #region Properties

        public clsMAplotsPar clsMAplotPar
        {
            get
            {
                mclsMApar.datasubset = "c(" + Selected + ")";
                mclsMApar.trBkground = Background;
                mclsMApar.CheckedDatasets = SelectedDatasets;
                mclsMApar.dCol = dataColor;
                mclsMApar.lCol = LOESSColor;
                mclsMApar.stamp = mchkBoxStamp.Checked;

                return mclsMApar;
            }
        }

        public string DataColor => dataColor;

        public string LOESSColor => lColor;


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

        public bool Background => mchkBoxTransparent.Checked;

        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }

        #endregion
    }
}