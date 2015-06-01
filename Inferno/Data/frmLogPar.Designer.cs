namespace DAnTE.Inferno
{
    partial class frmLogPar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogPar));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrBtnLogn = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mrBtnAdd = new System.Windows.Forms.RadioButton();
            this.mrBtnMult = new System.Windows.Forms.RadioButton();
            this.mtxtBoxBias = new System.Windows.Forms.TextBox();
            this.mrBtnLog10 = new System.Windows.Forms.RadioButton();
            this.mrBtnLog2 = new System.Windows.Forms.RadioButton();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Log Transform Parameters";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mrBtnLogn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.mrBtnLog10);
            this.groupBox1.Controls.Add(this.mrBtnLog2);
            this.groupBox1.Location = new System.Drawing.Point(15, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 176);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base";
            // 
            // mrBtnLogn
            // 
            this.mrBtnLogn.AutoSize = true;
            this.mrBtnLogn.Location = new System.Drawing.Point(19, 78);
            this.mrBtnLogn.Name = "mrBtnLogn";
            this.mrBtnLogn.Size = new System.Drawing.Size(80, 17);
            this.mrBtnLogn.TabIndex = 58;
            this.mrBtnLogn.Text = "Natural Log";
            this.mrBtnLogn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 57);
            this.label2.TabIndex = 56;
            this.label2.Text = "Bias will be added/multiplied before log transforming.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mrBtnAdd);
            this.groupBox2.Controls.Add(this.mrBtnMult);
            this.groupBox2.Controls.Add(this.mtxtBoxBias);
            this.groupBox2.Location = new System.Drawing.Point(102, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 123);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bias";
            // 
            // mrBtnAdd
            // 
            this.mrBtnAdd.AutoSize = true;
            this.mrBtnAdd.Location = new System.Drawing.Point(24, 81);
            this.mrBtnAdd.Name = "mrBtnAdd";
            this.mrBtnAdd.Size = new System.Drawing.Size(44, 17);
            this.mrBtnAdd.TabIndex = 52;
            this.mrBtnAdd.Text = "Add";
            this.mrBtnAdd.UseVisualStyleBackColor = true;
            // 
            // mrBtnMult
            // 
            this.mrBtnMult.AutoSize = true;
            this.mrBtnMult.Checked = true;
            this.mrBtnMult.Location = new System.Drawing.Point(24, 45);
            this.mrBtnMult.Name = "mrBtnMult";
            this.mrBtnMult.Size = new System.Drawing.Size(60, 17);
            this.mrBtnMult.TabIndex = 51;
            this.mrBtnMult.TabStop = true;
            this.mrBtnMult.Text = "Multiply";
            this.mrBtnMult.UseVisualStyleBackColor = true;
            // 
            // mtxtBoxBias
            // 
            this.mtxtBoxBias.Location = new System.Drawing.Point(40, 16);
            this.mtxtBoxBias.Name = "mtxtBoxBias";
            this.mtxtBoxBias.Size = new System.Drawing.Size(61, 20);
            this.mtxtBoxBias.TabIndex = 50;
            this.mtxtBoxBias.Text = "1";
            // 
            // mrBtnLog10
            // 
            this.mrBtnLog10.AutoSize = true;
            this.mrBtnLog10.Location = new System.Drawing.Point(19, 48);
            this.mrBtnLog10.Name = "mrBtnLog10";
            this.mrBtnLog10.Size = new System.Drawing.Size(55, 17);
            this.mrBtnLog10.TabIndex = 50;
            this.mrBtnLog10.Text = "Log10";
            this.mrBtnLog10.UseVisualStyleBackColor = true;
            // 
            // mrBtnLog2
            // 
            this.mrBtnLog2.AutoSize = true;
            this.mrBtnLog2.Checked = true;
            this.mrBtnLog2.Location = new System.Drawing.Point(19, 19);
            this.mrBtnLog2.Name = "mrBtnLog2";
            this.mrBtnLog2.Size = new System.Drawing.Size(49, 17);
            this.mrBtnLog2.TabIndex = 49;
            this.mrBtnLog2.TabStop = true;
            this.mrBtnLog2.Text = "Log2";
            this.mrBtnLog2.UseVisualStyleBackColor = true;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(99, 43);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 52;
            this.mlblDataName.Text = "label8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Data Source:";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(150, 274);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(76, 23);
            this.mbtnCancel.TabIndex = 54;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(44, 274);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(76, 23);
            this.mbtnOK.TabIndex = 53;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(15, 251);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(227, 15);
            this.niceLine2.TabIndex = 55;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 25);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(230, 15);
            this.niceLine1.TabIndex = 0;
            // 
            // frmLogPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(255, 314);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.niceLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogPar";
            this.ShowInTaskbar = false;
            this.Text = "Log Transform";
            this.Load += new System.EventHandler(this.frmLogPar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mrBtnLog10;
        private System.Windows.Forms.RadioButton mrBtnLog2;
        private System.Windows.Forms.TextBox mtxtBoxBias;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton mrBtnAdd;
        private System.Windows.Forms.RadioButton mrBtnMult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton mrBtnLogn;
    }
}