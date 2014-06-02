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
    public partial class frmFilterAnova : Form
    {
        Double cutoff = 0.05;
        private ArrayList marrColumns = new ArrayList();

        public frmFilterAnova()
        {
            InitializeComponent();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            bool success = true;
            if (mlstBoxpvals.SelectedIndex < 0)
            {
                MessageBox.Show("Select an ANOVA Column", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                success = false;
            }
            try
            {
                cutoff = Convert.ToDouble(mtxtBoxCutoff.Text);
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

        public ArrayList PopulateListBox
        {
            set
            {
                marrColumns = value;
                int mintMaxColumns = value.Count;
                object[] lstBoxEntries = new object[mintMaxColumns];
                value.CopyTo(lstBoxEntries);
                ListBox.ObjectCollection lboxObjColData = new ListBox.ObjectCollection(mlstBoxpvals, lstBoxEntries);
                mlstBoxpvals.Items.AddRange(lboxObjColData);
                //for (int i = 0; i < mlstBoxAllCols.Items.Count; i++)
                //    mlstBoxAllCols.SetSelected(i, true);
            }
        }

        public int SelectedColumn
        {
            get
            {
                return mlstBoxpvals.SelectedIndex;
            }
        }

        public Double Thres
        {
            get
            {
                return cutoff;
            }
        }

        public bool LessThan
        {
            get
            {
                return mrBtnLT.Checked;
            }
        }

        #endregion

        
    }
}