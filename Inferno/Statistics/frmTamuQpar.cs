using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DAnTE.Purgatorio;

namespace DAnTE.Inferno
{
    public partial class frmTamuQpar : Form
    {
        private readonly clsTamuQPar mclsTamuQPar;

        public frmTamuQpar(clsTamuQPar clsTamuQ)
        {
            InitializeComponent();
            mclsTamuQPar = clsTamuQ;
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstBoxFixed.Items.Count == 0)
                MessageBox.Show("Select at least one Fixed Factor", "Incomplete Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            else
                this.DialogResult = DialogResult.OK;

        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void mbtnFixedSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxFactors.SelectedItems.Count > 0)
            {
                foreach (var r in mlstBoxFactors.SelectedItems)
                {
                    mlstBoxFixed.Items.Add(r);
                }
                while (mlstBoxFactors.SelectedItems.Count > 0)
                    mlstBoxFactors.Items.Remove(mlstBoxFactors.SelectedItem);
                mbtnFixedUnselect.Enabled = true;
            }
            else
                MessageBox.Show("Select Fixed Effect Factors.", "Empty selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mbtnFixedUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxFixed.SelectedItems.Count > 0)
            {
                foreach (var r in mlstBoxFixed.SelectedItems)
                {
                    mlstBoxFactors.Items.Add(r);
                }
                while (mlstBoxFixed.SelectedItems.Count > 0)
                    mlstBoxFixed.Items.Remove(mlstBoxFixed.SelectedItem);
            }
            else
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (mlstBoxFixed.Items.Count == 0)
                mbtnFixedUnselect.Enabled = false;
        }

        private void frmANOVApar_Load(object sender, EventArgs e)
        {
            mlblDataName.Text = mclsTamuQPar.mstrDatasetName;
        }

        #region Properties

        public clsTamuQPar clsTamuQPar
        {
            get
            {
                mclsTamuQPar.fixedEff = this.FixedFactors;
                return mclsTamuQPar;
            }
        }

        public List<string> PopulateListBox
        {
            set
            {
                // Note: cannot use .DataSource = Value because we .Remove items from the listbox

                mlstBoxFactors.Items.Clear();
                foreach (var item in value)
                {
                    mlstBoxFactors.Items.Add(item);
                }
            }
        }

        public List<string> FixedFactors
        {
            get
            {
                var strarrFECols = new List<string>();
                for (var i = 0; i < mlstBoxFixed.Items.Count; i++)
                {
                    strarrFECols.Add(mlstBoxFixed.Items[i].ToString());
                }
                return strarrFECols;
            }
        }

        #endregion

        
    }
}