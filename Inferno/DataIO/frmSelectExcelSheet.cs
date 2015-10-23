using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DAnTE.Tools
{
    public partial class frmSelectExcelSheet : Form
    {
        public frmSelectExcelSheet()
        {
            InitializeComponent();
        }

        private void mBtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<string> PopulateListBox
        {
            set
            {
                mlstBoxSheets.DataSource = value;                
            }
        }

        public int SelectedSheet
        {
            get
            {
                return mlstBoxSheets.SelectedIndex;
            }
        }
    }
}