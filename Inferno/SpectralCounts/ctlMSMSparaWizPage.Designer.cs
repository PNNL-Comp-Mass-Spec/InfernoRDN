namespace DAnTE.Inferno
{
    partial class ctlMSMSparaWizPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.mNumUDXcorRank = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtBoxDCn2Th = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtBoxAnFolder = new System.Windows.Forms.TextBox();
            this.mBtnBrowse = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mgrpBoxXcTh = new System.Windows.Forms.GroupBox();
            this.mtxtBxXCOTh = new System.Windows.Forms.TextBox();
            this.mtxtBxXC3Th = new System.Windows.Forms.TextBox();
            this.mtxtBxXC2Th = new System.Windows.Forms.TextBox();
            this.mtxtBxXC1Th = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mchkBoxFull = new System.Windows.Forms.CheckBox();
            this.mchkBoxNone = new System.Windows.Forms.CheckBox();
            this.mchkBoxPartial = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mNumUDXcorRank)).BeginInit();
            this.mgrpBoxXcTh.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Size = new System.Drawing.Size(784, 64);
            this.Banner.Subtitle = "Select parameters for extracting spectral counts";
            this.Banner.Title = "Step 3: Select Parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 165);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Max Xcorr Rank";
            // 
            // mNumUDXcorRank
            // 
            this.mNumUDXcorRank.Location = new System.Drawing.Point(475, 163);
            this.mNumUDXcorRank.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.mNumUDXcorRank.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.mNumUDXcorRank.Name = "mNumUDXcorRank";
            this.mNumUDXcorRank.Size = new System.Drawing.Size(51, 20);
            this.mNumUDXcorRank.TabIndex = 60;
            this.mNumUDXcorRank.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 215);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "DelCn2 Threshold";
            // 
            // mtxtBoxDCn2Th
            // 
            this.mtxtBoxDCn2Th.Location = new System.Drawing.Point(475, 212);
            this.mtxtBoxDCn2Th.Name = "mtxtBoxDCn2Th";
            this.mtxtBoxDCn2Th.Size = new System.Drawing.Size(51, 20);
            this.mtxtBoxDCn2Th.TabIndex = 62;
            this.mtxtBoxDCn2Th.Text = "0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Analysis Folder to use";
            // 
            // mtxtBoxAnFolder
            // 
            this.mtxtBoxAnFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtBoxAnFolder.Location = new System.Drawing.Point(123, 70);
            this.mtxtBoxAnFolder.Name = "mtxtBoxAnFolder";
            this.mtxtBoxAnFolder.Size = new System.Drawing.Size(369, 20);
            this.mtxtBoxAnFolder.TabIndex = 64;
            // 
            // mBtnBrowse
            // 
            this.mBtnBrowse.Location = new System.Drawing.Point(498, 68);
            this.mBtnBrowse.Name = "mBtnBrowse";
            this.mBtnBrowse.Size = new System.Drawing.Size(33, 23);
            this.mBtnBrowse.TabIndex = 65;
            this.mBtnBrowse.Text = "..";
            this.mBtnBrowse.UseVisualStyleBackColor = true;
            this.mBtnBrowse.Click += new System.EventHandler(this.mBtnBrowse_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(123, 91);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(369, 23);
            this.label4.TabIndex = 66;
            this.label4.Text = "Selected files will be first copied to this folder and the subsequent files will " +
                "be saved.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(568, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = "Clicking \'Next\' will start the analysis.";
            // 
            // mgrpBoxXcTh
            // 
            this.mgrpBoxXcTh.Controls.Add(this.mtxtBxXCOTh);
            this.mgrpBoxXcTh.Controls.Add(this.mtxtBxXC3Th);
            this.mgrpBoxXcTh.Controls.Add(this.mtxtBxXC2Th);
            this.mgrpBoxXcTh.Controls.Add(this.mtxtBxXC1Th);
            this.mgrpBoxXcTh.Controls.Add(this.label9);
            this.mgrpBoxXcTh.Controls.Add(this.label8);
            this.mgrpBoxXcTh.Controls.Add(this.label7);
            this.mgrpBoxXcTh.Controls.Add(this.label6);
            this.mgrpBoxXcTh.Location = new System.Drawing.Point(17, 136);
            this.mgrpBoxXcTh.Name = "mgrpBoxXcTh";
            this.mgrpBoxXcTh.Size = new System.Drawing.Size(179, 137);
            this.mgrpBoxXcTh.TabIndex = 68;
            this.mgrpBoxXcTh.TabStop = false;
            this.mgrpBoxXcTh.Text = "XCorr Thresholds";
            // 
            // mtxtBxXCOTh
            // 
            this.mtxtBxXCOTh.Location = new System.Drawing.Point(93, 100);
            this.mtxtBxXCOTh.Name = "mtxtBxXCOTh";
            this.mtxtBxXCOTh.Size = new System.Drawing.Size(54, 20);
            this.mtxtBxXCOTh.TabIndex = 72;
            this.mtxtBxXCOTh.Text = "1.5";
            // 
            // mtxtBxXC3Th
            // 
            this.mtxtBxXC3Th.Location = new System.Drawing.Point(93, 76);
            this.mtxtBxXC3Th.Name = "mtxtBxXC3Th";
            this.mtxtBxXC3Th.Size = new System.Drawing.Size(54, 20);
            this.mtxtBxXC3Th.TabIndex = 71;
            this.mtxtBxXC3Th.Text = "1.5";
            // 
            // mtxtBxXC2Th
            // 
            this.mtxtBxXC2Th.Location = new System.Drawing.Point(93, 52);
            this.mtxtBxXC2Th.Name = "mtxtBxXC2Th";
            this.mtxtBxXC2Th.Size = new System.Drawing.Size(54, 20);
            this.mtxtBxXC2Th.TabIndex = 70;
            this.mtxtBxXC2Th.Text = "1.5";
            // 
            // mtxtBxXC1Th
            // 
            this.mtxtBxXC1Th.Location = new System.Drawing.Point(93, 26);
            this.mtxtBxXC1Th.Name = "mtxtBxXC1Th";
            this.mtxtBxXC1Th.Size = new System.Drawing.Size(54, 20);
            this.mtxtBxXC1Th.TabIndex = 69;
            this.mtxtBxXC1Th.Text = "1.5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "4 and above:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Charge State 3:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Charge State 2:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Charge State 1:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mchkBoxFull);
            this.groupBox1.Controls.Add(this.mchkBoxNone);
            this.groupBox1.Controls.Add(this.mchkBoxPartial);
            this.groupBox1.Location = new System.Drawing.Point(237, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 137);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tryptic State";
            // 
            // mchkBoxFull
            // 
            this.mchkBoxFull.AutoSize = true;
            this.mchkBoxFull.Checked = true;
            this.mchkBoxFull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxFull.Location = new System.Drawing.Point(17, 102);
            this.mchkBoxFull.Name = "mchkBoxFull";
            this.mchkBoxFull.Size = new System.Drawing.Size(42, 17);
            this.mchkBoxFull.TabIndex = 72;
            this.mchkBoxFull.Text = "Full";
            this.mchkBoxFull.UseVisualStyleBackColor = true;
            // 
            // mchkBoxNone
            // 
            this.mchkBoxNone.AutoSize = true;
            this.mchkBoxNone.Checked = true;
            this.mchkBoxNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxNone.Location = new System.Drawing.Point(17, 28);
            this.mchkBoxNone.Name = "mchkBoxNone";
            this.mchkBoxNone.Size = new System.Drawing.Size(52, 17);
            this.mchkBoxNone.TabIndex = 70;
            this.mchkBoxNone.Text = "None";
            this.mchkBoxNone.UseVisualStyleBackColor = true;
            // 
            // mchkBoxPartial
            // 
            this.mchkBoxPartial.AutoSize = true;
            this.mchkBoxPartial.Checked = true;
            this.mchkBoxPartial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxPartial.Location = new System.Drawing.Point(17, 66);
            this.mchkBoxPartial.Name = "mchkBoxPartial";
            this.mchkBoxPartial.Size = new System.Drawing.Size(55, 17);
            this.mchkBoxPartial.TabIndex = 71;
            this.mchkBoxPartial.Text = "Partial";
            this.mchkBoxPartial.UseVisualStyleBackColor = true;
            // 
            // ctlMSMSparaWizPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mgrpBoxXcTh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mBtnBrowse);
            this.Controls.Add(this.mtxtBoxAnFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtxtBoxDCn2Th);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mNumUDXcorRank);
            this.Controls.Add(this.label1);
            this.Name = "ctlMSMSparaWizPage";
            this.Size = new System.Drawing.Size(784, 421);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.ctlMSMSparaWizPage_SetActive);
            this.Controls.SetChildIndex(this.Banner, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.mNumUDXcorRank, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.mtxtBoxDCn2Th, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.mtxtBoxAnFolder, 0);
            this.Controls.SetChildIndex(this.mBtnBrowse, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.mgrpBoxXcTh, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.mNumUDXcorRank)).EndInit();
            this.mgrpBoxXcTh.ResumeLayout(false);
            this.mgrpBoxXcTh.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown mNumUDXcorRank;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mtxtBoxDCn2Th;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtBoxAnFolder;
        private System.Windows.Forms.Button mBtnBrowse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox mgrpBoxXcTh;
        private System.Windows.Forms.TextBox mtxtBxXCOTh;
        private System.Windows.Forms.TextBox mtxtBxXC3Th;
        private System.Windows.Forms.TextBox mtxtBxXC2Th;
        private System.Windows.Forms.TextBox mtxtBxXC1Th;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox mchkBoxNone;
        private System.Windows.Forms.CheckBox mchkBoxPartial;
        private System.Windows.Forms.CheckBox mchkBoxFull;
    }
}