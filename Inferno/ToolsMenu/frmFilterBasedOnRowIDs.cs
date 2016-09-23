using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmFilterBasedOnRowIDs : Form
    {

        public frmFilterBasedOnRowIDs()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #region Properties

        public List<string> PopulateDataComboBox
        {
            set { mcmbBoxData.DataSource = value; }
        }

        public string Dataset
        {
            get { return mcmbBoxData.SelectedItem.ToString(); }
        }

        #endregion
    }
}