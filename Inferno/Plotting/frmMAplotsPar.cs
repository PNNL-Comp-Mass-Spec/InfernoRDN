using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmMAplotsPar : Form
    {
        private clsMAplotsPar mclsMApar = new clsMAplotsPar();
        private int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT_CPU_INTENSIVE;
        private ArrayList marrDatasets = new ArrayList();
        string dataColor = "#00FF00", lColor = "#FF0000";
        private bool mWarnedTooManyDatasets = false;
        private bool mPopulating = false;

        public frmMAplotsPar(clsMAplotsPar clsMApar)
        {
            mclsMApar = clsMApar;
            InitializeComponent();
        }


        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.CheckedIndices.Count < 2)
            {
                MessageBox.Show("Select atleast two datasets.", "Select datasets");
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
            int N = mlstViewDataSets.Items.Count > MAX ? N = MAX : N = mlstViewDataSets.Items.Count; 
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

            if (mlstViewDataSets.Items.Count > MAX && checkStateNew)
            {
                if (!mWarnedTooManyDatasets)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("This will select too many datasets to be plotted on one page." +
                                    Environment.NewLine + "Therefore, total selected set to " + MAX.ToString() + ".",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mbtnDataC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.mlblDC.BackColor = hexColorDialog.Color;
                this.mlblDC.ForeColor = hexColorDialog.Color;
                dataColor = clsHexColorUtil.ColorToHex(this.hexColorDialog.Color);
                Settings.Default.maplotDcol = dataColor;
                Settings.Default.Save();
            }
        }

        private void mbtnLineC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                this.mlblLC.BackColor = hexColorDialog.Color;
                this.mlblLC.ForeColor = hexColorDialog.Color;
                lColor = clsHexColorUtil.ColorToHex(this.hexColorDialog.Color);
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
                    Environment.NewLine + "Maximum suggested is " + MAX.ToString() + ".",
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
            this.mlblDC.BackColor = clsHexColorUtil.HexToColor(dataColor);
            this.mlblDC.ForeColor = clsHexColorUtil.HexToColor(dataColor);
            this.mlblLC.BackColor = clsHexColorUtil.HexToColor(lColor);
            this.mlblLC.ForeColor = clsHexColorUtil.HexToColor(lColor);

            mchkBoxTransparent.Checked = false;
            mchkBoxStamp.Checked = false;
        }

        private void FormLoad_event(object sender, EventArgs e)
        {
            dataColor = mclsMApar.dCol;
            lColor = mclsMApar.lCol;
            this.mlblDC.BackColor = clsHexColorUtil.HexToColor(dataColor);
            this.mlblDC.ForeColor = clsHexColorUtil.HexToColor(dataColor);
            this.mlblLC.BackColor = clsHexColorUtil.HexToColor(lColor);
            this.mlblLC.ForeColor = clsHexColorUtil.HexToColor(lColor);

            this.mchkBoxTransparent.Checked = mclsMApar.trBkground;
            this.PopulateListView = mclsMApar.Datasets;
            this.SelectedDatasets = mclsMApar.CheckedDatasets;
            this.DataSetName = mclsMApar.mstrDatasetName;
            mchkBoxStamp.Checked = mclsMApar.stamp;
        }

        #region Properties

        public clsMAplotsPar clsMAplotPar
        {
            get
            {
                mclsMApar.datasubset = "c(" + Selected + ")";
                mclsMApar.trBkground = this.Background;
                mclsMApar.CheckedDatasets = this.SelectedDatasets;
                mclsMApar.dCol = this.dataColor;
                mclsMApar.lCol = this.LOESSColor;
                mclsMApar.stamp = mchkBoxStamp.Checked;

                return mclsMApar;
            }
        }

        public string DataColor
        {
            get { return dataColor; }
        }

        public string LOESSColor
        {
            get { return lColor; }
        }


        public ArrayList PopulateListView
        {
            set
            {
                marrDatasets = value;
                ListViewItem[] lstVcolln = new ListViewItem[marrDatasets.Count];
                var countChecked = 0;

                for (int i = 0; i < marrDatasets.Count; i++)
                {
                    ListViewItem lstVItem = new ListViewItem(marrDatasets[i].ToString())
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
                ListView.CheckedIndexCollection indexes = mlstViewDataSets.CheckedIndices;
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
                        if (k == MAX)
                            break;
                    }
                }
                return selected;
            }
        }

        public ArrayList SelectedDatasets
        {
            get
            {
                ArrayList selectedDS = new ArrayList();
                ListView.CheckedIndexCollection indexes = mlstViewDataSets.CheckedIndices;
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
                ArrayList selectedDS = value;
                for (int i = 0; i < mlstViewDataSets.Items.Count; i++)
                    for (int j = 0; j < selectedDS.Count; j++)
                    {
                        if (selectedDS[j].ToString().Equals(mlstViewDataSets.Items[i].ToString()))
                            mlstViewDataSets.Items[i].Checked = true;
                    }
            }
        }

        public bool Background
        {
            get
            {
                return mchkBoxTransparent.Checked;
            }
        }

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }
        #endregion
    }
}