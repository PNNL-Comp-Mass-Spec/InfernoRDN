using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DAnTE.Paradiso;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public partial class frmDAnTEmdi : Form
    {
        // Note that this key gets deleted when the program is Uninstalled
        // See Inno Setup file inferno_setup.iss
        private const string REGVALUE_BIOCONDUCTOR_VERSION_CHECK = "BioconductorCheckLatestInfernoVersion";

        // No longer used (only used on Windows XP and 2000)
        // private readonly frmRmsg mRmsgForm;

        public string mSessionFile = null;

        private readonly StreamWriter mCustomLogWriter;
        private readonly bool mCustomLoggerEnabled;
        private static bool mCustomLogSeparatorAdded;

        // This is a linux-style path that is used by R to save .png files
        // For example: C:/Users/username/AppData/Roaming/Inferno/_temp.png
        private readonly string mRTempFilePath;

        private string mRepository;  // = @"http://lib.stat.cmu.edu/R/CRAN";
        private string mRpackList;

        private bool mInstallRpacks;
        private bool mUpdateRpacks;

        private readonly clsRconnect mRConnector;

        public frmDAnTEmdi(string dntfile, string customLogFilePath)
        {
            mSessionFile = dntfile;

            InitializeComponent();

            if (!string.IsNullOrEmpty(customLogFilePath))
            {
                mCustomLoggerEnabled = true;

                if (!File.Exists(customLogFilePath))
                    mCustomLogWriter = new StreamWriter(customLogFilePath);
                else
                    mCustomLogWriter = File.AppendText(customLogFilePath);
            }

            this.Text = "InfernoRDN"; //Application.ProductVersion.ToString();

            var startupErrString = new StringBuilder();

            SplashScreen.ShowSplashScreen();
            Application.DoEvents();

            Log(mCustomLoggerEnabled, string.Format("Starting Inferno [{0}]...", DateTime.Now), mCustomLogWriter);
            SplashScreen.SetStatus(string.Format("Starting Inferno [{0}]...", DateTime.Now));
            System.Threading.Thread.Sleep(100);
            SplashScreen.SetStatus("Reading Configuration Parameters...");

            if (!ConfigParameters())
            {
                Log(mCustomLoggerEnabled, "Error: Error in reading inferno.conf file.", mCustomLogWriter);
                startupErrString.Append("* Error in reading inferno.conf file.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in reading inferno.conf file.",
                //    "inferno.conf error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            Log(mCustomLoggerEnabled, "Done reading configuration parameters.", mCustomLogWriter);

            SplashScreen.SetStatus("Initializing Folders...");

            // Initialize folders            
            var tempFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var appDataFldrPath = Path.Combine(tempFolderPath, "Inferno");
            if (!Directory.Exists(appDataFldrPath))
            {
                Directory.CreateDirectory(appDataFldrPath);
            }

            mRTempFilePath = appDataFldrPath.Replace(@"\", "/") + "/_temp.png";
            this.mhelpProviderDAnTE.HelpNamespace = Path.Combine(Application.StartupPath, "InfernoHelp.chm");

            System.Threading.Thread.Sleep(10);
            Log(mCustomLoggerEnabled, "Done setting folders.", mCustomLogWriter);

            SplashScreen.SetStatus("Establishing Connection to R...");
            mRConnector = new clsRconnect();
            if (!mRConnector.initR())
            {
                startupErrString.Append(string.Format("* R failed to initialize: {0}", mRConnector.Message)).AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Try again. R failed to initialize for some unknown reason.",
                //    "R connection failed.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Log(mCustomLoggerEnabled, "Error: Connection to R failed.", mCustomLogWriter);
                //this.Close();
            }
            System.Threading.Thread.Sleep(10);
            Log(mCustomLoggerEnabled, "Done Connecting to R.", mCustomLogWriter);

            SplashScreen.SetStatus("Initializing R Functions...");
            if (!LoadRfunctions("Inferno.RData"))
            {
                startupErrString.Append("* Error in sourcing R functions.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mCustomLoggerEnabled, "Error: Sourcing R functions failed.", mCustomLogWriter);
                //this.Close();
            }
            Log(mCustomLoggerEnabled, "Done sourcing R functions.", mCustomLogWriter);

            SplashScreen.SetStatus("Initializing R Plotting Functions...");

            var usePlotgg = Settings.Default.useGG;
            string mstrPlotFuncFileName;

            if (usePlotgg)
                mstrPlotFuncFileName = "Inferno_ggplots.RData";
            else
                mstrPlotFuncFileName = "Inferno_stdplots.RData";

            if (!LoadRfunctions(mstrPlotFuncFileName))
            {
                startupErrString.Append("* Error in sourcing R plotting functions.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R plotting functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mCustomLoggerEnabled, "Error: Sourcing R plotting functions failed.", mCustomLogWriter);
                //this.Close();
            }
            Log(mCustomLoggerEnabled, "Done sourcing R plotting functions.", mCustomLogWriter);

            //if (!InitLoadRpackages()) {
            //  startupErrString.Append("* Error loading key R packages.").AppendLine();
            //  //SplashScreen.CloseForm();
            //  //MessageBox.Show("Error loading key R packages", "Error loading key R packs",
            //  //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            //  Log(mblLog, "Error: Loading key R packages failed.", logwriter);
            //  //this.Close();
            //}
            //Log(mblLog, "Done loading key R packages.", logwriter);
            ////InitLoadRpackages();
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus("Checking R version...");
            if (!CheckRVersion("2", "8.0"))
            {
                startupErrString.Append("* R version is not compatible. Install a more recent version.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("R version is not compatible." + Environment.NewLine +
                //    "Please install R version 2.9.x." + Environment.NewLine +
                //    "R can be downloaded from http://cran.r-project.org/",
                //    "R version incompatible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mCustomLoggerEnabled, "Error: R version incompatible.", mCustomLogWriter);
                //this.Close();
            }
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus(SplashScreen.VALIDATING_R_PACKAGES);

            if (!InstallRequiredRPackages())
            {
                startupErrString.Append("* R failed to install required packages.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Try again. R failed to install required packages." + Environment.NewLine +
                //    "If this is the first time you run Inferno after installing, check permissions to modify R install folder, else " +
                //    "try changing the repository in the inferno.conf file.",
                //    "R problem...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Log(mCustomLoggerEnabled, "Error: Loading required R packages failed.", mCustomLogWriter);
                //this.Close();
            }
            Log(mCustomLoggerEnabled, "Done loading required R packages.", mCustomLogWriter);
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus("Cleaning up temp files; checking for " + mRTempFilePath.Replace("/", @"\"));
            if (!DeleteTempFile())
            {
                // This is not a fatal error
                // Log it, but move on
                startupErrString.Append("* Error cleaning temp files.").AppendLine();
                Log(mCustomLoggerEnabled, "Error: Cleaning temp files failed.", mCustomLogWriter);
            }
            System.Threading.Thread.Sleep(10);


            // No longer used (only used on Windows XP and 2000)
            //
            //SplashScreen.SetStatus("Setting Up Child Forms...");
            //if (RunRlogs())
            //{
            //    mRmsgForm = new frmRmsg();
            //    try
            //    {
            //        //-// rConnector.SetCharacterOutputDevice((StatConnectorCommonLib.IStatConnectorCharacterDevice)mfrmRmsg.axStatConnectorCharacterDevice1.GetOcx());
            //        mRConnector.EvaluateNoReturn("print(version)");
            //        Log(mCustomLoggerEnabled, "Done starting R log viewer.", mCustomLogWriter);
            //    }
            //    catch (Exception ex)
            //    {
            //        startupErrString.Append("* Error starting R log viewer.").AppendLine();
            //        //MessageBox.Show("Error: " + ex.Message, "Watchout!");
            //        Log(mCustomLoggerEnabled, "Error: Error starting R log viewer." + ex.Message, mCustomLogWriter);
            //    }
            //}
            //else
            //{
            //    Log(mCustomLoggerEnabled, "R log viewer not used for this OS.", mCustomLogWriter);
            //}

            this.Activate();
            SplashScreen.CloseForm();

            Log(mCustomLoggerEnabled, "Inferno started.", mCustomLogWriter);
            if (startupErrString.Length > 1)
            {
                var errorMessage = startupErrString.ToString();
                if (!string.IsNullOrEmpty(clsRCmdLog.CurrentLogFilePath))
                {
                    errorMessage += "\nFor more information, see file " + clsRCmdLog.CurrentLogFilePath;
                }

                MessageBox.Show(errorMessage, "Errors Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #region Private methods

        private static void Log(bool useCustomLog, string logMessage, TextWriter w)
        {
            clsRCmdLog.LogOperation(logMessage);

            if (useCustomLog && w != null)
            {
                if (!mCustomLogSeparatorAdded)
                {
                    w.WriteLine();
                    w.WriteLine("---------------------------------------------------------------------");
                    mCustomLogSeparatorAdded = true;
                }

                w.WriteLine();
                w.WriteLine("{0}, {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine(logMessage);

                w.Flush();
            }
            else
            {
                Console.WriteLine(logMessage);
            }
        }


        private bool ConfigParameters()
        {
            var confPath = Path.Combine(Application.StartupPath, "inferno.conf");
            Engine engine = new Engine(confPath);

            engine.AddParameter(new Parameter("Repository", ParameterType.REQUIRED));
            engine.AddParameter(new Parameter("Rpackages", ParameterType.REQUIRED));
            //engine.AddParameter(new Parameter("Rfolder", ParameterType.OPTIONAL, "NONE"));
            engine.AddParameter(new Parameter("InstallRpacks", ParameterType.REQUIRED));
            engine.AddParameter(new Parameter("UpdateRpacks", ParameterType.REQUIRED));
            EngineStatus es = engine.ReadFile();
            if (es == EngineStatus.FAIL)
            {
                Console.WriteLine("Configuration Error: " + engine.Details);
                //Bail out as you see fit.
                return false;
            }
            //Else the configuration parameters have been read.
            mRepository = engine["Repository"].Val;
            mRpackList = engine["Rpackages"].Val;
            //RfilesPath = engine["Rfolder"].Val;

            if (engine["InstallRpacks"].Val.Equals("true"))
                mInstallRpacks = true;
            else
                mInstallRpacks = false;

            if (engine["UpdateRpacks"].Val.Equals("true"))
                mUpdateRpacks = true;
            else
                mUpdateRpacks = false;

            return true;
        }

        /// <summary>
        /// Load the RData file from root folder
        /// </summary>
        /// <returns></returns>
        private bool LoadRfunctions(string rdatafile)
        {
            bool success = false;

            string mstrRDataFile = Application.StartupPath.Replace("\\", "/") + "/" + rdatafile;
            try
            {
                success = mRConnector.loadRData(mstrRDataFile);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        private bool CheckRVersion(string mstrMajor, string mstrMinor)
        {
            string rcmd;
            bool mblresult = true;

            try
            {
                rcmd = @"verOK <- RVersionOK(major=" + mstrMajor + ",minor=" + mstrMinor + ")";
                mRConnector.EvaluateNoReturn(rcmd);
                ////object rout = rConnector.GetSymbol("verOK");
                ////mblresult = (bool)rout;
                mblresult = mRConnector.GetSymbolAsBool("verOK");
            }
            catch (Exception ex)
            {
                mblresult = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return mblresult;
        }

        private static double GetVersionDifferenceHours(IList<string> oldVersion, IList<string> newVersion)
        {

            int daysOld;
            int daysNew;

            if (int.TryParse(oldVersion[2], out daysOld) &&
                int.TryParse(newVersion[2], out daysNew))
            {
                int secondsOld;
                int secondsNew;
                if (int.TryParse(oldVersion[3], out secondsOld) &&
                    int.TryParse(newVersion[3], out secondsNew))
                {
                    var versionDifferenceHours = (daysNew + secondsNew / 86400.0) * 24 - (daysOld + secondsOld / 86400.0) * 24;
                    return versionDifferenceHours;

                }
            }

            // Error converting; say 24 hours part (to assure an update occurs)
            return 24;
        }

        private bool InstallRequiredRPackages()
        {
            var currentTask = "initializing";

            try
            {
                if (mInstallRpacks)
                {
                    currentTask = "installing default packages from " + mRepository;

                    var rcommand = @"installPackages(c(" + mRpackList + @"), repository=""" + mRepository + @""")";
                    mRConnector.EvaluateNoReturn(rcommand);

                    // Also confirm that we have the Bioconductor qvalue package
                    // Check the registry for the most recent version of this program that has installed bioconductor and qvalue
                    var appVersionBioconductorCheck = RegistryAccess.GetStringRegistryValue(REGVALUE_BIOCONDUCTOR_VERSION_CHECK, "");
                    var appVersionCurrent = Tools.clsRCmdLog.GetProgramVersion();

                    var updateRequired = !string.Equals(appVersionBioconductorCheck, appVersionCurrent);
                    if (updateRequired && !string.IsNullOrWhiteSpace(appVersionBioconductorCheck))
                    {
                        // Only update if the versions differ by at least 8 hours
                        var currentVersionParts = appVersionCurrent.Split('.').ToList();
                        var lastUpdateParts = appVersionBioconductorCheck.Split('.').ToList();

                        if (currentVersionParts.Count >= 4 && lastUpdateParts.Count >= 4)
                        {
                            var versionDifferenceHours = GetVersionDifferenceHours(lastUpdateParts, currentVersionParts);

                            if (versionDifferenceHours < 8)
                            {
                                updateRequired = false;
                                mUpdateRpacks = false;
                            }
                        }

                    }

                    if (updateRequired)
                    {

                        currentTask = "installing Bioconductor from http://bioconductor.org/biocLite.R";
                        rcommand = @"source(""http://bioconductor.org/biocLite.R"")";
                        mRConnector.EvaluateNoReturn(rcommand);

                        currentTask = "installing qvalue from Bioconductor";
                        rcommand = @"biocLite(""qvalue"", suppressUpdates=TRUE)";
                        mRConnector.EvaluateNoReturn(rcommand);

                        RegistryAccess.SetStringRegistryValue(REGVALUE_BIOCONDUCTOR_VERSION_CHECK, appVersionCurrent);
                    }
                }

                if (mUpdateRpacks)
                {
                    currentTask = "updating packages using " + mRepository;
                    var rcommand = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + mRepository + @""")";
                    mRConnector.EvaluateNoReturn(rcommand);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log(mCustomLoggerEnabled, "Exception in InstallRequiredRPackages while " + currentTask + ": " + ex.Message, mCustomLogWriter);
                return false;
            }

        }
      
        private bool UpdateRPackages()
        {
            string rcommand;
            bool mblresult = true;

            try
            {
                rcommand = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + mRepository + @""")";
                mRConnector.EvaluateNoReturn(rcommand);

                mblresult = true;
            }
            catch (Exception ex)
            {
                mblresult = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return mblresult;
        }

        //private bool InitLoadRpackages()
        //{
        //  string rcommand;
        //  bool mblresult = true;

        //  try {
        //    /* //-//
        //            rcommand = @"require(rcom)";
        //            rConnector.EvaluateNoReturn(rcommand);
        //            rcommand = @"require(rscproxy)";
        //            rConnector.EvaluateNoReturn(rcommand);
        //    */
        //    mblresult = true;
        //  }
        //  catch (Exception ex) {
        //    mblresult = false;
        //    Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
        //  }
        //  return mblresult;
        //}

        private bool DeleteTempFile()
        {
            var tempFilePath = mRTempFilePath.Replace("/", @"\");

            if (File.Exists(tempFilePath))
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch
                {
                    // Ignore errors here, but return false
                    return false;
                }
            }

            return true;
        }

        private void StartMain(string dntfile)
        {
            try
            {
                foreach (Form f in MdiChildren)
                {
                    frmDAnTE mf = f as frmDAnTE;
                    if (mf != null)
                    {
                        if (mf.WindowState == FormWindowState.Minimized)
                            mf.WindowState = FormWindowState.Normal;
                        mf.BringToFront();
                        if (dntfile != null)
                            mf.OpenSessionThreaded(dntfile);
                        return;
                    }
                }
                frmDAnTE mfrmDAnTE = frmDAnTE.GetChildInstance();
                mfrmDAnTE.RTempFilePath = mRTempFilePath;
                mfrmDAnTE.RConnector = mRConnector;
                mfrmDAnTE.SessionFile = dntfile;
                mfrmDAnTE.MdiParent = this;
                mfrmDAnTE.ParentInstance = this;

                mfrmDAnTE.Show();
                mfrmDAnTE.BringToFront();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Check the platform for 32/64bit
        private enum Platform
        {
            X86,
            X64,
            Unknown
        }

        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        private Platform GetPlatform()
        {
            SYSTEM_INFO sysInfo = new SYSTEM_INFO();
            GetNativeSystemInfo(ref sysInfo);

            switch (sysInfo.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return Platform.X64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return Platform.X86;

                default:
                    return Platform.Unknown;
            }
        }

        // No longer used (only used on Windows XP and 2000)
        /*
        private bool RunRlogs()
        {
            bool mblRunOK = false;

            //Platform plt = GetPlatform();
            //if (plt == Platform.X86)
            //    mblRunOK = true;

            // Get OperatingSystem information from the system namespace.
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            // Determine the platform.
            switch (osInfo.Platform)
            {
                // Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000,
                // or Windows XP.
                case System.PlatformID.Win32NT:

                    switch (osInfo.Version.Major)
                    {
                        case 5:
                            if ((osInfo.Version.Minor == 0) || (osInfo.Version.Minor == 1))
                                mblRunOK = true; // 2000 or XP
                            else
                                mblRunOK = false; // 64bit, Server2003
                            break;
                        case 6:
                            mblRunOK = false; //Vista, Server2008, Windows7
                            break;
                    } break;
            }
            return mblRunOK;
            //return true;
        }
        */

        #endregion

        #endregion

        private void mnuItemNew_Click(object sender, EventArgs e)
        {
            StartMain(mSessionFile);
        }

        private void mnuItemExit_Click(object sender, EventArgs e)
        {
            frmDAnTE mfrmDAnTE = frmDAnTE.GetChildInstance();

            if (mfrmDAnTE.OK2Exit())
            {
                this.DialogResult = DialogResult.OK;
                mRConnector.closeR();
                this.Close();
            }
        }

        private void mnuItemResource_Click(object sender, EventArgs e)
        {
            if (!LoadRfunctions("Inferno.RData"))
                MessageBox.Show("Error ocurred while Re-sourcing. Changes may not be effective", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //    statusBarPanelMsg.Text = "R functions re-sourced.";
        }

        private void mnuItemggplots_Click(object sender, EventArgs e)
        {
            if (!LoadRfunctions("Inferno_ggplots.RData"))
                MessageBox.Show("Error ocurred while Re-sourcing. Changes may not be effective", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Settings.Default.useGG = true;
        }

        private void mnuItemStdPlots_Click(object sender, EventArgs e)
        {
            if (!LoadRfunctions("Inferno_stdplots.RData"))
                MessageBox.Show("Error ocurred while Re-sourcing. Changes may not be effective", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Settings.Default.useGG = false;
        }

        private void mnuItemHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, this.mhelpProviderDAnTE.HelpNamespace);
        }

        private void mnuItemBugs_Click(object sender, EventArgs e)
        {
            // frmTracWebBugReport mfrmTracWeb = new frmTracWebBugReport();
            //mfrmTracWeb.Show();

            frmBugReportEmail mfrmBugEmail = new frmBugReportEmail();
            mfrmBugEmail.Show();
        }

        private void mnuItemAbout_Click(object sender, EventArgs e)
        {

            frmAbout2 mfrmAbout = new frmAbout2();
            mfrmAbout.ShowDialog();
        }

        private void frmDAnTEmdi_Load(object sender, EventArgs e)
        {
            StartMain(mSessionFile);
        }

        private void mnuWindowItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            if (item != null)
            {
                string enumVal = item.Tag as string;
                if (enumVal != null)
                {
                    LayoutMdi((MdiLayout)Enum.Parse(typeof(MdiLayout), enumVal));
                }
            }
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
                if (!fExt.Equals(".dnt"))
                {
                    MessageBox.Show("Wrong file type!", "Use only .dnt files...",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    StartMain(s[0]);
                }
            }
        }

        public ToolStrip MainDanteToolBar
        {
            get { return mtoolStripMDI; }
            set { mtoolStripMDI = value; }
        }

        private void mnuItemUpgradeRPacks_Click(object sender, EventArgs e)
        {
            if (!UpdateRPackages())
            {
                MessageBox.Show("Error updating R packages", "Error loading R packs",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            MessageBox.Show("R packages are now up-to-date", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}