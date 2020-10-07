using System;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmShowProgress : Form
    {

        public frmShowProgress()
        {
            InitializeComponent();
            Reset("No task");
        }

        private void frmPrBarLoad_event(object sender, EventArgs e)
        {
            pbarProgress.MarqueeAnimationSpeed = 0;
            pbarProgress.Minimum = 0;
            pbarProgress.Maximum = 100;
            pbarProgress.Value = 1;
        }

        public string ErrorMessage
        {
            get => lblErrorMsg.Text;
            set => lblErrorMsg.Text = value;
        }

        private string Message
        {
            set => lblProgressMsg.Text = value;
        }

        public string WarningMessage
        {
            get => lblWarningMsg.Text;
            set => lblWarningMsg.Text = value;
        }

        public int PercentComplete
        {
            get => pbarProgress.Value;
            set
            {
                if (value < pbarProgress.Minimum)
                    pbarProgress.Value = pbarProgress.Minimum;
                else if (value > pbarProgress.Maximum)
                    pbarProgress.Value = pbarProgress.Maximum;
                else
                    pbarProgress.Value = value;
            }
        }

        public void AppendWarningMessage(string message)
        {
            if (lblWarningMsg.Text.Contains(message))
                return;

            if (string.IsNullOrWhiteSpace(lblWarningMsg.Text))
            {
                lblWarningMsg.Text = message;
                return;
            }

            lblWarningMsg.Text = lblWarningMsg.Text + "\n\n" + message;
        }

        public void Reset(string task)
        {
            Message = task;
            ErrorMessage = string.Empty;
            WarningMessage = string.Empty;
            PercentComplete = 0;
        }
    }
}