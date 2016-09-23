namespace DAnTE.Inferno
{
    partial class frmWilcoxonPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWilcoxonPar));
            this.label1 = new System.Windows.Forms.Label();
            this.mlstBoxFactors = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mNumUpDthres = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mrTBox = new System.Windows.Forms.RichTextBox();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            ((System.ComponentModel.ISupportInitialize)(this.mNumUpDthres)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Parameters for Wilcoxon Test";
            // 
            // mlstBoxFactors
            // 
            this.mlstBoxFactors.FormattingEnabled = true;
            this.mlstBoxFactors.ItemHeight = 16;
            this.mlstBoxFactors.Location = new System.Drawing.Point(47, 228);
            this.mlstBoxFactors.Margin = new System.Windows.Forms.Padding(4);
            this.mlstBoxFactors.Name = "mlstBoxFactors";
            this.mlstBoxFactors.Size = new System.Drawing.Size(271, 164);
            this.mlstBoxFactors.TabIndex = 4;
            this.mlstBoxFactors.SelectedIndexChanged += new System.EventHandler(this.mlstBoxFactors_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 196);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select a Factor (should have only two levels):";
            // 
            // mNumUpDthres
            // 
            this.mNumUpDthres.Location = new System.Drawing.Point(203, 416);
            this.mNumUpDthres.Margin = new System.Windows.Forms.Padding(4);
            this.mNumUpDthres.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.mNumUpDthres.Name = "mNumUpDthres";
            this.mNumUpDthres.Size = new System.Drawing.Size(105, 22);
            this.mNumUpDthres.TabIndex = 62;
            this.mNumUpDthres.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 409);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 43);
            this.label4.TabIndex = 61;
            this.label4.Text = "Minimum Number of Data Points per Factor Level:";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(203, 481);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 68;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(69, 481);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(100, 28);
            this.mbtnOK.TabIndex = 67;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(119, 66);
            this.mlblDataName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(52, 17);
            this.mlblDataName.TabIndex = 70;
            this.mlblDataName.Text = "label8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 69;
            this.label5.Text = "Data source:";
            // 
            // mrTBox
            // 
            this.mrTBox.BackColor = System.Drawing.SystemColors.Control;
            this.mrTBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mrTBox.Location = new System.Drawing.Point(16, 107);
            this.mrTBox.Margin = new System.Windows.Forms.Padding(4);
            this.mrTBox.Name = "mrTBox";
            this.mrTBox.Size = new System.Drawing.Size(343, 86);
            this.mrTBox.TabIndex = 71;
            this.mrTBox.Text = "Wilcoxon Rank Sum test (also known as Mann-Whitney test) is the non parametric eq" +
    "uivalent of a t-test. The normality assumption is relaxed.";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(16, 455);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(343, 17);
            this.niceLine2.TabIndex = 66;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 44);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(343, 17);
            this.niceLine1.TabIndex = 3;
            // 
            // frmWilcoxonPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(376, 526);
            this.Controls.Add(this.mrTBox);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mNumUpDthres);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mlstBoxFactors);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWilcoxonPar";
            this.ShowInTaskbar = false;
            this.Text = "Wilcoxon Rank Sum Test";
            this.Load += new System.EventHandler(this.frmWilcoxonPar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mNumUpDthres)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox mlstBoxFactors;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown mNumUpDthres;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox mrTBox;
    }
}