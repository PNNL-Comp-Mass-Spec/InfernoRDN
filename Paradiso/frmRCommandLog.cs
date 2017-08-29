using System;
using System.IO;
using System.Windows.Forms;

namespace DAnTE.Paradiso
{
    public partial class frmRCommandLog : Form
    {
        public frmRCommandLog()
        {
            InitializeComponent();
        }

        private string mLogFilePath;

        public string LogFilePath
        {
            get => mLogFilePath;
            set => ShowLogFile(value);
        }

        public void ShowLogFile(string logFilePath)
        {
            mLogFilePath = logFilePath;
            var fiLogFile = new FileInfo(logFilePath);

            if (!fiLogFile.Exists)
            {
                txtRCmdLog.Text = string.Empty;
                txtLogFilePath.Text = "File not found: " + logFilePath;
            }
            else
            {
                txtLogFilePath.Text = fiLogFile.FullName;
                using (
                    var reader =
                        new StreamReader(new FileStream(fiLogFile.FullName, FileMode.Open, FileAccess.Read,
                                                        FileShare.ReadWrite)))
                {
                    txtRCmdLog.Text = reader.ReadToEnd();
                }
            }
        }

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}