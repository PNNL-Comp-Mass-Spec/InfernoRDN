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
            this.mprgrsBar = new System.Windows.Forms.ProgressBar();
            this.mlblProgressMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mprgrsBar
            // 
            this.mprgrsBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.mprgrsBar.Location = new System.Drawing.Point(12, 34);
            this.mprgrsBar.Name = "mprgrsBar";
            this.mprgrsBar.Size = new System.Drawing.Size(405, 20);
            this.mprgrsBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.mprgrsBar.TabIndex = 0;
            this.mprgrsBar.UseWaitCursor = true;
            // 
            // mlblProgressMsg
            // 
            this.mlblProgressMsg.AutoSize = true;
            this.mlblProgressMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mlblProgressMsg.Location = new System.Drawing.Point(12, 9);
            this.mlblProgressMsg.Name = "mlblProgressMsg";
            this.mlblProgressMsg.Size = new System.Drawing.Size(49, 13);
            this.mlblProgressMsg.TabIndex = 1;
            this.mlblProgressMsg.Text = "message";
            // 
            // frmShowProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 66);
            this.Controls.Add(this.mlblProgressMsg);
            this.Controls.Add(this.mprgrsBar);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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

        private System.Windows.Forms.ProgressBar mprgrsBar;
        private System.Windows.Forms.Label mlblProgressMsg;
    }
}