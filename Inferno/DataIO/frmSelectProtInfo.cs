using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DAnTE.Inferno
{
    public partial class frmSelectProtInfo : Form
    {
        private ArrayList marrColumns = new ArrayList();

        public frmSelectProtInfo()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.Items.Count != 1)
                MessageBox.Show("Select unique row identifier (e.g. Mass Tags)", "Incomplete Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mlstBoxIPI.Items.Count !=1)
                MessageBox.Show("Select protein identifiers", "Incomplete Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                this.DialogResult = DialogResult.OK;

        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void mbtnMTSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.Items.Count == 1)
                MessageBox.Show("You have already selected the Row IDs","Row IDs selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (mlstBoxAllCols.SelectedItems.Count == 1)
            {
                mlstBoxMT.Items.Add(mlstBoxAllCols.SelectedItem);
                mlstBoxAllCols.Items.Remove(mlstBoxAllCols.SelectedItem);
                mbtnMTUnselect.Enabled = true;
            }
            else
                MessageBox.Show("Select One Column as Unique RowID (Mass Tags).", "Empty selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mbtnMTUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.SelectedItems.Count == 1)
            {
                mlstBoxAllCols.Items.Add(mlstBoxMT.SelectedItem);
                mlstBoxMT.Items.Remove(mlstBoxMT.SelectedItem);
            }
            else
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (mlstBoxMT.Items.Count == 0)
                mbtnMTUnselect.Enabled = false;
        }

        private void mbtnIPISelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxIPI.Items.Count == 1)
                MessageBox.Show("You have already selected protein IDs", "Protein IDs selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (mlstBoxAllCols.SelectedItems.Count == 1)
            {
                mlstBoxIPI.Items.Add(mlstBoxAllCols.SelectedItem);
                mlstBoxAllCols.Items.Remove(mlstBoxAllCols.SelectedItem);
                mbtnIPIUnselect.Enabled = true;
            }
            else
                MessageBox.Show("Select one column for Protein IDs.", "Wrong selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mbtnIPIUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxIPI.SelectedItems.Count == 1)
            {
                mlstBoxAllCols.Items.Add(mlstBoxIPI.SelectedItem);
                mlstBoxIPI.Items.Remove(mlstBoxIPI.SelectedItem);
            }
            else
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            if (mlstBoxIPI.Items.Count == 0)
                mbtnIPIUnselect.Enabled = false;
        }

        #region Properties

        public ArrayList PopulateListBox
        {
            set
            {
                marrColumns = value;
                int mintMaxColumns = value.Count;
                object[] lstBoxEntries = new object[mintMaxColumns];
                value.CopyTo(lstBoxEntries);
                ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxAllCols, lstBoxEntries);
                mlstBoxAllCols.Items.AddRange(lboxObjColData);
                //for (int i = 0; i < mlstBoxAllCols.Items.Count; i++)
                //    mlstBoxAllCols.SetSelected(i, true);
            }
        }

        public string[] Columns2Remove
        {
            get
            {
                string[] strarr2Rem = new string[mlstBoxAllCols.Items.Count];
                for (int i = 0; i < mlstBoxAllCols.Items.Count; i++)
                {
                    strarr2Rem[i] = mlstBoxAllCols.Items[i].ToString();
                }
                return strarr2Rem;
            }
        }

        public string RowIDColumn
        {
            get
            {
                return mlstBoxMT.Items[0].ToString();
            }
        }

        public string ProteinIDColumn
        {
            get
            {
                return mlstBoxIPI.Items[0].ToString();
            }
        }
        #endregion
    }
}