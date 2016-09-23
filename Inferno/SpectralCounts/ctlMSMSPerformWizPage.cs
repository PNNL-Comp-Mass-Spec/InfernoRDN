using System;
using System.ComponentModel;
using Wizard.UI;

namespace DAnTE.Inferno
{
    public partial class ctlMSMSPerformWizPage : Wizard.UI.InternalWizardPage
    {
        public ctlMSMSPerformWizPage()
        {
            InitializeComponent();
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void ctlMSMSPerformWizPage_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next);
            this.mprogBar.Value = 0;
        }

        public void ResetMessages()
        {
            mlstBoxMessages.Items.Clear();
        }

        #region Properties

        public int ProgressVal
        {
            set { this.mprogBar.Value = value; }
        }

        public bool EnableNextBtn
        {
            set { this.EnableNextButton(value); }
        }

        public string ShowMessege
        {
            set { mlstBoxMessages.Items.Add(value); }
        }

        #endregion

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // Banner
        //    // 
        //    this.Banner.Size = new System.Drawing.Size(537, 64);
        //    // 
        //    // ctlMSMSPerformWizPage
        //    // 
        //    this.Name = "ctlMSMSPerformWizPage";
        //    this.Size = new System.Drawing.Size(537, 264);
        //    this.ResumeLayout(false);

        //}
    }
}