using System;
using System.Windows.Forms;

namespace DAnTE.Paradiso
{
    public partial class frmAbout : Form
    {
        private bool m_fadeInFlag;

        public frmAbout()
        {
            InitializeComponent();
        }

        private void m_fadeInOutTimer_Tick(object sender, EventArgs e)
        {
            // How should we fade?
            if (!m_fadeInFlag)
            {
                // Fading out
                Opacity -= (m_fadeInOutTimer.Interval / 400.0);

                // Should we continue to fade?
                if (this.Opacity > 0)
                {
                    m_fadeInOutTimer.Enabled = true;
                }
                else
                {
                    // Close the form.
                    m_fadeInOutTimer.Enabled = false;
                    Close();
                }
            }
            else
            {
                // Fading in
                Opacity += (m_fadeInOutTimer.Interval / 400.0);
                m_fadeInOutTimer.Enabled = (Opacity < 1.0);
                m_fadeInFlag = (Opacity < 1.0);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Should we start fading?
            if (!DesignMode)
            {
                m_fadeInFlag = true;
                Opacity = 0;

                m_fadeInOutTimer.Enabled = true;
            } // End if we should start the fading process.
            ShowCredits();
        } // End OnLoad()

        //protected override void OnClosing(CancelEventArgs e)
        //{

        //    base.OnClosing(e);

        //    // If the user canceled then don't fade anything.
        //    if (e.Cancel)
        //        return;

        //    // Should we fade instead of closing?
        //    if (Opacity > 0)
        //    {
        //        m_fadeInFlag = false;
        //        m_fadeInOutTimer.Enabled = true;
        //        e.Cancel = true;
        //    } // End if we should fade instead of closing.

        //} // End OnClosing()


        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowCredits()
        {
            var credits = "Thanks:" + Environment.NewLine + Environment.NewLine + "DAnTE project @PNNL";
            mTransparentlbl.Caption = credits;
        }

        public string version
        {
            set => mlblVersion.Text = value;
        }
    }
}