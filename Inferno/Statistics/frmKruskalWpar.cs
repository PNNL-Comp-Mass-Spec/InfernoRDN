using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmKruskalWpar : Form
    {
        private string sfactor = "";
        private readonly clsKruskalWPar mclsKruskalWPar;

        public frmKruskalWpar(clsKruskalWPar clsKW)
        {
            InitializeComponent();
            mclsKruskalWPar = clsKW;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactors.SelectedIndex < 0)
                MessageBox.Show("Select a Factor to Perform Kruskal-Wallis Test", "Incomplete Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void mlstBoxFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            sfactor = mlstBoxFactors.SelectedItem.ToString();
        }

        private void frmKruskalWpar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsKruskalWPar.mstrDatasetName;
            if (mlstBoxFactors.Items.Count > 0)
                mlstBoxFactors.SelectedIndex = mclsKruskalWPar.nF;
            mNumUpDthres.Value = mclsKruskalWPar.numDatapts;
            //mrtBox.Rtf = @"{\rtf1\ansi \ul Kruskal-Wallis Test\ul0  is the non parametric equivalent" + 
            //    "of One-way ANOVA. The normality assumption is relaxed.}";
        }

        #region Properties

        public clsKruskalWPar clsKruskalWPar
        {
            get
            {
                mclsKruskalWPar.selectedFactor = this.SelectedFactor;
                mclsKruskalWPar.numDatapts = this.NumDataThresh;
                if (mlstBoxFactors.SelectedIndex != -1)
                    mclsKruskalWPar.nF = mlstBoxFactors.SelectedIndex;

                return mclsKruskalWPar;
            }
        }

        public List<string> PopulateListBox
        {
            set => mlstBoxFactors.DataSource = value;
        }

        public string SelectedFactor => sfactor;

        public int NumDataThresh
        {
            get
            {
                int n = Convert.ToInt16(mNumUpDthres.Value);
                return (n);
            }
        }

        #endregion
    }
}