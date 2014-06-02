using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmTamuQpar : Form
    {
        private ArrayList marrColumns = new ArrayList();
        private clsTamuQPar mclsTamuQPar = new clsTamuQPar();

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
                foreach (object r in mlstBoxFactors.SelectedItems)
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
                foreach (object r in mlstBoxFixed.SelectedItems)
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

        public ArrayList PopulateListBox
        {
            set
            {
                marrColumns = value;
                int mintMaxColumns = value.Count;
                object[] lstBoxEntries = new object[mintMaxColumns];
                value.CopyTo(lstBoxEntries);
                ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxFactors, lstBoxEntries);
                mlstBoxFactors.Items.AddRange(lboxObjColData);
                //for (int i = 0; i < mlstBoxAllCols.Items.Count; i++)
                //    mlstBoxAllCols.SetSelected(i, true);
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

        #endregion

        
    }
}