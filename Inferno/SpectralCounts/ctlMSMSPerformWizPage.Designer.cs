namespace DAnTE.Inferno
{
    partial class ctlMSMSPerformWizPage
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
            this.mprogBar = new System.Windows.Forms.ProgressBar();
            this.mlstBoxMessages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(663, 64);
            this.Banner.Subtitle = "Process MS2 results and Create the Peptide Counts Table";
            this.Banner.Title = "Step 4: Performing Analysis";
            // 
            // mprogBar
            // 
            this.mprogBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mprogBar.Location = new System.Drawing.Point(16, 70);
            this.mprogBar.Name = "mprogBar";
            this.mprogBar.Size = new System.Drawing.Size(630, 16);
            this.mprogBar.TabIndex = 1;
            // 
            // mlstBoxMessages
            // 
            this.mlstBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mlstBoxMessages.FormattingEnabled = true;
            this.mlstBoxMessages.Location = new System.Drawing.Point(16, 103);
            this.mlstBoxMessages.Name = "mlstBoxMessages";
            this.mlstBoxMessages.Size = new System.Drawing.Size(630, 251);
            this.mlstBoxMessages.TabIndex = 2;
            // 
            // ctlMSMSPerformWizPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mprogBar);
            this.Controls.Add(this.mlstBoxMessages);
            this.Name = "ctlMSMSPerformWizPage";
            this.Size = new System.Drawing.Size(663, 388);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.ctlMSMSPerformWizPage_SetActive);
            this.Controls.SetChildIndex(this.mlstBoxMessages, 0);
            this.Controls.SetChildIndex(this.mprogBar, 0);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar mprogBar;
        private System.Windows.Forms.ListBox mlstBoxMessages;

    }
}