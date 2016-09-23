using System.ComponentModel;

namespace Wizard.UI
{
    public class WizardPageEventArgs : CancelEventArgs
    {
        public string NewPage { get; set; } = null;
    }

    public delegate void WizardPageEventHandler(object sender, WizardPageEventArgs e);
}