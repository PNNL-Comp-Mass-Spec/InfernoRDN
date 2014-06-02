namespace DAnTE.Inferno
{
    partial class frmMeanCenterPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeanCenterPar));
            this.label1 = new System.Windows.Forms.Label();
            this.mrbtnMean = new System.Windows.Forms.RadioButton();
            this.mrbtnMedian = new System.Windows.Forms.RadioButton();
            this.mchkboxCenterZ = new System.Windows.Forms.CheckBox();
            this.mlblDesc = new System.Windows.Forms.Label();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mrbtnSubtract = new System.Windows.Forms.RadioButton();
            this.mrbtnDivide = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Central Tendancy Adjustment";
            // 
            // mrbtnMean
            // 
            this.mrbtnMean.AutoSize = true;
            this.mrbtnMean.Checked = true;
            this.mrbtnMean.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrbtnMean.Location = new System.Drawing.Point(38, 25);
            this.mrbtnMean.Name = "mrbtnMean";
            this.mrbtnMean.Size = new System.Drawing.Size(52, 17);
            this.mrbtnMean.TabIndex = 2;
            this.mrbtnMean.TabStop = true;
            this.mrbtnMean.Text = "Mean";
            this.mrbtnMean.UseVisualStyleBackColor = true;
            // 
            // mrbtnMedian
            // 
            this.mrbtnMedian.AutoSize = true;
            this.mrbtnMedian.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrbtnMedian.Location = new System.Drawing.Point(38, 59);
            this.mrbtnMedian.Name = "mrbtnMedian";
            this.mrbtnMedian.Size = new System.Drawing.Size(60, 17);
            this.mrbtnMedian.TabIndex = 3;
            this.mrbtnMedian.Text = "Median";
            this.mrbtnMedian.UseVisualStyleBackColor = true;
            // 
            // mchkboxCenterZ
            // 
            this.mchkboxCenterZ.AutoSize = true;
            this.mchkboxCenterZ.Location = new System.Drawing.Point(33, 204);
            this.mchkboxCenterZ.Name = "mchkboxCenterZ";
            this.mchkboxCenterZ.Size = new System.Drawing.Size(125, 17);
            this.mchkboxCenterZ.TabIndex = 4;
            this.mchkboxCenterZ.Text = "New Center at Zero?";
            this.mchkboxCenterZ.UseVisualStyleBackColor = true;
            // 
            // mlblDesc
            // 
            this.mlblDesc.Location = new System.Drawing.Point(30, 233);
            this.mlblDesc.Name = "mlblDesc";
            this.mlblDesc.Size = new System.Drawing.Size(250, 114);
            this.mlblDesc.TabIndex = 5;
            this.mlblDesc.Text = "label2";
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(56, 371);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 6;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(178, 371);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 7;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mrbtnSubtract
            // 
            this.mrbtnSubtract.AutoSize = true;
            this.mrbtnSubtract.Checked = true;
            this.mrbtnSubtract.Location = new System.Drawing.Point(36, 62);
            this.mrbtnSubtract.Name = "mrbtnSubtract";
            this.mrbtnSubtract.Size = new System.Drawing.Size(65, 17);
            this.mrbtnSubtract.TabIndex = 1;
            this.mrbtnSubtract.TabStop = true;
            this.mrbtnSubtract.Text = "Subtract";
            this.mrbtnSubtract.UseVisualStyleBackColor = true;
            // 
            // mrbtnDivide
            // 
            this.mrbtnDivide.AutoSize = true;
            this.mrbtnDivide.Location = new System.Drawing.Point(36, 26);
            this.mrbtnDivide.Name = "mrbtnDivide";
            this.mrbtnDivide.Size = new System.Drawing.Size(55, 17);
            this.mrbtnDivide.TabIndex = 0;
            this.mrbtnDivide.Text = "Divide";
            this.mrbtnDivide.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.mrbtnSubtract);
            this.groupBox3.Controls.Add(this.mrbtnDivide);
            this.groupBox3.Location = new System.Drawing.Point(15, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 125);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Adjustment";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mrbtnMean);
            this.groupBox4.Controls.Add(this.mrbtnMedian);
            this.groupBox4.Location = new System.Drawing.Point(135, 26);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(126, 93);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tendancy";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 350);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(277, 15);
            this.niceLine2.TabIndex = 8;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(277, 15);
            this.niceLine1.TabIndex = 1;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(90, 47);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 54;
            this.mlblDataName.Text = "label8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Data Source:";
            // 
            // frmMeanCenterPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 407);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.mlblDesc);
            this.Controls.Add(this.mchkboxCenterZ);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeanCenterPar";
            this.ShowInTaskbar = false;
            this.Text = "Select Options";
            this.Load += new System.EventHandler(this.frmMeanCenterPar_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.RadioButton mrbtnMean;
        private System.Windows.Forms.RadioButton mrbtnMedian;
        private System.Windows.Forms.CheckBox mchkboxCenterZ;
        private System.Windows.Forms.Label mlblDesc;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.RadioButton mrbtnSubtract;
        private System.Windows.Forms.RadioButton mrbtnDivide;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label2;
    }
}