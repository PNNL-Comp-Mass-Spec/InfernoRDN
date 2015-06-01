namespace DAnTE.Inferno
{
    partial class frmMergeColsPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMergeColsPar));
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrBtnMedian = new System.Windows.Forms.RadioButton();
            this.mrBtnMean = new System.Windows.Forms.RadioButton();
            this.mrBtnSum = new System.Windows.Forms.RadioButton();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(60, 89);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(124, 21);
            this.mcmbBoxFactors.TabIndex = 17;
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(33, 73);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(73, 13);
            this.mlblPickFactor.TabIndex = 16;
            this.mlblPickFactor.Text = "Pick a Factor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Merge Columns";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mrBtnMedian);
            this.groupBox1.Controls.Add(this.mrBtnMean);
            this.groupBox1.Controls.Add(this.mrBtnSum);
            this.groupBox1.Location = new System.Drawing.Point(62, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 105);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // mrBtnMedian
            // 
            this.mrBtnMedian.AutoSize = true;
            this.mrBtnMedian.Location = new System.Drawing.Point(34, 46);
            this.mrBtnMedian.Name = "mrBtnMedian";
            this.mrBtnMedian.Size = new System.Drawing.Size(60, 17);
            this.mrBtnMedian.TabIndex = 2;
            this.mrBtnMedian.Text = "Median";
            this.mrBtnMedian.UseVisualStyleBackColor = true;
            // 
            // mrBtnMean
            // 
            this.mrBtnMean.AutoSize = true;
            this.mrBtnMean.Checked = true;
            this.mrBtnMean.Location = new System.Drawing.Point(34, 19);
            this.mrBtnMean.Name = "mrBtnMean";
            this.mrBtnMean.Size = new System.Drawing.Size(52, 17);
            this.mrBtnMean.TabIndex = 1;
            this.mrBtnMean.TabStop = true;
            this.mrBtnMean.Text = "Mean";
            this.mrBtnMean.UseVisualStyleBackColor = true;
            // 
            // mrBtnSum
            // 
            this.mrBtnSum.AutoSize = true;
            this.mrBtnSum.Location = new System.Drawing.Point(34, 72);
            this.mrBtnSum.Name = "mrBtnSum";
            this.mrBtnSum.Size = new System.Drawing.Size(46, 17);
            this.mrBtnSum.TabIndex = 0;
            this.mrBtnSum.Text = "Sum";
            this.mrBtnSum.UseVisualStyleBackColor = true;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(133, 259);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 26;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(31, 259);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 25;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(109, 48);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 54;
            this.mlblDataName.Text = "label8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Data Source:";
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 238);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(222, 15);
            this.niceLine1.TabIndex = 27;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 30);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(222, 15);
            this.niceLine2.TabIndex = 23;
            // 
            // frmMergeColsPar
            // 
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(246, 297);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mcmbBoxFactors);
            this.Controls.Add(this.mlblPickFactor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMergeColsPar";
            this.ShowInTaskbar = false;
            this.Text = "Merge Columns";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.Label mlblPickFactor;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mrBtnMean;
        private System.Windows.Forms.RadioButton mrBtnSum;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton mrBtnMedian;
    }
}