namespace DAnTE.Inferno
{
    partial class frmColorPalette
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmColorPalette));
            this.panel2 = new System.Windows.Forms.Panel();
            this.mrBtnBWR = new System.Windows.Forms.RadioButton();
            this.mrBtnBlackBody = new System.Windows.Forms.RadioButton();
            this.mbtnLow = new System.Windows.Forms.Button();
            this.mrbtnCustom = new System.Windows.Forms.RadioButton();
            this.mbtnHigh = new System.Windows.Forms.Button();
            this.mrbtnHeat = new System.Windows.Forms.RadioButton();
            this.mrbtnRedGreen = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.mbtnMid = new System.Windows.Forms.Button();
            this.mlblLow = new System.Windows.Forms.Label();
            this.mlblHigh = new System.Windows.Forms.Label();
            this.mlblMid = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.mrBtnBWR);
            this.panel2.Controls.Add(this.mrBtnBlackBody);
            this.panel2.Controls.Add(this.mbtnLow);
            this.panel2.Controls.Add(this.mrbtnCustom);
            this.panel2.Controls.Add(this.mbtnHigh);
            this.panel2.Controls.Add(this.mrbtnHeat);
            this.panel2.Controls.Add(this.mrbtnRedGreen);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.mbtnMid);
            this.panel2.Controls.Add(this.mlblLow);
            this.panel2.Controls.Add(this.mlblHigh);
            this.panel2.Controls.Add(this.mlblMid);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(15, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 169);
            this.panel2.TabIndex = 4;
            // 
            // mrBtnBWR
            // 
            this.mrBtnBWR.AutoSize = true;
            this.mrBtnBWR.Location = new System.Drawing.Point(40, 84);
            this.mrBtnBWR.Name = "mrBtnBWR";
            this.mrBtnBWR.Size = new System.Drawing.Size(136, 17);
            this.mrBtnBWR.TabIndex = 13;
            this.mrBtnBWR.Text = "Blue-White-Red Palette";
            this.mrBtnBWR.UseVisualStyleBackColor = true;
            // 
            // mrBtnBlackBody
            // 
            this.mrBtnBlackBody.AutoSize = true;
            this.mrBtnBlackBody.Checked = true;
            this.mrBtnBlackBody.Location = new System.Drawing.Point(40, 15);
            this.mrBtnBlackBody.Name = "mrBtnBlackBody";
            this.mrBtnBlackBody.Size = new System.Drawing.Size(116, 17);
            this.mrBtnBlackBody.TabIndex = 12;
            this.mrBtnBlackBody.TabStop = true;
            this.mrBtnBlackBody.Text = "\'BlackBody\' Palette";
            this.mrBtnBlackBody.UseVisualStyleBackColor = true;
            // 
            // mbtnLow
            // 
            this.mbtnLow.Location = new System.Drawing.Point(123, 132);
            this.mbtnLow.Name = "mbtnLow";
            this.mbtnLow.Size = new System.Drawing.Size(25, 20);
            this.mbtnLow.TabIndex = 7;
            this.mbtnLow.Text = "\'\'\'";
            this.mbtnLow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mbtnLow.UseVisualStyleBackColor = true;
            this.mbtnLow.Click += new System.EventHandler(this.mbtnLow_Click);
            // 
            // mrbtnCustom
            // 
            this.mrbtnCustom.AutoSize = true;
            this.mrbtnCustom.Location = new System.Drawing.Point(40, 106);
            this.mrbtnCustom.Name = "mrbtnCustom";
            this.mrbtnCustom.Size = new System.Drawing.Size(96, 17);
            this.mrbtnCustom.TabIndex = 2;
            this.mrbtnCustom.Text = "Custom Palette";
            this.mrbtnCustom.UseVisualStyleBackColor = true;
            this.mrbtnCustom.CheckedChanged += new System.EventHandler(this.mrbtnCustom_CheckedChanged);
            // 
            // mbtnHigh
            // 
            this.mbtnHigh.Location = new System.Drawing.Point(317, 132);
            this.mbtnHigh.Name = "mbtnHigh";
            this.mbtnHigh.Size = new System.Drawing.Size(25, 20);
            this.mbtnHigh.TabIndex = 11;
            this.mbtnHigh.Text = "\'\'\'";
            this.mbtnHigh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mbtnHigh.UseVisualStyleBackColor = true;
            this.mbtnHigh.Click += new System.EventHandler(this.mbtnHigh_Click);
            // 
            // mrbtnHeat
            // 
            this.mrbtnHeat.AutoSize = true;
            this.mrbtnHeat.Location = new System.Drawing.Point(40, 38);
            this.mrbtnHeat.Name = "mrbtnHeat";
            this.mrbtnHeat.Size = new System.Drawing.Size(84, 17);
            this.mrbtnHeat.TabIndex = 1;
            this.mrbtnHeat.Text = "Heat Palette";
            this.mrbtnHeat.UseVisualStyleBackColor = true;
            // 
            // mrbtnRedGreen
            // 
            this.mrbtnRedGreen.AutoSize = true;
            this.mrbtnRedGreen.Location = new System.Drawing.Point(40, 61);
            this.mrbtnRedGreen.Name = "mrbtnRedGreen";
            this.mrbtnRedGreen.Size = new System.Drawing.Size(113, 17);
            this.mrbtnRedGreen.TabIndex = 0;
            this.mrbtnRedGreen.Text = "Red-Green Palette";
            this.mrbtnRedGreen.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Low:";
            // 
            // mbtnMid
            // 
            this.mbtnMid.Location = new System.Drawing.Point(218, 132);
            this.mbtnMid.Name = "mbtnMid";
            this.mbtnMid.Size = new System.Drawing.Size(25, 20);
            this.mbtnMid.TabIndex = 9;
            this.mbtnMid.Text = "\'\'\'";
            this.mbtnMid.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mbtnMid.UseVisualStyleBackColor = true;
            this.mbtnMid.Click += new System.EventHandler(this.mbtnMid_Click);
            // 
            // mlblLow
            // 
            this.mlblLow.AutoSize = true;
            this.mlblLow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblLow.Location = new System.Drawing.Point(101, 133);
            this.mlblLow.Name = "mlblLow";
            this.mlblLow.Size = new System.Drawing.Size(18, 15);
            this.mlblLow.TabIndex = 6;
            this.mlblLow.Text = "M";
            this.mlblLow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlblHigh
            // 
            this.mlblHigh.AutoSize = true;
            this.mlblHigh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblHigh.Location = new System.Drawing.Point(294, 133);
            this.mlblHigh.Name = "mlblHigh";
            this.mlblHigh.Size = new System.Drawing.Size(18, 15);
            this.mlblHigh.TabIndex = 10;
            this.mlblHigh.Text = "M";
            this.mlblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlblMid
            // 
            this.mlblMid.AutoSize = true;
            this.mlblMid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblMid.Location = new System.Drawing.Point(195, 133);
            this.mlblMid.Name = "mlblMid";
            this.mlblMid.Size = new System.Drawing.Size(18, 15);
            this.mlblMid.TabIndex = 8;
            this.mlblMid.Text = "M";
            this.mlblMid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "High:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mid:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 47;
            this.label14.Text = "Select Colors";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(266, 242);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(76, 23);
            this.mbtnCancel.TabIndex = 51;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(77, 242);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(76, 23);
            this.mbtnOK.TabIndex = 50;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(171, 242);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 52;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 221);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(383, 15);
            this.niceLine2.TabIndex = 49;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(383, 15);
            this.niceLine1.TabIndex = 48;
            // 
            // frmColorPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(413, 277);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmColorPalette";
            this.ShowInTaskbar = false;
            this.Text = "Select Color Palette";
            this.Load += new System.EventHandler(this.frmColorPalette_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton mrBtnBlackBody;
        private System.Windows.Forms.Button mbtnLow;
        private System.Windows.Forms.RadioButton mrbtnCustom;
        private System.Windows.Forms.Button mbtnHigh;
        private System.Windows.Forms.RadioButton mrbtnHeat;
        private System.Windows.Forms.RadioButton mrbtnRedGreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mbtnMid;
        private System.Windows.Forms.Label mlblLow;
        private System.Windows.Forms.Label mlblHigh;
        private System.Windows.Forms.Label mlblMid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.RadioButton mrBtnBWR;
        private System.Windows.Forms.Button mbtnDefaults;
    }
}