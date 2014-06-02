namespace DAnTE.Inferno
{
    partial class frmRRollUpPar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRRollUpPar));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mchkBoxCenter = new System.Windows.Forms.CheckBox();
            this.mchkBoxMode = new System.Windows.Forms.CheckBox();
            this.mtxtBoxGpval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtBoxGminP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mchkBoxOneHit = new System.Windows.Forms.CheckBox();
            this.mbtnSelectFolder = new System.Windows.Forms.Button();
            this.mtxtBoxFolder = new System.Windows.Forms.TextBox();
            this.mchkBoxPlot = new System.Windows.Forms.CheckBox();
            this.mtxtBoxMinOlap = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtBoxMinPresent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnDefaults = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mtTipRefRollUp = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mchkBoxCenter);
            this.groupBox1.Controls.Add(this.mchkBoxMode);
            this.groupBox1.Controls.Add(this.mtxtBoxGpval);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.mtxtBoxGminP);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.mchkBoxOneHit);
            this.groupBox1.Controls.Add(this.mbtnSelectFolder);
            this.groupBox1.Controls.Add(this.mtxtBoxFolder);
            this.groupBox1.Controls.Add(this.mchkBoxPlot);
            this.groupBox1.Controls.Add(this.mtxtBoxMinOlap);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mtxtBoxMinPresent);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 272);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Options for Peptide Scaling";
            // 
            // mchkBoxCenter
            // 
            this.mchkBoxCenter.AutoSize = true;
            this.mchkBoxCenter.Location = new System.Drawing.Point(24, 173);
            this.mchkBoxCenter.Name = "mchkBoxCenter";
            this.mchkBoxCenter.Size = new System.Drawing.Size(198, 17);
            this.mchkBoxCenter.TabIndex = 16;
            this.mchkBoxCenter.Text = "Mean Center Peptides to Zero Mean";
            this.mchkBoxCenter.UseVisualStyleBackColor = true;
            // 
            // mchkBoxMode
            // 
            this.mchkBoxMode.AutoSize = true;
            this.mchkBoxMode.Location = new System.Drawing.Point(245, 130);
            this.mchkBoxMode.Name = "mchkBoxMode";
            this.mchkBoxMode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mchkBoxMode.Size = new System.Drawing.Size(179, 17);
            this.mchkBoxMode.TabIndex = 15;
            this.mchkBoxMode.Text = "Rollup as Mean - default Median";
            this.mchkBoxMode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.mchkBoxMode.UseVisualStyleBackColor = true;
            // 
            // mtxtBoxGpval
            // 
            this.mtxtBoxGpval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtBoxGpval.Location = new System.Drawing.Point(411, 82);
            this.mtxtBoxGpval.Name = "mtxtBoxGpval";
            this.mtxtBoxGpval.Size = new System.Drawing.Size(64, 20);
            this.mtxtBoxGpval.TabIndex = 13;
            this.mtxtBoxGpval.Text = "0.05";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(269, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 32);
            this.label2.TabIndex = 14;
            this.label2.Text = "p-value Cutoff for Grubbs\' Test";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mtxtBoxGminP
            // 
            this.mtxtBoxGminP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtBoxGminP.Location = new System.Drawing.Point(411, 37);
            this.mtxtBoxGminP.Name = "mtxtBoxGminP";
            this.mtxtBoxGminP.Size = new System.Drawing.Size(64, 20);
            this.mtxtBoxGminP.TabIndex = 11;
            this.mtxtBoxGminP.Text = "5";
            this.mtxtBoxGminP.TextChanged += new System.EventHandler(this.mtxtBoxGminP_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(251, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 32);
            this.label6.TabIndex = 12;
            this.label6.Text = "Minimum Number of Peptides required for Grubbs\' Test";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mchkBoxOneHit
            // 
            this.mchkBoxOneHit.AutoSize = true;
            this.mchkBoxOneHit.Location = new System.Drawing.Point(49, 130);
            this.mchkBoxOneHit.Name = "mchkBoxOneHit";
            this.mchkBoxOneHit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mchkBoxOneHit.Size = new System.Drawing.Size(146, 17);
            this.mchkBoxOneHit.TabIndex = 10;
            this.mchkBoxOneHit.Text = "Include One-Hit-Wonders";
            this.mchkBoxOneHit.UseVisualStyleBackColor = true;
            // 
            // mbtnSelectFolder
            // 
            this.mbtnSelectFolder.Enabled = false;
            this.mbtnSelectFolder.Location = new System.Drawing.Point(411, 235);
            this.mbtnSelectFolder.Name = "mbtnSelectFolder";
            this.mbtnSelectFolder.Size = new System.Drawing.Size(25, 20);
            this.mbtnSelectFolder.TabIndex = 9;
            this.mbtnSelectFolder.Text = "...";
            this.mbtnSelectFolder.UseVisualStyleBackColor = true;
            this.mbtnSelectFolder.Click += new System.EventHandler(this.mbtnSelectFolder_Click);
            // 
            // mtxtBoxFolder
            // 
            this.mtxtBoxFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtBoxFolder.Enabled = false;
            this.mtxtBoxFolder.Location = new System.Drawing.Point(75, 235);
            this.mtxtBoxFolder.Name = "mtxtBoxFolder";
            this.mtxtBoxFolder.Size = new System.Drawing.Size(329, 20);
            this.mtxtBoxFolder.TabIndex = 8;
            // 
            // mchkBoxPlot
            // 
            this.mchkBoxPlot.AutoSize = true;
            this.mchkBoxPlot.Location = new System.Drawing.Point(24, 212);
            this.mchkBoxPlot.Name = "mchkBoxPlot";
            this.mchkBoxPlot.Size = new System.Drawing.Size(386, 17);
            this.mchkBoxPlot.TabIndex = 7;
            this.mchkBoxPlot.Text = "Plot each Protein/Peptide profile to a folder (WARNING: Could be very slow)";
            this.mchkBoxPlot.UseVisualStyleBackColor = true;
            this.mchkBoxPlot.CheckedChanged += new System.EventHandler(this.mchkBoxPlot_CheckedChanged);
            // 
            // mtxtBoxMinOlap
            // 
            this.mtxtBoxMinOlap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtBoxMinOlap.Location = new System.Drawing.Point(181, 82);
            this.mtxtBoxMinOlap.Name = "mtxtBoxMinOlap";
            this.mtxtBoxMinOlap.Size = new System.Drawing.Size(64, 20);
            this.mtxtBoxMinOlap.TabIndex = 3;
            this.mtxtBoxMinOlap.Text = "3";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 49);
            this.label3.TabIndex = 4;
            this.label3.Text = "Exclude peptides from scaling if they are at least not present in this many datas" +
                "ets";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mtxtBoxMinPresent
            // 
            this.mtxtBoxMinPresent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtBoxMinPresent.Location = new System.Drawing.Point(181, 37);
            this.mtxtBoxMinPresent.Name = "mtxtBoxMinPresent";
            this.mtxtBoxMinPresent.Size = new System.Drawing.Size(64, 20);
            this.mtxtBoxMinPresent.TabIndex = 1;
            this.mtxtBoxMinPresent.Text = "50";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Minimum Presence of at least one Peptide for a Protein (%)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(115, 385);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 5;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(329, 385);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 7;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnDefaults
            // 
            this.mbtnDefaults.Location = new System.Drawing.Point(226, 385);
            this.mbtnDefaults.Name = "mbtnDefaults";
            this.mbtnDefaults.Size = new System.Drawing.Size(75, 23);
            this.mbtnDefaults.TabIndex = 8;
            this.mbtnDefaults.Text = "Defaults";
            this.mbtnDefaults.UseVisualStyleBackColor = true;
            this.mbtnDefaults.Click += new System.EventHandler(this.mbtnDefaults_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Data Source:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(299, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "RRollup - Reference Peptide Based Scaling, Rollup";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(294, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(207, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "This method assumes that the data is in log scale.";
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(94, 68);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 55;
            this.mlblDataName.Text = "label8";
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(11, 40);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(501, 15);
            this.niceLine1.TabIndex = 20;
            // 
            // frmRRollUpPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 420);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mbtnDefaults);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRRollUpPar";
            this.ShowInTaskbar = false;
            this.Text = "RRollup Options";
            this.Load += new System.EventHandler(this.frmRRollUpPar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtBoxMinPresent;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.TextBox mtxtBoxMinOlap;
        private System.Windows.Forms.Button mbtnDefaults;
        private System.Windows.Forms.Button mbtnSelectFolder;
        private System.Windows.Forms.TextBox mtxtBoxFolder;
        private System.Windows.Forms.CheckBox mchkBoxPlot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip mtTipRefRollUp;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox mchkBoxOneHit;
        private System.Windows.Forms.TextBox mtxtBoxGpval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mtxtBoxGminP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox mchkBoxMode;
        private System.Windows.Forms.CheckBox mchkBoxCenter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label mlblDataName;
    }
}