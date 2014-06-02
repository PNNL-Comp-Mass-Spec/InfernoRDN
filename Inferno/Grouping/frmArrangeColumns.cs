using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmArrangeColumns : Form
    {
        private ArrayList marrDatasetInfo = new ArrayList();

        public frmArrangeColumns()
        {
            InitializeComponent();
        }

        private void FillListView()
        {
            mlistViewDatasets.Clear();
            clsDatasetInfo currDataset = (clsDatasetInfo)marrDatasetInfo[0];

            ColumnHeader col = new ColumnHeader();
            col.Text = "Dataset Name";
            col.Width = 400;
            mlistViewDatasets.Columns.Add(col);

            // Fill only the dataset names
            for (int row = 0; row < marrDatasetInfo.Count; row++)
            {
                currDataset = (clsDatasetInfo)marrDatasetInfo[row];
                ListViewItem dataItem = new ListViewItem(currDataset.mstrDataSetName);
                dataItem.Tag = row + 1;
                mlistViewDatasets.Items.Add(dataItem);
            }

        }

        private void frmArrangeColumns_Load(object sender, EventArgs e)
        {
            FillListView();
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void mbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void mbtnUp_Click(object sender, EventArgs e)
        {
            MoveListViewItem(true);
        }

        private void mbtnDown_Click(object sender, EventArgs e)
        {
            MoveListViewItem(false);
        }

        private void mbtnDel_Click(object sender, EventArgs e)
        {
            if (mlistViewDatasets.SelectedIndices.Count != 0)
            {
                mlistViewDatasets.Items.RemoveAt(mlistViewDatasets.SelectedIndices[0]);
            }
        }

        private void MoveListViewItem(bool mblMoveUp)
        {
            string mstrSelected;
            int selectedTag, index, newIndex = -1;

            if (mlistViewDatasets.SelectedIndices.Count != 0)
            {
                index = mlistViewDatasets.SelectedIndices[0];
                if (mblMoveUp)
                {
                    if (index != 0)
                        newIndex = index - 1;
                }
                else
                {
                    if (index != mlistViewDatasets.Items.Count - 1)
                        newIndex = index + 1;
                }
                if (newIndex != -1)
                {
                    mstrSelected = mlistViewDatasets.Items[index].Text;
                    selectedTag = (int)mlistViewDatasets.Items[index].Tag;
                    mlistViewDatasets.Items[index].Text = mlistViewDatasets.Items[newIndex].Text;
                    mlistViewDatasets.Items[index].Tag = mlistViewDatasets.Items[newIndex].Tag;
                    mlistViewDatasets.Items[newIndex].Text = mstrSelected;
                    mlistViewDatasets.Items[newIndex].Tag = selectedTag;
                    mlistViewDatasets.Items[newIndex].Selected = true;
                    mlistViewDatasets.Refresh();
                }
            }
        }

        #region Properties

        public ArrayList DatasetInfo
        {
            set
            {
                marrDatasetInfo = value;
            }
            get
            {
                return marrDatasetInfo;
            }
        }

        public ArrayList NewDatasetOrder
        {
            get
            {
                ArrayList newOrd = new ArrayList();
                foreach (ListViewItem it in mlistViewDatasets.Items)
                    newOrd.Add(it.Tag);
                return newOrd;
            }
        }

        #endregion

        

        
    }
}