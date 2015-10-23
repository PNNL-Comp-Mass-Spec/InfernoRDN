using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmANOVApar : Form
    {
        private readonly clsAnovaPar mclsAnovaPar;

        public frmANOVApar(clsAnovaPar clsAnova)
        {
            InitializeComponent();
            mclsAnovaPar = clsAnova;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstBoxFixed.Items.Count == 0)
                MessageBox.Show("Select at least one Fixed Factor", "Incomplete Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mchkBoxRandom.Checked && mlstBoxRandom.Items.Count == 0)
                MessageBox.Show("Select Random Effect Factor(s)", "Incomplete Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                this.DialogResult = DialogResult.OK;

        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SelectRandomEff_event(object sender, EventArgs e)
        {
            ToggleRandomEff();
            ToggleUnbalanced();
        }

        private void ToggleRandomEff()
        {
            if (mchkBoxRandom.Checked)
            {
                mlstBoxRandom.Enabled = true;
                mBtnRandomSelect.Enabled = true;
                mchkBoxREML.Enabled = true;
                if (mlstBoxRandom.Items.Count > 0)
                    mBtnRandomUnselect.Enabled = true;
            }
            else
            {
                mlstBoxRandom.Enabled = false;
                mBtnRandomSelect.Enabled = false;
                mBtnRandomUnselect.Enabled = false;
                mchkBoxREML.Enabled = false;
            }
        }

        private void ToggleUnbalanced()
        {
            if (mlstBoxFixed.Items.Count > 1)
            {
                mchkBoxUnbalanced.Enabled = true;
                mchkBoxInteractions.Enabled = true;
            }
            else
            {
                mchkBoxUnbalanced.Enabled = false;
                mchkBoxInteractions.Enabled = false;
            }
        }
        
        private void mbtnFixedSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactors.SelectedItems.Count > 0)
            {
                foreach (object r in mlstBoxFactors.SelectedItems)
                {
                    mlstBoxFixed.Items.Add(r);
                }
                while (mlstBoxFactors.SelectedItems.Count > 0)
                    mlstBoxFactors.Items.Remove(mlstBoxFactors.SelectedItem);
                mbtnFixedUnselect.Enabled = true;
                ToggleUnbalanced();
            }
            else
                MessageBox.Show("Select Fixed Effect Factors.", "Empty selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mbtnFixedUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxFixed.SelectedItems.Count > 0)
            {
                foreach (object r in mlstBoxFixed.SelectedItems)
                {
                    mlstBoxFactors.Items.Add(r);
                }
                while (mlstBoxFixed.SelectedItems.Count > 0)
                    mlstBoxFixed.Items.Remove(mlstBoxFixed.SelectedItem);
                ToggleUnbalanced();
            }
            else
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (mlstBoxFixed.Items.Count == 0)
                mbtnFixedUnselect.Enabled = false;
        }

        private void mbtnRandomSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactors.SelectedItems.Count > 0)
            {
                mlstBoxRandom.Items.Add(mlstBoxFactors.SelectedItem);
                mlstBoxFactors.Items.Remove(mlstBoxFactors.SelectedItem);
                mBtnRandomUnselect.Enabled = true;
            }
            else
                MessageBox.Show("Select Random Effect Factor(s).", "Wrong selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mbtnRandomUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxRandom.SelectedItems.Count > 0)
            {
                mlstBoxFactors.Items.Add(mlstBoxRandom.SelectedItem);
                mlstBoxRandom.Items.Remove(mlstBoxRandom.SelectedItem);
            }
            else
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (mlstBoxRandom.Items.Count == 0)
                mBtnRandomUnselect.Enabled = false;
        }

        private void frmANOVApar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsAnovaPar.mstrDatasetName;
            mchkBoxRandom.Checked = mclsAnovaPar.randomE;
            mchkBoxREML.Checked = mclsAnovaPar.useREML;
            
            ToggleRandomEff();
            ToggleUnbalanced();
        }

        #region Properties

        public clsAnovaPar clsAnovaPar
        {
            get
            {
                mclsAnovaPar.useREML = mchkBoxREML.Checked;
                mclsAnovaPar.unbalanced = mchkBoxUnbalanced.Checked;
                mclsAnovaPar.randomE = UseRandomEff;
                mclsAnovaPar.fixedEff = this.FixedFactors;
                mclsAnovaPar.randomEff = this.RandomFactors;
                mclsAnovaPar.interactions = this.Interactions;
                mclsAnovaPar.numDatapts = this.NumDataThresh;
                
                return mclsAnovaPar;
            }
        }

        public bool UseRandomEff
        {
            get
            {
                if (mchkBoxRandom.Checked && mlstBoxRandom.Items.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public List<string> PopulateListBox
        {
            set
            {
                mlstBoxFactors.DataSource = value;
            }
        }
                
        public ArrayList FixedFactors
        {
            get
            {
                ArrayList strarrFECols = new ArrayList();
                for (int i = 0; i < mlstBoxFixed.Items.Count; i++)
                {
                    strarrFECols.Add(mlstBoxFixed.Items[i].ToString());
                }
                return strarrFECols;
            }
        }

        public ArrayList RandomFactors
        {
            get
            {
                ArrayList strarrRECols = new ArrayList();
                for (int i = 0; i < mlstBoxRandom.Items.Count; i++)
                {
                    strarrRECols.Add(mlstBoxRandom.Items[i].ToString());
                }
                return strarrRECols;
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

        public bool Interactions
        {
            get
            {
                return mchkBoxInteractions.Checked;
            }
        }

        #endregion

        
    }
}