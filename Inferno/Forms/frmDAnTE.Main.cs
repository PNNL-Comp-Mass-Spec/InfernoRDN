using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class frmDAnTE : System.Windows.Forms.Form
    {
        #region Other Variables

        public const string PROGRAM_DATE = "August 6, 2015";

        public const int MAX_DATASETS_TO_SELECT = 30;
        public const int MAX_DATASETS_TO_SELECT_CPU_INTENSIVE = 20;

        private IContainer components;

        // Tab Page controls 
        private TabControl mtabControlData;

        // Tab controls for Expressions
        private TabPage ctltabPage;
        
        private readonly frmShowProgress mfrmShowProgress;

        private ArrayList marrDataSetNames = new ArrayList();

        private string[] mstrArrProteins = null;
        private string[] mstrArrMassTags = null;

        private string mstrLoadedfileName; //filename of the loaded data

        private string sessionFile = null;

        // This is a linux-style path that is used by R to save .png files
        // For example: C:/Users/username/AppData/Roaming/Inferno/_temp.png
        private string mRTempFilePath = "";
        private clsRconnect mRConnector;

        private string mstrFldgTitle;

        private enmDataType dataSetType = enmDataType.ESET;
        private static frmDAnTE m_frmDAnTE;
        private readonly BackgroundWorker m_BackgroundWorker;

        private frmDAnTEmdi m_frmDAnTEmdi;
        private ToolStripMenuItem mnuItemMissFilt;
        private ToolStripMenuItem mnuItemFC;
        private ToolStripMenuItem ctxtMnuItemFilter;
        private ToolStripSeparator toolStripSeparator13;

        private readonly Hashtable mhtDatasets = new Hashtable();
        private readonly Hashtable mhtAnalysisObjects = new Hashtable();
        private readonly ArrayList marrAnalysisObjects = new ArrayList();
        private ToolStripMenuItem mnuItemVenn;

        private int mintFilterTblNum = 0;

        #endregion

        #region Form Constructor

        public frmDAnTE()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Settings.Default.SessionFileName = null;
            Settings.Default.Save();

            if (IsMdiChild)
            {
                //ToolStripManager.Merge(this.mtoolStripDAnTE, "mtoolStripMDI");
                this.mtoolStripDAnTE.Visible = false;
            }

            mfrmShowProgress = new frmShowProgress();

            //Threading -----------------------------------
            this.m_BackgroundWorker = new BackgroundWorker();
        }

        #endregion

        #region Private methods

        public static frmDAnTE GetChildInstance()
        {
            if (m_frmDAnTE == null || m_frmDAnTE.IsDisposed) //if not created yet, Create an instance
                m_frmDAnTE = new frmDAnTE();
            return m_frmDAnTE;  //just created or created earlier.Return it
        }

        private DataGridViewSelectedRowCollection GetSelectedRows(DataGridView currGrid)
        {
            DataGridViewSelectedRowCollection selectedRows = currGrid.SelectedRows;

            if (selectedRows.Count < 1)
            {
                var rowIndicesAdded = new SortedSet<int>();

                // Make a list of the rows with selected cells
                foreach (DataGridViewCell selectedCell in currGrid.SelectedCells)
                {
                    if (rowIndicesAdded.Contains(selectedCell.RowIndex))
                        continue;

                    rowIndicesAdded.Add(selectedCell.RowIndex);

                }

                // Auto select the rows
                foreach (int rowIndex in rowIndicesAdded)
                {
                    currGrid.Rows[rowIndex].Selected = true;
                }

                selectedRows = currGrid.SelectedRows;
            }

            return selectedRows;
        }

        private bool ValidateExpressionsLoaded(string currentTask)
        {
            if (!mhtDatasets.ContainsKey("Expressions"))
            {
                MessageBox.Show("'Expressions' table not found; cannot " + currentTask, "Error");
                return false;
            }

            return true;
        }

        private bool ValidateFactorsDefined(string currentTask)
        {
            if (!mhtDatasets.ContainsKey("Factors"))
            {
                MessageBox.Show("Factors must be defined in order to " + currentTask + ".  See Define Factors in the Grouping menu.", "Factors not defined");
                return false;
            }

            return true;
        }

        private bool ValidateIsPlottable(clsDatasetTreeNode mclsSelected, int minimumColCount = 1)
        {
            if (!mclsSelected.mblIsPlottable)
            {
                MessageBox.Show("Table '" + mclsSelected.mstrDataText + "' does not contain data that can be plotted.  Please select a different table from the list", "Invalid table");
                return false;
            }

            if (minimumColCount > 1 && mclsSelected.mDTable.Columns.Count < minimumColCount)
            {
                MessageBox.Show("Table '" + mclsSelected.mstrDataText + "' cannot be plotted; it must have at least " + minimumColCount + " columns of data", "Not enough columns");
                return false;
            }
            return true;
        }

        private bool ValidateNodeIsSelected(clsDatasetTreeNode selectedNode)
        {
            if (selectedNode == null)
            {
                MessageBox.Show("Data not loaded (or data table not selected)", "Nothing to do");
                return false;
            }

            return true;
        }

        private bool ValidateDataMatrixTableSelected(clsDatasetTreeNode mclsSelected, bool checkColumnCount = false)
        {
            if (mclsSelected == null)
            {
                MessageBox.Show("Please select a numeric data table from the list", "Invalid table");
                return false;
            }

            if (mclsSelected.mDTable == null)
            {
                MessageBox.Show("Please select a numeric data table from the list", "Invalid table");
                return false;
            }

            if (!mclsSelected.mblIsNumeric)
            {
                MessageBox.Show("Table '" + mclsSelected.mstrDataText + "' does not contain a matrix of numeric data.  Please select a different table from the list", "Invalid table");
                return false;
            }

            if (checkColumnCount)
            {
                int numCols = mclsSelected.mDTable.Columns.Count - 1;
                if (numCols <= 0)
                {
                    MessageBox.Show("Table '" + mclsSelected.mstrDataText + "' must have at least 2 columns of data", "Invalid table");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Event handlers --------------

        private void OnLoad_event(object sender, System.EventArgs e)
        {
            //if (IsMdiChild)
            //{
            //    mnuStripDAnTE.Visible = false;
            //    mtoolStripDAnTE.Visible = false;
            //    frmDAnTEmdi mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
            //    ToolStripManager.RevertMerge(mp.mtoolStripMDI); //toolstrip refere to parent toolstrip
            //    ToolStripManager.Merge(this.mtoolStripDAnTE, mp.mtoolStripMDI);
            //}
            if (sessionFile != null)
            {
                OpenSessionThreaded(sessionFile);
            }
        }

        private void OnClosed_event(object sender, System.EventArgs e)
        {
            ToolStripManager.RevertMerge("mtoolStripMDI");
        }

        /// <summary>
        /// What to do when an item from the treeview control is selected
        /// </summary>
        private void ctltreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                ctltreeView.SelectedNode = ctltreeView.GetNodeAt(e.X, e.Y);
            NodeSelect(e.Node);
        }

        private void ctltreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            NodeSelect(e.Node);
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (s.Length > 1)
                MessageBox.Show("Only one file at a time!", "One file please...",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                string fExt = Path.GetExtension(s[0]);
                if (fExt.Equals(".dnt"))
                    OpenSessionThreaded(s[0]);
                else if (fExt.Equals(".csv"))
                {
                    dataSetType = enmDataType.ESET;
                    mstrLoadedfileName = s[0];
                    DataFileOpenThreaded(s[0], "Opening data in a flat file...");
                }
                else
                {
                    MessageBox.Show("Wrong file type!", "Use only .dnt files...",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, this.mhelpProviderDAnTE.HelpNamespace);
        }

        #endregion

        #region Properties

        public clsRconnect RConnector
        {
            set
            {
                mRConnector = value;
            }
        }

        public string RTempFilePath
        {
            set { mRTempFilePath = value; }
        }

        public frmDAnTEmdi ParentInstance
        {
            set
            {
                m_frmDAnTEmdi = value;
            }
        }

        public ToolStrip ToolStripDAnTE
        {
            get
            {
                return mtoolStripDAnTE;
            }
        }

        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public string SessionFile
        {
            set
            {
                sessionFile = value;
            }
        }

        #endregion

        private void frmDAnTE_Activated(object sender, EventArgs e)
        {
            if (IsMdiChild)
            {
                mnuStripDAnTE.Visible = false;
                mtoolStripDAnTE.Visible = false;
                frmDAnTEmdi mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
                ToolStripManager.RevertMerge(mp.mtoolStripMDI); //toolstrip refere to parent toolstrip
                ToolStripManager.Merge(this.mtoolStripDAnTE, mp.mtoolStripMDI);
            }
        }
     
    }
}
