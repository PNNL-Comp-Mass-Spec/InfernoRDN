using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmLinRegPar : Form
    {
        readonly Purgatorio.clsLinRegrnPar mclsLinReg = new Purgatorio.clsLinRegrnPar();

        public frmLinRegPar(Purgatorio.clsLinRegrnPar o)
        {
            InitializeComponent();
            mclsLinReg = o;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            var outfolderOK = Directory.Exists(mtxtBoxFolder.Text);
            if (mchkBoxPlot.Checked && !outfolderOK)
                MessageBox.Show("Invalid Folder name", "Error!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            else
                DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void mchkBoxPlot_CheckedChanged(object sender, EventArgs e)
        {
            if (mchkBoxPlot.Checked)
            {
                mtxtBoxFolder.Enabled = true;
                mbtnSelectFolder.Enabled = true;
                mtxtBoxFolder.Text = Settings.Default.WorkingFolder;
            }
            else
            {
                mtxtBoxFolder.Enabled = false;
                mbtnSelectFolder.Enabled = false;
            }
        }

        private void mbtnSelectFolder_Click(object sender, EventArgs e)
        {
            string folderName = null;
            var folderBrowserDialog1 = new FolderBrowserDialog();
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                mtxtBoxFolder.Text = folderName;
            }
            else mtxtBoxFolder.Text = Settings.Default.WorkingFolder;
        }

        private void frmPickFactor_Load(object sender, EventArgs e)
        {
            mtxtBoxFolder.Text = Settings.Default.WorkingFolder;
            mcmbBoxFactors.DataSource = mclsLinReg.marrFactors;
            mlblDataName.Text = mclsLinReg.DataSetName;
            mrBtnMinMissing.Checked = true;
        }

        #region Properties

        public Purgatorio.clsLinRegrnPar clsLinRegPar
        {
            get
            {
                mclsLinReg.OutFolder_pub = mtxtBoxFolder.Text;
                mclsLinReg.mblPlot = mchkBoxPlot.Checked;
                mclsLinReg.mintFactorIndex = mcmbBoxFactors.SelectedIndex + 1;
                mclsLinReg.Reference_pub = this.Reference;

                mclsLinReg.mblPlot = PlotFlag;
                mclsLinReg.OutFolder_pub = OutFolder;
                mclsLinReg.mintFactorIndex = FactorIndex;
                mclsLinReg.FactorSelected = Factor;
                mclsLinReg.Reference_pub = Reference;

                //mclsLinRegPar.RDataset = selectedNode.RDatasetName;
                //mclsLinRegPar.DataSetName = mlblDataName.Text;
                //mclsLinRegPar.marrFactors = clsDataTable.DataTableRows(factorTable.mDTable);

                return mclsLinReg;
            }
        }

        public string OutFolder
        {
            get => mtxtBoxFolder.Text;
            set => mtxtBoxFolder.Text = value;
        }

        public bool DoPlotting => mchkBoxPlot.Checked;

        public bool PlotFlag => mchkBoxPlot.Checked;

        public List<string> PopulateFactorComboBox
        {
            set => mcmbBoxFactors.DataSource = value;
        }

        //[clsAnalysis("Dataset Table", "Normalization")]
        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }

        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem == null)
                    return string.Empty;

                return mcmbBoxFactors.SelectedItem.ToString();
            }
        }

        public int FactorIndex => mcmbBoxFactors.SelectedIndex + 1;

        public string Reference
        {
            get
            {
                if (mrBtnFirst.Checked)
                    return "FirstDataset";
                else if (mrBtnMedian.Checked)
                    return "MedianData";
                else if (mrBtnMinMissing.Checked)
                    return "LeastMissing";
                else
                    return "LeastMissing";
            }
        }

        #endregion
    }
}