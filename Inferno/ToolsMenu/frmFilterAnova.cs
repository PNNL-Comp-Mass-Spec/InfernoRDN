using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmFilterAnova : Form
    {
        double cutoff = 0.05;

        public frmFilterAnova()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            var success = true;
            if (mlstBoxpvals.SelectedIndex < 0)
            {
                MessageBox.Show("Select an ANOVA Column", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                success = false;
            }
            try
            {
                cutoff = Convert.ToDouble(mtxtBoxCutoff.Text, NumberFormatInfo.InvariantInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Cutoff" + Environment.NewLine + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }
            if (success)
                DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #region Properties

        public List<string> PopulateDataComboBox
        {
            set => mcmbBoxData.DataSource = value;
        }

        public string Dataset => mcmbBoxData.SelectedItem.ToString();

        public List<string> PopulateListBox
        {
            set => mlstBoxpvals.DataSource = value;
        }

        public int SelectedColumn => mlstBoxpvals.SelectedIndex;

        public Double Thres => cutoff;

        public bool LessThan => mrBtnLT.Checked;

        #endregion
    }
}