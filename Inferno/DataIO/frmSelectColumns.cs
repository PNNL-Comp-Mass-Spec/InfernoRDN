using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DAnTE.Inferno
{
    public partial class frmSelectColumns : Form
    {
        public frmSelectColumns()
        {
            InitializeComponent();
            if (!mchkBoxProtein.Checked)
            {
                mlstBoxProteinInfo.Enabled = false;
                mbtnProteinSelect.Enabled = false;
                mbtnProteinUnselect.Enabled = false;
            }
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.Items.Count != 1)
                MessageBox.Show("Select unique row identifier (e.g. Mass Tags)", "Incomplete Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mlstBoxData.Items.Count < 2)
                MessageBox.Show("Not enough datasets", "Incomplete Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mchkBoxProtein.Checked && mlstBoxProteinInfo.Items.Count < 1)
                MessageBox.Show("Select protein identifiers", "Incomplete Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                this.DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SelectIPI_event(object sender, EventArgs e)
        {
            if (mchkBoxProtein.Checked)
            {
                mlstBoxProteinInfo.Enabled = true;
                mbtnProteinSelect.Enabled = true;
                if (mlstBoxProteinInfo.Items.Count > 0)
                    mbtnProteinUnselect.Enabled = true;
            }
            else
            {
                mlstBoxProteinInfo.Enabled = false;
                mbtnProteinSelect.Enabled = false;
                mbtnProteinUnselect.Enabled = false;
            }
        }

        private void mbtnDataSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxAllCols.SelectedItems.Count > 0)
            {
                foreach (var r in mlstBoxAllCols.SelectedItems)
                {
                    mlstBoxData.Items.Add(r);
                }
                while (mlstBoxAllCols.SelectedItems.Count > 0)
                    mlstBoxAllCols.Items.Remove(mlstBoxAllCols.SelectedItem);
                mbtnDataUnselect.Enabled = true;
            }
            else
            {
                MessageBox.Show("Select Multiple Datasets", "Empty selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        private void mbtnDataUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxData.SelectedItems.Count > 0)
            {
                foreach (var r in mlstBoxData.SelectedItems)
                {
                    mlstBoxAllCols.Items.Add(r);
                }
                while (mlstBoxData.SelectedItems.Count > 0)
                    mlstBoxData.Items.Remove(mlstBoxData.SelectedItem);
            }
            else
            {
                MessageBox.Show("Select Datasets", "Empty selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            if (mlstBoxData.Items.Count == 0)
                mbtnDataUnselect.Enabled = false;
        }

        private void mbtnMTSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.Items.Count == 1)
            {
                MessageBox.Show("You have already selected the Row IDs", "Row IDs selected",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (mlstBoxAllCols.SelectedItems.Count == 1)
            {
                mlstBoxMT.Items.Add(mlstBoxAllCols.SelectedItem);
                mlstBoxAllCols.Items.Remove(mlstBoxAllCols.SelectedItem);
                mbtnMTUnselect.Enabled = true;
            }
            else
            {
                MessageBox.Show("Select One Column as Unique RowID (Mass Tags).", "Empty selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mbtnMTUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxMT.SelectedItems.Count == 1)
            {
                mlstBoxAllCols.Items.Add(mlstBoxMT.SelectedItem);
                mlstBoxMT.Items.Remove(mlstBoxMT.SelectedItem);
            }
            else
            {
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            if (mlstBoxMT.Items.Count == 0)
                mbtnMTUnselect.Enabled = false;
        }

        private void mbtnProteinSelect_Click(object sender, EventArgs e)
        {
            if (mlstBoxAllCols.SelectedItems.Count > 0)
            {
                mlstBoxProteinInfo.Items.Add(mlstBoxAllCols.SelectedItem);
                mlstBoxAllCols.Items.Remove(mlstBoxAllCols.SelectedItem);
                mbtnProteinUnselect.Enabled = true;
            }
            else
            {
                MessageBox.Show("Select one column for Protein IDs.", "Wrong selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mbtnProteinUnselect_Click(object sender, EventArgs e)
        {
            if (mlstBoxProteinInfo.SelectedItems.Count == 1)
            {
                mlstBoxAllCols.Items.Add(mlstBoxProteinInfo.SelectedItem);
                mlstBoxProteinInfo.Items.Remove(mlstBoxProteinInfo.SelectedItem);
            }
            else
            {
                MessageBox.Show("Make the selection first.", "Empty selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }

            if (mlstBoxProteinInfo.Items.Count == 0)
                mbtnProteinUnselect.Enabled = false;
        }

        #region Properties

        public List<string> PopulateListBox
        {
            set
            {
                // Note: cannot use .DataSource = Value because we .Remove items from the listbox

                mlstBoxAllCols.Items.Clear();
                foreach (var item in value)
                {
                    mlstBoxAllCols.Items.Add(item);
                }
            }
        }

        public string[] Columns2Remove
        {
            get
            {
                var strarr2Rem = new string[mlstBoxAllCols.Items.Count];
                for (var i = 0; i < mlstBoxAllCols.Items.Count; i++)
                {
                    strarr2Rem[i] = mlstBoxAllCols.Items[i].ToString();
                }
                return strarr2Rem;
            }
        }

        public string[] DataColumns
        {
            get
            {
                var strarrDataCols = new string[mlstBoxData.Items.Count];
                for (var i = 0; i < mlstBoxData.Items.Count; i++)
                {
                    strarrDataCols[i] = mlstBoxData.Items[i].ToString();
                }
                return strarrDataCols;
            }
        }

        public string RowIDColumn => mlstBoxMT.Items[0].ToString();

        /// <summary>
        /// The Protein ID column, which is the first item in the Protein Name list box
        /// </summary>
        public string ProteinIDColumn
        {
            get
            {
                if (mlstBoxProteinInfo.Items.Count == 0)
                    return string.Empty;

                return mlstBoxProteinInfo.Items[0].ToString();
            }
        }

        public List<string> ProteinMetadataColumns
        {
            get
            {
                if (mlstBoxProteinInfo.Items.Count < 1)
                    return new List<string>();

                var metadataColumns = new List<string>();

                for (var i = 1; i < mlstBoxProteinInfo.Items.Count; i++)
                {
                    metadataColumns.Add(mlstBoxProteinInfo.Items[i].ToString());
                }

                return metadataColumns;
            }
        }

        public bool Proteins
        {
            get { return mchkBoxProtein.Checked; }
            set
            {
                if (value)
                {
                    mchkBoxProtein.Checked = false;
                    mlstBoxProteinInfo.Enabled = false;
                }
            }
        }

        #endregion
    }
}