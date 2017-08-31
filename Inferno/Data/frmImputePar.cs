using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmImputePar : Form
    {
        readonly Purgatorio.clsImputePar mclsImputePar;

        public frmImputePar(Purgatorio.clsImputePar mclsImpute)
        {
            InitializeComponent();
            mclsImputePar = mclsImpute;
        }

        private void frmImputePar_Load(object sender, EventArgs e)
        {
            if (mcmbBoxFactors.Items.Count > 0)
                mcmbBoxFactors.SelectedIndex = 0;

            var toolTipHelper = new ToolTip();
            toolTipHelper.SetToolTip(mtxtBoxnPCs, "Number of Principal Components");
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            float fThreshold;
            float svdThreshold;

            if (mtxtBoxFthres.Text.Length == 0 || mtxtBoxK.Text.Length == 0 || mtxtBoxnPCs.Text.Length == 0 ||
                mtxtBoxSVDiter.Text.Length == 0 || mtxtBoxSVDthres.Text.Length == 0)
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                if (!short.TryParse(mtxtBoxK.Text, out _))
                {
                    MessageBox.Show("Number of neighbors must be an integer; invalid value: " + mtxtBoxK.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!short.TryParse(mtxtBoxSVDiter.Text, out _))
                {
                    MessageBox.Show("SVD iterations must be an integer; invalid value: " + mtxtBoxSVDiter.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!short.TryParse(mtxtBoxnPCs.Text, out _))
                {
                    MessageBox.Show("# of PCs (principal components) must be an integer; invalid value: " + mtxtBoxnPCs.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!float.TryParse(mtxtBoxFthres.Text, out fThreshold))
                {
                    MessageBox.Show("Imputation threshold must be a number; invalid value: " + mtxtBoxFthres.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!float.TryParse(mtxtBoxSVDthres.Text, out svdThreshold))
                {
                    MessageBox.Show("SVD threshold must be a number; invalid value: " + mtxtBoxSVDthres.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!float.TryParse(mtxtBoxConst.Text, out _))
                {
                    MessageBox.Show("Substitute constant must be a number; invalid value: " + mtxtBoxConst.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data type error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if ((fThreshold < 0 || fThreshold > 50) && (!mrBtnMean.Checked || !mrBtnMedian.Checked))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("For best results imputation threshold should be around 20% (and must be between 0% and 50%)", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (svdThreshold < 0 || svdThreshold >= 1)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Iteration threshold chosen is not allowed (must be between 0 and 1)", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            mtxtBoxFthres.Text = "20";
            mtxtBoxK.Text = "10";
            mtxtBoxnPCs.Text = "5";
            mtxtBoxSVDthres.Text = "0.01";
            mtxtBoxSVDiter.Text = "100";
            mtxtBoxConst.Text = "1.0";
            mrBtnWKNN.Checked = true;
            mchkBoxNoImpute.Checked = false;
            if (mcmbBoxFactors.Items.Count > 0)
                mcmbBoxFactors.SelectedIndex = 0;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private string SelectedMode()
        {
            string mode = "mean";

            if (mtabCImpute.SelectedIndex == 1)
            {
                if (mrBtnMean.Checked)
                    mode = "mean";
                if (mrBtnMedian.Checked)
                    mode = "median";
                if (mrBtnConst.Checked)
                    mode = "const";
            }
            else
            {
                if (mrBtnRowMean.Checked)
                    mode = "rowmean";
                if (mrBtnKNN.Checked)
                    mode = "knn";
                if (mrBtnWKNN.Checked)
                    mode = "knnw";
                if (mrBtnSVD.Checked)
                    mode = "svd";
            }
            return mode;
        }

        private void mtxtBoxConst_Click(object sender, EventArgs e)
        {
            mrBtnConst.Checked = true;
        }

        #region Properties

        public Purgatorio.clsImputePar clsImputePar
        {
            get
            {
                mclsImputePar.mstrFiltCutoff = CutOff;
                mclsImputePar.mstrmode = Mode;
                mclsImputePar.mstrK = K;
                mclsImputePar.mstrNPCs = nPCs;
                mclsImputePar.mstrSVDth = SVDthres;
                mclsImputePar.mstrMaxIter = MaxSteps;
                mclsImputePar.mstrSubConst = Const;
                mclsImputePar.FactorSelected = Factor;
                mclsImputePar.mintFactorIndex = FactorIndex;
                mclsImputePar.mblNoFill = NoFill;

                return mclsImputePar;
            }
        }

        public List<string> PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (!(value[0].Equals("<All>")))
                        value.Insert(0, "<All>");
                    mcmbBoxFactors.DataSource = value;
                }
                else
                    mcmbBoxFactors.Items.Add("<All>");
            }
        }

        public string Factor1
        {
            get
            {
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    if (mcmbBoxFactors.SelectedItem.ToString().Equals("<All>"))
                        return "Factor=1";
                    if (mtabCImpute.SelectedIndex == 1 &&
                        (mrBtnConst.Checked || mrBtnMean.Checked || mrBtnMedian.Checked))
                        return "Factor=1";
                    idx = mcmbBoxFactors.SelectedIndex;
                    return "Factor=factors[" + idx.ToString() + ",]";
                }
                else
                    return "Factor=1";
            }
        }

        public string Factor
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    if (mcmbBoxFactors.SelectedItem.ToString().Equals("<All>"))
                        return "AllData";
                    return mcmbBoxFactors.SelectedItem.ToString();
                }
                else
                    return "AllData";
            }
        }

        public int FactorIndex
        {
            get
            {
                int idx = 0;
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    if (mcmbBoxFactors.SelectedItem.ToString().Equals("<All>"))
                        return -1;
                    idx = mcmbBoxFactors.SelectedIndex;
                    return idx;
                }
                else
                    return -1;
            }
        }

        public bool NoFill => mchkBoxNoImpute.Checked;

        public string DataSetName
        {
            set => mlblDataName.Text = value;
        }

        public string CutOff => mtxtBoxFthres.Text;

        public string Mode => SelectedMode();

        public string K => mtxtBoxK.Text;

        public string nPCs => mtxtBoxnPCs.Text;

        public string SVDthres => mtxtBoxSVDthres.Text;

        public string MaxSteps => mtxtBoxSVDiter.Text;

        public string Const => mtxtBoxConst.Text;

        #endregion
    }
}