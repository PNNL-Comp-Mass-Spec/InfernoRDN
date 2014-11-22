using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    public partial class frmShowProgress : Form
    {
        public delegate void ButtonClicked();
        //public event ButtonClicked meventCancelButtonClicked;

        public frmShowProgress()
        {
            InitializeComponent();
        }

        //private void mbtnCancel_Click(object sender, System.EventArgs e)
        //{
        //    if (meventCancelButtonClicked != null)
        //        meventCancelButtonClicked();
        //}

        private void frmPrBarLoad_event(object sender, EventArgs e)
        {
            this.mprgrsBar.MarqueeAnimationSpeed = 0;
            this.mprgrsBar.Minimum = 0;
            this.mprgrsBar.Maximum = 100;
            this.mprgrsBar.Value = 1;
        }

        public string Message
        {
            set
            {
                mlblProgressMsg.Text = value;
            }
        }

        public int PercentComplete
        {
            get
            {
                return mprgrsBar.Value;
            }
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