using System;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmShowProgress : Form
    {

        public frmShowProgress()
        {
            InitializeComponent();
        }

        private void frmPrBarLoad_event(object sender, EventArgs e)
        {
            this.mprgrsBar.MarqueeAnimationSpeed = 0;
            this.mprgrsBar.Minimum = 0;
            this.mprgrsBar.Maximum = 100;
            this.mprgrsBar.Value = 1;
        }

        public string Message
        {
            set { mlblProgressMsg.Text = value; }
        }

        public int PercentComplete
        {
            get { return mprgrsBar.Value; }
            set
            {
                if (value < mprgrsBar.Minimum)
                    mprgrsBar.Value = this.mprgrsBar.Minimum;
                else if (value > mprgrsBar.Maximum)
                    mprgrsBar.Value = this.mprgrsBar.Maximum;
                else
                    mprgrsBar.Value = value;
            }
        }
    }
}