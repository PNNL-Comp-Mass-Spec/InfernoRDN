using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Wizard.UI;
using DAnTE.Properties;

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
        public string PepProphMin
        {
            get
            {
                return mtxtBoxPepProph.Text;
            }
        }
        #endregion

    }
}