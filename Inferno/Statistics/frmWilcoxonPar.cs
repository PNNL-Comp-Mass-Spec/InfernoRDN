using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmWilcoxonPar : Form
    {
        private List<clsFactorInfo> marrFactors = new List<clsFactorInfo>();
        private bool[] ttestFactors;
        private string sfactor = "";
        private readonly clsWilcoxonPar mclsWilcoxonPar;

        public frmWilcoxonPar(clsWilcoxonPar clsWilcox)
        {
            InitializeComponent();
            mclsWilcoxonPar = clsWilcox;
        }

        private void Check4TtestFactors()
        {
            if (marrFactors.Count == 0)
            {
                ttestFactors = new bool[1];
                return;
            }

            ttestFactors = new bool[marrFactors.Count];
            for (var i = 0; i < marrFactors.Count; i++)
            {
                if (marrFactors[i].marrValues.Count == 2)
                    ttestFactors[i] = true;
                else
                    ttestFactors[i] = false;
            }
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
            var selected = mlstBoxFactors.SelectedIndex;

            if (selected == -1)
            {
                return;
            }

            Check4TtestFactors();
            if (!ttestFactors[selected])
            {
                MessageBox.Show("This factor has more than two levels." +
                                Environment.NewLine + " Select a factor with two levels to perform the Wilcoxon Test.",
                                "Factor not suitable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mlstBoxFactors.SelectedIndex = -1;
            }
            else
                sfactor = mlstBoxFactors.SelectedItem.ToString();
        }

        private void frmWilcoxonPar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsWilcoxonPar.mstrDatasetName;
            mlstBoxFactors.SelectedIndex = -1;
            //if (mlstBoxFactors.Items.Count > 0)
            //    mlstBoxFactors.SelectedIndex = mclsWilcoxonPar.nF;
            mNumUpDthres.Value = mclsWilcoxonPar.numDatapts;
            //mrTBox.SelectionStart = 0;
            //mrTBox.Rtf = @"{\rtf1\ansi \ul Wilcoxon Rank Sum Test\ul0. \par Also known as Mann-Whitney test is the " + 
            //                @"non parametric equivalent of a t-test. The normality assumption is relaxed.}";
        }

        #region Properties

        public clsWilcoxonPar clsWilcoxonPar
        {
            get
            {
                mclsWilcoxonPar.selectedFactor = this.SelectedFactor;
                mclsWilcoxonPar.numDatapts = this.NumDataThresh;
                if (mlstBoxFactors.SelectedIndex != -1)
                    mclsWilcoxonPar.nF = mlstBoxFactors.SelectedIndex;

                return mclsWilcoxonPar;
            }
        }

        public List<string> PopulateListBox
        {
            set
            {
                mlstBoxFactors.DataSource = value;
            }
        }
                
        public string SelectedFactor
        {
            get
            {
                return sfactor;
            }
        }
                
        public int NumDataThresh
        {
            get
            {
                int n = Convert.ToInt16(mNumUpDthres.Value);
                return (n);
            }
        }

        public List<clsFactorInfo> FactorList
        {
            set
            {
                marrFactors = value;
            }
        }

        #endregion

        
    }
}