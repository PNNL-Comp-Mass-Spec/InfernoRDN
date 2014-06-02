namespace DAnTE.Inferno
{
    partial class frmAbundanceFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbundanceFilter));
            this.mtxtBoxFthres = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrdBtnMean = new System.Windows.Forms.RadioButton();
            this.mrdBtnMax = new System.Windows.Forms.RadioButton();
            this.mrdBtnMin = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxtBoxFthres
            // 
            this.mtxtBoxFthres.Location = new System.Drawing.Point(520, 416);
            this.mtxtBoxFthres.Name = "mtxtBoxFthres";
            this.mtxtBoxFthres.Size = new System.Drawing.Size(63, 20);
            this.mtxtBoxFthres.TabIndex = 41;
            this.mtxtBoxFthres.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Missing Percentage Allowed (%):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Filter Abundances";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(179, 523);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 37;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(51, 523);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 36;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(117, 53);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 57;
            this.mlblDataName.Text = "label8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Data Source:";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 502);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(268, 15);
            this.niceLine2.TabIndex = 43;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(268, 15);
            this.niceLine1.TabIndex = 38;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mrdBtnMin);
            this.groupBox1.Controls.Add(this.mrdBtnMax);
            this.groupBox1.Controls.Add(this.mrdBtnMean);
            this.groupBox1.Location = new System.Drawing.Point(15, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 136);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose a row statistic:";
            // 
            // mrdBtnMean
            // 
            this.mrdBtnMean.AutoSize = true;
            this.mrdBtnMean.Location = new System.Drawing.Point(36, 29);
            this.mrdBtnMean.Name = "mrdBtnMean";
            this.mrdBtnMean.Size = new System.Drawing.Size(77, 17);
            this.mrdBtnMean.TabIndex = 0;
            this.mrdBtnMean.TabStop = true;
            this.mrdBtnMean.Text = "Row Mean";
            this.mrdBtnMean.UseVisualStyleBackColor = true;
            // 
            // mrdBtnMax
            // 
            this.mrdBtnMax.AutoSize = true;
            this.mrdBtnMax.Location = new System.Drawing.Point(36, 64);
            this.mrdBtnMax.Name = "mrdBtnMax";
            this.mrdBtnMax.Size = new System.Drawing.Size(94, 17);
            this.mrdBtnMax.TabIndex = 1;
            this.mrdBtnMax.TabStop = true;
            this.mrdBtnMax.Text = "Row Maximum";
            this.mrdBtnMax.UseVisualStyleBackColor = true;
            // 
            // mrdBtnMin
            // 
            this.mrdBtnMin.AutoSize = true;
            this.mrdBtnMin.Location = new System.Drawing.Point(36, 101);
            this.mrdBtnMin.Name = "mrdBtnMin";
            this.mrdBtnMin.Size = new System.Drawing.Size(91, 17);
            this.mrdBtnMin.TabIndex = 2;
            this.mrdBtnMin.TabStop = true;
            this.mrdBtnMin.Text = "Row Minimum";
            this.mrdBtnMin.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 106);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cutoff";
            // 
            // frmAbundanceFilter
            // 
            this.AcceptButton = this.mbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(683, 557);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mtxtBoxFthres);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbundanceFilter";
            this.ShowInTaskbar = false;
            this.Text = "Abundances";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.TextBox mtxtBoxFthres;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mrdBtnMax;
        private System.Windows.Forms.RadioButton mrdBtnMean;
        private System.Windows.Forms.RadioButton mrdBtnMin;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}