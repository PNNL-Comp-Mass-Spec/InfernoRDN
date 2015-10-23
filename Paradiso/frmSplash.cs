using System.Windows.Forms;

namespace DAnTE.Paradiso
{
    public partial class frmSplash : Form,ISplashForm
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