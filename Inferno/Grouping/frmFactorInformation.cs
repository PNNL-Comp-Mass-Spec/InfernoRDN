using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmFactorInformation : Form
    {
        private const int NUM_COLUMNS = 1;
        private ArrayList marrDatasetInfo = new ArrayList();
        private ArrayList marrFactorInfo = new ArrayList();
        private bool mblfactorsLoaded = false;
        private bool mblOrderChanged = false;
        private bool mblOrderChangeOnly = false;

        public frmFactorInformation()
        {
            InitializeComponent();
        }

        private ArrayList MakeDeepCopy(ArrayList marrIN) //Deep copy arraylists of classes
        {
            ArrayList copyTo = new ArrayList();
            foreach (object obj in marrIN)
            {
                copyTo.Add(((ICloneable)obj).Clone());
            }
            return copyTo;
        }

        private void frmFactorInformation_Load(object sender, EventArgs e)
        {
            FillListView();
            mbtnDefFac.Visible = !mblOrderChangeOnly;
        }

        private void mbtnDefFac_Click(object sender, EventArgs e)
        {
            ArrayList tmpFactors = MakeDeepCopy(marrFactorInfo); // keep copies

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
            bool found = false;
            clsDatasetInfo currDataset;
            clsFactorInfo currFactor;
            Factor tmpFactor;


            currDataset = (clsDatasetInfo)marrDatasetInfo[0];
            int toRemove = 0;
            int NumFactors = currDataset.marrFactorAssnmnts.Count;
            while (NumFactors > 0 && toRemove < NumFactors)
            {
                for (int numF = 0; numF < marrFactorInfo.Count; numF++)
                {
                    currFactor = (clsFactorInfo)marrFactorInfo[numF];
                    if (((Factor)currDataset.marrFactorAssnmnts[toRemove]).Name.Equals(
                        currFactor.mstrFactor))
                    {
                        found = true;
                        toRemove++;
                        break;
                    }
                    else
                        found = false;
                }//innermost for
                if (!found) // delete factor
                {
                    for (int nDataset = 0; nDataset < marrDatasetInfo.Count; nDataset++) //Delete?
                    {
                        ((clsDatasetInfo)marrDatasetInfo[nDataset]).marrFactorAssnmnts.RemoveAt(toRemove);
                    }
                    NumFactors--;
                }
            }//while (Dataset Factors)
            
            currDataset = (clsDatasetInfo)marrDatasetInfo[0];
            for (int numF = 0; numF < marrFactorInfo.Count; numF++)
            {
                currFactor = (clsFactorInfo)marrFactorInfo[numF];
                for (int numFinD = 0; numFinD < currDataset.marrFactorAssnmnts.Count; numFinD++)
                {
                    if (((Factor)currDataset.marrFactorAssnmnts[numFinD]).Name.Equals(
                        currFactor.mstrFactor))
                    {
                        found = true;
                        break;
                    }
                    else
                        found = false;
                }//innermost for
                if (!found) // new factor, so add the first value to all the datasets
                {
                    for (int nDataset = 0; nDataset < marrDatasetInfo.Count; nDataset++) //Add?
                    {
                        tmpFactor = new Factor(currFactor.mstrFactor, currFactor.marrValues[0].ToString());
                        ((clsDatasetInfo)marrDatasetInfo[nDataset]).marrFactorAssnmnts.Add(tmpFactor);
                    }
                }
            }//for (factor)

        }//method

        private void FillListView()
        {
            mlstViewDataSets.Clear();
            clsDatasetInfo currDataset = (clsDatasetInfo)marrDatasetInfo[0];

            ColumnHeader col = new ColumnHeader();
            col.Text = "Dataset Name";
            col.Width = 150;
            mlstViewDataSets.Columns.Add(col);

            if (mblfactorsLoaded)
            {
                for (int numCol = 0; numCol < currDataset.marrFactorAssnmnts.Count; numCol++)
                {
                    int k = 0;
                    for (int i = 0; i < marrFactorInfo.Count; i++)
                    {
                        if (((clsFactorInfo)marrFactorInfo[numCol]).mstrFactor.Equals(((Factor)currDataset.marrFactorAssnmnts[i]).Name))
                            break;
                        else
                            k++;
                    }
                    //Add columns for each factor
                    col = new ColumnHeader();
                    col.Text = ((Factor)currDataset.marrFactorAssnmnts[k]).Name;
                    col.Width = 100;
                    mlstViewDataSets.Columns.Add(col);
                }
                for (int row = 0; row < marrDatasetInfo.Count; row++)
                {   // Start filling the rows
                    currDataset = (clsDatasetInfo)marrDatasetInfo[row];
                    ListViewItem dataItem = new ListViewItem(currDataset.mstrDataSetName);
                    for (int ncol = 0; ncol < currDataset.marrFactorAssnmnts.Count; ncol++)
                    {
                        int k = 0;
                        for (int i = 0; i < marrFactorInfo.Count; i++)
                        {
                            if (((clsFactorInfo)marrFactorInfo[ncol]).mstrFactor.Equals(((Factor)currDataset.marrFactorAssnmnts[i]).Name))
                                break;
                            else
                                k++;
                        }
                        //Fill factor columns with values
                        dataItem.SubItems.Add(((Factor)currDataset.marrFactorAssnmnts[k]).Value);
                    }
                    //dataItem.Tag = currDataset.mstrDataSetName;
                    dataItem.Tag = row;
                    mlstViewDataSets.Items.Add(dataItem);
                }
            }
            else
            {   // Fill only the dataset names
                for (int row = 0; row < marrDatasetInfo.Count; row++)
                {
                    currDataset = (clsDatasetInfo)marrDatasetInfo[row];
                    ListViewItem dataItem = new ListViewItem(currDataset.mstrDataSetName);
                    //dataItem.Tag = currDataset.mstrDataSetName;
                    dataItem.Tag = row;
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
            ArrayList order = this.NewDatasetOrder;
            ArrayList orderedDatasets = new ArrayList();
            ArrayList tmpDatasets = MakeDeepCopy(marrDatasetInfo);

            for (int num = 0; num < order.Count; num++)
            {
                orderedDatasets.Add(tmpDatasets[(int)order[num]]);
            }
            marrDatasetInfo = orderedDatasets;
        }

        private void MoveListViewItem(bool mblMoveUp)
        {
            int newIndex = -1;

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
            int numEntries = mlstViewDataSets.Items.Count;
            clsFactorInfo currentFactor = new clsFactorInfo();

            for (int row = 0; row < numEntries; row++)
            {
                if (marrFactorInfo.Count > 0) // update factors
                {
                    for (int i = 0; i < marrFactorInfo.Count; i++)
                    {
                        ((Factor)((clsDatasetInfo)marrDatasetInfo[row]).marrFactorAssnmnts[i]).Value =
                            mlstViewDataSets.Items[row].SubItems[i + NUM_COLUMNS].Text; // Factors start at column 7th
                    }
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
            int sel = cmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = cmbBox.Items[sel].ToString();
                li.SubItems[subItemSelected].Text = itemSel;
                updateDatasetFactorInfo();
            }
        }

        private void doubleClick_event(object sender, System.EventArgs e)
        {
            // Check the subitem clicked .
            int nStart = X;
            int spos = 0;
            subItemSelected = 0;

            int sposX = mlstViewDataSets.Location.X;
            int sposY = mlstViewDataSets.Location.Y;
            int epos = mlstViewDataSets.Columns[0].Width;
            for (int i = 0; i < mlstViewDataSets.Columns.Count; i++)
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

            string colName = mlstViewDataSets.Columns[subItemSelected].Text;
            int factorIdx = 0;

            if (subItemSelected > 0)
            { // Factor columns
                cmbBox.Items.Clear();
                string colFactor = mlstViewDataSets.Columns[subItemSelected].Text;// factor name
                for (int i = 0; i < marrFactorInfo.Count; i++)
                {
                    if (((clsFactorInfo)marrFactorInfo[i]).mstrFactor.Equals(colFactor))
                    {
                        factorIdx = i;
                        break;
                    }
                }
                int numVals = ((clsFactorInfo)marrFactorInfo[factorIdx]).marrValues.Count; // how many values
                string[] s = new string[numVals]; // this will hold the combo box values
                for (int i = 0; i < numVals; i++)
                    s[i] = ((clsFactorInfo)marrFactorInfo[factorIdx]).marrValues[i].ToString();
                cmbBox.Items.AddRange(s);

                Rectangle r = new Rectangle(spos, li.Bounds.Y, epos, li.Bounds.Bottom);
                cmbBox.Size = new System.Drawing.Size(epos - spos, li.Bounds.Bottom - li.Bounds.Top);
                cmbBox.Location = new System.Drawing.Point(sposX + spos + 2, sposY + li.Bounds.Y);
                cmbBox.Show();
                cmbBox.Text = subItemText;
                cmbBox.SelectAll();
                cmbBox.Focus();
            }
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
            int nStart = X;
            int spos = 0;
            subItemSelected = 0;

            int sposX = mlstViewDataSets.Location.X;
            int sposY = mlstViewDataSets.Location.Y;
            int epos = mlstViewDataSets.Columns[0].Width;
            for (int i = 0; i < mlstViewDataSets.Columns.Count; i++)
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

            Point p = new Point(e.X, e.Y);
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

            int idx = 0;
            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag) //(item.Tag.Equals(li.Tag))
                    break;
                else
                    idx++;
            }
            string factorVal = li.SubItems[subItemSelected].Text;
            for (int num = idx; num < mlstViewDataSets.Items.Count; num++)
            {
                mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal;
                ((Factor)((clsDatasetInfo)marrDatasetInfo[num]).marrFactorAssnmnts[subItemSelected - NUM_COLUMNS]).Value = factorVal;
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

            int idx = 0;
            int nBlock = 1;
            frmInputBlockSize mfrmInputBlockSize = new frmInputBlockSize();

            DialogResult res = mfrmInputBlockSize.ShowDialog();
            if (res == DialogResult.Cancel)
                return;
            if (res == DialogResult.OK)
            {
                nBlock = mfrmInputBlockSize.blockSize;
            }

            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag) //(item.Tag.Equals(li.Tag))
                    break;
                else
                    idx++;
            }
            string factorVal = li.SubItems[subItemSelected].Text;
            for (int num = idx; num < idx + nBlock; num++)
            {
                if (num < mlstViewDataSets.Items.Count)
                {
                    mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal;
                    ((Factor)((clsDatasetInfo)marrDatasetInfo[num]).marrFactorAssnmnts[subItemSelected - NUM_COLUMNS]).Value = factorVal;
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

            Random rnum = new Random();
            int idxFactor = subItemSelected - NUM_COLUMNS;
            string currFactorVal;
            clsFactorInfo currentFactor = new clsFactorInfo();

            currentFactor = (clsFactorInfo)marrFactorInfo[idxFactor];
            for (int num = 0; num < mlstViewDataSets.Items.Count; num++)
            {
                currFactorVal = currentFactor.marrValues[rnum.Next(currentFactor.marrValues.Count)].ToString();
                mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = currFactorVal;
                ((Factor)((clsDatasetInfo)marrDatasetInfo[num]).marrFactorAssnmnts[subItemSelected - NUM_COLUMNS]).Value = currFactorVal;
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

            int idx = 0;
            int nBlock = 1;
            frmInputBlockSize mfrmInputBlockSize = new frmInputBlockSize();

            DialogResult res = mfrmInputBlockSize.ShowDialog();
            if (res == DialogResult.Cancel)
                return;
            if (res == DialogResult.OK)
            {
                nBlock = mfrmInputBlockSize.blockSize;
            }

            foreach (ListViewItem item in mlstViewDataSets.Items)
            {
                if ((int)item.Tag == (int)li.Tag) //(item.Tag.Equals(li.Tag))
                    break;
                else
                    idx++; // which item is clicked ?
            }
            string factorVal = li.SubItems[subItemSelected].Text; // get it's factor value

            int numVals = ((clsFactorInfo)marrFactorInfo[subItemSelected - NUM_COLUMNS]).vCount; //number of values in this factor
            string[] fvals = new string[numVals]; //Factor values as a string array
            fvals = ((clsFactorInfo)marrFactorInfo[subItemSelected - NUM_COLUMNS]).FactorValues;
            int startIdxVal = 0; //start with this value and its index is...
            for (startIdxVal = 0; startIdxVal < numVals; startIdxVal++)
                if (fvals[startIdxVal].Equals(factorVal))
                    break;

            int start = idx;
            int cycle = 0;
            while (start < mlstViewDataSets.Items.Count)//all cycles
            {
                for (int num = start; num < (start + nBlock); num++)//set values in one block
                {
                    if (num < mlstViewDataSets.Items.Count)
                    {
                        factorVal = fvals[(startIdxVal + cycle) % numVals]; // index should go in a cycle, hence the mod operator.
                        mlstViewDataSets.Items[num].SubItems[subItemSelected].Text = factorVal; // update in the view
                        // update in the datasaet info
                        ((Factor)((clsDatasetInfo)marrDatasetInfo[num]).marrFactorAssnmnts[subItemSelected - NUM_COLUMNS]).Value = factorVal;
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

        public ArrayList DatasetInfo
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

        public ArrayList FactorInfo
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

        public ArrayList NewDatasetOrder
        {
            get
            {
                ArrayList newOrd = new ArrayList();
                foreach (ListViewItem it in mlstViewDataSets.Items)
                    newOrd.Add(it.Tag);
                return newOrd;
            }
        }

        public ArrayList NewDatasetNameOrder
        {
            get
            {
                ArrayList newOrd = new ArrayList();
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