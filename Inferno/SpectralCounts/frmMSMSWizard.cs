using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;
using ICSharpCode.SharpZipLib.Zip;

namespace DAnTE.Inferno
{
    public enum enmMSMSreadType
    {
        SEQOUT,
        SYNOUT,
        LABKEY
    };

    public partial class frmMSMSWizard : Wizard.UI.WizardSheet
    {
        private ctlMSMSWelcomeWizPage mctlWelcomePage;
        private ctlMSMSSelectFilesFromDBFilterWizPage mctlSelectFromLabKeyPage;
        private ctlMSMSSelectFromHDDWizPage mctlSelectHDDPage;
        private ctlMSMSparaWizPage mctlParaPage;
        private ctlMSMSLabKeyParaWizPage mctlLabKeyParaPage;
        private ctlMSMSPerformWizPage mctlPerformPage;
        private ctlMSMSCompleteWizPage mctlCompletedPage;
        private List<string> marrFilePaths;
        private List<string> marrLabKeyPathNames;
        private readonly List<string> successfulDataSets = new List<string>();
        private string mstrProjFolder;
        private string mstrAnalysisFolder;
        private string mstrSeqOutFolder;
        private int XcRank = 1;
        private string XCorrTh = "";
        private string DelCn2Th = "";
        private string TrypState = "";
        private bool mblAnalysisStarted;
        private int progressPrcnt;
        private readonly clsRconnect rConnector;
        private DataTable mDTEset = new DataTable();
        private enmMSMSreadType datasource;
        private bool mRunPeptideFileExtractor;
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
            this.backgroundWorker1.DoWork += this.backgroundWorker1_DoWork;
            this.backgroundWorker1.RunWorkerCompleted += this.backgroundWorker1_RunWorkerCompleted;
            this.backgroundWorker1.ProgressChanged += this.backgroundWorker1_WorkProgressChanged;
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
            this.backgroundWorker1.DoWork += this.backgroundWorker1_DoWork;
            this.backgroundWorker1.RunWorkerCompleted += this.backgroundWorker1_RunWorkerCompleted;
            this.backgroundWorker1.ProgressChanged += this.backgroundWorker1_WorkProgressChanged;
        }

        private void Init()
        {
            marrFilePaths = new List<string>();

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


            mctlWelcomePage.WizardNext += mctlWelcomePage_WizardNext;
            mctlSelectFromLabKeyPage.WizardNext += mctlSelectFromLabKeyPage_WizardNext;
            mctlSelectFromLabKeyPage.WizardBack += mctlSelectFromLabKeyPage_WizardBack;
            mctlSelectHDDPage.WizardNext += mctlSelectHDDPage_WizardNext;
            mctlSelectHDDPage.WizardBack += mctlSelectHDDPage_WizardBack;
            mctlParaPage.WizardNext += mctlparaPage_WizardNext;
            mctlParaPage.WizardBack += mctlparaPage_WizardBack;
            mctlLabKeyParaPage.WizardNext += mctlLabKeyParaPage_WizardNext;
            mctlLabKeyParaPage.WizardBack += mctlLabKeyParaPage_WizardBack;
            mctlPerformPage.WizardNext += mctlPerformPage_WizardNext;
            mctlPerformPage.WizardBack += mctlPerformPage_WizardBack;
            mctlCompletedPage.WizardFinish += mctlCompletedPage_WizardFinish;
            mctlCompletedPage.WizardBack += mctlCompletedPage_WizardBack;

            this.Pages.Add(mctlWelcomePage);
            this.Pages.Add(mctlSelectFromLabKeyPage);
            this.Pages.Add(mctlSelectHDDPage);
            this.Pages.Add(mctlParaPage);
            this.Pages.Add(mctlLabKeyParaPage);
            this.Pages.Add(mctlPerformPage);
            this.Pages.Add(mctlCompletedPage);
        }

        private List<string> ConvertSYNOUT2DatasetNames(IEnumerable<string> filePaths)
        {
            var datasetNames = new List<string>();

            foreach (var filePath in filePaths)
            {
                var datasetName = Path.GetFileName(filePath);
                if (string.IsNullOrWhiteSpace(datasetName))
                    continue;

                int extidx;
                if ((extidx = datasetName.IndexOf("_out.txt", StringComparison.OrdinalIgnoreCase)) == -1)
                    if ((extidx = datasetName.IndexOf("_out.zip", StringComparison.OrdinalIgnoreCase)) == -1)
                        extidx = datasetName.IndexOf("_syn.txt", StringComparison.OrdinalIgnoreCase);

                if (extidx != -1)
                    datasetName = datasetName.Substring(0, extidx);

                datasetNames.Add(datasetName);
            }

            return datasetNames;
        }

