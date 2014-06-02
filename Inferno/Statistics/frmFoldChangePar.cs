using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmFoldChangePar : Form
    {
        private const int MAX_LEVELS = 100;
        private ArrayList marrFactors;
        private string[] strarrFactors = new string[MAX_LEVELS];
        private int numFactors = 0;
        private clsFoldChangePar mclsFCPar = new clsFoldChangePar();

        public frmFoldChangePar(clsFoldChangePar mclsFCpar)
        {
            InitializeComponent();
            mclsFCPar = mclsFCpar;
        }

        private void updateFactorForm()
        {
            ArrayList marrFs = new ArrayList();

            fillFactorArray();

            for (int num = 0; num < marrFactors.Count; num++)
                marrFs.Add(((clsFactorInfo)marrFactors[num]).mstrFactor);

            mcmbBoxFactors.DataSource = marrFs;
        }

        private void fillFactorArray()
        {
            for (int num = 0; num < marrFactors.Count; num++)
                strarrFactors[num] = ((clsFactorInfo)marrFactors[num]).mstrFactor;
            numFactors = marrFactors.Count;
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
            int nSelected;
            string[] strArrTmp = new string[MAX_LEVELS];
            clsFactorInfo selectedF = new clsFactorInfo();

            if (mcmbBoxFactors.SelectedIndex > -1)
            {
                nSelected = mcmbBoxFactors.SelectedIndex;
                mlstBoxFactrVals.Items.Clear();
                selectedF = ((clsFactorInfo)marrFactors[nSelected]);
                if (selectedF.vCount > 0)
                {
                    for (int i = 0; i < selectedF.vCount; i++)
                    {
                        mlstBoxFactrVals.Items.Add(selectedF.marrValues[i].ToString());
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
            updateFactorForm();
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
            set
            {
                mlblDataName.Text = value;
            }
        }
                
        private ArrayList PopulateFactorComboBox
        {
            set
            {
                mcmbBoxFactors.DataSource = value;
            }
        }
        
        public string FactorName
        {
            get
            {
                if (mcmbBoxFactors.SelectedItem != null)
                {
                    return mcmbBoxFactors.SelectedItem.ToString();
                }
                else
                    return mcmbBoxFactors.Items[0].ToString();
            }
        }

        public ArrayList FactorInfoArray
        {
            set
            {
                marrFactors = value;
            }
        }
        
    }
}