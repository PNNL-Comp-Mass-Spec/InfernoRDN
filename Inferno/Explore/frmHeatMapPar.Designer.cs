namespace DAnTE.Inferno
{
    partial class frmHeatMapPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHeatMapPar));
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mtxtBoxEnd = new System.Windows.Forms.TextBox();
            this.mtxtBoxStart = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mrBtnRowSelection = new System.Windows.Forms.RadioButton();
            this.mrBtnSelectSubset = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mtabLayoutRows = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mcmbBoxDist = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mpanelHclust = new System.Windows.Forms.Panel();
            this.mcmbBoxAggl = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mrBtnHclust = new System.Windows.Forms.RadioButton();
            this.mpanelKmeans = new System.Windows.Forms.Panel();
            this.mtxtBoxK = new System.Windows.Forms.TextBox();
            this.mchkBoxSeed = new System.Windows.Forms.CheckBox();
            this.mlblK = new System.Windows.Forms.Label();
            this.mrBtnKmeans = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.mchkBoxRows = new System.Windows.Forms.CheckBox();
            this.mchkBoxCols = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mlblHeatPalette = new System.Windows.Forms.Label();
            this.mbtnHeatmapC = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mchkBoxScale = new System.Windows.Forms.CheckBox();
            this.mchkBoxXlab = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.mchkBoxColRng = new System.Windows.Forms.CheckBox();
            this.mtxtBoxMaxCol = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mtxtBoxMinCol = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.mtabLayoutRows.SuspendLayout();
            this.panel5.SuspendLayout();
            this.mpanelHclust.SuspendLayout();
            this.mpanelKmeans.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Heatmap/Clustering Parameters";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(416, 357);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 26;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(302, 357);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 25;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mtxtBoxEnd);
            this.panel1.Controls.Add(this.mtxtBoxStart);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.mrBtnRowSelection);
            this.panel1.Controls.Add(this.mrBtnSelectSubset);
            this.panel1.Location = new System.Drawing.Point(196, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 158);
            this.panel1.TabIndex = 27;
            // 
            // mtxtBoxEnd
            // 
            this.mtxtBoxEnd.Location = new System.Drawing.Point(93, 116);
            this.mtxtBoxEnd.Name = "mtxtBoxEnd";
            this.mtxtBoxEnd.Size = new System.Drawing.Size(71, 20);
            this.mtxtBoxEnd.TabIndex = 51;
            this.mtxtBoxEnd.Text = "50";
            // 
            // mtxtBoxStart
            // 
            this.mtxtBoxStart.Location = new System.Drawing.Point(93, 86);
            this.mtxtBoxStart.Name = "mtxtBoxStart";
            this.mtxtBoxStart.Size = new System.Drawing.Size(71, 20);
            this.mtxtBoxStart.TabIndex = 50;
            this.mtxtBoxStart.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "Ending row:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Starting row:";
            // 
            // mrBtnRowSelection
            // 
            this.mrBtnRowSelection.AutoSize = true;
            this.mrBtnRowSelection.Checked = true;
            this.mrBtnRowSelection.Location = new System.Drawing.Point(8, 14);
            this.mrBtnRowSelection.Name = "mrBtnRowSelection";
            this.mrBtnRowSelection.Size = new System.Drawing.Size(175, 17);
            this.mrBtnRowSelection.TabIndex = 26;
            this.mrBtnRowSelection.TabStop = true;
            this.mrBtnRowSelection.Text = "Use Row Selection in Data Grid";
            this.mrBtnRowSelection.UseVisualStyleBackColor = true;
            // 
            // mrBtnSelectSubset
            // 
            this.mrBtnSelectSubset.AutoSize = true;
            this.mrBtnSelectSubset.Location = new System.Drawing.Point(8, 53);
            this.mrBtnSelectSubset.Name = "mrBtnSelectSubset";
            this.mrBtnSelectSubset.Size = new System.Drawing.Size(138, 17);
            this.mrBtnSelectSubset.TabIndex = 25;
            this.mrBtnSelectSubset.Text = "Select a subset of rows:";
            this.mrBtnSelectSubset.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Data Selection:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mtabLayoutRows);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.mchkBoxRows);
            this.panel2.Controls.Add(this.mchkBoxCols);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 259);
            this.panel2.TabIndex = 29;
            // 
            // mtabLayoutRows
            // 
            this.mtabLayoutRows.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.mtabLayoutRows.ColumnCount = 1;
            this.mtabLayoutRows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.11111F));
            this.mtabLayoutRows.Controls.Add(this.panel5, 0, 0);
            this.mtabLayoutRows.Location = new System.Drawing.Point(8, 33);
            this.mtabLayoutRows.Name = "mtabLayoutRows";
            this.mtabLayoutRows.RowCount = 1;
            this.mtabLayoutRows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mtabLayoutRows.Size = new System.Drawing.Size(360, 158);
            this.mtabLayoutRows.TabIndex = 64;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.mcmbBoxDist);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.mpanelHclust);
            this.panel5.Controls.Add(this.mrBtnHclust);
            this.panel5.Controls.Add(this.mpanelKmeans);
            this.panel5.Controls.Add(this.mrBtnKmeans);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(5, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(350, 148);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Location = new System.Drawing.Point(12, 90);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(321, 4);
            this.panel6.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(60, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(234, 12);
            this.label9.TabIndex = 66;
            this.label9.Text = "Select distance metric for either Hierarchical or K-means";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(175, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 84);
            this.panel4.TabIndex = 64;
            // 
            // mcmbBoxDist
            // 
            this.mcmbBoxDist.FormattingEnabled = true;
            this.mcmbBoxDist.Location = new System.Drawing.Point(154, 102);
            this.mcmbBoxDist.Name = "mcmbBoxDist";
            this.mcmbBoxDist.Size = new System.Drawing.Size(136, 21);
            this.mcmbBoxDist.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Distance metric:";
            // 
            // mpanelHclust
            // 
            this.mpanelHclust.Controls.Add(this.mcmbBoxAggl);
            this.mpanelHclust.Controls.Add(this.label7);
            this.mpanelHclust.Location = new System.Drawing.Point(26, 34);
            this.mpanelHclust.Name = "mpanelHclust";
            this.mpanelHclust.Size = new System.Drawing.Size(139, 47);
            this.mpanelHclust.TabIndex = 62;
            // 
            // mcmbBoxAggl
            // 
            this.mcmbBoxAggl.FormattingEnabled = true;
            this.mcmbBoxAggl.Location = new System.Drawing.Point(12, 20);
            this.mcmbBoxAggl.Name = "mcmbBoxAggl";
            this.mcmbBoxAggl.Size = new System.Drawing.Size(112, 21);
            this.mcmbBoxAggl.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Agglomeration method:";
            // 
            // mrBtnHclust
            // 
            this.mrBtnHclust.AutoSize = true;
            this.mrBtnHclust.Checked = true;
            this.mrBtnHclust.Location = new System.Drawing.Point(12, 11);
            this.mrBtnHclust.Name = "mrBtnHclust";
            this.mrBtnHclust.Size = new System.Drawing.Size(133, 17);
            this.mrBtnHclust.TabIndex = 51;
            this.mrBtnHclust.TabStop = true;
            this.mrBtnHclust.Text = "Hierarchical Clustering:";
            this.mrBtnHclust.UseVisualStyleBackColor = true;
            this.mrBtnHclust.CheckedChanged += new System.EventHandler(this.mrBtnHclust_CheckedChanged);
            // 
            // mpanelKmeans
            // 
            this.mpanelKmeans.Controls.Add(this.mtxtBoxK);
            this.mpanelKmeans.Controls.Add(this.mchkBoxSeed);
            this.mpanelKmeans.Controls.Add(this.mlblK);
            this.mpanelKmeans.Location = new System.Drawing.Point(209, 34);
            this.mpanelKmeans.Name = "mpanelKmeans";
            this.mpanelKmeans.Size = new System.Drawing.Size(114, 47);
            this.mpanelKmeans.TabIndex = 65;
            // 
            // mtxtBoxK
            // 
            this.mtxtBoxK.Location = new System.Drawing.Point(29, 1);
            this.mtxtBoxK.Name = "mtxtBoxK";
            this.mtxtBoxK.Size = new System.Drawing.Size(35, 20);
            this.mtxtBoxK.TabIndex = 55;
            this.mtxtBoxK.Text = "5";
            // 
            // mchkBoxSeed
            // 
            this.mchkBoxSeed.AutoSize = true;
            this.mchkBoxSeed.Location = new System.Drawing.Point(3, 29);
            this.mchkBoxSeed.Name = "mchkBoxSeed";
            this.mchkBoxSeed.Size = new System.Drawing.Size(110, 17);
            this.mchkBoxSeed.TabIndex = 59;
            this.mchkBoxSeed.Text = "Fix Random Seed";
            this.mchkBoxSeed.UseVisualStyleBackColor = true;
            // 
            // mlblK
            // 
            this.mlblK.AutoSize = true;
            this.mlblK.Location = new System.Drawing.Point(3, 4);
            this.mlblK.Name = "mlblK";
            this.mlblK.Size = new System.Drawing.Size(20, 13);
            this.mlblK.TabIndex = 56;
            this.mlblK.Text = "K :";
            // 
            // mrBtnKmeans
            // 
            this.mrBtnKmeans.AutoSize = true;
            this.mrBtnKmeans.Location = new System.Drawing.Point(195, 11);
            this.mrBtnKmeans.Name = "mrBtnKmeans";
            this.mrBtnKmeans.Size = new System.Drawing.Size(118, 17);
            this.mrBtnKmeans.TabIndex = 52;
            this.mrBtnKmeans.Text = "K-means Clustering:";
            this.mrBtnKmeans.UseVisualStyleBackColor = true;
            this.mrBtnKmeans.CheckedChanged += new System.EventHandler(this.mrBtnKmeans_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(32, 229);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(259, 12);
            this.label8.TabIndex = 65;
            this.label8.Text = "Hierarchical : Complete Linkage with Distance selected above";
            // 
            // mchkBoxRows
            // 
            this.mchkBoxRows.AutoSize = true;
            this.mchkBoxRows.Checked = true;
            this.mchkBoxRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxRows.Location = new System.Drawing.Point(12, 10);
            this.mchkBoxRows.Name = "mchkBoxRows";
            this.mchkBoxRows.Size = new System.Drawing.Size(53, 17);
            this.mchkBoxRows.TabIndex = 58;
            this.mchkBoxRows.Text = "Rows";
            this.mchkBoxRows.UseVisualStyleBackColor = true;
            this.mchkBoxRows.CheckedChanged += new System.EventHandler(this.mchkBoxRows_CheckedChanged);
            // 
            // mchkBoxCols
            // 
            this.mchkBoxCols.AutoSize = true;
            this.mchkBoxCols.Location = new System.Drawing.Point(12, 209);
            this.mchkBoxCols.Name = "mchkBoxCols";
            this.mchkBoxCols.Size = new System.Drawing.Size(66, 17);
            this.mchkBoxCols.TabIndex = 57;
            this.mchkBoxCols.Text = "Columns";
            this.mchkBoxCols.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Clustering:";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(77, 14);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 57;
            this.mlblDataName.Text = "label8";
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(54, 50);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(111, 21);
            this.mcmbBoxFactors.TabIndex = 56;
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(10, 53);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(40, 13);
            this.mlblPickFactor.TabIndex = 55;
            this.mlblPickFactor.Text = "Factor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Data source:";
            // 
            // mlblHeatPalette
            // 
            this.mlblHeatPalette.AutoSize = true;
            this.mlblHeatPalette.Location = new System.Drawing.Point(112, 13);
            this.mlblHeatPalette.Name = "mlblHeatPalette";
            this.mlblHeatPalette.Size = new System.Drawing.Size(39, 13);
            this.mlblHeatPalette.TabIndex = 59;
            this.mlblHeatPalette.Text = "palette";
            // 
            // mbtnHeatmapC
            // 
            this.mbtnHeatmapC.Location = new System.Drawing.Point(17, 8);
            this.mbtnHeatmapC.Name = "mbtnHeatmapC";
            this.mbtnHeatmapC.Size = new System.Drawing.Size(77, 23);
            this.mbtnHeatmapC.TabIndex = 58;
            this.mbtnHeatmapC.Text = "Color Palette";
            this.mbtnHeatmapC.UseVisualStyleBackColor = true;
            this.mbtnHeatmapC.Click += new System.EventHandler(this.mbtnHeatmapPalette_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mchkBoxScale);
            this.panel3.Controls.Add(this.mchkBoxXlab);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.mlblPickFactor);
            this.panel3.Controls.Add(this.mcmbBoxFactors);
            this.panel3.Controls.Add(this.mlblDataName);
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(181, 158);
            this.panel3.TabIndex = 61;
            // 
            // mchkBoxScale
            // 
            this.mchkBoxScale.AutoSize = true;
            this.mchkBoxScale.Checked = true;
            this.mchkBoxScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxScale.Location = new System.Drawing.Point(16, 89);
            this.mchkBoxScale.Name = "mchkBoxScale";
            this.mchkBoxScale.Size = new System.Drawing.Size(83, 17);
            this.mchkBoxScale.TabIndex = 65;
            this.mchkBoxScale.Text = "Scale Rows";
            this.mchkBoxScale.UseVisualStyleBackColor = true;
            // 
            // mchkBoxXlab
            // 
            this.mchkBoxXlab.AutoSize = true;
            this.mchkBoxXlab.Location = new System.Drawing.Point(16, 119);
            this.mchkBoxXlab.Name = "mchkBoxXlab";
            this.mchkBoxXlab.Size = new System.Drawing.Size(129, 17);
            this.mchkBoxXlab.TabIndex = 64;
            this.mchkBoxXlab.Text = "Suppress Row Labels";
            this.mchkBoxXlab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(383, 170);
            this.tableLayoutPanel1.TabIndex = 62;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(402, 65);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(388, 271);
            this.tableLayoutPanel2.TabIndex = 63;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(13, 241);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.69565F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.30435F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(383, 95);
            this.tableLayoutPanel3.TabIndex = 64;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.mchkBoxColRng);
            this.panel7.Controls.Add(this.mtxtBoxMaxCol);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.mtxtBoxMinCol);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.mlblHeatPalette);
            this.panel7.Controls.Add(this.mbtnHeatmapC);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(6, 6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(371, 83);
            this.panel7.TabIndex = 0;
            // 
            // mchkBoxColRng
            // 
            this.mchkBoxColRng.AutoSize = true;
            this.mchkBoxColRng.Location = new System.Drawing.Point(19, 51);
            this.mchkBoxColRng.Name = "mchkBoxColRng";
            this.mchkBoxColRng.Size = new System.Drawing.Size(88, 17);
            this.mchkBoxColRng.TabIndex = 66;
            this.mchkBoxColRng.Text = "Color Range:";
            this.mchkBoxColRng.UseVisualStyleBackColor = true;
            this.mchkBoxColRng.CheckedChanged += new System.EventHandler(this.mchkBoxColRng_CheckedChanged);
            // 
            // mtxtBoxMaxCol
            // 
            this.mtxtBoxMaxCol.Location = new System.Drawing.Point(282, 49);
            this.mtxtBoxMaxCol.Name = "mtxtBoxMaxCol";
            this.mtxtBoxMaxCol.Size = new System.Drawing.Size(53, 20);
            this.mtxtBoxMaxCol.TabIndex = 68;
            this.mtxtBoxMaxCol.Text = "5";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(251, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 13);
            this.label13.TabIndex = 67;
            this.label13.Text = "Max:";
            // 
            // mtxtBoxMinCol
            // 
            this.mtxtBoxMinCol.Location = new System.Drawing.Point(167, 49);
            this.mtxtBoxMinCol.Name = "mtxtBoxMinCol";
            this.mtxtBoxMinCol.Size = new System.Drawing.Size(53, 20);
            this.mtxtBoxMinCol.TabIndex = 53;
            this.mtxtBoxMinCol.Text = "-5";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(139, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 52;
            this.label12.Text = "Min:";
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(12, 336);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(778, 15);
            this.niceLine3.TabIndex = 65;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 25);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(778, 15);
            this.niceLine2.TabIndex = 23;
            // 
            // frmHeatMapPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 392);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHeatMapPar";
            this.ShowInTaskbar = false;
            this.Text = "Heatmap";
            this.Load += new System.EventHandler(this.frmHeatMapPar_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.mtabLayoutRows.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.mpanelHclust.ResumeLayout(false);
            this.mpanelHclust.PerformLayout();
            this.mpanelKmeans.ResumeLayout(false);
            this.mpanelKmeans.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton mrBtnRowSelection;
        private System.Windows.Forms.RadioButton mrBtnSelectSubset;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton mrBtnKmeans;
        private System.Windows.Forms.RadioButton mrBtnHclust;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mlblHeatPalette;
        private System.Windows.Forms.Button mbtnHeatmapC;
        private System.Windows.Forms.Label mlblK;
        private System.Windows.Forms.TextBox mtxtBoxK;
        private System.Windows.Forms.TextBox mtxtBoxEnd;
        private System.Windows.Forms.TextBox mtxtBoxStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox mchkBoxCols;
        private System.Windows.Forms.CheckBox mchkBoxRows;
        private System.Windows.Forms.CheckBox mchkBoxSeed;
        private System.Windows.Forms.Panel mpanelHclust;
        private System.Windows.Forms.ComboBox mcmbBoxAggl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox mcmbBoxDist;
        private System.Windows.Forms.Panel mpanelKmeans;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel mtabLayoutRows;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox mchkBoxXlab;
        private System.Windows.Forms.CheckBox mchkBoxScale;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox mtxtBoxMaxCol;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox mtxtBoxMinCol;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox mchkBoxColRng;
        private DAnTE.ExtraControls.NiceLine niceLine3;

    }
}