using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    /// <summary>
    /// Main DAnTE form
    /// </summary>
    public partial class frmDAnTE : Form
    {
        #region Other Variables

        public const string PROGRAM_DATE = "December 13, 2017";

        public const int SUGGESTED_DATASETS_TO_SELECT = 30;
        public const int MAX_DATASETS_TO_SELECT = 60;
        public const int MAX_DATASETS_TO_SELECT_CPU_INTENSIVE = 30;

        private IContainer components;

        // Tab Page controls
        private TabControl mtabControlData;

        // Tab controls for Expressions
        private TabPage ctltabPage;

        private readonly frmShowProgress mfrmShowProgress;

        // ReSharper disable once NotAccessedField.Local
        // Used by HandleFileOpenCompleted in Inferno\Events\frmDAnTE.FileIOEvents.cs
        private List<string> mDataSetNames = new List<string>();

        private string[] mstrArrProteins;
        private string[] mstrArrMassTags;

        // File path of the loaded data, but also used when loading a protein info file or factors file
        private string mstrLoadedfileName;

        // File path of the session (.dnt) file
        private string mSessionFile;

        private readonly Timer mAutoLoadTimer;

        private bool mAutoLoadSessionFile;
        private DateTime mAutoLoadScheduledTime;

        // This is a linux-style path that is used by R to save .png files
        // For example: C:/Users/username/AppData/Roaming/Inferno/_temp.png
        private string mRTempFilePath = "";
        private clsRconnect mRConnector;

        private string mstrFldgTitle;

        private enmDataType mDataSetType = enmDataType.ESET;
        private static frmDAnTE m_frmDAnTE;
        private readonly BackgroundWorker m_BackgroundWorker;

        private frmDAnTEmdi m_frmDAnTEmdi;
        private ToolStripMenuItem mnuItemMissFilt;
        private ToolStripMenuItem mnuItemFC;
        private ToolStripMenuItem ctxtMnuItemFilter;
        private ToolStripSeparator toolStripSeparator13;

        private readonly Dictionary<string, clsDatasetTreeNode> mhtDatasets =
            new Dictionary<string, clsDatasetTreeNode>();

        private readonly Dictionary<string, string> mhtAnalysisObjects = new Dictionary<string, string>();
        private readonly List<clsAnalysisObject> marrAnalysisObjects = new List<clsAnalysisObject>();
        private ToolStripMenuItem mnuItemVenn;

        private int mintFilterTblNum;

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
                mtoolStripDAnTE.Visible = false;
            }

            mAutoLoadTimer = new Timer
            {
                Interval = 250,
                Enabled = true
            };
            mAutoLoadTimer.Tick += mAutoLoadTimer_Tick;

            mfrmShowProgress = new frmShowProgress();

            //Threading -----------------------------------
            m_BackgroundWorker = new BackgroundWorker();
        }

        #endregion

        #region Private methods

        public static frmDAnTE GetChildInstance()
        {
            //if not created yet, Create an instance
            if (m_frmDAnTE == null || m_frmDAnTE.IsDisposed)
                m_frmDAnTE = new frmDAnTE();

            // Return the instance
            return m_frmDAnTE;
        }

        private DataGridViewSelectedRowCollection GetSelectedRows(DataGridView currGrid)
        {
            var selectedRows = currGrid.SelectedRows;

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
                foreach (var rowIndex in rowIndicesAdded)
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
                MessageBox.Show(
                    "Factors must be defined in order to " + currentTask + ".  See Define Factors in the Grouping menu.",
                    "Factors not defined");
                return false;
            }

            return true;
        }

        private bool ValidateIsPlottable(clsDatasetTreeNode mclsSelected, int minimumColCount = 1)
        {
            if (!mclsSelected.mblIsPlottable)
            {
                MessageBox.Show(
                    "Table '" + mclsSelected.mstrDataText +
                    "' does not contain data that can be plotted.  Please select a different table from the list",
                    "Invalid table");
                return false;
            }

            if (minimumColCount > 1 && mclsSelected.mDTable.Columns.Count < minimumColCount)
            {
                MessageBox.Show(
                    "Table '" + mclsSelected.mstrDataText + "' cannot be plotted; it must have at least " +
                    minimumColCount + " columns of data", "Not enough columns");
                return false;
            }
            return true;
        }

        private bool ValidateNodeIsSelected(clsDatasetTreeNode selectedNode)
        {
            if (selectedNode == null)
            {
                MessageBox.Show("Data not loaded (or data table not selected)", "Nothing to do", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
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
                MessageBox.Show(
                    "Table '" + mclsSelected.mstrDataText +
                    "' does not contain a matrix of numeric data.  Please select a different table from the list",
                    "Invalid table");
                return false;
            }

            if (checkColumnCount)
            {
                var numCols = mclsSelected.mDTable.Columns.Count - 1;
                if (numCols <= 0)
                {
                    MessageBox.Show("Table '" + mclsSelected.mstrDataText + "' must have at least 2 columns of data",
                                    "Invalid table");
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Event handlers --------------

        private void OnLoad_event(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mSessionFile))
            {
                return;
            }

            mAutoLoadSessionFile = true;
            mAutoLoadScheduledTime = DateTime.UtcNow.AddMilliseconds(250);
        }

        private void OnClosed_event(object sender, EventArgs e)
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

        /// <summary>
        /// User dropped a file into a empty window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            var s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (s.Length > 1)
                MessageBox.Show("Only one file at a time!", "One file please...",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var fExt = Path.GetExtension(s[0]);
                var fileTypeError = string.IsNullOrEmpty(fExt);

                if (!fileTypeError)
                {
                    if (fExt.Equals(".dnt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        OpenSessionCheckExisting(s[0], USE_THREADED_LOAD);
                    }
                    else if (fExt.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
                    {
                        mDataSetType = enmDataType.ESET;

                        OpenExpressionFile(s[0]);
                    }
                    else
                    {
                        fileTypeError = true;
                    }
                }

                if (fileTypeError)
                {
                    MessageBox.Show("Only .dnt or .csv files can be opened via drag/drop here", "Unsupported file type",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, mhelpProviderDAnTE.HelpNamespace);
        }

        #endregion

        #region Properties

        public clsRconnect RConnector
        {
            set => mRConnector = value;
        }

        public string RTempFilePath
        {
            set => mRTempFilePath = value;
        }

        public frmDAnTEmdi ParentInstance
        {
            set => m_frmDAnTEmdi = value;
        }

        public ToolStrip ToolStripDAnTE => mtoolStripDAnTE;

        public string Title
        {
            get => Text;
            set => Text = value;
        }

        public string SessionFile
        {
            set => mSessionFile = value;
        }

        #endregion

        #region Event Handlers

        void mAutoLoadTimer_Tick(object sender, EventArgs e)
        {
            if (!mAutoLoadSessionFile || DateTime.UtcNow <= mAutoLoadScheduledTime)
            {
                return;
            }

            mAutoLoadSessionFile = false;
            mAutoLoadTimer.Enabled = false;

            SessionFileOpenNonThreaded(mSessionFile);
        }

        private void frmDAnTE_Activated(object sender, EventArgs e)
        {
            if (IsMdiChild)
            {
                mnuStripDAnTE.Visible = false;
                mtoolStripDAnTE.Visible = false;
                var mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
                if (mp != null)
                {
                    ToolStripManager.RevertMerge(mp.mToolStripMDI); //toolstrip reference to parent toolstrip
                    ToolStripManager.Merge(mtoolStripDAnTE, mp.mToolStripMDI);
                }
            }
        }

        #endregion
    }
}