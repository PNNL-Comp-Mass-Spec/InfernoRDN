using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public class frmMain : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components;

        public frmMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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

        private static bool AppUpdatesAvailableInternal()
        {
            const string fileLocation = @"\\floyd\Software\Inferno\";
            var currentVersion = Application.ProductVersion;

            if (Directory.Exists(fileLocation))
            {
                var folders = Directory.GetDirectories(fileLocation);
                foreach (var f in folders)
                {
                    if (f.Contains("Ver_"))
                    {
                        var fname = f.Substring(f.IndexOf("_", StringComparison.Ordinal) + 1).Trim();
                        if (fname.Equals(currentVersion))
                            return false;
                        else
                            return true;
                    }
                }
            }
            return false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Size = new System.Drawing.Size(300, 300);
            this.Text = "frmMain";
        }

        #endregion

        private static string mDanteFilePath;
        private static string mLogFilePath;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (AppUpdatesAvailableInternal())
            //{
            //    MessageBox.Show("New version of Inferno available.",
            //        "New version!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            //Splasher.Show(typeof(frmSplash));

            mDanteFilePath = string.Empty;
            mLogFilePath = string.Empty;

            try
            {
                var commandLineParser = new PRISM.clsParseCommandLine();
                var success = false;

                if (commandLineParser.ParseCommandLine())
                {
                    if (SetOptionsUsingCommandLineParameters(commandLineParser))
                        success = true;
                }
                else
                {
                    if (commandLineParser.ParameterCount + commandLineParser.NonSwitchParameterCount == 0 &&
                        !commandLineParser.NeedToShowHelp)
                        success = true;
                }

                if (!success || commandLineParser.NeedToShowHelp)
                {
                    var syntaxMessage = "Supported command line switches are /F and /L \n" +
                                        "Use '/F FilePath.dnt' to load a data file \n" +
                                        "Use '/L LogFilePath' to specify a custom log file path";

                    MessageBox.Show(syntaxMessage, "InfernoRDN Syntax", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception parsing the command line arguments: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            var danteMDI = new frmDAnTEmdi(mDanteFilePath, mLogFilePath);

            if (!danteMDI.IsDisposed)
                Application.Run(danteMDI);
        }

        private static bool SetOptionsUsingCommandLineParameters(PRISM.clsParseCommandLine commandLineParser)
        {
            // Returns True if no problems; otherwise, returns false
            var lstValidParameters = new List<string> {"F", "L"};

            try
            {
                // Make sure no invalid parameters are present
                if (commandLineParser.InvalidParametersPresent(lstValidParameters))
                {
                    var badArguments = new List<string>();
                    foreach (var item in commandLineParser.InvalidParameters(lstValidParameters))
                    {
                        badArguments.Add("/" + item);
                    }

                    ShowErrorMessage("Invalid command line parameters", badArguments);

                    return false;
                }

                // Query commandLineParser to see if various parameters are present

                if (commandLineParser.NonSwitchParameterCount > 0)
                    mDanteFilePath = commandLineParser.RetrieveNonSwitchParameter(0);

                if (!ParseParameter(commandLineParser, "F", "a session file path", ref mDanteFilePath)) return false;

                if (!ParseParameter(commandLineParser, "L", "a log file path", ref mLogFilePath)) return false;

                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error parsing the command line parameters: " + Environment.NewLine + ex.Message);
            }

            return false;
        }

        private static bool ParseParameter(
            PRISM.clsParseCommandLine commandLineParser,
            string parameterName,
            string description,
            ref string targetVariable)
        {
            string value;
            if (commandLineParser.RetrieveValueForParameter(parameterName, out value))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ShowErrorMessage("/" + parameterName + " does not have " + description);
                    return false;
                }
                targetVariable = string.Copy(value);
            }
            return true;
        }

        private static void ShowErrorMessage(string strMessage)
        {
            const string strSeparator = "------------------------------------------------------------------------------";

            Console.WriteLine();
            Console.WriteLine(strSeparator);
            Console.WriteLine(strMessage);
            Console.WriteLine(strSeparator);
            Console.WriteLine();

            MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private static void ShowErrorMessage(string strTitle, IEnumerable<string> items)
        {
            const string strSeparator = "------------------------------------------------------------------------------";

            Console.WriteLine();
            Console.WriteLine(strSeparator);
            Console.WriteLine(strTitle);
            var strMessage = strTitle + ":";

            foreach (var item in items)
            {
                Console.WriteLine("   " + item);
                strMessage += " " + item;
            }
            Console.WriteLine(strSeparator);
            Console.WriteLine();

            MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}