using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using ICSharpCode.SharpZipLib.Zip;

namespace DAnTE.Inferno
{
    public enum enmMSMSreadType { SEQOUT, SYNOUT, LABKEY };

    public partial class frmMSMSWizard : Wizard.UI.WizardSheet
    {
        private ctlMSMSWelcomeWizPage mctlWelcomePage;
        private ctlMSMSSelectFilesFromDBFilterWizPage mctlSelectFromLabKeyPage;
        private ctlMSMSSelectFromHDDWizPage mctlSelectHDDPage;
        private ctlMSMSparaWizPage mctlParaPage;
        private ctlMSMSLabKeyParaWizPage mctlLabKeyParaPage;
        private ctlMSMSPerformWizPage mctlPerformPage;
        private ctlMSMSCompleteWizPage mctlCompletedPage;
        private ArrayList marrFileNames;
        private ArrayList marrLabKeyPathNames;
        private ArrayList successfulDataSets = new ArrayList();
        private string mstrProjFolder;
        private string mstrAnalysisFolder;
        private string mstrSeqOutFolder;
        private int XcRank = 1;
        private string XCorrTh = "";
        private string DelCn2Th = "";
        private string TrypState = "";
        private bool mblAnalysisStarted = false;
        private int progressPrcnt = 0;
        private clsRconnect rConnector;
        private DataTable mDTEset = new DataTable();
        private enmMSMSreadType datasource;
        private bool runPFE = false;
        private bool mblUseLabKeyOutFiles = false;
        private string mstrPepProph = "0.95";

        private System.ComponentModel.BackgroundWorker backgroundWorker1;

        public frmMSMSWizard()
        {
            InitializeComponent();
            Init();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_WorkProgressChanged);
        }

