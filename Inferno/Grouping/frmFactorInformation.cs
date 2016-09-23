using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmFactorInformation : Form
    {
        private const int NUM_COLUMNS = 1;
        private List<clsDatasetInfo> marrDatasetInfo = new List<clsDatasetInfo>();
        private List<clsFactorInfo> marrFactorInfo = new List<clsFactorInfo>();
        private bool mblfactorsLoaded;
        private bool mblOrderChanged;
        private bool mblOrderChangeOnly;

        public frmFactorInformation()
        {
            InitializeComponent();
        }

        private List<clsFactorInfo> MakeDeepCopy(IEnumerable<clsFactorInfo> sourceList)
        {
            var newList = new List<clsFactorInfo>();

            foreach (var item in sourceList)
            {
                newList.Add((clsFactorInfo)(item.Clone()));
            }

            return newList;
           
        }

        private List<clsDatasetInfo> MakeDeepCopy(IEnumerable<clsDatasetInfo> sourceList)
        {
            var newList = new List<clsDatasetInfo>();

            foreach (var item in sourceList)
            {
                newList.Add((clsDatasetInfo)(item.Clone()));
            }

            return newList;
           
        }
        
        private void frmFactorInformation_Load(object sender, EventArgs e)
        {
            FillListView();
            mbtnDefFac.Visible = !mblOrderChangeOnly;
        }

        private void mbtnDefFac_Click(object sender, EventArgs e)
        {
            var tmpFactors = MakeDeepCopy(marrFactorInfo); // keep copies

            frmDefFactors mfrmDefFactors;

            if (mblfactorsLoaded)
                mfrmDefFactors = new frmDefFactors(marrFactorInfo);
            else
                mfrmDefFactors = new frmDefFactors();

            if (mfrmDefFactors.ShowDialog() == DialogResult.OK)
            {
                marrFactorInfo = mfrmDefFactors.FactorInfoArray;
                mblfactorsLoaded = true;

                SetFactorAssignments();
                FillListView();
            }
            else //User cancels the factor changes, so revert to previous.
            {
                marrFactorInfo = tmpFactors;
            }
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

        private void SetFactorAssignments()
        {
            var found = false;

            var currDataset = marrDatasetInfo[0];
            var toRemove = 0;
            var NumFactors = currDataset.marrFactorAssnmnts.Count;
            while (NumFactors > 0 && toRemove < NumFactors)
            {
                foreach (var currFactor in marrFactorInfo)
                {
                    if (currDataset.marrFactorAssnmnts[toRemove].Name.Equals(
                        currFactor.mstrFactor))
                    {
                        found = true;
                        toRemove++;
                        break;
                    }

                    found = false;
                }

                if (found)
                {
                    continue;
                }

                // Not found; delete the factor
                foreach (var dataset in marrDatasetInfo)
                {
                    dataset.marrFactorAssnmnts.RemoveAt(toRemove);
                }

                NumFactors--;
            }
            
            currDataset = marrDatasetInfo[0];
            foreach (var currFactor in marrFactorInfo)
            {
                foreach (var factor in currDataset.marrFactorAssnmnts)
                {
                    if (factor.Name.Equals(
                        currFactor.mstrFactor))
                    {
                        found = true;
                        break;
                    }

                    found = false;
                }

                if (found)
                {
                    continue;
                }

                // New factor, so add the first value to all the datasets
                foreach (var dataset in marrDatasetInfo)
                {
                    var tmpFactor = new Factor(currFactor.mstrFactor, currFactor.marrValues[0]);
                    dataset.marrFactorAssnmnts.Add(tmpFactor);
                }
            }
        }

        private void FillListView()
        {
            mlstViewDataSets.Clear();
            var currDataset = marrDatasetInfo[0];

            var col = new ColumnHeader
            {
                Text = "Dataset Name",
                Width = 150
            };
            mlstViewDataSets.Columns.Add(col);

            if (mblfactorsLoaded)
            {
                for (var numCol = 0; numCol < currDataset.marrFactorAssnmnts.Count; numCol++)
                {
                    var k = 0;
                    for (var i = 0; i < marrFactorInfo.Count; i++)
                    {
                        if (marrFactorInfo[numCol].mstrFactor.Equals(currDataset.marrFactorAssnmnts[i].Name))
                            break;
                        else
                            k++;
                    }

                    //Add columns for each factor
                    col = new ColumnHeader
                    {
                        Text = currDataset.marrFactorAssnmnts[k].Name,
                        Width = 100
                    };
                    mlstViewDataSets.Columns.Add(col);
                }
                for (var row = 0; row < marrDatasetInfo.Count; row++)
                {   
                    // Start filling the rows
                    currDataset = marrDatasetInfo[row];
                    var dataItem = new ListViewItem(currDataset.mstrDataSetName);
                    for (var ncol = 0; ncol < currDataset.marrFactorAssnmnts.Count; ncol++)
                    {
                        var k = 0;
                        for (var i = 0; i < marrFactorInfo.Count; i++)
                        {
                            if (marrFactorInfo[ncol].mstrFactor.Equals(currDataset.marrFactorAssnmnts[i].Name))
                                break;
                            else
                                k++;
                        }

                        //Fill factor columns with values
                        dataItem.SubItems.Add(currDataset.marrFactorAssnmnts[k].Value);
                    }
                    
                    // Store the row number (integer) as the tag
                    dataItem.Tag = row;
                    mlstViewDataSets.Items.Add(dataItem);
                }
            }
            else
            {   // Fill only the dataset names
                for (var row = 0; row < marrDatasetInfo.Count; row++)
                {
                    currDataset = marrDatasetInfo[row];
                    var dataItem = new ListViewItem(currDataset.mstrDataSetName)
                    {
                        Tag = row
                    };

                    // Store the row number (integer) as the tag
                    mlstViewDataSets.Items.Add(dataItem);
                }
            }
        }

        private void mbtnUp_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.SelectedIndices.Count != 0)
            {
                MoveListViewItem(true);
                mblOrderChanged = true;
            }
        }

        private void mbtnDown_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.SelectedIndices.Count != 0)
            {
                MoveListViewItem(false);
                mblOrderChanged = true;
            }
        }

        private void mbtnDel_Click(object sender, EventArgs e)
        {
            if (mlstViewDataSets.SelectedIndices.Count != 0)
            {
                mlstViewDataSets.Items.RemoveAt(mlstViewDataSets.SelectedIndices[0]);
                mblOrderChanged = true;
            }
        }

        private void ArrangeDatasets()
        {
            var currentIndexOrder = this.NewDatasetOrder;
            var orderedDatasets = new List<clsDatasetInfo>();

            var tmpDatasets = MakeDeepCopy(marrDatasetInfo);

            foreach (var itemIndex in currentIndexOrder)
            {
                orderedDatasets.Add(tmpDatasets[itemIndex]);
            }

            marrDatasetInfo = orderedDatasets;
        }

        private void MoveListViewItem(bool mblMoveUp)
        {
            var newIndex = -1;

            if (mlstViewDataSets.SelectedIndices.Count != 0)
            {
                var index = mlstViewDataSets.SelectedIndices[0];
                if (mblMoveUp)
                {
                    if (index != 0)
                        newIndex = index - 1;
                }
                else
                {
                    if (index != mlstViewDataSets.Items.Count - 1)
                        newIndex = index + 1;
                }
                if (newIndex != -1)
                {
                    var nextItem = (ListViewItem)mlstViewDataSets.Items[newIndex].Clone();
                    mlstViewDataSets.Items.RemoveAt(newIndex);
                    mlstViewDataSets.Items.Insert(index, nextItem);
                    mlstViewDataSets.Items[newIndex].Selected = true;
                    mlstViewDataSets.Refresh();
                }
            }
        }


        #region editing the listview ---------------------

        private void updateDatasetFactorInfo()
        {
            var numEntries = mlstViewDataSets.Items.Count;

            for (var row = 0; row < numEntries; row++)
            {
                if (marrFactorInfo.Count <= 0)
                {
                    continue;
                }

                // update factors
                for (var i = 0; i < marrFactorInfo.Count; i++)
                {
                    marrDatasetInfo[row].marrFactorAssnmnts[i].Value =
                        mlstViewDataSets.Items[row].SubItems[i + NUM_COLUMNS].Text; // Factors start at column 7th
                }
            }
        }


        private void cmbBoxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                cmbBox.Hide();
            }
        }

        private void cmbBoxSelectedIndexChanged(object sender, System.EventArgs e)
        {
            var sel = cmbBox.SelectedIndex;
            if (sel < 0)
            {
                return;
            }

            var itemSel = cmbBox.Items[sel].ToString();
            li.SubItems[subItemSelected].Text = itemSel;
            updateDatasetFactorInfo();
        }

        private void doubleClick_event(object sender, System.EventArgs e)
        {
            // Check the subitem clicked .
            var nStart = X;
            var spos = 0;
            subItemSelected = 0;

            var sposX = mlstViewDataSets.Location.X;
            var sposY = mlstViewDataSets.Location.Y;
            var epos = mlstViewDataSets.Columns[0].Width;
            for (var i = 0; i < mlstViewDataSets.Columns.Count; i++)
            {
                if (nStart > spos && nStart < epos)
                {
                    subItemSelected = i;
                    break;
                }

                spos = epos;
                if (i + 1 < mlstViewDataSets.Columns.Count)
                    epos += mlstViewDataSets.Columns[i + 1].Width;
            }

            //Console.WriteLine("SUB ITEM SELECTED = " + li.SubItems[subItemSelected].Text);
            subItemText = li.SubItems[subItemSelected].Text;

            var colName = mlstViewDataSets.Columns[subItemSelected].Text;
            var factorIdx = 0;

            if (subItemSelected <= 0)
            {
                return;
            }
            
            // Factor columns
            cmbBox.Items.Clear();
            var colFactor = mlstViewDataSets.Columns[subItemSelected].Text;// factor name
            for (var i = 0; i < marrFactorInfo.Count; i++)
            {
                if (marrFactorInfo[i].mstrFactor.Equals(colFactor))
                {
                    factorIdx = i;
                    break;
                }
            }

            // Note: cannot use .DataSource = Value because we .Clear items from the listbox
            foreach (var item in marrFactorInfo[factorIdx].marrValues)
            {
                cmbBox.Items.Add(item);
            }                

            cmbBox.Size = new System.Drawing.Size(epos - spos, li.Bounds.Bottom - li.Bounds.Top);
            cmbBox.Location = new System.Drawing.Point(sposX + spos + 2, sposY + li.Bounds.Y);
            cmbBox.Show();
            cmbBox.Text = subItemText;
            cmbBox.SelectAll();
            cmbBox.Focus();
        }

        private void mouseDown_event(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            li = mlstViewDataSets.GetItemAt(e.X, e.Y);
            X = e.X;
            Y = e.Y;
            if (li == null)
            {
                return;
            }

            // Check the subitem clicked .
            var nStart = X;
            var spos = 0;
            subItemSelected = 0;

            var sposX = mlstViewDataSets.Location.X;
            var sposY = mlstViewDataSets.Location.Y;
            var epos = mlstViewDataSets.Columns[0].Width;
            for (var i = 0; i < mlstViewDataSets.Columns.Count; i++)
            {
                if (nStart > spos && nStart < epos)
                {
                    subItemSelected = i;
                    break;
                }

                spos = epos;
                if (i + 1 < mlstViewDataSets.Columns.Count)
                    epos += mlstViewDataSets.Columns[i + 1].Width;
            }

            var p = new Point(e.X, e.Y);
            if (((e.Button & MouseButtons.Right) == MouseButtons.Right) && (subItemSelected > (NUM_COLUMNS - 1)))
            {
                cntxtMnuFactors.Show(mlstViewDataSets, p);
            }
        }

        private void cmbBoxFocusOver(object sender, System.EventArgs e)
        {
            cmbBox.Hide();
        }

        #endregion

        #region Context menu items
        private void menuItemFillBelow_Click(object sender, System.EventArgs e)
        {
            if (subItemSelected < NUM_COLUMNS || li == null)
            {
                MessageBox.Show("Add one or more factors, then select a cell in a column with factors", "Invalid selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            var idx = 0;
            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag)
                    break;
                else
                    idx++;
            }

            var factorVal = li.SubItems[subItemSelected].Text;
            for (var num = idx; num < mlstViewDataSets.Items.Count; num++)
            {
                mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal;
                marrDatasetInfo[num].marrFactorAssnmnts[subItemSelected - NUM_COLUMNS].Value = factorVal;
            }

        }

        private void menuItemFillNBelow_Click(object sender, System.EventArgs e)
        {
            if (subItemSelected < NUM_COLUMNS || li == null)
            {
                MessageBox.Show("Add one or more factors, then select a cell in a column with factors", "Invalid selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }          

            var idx = 0;
            var nBlock = 1;
            var inputBlockSizeParams = new frmInputBlockSize();

            var res = inputBlockSizeParams.ShowDialog();
            if (res == DialogResult.Cancel)
                return;
            if (res == DialogResult.OK)
            {
                nBlock = inputBlockSizeParams.blockSize;
            }

            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag)
                    break;
                else
                    idx++;
            }
            var factorVal = li.SubItems[subItemSelected].Text;
            for (var num = idx; num < idx + nBlock; num++)
            {
                if (num < mlstViewDataSets.Items.Count)
                {
                    mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal;
                    marrDatasetInfo[num].marrFactorAssnmnts[subItemSelected - NUM_COLUMNS].Value = factorVal;
                }

            }
        }

        private void menuItemFillRand_Click(object sender, System.EventArgs e)
        {
            if (subItemSelected < NUM_COLUMNS || li == null)
            {
                MessageBox.Show("Add one or more factors, then select a cell in a column with factors", "Invalid selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            var rnum = new Random();
            var idxFactor = subItemSelected - NUM_COLUMNS;

            var currentFactor = marrFactorInfo[idxFactor];
            for (var num = 0; num < mlstViewDataSets.Items.Count; num++)
            {
                var currFactorVal = currentFactor.marrValues[rnum.Next(currentFactor.marrValues.Count)];
                mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = currFactorVal;
                marrDatasetInfo[num].marrFactorAssnmnts[subItemSelected - NUM_COLUMNS].Value = currFactorVal;
            }
        }

        private void menuItemFillNCycl_Click(object sender, System.EventArgs e)
        {
            if (subItemSelected < NUM_COLUMNS || li == null)
            {
                MessageBox.Show("Add one or more factors, then select a cell in a column with factors", "Invalid selection", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            var idx = 0;
            var nBlock = 1;
            var inputBlockSizeParams = new frmInputBlockSize();

            var res = inputBlockSizeParams.ShowDialog();
            if (res == DialogResult.Cancel)
                return;
            if (res == DialogResult.OK)
            {
                nBlock = inputBlockSizeParams.blockSize;
            }

            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag)
                    break;
                else
                    idx++; // which item is clicked ?
            }
            var factorVal = li.SubItems[subItemSelected].Text; // get it's factor value

            var numVals = marrFactorInfo[subItemSelected - NUM_COLUMNS].vCount; //number of values in this factor

            var fvals = marrFactorInfo[subItemSelected - NUM_COLUMNS].FactorValues;
            
            int startIdxVal; //start with this value and its index is...
            for (startIdxVal = 0; startIdxVal < numVals; startIdxVal++)
                if (fvals[startIdxVal].Equals(factorVal))
                    break;

            var start = idx;
            var cycle = 0;
            while (start < mlstViewDataSets.Items.Count)//all cycles
            {
                for (var num = start; num < (start + nBlock); num++)//set values in one block
                {
                    if (num < mlstViewDataSets.Items.Count)
                    {
                        factorVal = fvals[(startIdxVal + cycle) % numVals]; // index should go in a cycle, hence the mod operator.
                        mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal; // update in the view
                        // update in the datasaet info
                        marrDatasetInfo[num].marrFactorAssnmnts[subItemSelected - NUM_COLUMNS].Value = factorVal;
                    }
                    else
                        break;
                }
                start = start + nBlock; //jump to the next block,
                cycle++; //i.e. the next cycle
            }
        }
        #endregion

        #region Properties

        public List<clsDatasetInfo> DatasetInfo
        {
            set
            {
                marrDatasetInfo = value;
            }
            get
            {
                ArrangeDatasets();
                return marrDatasetInfo;
            }
        }

        public List<clsFactorInfo> FactorInfo
        {
            set
            {
                marrFactorInfo = value;
            }
            get
            {
                return marrFactorInfo;
            }
        }

        public bool FactorsLoaded
        {
            set
            {
                mblfactorsLoaded = value;
            }
        }

        public List<int> NewDatasetOrder
        {
            get
            {
                var newOrd = new List<int>();
                foreach (ListViewItem it in mlstViewDataSets.Items)
                    newOrd.Add((int)it.Tag);

                return newOrd;
            }
        }

        public List<string> NewDatasetNameOrder
        {
            get
            {
                var newOrd = new List<string>();
                foreach (ListViewItem it in mlstViewDataSets.Items)
                    newOrd.Add(it.Text);
                return newOrd;
            }
        }

        public bool OrderChanged
        {
            get
            {
                return mblOrderChanged;
            }
        }

        public string Title
        {
            set
            {
                mlblTitle.Text = value;
            }
        }

        public string SubTitle
        {
            set
            {
                txtDirections.Text = value;
            }
        }

        public string WinTitle
        {
            set
            {
                this.Text = value;
            }
        }

        public bool OrderChangeOnly
        {
            set
            {
                mblOrderChangeOnly = value;
            }
        }

        #endregion
        
    }
}