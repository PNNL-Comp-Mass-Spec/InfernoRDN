using System.ComponentModel;
using Wizard.UI;

namespace DAnTE.Inferno
{
    public partial class ctlMSMSCompleteWizPage : Wizard.UI.ExternalWizardPage
    {
        public ctlMSMSCompleteWizPage()
        {
            InitializeComponent();
        }

        private void ctlAnovaCompleteWizPage_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Finish);
        }
    }
}