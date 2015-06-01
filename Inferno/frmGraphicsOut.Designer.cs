namespace Inferno
{
    partial class frmGraphicsOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGraphicsOut));
            this.panel1 = new System.Windows.Forms.Panel();
            this.axStatConnGDev = new AxSTATCONNECTORCLNTLib.AxStatConnectorGraphicsDevice();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axStatConnGDev)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axStatConnGDev);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 680);
            this.panel1.TabIndex = 0;
            // 
            // axStatConnGDev
            // 
            this.axStatConnGDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axStatConnGDev.Enabled = true;
            this.axStatConnGDev.Location = new System.Drawing.Point(0, 0);
            this.axStatConnGDev.Name = "axStatConnGDev";
            this.axStatConnGDev.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axStatConnGDev.OcxState")));
            this.axStatConnGDev.Size = new System.Drawing.Size(709, 680);
            this.axStatConnGDev.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mbtnCancel);
            this.panel2.Controls.Add(this.mbtnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 680);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(709, 55);
            this.panel2.TabIndex = 1;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mbtnCancel.Location = new System.Drawing.Point(372, 16);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 2;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mbtnOK.Location = new System.Drawing.Point(269, 16);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 0;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // frmGraphicsOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(709, 735);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 320);
            this.Name = "frmGraphicsOut";
            this.Text = "Charts";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axStatConnGDev)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        public AxSTATCONNECTORCLNTLib.AxStatConnectorGraphicsDevice axStatConnGDev;
    }
}