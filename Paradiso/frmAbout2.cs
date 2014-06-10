using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DAnTE.Paradiso
{
    public partial class frmAbout2 : Form
    {
        private bool m_fadeInFlag;

        public frmAbout2()
        {
            InitializeComponent();
        }

        private void m_fadeInOutTimer_Tick(object sender, System.EventArgs e)
        {
          
            if (m_fadeInFlag == false)
            {
				// Fading out

                Opacity -= (m_fadeInOutTimer.Interval / 400.0);

                // Should we continue to fade?
                if (this.Opacity > 0)
                    m_fadeInOutTimer.Enabled = true;
                else
                {

                    m_fadeInOutTimer.Enabled = false;
                    Close();

                } // End else we should close the form.

            }
            else
            {
				// Fading in

                Opacity += (m_fadeInOutTimer.Interval / 400.0);
				m_fadeInFlag = (Opacity < 1.0);
				if (!m_fadeInFlag)
				{
					m_fadeInOutTimer.Enabled = false;

					ShowCredits();
				}

            }

        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            
            if (!DesignMode)
            {
				// Only show the fading process at runtime

                m_fadeInFlag = true;
                Opacity = 0;

                m_fadeInOutTimer.Enabled = true;

            } 

			mlblCredits.Visible = false;
            mlblDev.Visible = false;
            
        } 

		// Uncomment to toggle fading out the about box
		//
        //protected override void OnClosing(CancelEventArgs e)
        //{

        //    base.OnClosing(e);

        //    // If the user canceled then don't fade anything.
        //    if (e.Cancel == true)
        //        return;

        //    if (Opacity > 0)
        //    {
        //        m_fadeInFlag = false;
        //        m_fadeInOutTimer.Enabled = true;
        //        e.Cancel = true;
        //    }

        //}

        private void mbtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void ShowCredits()
        {
			string credits = "Thanks:" + Environment.NewLine + Environment.NewLine + "DAnTE project @PNNL" +
				Environment.NewLine +
				Environment.NewLine + "Konstantinos Petritis and the" +
				Environment.NewLine + "Center for Proteomics staff at TGen." +
				Environment.NewLine +
				Environment.NewLine + "Gary Kiebel and Matthew Monroe" +
				Environment.NewLine + "at Pacific Northwest National Laboratory" +
			    Environment.NewLine + "(matthew.monroe@pnnl.gov)";

			mlblCredits.Visible = true;			
			mlblCredits.Text = credits;

			mlblDev.Visible = true;
			mlblDev.Text = "Developed by:" + Environment.NewLine +
				"   Ashoka Polpitiya" + Environment.NewLine +
				"   (ashoka@tgen.org)";
        }

    }
}