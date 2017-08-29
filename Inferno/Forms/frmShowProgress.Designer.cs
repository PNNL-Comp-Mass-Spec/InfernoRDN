namespace DAnTE.Inferno
{
    partial class frmShowProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowProgress));
            this.pbarProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgressMsg = new System.Windows.Forms.Label();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbarProgress
            // 
            this.pbarProgress.ForeColor = System.Drawing.Color.LimeGreen;
            this.pbarProgress.Location = new System.Drawing.Point(16, 42);
            this.pbarProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbarProgress.Name = "pbarProgress";
            this.pbarProgress.Size = new System.Drawing.Size(540, 25);
            this.pbarProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbarProgress.TabIndex = 0;
            this.pbarProgress.UseWaitCursor = true;
            // 
            // lblProgressMsg
            // 
            this.lblProgressMsg.AutoSize = true;
            this.lblProgressMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProgressMsg.Location = new System.Drawing.Point(16, 11);
            this.lblProgressMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgressMsg.Name = "lblProgressMsg";
            this.lblProgressMsg.Size = new System.Drawing.Size(65, 17);
            this.lblProgressMsg.TabIndex = 1;
            this.lblProgressMsg.Text = "message";
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblErrorMsg.Location = new System.Drawing.Point(16, 85);
            this.lblErrorMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(39, 17);
            this.lblErrorMsg.TabIndex = 2;
            this.lblErrorMsg.Text = "error";
            // 
            // frmShowProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 122);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblProgressMsg);
            this.Controls.Add(this.pbarProgress);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimizeBox = false;
            this.Name = "frmShowProgress";
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing ...";
            this.Load += new System.EventHandler(this.frmPrBarLoad_event);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbarProgress;
        private System.Windows.Forms.Label lblProgressMsg;
        private System.Windows.Forms.Label lblErrorMsg;
    }
}