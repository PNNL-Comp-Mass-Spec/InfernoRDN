using System;
using System.IO;
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
        private frmRmsg mfrmRmsg;

        public string SessionFile = null;
        public string logFilename = null;
        private System.IO.StreamWriter logwriter;
        private bool mblLog = false;
        private string tempPath = @"c:";
        private string tempFile = "";
        private string Repository;// = @"http://lib.stat.cmu.edu/R/CRAN";
        private string RpackList;
        private bool mblInstallRpacks = false;
        private bool mblUpdateRpacks = false;
        private clsRconnect rConnector;
        private bool mblPlotgg = Settings.Default.useGG;
        private string mstrPlotFuncFileName = null;
        private StringBuilder startupErrString = new StringBuilder();


        public frmDAnTEmdi(string dntfile, string logfile)
        {
            logFilename = logfile;
            SessionFile = dntfile;

            InitializeComponent();

            if (logFilename != null)
            {
                mblLog = true;

                if (!File.Exists(logFilename))
                    logwriter = new StreamWriter(logFilename);
                else
                    logwriter = File.AppendText(logFilename);
            }

            this.Text = "InfernoRDN"; //Application.ProductVersion.ToString();

            SplashScreen.ShowSplashScreen();
            Application.DoEvents();

            Log(mblLog, string.Format("Starting Inferno [{0}]...", DateTime.Now), logwriter);
            SplashScreen.SetStatus(string.Format("Starting Inferno [{0}]...", DateTime.Now));
            System.Threading.Thread.Sleep(100);
            SplashScreen.SetStatus("Reading Configuration Parameters...");
            if (!ConfigParameters())
            {
                Log(mblLog, "Error: Error in reading inferno.conf file.", logwriter);
                startupErrString.Append("* Error in reading inferno.conf file.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in reading inferno.conf file.",
                //    "inferno.conf error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            Log(mblLog, "Done reading configuration parameters.", logwriter);

            SplashScreen.SetStatus("Initializing Folders...");
            // Initialize folders
            //tempPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            tempPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDataFldrPath = tempPath.Replace("\\", "/") + "/Inferno";
            if (!Directory.Exists(appDataFldrPath))
            {
                Directory.CreateDirectory(appDataFldrPath);
            }
            tempFile = appDataFldrPath + "/_temp.png";
            this.mhelpProviderDAnTE.HelpNamespace = Application.StartupPath + "\\InfernoHelp.chm";
            System.Threading.Thread.Sleep(10);
            Log(mblLog, "Done setting folders.", logwriter);

            SplashScreen.SetStatus("Establishing Connection to R...");
            rConnector = new clsRconnect();
            if (!rConnector.initR())
            {
                startupErrString.Append(string.Format("* R failed to initialize: {0}", rConnector.Message)).AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Try again. R failed to initialize for some unknown reason.",
                //    "R connection failed.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Log(mblLog, "Error: Connection to R failed.", logwriter);
                //this.Close();
            }
            System.Threading.Thread.Sleep(10);
            Log(mblLog, "Done Connecting to R.", logwriter);

            SplashScreen.SetStatus("Initializing R Functions...");
            if (!LoadRfunctions("Inferno.RData"))
            {
                startupErrString.Append("* Error in sourcing R functions.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mblLog, "Error: Sourcing R functions failed.", logwriter);
                //this.Close();
            }
            Log(mblLog, "Done sourcing R functions.", logwriter);

            SplashScreen.SetStatus("Initializing R Plotting Functions...");
            if (mblPlotgg)
                mstrPlotFuncFileName = "Inferno_ggplots.RData";
            else
                mstrPlotFuncFileName = "Inferno_stdplots.RData";

            if (!LoadRfunctions(mstrPlotFuncFileName))
            {
                startupErrString.Append("* Error in sourcing R plotting functions.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error in sourcing R plotting functions", "Initializing R error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mblLog, "Error: Sourcing R plotting functions failed.", logwriter);
                //this.Close();
            }
            Log(mblLog, "Done sourcing R plotting functions.", logwriter);

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
                Log(mblLog, "Error: R version incompatible.", logwriter);
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
                Log(mblLog, "Error: Loading required R packages failed.", logwriter);
                //this.Close();
            }
            Log(mblLog, "Done loading required R packages.", logwriter);
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus("Cleaning up temp files...");
            if (!DeleteTempFile(tempFile))
            {
                startupErrString.Append("* Error cleaning temp files.").AppendLine();
                //SplashScreen.CloseForm();
                //MessageBox.Show("Error cleaning temp files", "Error cleaning files",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(mblLog, "Error: Cleaning temp files failed.", logwriter);
                //this.Close();
            }
            Log(mblLog, "Done cleaning temp files.", logwriter);
            //DeleteTempFile(tempFile);
            System.Threading.Thread.Sleep(10);

            SplashScreen.SetStatus("Setting Up Child Forms...");
            if (RunRlogs())
            {
                mfrmRmsg = new frmRmsg();
                try
                {
                    //-// rConnector.SetCharacterOutputDevice((StatConnectorCommonLib.IStatConnectorCharacterDevice)mfrmRmsg.axStatConnectorCharacterDevice1.GetOcx());
                    rConnector.EvaluateNoReturn("print(version)");
                    Log(mblLog, "Done starting R log viewer.", logwriter);
                }
                catch (Exception ex)
                {
                    startupErrString.Append("* Error starting R log viewer.").AppendLine();
                    //MessageBox.Show("Error: " + ex.Message, "Watchout!");
                    Log(mblLog, "Error: Error starting R log viewer." + ex.Message, logwriter);
                }
            }
            else
                Log(mblLog, "R log viewer not used for this OS.", logwriter);
            this.Activate();
            SplashScreen.CloseForm();

            Log(mblLog, "Inferno started.", logwriter);
            if (startupErrString.Length > 1)
                MessageBox.Show(startupErrString.ToString(), "Errors Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region Private methods

        private static void Log(bool mblog, String logMessage, TextWriter w)
        {
            clsRCmdLog.LogOperation(logMessage);
            if (mblog)
            {
                //w.Write("\r\nLog Entry : ");
                w.Write("\r\n{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("{0}", logMessage);
                //w.WriteLine("-------------------------------");
                // Update the underlying file.
                w.Flush();
            }
        }


        private bool ConfigParameters()
        {
            string confPath = Application.StartupPath + "\\inferno.conf";
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
            Repository = engine["Repository"].Val;
            RpackList = engine["Rpackages"].Val;
            //RfilesPath = engine["Rfolder"].Val;
            if (engine["InstallRpacks"].Val.Equals("true"))
                mblInstallRpacks = true;
            else
                mblInstallRpacks = false;

            if (engine["UpdateRpacks"].Val.Equals("true"))
                mblUpdateRpacks = true;
            else
                mblUpdateRpacks = false;

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
                success = rConnector.loadRData(mstrRDataFile);
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
                rConnector.EvaluateNoReturn(rcmd);
                ////object rout = rConnector.GetSymbol("verOK");
                ////mblresult = (bool)rout;
                mblresult = rConnector.GetSymbolAsBool("verOK");
            }
            catch (Exception ex)
            {
                mblresult = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return mblresult;
        }

        private bool InstallRequiredRPackages()
        {
            string rcommand;
            bool mblresult = true;

            try
            {
                if (mblInstallRpacks)
                {
                    rcommand = @"installPackages(c(" + RpackList + @"), repository=""" + Repository + @""")";
                    rConnector.EvaluateNoReturn(rcommand);
                }

                if (mblUpdateRpacks)
                {
                    rcommand = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + Repository + @""")";
                    rConnector.EvaluateNoReturn(rcommand);
                }

                mblresult = true;
            }
            catch (Exception ex)
            {
                mblresult = false;
                Console.WriteLine("Exception thrown: " + ex.Message, "Error!");
            }
            return mblresult;
        }

        private bool UpdateRPackages()
        {
            string rcommand;
            bool mblresult = true;

            try
            {
                rcommand = @"update.packages(checkBuilt=TRUE, ask=FALSE,repos=""" + Repository + @""")";
                rConnector.EvaluateNoReturn(rcommand);

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

        private bool DeleteTempFile(string tempfile)
        {
            bool ok = true;
            tempfile = tempfile.Replace("/", "\\");

            if (File.Exists(tempfile))
            {
                try
                {
                    rConnector.EvaluateNoReturn("graphics.off()");
                    File.Delete(tempfile);
                }
                catch (Exception ex)
                {
                    ok = false;
                    Console.WriteLine(ex.Message);
                }
            }
            return ok;
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
                mfrmDAnTE.TempFile = tempFile;
                mfrmDAnTE.TempLocation = tempPath;
                mfrmDAnTE.RConnector = rConnector;
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


        #endregion

        #endregion

        private void mnuItemNew_Click(object sender, EventArgs e)
        {
            StartMain(SessionFile);
        }

        private void mnuItemExit_Click(object sender, EventArgs e)
        {
            frmDAnTE mfrmDAnTE = frmDAnTE.GetChildInstance();

            if (mfrmDAnTE.OK2Exit())
            {
                this.DialogResult = DialogResult.OK;
                rConnector.closeR();
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

        [Obsolete("Old feature that no longer works")]
        private void mnuItemRlogs_Click(object sender, EventArgs e)
        {
            if (RunRlogs())
            {
                try
                {
                    mfrmRmsg.Show();
                    mfrmRmsg.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Watchout!");
                }
            }
            else
            {
                MessageBox.Show("This feature available only on 32bit Windows XP.", "Not available",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuItemClearTmp_Click(object sender, EventArgs e)
        {
            DeleteTempFile(tempFile);
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
            //frmAbout mfrmAbout = new frmAbout();
            //mfrmAbout.version = "Version " + Application.ProductVersion.ToString();
            ////mfrmAbout.version = "Version 0.13";
            //mfrmAbout.ShowDialog();

            frmAbout2 mfrmAbout = new frmAbout2();
            mfrmAbout.ShowDialog();
        }

        //protected override void OnMdiChildActivate(EventArgs e)
        //{
        //    ToolStripManager.RevertMerge(this.mtoolStripMDI);

        //    frmDAnTE f1 = ActiveMdiChild as frmDAnTE;
        //    if (f1 != null)
        //    {
        //        ToolStripManager.Merge(f1.ToolStripDAnTE, mtoolStripMDI.Name);
        //    }
        //    base.OnMdiChildActivate(e);
        //}

        private void frmDAnTEmdi_Load(object sender, EventArgs e)
        {
            StartMain(SessionFile);
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