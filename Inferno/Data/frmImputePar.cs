using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmImputePar : Form
    {
        readonly DAnTE.Purgatorio.clsImputePar mclsImputePar = new DAnTE.Purgatorio.clsImputePar();

        public frmImputePar(DAnTE.Purgatorio.clsImputePar mclsImpute)
        {
            InitializeComponent();
            mclsImputePar = mclsImpute;
        }

        private void frmImputePar_Load(object sender, EventArgs e)
        {
            mcmbBoxFactors.SelectedIndex = 0;
        }
        
        private void mbtnOK_Click(object sender, EventArgs e)
        {
            int k = 10, maxIter = 100, npcs = 5;
            float cutoff = 25.0f, svdthres = 0.01f, constant = 1.0f;

            if (mtxtBoxFthres.Text.Length == 0 || mtxtBoxK.Text.Length == 0 || mtxtBoxnPCs.Text.Length == 0 ||
                mtxtBoxSVDiter.Text.Length == 0 || mtxtBoxSVDthres.Text.Length == 0)
            {
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                k = Convert.ToInt16(mtxtBoxK.Text);
                maxIter = Convert.ToInt16(mtxtBoxSVDiter.Text);
                npcs = Convert.ToInt16(mtxtBoxnPCs.Text);
                cutoff = Convert.ToSingle(mtxtBoxFthres.Text, NumberFormatInfo.InvariantInfo);
                svdthres = Convert.ToSingle(mtxtBoxSVDthres.Text, NumberFormatInfo.InvariantInfo);
                constant = Convert.ToSingle(mtxtBoxConst.Text, NumberFormatInfo.InvariantInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data type error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if ((cutoff < 0 || cutoff > 50) && (!mrBtnMean.Checked || !mrBtnMedian.Checked))
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("For best results imputation threshold should be around 20%", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (svdthres < 0 || svdthres >= 1)
            {
                DialogResult = DialogResult.None;
                MessageBox.Show("Iteration threshold chosen is not allowed.", "Error", MessageBoxButtons.OK,
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

        public DAnTE.Purgatorio.clsImputePar clsImputePar
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
        
        public ArrayList PopulateFactorComboBox
        {
            set
            {
                if (value != null)
                {
                    if (!(value[0].Equals("<All>")))
                        value.Insert(0,"<All>");
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

        public bool NoFill
        {
            get
            {
                return mchkBoxNoImpute.Checked;
            }
        }
        
        public string DataSetName
        {
            set
            {
                mlblDataName.Text = value;
            }
        }

        public string CutOff
        {
            get
            {
                return mtxtBoxFthres.Text;
            }
        }

        public string Mode
        {
            get
            {
                return SelectedMode();
            }
        }

        public string K
        {
            get
            {
                return mtxtBoxK.Text;
            }
        }

        public string nPCs
        {
            get
            {
                return mtxtBoxnPCs.Text;
            }
        }

        public string SVDthres
        {
            get
            {
                return mtxtBoxSVDthres.Text;
            }
        }

        public string MaxSteps
        {
            get
            {
                return mtxtBoxSVDiter.Text;
            }
        }

        public string Const
        {
            get
            {
                return mtxtBoxConst.Text;
            }
        }

        #endregion

        
       
    }
}