using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DAnTE.Tools;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public partial class frmLinRegPar : Form
    {
        readonly DAnTE.Purgatorio.clsLinRegrnPar mclsLinReg = new DAnTE.Purgatorio.clsLinRegrnPar();

        public frmLinRegPar(DAnTE.Purgatorio.clsLinRegrnPar o)
        {
            InitializeComponent();
            mclsLinReg = o;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool outfolderOK = Directory.Exists(mtxtBoxFolder.Text);
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
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
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

        public DAnTE.Purgatorio.clsLinRegrnPar clsLinRegPar
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

                //mclsLinRegPar.Rdataset = mclsSelected.mstrRdatasetName;
                //mclsLinRegPar.DataSetName = mlblDataName.Text;
                //mclsLinRegPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

                return mclsLinReg;
            }
        }

        public string OutFolder
        {
            get { return mtxtBoxFolder.Text; }
            set { mtxtBoxFolder.Text = value; }
        }

        public bool DoPlotting
        {
            get { return mchkBoxPlot.Checked; }
        }

        public bool PlotFlag
        {
            get { return mchkBoxPlot.Checked; }
        }

        public ArrayList PopulateFactorComboBox
        {
            set { mcmbBoxFactors.DataSource = value; }
        }

        //[clsAnalysis("Dataset Table", "Normalization")]
        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public string Factor
        {
            get { return mcmbBoxFactors.SelectedItem.ToString(); }
        }

        public int FactorIndex
        {
            get { return mcmbBoxFactors.SelectedIndex + 1; }
        }

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