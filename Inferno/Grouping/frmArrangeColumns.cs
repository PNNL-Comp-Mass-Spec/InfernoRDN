using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmArrangeColumns : Form
    {
        private List<clsDatasetInfo> marrDatasetInfo = new List<clsDatasetInfo>();

        public frmArrangeColumns()
        {
            InitializeComponent();
        }

        private void FillListView()
        {
            mlistViewDatasets.Clear();

            var col = new ColumnHeader
            {
                Text = "Dataset Name",
                Width = 400
            };
            mlistViewDatasets.Columns.Add(col);

            // Fill only the dataset names        
            for (var row = 0; row < marrDatasetInfo.Count; row++)
            {
                var currDataset = marrDatasetInfo[row];
                var dataItem = new ListViewItem(currDataset.mstrDataSetName)
                {
                    Tag = row + 1
                };
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
            var newIndex = -1;

            if (mlistViewDatasets.SelectedIndices.Count != 0)
            {
                var index = mlistViewDatasets.SelectedIndices[0];
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
                    var selectedListView = mlistViewDatasets.Items[index].Text;
                    var selectedTag = (int)mlistViewDatasets.Items[index].Tag;
                    mlistViewDatasets.Items[index].Text = mlistViewDatasets.Items[newIndex].Text;
                    mlistViewDatasets.Items[index].Tag = mlistViewDatasets.Items[newIndex].Tag;
                    mlistViewDatasets.Items[newIndex].Text = selectedListView;
                    mlistViewDatasets.Items[newIndex].Tag = selectedTag;
                    mlistViewDatasets.Items[newIndex].Selected = true;
                    mlistViewDatasets.Refresh();
                }
            }
        }

        #region Properties

        public List<clsDatasetInfo> DatasetInfo
        {
            set => marrDatasetInfo = value;
            get => marrDatasetInfo;
        }

        public List<int> NewDatasetOrder
        {
            get
            {
                var newOrd = new List<int>();
                foreach (ListViewItem it in mlistViewDatasets.Items)
                {
                    newOrd.Add((int)it.Tag);
                }

                return newOrd;
            }
        }

        #endregion
    }
}