using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmQQPar : Form
    {
        private const int SUGGESTED_MAX = frmDAnTE.SUGGESTED_DATASETS_TO_SELECT;
        private const int MAX = frmDAnTE.MAX_DATASETS_TO_SELECT;

        private List<string> marrDatasets = new List<string>();
        string foreC = "#FFC38A", borderC = "#5FAE27", lineC = "#FF0000";
        private readonly clsQQPar mclsQQPar;
        private bool mWarnedTooManyDatasets;
        private bool mPopulating;

        public frmQQPar(clsQQPar clsQQPar)
        {
            mclsQQPar = clsQQPar;
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            var valid = true;

            try
            {
                var numCol = Convert.ToInt32(mtxtPlotCols.Text);
                var shape = double.Parse(mtxtBoxShape.Text, CultureInfo.InvariantCulture);
                var scale = double.Parse(mtxtBoxScale.Text, CultureInfo.InvariantCulture);
                var df = short.Parse(mtxtBoxDf.Text);
                var exprate = double.Parse(mtxtBoxExp.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                valid = false;
                MessageBox.Show("Error:" + ex.Message, "Wrong data type");
            }
            if (mlstViewDataSets.CheckedIndices.Count == 0)
            {
                MessageBox.Show("No datasets selected.", "Select datasets");
                DialogResult = DialogResult.None;
            }
            else if (valid)
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
                mtxtPlotCols.Text = "5";
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
                mlblFC.BackColor = hexColorDialog.Color;
                mlblFC.ForeColor = hexColorDialog.Color;
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
                mlblBC.BackColor = hexColorDialog.Color;
                mlblBC.ForeColor = hexColorDialog.Color;
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
                mlblLC.BackColor = hexColorDialog.Color;
                mlblLC.ForeColor = hexColorDialog.Color;
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
                                    Environment.NewLine + "Maximum allowed is " + MAX + ".",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                var colN = Math.Ceiling(Math.Sqrt(mlstViewDataSets.CheckedIndices.Count));
                if (colN < 1)
                    colN = 1;
                mtxtPlotCols.Text = colN.ToString(CultureInfo.InvariantCulture);
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
            mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
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
            mlblFC.BackColor = clsHexColorUtil.HexToColor(foreC);
            mlblFC.ForeColor = clsHexColorUtil.HexToColor(foreC);
            mlblBC.BackColor = clsHexColorUtil.HexToColor(borderC);
            mlblBC.ForeColor = clsHexColorUtil.HexToColor(borderC);
            mlblLC.BackColor = clsHexColorUtil.HexToColor(lineC);
            mlblLC.ForeColor = clsHexColorUtil.HexToColor(lineC);
            PopulateListView = mclsQQPar.Datasets;
            DataSetName = mclsQQPar.mstrDatasetName;
            SelectedDatasets = mclsQQPar.CheckedDatasets;
            ReferenceDist = mclsQQPar.reference;
            mtxtBoxScale.Text = mclsQQPar.wscale;
            mtxtBoxShape.Text = mclsQQPar.wshape;
            mtxtBoxDf.Text = mclsQQPar.df;
            mtxtBoxExp.Text = mclsQQPar.exprate;
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
                mclsQQPar.reference = ReferenceDist;
                mclsQQPar.wscale = mtxtBoxScale.Text;
                mclsQQPar.wshape = mtxtBoxShape.Text;
                mclsQQPar.df = mtxtBoxDf.Text;
                mclsQQPar.exprate = mtxtBoxExp.Text;

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
                int Ncols;
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
                    var lstVItem = new ListViewItem(marrDatasets[i])
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
                if (indexes.Count == 0)
                    return null;

                var k = 0;
                foreach (int i in indexes)
                {
                    if (k == 0)
                        selected = Convert.ToString(Convert.ToInt16(mlstViewDataSets.Items[i].Tag) + 1);
                    else
                        selected = selected + "," + Convert.ToString(Convert.ToInt16(
                            mlstViewDataSets.Items[i].Tag) + 1);
                    k++;
                    if (k == MAX)
                        break;
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