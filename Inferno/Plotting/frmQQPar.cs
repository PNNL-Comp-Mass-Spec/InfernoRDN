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
    public partial class frmQQPar : Form
    {
        private int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT;
        private int numCol;
        private ArrayList marrDatasets = new ArrayList();
        string foreC = "#FFC38A", borderC = "#5FAE27", lineC = "#FF0000";
        private readonly clsQQPar mclsQQPar;
        private bool mWarnedTooManyDatasets = false;
        private bool mPopulating = false;

        public frmQQPar(clsQQPar clsQQPar)
        {
            mclsQQPar = clsQQPar;
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool valid = true;
            double shape, scale, exprate;
            int df;

            try
            {
                numCol = Convert.ToInt32(mtxtPlotCols.Text);
                shape = Double.Parse(mtxtBoxShape.Text);
                scale = Double.Parse(mtxtBoxScale.Text);
                df = Int16.Parse(mtxtBoxDf.Text);
                exprate = Double.Parse(mtxtBoxExp.Text);
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

            if (mlstViewDataSets.Items.Count > MAX && checkStateNew && !mPopulating)
            {
                mtxtPlotCols.Text = "5";
                if (!mWarnedTooManyDatasets)
                {
                    mWarnedTooManyDatasets = true;
                    MessageBox.Show("This will select too many datasets to be plotted on one page." +
                                    Environment.NewLine + "Therefore, total selected set to " + MAX.ToString() + ".",
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
                Settings.Default.qqForeCol = foreC;
                Settings.Default.Save();
                mclsQQPar.Fcol = foreC;
            }
        }

        private void mbtnBorderC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                borderC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblBC.BackColor = hexColorDialog.Color;
                this.mlblBC.ForeColor = hexColorDialog.Color;
                Settings.Default.qqBkCol = borderC;
                Settings.Default.Save();
                mclsQQPar.Bcol = borderC;
            }
        }

        private void mbtnLineC_Click(object sender, EventArgs e)
        {
            if (hexColorDialog.ShowDialog() == DialogResult.OK)
            {
                lineC = clsHexColorUtil.ColorToHex(hexColorDialog.Color);
                this.mlblLC.BackColor = hexColorDialog.Color;
                this.mlblLC.ForeColor = hexColorDialog.Color;
                Settings.Default.qqLCol = lineC;
                Settings.Default.Save();
                mclsQQPar.Lcol = lineC;
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
                                    Environment.NewLine + "Maximum suggested is " + MAX.ToString() + ".",
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

        private void mrBtnStudent_CheckedChanged(object sender, EventArgs e)
        {
            mtxtBoxDf.Enabled = mrBtnStudent.Checked;
        }

        private void mrBtnWeibull_CheckedChanged(object sender, EventArgs e)
        {
            mtxtBoxScale.Enabled = mrBtnWeibull.Checked;
            mtxtBoxShape.Enabled = mrBtnWeibull.Checked;
        }

        private void mrBtnExp_CheckedChanged(object sender, EventArgs e)
        {
            mtxtBoxExp.Enabled = mrBtnExp.Checked;
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            foreC = "#C0C0C0";
            borderC = "#000000";
            Settings.Default.qqForeCol = foreC;
            Settings.Default.qqBkCol = borderC;
            Settings.Default.Save();
            this.mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            this.mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
            mtxtPlotCols.Text = "2";
            mchkBoxTransparent.Checked = false;
            mrBtnNormal.Checked = true;
            mtxtBoxScale.Text = "1.0";
            mtxtBoxShape.Text = "2.0";
            mtxtBoxDf.Text = "4";
            mtxtBoxExp.Text = "1.0";
        }

        private void FormLoad_event(object sender, EventArgs e)
        {
            foreC = mclsQQPar.Fcol;
            borderC = mclsQQPar.Bcol;
            mtxtPlotCols.Text = mclsQQPar.ncolumns.ToString();
            this.mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            this.mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            this.mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
            this.mlblLC.BackColor = clsHexColorUtil.HexToColor(lineC);
            this.mlblLC.ForeColor = clsHexColorUtil.HexToColor(lineC);
            this.PopulateListView = mclsQQPar.Datasets;
            this.DataSetName = mclsQQPar.mstrDatasetName;
            this.SelectedDatasets = mclsQQPar.CheckedDatasets;
            this.ReferenceDist = mclsQQPar.reference;
            this.mtxtBoxScale.Text = mclsQQPar.wscale;
            this.mtxtBoxShape.Text = mclsQQPar.wshape;
            this.mtxtBoxDf.Text = mclsQQPar.df;
            this.mtxtBoxExp.Text = mclsQQPar.exprate;
            mtxtBoxDf.Enabled = mrBtnStudent.Checked;
            mtxtBoxScale.Enabled = mrBtnWeibull.Checked;
            mtxtBoxShape.Enabled = mrBtnWeibull.Checked;
            mtxtBoxExp.Enabled = mrBtnExp.Checked;
        }

        #region Properties

        public clsQQPar clsQQPar
        {
            get
            {
                mclsQQPar.datasubset = "c(" + Selected + ")";
                mclsQQPar.ncolumns = NumPlotColumns;
                mclsQQPar.Fcol = ForeGColor;
                mclsQQPar.Bcol = BorderColor;
                mclsQQPar.Lcol = LineColor;
                mclsQQPar.bkground = Background;
                mclsQQPar.CheckedDatasets = SelectedDatasets;
                mclsQQPar.ncolumns = Convert.ToInt16(mtxtPlotCols.Text);
                mclsQQPar.reference = this.ReferenceDist;
                mclsQQPar.wscale = this.mtxtBoxScale.Text;
                mclsQQPar.wshape = this.mtxtBoxShape.Text;
                mclsQQPar.df = this.mtxtBoxDf.Text;
                mclsQQPar.exprate = this.mtxtBoxExp.Text;

                return mclsQQPar;
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

        public string LineColor
        {
            get { return lineC; }
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

        public ArrayList PopulateListView
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

        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        private int ReferenceDist
        {
            set
            {
                switch (value)
                {
                    case 0:
                        mrBtnNormal.Checked = true;
                        break;
                    case 1:
                        mrBtnExp.Checked = true;
                        break;
                    case 2:
                        mrBtnStudent.Checked = true;
                        break;
                    case 3:
                        mrBtnWeibull.Checked = true;
                        break;
                    default:
                        mrBtnNormal.Checked = true;
                        break;
                }
            }
            get
            {
                if (mrBtnNormal.Checked)
                    return 0;
                if (mrBtnExp.Checked)
                    return 1;
                if (mrBtnStudent.Checked)
                    return 2;
                if (mrBtnWeibull.Checked)
                    return 3;
                return 0;
            }
        }
        #endregion



    }
}