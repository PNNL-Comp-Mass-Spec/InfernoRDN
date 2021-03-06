using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    public class ctlMSMSSelectFromHDDWizPage : Wizard.UI.InternalWizardPage
    {
        private Panel panelStep;
        private Label labelSelect;
        private Button mbtnSelectFiles;
        private Panel panelFileNames;
        private readonly IContainer components = null;

        private string[] strarrFilePaths;
        private List<string> marrDatasetFilePaths = new List<string>();

        private ListView joblistView;
        private Button mbtnClear;
        private ColumnHeader mFileNameColumnHeader;
        private readonly OpenFileDialog openFileDialog1;

        public ctlMSMSSelectFromHDDWizPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            openFileDialog1 = new OpenFileDialog();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
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
            this.mbtnClear = new System.Windows.Forms.Button();
            this.mbtnSelectFiles = new System.Windows.Forms.Button();
            this.labelSelect = new System.Windows.Forms.Label();
            this.panelFileNames = new System.Windows.Forms.Panel();
            this.joblistView = new System.Windows.Forms.ListView();
            this.mFileNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.panelStep.SuspendLayout();
            this.panelFileNames.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(784, 64);
            this.Banner.Subtitle = "Select files from your local or networked drives";
            this.Banner.Title = "Step 2. Select files for Analysis";
            // 
            // panelStep
            // 
            this.panelStep.Controls.Add(this.mbtnClear);
            this.panelStep.Controls.Add(this.mbtnSelectFiles);
            this.panelStep.Controls.Add(this.labelSelect);
            this.panelStep.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStep.Location = new System.Drawing.Point(0, 64);
            this.panelStep.Name = "panelStep";
            this.panelStep.Size = new System.Drawing.Size(784, 24);
            this.panelStep.TabIndex = 2;
            // 
            // mbtnClear
            // 
            this.mbtnClear.Location = new System.Drawing.Point(364, 0);
            this.mbtnClear.Name = "mbtnClear";
            this.mbtnClear.Size = new System.Drawing.Size(54, 24);
            this.mbtnClear.TabIndex = 2;
            this.mbtnClear.Text = "Clear";
            this.mbtnClear.Click += new System.EventHandler(this.mbtnClear_Click);
            // 
            // mbtnSelectFiles
            // 
            this.mbtnSelectFiles.Location = new System.Drawing.Point(134, 0);
            this.mbtnSelectFiles.Name = "mbtnSelectFiles";
            this.mbtnSelectFiles.Size = new System.Drawing.Size(58, 24);
            this.mbtnSelectFiles.TabIndex = 1;
            this.mbtnSelectFiles.Text = "Browse";
            this.mbtnSelectFiles.Click += new System.EventHandler(this.mbtnSelectFiles_Click);
            // 
            // labelSelect
            // 
            this.labelSelect.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelSelect.Location = new System.Drawing.Point(0, 0);
            this.labelSelect.Name = "labelSelect";
            this.labelSelect.Size = new System.Drawing.Size(160, 24);
            this.labelSelect.TabIndex = 0;
            this.labelSelect.Text = "      Select Sequest Files:";
            this.labelSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFileNames
            // 
            this.panelFileNames.Controls.Add(this.joblistView);
            this.panelFileNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileNames.Location = new System.Drawing.Point(0, 88);
            this.panelFileNames.Name = "panelFileNames";
            this.panelFileNames.Size = new System.Drawing.Size(784, 333);
            this.panelFileNames.TabIndex = 3;
            // 
            // joblistView
            // 
            this.joblistView.AllowDrop = true;
            this.joblistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
            {
                this.mFileNameColumnHeader
            });
            this.joblistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.joblistView.FullRowSelect = true;
            this.joblistView.GridLines = true;
            this.joblistView.Location = new System.Drawing.Point(0, 0);
            this.joblistView.Name = "joblistView";
            this.joblistView.Size = new System.Drawing.Size(784, 333);
            this.joblistView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.joblistView.TabIndex = 1;
            this.joblistView.UseCompatibleStateImageBehavior = false;
            this.joblistView.View = System.Windows.Forms.View.Details;
            // 
            // fileName
            // 
            this.mFileNameColumnHeader.Text = "File name";
            this.mFileNameColumnHeader.Width = 420;
            // 
            // ctlMSMSSelectFromHDDWizPage
            // 
            this.Controls.Add(this.panelFileNames);
            this.Controls.Add(this.panelStep);
            this.Name = "ctlMSMSSelectFromHDDWizPage";
            this.Size = new System.Drawing.Size(784, 421);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.ctlMSMSSelectFromHDDWizPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.panelStep, 0);
            this.Controls.SetChildIndex(this.panelFileNames, 0);
            this.panelStep.ResumeLayout(false);
            this.panelFileNames.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private void AddToList(string fileName)
        {
            var dataItem = new ListViewItem(fileName);
            //dataItem.SubItems.Add(datasetInfo.mstrAnalysisJobId);
            //dataItem.SubItems.Add(datasetInfo.mstrDatasetName);

            joblistView.Items.Add(dataItem);
        }

        private void mbtnSelectFiles_Click(object sender, System.EventArgs e)
        {
            var msmsFolder = Settings.Default.msmsFolder;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter =
                "*_out.txt files (*_out.txt)|*_out.txt|*_syn.txt files (*_syn.txt)|*_syn.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            if (msmsFolder != "")
                openFileDialog1.InitialDirectory = msmsFolder;
            else
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.RestoreDirectory = false;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            strarrFilePaths = openFileDialog1.FileNames;

            msmsFolder = Path.GetDirectoryName(strarrFilePaths[0]);
            Settings.Default.msmsFolder = msmsFolder;
            Settings.Default.Save();

            foreach (var filePath in strarrFilePaths)
            {
                var newFileName = Path.GetFileName(filePath);
                if (marrDatasetFilePaths.Contains(filePath))
                {
                    continue;
                }
                marrDatasetFilePaths.Add(filePath);
                AddToList(newFileName);
            }
        }

        private void mbtnClear_Click(object sender, EventArgs e)
        {
            marrDatasetFilePaths.Clear();
            joblistView.Items.Clear();
        }

        private void ctlMSMSSelectFromHDDWizPage_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(Wizard.UI.WizardButtons.Back | Wizard.UI.WizardButtons.Next);
        }

        public List<string> DatasetNames
        {
            get => marrDatasetFilePaths;
            set => marrDatasetFilePaths = value;
        }
    }
}