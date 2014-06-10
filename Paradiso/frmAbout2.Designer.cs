namespace DAnTE.Paradiso
{
    partial class frmAbout2
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout2));
			this.mbtnOK = new System.Windows.Forms.Button();
			this.m_fadeInOutTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.mlblDev = new System.Windows.Forms.Label();
			this.mlblCredits = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mbtnOK
			// 
			this.mbtnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.mbtnOK.Location = new System.Drawing.Point(135, 18);
			this.mbtnOK.Margin = new System.Windows.Forms.Padding(4);
			this.mbtnOK.Name = "mbtnOK";
			this.mbtnOK.Size = new System.Drawing.Size(100, 28);
			this.mbtnOK.TabIndex = 2;
			this.mbtnOK.Text = "&OK";
			this.mbtnOK.UseVisualStyleBackColor = false;
			this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
			// 
			// m_fadeInOutTimer
			// 
			this.m_fadeInOutTimer.Tick += new System.EventHandler(this.m_fadeInOutTimer_Tick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.panel1.Controls.Add(this.mbtnOK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 361);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(387, 59);
			this.panel1.TabIndex = 9;
			// 
			// mlblDev
			// 
			this.mlblDev.BackColor = System.Drawing.Color.Transparent;
			this.mlblDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mlblDev.Location = new System.Drawing.Point(31, 33);
			this.mlblDev.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.mlblDev.Name = "mlblDev";
			this.mlblDev.Size = new System.Drawing.Size(321, 66);
			this.mlblDev.TabIndex = 10;
			this.mlblDev.Text = "Author";
			// 
			// mlblCredits
			// 
			this.mlblCredits.BackColor = System.Drawing.Color.Transparent;
			this.mlblCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mlblCredits.Location = new System.Drawing.Point(31, 112);
			this.mlblCredits.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.mlblCredits.Name = "mlblCredits";
			this.mlblCredits.Size = new System.Drawing.Size(321, 245);
			this.mlblCredits.TabIndex = 11;
			this.mlblCredits.Text = "Credits";
			// 
			// frmAbout2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.BackgroundImage = global::DAnTE.Properties.Resources.AboutInfernoBack;
			this.ClientSize = new System.Drawing.Size(387, 420);
			this.Controls.Add(this.mlblCredits);
			this.Controls.Add(this.mlblDev);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About InfernoRDN";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
		private System.Windows.Forms.Timer m_fadeInOutTimer;
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label mlblDev;
		private System.Windows.Forms.Label mlblCredits;
    }
}