        public frmMSMSWizard(clsRconnect rconn)
        {
            rConnector = rconn;
            InitializeComponent();
            Init();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_WorkProgressChanged);
        }

        private void Init()
        {
            marrFileNames = new ArrayList();

            mctlWelcomePage = new ctlMSMSWelcomeWizPage();
            mctlSelectFromLabKeyPage = new ctlMSMSSelectFilesFromDBFilterWizPage(rConnector);
            mctlSelectHDDPage = new ctlMSMSSelectFromHDDWizPage();
            mctlParaPage = new ctlMSMSparaWizPage();
            mctlLabKeyParaPage = new ctlMSMSLabKeyParaWizPage();
            mctlPerformPage = new ctlMSMSPerformWizPage();
            mctlCompletedPage = new ctlMSMSCompleteWizPage();

            mctlWelcomePage.Dock = DockStyle.Fill;
            mctlSelectFromLabKeyPage.Dock = DockStyle.Fill;
            mctlSelectHDDPage.Dock = DockStyle.Fill;
            mctlParaPage.Dock = DockStyle.Fill;
            mctlLabKeyParaPage.Dock = DockStyle.Fill;
            mctlPerformPage.Dock = DockStyle.Fill;
            mctlCompletedPage.Dock = DockStyle.Fill;


            mctlWelcomePage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlWelcomePage_WizardNext);
            mctlSelectFromLabKeyPage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlSelectFromLabKeyPage_WizardNext);
            mctlSelectFromLabKeyPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlSelectFromLabKeyPage_WizardBack);
            mctlSelectHDDPage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlSelectHDDPage_WizardNext);
            mctlSelectHDDPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlSelectHDDPage_WizardBack);
            mctlParaPage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlparaPage_WizardNext);
            mctlParaPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlparaPage_WizardBack);
            mctlLabKeyParaPage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlLabKeyParaPage_WizardNext);
            mctlLabKeyParaPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlLabKeyParaPage_WizardBack);
            mctlPerformPage.WizardNext += new Wizard.UI.WizardPageEventHandler(mctlPerformPage_WizardNext);
            mctlPerformPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlPerformPage_WizardBack);
            mctlCompletedPage.WizardFinish += new CancelEventHandler(mctlCompletedPage_WizardFinish);
            mctlCompletedPage.WizardBack += new Wizard.UI.WizardPageEventHandler(mctlCompletedPage_WizardBack);

            this.Pages.Add(mctlWelcomePage);
            this.Pages.Add(mctlSelectFromLabKeyPage);
            this.Pages.Add(mctlSelectHDDPage);
            this.Pages.Add(mctlParaPage);
            this.Pages.Add(mctlLabKeyParaPage);
            this.Pages.Add(mctlPerformPage);
            this.Pages.Add(mctlCompletedPage);
        }

        private ArrayList ConvertSYNOUT2DatasetNames(ArrayList filenames)
        {
            ArrayList datasets = new ArrayList();
            string dataname = null;
            int extidx;

            for (int i = 0; i < filenames.Count; i++)
            {
                dataname = Path.GetFileName(filenames[i].ToString());
                if ((extidx = dataname.ToLower().IndexOf("_out.txt")) == -1)
                    if ((extidx = dataname.ToLower().IndexOf("_out.zip")) == -1)
                        extidx = dataname.ToLower().IndexOf("_syn.txt");
                if (extidx != -1)
                    dataname = dataname.Substring(0, extidx);
                datasets.Add(dataname);
            }
            return datasets;
        }

        private ArrayList GetSEQOUT2DatasetNames(string mstrSeqFolder)
        {
            ArrayList marrDatasets = new ArrayList();
            Hashtable mhtDatasets = new Hashtable();
            string mstrDataset = "";

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(mstrSeqFolder);
            foreach (System.IO.FileInfo f in dir.GetFiles())
            {
                if (Path.GetExtension(f.Name).ToLower().Equals(".out"))
                {
                    mstrDataset = f.Name.Substring(0, f.Name.IndexOf("."));
                    if (!mhtDatasets.Contains(mstrDataset))
                    {
                        mhtDatasets.Add(mstrDataset, string.Empty);
                        marrDatasets.Add(mstrDataset);
                    }
                }
            }
            return marrDatasets;
        }

        /// <summary>
        /// This is where main action takes place.
        /// Will be called when the 'Next' button is pressed on the 'Select Parameters' page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList datasetnames = new ArrayList();
            progressPrcnt = 0;

            switch (datasource)
            {
                case enmMSMSreadType.SYNOUT:
                    // *_syn.txt or *_out.txt files from the local drive
                    backgroundWorker1.ReportProgress(progressPrcnt, "Copying files ...");
                    // file copy
                    runPFE = CopyMSMSFiles(); // *_syn.txt or *_out.txt files from local folder
                    datasetnames = ConvertSYNOUT2DatasetNames(marrFileNames);
                    if (runPFE) // if *_out.txt files are found (but not the corresponding *_syn.txt)
                        RunPeptideFileExtractor(datasetnames);
                    CreatSpectralCountTablesSEQSYN(datasetnames, e);
                    break;
                case enmMSMSreadType.LABKEY:
                    // Select from the LabKey server
                    backgroundWorker1.ReportProgress(50, "");
                    CreatSpectralCountTablesLabKey(marrLabKeyPathNames, e);
                    break;
                case enmMSMSreadType.SEQOUT:
                    //Process Sequest *.Out files from a local folder
                    datasetnames = GetSEQOUT2DatasetNames(mstrAnalysisFolder);
                    progressPrcnt = 10;
                    backgroundWorker1.ReportProgress(progressPrcnt, "Selecting datasets ...");
                    RunPeptideFileExtractor(datasetnames);
                    CreatSpectralCountTablesSEQSYN(datasetnames, e);
                    break;
                default:
                    break;
            }
            //backgroundWorker1.ReportProgress(60, "");
        }

        private void CreatSpectralCountTablesSEQSYN(ArrayList datasetnames, DoWorkEventArgs e)
        {
            string dName = "";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(mstrAnalysisFolder);
            foreach (System.IO.FileInfo f in dir.GetFiles())
            {
                if (f.Name.EndsWith("_syn.txt"))
                {
                    dName = f.Name.Substring(0, f.Name.IndexOf("_syn.txt"));
                    if (datasetnames.Contains(dName))
                        successfulDataSets.Add(dName);
                }
            }
            if (successfulDataSets.Count > 1)
            {
                string fileList = @"c(""" + successfulDataSets[0].ToString();
                string rcmd = "X <- createMSMSdt.SpectralCount(";
                string dataFolder = mstrAnalysisFolder.Replace("\\", "/");
                for (int i = 1; i < successfulDataSets.Count; i++)
                {
                    fileList = fileList + @""",""" + successfulDataSets[i].ToString();
                }
                fileList += @""")";

                rcmd += fileList + @",""" + dataFolder + @""",XcRank=" + XcRank.ToString() +
                    "," + XCorrTh + "," + DelCn2Th + "," + TrypState + ")";

                try
                {
                    backgroundWorker1.ReportProgress(70,
                        "Creating peptide count table ... may take a while for large datasets ...");
                    rConnector.EvaluateNoReturn(rcmd);
                    rConnector.EvaluateNoReturn("Eset <- X$eset");
                    rConnector.EvaluateNoReturn("EsetRows <- X$rows");
                    var rows = rConnector.GetSymbolAsNumbers("EsetRows");
                    if ((int)rows[0] > 0)
                    {
                        if (rConnector.GetTableFromRmatrix("Eset"))
                        {
                            mDTEset = rConnector.DataTable.Copy();
                            mDTEset.TableName = "Eset";
                            rConnector.EvaluateNoReturn("cat(\"Spectral count data obtained.\n\")");
                            e.Result = enmDataType.ESET;
                        }
                        else
                            e.Result = null;
                    }
                    else
                        e.Result = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                    e.Result = null;
                    e.Cancel = true;
                }
                backgroundWorker1.ReportProgress(100, "Done.");
            }
            else
                backgroundWorker1.ReportProgress(100, "You need at leaset two unique datasets... Giving up.");
        }

        private void CreatSpectralCountTablesLabKey(ArrayList successfulDataSets, DoWorkEventArgs e)
        {
            if (successfulDataSets.Count > 1)
            {
                string fileList = @"c(""" + successfulDataSets[0].ToString();
                string rcmd = "X <- LabKeySpectralCounts(";

                for (int i = 1; i < successfulDataSets.Count; i++)
                {
                    fileList = fileList + @""",""" + successfulDataSets[i].ToString();
                }
                fileList += @""")";

                rcmd += fileList + @",""/" + mstrProjFolder + @"/"",PepProph=" + mstrPepProph + ")";

                try
                {
                    backgroundWorker1.ReportProgress(70,
                        "Creating peptide count table ... may take a while for large datasets ...");
                    rConnector.EvaluateNoReturn(rcmd);
                    rConnector.EvaluateNoReturn("Eset <- X$eset");
                    rConnector.EvaluateNoReturn("EsetRows <- X$rows");
                    var rows = rConnector.GetSymbolAsNumbers("EsetRows");
                    if ((int)rows[0] > 0)
                    {
                        if (rConnector.GetTableFromRmatrix("Eset"))
                        {
                            mDTEset = rConnector.DataTable.Copy();
                            mDTEset.TableName = "Eset";
                            rConnector.EvaluateNoReturn("cat(\"Spectral count data obtained.\n\")");
                            e.Result = enmDataType.ESET;
                        }
                        else
                            e.Result = null;
                    }
                    else
                    {
                        e.Result = null;
                        backgroundWorker1.ReportProgress(100, "Empty table returned.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                    e.Result = null;
                    e.Cancel = true;
                }
                backgroundWorker1.ReportProgress(100, "Done.");
            }
            else
                backgroundWorker1.ReportProgress(100, "You need at leaset two unique datasets... Giving up.");
        }

        /// <summary>
        /// Copy _syn.txt or _out.txt files to the analysis folder
        /// Will also copy *_syn.txt files from LabKey (but not the _out.zip files. use
        /// CopyUnzipMSMSOutFiles() to copy _out.zip files
        /// </summary>
        /// <returns></returns>
        private bool CopyMSMSFiles()
        {
            string sourceFName = null, destPath = null, sourcePath;
            FileInfo destFinfo, sourceFinfo;
            bool runPFE = false;
            int step = (int)(marrFileNames.Count != 0 ? 30 / marrFileNames.Count : 30); // Progressbar upto 30%

            for (int i = 0; i < marrFileNames.Count; i++)
            {
                sourcePath = marrFileNames[i].ToString();
                if (!(sourcePath.EndsWith("_syn.txt")))
                    runPFE = true;
                sourceFName = Path.GetFileName(sourcePath);
                destPath = Path.Combine(mstrAnalysisFolder, sourceFName);
                sourceFinfo = new FileInfo(sourcePath);
                destFinfo = new FileInfo(destPath);

                if (((System.IO.File.Exists(destPath)) &&
                    DateTime.Compare(sourceFinfo.LastWriteTime, destFinfo.LastWriteTime) > 0) ||
                    (!System.IO.File.Exists(destPath)))
                {
                    try
                    {
                        System.IO.File.Copy(sourcePath, destPath, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                progressPrcnt += step;
                backgroundWorker1.ReportProgress(progressPrcnt, Path.GetFileName(marrFileNames[i].ToString()));
            }
            return runPFE;
        }

        /// <summary>
        /// Copy _out.zip files from DMS. This will unzip and copy on the fly
        /// </summary>
        /// <returns></returns>
        private bool CopyUnzipMSMSOutFiles()
        {
            string destFName = null, destPath = null, sourcePath;
            int step = (int)(marrFileNames.Count != 0 ? 30 / marrFileNames.Count : 30); // Progressbar upto 30%

            for (int i = 0; i < marrFileNames.Count; i++)
            {
                sourcePath = marrFileNames[i].ToString();

                using (ZipInputStream s = new ZipInputStream(File.OpenRead(sourcePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        destFName = Path.GetFileName(theEntry.Name);
                        destPath = Path.Combine(mstrAnalysisFolder, destFName);
                        if (destFName != string.Empty)
                        {
                            if (!System.IO.File.Exists(destPath))
                                using (FileStream streamWriter = File.Create(destPath))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                        }
                    }
                }
                progressPrcnt += step;
                backgroundWorker1.ReportProgress(progressPrcnt, Path.GetFileName(marrFileNames[i].ToString()));
            }
            return true;
        }

        /// <summary>
        /// Run 'Peptide File Extractor' application on *_out.txt files
        /// </summary>
        /// <param name="marrDatasetnames"></param>
        private void RunPeptideFileExtractor(ArrayList marrDatasetnames)
        {
            int step = (int)(marrDatasetnames.Count != 0 ? 30 / marrDatasetnames.Count : 30); // Progressbar upto 30%

            System.Diagnostics.ProcessStartInfo pfec = new System.Diagnostics.ProcessStartInfo(
                Path.Combine(Application.StartupPath, "Tools", "PeptideFileExtractorConsole.exe"))
            {
                RedirectStandardOutput = false,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = true
            };

            backgroundWorker1.ReportProgress(progressPrcnt, "Running Peptide Extractor ...");
            for (int i = 0; i < marrDatasetnames.Count; i++)
            {
                pfec.Arguments = marrDatasetnames[i].ToString() + @" /I:""" + mstrAnalysisFolder + @""" /K";
                var proc = System.Diagnostics.Process.Start(pfec);
                proc.WaitForExit();

                progressPrcnt += step;
                if (File.Exists(Path.Combine(mstrAnalysisFolder, marrDatasetnames[i] + "_syn.txt")))
                    backgroundWorker1.ReportProgress(progressPrcnt, marrDatasetnames[i] + " ... done.");
                else
                    backgroundWorker1.ReportProgress(progressPrcnt, marrDatasetnames[i] + " ... failed.");
            }
        }

        private void backgroundWorker1_WorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressPrcnt = e.ProgressPercentage;
            string mstrMssg = (string)e.UserState;

            mctlPerformPage.EnableNextButton(false);
            mctlPerformPage.ProgressVal = progressPrcnt;
            if (!mstrMssg.Equals(""))
                mctlPerformPage.ShowMessege = mstrMssg;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mctlPerformPage.EnableNextButton(true);
        }

        void mctlCompletedPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            mctlPerformPage.EnableNextButton(true);
            mctlPerformPage.ProgressVal = progressPrcnt;
            e.NewPage = mctlPerformPage.Name;
        }

        void mctlPerformPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            progressPrcnt = 0;
            //e.NewPage = mctlParaPage.Name;
            mblAnalysisStarted = true;

            if (datasource == enmMSMSreadType.SYNOUT)
                e.NewPage = mctlParaPage.Name;
            else if (datasource == enmMSMSreadType.LABKEY)
                e.NewPage = mctlLabKeyParaPage.Name;
            else
                e.NewPage = mctlWelcomePage.Name;
        }

        void mctlPerformPage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            e.NewPage = mctlCompletedPage.Name;
        }

        void mctlCompletedPage_WizardFinish(object sender, CancelEventArgs e)
        {

        }

        void mctlparaPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            if (datasource == enmMSMSreadType.SYNOUT)
                e.NewPage = mctlSelectHDDPage.Name;
            else if (datasource == enmMSMSreadType.LABKEY)
                e.NewPage = mctlSelectFromLabKeyPage.Name;
            else
                e.NewPage = mctlWelcomePage.Name;
        }

        void mctlparaPage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            e.NewPage = mctlPerformPage.Name;
            mstrAnalysisFolder = mctlParaPage.AnalysisFolder;
            XcRank = mctlParaPage.MaxRank;
            XCorrTh = mctlParaPage.XCorrThresholds;
            DelCn2Th = mctlParaPage.DelCn2Threshold;
            TrypState = mctlParaPage.TrypticState;
            if (!mblAnalysisStarted)
                backgroundWorker1.RunWorkerAsync();
        }

        void mctlLabKeyParaPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            if (datasource == enmMSMSreadType.SYNOUT)
                e.NewPage = mctlSelectHDDPage.Name;
            else if (datasource == enmMSMSreadType.LABKEY)
                e.NewPage = mctlSelectFromLabKeyPage.Name;
            else
                e.NewPage = mctlWelcomePage.Name;
        }

        void mctlLabKeyParaPage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            e.NewPage = mctlPerformPage.Name;
            mstrPepProph = mctlLabKeyParaPage.PepProphMin;
            if (!mblAnalysisStarted)
                backgroundWorker1.RunWorkerAsync();
        }

        void mctlSelectHDDPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            e.NewPage = mctlWelcomePage.Name;
        }

        void mctlSelectHDDPage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            marrFileNames = mctlSelectHDDPage.DatasetNames;
            if (Settings.Default.msmsAnalysisFolder != "")
                mctlParaPage.AnalysisFolder = Settings.Default.msmsAnalysisFolder;
            else
                mctlParaPage.AnalysisFolder = Settings.Default.msmsFolder;

            if (marrFileNames.Count < 2)
            {
                MessageBox.Show("Select at leaset two files.", "File selection", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.NewPage = mctlSelectHDDPage.Name;
            }
            else
            {
                e.NewPage = mctlParaPage.Name;
                mctlParaPage.UseSeqOUTFiles = false;
            }
        }

        void mctlSelectFromLabKeyPage_WizardBack(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            e.NewPage = mctlWelcomePage.Name;
        }

        void mctlSelectFromLabKeyPage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            marrLabKeyPathNames = mctlSelectFromLabKeyPage.DatasetNames;
            mstrProjFolder = mctlSelectFromLabKeyPage.ProjectFolder;

            if (marrLabKeyPathNames.Count < 2)
            {
                MessageBox.Show("Select at least two datasets", "File selection ?", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.NewPage = mctlSelectFromLabKeyPage.Name;
            }
            else if (datasource == enmMSMSreadType.LABKEY)
                e.NewPage = mctlLabKeyParaPage.Name;
            else
                e.NewPage = mctlParaPage.Name;
        }

        void mctlWelcomePage_WizardNext(object sender, Wizard.UI.WizardPageEventArgs e)
        {
            switch (mctlWelcomePage.MSMSreadtype)
            {
                case enmMSMSreadType.SYNOUT:
                    e.NewPage = mctlSelectHDDPage.Name;
                    datasource = enmMSMSreadType.SYNOUT;
                    break;
                case enmMSMSreadType.SEQOUT:
                    e.NewPage = mctlParaPage.Name;
                    datasource = enmMSMSreadType.SEQOUT;
                    mstrSeqOutFolder = mctlWelcomePage.SeqOutFolder;
                    mctlParaPage.UseSeqOUTFiles = true;
                    mctlParaPage.AnalysisFolder = mstrSeqOutFolder;
                    break;
                case enmMSMSreadType.LABKEY:
                    e.NewPage = mctlSelectFromLabKeyPage.Name;
                    datasource = enmMSMSreadType.LABKEY;
                    break;
                default:
                    break;
            }
        }

        private void GetSelectedFilePaths(ArrayList marrPaths)
        {
            string mstrfilePath = "";
            for (int i = 0; i < marrPaths.Count; i++)
            {
                mstrfilePath = marrPaths[i].ToString();
                if (mblUseLabKeyOutFiles)
                    mstrfilePath += GetFileNameFromPath(mstrfilePath, "_out.zip");
                else
                    mstrfilePath += GetFileNameFromPath(mstrfilePath, "_syn.txt");
                marrFileNames.Add(mstrfilePath);
            }
        }

        private string GetFileNameFromPath(string sourcePath, string extn)
        {
            string FileName = "";

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sourcePath);
            foreach (System.IO.FileInfo f in dir.GetFiles())
            {
                if (f.Name.EndsWith(extn))
                {
                    FileName = f.Name;
                    break;
                }
            }
            return FileName;
        }

        public DataTable SpectralDT
        {
            get
            {
                return mDTEset;
            }
        }

    }
}

