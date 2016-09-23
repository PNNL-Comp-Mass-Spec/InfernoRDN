using System.Windows.Forms;

namespace Wizard.UI
{
    public class ExternalWizardPage : Wizard.UI.WizardPage
    {
        private Panel panel1;
        private readonly System.ComponentModel.IContainer components = null;

        protected ExternalWizardPage()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ExternalWizardPage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 293);
            this.panel1.TabIndex = 1;
            // 
            // ExternalWizardPage
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Name = "ExternalWizardPage";
            this.Size = new System.Drawing.Size(513, 293);
            this.ResumeLayout(false);
        }

        #endregion
    }
}