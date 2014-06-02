namespace DAnTE.Inferno
{
    partial class frmBoxPlotPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBoxPlotPar));
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnColor = new System.Windows.Forms.Button();
            this.mbtnToggle = new System.Windows.Forms.Button();
            this.hexColorDialog = new System.Windows.Forms.ColorDialog();
            this.mlblColor = new System.Windows.Forms.Label();
            this.mchkBoxOutl = new System.Windows.Forms.CheckBox();
            this.mNumUDwidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mnumUDFontSc = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.mchkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.mchkBoxStamp = new System.Windows.Forms.CheckBox();
            this.mchkBoxCount = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mNumUDwidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnumUDFontSc)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(81, 531);
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
            this.mbtnCancel.Location = new System.Drawing.Point(291, 531);
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
            this.mlstViewDataSets.Location = new System.Drawing.Point(43, 312);
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(382, 163);
            this.mlstViewDataSets.TabIndex = 15;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.List;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Color:";
            // 
            // mbtnColor
            // 
            this.mbtnColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnColor.Location = new System.Drawing.Point(108, 106);
            this.mbtnColor.Name = "mbtnColor";
            this.mbtnColor.Size = new System.Drawing.Size(24, 19);
            this.mbtnColor.TabIndex = 22;
            this.mbtnColor.Text = "...";
            this.mbtnColor.UseVisualStyleBackColor = true;
            this.mbtnColor.Click += new System.EventHandler(this.mbtnColor_Click);
            // 
            // mbtnToggle
            // 
            this.mbtnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnToggle.Location = new System.Drawing.Point(359, 481);
            this.mbtnToggle.Name = "mbtnToggle";
            this.mbtnToggle.Size = new System.Drawing.Size(65, 23);
            this.mbtnToggle.TabIndex = 24;
            this.mbtnToggle.Text = "Toggle All";
            this.mbtnToggle.UseVisualStyleBackColor = true;
            this.mbtnToggle.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // mlblColor
            // 
            this.mlblColor.AutoSize = true;
            this.mlblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblColor.Location = new System.Drawing.Point(80, 108);
            this.mlblColor.Name = "mlblColor";
            this.mlblColor.Size = new System.Drawing.Size(22, 15);
            this.mlblColor.TabIndex = 25;
            this.mlblColor.Text = "FC";
            // 
            // mchkBoxOutl
            // 
            this.mchkBoxOutl.AutoSize = true;
            this.mchkBoxOutl.Checked = true;
            this.mchkBoxOutl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxOutl.Location = new System.Drawing.Point(43, 209);
            this.mchkBoxOutl.Name = "mchkBoxOutl";
            this.mchkBoxOutl.Size = new System.Drawing.Size(91, 17);
            this.mchkBoxOutl.TabIndex = 26;
            this.mchkBoxOutl.Text = "Show Outliers";
            this.mchkBoxOutl.UseVisualStyleBackColor = true;
            // 
            // mNumUDwidth
            // 
            this.mNumUDwidth.DecimalPlaces = 1;
            this.mNumUDwidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.mNumUDwidth.Location = new System.Drawing.Point(116, 142);
            this.mNumUDwidth.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mNumUDwidth.Name = "mNumUDwidth";
            this.mNumUDwidth.Size = new System.Drawing.Size(67, 20);
            this.mNumUDwidth.TabIndex = 27;
            this.mNumUDwidth.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Box width:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "(0.0 - 1.0)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(189, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "(0.0 - 1.0)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Font scaling:";
            // 
            // mnumUDFontSc
            // 
            this.mnumUDFontSc.DecimalPlaces = 1;
            this.mnumUDFontSc.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.mnumUDFontSc.Location = new System.Drawing.Point(116, 171);
            this.mnumUDFontSc.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mnumUDFontSc.Name = "mnumUDFontSc";
            this.mnumUDFontSc.Size = new System.Drawing.Size(67, 20);
            this.mnumUDFontSc.TabIndex = 30;
            this.mnumUDFontSc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "(Only works with PNG format)";
            // 
            // mchkBoxTransparent
            // 
            this.mchkBoxTransparent.AutoSize = true;
            this.mchkBoxTransparent.Location = new System.Drawing.Point(43, 256);
            this.mchkBoxTransparent.Name = "mchkBoxTransparent";
            this.mchkBoxTransparent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxTransparent.Size = new System.Drawing.Size(144, 17);
            this.mchkBoxTransparent.TabIndex = 39;
            this.mchkBoxTransparent.Text = "Transparent Background";
            this.mchkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Box Plots";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Data Source:";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(116, 49);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 48;
            this.mlblDataName.Text = "label8";
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(291, 105);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(124, 21);
            this.mcmbBoxFactors.TabIndex = 50;
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(185, 108);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(106, 13);
            this.mlblPickFactor.TabIndex = 49;
            this.mlblPickFactor.Text = "Based on the Factor:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(145, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "-OR-";
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(9, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(428, 15);
            this.niceLine4.TabIndex = 47;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(13, 510);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(424, 15);
            this.niceLine3.TabIndex = 43;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.niceLine2.Location = new System.Drawing.Point(12, 291);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(425, 15);
            this.niceLine2.TabIndex = 42;
            // 
            // niceLine1
            // 
            this.niceLine1.Caption = "Plot Properties";
            this.niceLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.niceLine1.Location = new System.Drawing.Point(12, 78);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(425, 15);
            this.niceLine1.TabIndex = 41;
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(188, 531);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 52;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // mchkBoxStamp
            // 
            this.mchkBoxStamp.AutoSize = true;
            this.mchkBoxStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxStamp.Location = new System.Drawing.Point(43, 486);
            this.mchkBoxStamp.Name = "mchkBoxStamp";
            this.mchkBoxStamp.Size = new System.Drawing.Size(120, 16);
            this.mchkBoxStamp.TabIndex = 60;
            this.mchkBoxStamp.Text = "Add Date/Name Stamp";
            this.mchkBoxStamp.UseVisualStyleBackColor = true;
            // 
            // mchkBoxCount
            // 
            this.mchkBoxCount.AutoSize = true;
            this.mchkBoxCount.Location = new System.Drawing.Point(43, 232);
            this.mchkBoxCount.Name = "mchkBoxCount";
            this.mchkBoxCount.Size = new System.Drawing.Size(240, 17);
            this.mchkBoxCount.TabIndex = 61;
            this.mchkBoxCount.Text = "Show Number of Points in the Dataset at Top";
            this.mchkBoxCount.UseVisualStyleBackColor = true;
            // 
            // frmBoxPlotPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(449, 570);
            this.Controls.Add(this.mchkBoxCount);
            this.Controls.Add(this.mchkBoxStamp);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mcmbBoxFactors);
            this.Controls.Add(this.mlblPickFactor);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.mchkBoxTransparent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mnumUDFontSc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mNumUDwidth);
            this.Controls.Add(this.mchkBoxOutl);
            this.Controls.Add(this.mlblColor);
            this.Controls.Add(this.mbtnToggle);
            this.Controls.Add(this.mbtnColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mlstViewDataSets);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBoxPlotPar";
            this.ShowInTaskbar = false;
            this.Text = "Select Box Plot Parameters";
            this.Load += new System.EventHandler(this.FormLoad_event);
            ((System.ComponentModel.ISupportInitialize)(this.mNumUDwidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mnumUDFontSc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.ListView mlstViewDataSets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mbtnColor;
        private System.Windows.Forms.Button mbtnToggle;
        private System.Windows.Forms.ColorDialog hexColorDialog;
        private System.Windows.Forms.Label mlblColor;
        private System.Windows.Forms.CheckBox mchkBoxOutl;
        private System.Windows.Forms.NumericUpDown mNumUDwidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown mnumUDFontSc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox mchkBoxTransparent;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private ExtraControls.NiceLine niceLine3;
        private ExtraControls.NiceLine niceLine4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.CheckBox mchkBoxStamp;
        private System.Windows.Forms.CheckBox mchkBoxCount;
    }
}