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
            this.mbtnCredits = new System.Windows.Forms.Button();
            this.mPixBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mlblDev = new System.Windows.Forms.Label();
            this.mTransparentlbl = new DAnTE.ExtraControls.ucTransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mPixBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbtnOK
            // 
            this.mbtnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnOK.Location = new System.Drawing.Point(189, 13);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 2;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = false;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // m_fadeInOutTimer
            // 
            this.m_fadeInOutTimer.Tick += new System.EventHandler(this.m_fadeInOutTimer_Tick);
            // 
            // mbtnCredits
            // 
            this.mbtnCredits.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mbtnCredits.Location = new System.Drawing.Point(26, 13);
            this.mbtnCredits.Name = "mbtnCredits";
            this.mbtnCredits.Size = new System.Drawing.Size(75, 23);
            this.mbtnCredits.TabIndex = 6;
            this.mbtnCredits.Text = "Thanks";
            this.mbtnCredits.UseVisualStyleBackColor = false;
            this.mbtnCredits.Click += new System.EventHandler(this.mbtnCredits_Click);
            // 
            // mPixBox
            // 
            this.mPixBox.BackgroundImage = global::DAnTE.Properties.Resources.AboutInferno;
            this.mPixBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mPixBox.ErrorImage = null;
            this.mPixBox.InitialImage = null;
            this.mPixBox.Location = new System.Drawing.Point(0, 0);
            this.mPixBox.Name = "mPixBox";
            this.mPixBox.Size = new System.Drawing.Size(290, 397);
            this.mPixBox.TabIndex = 7;
            this.mPixBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.mbtnOK);
            this.panel1.Controls.Add(this.mbtnCredits);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 48);
            this.panel1.TabIndex = 9;
            // 
            // mlblDev
            // 
            this.mlblDev.BackColor = System.Drawing.Color.Transparent;
            this.mlblDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDev.Location = new System.Drawing.Point(23, 51);
            this.mlblDev.Name = "mlblDev";
            this.mlblDev.Size = new System.Drawing.Size(241, 54);
            this.mlblDev.TabIndex = 10;
            this.mlblDev.Text = "label1";
            // 
            // mTransparentlbl
            // 
            this.mTransparentlbl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mTransparentlbl.BackColor = System.Drawing.Color.Transparent;
            this.mTransparentlbl.BorderColor = System.Drawing.Color.Transparent;
            this.mTransparentlbl.Caption = "Credits:";
            this.mTransparentlbl.DimmedColor = System.Drawing.Color.Transparent;
            this.mTransparentlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mTransparentlbl.Location = new System.Drawing.Point(26, 129);
            this.mTransparentlbl.MovingActive = true;
            this.mTransparentlbl.Name = "mTransparentlbl";
            this.mTransparentlbl.Opacity = 0;
            this.mTransparentlbl.Size = new System.Drawing.Size(227, 186);
            this.mTransparentlbl.TabIndex = 5;
            // 
            // frmAbout2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::DAnTE.Properties.Resources.AboutInfernoBack;
            this.ClientSize = new System.Drawing.Size(290, 397);
            this.Controls.Add(this.mlblDev);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mTransparentlbl);
            this.Controls.Add(this.mPixBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Inferno";
            ((System.ComponentModel.ISupportInitialize)(this.mPixBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Timer m_fadeInOutTimer;
        private ExtraControls.ucTransparentLabel mTransparentlbl;
        private System.Windows.Forms.Button mbtnCredits;
        private System.Windows.Forms.PictureBox mPixBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mlblDev;
    }
}