namespace DAnTE.Inferno
{
    partial class frmCorrelationPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorrelationPar));
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.mchkBoxLoess = new System.Windows.Forms.CheckBox();
            this.mchkBoxPoints = new System.Windows.Forms.CheckBox();
            this.mchkBoxStamp = new System.Windows.Forms.CheckBox();
            this.mlbl2DPalette = new System.Windows.Forms.Label();
            this.mbtn2Dpalette = new System.Windows.Forms.Button();
            this.mlblHeatPalette = new System.Windows.Forms.Label();
            this.mbtnHeatmapC = new System.Windows.Forms.Button();
            this.mchkBoxShowCorr = new System.Windows.Forms.CheckBox();
            this.mrbtn2Dmat = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.mchkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mnumUDFontSc = new System.Windows.Forms.NumericUpDown();
            this.mchkboxYXLine = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mbtnEcol = new System.Windows.Forms.Button();
            this.mlblEcolor = new System.Windows.Forms.Label();
            this.mrbtnEllipse = new System.Windows.Forms.RadioButton();
            this.mchkBoxdHist = new System.Windows.Forms.CheckBox();
            this.mrbtnHeatmap = new System.Windows.Forms.RadioButton();
            this.mrBtnScatter = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.mbtnToggleAll = new System.Windows.Forms.Button();
            this.hexColorDialog = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.mlblMin = new System.Windows.Forms.Label();
            this.mlblMax = new System.Windows.Forms.Label();
            this.mpanelCorr = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mRangeBar = new DAnTE.ExtraControls.ZzzzRangeBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mnumUDFontSc)).BeginInit();
            this.panel1.SuspendLayout();
            this.mpanelCorr.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(215, 506);
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
            this.mbtnCancel.Location = new System.Drawing.Point(449, 507);
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
            this.mlstViewDataSets.Location = new System.Drawing.Point(10, 12);
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(270, 325);
            this.mlstViewDataSets.TabIndex = 15;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.List;
            this.mlstViewDataSets.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.mlstViewDataSets_ItemChecked);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.mchkBoxLoess);
            this.panel6.Controls.Add(this.mchkBoxPoints);
            this.panel6.Controls.Add(this.mchkBoxStamp);
            this.panel6.Controls.Add(this.mlbl2DPalette);
            this.panel6.Controls.Add(this.mbtn2Dpalette);
            this.panel6.Controls.Add(this.mlblHeatPalette);
            this.panel6.Controls.Add(this.mbtnHeatmapC);
            this.panel6.Controls.Add(this.mchkBoxShowCorr);
            this.panel6.Controls.Add(this.mrbtn2Dmat);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.mchkBoxTransparent);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.mnumUDFontSc);
            this.panel6.Controls.Add(this.mchkboxYXLine);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.mbtnEcol);
            this.panel6.Controls.Add(this.mlblEcolor);
            this.panel6.Controls.Add(this.mrbtnEllipse);
            this.panel6.Controls.Add(this.mchkBoxdHist);
            this.panel6.Controls.Add(this.mrbtnHeatmap);
            this.panel6.Controls.Add(this.mrBtnScatter);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(9, 106);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(271, 373);
            this.panel6.TabIndex = 19;
            // 
            // mchkBoxLoess
            // 
            this.mchkBoxLoess.AutoSize = true;
            this.mchkBoxLoess.Enabled = false;
            this.mchkBoxLoess.Location = new System.Drawing.Point(179, 242);
            this.mchkBoxLoess.Name = "mchkBoxLoess";
            this.mchkBoxLoess.Size = new System.Drawing.Size(80, 17);
            this.mchkBoxLoess.TabIndex = 61;
            this.mchkBoxLoess.Text = "LOESS line";
            this.mchkBoxLoess.UseVisualStyleBackColor = true;
            // 
            // mchkBoxPoints
            // 
            this.mchkBoxPoints.AutoSize = true;
            this.mchkBoxPoints.Enabled = false;
            this.mchkBoxPoints.Location = new System.Drawing.Point(35, 242);
            this.mchkBoxPoints.Name = "mchkBoxPoints";
            this.mchkBoxPoints.Size = new System.Drawing.Size(142, 17);
            this.mchkBoxPoints.TabIndex = 60;
            this.mchkBoxPoints.Text = "Show overlapping points";
            this.mchkBoxPoints.UseVisualStyleBackColor = true;
            // 
            // mchkBoxStamp
            // 
            this.mchkBoxStamp.AutoSize = true;
            this.mchkBoxStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxStamp.Location = new System.Drawing.Point(12, 347);
            this.mchkBoxStamp.Name = "mchkBoxStamp";
            this.mchkBoxStamp.Size = new System.Drawing.Size(120, 16);
            this.mchkBoxStamp.TabIndex = 59;
            this.mchkBoxStamp.Text = "Add Date/Name Stamp";
            this.mchkBoxStamp.UseVisualStyleBackColor = true;
            // 
            // mlbl2DPalette
            // 
            this.mlbl2DPalette.AutoSize = true;
            this.mlbl2DPalette.Location = new System.Drawing.Point(111, 92);
            this.mlbl2DPalette.Name = "mlbl2DPalette";
            this.mlbl2DPalette.Size = new System.Drawing.Size(39, 13);
            this.mlbl2DPalette.TabIndex = 52;
            this.mlbl2DPalette.Text = "palette";
            // 
            // mbtn2Dpalette
            // 
            this.mbtn2Dpalette.Location = new System.Drawing.Point(47, 87);
            this.mbtn2Dpalette.Name = "mbtn2Dpalette";
            this.mbtn2Dpalette.Size = new System.Drawing.Size(50, 23);
            this.mbtn2Dpalette.TabIndex = 51;
            this.mbtn2Dpalette.Text = "Palette";
            this.mbtn2Dpalette.UseVisualStyleBackColor = true;
            this.mbtn2Dpalette.Click += new System.EventHandler(this.mbtn2Dpalette_Click);
            // 
            // mlblHeatPalette
            // 
            this.mlblHeatPalette.AutoSize = true;
            this.mlblHeatPalette.Location = new System.Drawing.Point(110, 40);
            this.mlblHeatPalette.Name = "mlblHeatPalette";
            this.mlblHeatPalette.Size = new System.Drawing.Size(39, 13);
            this.mlblHeatPalette.TabIndex = 50;
            this.mlblHeatPalette.Text = "palette";
            // 
            // mbtnHeatmapC
            // 
            this.mbtnHeatmapC.Location = new System.Drawing.Point(47, 35);
            this.mbtnHeatmapC.Name = "mbtnHeatmapC";
            this.mbtnHeatmapC.Size = new System.Drawing.Size(50, 23);
            this.mbtnHeatmapC.TabIndex = 41;
            this.mbtnHeatmapC.Text = "Palette";
            this.mbtnHeatmapC.UseVisualStyleBackColor = true;
            this.mbtnHeatmapC.Click += new System.EventHandler(this.mbtnHeatmapPalette_Click);
            // 
            // mchkBoxShowCorr
            // 
            this.mchkBoxShowCorr.AutoSize = true;
            this.mchkBoxShowCorr.Location = new System.Drawing.Point(47, 122);
            this.mchkBoxShowCorr.Name = "mchkBoxShowCorr";
            this.mchkBoxShowCorr.Size = new System.Drawing.Size(141, 17);
            this.mchkBoxShowCorr.TabIndex = 40;
            this.mchkBoxShowCorr.Text = "Show Correlation Values";
            this.mchkBoxShowCorr.UseVisualStyleBackColor = true;
            // 
            // mrbtn2Dmat
            // 
            this.mrbtn2Dmat.AutoSize = true;
            this.mrbtn2Dmat.Location = new System.Drawing.Point(12, 64);
            this.mrbtn2Dmat.Name = "mrbtn2Dmat";
            this.mrbtn2Dmat.Size = new System.Drawing.Size(133, 17);
            this.mrbtn2Dmat.TabIndex = 39;
            this.mrbtn2Dmat.Text = "2D Box (OmniViz) Style";
            this.mrbtn2Dmat.UseVisualStyleBackColor = true;
            this.mrbtn2Dmat.CheckedChanged += new System.EventHandler(this.mrbtn2Dmat_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(152, 319);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "(Only for PNG format)";
            // 
            // mchkBoxTransparent
            // 
            this.mchkBoxTransparent.AutoSize = true;
            this.mchkBoxTransparent.Location = new System.Drawing.Point(12, 318);
            this.mchkBoxTransparent.Name = "mchkBoxTransparent";
            this.mchkBoxTransparent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxTransparent.Size = new System.Drawing.Size(144, 17);
            this.mchkBoxTransparent.TabIndex = 37;
            this.mchkBoxTransparent.Text = "Transparent Background";
            this.mchkBoxTransparent.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "(0.0 - 1.0)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Font scaling:";
            // 
            // mnumUDFontSc
            // 
            this.mnumUDFontSc.DecimalPlaces = 1;
            this.mnumUDFontSc.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.mnumUDFontSc.Location = new System.Drawing.Point(82, 287);
            this.mnumUDFontSc.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mnumUDFontSc.Name = "mnumUDFontSc";
            this.mnumUDFontSc.Size = new System.Drawing.Size(67, 20);
            this.mnumUDFontSc.TabIndex = 33;
            this.mnumUDFontSc.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // mchkboxYXLine
            // 
            this.mchkboxYXLine.AutoSize = true;
            this.mchkboxYXLine.Checked = true;
            this.mchkboxYXLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkboxYXLine.Enabled = false;
            this.mchkboxYXLine.Location = new System.Drawing.Point(179, 213);
            this.mchkboxYXLine.Name = "mchkboxYXLine";
            this.mchkboxYXLine.Size = new System.Drawing.Size(71, 17);
            this.mchkboxYXLine.TabIndex = 14;
            this.mchkboxYXLine.Text = "Y = X line";
            this.mchkboxYXLine.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Color:";
            // 
            // mbtnEcol
            // 
            this.mbtnEcol.Location = new System.Drawing.Point(170, 147);
            this.mbtnEcol.Name = "mbtnEcol";
            this.mbtnEcol.Size = new System.Drawing.Size(25, 20);
            this.mbtnEcol.TabIndex = 13;
            this.mbtnEcol.Text = "\'\'\'";
            this.mbtnEcol.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.mbtnEcol.UseVisualStyleBackColor = true;
            this.mbtnEcol.Click += new System.EventHandler(this.mbtnEcol_Click);
            // 
            // mlblEcolor
            // 
            this.mlblEcolor.AutoSize = true;
            this.mlblEcolor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlblEcolor.Location = new System.Drawing.Point(148, 148);
            this.mlblEcolor.Name = "mlblEcolor";
            this.mlblEcolor.Size = new System.Drawing.Size(18, 15);
            this.mlblEcolor.TabIndex = 12;
            this.mlblEcolor.Text = "M";
            this.mlblEcolor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mrbtnEllipse
            // 
            this.mrbtnEllipse.AutoSize = true;
            this.mrbtnEllipse.Location = new System.Drawing.Point(12, 145);
            this.mrbtnEllipse.Name = "mrbtnEllipse";
            this.mrbtnEllipse.Size = new System.Drawing.Size(81, 17);
            this.mrbtnEllipse.TabIndex = 4;
            this.mrbtnEllipse.Text = "Ellipse Style";
            this.mrbtnEllipse.UseVisualStyleBackColor = true;
            this.mrbtnEllipse.CheckedChanged += new System.EventHandler(this.mrbtnEllipse_CheckedChanged);
            // 
            // mchkBoxdHist
            // 
            this.mchkBoxdHist.AutoSize = true;
            this.mchkBoxdHist.Checked = true;
            this.mchkBoxdHist.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxdHist.Enabled = false;
            this.mchkBoxdHist.Location = new System.Drawing.Point(35, 213);
            this.mchkBoxdHist.Name = "mchkBoxdHist";
            this.mchkBoxdHist.Size = new System.Drawing.Size(136, 17);
            this.mchkBoxdHist.TabIndex = 2;
            this.mchkBoxdHist.Text = "Histograms on diagonal";
            this.mchkBoxdHist.UseVisualStyleBackColor = true;
            // 
            // mrbtnHeatmap
            // 
            this.mrbtnHeatmap.AutoSize = true;
            this.mrbtnHeatmap.Checked = true;
            this.mrbtnHeatmap.Location = new System.Drawing.Point(12, 12);
            this.mrbtnHeatmap.Name = "mrbtnHeatmap";
            this.mrbtnHeatmap.Size = new System.Drawing.Size(94, 17);
            this.mrbtnHeatmap.TabIndex = 1;
            this.mrbtnHeatmap.TabStop = true;
            this.mrbtnHeatmap.Text = "Heatmap Style";
            this.mrbtnHeatmap.UseVisualStyleBackColor = true;
            this.mrbtnHeatmap.CheckedChanged += new System.EventHandler(this.mrbtnHeatmap_CheckedChanged);
            // 
            // mrBtnScatter
            // 
            this.mrBtnScatter.AutoSize = true;
            this.mrBtnScatter.Location = new System.Drawing.Point(12, 180);
            this.mrBtnScatter.Name = "mrBtnScatter";
            this.mrBtnScatter.Size = new System.Drawing.Size(85, 17);
            this.mrBtnScatter.TabIndex = 0;
            this.mrBtnScatter.Text = "Scatter Style";
            this.mrBtnScatter.UseVisualStyleBackColor = true;
            this.mrBtnScatter.CheckedChanged += new System.EventHandler(this.mrBtnScatter_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 262);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(235, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "______________________________________";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mbtnToggleAll
            // 
            this.mbtnToggleAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbtnToggleAll.Location = new System.Drawing.Point(215, 343);
            this.mbtnToggleAll.Name = "mbtnToggleAll";
            this.mbtnToggleAll.Size = new System.Drawing.Size(65, 23);
            this.mbtnToggleAll.TabIndex = 20;
            this.mbtnToggleAll.Text = "Toggle All";
            this.mbtnToggleAll.UseVisualStyleBackColor = true;
            this.mbtnToggleAll.Click += new System.EventHandler(this.buttonToggleAll_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.mlstViewDataSets);
            this.panel1.Controls.Add(this.mbtnToggleAll);
            this.panel1.Location = new System.Drawing.Point(449, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 373);
            this.panel1.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Correlation Range:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Min:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(61, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "Max:";
            // 
            // mlblMin
            // 
            this.mlblMin.AutoSize = true;
            this.mlblMin.Location = new System.Drawing.Point(94, 136);
            this.mlblMin.Name = "mlblMin";
            this.mlblMin.Size = new System.Drawing.Size(41, 13);
            this.mlblMin.TabIndex = 45;
            this.mlblMin.Text = "label12";
            // 
            // mlblMax
            // 
            this.mlblMax.AutoSize = true;
            this.mlblMax.Location = new System.Drawing.Point(94, 157);
            this.mlblMax.Name = "mlblMax";
            this.mlblMax.Size = new System.Drawing.Size(41, 13);
            this.mlblMax.TabIndex = 46;
            this.mlblMax.Text = "label13";
            // 
            // mpanelCorr
            // 
            this.mpanelCorr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mpanelCorr.Controls.Add(this.label13);
            this.mpanelCorr.Controls.Add(this.label12);
            this.mpanelCorr.Controls.Add(this.mlblMax);
            this.mpanelCorr.Controls.Add(this.mlblMin);
            this.mpanelCorr.Controls.Add(this.label11);
            this.mpanelCorr.Controls.Add(this.label3);
            this.mpanelCorr.Controls.Add(this.label1);
            this.mpanelCorr.Controls.Add(this.mRangeBar);
            this.mpanelCorr.Location = new System.Drawing.Point(286, 106);
            this.mpanelCorr.Name = "mpanelCorr";
            this.mpanelCorr.Size = new System.Drawing.Size(157, 373);
            this.mpanelCorr.TabIndex = 43;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(41, 353);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "1.0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 47;
            this.label12.Text = "0.0";
            // 
            // mRangeBar
            // 
            this.mRangeBar.DivisionNum = 20;
            this.mRangeBar.HeightOfBar = 5;
            this.mRangeBar.HeightOfMark = 24;
            this.mRangeBar.HeightOfTick = 5;
            this.mRangeBar.InnerColor = System.Drawing.Color.LightGreen;
            this.mRangeBar.Location = new System.Drawing.Point(3, 33);
            this.mRangeBar.Name = "mRangeBar";
            this.mRangeBar.Orientation = DAnTE.ExtraControls.ZzzzRangeBar.RangeBarOrientation.vertical;
            this.mRangeBar.RangeMaximum = 10;
            this.mRangeBar.RangeMinimum = 6;
            this.mRangeBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mRangeBar.ScaleOrientation = DAnTE.ExtraControls.ZzzzRangeBar.TopBottomOrientation.both;
            this.mRangeBar.Size = new System.Drawing.Size(45, 333);
            this.mRangeBar.TabIndex = 41;
            this.mRangeBar.TotalMaximum = 20;
            this.mRangeBar.TotalMinimum = 0;
            this.mRangeBar.RangeChanging += new DAnTE.ExtraControls.ZzzzRangeBar.RangeChangedEventHandler(this.mRangeBar_RangeChanging);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 46;
            this.label14.Text = "Correlation Plots";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Data Source:";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(96, 52);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 49;
            this.mlblDataName.Text = "label8";
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(332, 506);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 42;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(12, 25);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(730, 15);
            this.niceLine4.TabIndex = 47;
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(13, 485);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(729, 15);
            this.niceLine3.TabIndex = 41;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Select Datasets to Plot";
            this.niceLine2.Location = new System.Drawing.Point(449, 85);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(293, 15);
            this.niceLine2.TabIndex = 40;
            // 
            // niceLine1
            // 
            this.niceLine1.Caption = "Plot Properties";
            this.niceLine1.Location = new System.Drawing.Point(12, 85);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(431, 15);
            this.niceLine1.TabIndex = 39;
            // 
            // frmCorrelationPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(752, 545);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.mpanelCorr);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCorrelationPar";
            this.ShowInTaskbar = false;
            this.Text = "Select Correlation Plot Parameters";
            this.Load += new System.EventHandler(this.frmCorrelationPar_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mnumUDFontSc)).EndInit();
            this.panel1.ResumeLayout(false);
            this.mpanelCorr.ResumeLayout(false);
            this.mpanelCorr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.ListView mlstViewDataSets;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton mrbtnHeatmap;
        private System.Windows.Forms.RadioButton mrBtnScatter;
        private System.Windows.Forms.Button mbtnToggleAll;
        private System.Windows.Forms.CheckBox mchkBoxdHist;
        private System.Windows.Forms.ColorDialog hexColorDialog;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button mbtnEcol;
        private System.Windows.Forms.Label mlblEcolor;
        private System.Windows.Forms.RadioButton mrbtnEllipse;
        private System.Windows.Forms.CheckBox mchkboxYXLine;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown mnumUDFontSc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox mchkBoxTransparent;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private ExtraControls.NiceLine niceLine3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton mrbtn2Dmat;
        private System.Windows.Forms.CheckBox mchkBoxShowCorr;
        private ExtraControls.ZzzzRangeBar mRangeBar;
        private System.Windows.Forms.Label mlblMax;
        private System.Windows.Forms.Label mlblMin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel mpanelCorr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private ExtraControls.NiceLine niceLine4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label mlbl2DPalette;
        private System.Windows.Forms.Button mbtn2Dpalette;
        private System.Windows.Forms.Label mlblHeatPalette;
        private System.Windows.Forms.Button mbtnHeatmapC;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.CheckBox mchkBoxStamp;
        private System.Windows.Forms.CheckBox mchkBoxPoints;
        private System.Windows.Forms.CheckBox mchkBoxLoess;
    }
}