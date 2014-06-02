using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmFilterBasedOnRowIDs : Form
    {
        //Double cutoff = 0.05;
        private ArrayList marrColumns = new ArrayList();

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

        public ArrayList PopulateDataComboBox
        {
            set
            {
                mcmbBoxData.DataSource = value;
            }
        }

        public string Dataset
        {
            get
            {
                return mcmbBoxData.SelectedItem.ToString();
            }
        }
        
        #endregion

        
    }
}