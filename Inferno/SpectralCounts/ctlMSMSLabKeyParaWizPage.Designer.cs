namespace DAnTE.Inferno
{
    partial class ctlMSMSLabKeyParaWizPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtBoxPepProph = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(784, 64);
            this.Banner.Subtitle = "Select parameters for extracting spectral counts";
            this.Banner.Title = "Step 3: Select Parameters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(568, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Clicking \'Next\' will start the analysis.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Min. PeptideProphet Probability";
            // 
            // mtxtBoxPepProph
            // 
            this.mtxtBoxPepProph.Location = new System.Drawing.Point(185, 85);
            this.mtxtBoxPepProph.Name = "mtxtBoxPepProph";
            this.mtxtBoxPepProph.Size = new System.Drawing.Size(100, 20);
            this.mtxtBoxPepProph.TabIndex = 69;
            this.mtxtBoxPepProph.Text = "0.95";
            // 
            // ctlMSMSLabKeyParaWizPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mtxtBoxPepProph);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Name = "ctlMSMSLabKeyParaWizPage";
            this.Size = new System.Drawing.Size(784, 421);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.ctlMSMSparaWizPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.mtxtBoxPepProph, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mtxtBoxPepProph;
    }
}