        private List<string> GetSEQOUT2DatasetNames(string mstrSeqFolder)
        {
            var datasetNames = new SortedSet<string>();

            var dir = new DirectoryInfo(mstrSeqFolder);

            foreach (var f in dir.GetFiles())
            {
                if (!Path.GetExtension(f.Name).Equals(".out", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var datasetNameToAdd = f.Name.Substring(0, f.Name.IndexOf('.'));
                if (!datasetNames.Contains(datasetNameToAdd))
                {
                    datasetNames.Add(datasetNameToAdd);
                }
            }
            return datasetNames.ToList();
        }

        /// <summary>
        /// This is where main action takes place.
        /// Will be called when the 'Next' button is pressed on the 'Select Parameters' page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> datasetNames;
            progressPrcnt = 0;

            switch (datasource)
            {
                case enmMSMSreadType.SYNOUT:
                    // *_syn.txt or *_out.txt files from the local drive
                    backgroundWorker1.ReportProgress(progressPrcnt, "Copying files ...");
                    // file copy
                    mRunPeptideFileExtractor = CopyMSMSFiles(); // *_syn.txt or *_out.txt files from local folder
                    datasetNames = ConvertSYNOUT2DatasetNames(marrFilePaths);
                    if (mRunPeptideFileExtractor) // if *_out.txt files are found (but not the corresponding *_syn.txt)
                        RunPeptideFileExtractor(datasetNames);
                    CreatSpectralCountTablesSEQSYN(datasetNames, e);
                    break;
                case enmMSMSreadType.LABKEY:
                    // Select from the LabKey server
                    backgroundWorker1.ReportProgress(50, "");
                    CreatSpectralCountTablesLabKey(marrLabKeyPathNames, e);
                    break;
                case enmMSMSreadType.SEQOUT:
                    //Process Sequest *.Out files from a local folder
                    datasetNames = GetSEQOUT2DatasetNames(mstrAnalysisFolder);
                    progressPrcnt = 10;
                    backgroundWorker1.ReportProgress(progressPrcnt, "Selecting datasets ...");
                    RunPeptideFileExtractor(datasetNames);
                    CreatSpectralCountTablesSEQSYN(datasetNames, e);
                    break;
                default:
                    break;
            }
            //backgroundWorker1.ReportProgress(60, "");
        }

        private void CreatSpectralCountTablesSEQSYN(ICollection<string> datasetNames, DoWorkEventArgs e)
        {
            var dir = new DirectoryInfo(mstrAnalysisFolder);
            foreach (var f in dir.GetFiles())
            {
                if (!f.Name.EndsWith("_syn.txt", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var dName = f.Name.Substring(0, f.Name.IndexOf("_syn.txt", StringComparison.OrdinalIgnoreCase));
                if (datasetNames.Contains(dName))
                    successfulDataSets.Add(dName);
            }

            if (successfulDataSets.Count > 1)
            {
                var fileList = @"c(""" + successfulDataSets[0];
                var rcmd = "X <- createMSMSdt.SpectralCount(";
                var dataFolder = mstrAnalysisFolder.Replace("\\", "/");
                for (var i = 1; i < successfulDataSets.Count; i++)
                {
                    fileList = fileList + @""",""" + successfulDataSets[i];
                }
                fileList += @""")";

                rcmd += fileList + @",""" + dataFolder + @""",XcRank=" + XcRank +
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

        private void CreatSpectralCountTablesLabKey(IList<string> labkeyPathNames, DoWorkEventArgs e)
        {
            if (labkeyPathNames.Count <= 1)
            {
                backgroundWorker1.ReportProgress(100, "You need at leaset two unique datasets... Giving up.");
                return;
            }

            var fileList = @"c(""" + labkeyPathNames[0];
            var rcmd = "X <- LabKeySpectralCounts(";

            for (var i = 1; i < labkeyPathNames.Count; i++)
            {
                fileList = fileList + @""",""" + labkeyPathNames[i];
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
                    {
                        e.Result = null;
                    }
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

        /// <summary>
        /// Copy _syn.txt or _out.txt files to the analysis folder
        /// Will also copy *_syn.txt files from LabKey (but not the _out.zip files. use
        /// CopyUnzipMSMSOutFiles() to copy _out.zip files
        /// </summary>
        /// <returns></returns>
        private bool CopyMSMSFiles()
        {
            var runPeptideFileExtractor = false;
            var step = marrFilePaths.Count != 0 ? 30 / marrFilePaths.Count : 30; // Progressbar upto 30%

            foreach (var sourcePath in marrFilePaths)
            {
                if (!(sourcePath.EndsWith("_syn.txt", StringComparison.OrdinalIgnoreCase)))
                    runPeptideFileExtractor = true;

                var sourceFName = Path.GetFileName(sourcePath);
                var destPath = Path.Combine(mstrAnalysisFolder, sourceFName);
                var sourceFinfo = new FileInfo(sourcePath);
                var destFinfo = new FileInfo(destPath);

                if (((File.Exists(destPath)) &&
                     DateTime.Compare(sourceFinfo.LastWriteTime, destFinfo.LastWriteTime) > 0) ||
                    (!File.Exists(destPath)))
                {
                    try
                    {
                        File.Copy(sourcePath, destPath, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                progressPrcnt += step;
                backgroundWorker1.ReportProgress(progressPrcnt, Path.GetFileName(sourcePath));
            }

            return runPeptideFileExtractor;
        }

        /// <summary>
        /// Copy _out.zip files from DMS. This will unzip and copy on the fly
        /// </summary>
        /// <returns></returns>
        private bool CopyUnzipMSMSOutFiles()
        {
            var step = marrFilePaths.Count != 0 ? 30 / marrFilePaths.Count : 30; // Progress bar up to 30%

            foreach (var sourcePath in marrFilePaths)
            {
                using (var s = new ZipInputStream(File.OpenRead(sourcePath)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        var destFName = Path.GetFileName(theEntry.Name);
                        if (string.IsNullOrWhiteSpace(destFName))
                            continue;

                        var destPath = Path.Combine(mstrAnalysisFolder, destFName);

                        if (File.Exists(destPath))
                        {
                            continue;
                        }

                        using (var streamWriter = File.Create(destPath))
                        {
                            var data = new byte[2048];
                            while (true)
                            {
                                var size = s.Read(data, 0, data.Length);
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
                progressPrcnt += step;
                backgroundWorker1.ReportProgress(progressPrcnt, Path.GetFileName(sourcePath));
            }
            return true;
        }

        /// <summary>
        /// Run 'Peptide File Extractor' application on *_out.txt files
        /// </summary>
        /// <param name="marrDatasetnames"></param>
        private void RunPeptideFileExtractor(ICollection<string> marrDatasetnames)
        {
            var step = marrDatasetnames.Count != 0 ? 30 / marrDatasetnames.Count : 30; // Progressbar up to 30%

            var pfec = new System.Diagnostics.ProcessStartInfo(
                Path.Combine(Application.StartupPath, "Tools", "PeptideFileExtractorConsole.exe"))
            {
                RedirectStandardOutput = false,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = true
            };

            backgroundWorker1.ReportProgress(progressPrcnt, "Running Peptide Extractor ...");
            foreach (var datasetName in marrDatasetnames)
            {
                pfec.Arguments = datasetName + @" /I:""" + mstrAnalysisFolder + @""" /K";
                var proc = System.Diagnostics.Process.Start(pfec);
                if (proc != null)
                {
                    proc.WaitForExit();
                }

                progressPrcnt += step;
                if (File.Exists(Path.Combine(mstrAnalysisFolder, datasetName + "_syn.txt")))
                    backgroundWorker1.ReportProgress(progressPrcnt, datasetName + " ... done.");
                else
                    backgroundWorker1.ReportProgress(progressPrcnt, datasetName + " ... failed.");
            }
        }

        private void backgroundWorker1_WorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressPrcnt = e.ProgressPercentage;
            var userState = (string)e.UserState;

            mctlPerformPage.EnableNextButton(false);
            mctlPerformPage.ProgressVal = progressPrcnt;

            if (!string.IsNullOrWhiteSpace(userState))
                mctlPerformPage.ShowMessege = userState;
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
            marrFilePaths = mctlSelectHDDPage.DatasetNames;
            if (Settings.Default.msmsAnalysisFolder != "")
                mctlParaPage.AnalysisFolder = Settings.Default.msmsAnalysisFolder;
            else
                mctlParaPage.AnalysisFolder = Settings.Default.msmsFolder;

            if (marrFilePaths.Count < 2)
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

        public DataTable SpectralDT => mDTEset;
    }
}