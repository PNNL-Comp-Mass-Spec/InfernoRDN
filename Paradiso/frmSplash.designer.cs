namespace DAnTE.Paradiso
{
    partial class frmSplash
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbStatusInfo = new System.Windows.Forms.Label();
            this.mlblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbStatusInfo
            // 
            this.lbStatusInfo.AutoSize = true;
            this.lbStatusInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbStatusInfo.ForeColor = System.Drawing.SystemColors.Control;
            this.lbStatusInfo.Location = new System.Drawing.Point(51, 260);
            this.lbStatusInfo.Name = "lbStatusInfo";
            this.lbStatusInfo.Size = new System.Drawing.Size(54, 13);
            this.lbStatusInfo.TabIndex = 0;
            this.lbStatusInfo.Text = "Loading...";
            // 
            // mlblVersion
            // 
            this.mlblVersion.AutoSize = true;
            this.mlblVersion.BackColor = System.Drawing.Color.Transparent;
            this.mlblVersion.ForeColor = System.Drawing.SystemColors.Control;
            this.mlblVersion.Location = new System.Drawing.Point(51, 207);
            this.mlblVersion.Name = "mlblVersion";
            this.mlblVersion.Size = new System.Drawing.Size(42, 13);
            this.mlblVersion.TabIndex = 1;
            this.mlblVersion.Text = "Version";
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DAnTE.Properties.Resources.InfernoSplash2;
            this.ClientSize = new System.Drawing.Size(606, 381);
            this.Controls.Add(this.mlblVersion);
            this.Controls.Add(this.lbStatusInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbStatusInfo;
        private System.Windows.Forms.Label mlblVersion;
    }
}