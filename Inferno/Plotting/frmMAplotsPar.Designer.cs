namespace DAnTE.Inferno
{
    partial class frmMAplotsPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMAplotsPar));
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnForeC = new System.Windows.Forms.Button();
            this.mbtnBorderC = new System.Windows.Forms.Button();
            this.mbtnToggle = new System.Windows.Forms.Button();
            this.hexColorDialog = new System.Windows.Forms.ColorDialog();
            this.mlblDC = new System.Windows.Forms.Label();
            this.mlblLC = new System.Windows.Forms.Label();
            this.mchkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.mchkBoxStamp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(78, 461);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(76, 23);
            this.mbtnOK.TabIndex = 7;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(287, 462);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(76, 23);
            this.mbtnCancel.TabIndex = 8;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mlstViewDataSets
            // 
            this.mlstViewDataSets.CheckBoxes = true;
            this.mlstViewDataSets.Location = new System.Drawing.Point(42, 242);
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(382, 163);
            this.mlstViewDataSets.TabIndex = 15;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.List;
            this.mlstViewDataSets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.mlstViewDataSets_ItemChecked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Data Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "LOESS Line Color:";
            // 
            // mbtnForeC
            // 
            this.mbtnForeC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnForeC.Location = new System.Drawing.Point(192, 103);
            this.mbtnForeC.Name = "mbtnForeC";
            this.mbtnForeC.Size = new System.Drawing.Size(24, 20);
            this.mbtnForeC.TabIndex = 22;
            this.mbtnForeC.Text = "...";
            this.mbtnForeC.UseVisualStyleBackColor = true;
            this.mbtnForeC.Click += new System.EventHandler(this.mbtnDataC_Click);
            // 
            // mbtnBorderC
            // 
            this.mbtnBorderC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnBorderC.Location = new System.Drawing.Point(192, 129);
            this.mbtnBorderC.Name = "mbtnBorderC";
            this.mbtnBorderC.Size = new System.Drawing.Size(24, 20);
            this.mbtnBorderC.TabIndex = 23;
            this.mbtnBorderC.Text = "...";
            this.mbtnBorderC.UseVisualStyleBackColor = true;
            this.mbtnBorderC.Click += new System.EventHandler(this.mbtnLineC_Click);
            // 
            // mbtnToggle
            // 
            this.mbtnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnToggle.Location = new System.Drawing.Point(358, 411);
            this.mbtnToggle.Name = "mbtnToggle";
            this.mbtnToggle.Size = new System.Drawing.Size(65, 23);
            this.mbtnToggle.TabIndex = 24;
            this.mbtnToggle.Text = "Toggle All";
            this.mbtnToggle.UseVisualStyleBackColor = true;
            this.mbtnToggle.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // mlblDC
            // 
            this.mlblDC.AutoSize = true;
            this.mlblDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblDC.Location = new System.Drawing.Point(164, 103);
            this.mlblDC.Name = "mlblDC";
            this.mlblDC.Size = new System.Drawing.Size(22, 15);
            this.mlblDC.TabIndex = 25;
            this.mlblDC.Text = "FC";
            // 
            // mlblLC
            // 
            this.mlblLC.AutoSize = true;
            this.mlblLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblLC.Location = new System.Drawing.Point(163, 129);
            this.mlblLC.Name = "mlblLC";
            this.mlblLC.Size = new System.Drawing.Size(23, 15);
            this.mlblLC.TabIndex = 26;
            this.mlblLC.Text = "BC";
            // 
            // mchkBoxTransparent
            // 
            this.mchkBoxTransparent.AutoSize = true;
            this.mchkBoxTransparent.Location = new System.Drawing.Point(36, 156);
            this.mchkBoxTransparent.Name = "mchkBoxTransparent";
            this.mchkBoxTransparent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mchkBoxTransparent.Size = new System.Drawing.Size(147, 17);
            this.mchkBoxTransparent.TabIndex = 27;
            this.mchkBoxTransparent.Text = "Transparent Background:";
            this.mchkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(201, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "(Only works with PNG format)";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(241, 47);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 52;
            this.mlblDataName.Text = "label8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "MA Plots";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Data Source:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "M=log2(D1/D2), A=0.5*log2(D1*D2)";
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(12, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(416, 15);
            this.niceLine4.TabIndex = 51;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(12, 440);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(411, 15);
            this.niceLine3.TabIndex = 31;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Location = new System.Drawing.Point(14, 210);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(411, 15);
            this.niceLine2.TabIndex = 30;
            // 
            // niceLine1
            // 
            this.niceLine1.Caption = "Plot Properties";
            this.niceLine1.Location = new System.Drawing.Point(12, 76);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(411, 15);
            this.niceLine1.TabIndex = 29;
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(183, 462);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 54;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // mchkBoxStamp
            // 
            this.mchkBoxStamp.AutoSize = true;
            this.mchkBoxStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxStamp.Location = new System.Drawing.Point(42, 416);
            this.mchkBoxStamp.Name = "mchkBoxStamp";
            this.mchkBoxStamp.Size = new System.Drawing.Size(120, 16);
            this.mchkBoxStamp.TabIndex = 63;
            this.mchkBoxStamp.Text = "Add Date/Name Stamp";
            this.mchkBoxStamp.UseVisualStyleBackColor = true;
            // 
            // frmMAplotsPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(443, 497);
            this.Controls.Add(this.mchkBoxStamp);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mlblDC);
            this.Controls.Add(this.mlblLC);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mchkBoxTransparent);
            this.Controls.Add(this.mbtnToggle);
            this.Controls.Add(this.mbtnBorderC);
            this.Controls.Add(this.mbtnForeC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mlstViewDataSets);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMAplotsPar";
            this.ShowInTaskbar = false;
            this.Text = "Select MA Plot Parameters";
            this.Load += new System.EventHandler(this.FormLoad_event);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.ListView mlstViewDataSets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button mbtnForeC;
        private System.Windows.Forms.Button mbtnBorderC;
        private System.Windows.Forms.Button mbtnToggle;
        private System.Windows.Forms.ColorDialog hexColorDialog;
        private System.Windows.Forms.Label mlblDC;
        private System.Windows.Forms.Label mlblLC;
        private System.Windows.Forms.CheckBox mchkBoxTransparent;
        private System.Windows.Forms.Label label6;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private ExtraControls.NiceLine niceLine3;
        private System.Windows.Forms.Label mlblDataName;
        private ExtraControls.NiceLine niceLine4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.CheckBox mchkBoxStamp;
    }
}