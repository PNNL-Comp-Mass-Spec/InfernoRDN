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
    // ReSharper disable once IdentifierTypo
    public partial class frmDAnTEmdi : Form
    {
        // Ignore Spelling: InnoSetup, Bioconductor, Rpacks, Rpackages, Rfolder, Rfiles, Bioc, repos, qvalue, verOK

        // Note that this registry key gets deleted when the program is uninstalled (though only for the user who installed the program)
        // See InnoSetup file inferno_setup.iss
        // Location in the registry: HKEY_CURRENT_USER\Software\PNNL\Inferno
        private const string REG_VALUE_BIOCONDUCTOR_VERSION_CHECK = "BioconductorCheckLatestInfernoVersion";

        public readonly string mSessionFile;

        private readonly StreamWriter mCustomLogWriter;
        private readonly bool mCustomLoggerEnabled;
        private static bool mCustomLogSeparatorAdded;

        // This is a Linux-style path that is used by R to save .png files
        // For example: C:/Users/username/AppData/Roaming/Inferno/_temp.png
        private readonly string mRTempFilePath;

        private string mRepository; // = @"http://lib.stat.cmu.edu/R/CRAN";
        private string mRPackageList;

        private bool mInstallRPackages;
        private bool mUpdateRPackages;

        private readonly clsRconnect mRConnector;

        // ReSharper disable once IdentifierTypo
        public frmDAnTEmdi(string danteFilePath, string customLogFilePath)
        {
            mSessionFile = danteFilePath;

            InitializeComponent();

            if (!string.IsNullOrEmpty(customLogFilePath))
            {
                mCustomLoggerEnabled = true;

                try
                {
                    if (File.Exists(customLogFilePath))
                    {
                        mCustomLogWriter = File.AppendText(customLogFilePath);
                    }
                    else
                    {
                        mCustomLogWriter = new StreamWriter(customLogFilePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error appending to the log file at {0}: {1}", customLogFilePath,
                                                  ex.Message));
                }
            }

            Text = "InfernoRDN";

            var startupErrString = new StringBuilder();

            SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            var appVersionCurrent = clsRCmdLog.GetProgramVersion();

            Log(mCustomLoggerEnabled, string.Format("Starting Inferno v{0} [{1}]...", appVersionCurrent, DateTime.Now), mCustomLogWriter);
            SplashScreen.SetStatus(string.Format("Starting Inferno v{0} [{1}]...", appVersionCurrent, DateTime.Now));
            System.Threading.Thread.Sleep(100);
            SplashScreen.SetStatus("Reading Configuration Parameters...");

            if (!ConfigParameters())
            {
                Log(mCustomLoggerEnabled, "Error: Error in reading inferno.conf file.", mCustomLogWriter);
                startupErrString.AppendLine("* Error in reading inferno.conf file.");
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in reading inferno.conf file.",
                //    "inferno.conf error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            Log(mCustomLoggerEnabled, "Done reading configuration parameters.", mCustomLogWriter);

            SplashScreen.SetStatus("Initializing Folders...");

            // Initialize folders
            var tempFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var appDataDirectoryPath = Path.Combine(tempFolderPath, "Inferno");
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            mRTempFilePath = appDataDirectoryPath.Replace(@"\", "/") + "/_temp.png";
            mhelpProviderDAnTE.HelpNamespace = Path.Combine(Application.StartupPath, "InfernoHelp.chm");

            System.Threading.Thread.Sleep(10);
            Log(mCustomLoggerEnabled, "Done setting folders.", mCustomLogWriter);

            SplashScreen.SetStatus("Establishing Connection to R...");
            mRConnector = new clsRconnect();
            var connectionSucceeded = mRConnector.initR();

            if (!connectionSucceeded)
            {
                startupErrString.AppendLine(string.Format("* R failed to initialize: {0}", mRConnector.Message));
                //SplashScreen.CloseForm();
                //MessageBox.Show("Try again. R failed to initialize for some unknown reason.",
                //    "R connection failed.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Log(mCustomLoggerEnabled, "Error: Connection to R failed.", mCustomLogWriter);
                //this.Close();
            }
            else
            {
                System.Threading.Thread.Sleep(10);
                Log(mCustomLoggerEnabled, "Done Connecting to R.", mCustomLogWriter);
            }

            SplashScreen.SetStatus("Initializing R Functions...");
            if (!LoadRFunctions("Inferno.RData"))
            {
                if (connectionSucceeded)
                {
                    startupErrString.AppendLine("* Error in sourcing R functions.");
                }
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mCustomLoggerEnabled, "Error: Sourcing R functions failed.", mCustomLogWriter);
                //this.Close();
            }
            else
            {
                Log(mCustomLoggerEnabled, "Done sourcing R functions.", mCustomLogWriter);
            }

            SplashScreen.SetStatus("Initializing R Plotting Functions...");

            var useGGPlot = Settings.Default.useGG;
            string plotFuncFileName;

            if (useGGPlot)
                plotFuncFileName = "Inferno_ggplots.RData";
            else
                plotFuncFileName = "Inferno_stdplots.RData";

            if (!LoadRFunctions(plotFuncFileName))
            {
                if (connectionSucceeded)
                {
                    startupErrString.AppendLine("* Error in sourcing R plotting functions.");
                }
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R plotting functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mCustomLoggerEnabled, "Error: Sourcing R plotting functions failed.", mCustomLogWriter);
                //this.Close();
            }
            else
            {
                Log(mCustomLoggerEnabled, "Done sourcing R plotting functions.", mCustomLogWriter);
            }

            //if (!InitLoadRPackages()) {
            //  startupErrString.AppendLine("* Error loading key R packages.");
            //  //SplashScreen.CloseForm();
            //  //MessageBox.Show("Error loading key R packages", "Error loading key R packs",
            //  //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            //  Log(mCustomLoggerEnabled, "Error: Loading key R packages failed.", logWriter);
            //  //this.Close();
            //}
            //Log(mCustomLoggerEnabled, "Done loading key R packages.", logWriter);
            ////InitLoadRPackages();
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus("Checking R version...");
            if (!CheckRVersion("3", "6.0"))
            {
                if (connectionSucceeded)
                {
                    startupErrString.AppendLine("* R version is not compatible. Install a more recent version.");
                }
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
                if (connectionSucceeded)
                {
                    startupErrString.AppendLine("* R failed to install required packages.");
                }

                //SplashScreen.CloseForm();
                //MessageBox.Show("Try again. R failed to install required packages." + Environment.NewLine +
                //    "If this is the first time you run Inferno after installing, check permissions to modify R install folder, else " +
                //    "try changing the repository in the inferno.conf file.",
                //    "R problem...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Log(mCustomLoggerEnabled, "Error: Loading required R packages failed.", mCustomLogWriter);
                //this.Close();
            }
            else
            {
                Log(mCustomLoggerEnabled, "Done loading required R packages.", mCustomLogWriter);
                System.Threading.Thread.Sleep(10);
            }

            SplashScreen.SetStatus("Cleaning up temp files; checking for " + mRTempFilePath.Replace("/", @"\"));
            if (!DeleteTempFile())
            {
                // This is not a fatal error
                // Log it, but move on
                startupErrString.AppendLine("* Error cleaning temp files.");
                Log(mCustomLoggerEnabled, "Error: Cleaning temp files failed.", mCustomLogWriter);
            }
            System.Threading.Thread.Sleep(10);

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

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
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
            var engine = new Engine(confPath);

            // ReSharper disable StringLiteralTypo
            // ReSharper disable CommentTypo

            engine.AddParameter(new Parameter("Repository", ParameterType.REQUIRED));
            engine.AddParameter(new Parameter("Rpackages", ParameterType.REQUIRED));
            //engine.AddParameter(new Parameter("Rfolder", ParameterType.OPTIONAL, "NONE"));
            engine.AddParameter(new Parameter("InstallRpacks", ParameterType.REQUIRED));
            engine.AddParameter(new Parameter("UpdateRpacks", ParameterType.REQUIRED));
            var es = engine.ReadFile();
            if (es == EngineStatus.FAIL)
            {
                Console.WriteLine("Configuration Error: " + engine.Details);
                //Bail out as you see fit.
                return false;
            }
            //Else the configuration parameters have been read.
            mRepository = engine["Repository"].Val;
            mRPackageList = engine["Rpackages"].Val;
            //RfilesPath = engine["Rfolder"].Val;

            if (engine["InstallRpacks"].Val.Equals("true"))
                mInstallRPackages = true;
            else
                mInstallRPackages = false;

            if (engine["UpdateRpacks"].Val.Equals("true"))
                mUpdateRPackages = true;
            else
                mUpdateRPackages = false;

            // ReSharper restore CommentTypo
            // ReSharper restore StringLiteralTypo

            return true;
        }

        /// <summary>
        /// Load the RData file from root folder
        /// </summary>
        /// <returns></returns>
        private bool LoadRFunctions(string rDataFile)
        {
            bool success;

            var rDataFilePath = Application.StartupPath.Replace("\\", "/") + "/" + rDataFile;
            try
            {
                success = mRConnector.loadRData(rDataFilePath);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        /// <summary>
        /// Confirm that the installed version of R is majorVersion.minorVersion or newer
        /// </summary>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        /// <returns></returns>
        private bool CheckRVersion(string majorVersion, string minorVersion)
        {
            bool success;

            try
            {
                var rCommand = @"verOK <- RVersionOK(major=" + majorVersion + ",minor=" + minorVersion + ")";
                mRConnector.EvaluateNoReturn(rCommand);
                ////object rout = rConnector.GetSymbol("verOK");
                ////success = (bool)rout;
                success = mRConnector.GetSymbolAsBool("verOK");
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return success;
        }

        private static double GetVersionDifferenceHours(IList<string> oldVersion, IList<string> newVersion)
        {
            if (int.TryParse(oldVersion[2], out var daysOld) &&
                int.TryParse(newVersion[2], out var daysNew))
            {
                if (int.TryParse(oldVersion[3], out var secondsOld) &&
                    int.TryParse(newVersion[3], out var secondsNew))
                {
                    var versionDifferenceHours = (daysNew + secondsNew / 86400.0) * 24 -
                                                 (daysOld + secondsOld / 86400.0) * 24;
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
                if (mInstallRPackages)
                {
                    // installing default packages from http://cran.fhcrc.org/
                    var installPackagesCommand = @"install.packages(c(" + mRPackageList + @"), repos=""" + mRepository + @""")";
                    currentTask = string.Format("installing default packages from {0}; command {1}", mRepository, installPackagesCommand);

                    mRConnector.EvaluateNoReturn(installPackagesCommand);

                    // Also confirm that we have additional packages from Bioconductor
                    // Check the registry for the most recent version of this program that has installed Bioconductor, qvalue, and impute
                    // Location in the registry: HKEY_CURRENT_USER\Software\PNNL\Inferno
                    var appVersionBioconductorCheck = RegistryAccess.GetStringRegistryValue(REG_VALUE_BIOCONDUCTOR_VERSION_CHECK, "");
                    var appVersionCurrent = clsRCmdLog.GetProgramVersion();

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
                                mUpdateRPackages = false;
                            }
                        }
                    }

                    if (updateRequired)
                    {
                        var installBiocManager = @"install.packages(""BiocManager"", repos='https://cran.revolutionanalytics.com/')";
                        currentTask = string.Format("installing BiocManager from https://cran.revolutionanalytics.com/; command {0}", installBiocManager);

                        mRConnector.EvaluateNoReturn(installBiocManager);

                        var installBioconductorPackages = @"BiocManager::install(c(""qvalue"", ""impute""), update = TRUE, ask = FALSE)";
                        currentTask = string.Format("installing qvalue and impute from Bioconductor; command {0}", installBioconductorPackages);

                        mRConnector.EvaluateNoReturn(installBioconductorPackages);

                        RegistryAccess.SetStringRegistryValue(REG_VALUE_BIOCONDUCTOR_VERSION_CHECK, appVersionCurrent);
                    }
                }

                if (mUpdateRPackages)
                {
                    // updating packages using http://cran.fhcrc.org/
                    var updatePackages = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + mRepository + @""")";
                    currentTask = string.Format("updating packages using {0}; command {1}", mRepository, updatePackages);

                    mRConnector.EvaluateNoReturn(updatePackages);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log(mCustomLoggerEnabled,
                    "Exception in InstallRequiredRPackages while " + currentTask + ": " + ex.Message, mCustomLogWriter);
                return false;
            }
        }

        private bool UpdateRPackages()
        {
            bool result;

            try
            {
                var rCommand = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + mRepository + @""")";
                mRConnector.EvaluateNoReturn(rCommand);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return result;
        }

        //private bool InitLoadRPackages()
        //{
        //  string rCommand;
        //  bool result = true;

        //  try {
        //    /* //-//
        //            rCommand = @"require(rcom)";
        //            rConnector.EvaluateNoReturn(rCommand);
        //            rCommand = @"require(rscproxy)";
        //            rConnector.EvaluateNoReturn(rCommand);
        //    */
        //    result = true;
        //  }
        //  catch (Exception ex) {
        //    result = false;
        //    Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
        //  }
        //  return result;
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

        private void StartMain(string sessionFilePath)
        {
            try
            {
                // Find a child window in which we can load the specified session file

                foreach (var mdiChild in MdiChildren)
                {
                    var danteForm = mdiChild as frmDAnTE;
                    if (danteForm == null)
                    {
                        continue;
                    }

                    if (danteForm.WindowState == FormWindowState.Minimized)
                        danteForm.WindowState = FormWindowState.Normal;

                    danteForm.BringToFront();
                    if (!string.IsNullOrWhiteSpace(sessionFilePath))
                        danteForm.OpenSessionCheckExisting(sessionFilePath, frmDAnTE.USE_THREADED_LOAD);
                    return;
                }

                var danteInstance = frmDAnTE.GetChildInstance();
                danteInstance.RTempFilePath = mRTempFilePath;
                danteInstance.RConnector = mRConnector;
                danteInstance.SessionFile = sessionFilePath;
                danteInstance.MdiParent = this;
                danteInstance.ParentInstance = this;

                danteInstance.Show();
                danteInstance.BringToFront();
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
            public readonly ushort wProcessorArchitecture;
            public readonly ushort wReserved;
            public readonly uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public readonly uint dwNumberOfProcessors;
            public readonly uint dwProcessorType;
            public readonly uint dwAllocationGranularity;
            public readonly ushort wProcessorLevel;
            public readonly ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [Obsolete("Unused")]
        private Platform GetPlatform()
        {
            var sysInfo = new SYSTEM_INFO();
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


        #endregion

        #endregion

        private void mnuItemNew_Click(object sender, EventArgs e)
        {
            StartMain(string.Empty);
        }

        private void mnuItemExit_Click(object sender, EventArgs e)
        {
            var danteInstance = frmDAnTE.GetChildInstance();

            if (danteInstance.OK2Exit())
            {
                DialogResult = DialogResult.OK;
                mRConnector.closeR();
                this.Close();
            }
        }

        private void mnuItemResource_Click(object sender, EventArgs e)
        {
            if (!LoadRFunctions("Inferno.RData"))
                MessageBox.Show("Error occurred while Re-sourcing. Changes may not be effective", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //    statusBarPanelMsg.Text = "R functions re-sourced.";
        }

        private void mnuItemGGPlots_Click(object sender, EventArgs e)
        {
            if (!LoadRFunctions("Inferno_ggplots.RData"))
                MessageBox.Show("Error occurred while Re-sourcing. Changes may not be effective", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Settings.Default.useGG = true;
        }

        private void mnuItemStdPlots_Click(object sender, EventArgs e)
        {
            if (!LoadRFunctions("Inferno_stdplots.RData"))
                MessageBox.Show("Error occurred while Re-sourcing. Changes may not be effective", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                Settings.Default.useGG = false;
        }

        private void mnuItemHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, mhelpProviderDAnTE.HelpNamespace);
        }

        private void mnuItemBugs_Click(object sender, EventArgs e)
        {
            var debugEmailForm = new frmBugReportEmail();
            debugEmailForm.Show();
        }

        private void mnuItemAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new frmAbout2();
            aboutForm.ShowDialog();
        }

        // ReSharper disable once IdentifierTypo
        private void frmDAnTEmdi_Load(object sender, EventArgs e)
        {
            StartMain(mSessionFile);
        }

        private void mnuWindowItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripItem;

            if (item?.Tag is string enumVal)
            {
                LayoutMdi((MdiLayout)Enum.Parse(typeof(MdiLayout), enumVal));
            }
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// User dropped a file into the main window
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
                if (fExt == null || !fExt.Equals(".dnt", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Only .dnt files can be opened via drag/drop here", "Unsupported file type",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                StartMain(s[0]);
            }
        }

        public ToolStrip MainDanteToolBar
        {
            get => mToolStripMDI;
            set => mToolStripMDI = value;
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