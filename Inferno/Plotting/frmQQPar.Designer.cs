namespace DAnTE.Inferno
{
    partial class frmQQPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQQPar));
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
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.mlblLC = new System.Windows.Forms.Label();
            this.mbtnLineC = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtxtBoxExp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.mtxtBoxDf = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.mtxtBoxScale = new System.Windows.Forms.TextBox();
            this.mtxtBoxShape = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mrBtnWeibull = new System.Windows.Forms.RadioButton();
            this.mrBtnStudent = new System.Windows.Forms.RadioButton();
            this.mrBtnExp = new System.Windows.Forms.RadioButton();
            this.mrBtnNormal = new System.Windows.Forms.RadioButton();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxtPlotCols
            // 
            this.mtxtPlotCols.Location = new System.Drawing.Point(169, 101);
            this.mtxtPlotCols.Name = "mtxtPlotCols";
            this.mtxtPlotCols.Size = new System.Drawing.Size(31, 20);
            this.mtxtPlotCols.TabIndex = 2;
            this.mtxtPlotCols.Text = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Columns on the Multi-Plot:";
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(145, 412);
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
            this.mbtnCancel.Location = new System.Drawing.Point(359, 412);
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
            this.mlstViewDataSets.Location = new System.Drawing.Point(344, 104);
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(210, 257);
            this.mlstViewDataSets.TabIndex = 15;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.List;
            this.mlstViewDataSets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.mlstViewDataSets_ItemChecked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Symbol Foreground Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Symbol Border Color:";
            // 
            // mbtnForeC
            // 
            this.mbtnForeC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnForeC.Location = new System.Drawing.Point(202, 123);
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
            this.mbtnBorderC.Location = new System.Drawing.Point(202, 145);
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
            this.mbtnToggle.Location = new System.Drawing.Point(489, 367);
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
            this.mlblFC.Location = new System.Drawing.Point(169, 127);
            this.mlblFC.Name = "mlblFC";
            this.mlblFC.Size = new System.Drawing.Size(22, 15);
            this.mlblFC.TabIndex = 25;
            this.mlblFC.Text = "FC";
            // 
            // mlblBC
            // 
            this.mlblBC.AutoSize = true;
            this.mlblBC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblBC.Location = new System.Drawing.Point(169, 149);
            this.mlblBC.Name = "mlblBC";
            this.mlblBC.Size = new System.Drawing.Size(23, 15);
            this.mlblBC.TabIndex = 26;
            this.mlblBC.Text = "BC";
            // 
            // mchkBoxTransparent
            // 
            this.mchkBoxTransparent.AutoSize = true;
            this.mchkBoxTransparent.Location = new System.Drawing.Point(35, 195);
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
            this.label6.Location = new System.Drawing.Point(184, 196);
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
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "QQ Plots";
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
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(254, 412);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 54;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // mlblLC
            // 
            this.mlblLC.AutoSize = true;
            this.mlblLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblLC.Location = new System.Drawing.Point(170, 172);
            this.mlblLC.Name = "mlblLC";
            this.mlblLC.Size = new System.Drawing.Size(22, 15);
            this.mlblLC.TabIndex = 57;
            this.mlblLC.Text = "LC";
            // 
            // mbtnLineC
            // 
            this.mbtnLineC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnLineC.Location = new System.Drawing.Point(202, 168);
            this.mbtnLineC.Name = "mbtnLineC";
            this.mbtnLineC.Size = new System.Drawing.Size(24, 20);
            this.mbtnLineC.TabIndex = 56;
            this.mbtnLineC.Text = "...";
            this.mbtnLineC.UseVisualStyleBackColor = true;
            this.mbtnLineC.Click += new System.EventHandler(this.mbtnLineC_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(107, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "Line Color:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mtxtBoxExp);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.mtxtBoxDf);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.mtxtBoxScale);
            this.groupBox1.Controls.Add(this.mtxtBoxShape);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.mrBtnWeibull);
            this.groupBox1.Controls.Add(this.mrBtnStudent);
            this.groupBox1.Controls.Add(this.mrBtnExp);
            this.groupBox1.Controls.Add(this.mrBtnNormal);
            this.groupBox1.Location = new System.Drawing.Point(15, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 143);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reference Distribution";
            // 
            // mtxtBoxExp
            // 
            this.mtxtBoxExp.Location = new System.Drawing.Point(142, 52);
            this.mtxtBoxExp.Name = "mtxtBoxExp";
            this.mtxtBoxExp.Size = new System.Drawing.Size(46, 20);
            this.mtxtBoxExp.TabIndex = 11;
            this.mtxtBoxExp.Text = "4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "rate:";
            // 
            // mtxtBoxDf
            // 
            this.mtxtBoxDf.Location = new System.Drawing.Point(142, 76);
            this.mtxtBoxDf.Name = "mtxtBoxDf";
            this.mtxtBoxDf.Size = new System.Drawing.Size(46, 20);
            this.mtxtBoxDf.TabIndex = 9;
            this.mtxtBoxDf.Text = "4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(124, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "df:";
            // 
            // mtxtBoxScale
            // 
            this.mtxtBoxScale.Location = new System.Drawing.Point(239, 101);
            this.mtxtBoxScale.Name = "mtxtBoxScale";
            this.mtxtBoxScale.Size = new System.Drawing.Size(46, 20);
            this.mtxtBoxScale.TabIndex = 7;
            this.mtxtBoxScale.Text = "1";
            // 
            // mtxtBoxShape
            // 
            this.mtxtBoxShape.Location = new System.Drawing.Point(142, 101);
            this.mtxtBoxShape.Name = "mtxtBoxShape";
            this.mtxtBoxShape.Size = new System.Drawing.Size(46, 20);
            this.mtxtBoxShape.TabIndex = 6;
            this.mtxtBoxShape.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(198, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Scale:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Shape:";
            // 
            // mrBtnWeibull
            // 
            this.mrBtnWeibull.AutoSize = true;
            this.mrBtnWeibull.Location = new System.Drawing.Point(31, 102);
            this.mrBtnWeibull.Name = "mrBtnWeibull";
            this.mrBtnWeibull.Size = new System.Drawing.Size(60, 17);
            this.mrBtnWeibull.TabIndex = 3;
            this.mrBtnWeibull.Text = "Weibull";
            this.mrBtnWeibull.UseVisualStyleBackColor = true;
            this.mrBtnWeibull.CheckedChanged += new System.EventHandler(this.mrBtnWeibull_CheckedChanged);
            // 
            // mrBtnStudent
            // 
            this.mrBtnStudent.AutoSize = true;
            this.mrBtnStudent.Location = new System.Drawing.Point(31, 79);
            this.mrBtnStudent.Name = "mrBtnStudent";
            this.mrBtnStudent.Size = new System.Drawing.Size(62, 17);
            this.mrBtnStudent.TabIndex = 2;
            this.mrBtnStudent.Text = "Student";
            this.mrBtnStudent.UseVisualStyleBackColor = true;
            this.mrBtnStudent.CheckedChanged += new System.EventHandler(this.mrBtnStudent_CheckedChanged);
            // 
            // mrBtnExp
            // 
            this.mrBtnExp.AutoSize = true;
            this.mrBtnExp.Location = new System.Drawing.Point(31, 54);
            this.mrBtnExp.Name = "mrBtnExp";
            this.mrBtnExp.Size = new System.Drawing.Size(80, 17);
            this.mrBtnExp.TabIndex = 1;
            this.mrBtnExp.Text = "Exponential";
            this.mrBtnExp.UseVisualStyleBackColor = true;
            this.mrBtnExp.CheckedChanged += new System.EventHandler(this.mrBtnExp_CheckedChanged);
            // 
            // mrBtnNormal
            // 
            this.mrBtnNormal.AutoSize = true;
            this.mrBtnNormal.Checked = true;
            this.mrBtnNormal.Location = new System.Drawing.Point(31, 31);
            this.mrBtnNormal.Name = "mrBtnNormal";
            this.mrBtnNormal.Size = new System.Drawing.Size(58, 17);
            this.mrBtnNormal.TabIndex = 0;
            this.mrBtnNormal.TabStop = true;
            this.mrBtnNormal.Text = "Normal";
            this.mrBtnNormal.UseVisualStyleBackColor = true;
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(12, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(560, 15);
            this.niceLine4.TabIndex = 51;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(15, 391);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(557, 15);
            this.niceLine3.TabIndex = 31;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Location = new System.Drawing.Point(344, 76);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(228, 15);
            this.niceLine2.TabIndex = 30;
            // 
            // niceLine1
            // 
            this.niceLine1.Caption = "Plot Properties";
            this.niceLine1.Location = new System.Drawing.Point(12, 76);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(317, 15);
            this.niceLine1.TabIndex = 29;
            // 
            // frmQQPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(586, 446);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mlblLC);
            this.Controls.Add(this.mbtnLineC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.mbtnDefaults);
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
            this.Name = "frmQQPar";
            this.ShowInTaskbar = false;
            this.Text = "Select QQ Plot Parameters";
            this.Load += new System.EventHandler(this.FormLoad_event);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.Label mlblLC;
        private System.Windows.Forms.Button mbtnLineC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox mtxtBoxScale;
        private System.Windows.Forms.TextBox mtxtBoxShape;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton mrBtnWeibull;
        private System.Windows.Forms.RadioButton mrBtnStudent;
        private System.Windows.Forms.RadioButton mrBtnExp;
        private System.Windows.Forms.RadioButton mrBtnNormal;
        private System.Windows.Forms.TextBox mtxtBoxDf;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox mtxtBoxExp;
        private System.Windows.Forms.Label label11;
    }
}