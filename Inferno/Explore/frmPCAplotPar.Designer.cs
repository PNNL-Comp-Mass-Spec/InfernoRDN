namespace DAnTE.Inferno
{
    partial class frmPCAplotPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPCAplotPar));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrbtn3D = new System.Windows.Forms.RadioButton();
            this.mrbtn2D = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mcmbBoxZ = new System.Windows.Forms.ComboBox();
            this.mcmbBoxY = new System.Windows.Forms.ComboBox();
            this.mcmbBoxX = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.mchkBoxDropLines = new System.Windows.Forms.CheckBox();
            this.mchkBoxPersp = new System.Windows.Forms.CheckBox();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.mchkBoxBiPlot = new System.Windows.Forms.CheckBox();
            this.mchkBoxLabels = new System.Windows.Forms.CheckBox();
            this.mchkBoxScree = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mchkBoxBiLabels = new System.Windows.Forms.CheckBox();
            this.mchkBoxBiLines = new System.Windows.Forms.CheckBox();
            this.mrBtnPCA = new System.Windows.Forms.RadioButton();
            this.mrBtnPLS = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mchkBoxStamp = new System.Windows.Forms.CheckBox();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.mbtnToggle = new System.Windows.Forms.Button();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Parameters for:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mrbtn3D);
            this.groupBox1.Controls.Add(this.mrbtn2D);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(26, 160);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 206);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plot Dimensions";
            // 
            // mrbtn3D
            // 
            this.mrbtn3D.AutoSize = true;
            this.mrbtn3D.Location = new System.Drawing.Point(21, 66);
            this.mrbtn3D.Name = "mrbtn3D";
            this.mrbtn3D.Size = new System.Drawing.Size(39, 17);
            this.mrbtn3D.TabIndex = 31;
            this.mrbtn3D.Text = "3D";
            this.mrbtn3D.UseVisualStyleBackColor = true;
            this.mrbtn3D.CheckedChanged += new System.EventHandler(this.mrbtn3D_CheckedChanged);
            // 
            // mrbtn2D
            // 
            this.mrbtn2D.AutoSize = true;
            this.mrbtn2D.Checked = true;
            this.mrbtn2D.Location = new System.Drawing.Point(21, 30);
            this.mrbtn2D.Name = "mrbtn2D";
            this.mrbtn2D.Size = new System.Drawing.Size(39, 17);
            this.mrbtn2D.TabIndex = 30;
            this.mrbtn2D.TabStop = true;
            this.mrbtn2D.Text = "2D";
            this.mrbtn2D.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mcmbBoxZ);
            this.groupBox2.Controls.Add(this.mcmbBoxY);
            this.groupBox2.Controls.Add(this.mcmbBoxX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(75, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 147);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Axes";
            // 
            // mcmbBoxZ
            // 
            this.mcmbBoxZ.Enabled = false;
            this.mcmbBoxZ.FormattingEnabled = true;
            this.mcmbBoxZ.Location = new System.Drawing.Point(51, 107);
            this.mcmbBoxZ.Name = "mcmbBoxZ";
            this.mcmbBoxZ.Size = new System.Drawing.Size(83, 21);
            this.mcmbBoxZ.TabIndex = 34;
            // 
            // mcmbBoxY
            // 
            this.mcmbBoxY.FormattingEnabled = true;
            this.mcmbBoxY.Location = new System.Drawing.Point(51, 72);
            this.mcmbBoxY.Name = "mcmbBoxY";
            this.mcmbBoxY.Size = new System.Drawing.Size(83, 21);
            this.mcmbBoxY.TabIndex = 33;
            // 
            // mcmbBoxX
            // 
            this.mcmbBoxX.FormattingEnabled = true;
            this.mcmbBoxX.Location = new System.Drawing.Point(51, 36);
            this.mcmbBoxX.Name = "mcmbBoxX";
            this.mcmbBoxX.Size = new System.Drawing.Size(83, 21);
            this.mcmbBoxX.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Z Axis:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Y Axis:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "X Axis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Data source:";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(260, 587);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 31;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(137, 587);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 30;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(211, 127);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(124, 21);
            this.mcmbBoxFactors.TabIndex = 33;
            this.mcmbBoxFactors.SelectedIndexChanged += new System.EventHandler(this.mcmbBoxFactors_SelectedIndexChanged);
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(120, 130);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(82, 13);
            this.mlblPickFactor.TabIndex = 32;
            this.mlblPickFactor.Text = "Pick the Factor:";
            // 
            // mchkBoxDropLines
            // 
            this.mchkBoxDropLines.AutoSize = true;
            this.mchkBoxDropLines.Checked = true;
            this.mchkBoxDropLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxDropLines.Enabled = false;
            this.mchkBoxDropLines.Location = new System.Drawing.Point(319, 235);
            this.mchkBoxDropLines.Name = "mchkBoxDropLines";
            this.mchkBoxDropLines.Size = new System.Drawing.Size(77, 17);
            this.mchkBoxDropLines.TabIndex = 34;
            this.mchkBoxDropLines.Text = "Drop Lines";
            this.mchkBoxDropLines.UseVisualStyleBackColor = true;
            // 
            // mchkBoxPersp
            // 
            this.mchkBoxPersp.AutoSize = true;
            this.mchkBoxPersp.Checked = true;
            this.mchkBoxPersp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxPersp.Enabled = false;
            this.mchkBoxPersp.Location = new System.Drawing.Point(319, 212);
            this.mchkBoxPersp.Name = "mchkBoxPersp";
            this.mchkBoxPersp.Size = new System.Drawing.Size(108, 17);
            this.mchkBoxPersp.TabIndex = 36;
            this.mchkBoxPersp.Text = "Perspective View";
            this.mchkBoxPersp.UseVisualStyleBackColor = true;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(208, 101);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 53;
            this.mlblDataName.Text = "label8";
            // 
            // mchkBoxBiPlot
            // 
            this.mchkBoxBiPlot.AutoSize = true;
            this.mchkBoxBiPlot.Location = new System.Drawing.Point(9, 12);
            this.mchkBoxBiPlot.Name = "mchkBoxBiPlot";
            this.mchkBoxBiPlot.Size = new System.Drawing.Size(84, 17);
            this.mchkBoxBiPlot.TabIndex = 54;
            this.mchkBoxBiPlot.Text = "Draw Bi-Plot";
            this.mchkBoxBiPlot.UseVisualStyleBackColor = true;
            this.mchkBoxBiPlot.CheckedChanged += new System.EventHandler(this.mchkBoxBiPlot_CheckedChanged);
            // 
            // mchkBoxLabels
            // 
            this.mchkBoxLabels.AutoSize = true;
            this.mchkBoxLabels.Checked = true;
            this.mchkBoxLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxLabels.Location = new System.Drawing.Point(319, 166);
            this.mchkBoxLabels.Name = "mchkBoxLabels";
            this.mchkBoxLabels.Size = new System.Drawing.Size(87, 17);
            this.mchkBoxLabels.TabIndex = 55;
            this.mchkBoxLabels.Text = "Show Labels";
            this.mchkBoxLabels.UseVisualStyleBackColor = true;
            // 
            // mchkBoxScree
            // 
            this.mchkBoxScree.AutoSize = true;
            this.mchkBoxScree.Location = new System.Drawing.Point(319, 189);
            this.mchkBoxScree.Name = "mchkBoxScree";
            this.mchkBoxScree.Size = new System.Drawing.Size(101, 17);
            this.mchkBoxScree.TabIndex = 56;
            this.mchkBoxScree.Text = "Show Screeplot";
            this.mchkBoxScree.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.mchkBoxBiLabels);
            this.panel1.Controls.Add(this.mchkBoxBiLines);
            this.panel1.Controls.Add(this.mchkBoxBiPlot);
            this.panel1.Location = new System.Drawing.Point(310, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(137, 105);
            this.panel1.TabIndex = 57;
            // 
            // mchkBoxBiLabels
            // 
            this.mchkBoxBiLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxBiLabels.Location = new System.Drawing.Point(26, 63);
            this.mchkBoxBiLabels.Name = "mchkBoxBiLabels";
            this.mchkBoxBiLabels.Size = new System.Drawing.Size(101, 33);
            this.mchkBoxBiLabels.TabIndex = 59;
            this.mchkBoxBiLabels.Text = "Show Feature Labels";
            this.mchkBoxBiLabels.UseVisualStyleBackColor = true;
            // 
            // mchkBoxBiLines
            // 
            this.mchkBoxBiLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxBiLines.Location = new System.Drawing.Point(26, 33);
            this.mchkBoxBiLines.Name = "mchkBoxBiLines";
            this.mchkBoxBiLines.Size = new System.Drawing.Size(101, 29);
            this.mchkBoxBiLines.TabIndex = 58;
            this.mchkBoxBiLines.Text = "Show Lines From Origin";
            this.mchkBoxBiLines.UseVisualStyleBackColor = true;
            // 
            // mrBtnPCA
            // 
            this.mrBtnPCA.AutoSize = true;
            this.mrBtnPCA.Checked = true;
            this.mrBtnPCA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrBtnPCA.Location = new System.Drawing.Point(3, 3);
            this.mrBtnPCA.Name = "mrBtnPCA";
            this.mrBtnPCA.Size = new System.Drawing.Size(227, 17);
            this.mrBtnPCA.TabIndex = 58;
            this.mrBtnPCA.TabStop = true;
            this.mrBtnPCA.Text = "Principal Component Analysis (PCA)";
            this.mrBtnPCA.UseVisualStyleBackColor = true;
            // 
            // mrBtnPLS
            // 
            this.mrBtnPLS.AutoSize = true;
            this.mrBtnPLS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrBtnPLS.Location = new System.Drawing.Point(3, 26);
            this.mrBtnPLS.Name = "mrBtnPLS";
            this.mrBtnPLS.Size = new System.Drawing.Size(181, 17);
            this.mrBtnPLS.TabIndex = 59;
            this.mrBtnPLS.Text = "Partial Least Squares (PLS)";
            this.mrBtnPLS.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mrBtnPCA);
            this.panel2.Controls.Add(this.mrBtnPLS);
            this.panel2.Location = new System.Drawing.Point(123, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 52);
            this.panel2.TabIndex = 60;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(15, 566);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(438, 15);
            this.niceLine3.TabIndex = 61;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(15, 74);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(436, 15);
            this.niceLine1.TabIndex = 1;
            // 
            // mchkBoxStamp
            // 
            this.mchkBoxStamp.AutoSize = true;
            this.mchkBoxStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxStamp.Location = new System.Drawing.Point(26, 544);
            this.mchkBoxStamp.Name = "mchkBoxStamp";
            this.mchkBoxStamp.Size = new System.Drawing.Size(120, 16);
            this.mchkBoxStamp.TabIndex = 62;
            this.mchkBoxStamp.Text = "Add Date/Name Stamp";
            this.mchkBoxStamp.UseVisualStyleBackColor = true;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Location = new System.Drawing.Point(15, 372);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(436, 15);
            this.niceLine2.TabIndex = 65;
            // 
            // mbtnToggle
            // 
            this.mbtnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnToggle.Location = new System.Drawing.Point(382, 539);
            this.mbtnToggle.Name = "mbtnToggle";
            this.mbtnToggle.Size = new System.Drawing.Size(65, 23);
            this.mbtnToggle.TabIndex = 64;
            this.mbtnToggle.Text = "Toggle All";
            this.mbtnToggle.UseVisualStyleBackColor = true;
            this.mbtnToggle.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // mlstViewDataSets
            // 
            this.mlstViewDataSets.CheckBoxes = true;
            this.mlstViewDataSets.Location = new System.Drawing.Point(26, 393);
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(421, 140);
            this.mlstViewDataSets.TabIndex = 63;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.List;
            // 
            // frmPCAplotPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 624);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mbtnToggle);
            this.Controls.Add(this.mlstViewDataSets);
            this.Controls.Add(this.mchkBoxStamp);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.mchkBoxScree);
            this.Controls.Add(this.mchkBoxLabels);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.mchkBoxPersp);
            this.Controls.Add(this.mchkBoxDropLines);
            this.Controls.Add(this.mcmbBoxFactors);
            this.Controls.Add(this.mlblPickFactor);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPCAplotPar";
            this.ShowInTaskbar = false;
            this.Text = "PCA/PLS Plot";
            this.Load += new System.EventHandler(this.frmPCAplotPar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mrbtn3D;
        private System.Windows.Forms.RadioButton mrbtn2D;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox mcmbBoxX;
        private System.Windows.Forms.ComboBox mcmbBoxZ;
        private System.Windows.Forms.ComboBox mcmbBoxY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.CheckBox mchkBoxDropLines;
        private System.Windows.Forms.CheckBox mchkBoxPersp;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.CheckBox mchkBoxBiPlot;
        private System.Windows.Forms.CheckBox mchkBoxLabels;
        private System.Windows.Forms.CheckBox mchkBoxScree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox mchkBoxBiLabels;
        private System.Windows.Forms.CheckBox mchkBoxBiLines;
        private System.Windows.Forms.RadioButton mrBtnPCA;
        private System.Windows.Forms.RadioButton mrBtnPLS;
        private System.Windows.Forms.Panel panel2;
        private DAnTE.ExtraControls.NiceLine niceLine3;
        private System.Windows.Forms.CheckBox mchkBoxStamp;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnToggle;
        private System.Windows.Forms.ListView mlstViewDataSets;
    }
}