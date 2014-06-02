using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DAnTE.Tools
{
    public partial class frmSelectExcelSheet : Form
    {
        private ArrayList marrColumns = new ArrayList();

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
        
        public ArrayList PopulateListBox
        {
            set
            {
                marrColumns = value;
                int mintMaxColumns = value.Count;
                object[] lstBoxEntries = new object[mintMaxColumns];
                value.CopyTo(lstBoxEntries);
                ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxSheets, lstBoxEntries);
                mlstBoxSheets.Items.AddRange(lboxObjColData);
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