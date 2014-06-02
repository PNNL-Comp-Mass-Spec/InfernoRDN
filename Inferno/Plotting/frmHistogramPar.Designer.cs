namespace DAnTE.Inferno
{
    partial class frmHistogramPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistogramPar));
            this.mtxtPlotCols = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnForeC = new System.Windows.Forms.Button();
            this.mbtnBorderC = new System.Windows.Forms.Button();
            this.mbtnToggle = new System.Windows.Forms.Button();
            this.hexColorDialog = new System.Windows.Forms.ColorDialog();
            this.mlblFC = new System.Windows.Forms.Label();
            this.mlblBC = new System.Windows.Forms.Label();
            this.mchkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mchkBoxRug = new System.Windows.Forms.CheckBox();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.mtxtBoxBins = new System.Windows.Forms.TextBox();
            this.mchkBoxAutoBin = new System.Windows.Forms.CheckBox();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mchkBoxStamp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // mtxtPlotCols
            // 
            this.mtxtPlotCols.Location = new System.Drawing.Point(169, 130);
            this.mtxtPlotCols.Name = "mtxtPlotCols";
            this.mtxtPlotCols.Size = new System.Drawing.Size(31, 20);
            this.mtxtPlotCols.TabIndex = 2;
            this.mtxtPlotCols.Text = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Columns on the Multi-Plot:";
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(81, 493);
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
            this.mbtnCancel.Location = new System.Drawing.Point(295, 493);
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
            this.mlstViewDataSets.Location = new System.Drawing.Point(40, 274);
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
            this.label3.Location = new System.Drawing.Point(72, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Foreground Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Border Color:";
            // 
            // mbtnForeC
            // 
            this.mbtnForeC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnForeC.Location = new System.Drawing.Point(202, 154);
            this.mbtnForeC.Name = "mbtnForeC";
            this.mbtnForeC.Size = new System.Drawing.Size(24, 20);
            this.mbtnForeC.TabIndex = 22;
            this.mbtnForeC.Text = "...";
            this.mbtnForeC.UseVisualStyleBackColor = true;
            this.mbtnForeC.Click += new System.EventHandler(this.mbtnForeC_Click);
            // 
            // mbtnBorderC
            // 
            this.mbtnBorderC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnBorderC.Location = new System.Drawing.Point(369, 155);
            this.mbtnBorderC.Name = "mbtnBorderC";
            this.mbtnBorderC.Size = new System.Drawing.Size(24, 20);
            this.mbtnBorderC.TabIndex = 23;
            this.mbtnBorderC.Text = "...";
            this.mbtnBorderC.UseVisualStyleBackColor = true;
            this.mbtnBorderC.Click += new System.EventHandler(this.mbtnBorderC_Click);
            // 
            // mbtnToggle
            // 
            this.mbtnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnToggle.Location = new System.Drawing.Point(356, 443);
            this.mbtnToggle.Name = "mbtnToggle";
            this.mbtnToggle.Size = new System.Drawing.Size(65, 23);
            this.mbtnToggle.TabIndex = 24;
            this.mbtnToggle.Text = "Toggle All";
            this.mbtnToggle.UseVisualStyleBackColor = true;
            this.mbtnToggle.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // mlblFC
            // 
            this.mlblFC.AutoSize = true;
            this.mlblFC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblFC.Location = new System.Drawing.Point(169, 156);
            this.mlblFC.Name = "mlblFC";
            this.mlblFC.Size = new System.Drawing.Size(22, 15);
            this.mlblFC.TabIndex = 25;
            this.mlblFC.Text = "FC";
            // 
            // mlblBC
            // 
            this.mlblBC.AutoSize = true;
            this.mlblBC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblBC.Location = new System.Drawing.Point(336, 155);
            this.mlblBC.Name = "mlblBC";
            this.mlblBC.Size = new System.Drawing.Size(23, 15);
            this.mlblBC.TabIndex = 26;
            this.mlblBC.Text = "BC";
            // 
            // mchkBoxTransparent
            // 
            this.mchkBoxTransparent.AutoSize = true;
            this.mchkBoxTransparent.Location = new System.Drawing.Point(34, 185);
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
            this.label6.Location = new System.Drawing.Point(199, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "(Only works with PNG format)";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(119, 49);
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
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Histograms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Data Source:";
            // 
            // mchkBoxRug
            // 
            this.mchkBoxRug.AutoSize = true;
            this.mchkBoxRug.Checked = true;
            this.mchkBoxRug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxRug.Location = new System.Drawing.Point(110, 212);
            this.mchkBoxRug.Name = "mchkBoxRug";
            this.mchkBoxRug.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mchkBoxRug.Size = new System.Drawing.Size(71, 17);
            this.mchkBoxRug.TabIndex = 53;
            this.mchkBoxRug.Text = "Add Rug:";
            this.mchkBoxRug.UseVisualStyleBackColor = true;
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(190, 493);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 54;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Manually set the Bins:";
            // 
            // mtxtBoxBins
            // 
            this.mtxtBoxBins.Enabled = false;
            this.mtxtBoxBins.Location = new System.Drawing.Point(169, 101);
            this.mtxtBoxBins.Name = "mtxtBoxBins";
            this.mtxtBoxBins.Size = new System.Drawing.Size(31, 20);
            this.mtxtBoxBins.TabIndex = 56;
            this.mtxtBoxBins.Text = "10";
            // 
            // mchkBoxAutoBin
            // 
            this.mchkBoxAutoBin.AutoSize = true;
            this.mchkBoxAutoBin.Checked = true;
            this.mchkBoxAutoBin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxAutoBin.Location = new System.Drawing.Point(216, 104);
            this.mchkBoxAutoBin.Name = "mchkBoxAutoBin";
            this.mchkBoxAutoBin.Size = new System.Drawing.Size(86, 17);
            this.mchkBoxAutoBin.TabIndex = 57;
            this.mchkBoxAutoBin.Text = "Auto Binning";
            this.mchkBoxAutoBin.UseVisualStyleBackColor = true;
            this.mchkBoxAutoBin.CheckedChanged += new System.EventHandler(this.mchkBoxAutoBin_CheckedChanged);
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(12, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(413, 15);
            this.niceLine4.TabIndex = 51;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(14, 472);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(411, 15);
            this.niceLine3.TabIndex = 31;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Location = new System.Drawing.Point(12, 242);
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
            // mchkBoxStamp
            // 
            this.mchkBoxStamp.AutoSize = true;
            this.mchkBoxStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxStamp.Location = new System.Drawing.Point(40, 450);
            this.mchkBoxStamp.Name = "mchkBoxStamp";
            this.mchkBoxStamp.Size = new System.Drawing.Size(120, 16);
            this.mchkBoxStamp.TabIndex = 58;
            this.mchkBoxStamp.Text = "Add Date/Name Stamp";
            this.mchkBoxStamp.UseVisualStyleBackColor = true;
            // 
            // frmHistogramPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(442, 530);
            this.Controls.Add(this.mchkBoxStamp);
            this.Controls.Add(this.mchkBoxAutoBin);
            this.Controls.Add(this.mtxtBoxBins);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.mchkBoxRug);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.mchkBoxTransparent);
            this.Controls.Add(this.mlblBC);
            this.Controls.Add(this.mlblFC);
            this.Controls.Add(this.mbtnToggle);
            this.Controls.Add(this.mbtnBorderC);
            this.Controls.Add(this.mbtnForeC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mlstViewDataSets);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mtxtPlotCols);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHistogramPar";
            this.ShowInTaskbar = false;
            this.Text = "Select Histogram Plot Parameters";
            this.Load += new System.EventHandler(this.FormLoad_event);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mtxtPlotCols;
        private System.Windows.Forms.ListView mlstViewDataSets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button mbtnForeC;
        private System.Windows.Forms.Button mbtnBorderC;
        private System.Windows.Forms.Button mbtnToggle;
        private System.Windows.Forms.ColorDialog hexColorDialog;
        private System.Windows.Forms.Label mlblFC;
        private System.Windows.Forms.Label mlblBC;
        private System.Windows.Forms.CheckBox mchkBoxTransparent;
        private System.Windows.Forms.Label label6;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private ExtraControls.NiceLine niceLine3;
        private System.Windows.Forms.Label mlblDataName;
        private ExtraControls.NiceLine niceLine4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox mchkBoxRug;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox mtxtBoxBins;
        private System.Windows.Forms.CheckBox mchkBoxAutoBin;
        private System.Windows.Forms.CheckBox mchkBoxStamp;
    }
}