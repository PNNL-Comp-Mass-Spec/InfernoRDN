namespace DAnTE.Paradiso
{
    partial class frmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
			this.mlblVersion = new System.Windows.Forms.Label();
			this.mbtnOK = new System.Windows.Forms.Button();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.m_fadeInOutTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.mTransparentlbl = new DAnTE.ExtraControls.ucTransparentLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mlblVersion
			// 
			this.mlblVersion.AutoSize = true;
			this.mlblVersion.BackColor = System.Drawing.Color.White;
			this.mlblVersion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mlblVersion.Location = new System.Drawing.Point(273, 185);
			this.mlblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.mlblVersion.Name = "mlblVersion";
			this.mlblVersion.Size = new System.Drawing.Size(46, 17);
			this.mlblVersion.TabIndex = 1;
			this.mlblVersion.Text = "label1";
			// 
			// mbtnOK
			// 
			this.mbtnOK.BackColor = System.Drawing.Color.White;
			this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.mbtnOK.Location = new System.Drawing.Point(640, 293);
			this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.mbtnOK.Name = "mbtnOK";
			this.mbtnOK.Size = new System.Drawing.Size(100, 28);
			this.mbtnOK.TabIndex = 2;
			this.mbtnOK.Text = "&OK";
			this.mbtnOK.UseVisualStyleBackColor = false;
			this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.BackColor = System.Drawing.Color.White;
			this.lblAuthor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAuthor.Location = new System.Drawing.Point(273, 293);
			this.lblAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(209, 17);
			this.lblAuthor.TabIndex = 3;
			this.lblAuthor.Text = "Developed by: Ashoka Polpitiya";
			// 
			// m_fadeInOutTimer
			// 
			this.m_fadeInOutTimer.Tick += new System.EventHandler(this.m_fadeInOutTimer_Tick);
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.mbtnOK);
			this.panel1.Controls.Add(this.mlblVersion);
			this.panel1.Controls.Add(this.mTransparentlbl);
			this.panel1.Controls.Add(this.lblAuthor);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(775, 341);
			this.panel1.TabIndex = 6;
			// 
			// mTransparentlbl
			// 
			this.mTransparentlbl.BackColor = System.Drawing.Color.Transparent;
			this.mTransparentlbl.BorderColor = System.Drawing.Color.Transparent;
			this.mTransparentlbl.Caption = "Credits:";
			this.mTransparentlbl.DimmedColor = System.Drawing.Color.LightGray;
			this.mTransparentlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mTransparentlbl.Location = new System.Drawing.Point(31, 281);
			this.mTransparentlbl.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.mTransparentlbl.Moving = DAnTE.ExtraControls.ucTransparentLabel.MoveType.DownToUp;
			this.mTransparentlbl.MovingActive = true;
			this.mTransparentlbl.Name = "mTransparentlbl";
			this.mTransparentlbl.Opacity = 0;
			this.mTransparentlbl.ShapeBorderStyle = DAnTE.ExtraControls.ucTransparentLabel.ShapeBorderStyles.ShapeBSFixedSingle;
			this.mTransparentlbl.Size = new System.Drawing.Size(187, 41);
			this.mTransparentlbl.TabIndex = 5;
			// 
			// frmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(775, 341);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label mlblVersion;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Timer m_fadeInOutTimer;
        private ExtraControls.ucTransparentLabel mTransparentlbl;
        private System.Windows.Forms.Panel panel1;
    }
}