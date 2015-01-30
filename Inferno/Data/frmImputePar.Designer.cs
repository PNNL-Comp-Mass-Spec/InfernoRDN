namespace DAnTE.Inferno
{
    partial class frmImputePar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImputePar));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtBoxFthres = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mrBtnMean = new System.Windows.Forms.RadioButton();
            this.mrBtnMedian = new System.Windows.Forms.RadioButton();
            this.mrBtnKNN = new System.Windows.Forms.RadioButton();
            this.mrBtnWKNN = new System.Windows.Forms.RadioButton();
            this.mrBtnSVD = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.mtxtBoxK = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mtxtBoxSVDiter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtBoxSVDthres = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mtxtBoxnPCs = new System.Windows.Forms.TextBox();
            this.mtxtBoxConst = new System.Windows.Forms.TextBox();
            this.mrBtnConst = new System.Windows.Forms.RadioButton();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.mchkBoxNoImpute = new System.Windows.Forms.CheckBox();
            this.mrBtnRowMean = new System.Windows.Forms.RadioButton();
            this.mtabCImpute = new System.Windows.Forms.TabControl();
            this.Advance = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Simple = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mtabCImpute.SuspendLayout();
            this.Advance.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Simple.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 17);
            this.label4.TabIndex = 28;
            this.label4.Text = "Imputing Missing Values";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Data Source:";
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(264, 545);
            this.mbtnDefaults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(100, 28);
            this.mbtnDefaults.TabIndex = 24;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(424, 545);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 23;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(95, 545);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(100, 28);
            this.mbtnOK.TabIndex = 22;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 17);
            this.label1.TabIndex = 29;
            this.label1.Text = "Impute Threshold (%):";
            // 
            // mtxtBoxFthres
            // 
            this.mtxtBoxFthres.Location = new System.Drawing.Point(164, 114);
            this.mtxtBoxFthres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxFthres.Name = "mtxtBoxFthres";
            this.mtxtBoxFthres.Size = new System.Drawing.Size(83, 22);
            this.mtxtBoxFthres.TabIndex = 30;
            this.mtxtBoxFthres.Text = "20";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(261, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(313, 66);
            this.label2.TabIndex = 31;
            this.label2.Text = "Percentage Missing Allowed : Entries with missing values more than this threshold" +
    " will be imputed with overall mean per sample (kNN and SVD methods only) unless " +
    "specified below.";
            // 
            // mrBtnMean
            // 
            this.mrBtnMean.AutoSize = true;
            this.mrBtnMean.Location = new System.Drawing.Point(17, 16);
            this.mrBtnMean.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnMean.Name = "mrBtnMean";
            this.mrBtnMean.Size = new System.Drawing.Size(155, 21);
            this.mrBtnMean.TabIndex = 0;
            this.mrBtnMean.Tag = "mean";
            this.mrBtnMean.Text = "Mean of the Sample";
            this.mrBtnMean.UseVisualStyleBackColor = true;
            // 
            // mrBtnMedian
            // 
            this.mrBtnMedian.AutoSize = true;
            this.mrBtnMedian.Checked = true;
            this.mrBtnMedian.Location = new System.Drawing.Point(17, 80);
            this.mrBtnMedian.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnMedian.Name = "mrBtnMedian";
            this.mrBtnMedian.Size = new System.Drawing.Size(166, 21);
            this.mrBtnMedian.TabIndex = 1;
            this.mrBtnMedian.TabStop = true;
            this.mrBtnMedian.Tag = "median";
            this.mrBtnMedian.Text = "Median of the Sample";
            this.mrBtnMedian.UseVisualStyleBackColor = true;
            // 
            // mrBtnKNN
            // 
            this.mrBtnKNN.AutoSize = true;
            this.mrBtnKNN.Location = new System.Drawing.Point(4, 47);
            this.mrBtnKNN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnKNN.Name = "mrBtnKNN";
            this.mrBtnKNN.Size = new System.Drawing.Size(208, 21);
            this.mrBtnKNN.TabIndex = 2;
            this.mrBtnKNN.Tag = "knn";
            this.mrBtnKNN.Text = "k Nearest Neighbours (kNN)";
            this.mrBtnKNN.UseVisualStyleBackColor = true;
            // 
            // mrBtnWKNN
            // 
            this.mrBtnWKNN.AutoSize = true;
            this.mrBtnWKNN.Checked = true;
            this.mrBtnWKNN.Location = new System.Drawing.Point(4, 86);
            this.mrBtnWKNN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnWKNN.Name = "mrBtnWKNN";
            this.mrBtnWKNN.Size = new System.Drawing.Size(120, 21);
            this.mrBtnWKNN.TabIndex = 3;
            this.mrBtnWKNN.TabStop = true;
            this.mrBtnWKNN.Tag = "knnw";
            this.mrBtnWKNN.Text = "Weighted kNN";
            this.mrBtnWKNN.UseVisualStyleBackColor = true;
            // 
            // mrBtnSVD
            // 
            this.mrBtnSVD.AutoSize = true;
            this.mrBtnSVD.Location = new System.Drawing.Point(4, 127);
            this.mrBtnSVD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnSVD.Name = "mrBtnSVD";
            this.mrBtnSVD.Size = new System.Drawing.Size(103, 21);
            this.mrBtnSVD.TabIndex = 4;
            this.mrBtnSVD.Tag = "svd";
            this.mrBtnSVD.Text = "SVD Impute";
            this.mrBtnSVD.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "# of neighbors:";
            // 
            // mtxtBoxK
            // 
            this.mtxtBoxK.Location = new System.Drawing.Point(168, 30);
            this.mtxtBoxK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxK.Name = "mtxtBoxK";
            this.mtxtBoxK.Size = new System.Drawing.Size(71, 22);
            this.mtxtBoxK.TabIndex = 1;
            this.mtxtBoxK.Text = "10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.mtxtBoxK);
            this.groupBox1.Location = new System.Drawing.Point(288, 74);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(260, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "kNN-Impute";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mtxtBoxSVDiter);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.mtxtBoxSVDthres);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.mtxtBoxnPCs);
            this.groupBox2.Location = new System.Drawing.Point(288, 154);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(260, 135);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SVD-Impute";
            // 
            // mtxtBoxSVDiter
            // 
            this.mtxtBoxSVDiter.Location = new System.Drawing.Point(168, 95);
            this.mtxtBoxSVDiter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxSVDiter.Name = "mtxtBoxSVDiter";
            this.mtxtBoxSVDiter.Size = new System.Drawing.Size(71, 22);
            this.mtxtBoxSVDiter.TabIndex = 5;
            this.mtxtBoxSVDiter.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Maximum Iterations:";
            // 
            // mtxtBoxSVDthres
            // 
            this.mtxtBoxSVDthres.Location = new System.Drawing.Point(168, 60);
            this.mtxtBoxSVDthres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxSVDthres.Name = "mtxtBoxSVDthres";
            this.mtxtBoxSVDthres.Size = new System.Drawing.Size(71, 22);
            this.mtxtBoxSVDthres.TabIndex = 3;
            this.mtxtBoxSVDthres.Text = "0.01";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Threshold for Iterations:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "# of PCs:";
            // 
            // mtxtBoxnPCs
            // 
            this.mtxtBoxnPCs.Location = new System.Drawing.Point(168, 26);
            this.mtxtBoxnPCs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxnPCs.Name = "mtxtBoxnPCs";
            this.mtxtBoxnPCs.Size = new System.Drawing.Size(71, 22);
            this.mtxtBoxnPCs.TabIndex = 1;
            this.mtxtBoxnPCs.Text = "5";
            // 
            // mtxtBoxConst
            // 
            this.mtxtBoxConst.Location = new System.Drawing.Point(199, 149);
            this.mtxtBoxConst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxConst.Name = "mtxtBoxConst";
            this.mtxtBoxConst.Size = new System.Drawing.Size(71, 22);
            this.mtxtBoxConst.TabIndex = 2;
            this.mtxtBoxConst.Text = "1.0";
            this.mtxtBoxConst.Click += new System.EventHandler(this.mtxtBoxConst_Click);
            // 
            // mrBtnConst
            // 
            this.mrBtnConst.AutoSize = true;
            this.mrBtnConst.Location = new System.Drawing.Point(17, 150);
            this.mrBtnConst.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnConst.Name = "mrBtnConst";
            this.mrBtnConst.Size = new System.Drawing.Size(152, 21);
            this.mrBtnConst.TabIndex = 5;
            this.mrBtnConst.Tag = "svd";
            this.mrBtnConst.Text = "Substitute Constant";
            this.mrBtnConst.UseVisualStyleBackColor = true;
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(139, 22);
            this.mcmbBoxFactors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(168, 24);
            this.mcmbBoxFactors.TabIndex = 52;
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(21, 26);
            this.mlblPickFactor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(107, 17);
            this.mlblPickFactor.TabIndex = 51;
            this.mlblPickFactor.Text = "Within a Factor:";
            // 
            // mchkBoxNoImpute
            // 
            this.mchkBoxNoImpute.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxNoImpute.Location = new System.Drawing.Point(25, 225);
            this.mchkBoxNoImpute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mchkBoxNoImpute.Name = "mchkBoxNoImpute";
            this.mchkBoxNoImpute.Size = new System.Drawing.Size(255, 62);
            this.mchkBoxNoImpute.TabIndex = 53;
            this.mchkBoxNoImpute.Text = "Do not impute entries with missing values more than the threshold (only in advanc" +
    "e methods).";
            this.mchkBoxNoImpute.UseVisualStyleBackColor = true;
            // 
            // mrBtnRowMean
            // 
            this.mrBtnRowMean.AutoSize = true;
            this.mrBtnRowMean.Location = new System.Drawing.Point(4, 7);
            this.mrBtnRowMean.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnRowMean.Name = "mrBtnRowMean";
            this.mrBtnRowMean.Size = new System.Drawing.Size(190, 21);
            this.mrBtnRowMean.TabIndex = 54;
            this.mrBtnRowMean.Tag = "svd";
            this.mrBtnRowMean.Text = "Row Mean within a Factor";
            this.mrBtnRowMean.UseVisualStyleBackColor = true;
            // 
            // mtabCImpute
            // 
            this.mtabCImpute.Controls.Add(this.Advance);
            this.mtabCImpute.Controls.Add(this.Simple);
            this.mtabCImpute.Location = new System.Drawing.Point(23, 174);
            this.mtabCImpute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtabCImpute.Name = "mtabCImpute";
            this.mtabCImpute.SelectedIndex = 0;
            this.mtabCImpute.Size = new System.Drawing.Size(576, 338);
            this.mtabCImpute.TabIndex = 37;
            // 
            // Advance
            // 
            this.Advance.BackColor = System.Drawing.SystemColors.Control;
            this.Advance.Controls.Add(this.mlblPickFactor);
            this.Advance.Controls.Add(this.mchkBoxNoImpute);
            this.Advance.Controls.Add(this.mcmbBoxFactors);
            this.Advance.Controls.Add(this.groupBox1);
            this.Advance.Controls.Add(this.groupBox2);
            this.Advance.Controls.Add(this.panel1);
            this.Advance.Location = new System.Drawing.Point(4, 25);
            this.Advance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Advance.Name = "Advance";
            this.Advance.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Advance.Size = new System.Drawing.Size(568, 309);
            this.Advance.TabIndex = 1;
            this.Advance.Text = "Advance";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mrBtnRowMean);
            this.panel1.Controls.Add(this.mrBtnSVD);
            this.panel1.Controls.Add(this.mrBtnWKNN);
            this.panel1.Controls.Add(this.mrBtnKNN);
            this.panel1.Location = new System.Drawing.Point(23, 74);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 165);
            this.panel1.TabIndex = 38;
            // 
            // Simple
            // 
            this.Simple.BackColor = System.Drawing.SystemColors.Control;
            this.Simple.Controls.Add(this.panel2);
            this.Simple.Location = new System.Drawing.Point(4, 25);
            this.Simple.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Simple.Name = "Simple";
            this.Simple.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Simple.Size = new System.Drawing.Size(568, 309);
            this.Simple.TabIndex = 0;
            this.Simple.Text = "Simple";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mrBtnMean);
            this.panel2.Controls.Add(this.mrBtnMedian);
            this.panel2.Controls.Add(this.mtxtBoxConst);
            this.panel2.Controls.Add(this.mrBtnConst);
            this.panel2.Location = new System.Drawing.Point(136, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 202);
            this.panel2.TabIndex = 38;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(160, 64);
            this.mlblDataName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(52, 17);
            this.mlblDataName.TabIndex = 55;
            this.mlblDataName.Text = "label8";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(20, 519);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(580, 17);
            this.niceLine2.TabIndex = 35;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 31);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(580, 17);
            this.niceLine1.TabIndex = 27;
            // 
            // frmImputePar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 591);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.mtabCImpute);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtxtBoxFthres);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImputePar";
            this.ShowInTaskbar = false;
            this.Text = "Select Options for Imputing";
            this.Load += new System.EventHandler(this.frmImputePar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mtabCImpute.ResumeLayout(false);
            this.Advance.ResumeLayout(false);
            this.Advance.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Simple.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mtxtBoxFthres;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton mrBtnSVD;
        private System.Windows.Forms.RadioButton mrBtnWKNN;
        private System.Windows.Forms.RadioButton mrBtnKNN;
        private System.Windows.Forms.RadioButton mrBtnMedian;
        private System.Windows.Forms.RadioButton mrBtnMean;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox mtxtBoxnPCs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mtxtBoxK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtBoxSVDthres;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox mtxtBoxSVDiter;
        private System.Windows.Forms.TextBox mtxtBoxConst;
        private System.Windows.Forms.RadioButton mrBtnConst;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.CheckBox mchkBoxNoImpute;
        private System.Windows.Forms.RadioButton mrBtnRowMean;
        private System.Windows.Forms.TabControl mtabCImpute;
        private System.Windows.Forms.TabPage Simple;
        private System.Windows.Forms.TabPage Advance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label mlblDataName;
    }
}