using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmFoldChangePar : Form
    {
        private List<clsFactorInfo> marrFactors;

        private readonly clsFoldChangePar mclsFCPar;

        public frmFoldChangePar(clsFoldChangePar mclsFCpar)
        {
            InitializeComponent();
            mclsFCPar = mclsFCpar;
        }

        private void UpdateFactorForm()
        {
            var factorNames = new List<string>();

            foreach (var factor in marrFactors)
                factorNames.Add(factor.mstrFactor);

            mcmbBoxFactors.DataSource = factorNames;
        }


        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (!mtxtBoxVal1.Text.Equals("") && !mtxtBoxVal2.Text.Equals("") &&
                !mtxtBoxVal1.Text.Equals(mtxtBoxVal2.Text))
                DialogResult = DialogResult.OK;
            else
                MessageBox.Show("Select two distinct factor values.", "Need more information",
                                MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void mBtnVal1_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactrVals.SelectedItems.Count == 1)
            {
                mtxtBoxVal1.Text = mlstBoxFactrVals.SelectedItem.ToString();
            }
        }

        private void mBtnVal2_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactrVals.SelectedItems.Count == 1)
            {
                mtxtBoxVal2.Text = mlstBoxFactrVals.SelectedItem.ToString();
            }
        }

        private void mcmbBoxFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mcmbBoxFactors.SelectedIndex > -1)
            {
                var nSelected = mcmbBoxFactors.SelectedIndex;
                mlstBoxFactrVals.Items.Clear();
                var selectedF = marrFactors[nSelected];
                if (selectedF.vCount > 0)
                {
                    for (var i = 0; i < selectedF.vCount; i++)
                    {
                        mlstBoxFactrVals.Items.Add(selectedF.marrValues[i]);
                    }
                    mlstBoxFactrVals.SelectedIndex = -1;
                }
            }
        }

        private void frmFoldChangePar_Load(object sender, EventArgs e)
        {
            this.DataSetName = mclsFCPar.mstrDatasetName;
            this.FactorInfoArray = mclsFCPar.marrFactors;
            this.mchkBoxLogScale.Checked = mclsFCPar.mbllogScale;
            UpdateFactorForm();
            this.mcmbBoxFactors.SelectedItem = 0;
        }

        public clsFoldChangePar clsFoldChangePar
        {
            get
            {
                mclsFCPar.selectedFactor = this.FactorName;
                mclsFCPar.mbllogScale = mchkBoxLogScale.Checked;
                mclsFCPar.selectedfVal1 = mtxtBoxVal1.Text;
                mclsFCPar.selectedfVal2 = mtxtBoxVal2.Text;
                return mclsFCPar;
            }
        }

        private string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public string FactorName
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    return mcmbBoxFactors.SelectedItem.ToString();
                }

                return mcmbBoxFactors.Items[0].ToString();
            }
        }

        public List<clsFactorInfo> FactorInfoArray
        {
            set { marrFactors = value; }
        }
    }
}