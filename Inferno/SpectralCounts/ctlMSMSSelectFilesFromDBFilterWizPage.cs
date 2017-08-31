using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public class ctlMSMSSelectFilesFromDBFilterWizPage : Wizard.UI.InternalWizardPage
    {
        public string mstrFilter;
        private Panel panelStep;
        private Label labelSelect;
        private Panel panelFileNames;
        private readonly System.ComponentModel.IContainer components = null;

        private List<string> marrDatasetInfo = new List<string>();
        private string[] mstrArrProjects;

        private TextBox mtxtBoxfilter;

        private ListView mlstViewJobs;
        private ColumnHeader datasetName;

        private Button mbtnFilter;
        private Button buttonToggleAll;

        private bool FilesSelected = false;
        private const int NUM_COLUMNS = 8;
        private readonly ListViewItemComparer _lvwItemComparer;

        //private bool defaultSelected = false ;
        //private int selectedFileIndex = 0 ;
        //MultiAlignWin.enmSelectType selection ;
        //private string job_Id ;
        private Button buttonClearAll;
        //private bool mblRunIt = false;
        private DataTable mdtLabKeyFiles = new DataTable();
        private readonly SortedSet<string> mhTable = new SortedSet<string>();
        private ColumnHeader numPeptides;
        private Label label2;
        private TextBox mtxtBoxURL;
        private Label label1;
        private string wildcardFilter = "";
        private string mstrUrl = "";
        private string mstrFolder = "";
        private Label mlblSelected;
        private readonly clsRconnect rConnector;
        private Button mbtnDefaults;
        private ComboBox mcmbBoxProjects;
        private int nselected = 0;


        public ctlMSMSSelectFilesFromDBFilterWizPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            _lvwItemComparer = new ListViewItemComparer();
            mlstViewJobs.ListViewItemSorter = _lvwItemComparer;

            SetActive += ctlSelectFilesFromDBFilterWizardPage_SetActive;
        }

        public ctlMSMSSelectFilesFromDBFilterWizPage(clsRconnect rconn)
        {
            rConnector = rconn;

            // This call is required by the Windows Form Designer.
            InitializeComponent();

            _lvwItemComparer = new ListViewItemComparer();
            mlstViewJobs.ListViewItemSorter = _lvwItemComparer;

            SetActive += ctlSelectFilesFromDBFilterWizardPage_SetActive;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelStep = new System.Windows.Forms.Panel();
            this.mcmbBoxProjects = new System.Windows.Forms.ComboBox();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.mlblSelected = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtBoxURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.buttonToggleAll = new System.Windows.Forms.Button();
            this.mbtnFilter = new System.Windows.Forms.Button();
            this.mtxtBoxfilter = new System.Windows.Forms.TextBox();
            this.labelSelect = new System.Windows.Forms.Label();
            this.panelFileNames = new System.Windows.Forms.Panel();
            this.mlstViewJobs = new System.Windows.Forms.ListView();
            this.datasetName = new System.Windows.Forms.ColumnHeader();
            this.numPeptides = new System.Windows.Forms.ColumnHeader();
            this.panelStep.SuspendLayout();
            this.panelFileNames.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(784, 64);
            this.Banner.Subtitle = "Specify partial names for datasets in LabKey server";
            this.Banner.Title = "Step 2. Select Datasets for Analysis";
            // 
            // panelStep
            // 
            this.panelStep.Controls.Add(this.mcmbBoxProjects);
            this.panelStep.Controls.Add(this.mbtnDefaults);
            this.panelStep.Controls.Add(this.mlblSelected);
            this.panelStep.Controls.Add(this.label2);
            this.panelStep.Controls.Add(this.mtxtBoxURL);
            this.panelStep.Controls.Add(this.label1);
            this.panelStep.Controls.Add(this.buttonClearAll);
            this.panelStep.Controls.Add(this.buttonToggleAll);
            this.panelStep.Controls.Add(this.mbtnFilter);
            this.panelStep.Controls.Add(this.mtxtBoxfilter);
            this.panelStep.Controls.Add(this.labelSelect);
            this.panelStep.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStep.Location = new System.Drawing.Point(0, 64);
            this.panelStep.Name = "panelStep";
            this.panelStep.Size = new System.Drawing.Size(784, 88);
            this.panelStep.TabIndex = 2;
            // 
            // mcmbBoxProjects
            // 
            this.mcmbBoxProjects.FormattingEnabled = true;
            this.mcmbBoxProjects.Location = new System.Drawing.Point(446, 12);
            this.mcmbBoxProjects.Name = "mcmbBoxProjects";
            this.mcmbBoxProjects.Size = new System.Drawing.Size(165, 21);
            this.mcmbBoxProjects.TabIndex = 11;
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                             System.Drawing.FontStyle.Regular,
                                                             System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnDefaults.Location = new System.Drawing.Point(642, 9);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(66, 24);
            this.mbtnDefaults.TabIndex = 10;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // mlblSelected
            // 
            this.mlblSelected.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F,
                                                             System.Drawing.FontStyle.Regular,
                                                             System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblSelected.Location = new System.Drawing.Point(639, 51);
            this.mlblSelected.Name = "mlblSelected";
            this.mlblSelected.Size = new System.Drawing.Size(127, 22);
            this.mlblSelected.TabIndex = 9;
            this.mlblSelected.Text = "0/0 selected.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(377, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Project Path:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mtxtBoxURL
            // 
            this.mtxtBoxURL.AcceptsReturn = true;
            this.mtxtBoxURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                           System.Drawing.FontStyle.Regular,
                                                           System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtBoxURL.Location = new System.Drawing.Point(119, 12);
            this.mtxtBoxURL.Name = "mtxtBoxURL";
            this.mtxtBoxURL.Size = new System.Drawing.Size(243, 20);
            this.mtxtBoxURL.TabIndex = 6;
            this.mtxtBoxURL.Text = "http://proteomics.tgen.org/labkey/";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "LabKey base URL:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Enabled = false;
            this.buttonClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClearAll.Location = new System.Drawing.Point(536, 49);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(75, 24);
            this.buttonClearAll.TabIndex = 4;
            this.buttonClearAll.Text = "Remove All";
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // buttonToggleAll
            // 
            this.buttonToggleAll.Enabled = false;
            this.buttonToggleAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                                System.Drawing.FontStyle.Regular,
                                                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonToggleAll.Location = new System.Drawing.Point(446, 49);
            this.buttonToggleAll.Name = "buttonToggleAll";
            this.buttonToggleAll.Size = new System.Drawing.Size(66, 24);
            this.buttonToggleAll.TabIndex = 3;
            this.buttonToggleAll.Text = "Toggle All";
            this.buttonToggleAll.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // mbtnFilter
            // 
            this.mbtnFilter.Location = new System.Drawing.Point(380, 49);
            this.mbtnFilter.Name = "mbtnFilter";
            this.mbtnFilter.Size = new System.Drawing.Size(34, 24);
            this.mbtnFilter.TabIndex = 0;
            this.mbtnFilter.Text = "OK";
            this.mbtnFilter.Click += new System.EventHandler(this.mbtnOKFilter_Click);
            // 
            // mtxtBoxfilter
            // 
            this.mtxtBoxfilter.AcceptsReturn = true;
            this.mtxtBoxfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                              System.Drawing.FontStyle.Regular,
                                                              System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtBoxfilter.Location = new System.Drawing.Point(119, 51);
            this.mtxtBoxfilter.Name = "mtxtBoxfilter";
            this.mtxtBoxfilter.Size = new System.Drawing.Size(243, 20);
            this.mtxtBoxfilter.TabIndex = 2;
            this.mtxtBoxfilter.Text = "LTQVELOS_";
            this.mtxtBoxfilter.TextChanged += new System.EventHandler(this.filterBox_TextChanged);
            this.mtxtBoxfilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtBoxfilter_KeyPress);
            // 
            // labelSelect
            // 
            this.labelSelect.Location = new System.Drawing.Point(3, 42);
            this.labelSelect.Name = "labelSelect";
            this.labelSelect.Size = new System.Drawing.Size(106, 39);
            this.labelSelect.TabIndex = 0;
            this.labelSelect.Text = "Specify part of the name of the dataset:";
            this.labelSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFileNames
            // 
            this.panelFileNames.Controls.Add(this.mlstViewJobs);
            this.panelFileNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileNames.Location = new System.Drawing.Point(0, 152);
            this.panelFileNames.Name = "panelFileNames";
            this.panelFileNames.Size = new System.Drawing.Size(784, 269);
            this.panelFileNames.TabIndex = 3;
            // 
            // mlstViewJobs
            // 
            this.mlstViewJobs.AllowDrop = true;
            this.mlstViewJobs.CheckBoxes = true;
            this.mlstViewJobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
            {
                this.datasetName,
                this.numPeptides
            });
            this.mlstViewJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlstViewJobs.FullRowSelect = true;
            this.mlstViewJobs.GridLines = true;
            this.mlstViewJobs.Location = new System.Drawing.Point(0, 0);
            this.mlstViewJobs.Name = "mlstViewJobs";
            this.mlstViewJobs.Size = new System.Drawing.Size(784, 269);
            this.mlstViewJobs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.mlstViewJobs.TabIndex = 0;
            this.mlstViewJobs.UseCompatibleStateImageBehavior = false;
            this.mlstViewJobs.View = System.Windows.Forms.View.Details;
            this.mlstViewJobs.ItemChecked +=
                new System.Windows.Forms.ItemCheckedEventHandler(this.joblistView_ItemChecked);
            this.mlstViewJobs.ColumnClick +=
                new System.Windows.Forms.ColumnClickEventHandler(this.joblistView_ColumnClick);
            // 
            // datasetName
            // 
            this.datasetName.Text = "Dataset name";
            this.datasetName.Width = 321;
            // 
            // numPeptides
            // 
            this.numPeptides.Text = "Peptides";
            this.numPeptides.Width = 120;
            // 
            // ctlMSMSSelectFilesFromDBFilterWizPage
            // 
            this.Controls.Add(this.panelFileNames);
            this.Controls.Add(this.panelStep);
            this.Name = "ctlMSMSSelectFilesFromDBFilterWizPage";
            this.Size = new System.Drawing.Size(784, 421);
            this.Load += new System.EventHandler(this.ctlMSMSSelectFilesFromDBFilterWizPage_Load);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.panelStep, 0);
            this.Controls.SetChildIndex(this.panelFileNames, 0);
            this.panelStep.ResumeLayout(false);
            this.panelStep.PerformLayout();
            this.panelFileNames.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private void ctlSelectFilesFromDBFilterWizardPage_SetActive(object sender,
                                                                    System.ComponentModel.CancelEventArgs e)
        {
            SetWizardButtons(Wizard.UI.WizardButtons.Back | Wizard.UI.WizardButtons.Next);
            if (mlstViewJobs.Items.Count > 0)
            {
                buttonToggleAll.Enabled = true;
            }
        }

        private void ctlMSMSSelectFilesFromDBFilterWizPage_Load(object sender, EventArgs e)
        {
            mbtnFilter.Focus();
            if (!GetLabkeyProjectNames())
                return;

            foreach (var project in mstrArrProjects)
                mcmbBoxProjects.Items.Add(project);

            if (mcmbBoxProjects.Items.Count > 0)
                mcmbBoxProjects.SelectedIndex = 0;
        }

        private void joblistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwItemComparer.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwItemComparer.Order == SortOrder.Ascending)
                {
                    _lvwItemComparer.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwItemComparer.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwItemComparer.SortColumn = e.Column;
                _lvwItemComparer.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            mlstViewJobs.Sort();
        }

        private void mbtnOKFilter_Click(object sender, EventArgs e)
        {
            if (!FetchMatchingData())
                MessageBox.Show("No data returned. Check your inputs again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        } // method

        private bool FetchMatchingData()
        {
            if (mtxtBoxfilter.Text.Length == 0)
            {
                MessageBox.Show("You must enter a part of the dataset name first!", "Ooops!");
                return false;
            }
            //string tmpwildcardFilter = mtxtBoxfilter.Text;

            var selectedProject = (string)mcmbBoxProjects.SelectedItem;

            if (!mtxtBoxfilter.Text.Equals(wildcardFilter) || !mtxtBoxURL.Text.Equals(mstrUrl) ||
                !selectedProject.Equals(mstrFolder))
                //check if filter string has changed
            {
                wildcardFilter = mtxtBoxfilter.Text;
                mstrFolder = (string)mcmbBoxProjects.SelectedItem;
                mstrUrl = mtxtBoxURL.Text;

                if (GetJobs2Table(wildcardFilter, mstrUrl, mstrFolder))
                {
                    AddToList(mdtLabKeyFiles);
                    if (mlstViewJobs.Items.Count > 0)
                    {
                        buttonToggleAll.Enabled = true;
                        buttonClearAll.Enabled = true;
                    }
                    nselected = mlstViewJobs.CheckedIndices.Count;
                    mlblSelected.Text = nselected.ToString() + "/" + mlstViewJobs.Items.Count.ToString() + " selected.";
                    return true;
                }
                return false;
                //MessageBox.Show(mlstViewJobs.Items.Count.ToString() + " files in total.", "Total",
                //    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            return true;
        }

        private void AddToList(DataTable mDT)
        {
            foreach (DataRow drow in mDT.Rows)
            {
                if (!mhTable.Contains(drow[1].ToString()))
                {
                    mhTable.Add(drow[1].ToString());
                    var dataItem = new ListViewItem(drow[1].ToString());
                    dataItem.SubItems.Add(drow[2].ToString());

                    dataItem.Tag = mlstViewJobs.Items.Count;
                    mlstViewJobs.Items.Add(dataItem);
                }
            }
        }

        private void SetupFilter(string field)
        {
            string fileFilter = null;

            fileFilter = @"filefilter<-makeFilter(c(""DatasetName"",""CONTAINS"",""" + field + @"""))";

            mstrFilter = fileFilter;
        }

        #region Get data from the DB

        private bool GetLabkeyProjectNames()
        {
            var success = true;
            var rcmd = @"projectNames <- LabKeyProjects()";

            try
            {
                rConnector.EvaluateNoReturn(rcmd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if (rConnector.GetRstringVector("projectNames"))
            {
                mstrArrProjects = rConnector.Vector;
            }
            else
                success = false;
            return success;
        }

        private bool GetJobs2Table(string datasetname, string mstrURL, string mstrPath)
        {
            var success = true;
            var rcmd = @"fileNames <- LabkeyFetch(""" + datasetname + @""",""" + mstrURL + @""",""/" + mstrPath +
                       @"/"",""ms2"",""DatasetNames"")";

            try
            {
                rConnector.EvaluateNoReturn(rcmd);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if (rConnector.GetTableFromRmatrixNonNumeric("fileNames"))
            {
                mdtLabKeyFiles = rConnector.DataTable.Copy();
                mdtLabKeyFiles.TableName = "Files";
            }
            else
                success = false;
            return success;
        }


        private string GetFileNameFromPath(string sourcePath, string extn)
        {
            var fileName = "";

            var dir = new System.IO.DirectoryInfo(sourcePath);
            foreach (var f in dir.GetFiles())
            {
                if (f.Name.ToLower().EndsWith(extn.ToLower()))
                {
                    fileName = f.Name;
                    break;
                }
            }
            return fileName;
        }

        #endregion

        private void filterBox_TextChanged(object sender, EventArgs e)
        {
            if (mtxtBoxfilter.Text.Length == 0)
                mbtnFilter.Enabled = false;
            else
                mbtnFilter.Enabled = true;
        }

        private void SelectCheckedItems()
        {
            marrDatasetInfo.Clear();
            var indexes = mlstViewJobs.CheckedIndices;

            foreach (int i in indexes)
            {
                //originalIndex = Convert.ToInt16(mlstViewJobs.Items[i].Tag) ;
                //archPath = mdtLabKeyFiles.Rows[originalIndex][0].ToString();
                marrDatasetInfo.Add(mlstViewJobs.Items[i].Text);
            }
        }


        private void joblistView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (mlstViewJobs.CheckedIndices.Count == 0 && e.NewValue == CheckState.Unchecked)
            {
                FilesSelected = false;
            }
            else
            {
                FilesSelected = true;
            }
            nselected = mlstViewJobs.CheckedIndices.Count;
            mlblSelected.Text = nselected.ToString() + "/" + mlstViewJobs.Items.Count.ToString() + " selected.";
        }


        private void buttonToggleAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < mlstViewJobs.Items.Count; i++)
            {
                if (mlstViewJobs.Items[i].Checked)
                {
                    mlstViewJobs.Items[i].Checked = false;
                }
                else
                {
                    mlstViewJobs.Items[i].Checked = true;
                }
            }
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            mlstViewJobs.Items.Clear();
            wildcardFilter = "";
            mdtLabKeyFiles.Clear();
            mhTable.Clear();

            buttonClearAll.Enabled = false;
            buttonToggleAll.Enabled = false;

            nselected = mlstViewJobs.CheckedIndices.Count;
            mlblSelected.Text = nselected.ToString() + "/" + mlstViewJobs.Items.Count.ToString() + " selected.";
        }

        private void mtxtBoxfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                FetchMatchingData();
        }

        private void joblistView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (mlstViewJobs.CheckedIndices.Count == 0)
            {
                FilesSelected = false;
            }
            else
            {
                FilesSelected = true;
            }
            nselected = mlstViewJobs.CheckedIndices.Count;
            mlblSelected.Text = nselected.ToString() + "/" + mlstViewJobs.Items.Count.ToString() + " selected.";
        }

        private void mbtnDefaults_Click(object sender, EventArgs e)
        {
            mtxtBoxURL.Text = "http://proteomics.tgen.org/labkey";
            mcmbBoxProjects.SelectedItem = 0;
            mtxtBoxfilter.Text = "LTQVELOS_";
            mstrUrl = "";
            mstrFolder = "";
            mstrFilter = "";
        }

        #region Accessors -----------------------------------------

        public List<string> DatasetNames
        {
            get
            {
                if (FilesSelected)
                    SelectCheckedItems();

                return marrDatasetInfo;
            }
            set => marrDatasetInfo = value;
        }

        public string ProjectFolder => mcmbBoxProjects.SelectedItem.ToString();

        #endregion
    }

    #region Class that Implements the manual sorting of items by columns.

    // Implements the manual sorting of items by columns.
    public class ListViewItemComparer : IComparer
    {
        // Specifies the column to be sorted
        private int ColumnToSort;

        // Specifies the order in which to sort (i.e. 'Ascending').
        private SortOrder OrderOfSort;

        // Case insensitive comparer object
        private CaseInsensitiveComparer ObjectCompare;

        // Class constructor, initializes various elements
        public ListViewItemComparer()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        // This method is inherited from the IComparer interface.
        // It compares the two objects passed using a case
        // insensitive comparison.
        //
        // x: First object to be compared
        // y: Second object to be compared
        //
        // The result of the comparison. "0" if equal,
        // negative if 'x' is less than 'y' and
        // positive if 'x' is greater than 'y'
        public int Compare(object x, object y)
        {
            // Cast the objects to be compared to ListViewItem objects
            var listviewX = (ListViewItem)x;
            var listviewY = (ListViewItem)y;

            // Case insensitive Compare
            var compareResult = ObjectCompare.Compare(
                listviewX.SubItems[ColumnToSort].Text,
                listviewY.SubItems[ColumnToSort].Text
                );

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }

            if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }

            // Return '0' to indicate they are equal
            return 0;
        }

        // Gets or sets the number of the column to which to
        // apply the sorting operation (Defaults to '0').
        public int SortColumn
        {
            set => ColumnToSort = value;
            get => ColumnToSort;
        }

        // Gets or sets the order of sorting to apply
        // (for example, 'Ascending' or 'Descending').
        public SortOrder Order
        {
            set => OrderOfSort = value;
            get => OrderOfSort;
        }
    }

    #endregion
}