using System.ComponentModel;
using Wizard.UI;

namespace DAnTE.Inferno
{
    public partial class ctlMSMSLabKeyParaWizPage : Wizard.UI.InternalWizardPage
    {
        public ctlMSMSLabKeyParaWizPage()
        {
            InitializeComponent();
        }

        private void ctlMSMSparaWizPage_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next);
        }

        #region Properties

        public string PepProphMin => mtxtBoxPepProph.Text;

        #endregion
    }
}