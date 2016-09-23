using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmMergeColsPar : Form
    {
        public frmMergeColsPar()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }


        public List<string> PopulateFactorComboBox
        {
            set { mcmbBoxFactors.DataSource = value; }
        }

        public string SelectedFactor
        {
            get
            {
                var idx = mcmbBoxFactors.SelectedIndex + 1;
                return "factors[" + idx.ToString() + ",]";
            }
        }

        public string DataSetName
        {
            set { mlblDataName.Text = value; }
        }

        public string pMode
        {
            get
            {
                if (mrBtnMean.Checked)
                    return "pmode='mean'";
                else if (mrBtnMedian.Checked)
                    return "pmode='median'";
                else
                    return "pmode='sum'";
            }
        }
    }
}