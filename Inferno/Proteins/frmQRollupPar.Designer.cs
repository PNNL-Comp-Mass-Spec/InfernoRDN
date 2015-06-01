namespace DAnTE.Inferno
{
    partial class frmQRollupPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQRollupPar));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mrbtnMean = new System.Windows.Forms.RadioButton();
            this.mrbtnMedian = new System.Windows.Forms.RadioButton();
            this.mtxtBoxThres = new System.Windows.Forms.TextBox();
            this.mtxtBoxMinPresent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mchkBoxOneHit = new System.Windows.Forms.CheckBox();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtxtBoxNum = new System.Windows.Forms.TextBox();
            this.mrBtnNum = new System.Windows.Forms.RadioButton();
            this.mrBtnPerct = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "QRollup";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mrbtnMean);
            this.groupBox2.Controls.Add(this.mrbtnMedian);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(26, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 101);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Method";
            // 
            // mrbtnMean
            // 
            this.mrbtnMean.AutoSize = true;
            this.mrbtnMean.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrbtnMean.Location = new System.Drawing.Point(26, 31);
            this.mrbtnMean.Name = "mrbtnMean";
            this.mrbtnMean.Size = new System.Drawing.Size(52, 17);
            this.mrbtnMean.TabIndex = 2;
            this.mrbtnMean.Text = "Mean";
            this.mrbtnMean.UseVisualStyleBackColor = true;
            // 
            // mrbtnMedian
            // 
            this.mrbtnMedian.AutoSize = true;
            this.mrbtnMedian.Checked = true;
            this.mrbtnMedian.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrbtnMedian.Location = new System.Drawing.Point(26, 65);
            this.mrbtnMedian.Name = "mrbtnMedian";
            this.mrbtnMedian.Size = new System.Drawing.Size(60, 17);
            this.mrbtnMedian.TabIndex = 3;
            this.mrbtnMedian.TabStop = true;
            this.mrbtnMedian.Text = "Median";
            this.mrbtnMedian.UseVisualStyleBackColor = true;
            // 
            // mtxtBoxThres
            // 
            this.mtxtBoxThres.Location = new System.Drawing.Point(131, 30);
            this.mtxtBoxThres.Name = "mtxtBoxThres";
            this.mtxtBoxThres.Size = new System.Drawing.Size(48, 20);
            this.mtxtBoxThres.TabIndex = 5;
            this.mtxtBoxThres.Text = "33";
            // 
            // mtxtBoxMinPresent
            // 
            this.mtxtBoxMinPresent.Location = new System.Drawing.Point(153, 286);
            this.mtxtBoxMinPresent.Name = "mtxtBoxMinPresent";
            this.mtxtBoxMinPresent.Size = new System.Drawing.Size(32, 20);
            this.mtxtBoxMinPresent.TabIndex = 7;
            this.mtxtBoxMinPresent.Text = "50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Minimum Presense (%)";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(235, 333);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 11;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(113, 333);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 10;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Data Source:";
            // 
            // niceLine3
            // 
            this.niceLine3.Location = new System.Drawing.Point(15, 67);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(389, 15);
            this.niceLine3.TabIndex = 22;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(11, 312);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(389, 15);
            this.niceLine2.TabIndex = 12;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(15, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(389, 15);
            this.niceLine1.TabIndex = 1;
            // 
            // mchkBoxOneHit
            // 
            this.mchkBoxOneHit.AutoSize = true;
            this.mchkBoxOneHit.Location = new System.Drawing.Point(223, 289);
            this.mchkBoxOneHit.Name = "mchkBoxOneHit";
            this.mchkBoxOneHit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxOneHit.Size = new System.Drawing.Size(146, 17);
            this.mchkBoxOneHit.TabIndex = 23;
            this.mchkBoxOneHit.Text = "Include One-Hit-Wonders";
            this.mchkBoxOneHit.UseVisualStyleBackColor = true;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(99, 43);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 55;
            this.mlblDataName.Text = "label8";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mtxtBoxNum);
            this.groupBox1.Controls.Add(this.mrBtnNum);
            this.groupBox1.Controls.Add(this.mrBtnPerct);
            this.groupBox1.Controls.Add(this.mtxtBoxThres);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(190, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Top Peptides";
            // 
            // mtxtBoxNum
            // 
            this.mtxtBoxNum.Location = new System.Drawing.Point(131, 64);
            this.mtxtBoxNum.Name = "mtxtBoxNum";
            this.mtxtBoxNum.Size = new System.Drawing.Size(48, 20);
            this.mtxtBoxNum.TabIndex = 6;
            this.mtxtBoxNum.Text = "3";
            // 
            // mrBtnNum
            // 
            this.mrBtnNum.AutoSize = true;
            this.mrBtnNum.Location = new System.Drawing.Point(28, 65);
            this.mrBtnNum.Name = "mrBtnNum";
            this.mrBtnNum.Size = new System.Drawing.Size(62, 17);
            this.mrBtnNum.TabIndex = 1;
            this.mrBtnNum.Text = "Number";
            this.mrBtnNum.UseVisualStyleBackColor = true;
            this.mrBtnNum.CheckedChanged += new System.EventHandler(this.mrBtnNum_CheckedChanged);
            // 
            // mrBtnPerct
            // 
            this.mrBtnPerct.AutoSize = true;
            this.mrBtnPerct.Checked = true;
            this.mrBtnPerct.Location = new System.Drawing.Point(28, 31);
            this.mrBtnPerct.Name = "mrBtnPerct";
            this.mrBtnPerct.Size = new System.Drawing.Size(97, 17);
            this.mrBtnPerct.TabIndex = 0;
            this.mrBtnPerct.TabStop = true;
            this.mrBtnPerct.Text = "Percentage (%)";
            this.mrBtnPerct.UseVisualStyleBackColor = true;
            this.mrBtnPerct.CheckedChanged += new System.EventHandler(this.mrBtnPerct_CheckedChanged);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(29, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(361, 73);
            this.label2.TabIndex = 57;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // frmQRollupPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(419, 369);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.mchkBoxOneHit);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.mtxtBoxMinPresent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQRollupPar";
            this.ShowInTaskbar = false;
            this.Text = "QRollup Options";
            this.Load += new System.EventHandler(this.frmQRollupPar_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton mrbtnMean;
        private System.Windows.Forms.RadioButton mrbtnMedian;
        private System.Windows.Forms.TextBox mtxtBoxThres;
        private System.Windows.Forms.TextBox mtxtBoxMinPresent;
        private System.Windows.Forms.Label label3;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label label5;
        private ExtraControls.NiceLine niceLine3;
        private System.Windows.Forms.CheckBox mchkBoxOneHit;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox mtxtBoxNum;
        private System.Windows.Forms.RadioButton mrBtnNum;
        private System.Windows.Forms.RadioButton mrBtnPerct;
        private System.Windows.Forms.Label label2;
    }
}