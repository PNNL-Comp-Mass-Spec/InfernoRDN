using System;
using System.Windows.Forms;

namespace DAnTE.Paradiso
{
    [Obsolete("Unused")]
    public partial class frmSplash : Form, ISplashForm
    {
        public frmSplash()
        {
            InitializeComponent();
            mlblVersion.Text = "Version " + Application.ProductVersion;
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string NewStatusInfo)
        {
            lbStatusInfo.Text = NewStatusInfo;
        }

        #endregion
    }
}