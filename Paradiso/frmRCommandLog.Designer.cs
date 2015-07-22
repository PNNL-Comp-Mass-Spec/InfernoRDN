namespace DAnTE.Paradiso
{
    partial class frmRCommandLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRCommandLog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.txtRCmdLog = new System.Windows.Forms.TextBox();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtLogFilePath);
            this.panel1.Controls.Add(this.mbtnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 494);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 59);
            this.panel1.TabIndex = 5;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnOK.Location = new System.Drawing.Point(30, 18);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(90, 27);
            this.mbtnOK.TabIndex = 0;
            this.mbtnOK.Text = "&OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // txtRCmdLog
            // 
            this.txtRCmdLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRCmdLog.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRCmdLog.Location = new System.Drawing.Point(12, 12);
            this.txtRCmdLog.Multiline = true;
            this.txtRCmdLog.Name = "txtRCmdLog";
            this.txtRCmdLog.ReadOnly = true;
            this.txtRCmdLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRCmdLog.Size = new System.Drawing.Size(853, 466);
            this.txtRCmdLog.TabIndex = 6;
            this.txtRCmdLog.WordWrap = false;
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogFilePath.Location = new System.Drawing.Point(140, 20);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.ReadOnly = true;
            this.txtLogFilePath.Size = new System.Drawing.Size(725, 27);
            this.txtLogFilePath.TabIndex = 1;
            // 
            // frmRCommandLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnOK;
            this.ClientSize = new System.Drawing.Size(877, 553);
            this.Controls.Add(this.txtRCmdLog);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRCommandLog";
            this.Text = "R Command Log";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.TextBox txtRCmdLog;
        private System.Windows.Forms.TextBox txtLogFilePath;
    }
}