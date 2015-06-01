namespace DAnTE.Inferno
{
    partial class frmFilterAnova
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilterAnova));
            this.label4 = new System.Windows.Forms.Label();
            this.mcmbBoxData = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mlstBoxpvals = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtBoxCutoff = new System.Windows.Forms.TextBox();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrBtnGT = new System.Windows.Forms.RadioButton();
            this.mrBtnLT = new System.Windows.Forms.RadioButton();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "Filter based on p,q values";
            // 
            // mcmbBoxData
            // 
            this.mcmbBoxData.FormattingEnabled = true;
            this.mcmbBoxData.Location = new System.Drawing.Point(108, 60);
            this.mcmbBoxData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mcmbBoxData.Name = "mcmbBoxData";
            this.mcmbBoxData.Size = new System.Drawing.Size(164, 24);
            this.mcmbBoxData.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Data Source:";
            // 
            // mlstBoxpvals
            // 
            this.mlstBoxpvals.FormattingEnabled = true;
            this.mlstBoxpvals.ItemHeight = 16;
            this.mlstBoxpvals.Location = new System.Drawing.Point(35, 134);
            this.mlstBoxpvals.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstBoxpvals.Name = "mlstBoxpvals";
            this.mlstBoxpvals.Size = new System.Drawing.Size(237, 116);
            this.mlstBoxpvals.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 106);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Select a column from p-value table:";
            // 
            // mtxtBoxCutoff
            // 
            this.mtxtBoxCutoff.Location = new System.Drawing.Point(35, 23);
            this.mtxtBoxCutoff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtBoxCutoff.Name = "mtxtBoxCutoff";
            this.mtxtBoxCutoff.Size = new System.Drawing.Size(93, 22);
            this.mtxtBoxCutoff.TabIndex = 36;
            this.mtxtBoxCutoff.Text = "0.05";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(173, 412);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 39;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(35, 412);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(100, 28);
            this.mbtnOK.TabIndex = 38;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mrBtnGT);
            this.groupBox1.Controls.Add(this.mrBtnLT);
            this.groupBox1.Controls.Add(this.mtxtBoxCutoff);
            this.groupBox1.Location = new System.Drawing.Point(35, 258);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(239, 121);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cutoff";
            // 
            // mrBtnGT
            // 
            this.mrBtnGT.AutoSize = true;
            this.mrBtnGT.Location = new System.Drawing.Point(35, 84);
            this.mrBtnGT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnGT.Name = "mrBtnGT";
            this.mrBtnGT.Size = new System.Drawing.Size(110, 21);
            this.mrBtnGT.TabIndex = 38;
            this.mrBtnGT.Text = "Greater than";
            this.mrBtnGT.UseVisualStyleBackColor = true;
            // 
            // mrBtnLT
            // 
            this.mrBtnLT.AutoSize = true;
            this.mrBtnLT.Checked = true;
            this.mrBtnLT.Location = new System.Drawing.Point(35, 55);
            this.mrBtnLT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mrBtnLT.Name = "mrBtnLT";
            this.mrBtnLT.Size = new System.Drawing.Size(91, 21);
            this.mrBtnLT.TabIndex = 37;
            this.mrBtnLT.TabStop = true;
            this.mrBtnLT.Text = "Less than";
            this.mrBtnLT.UseVisualStyleBackColor = true;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(16, 386);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(276, 17);
            this.niceLine2.TabIndex = 37;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 31);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(276, 17);
            this.niceLine1.TabIndex = 31;
            // 
            // frmFilterAnova
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(311, 457);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mlstBoxpvals);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mcmbBoxData);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFilterAnova";
            this.ShowInTaskbar = false;
            this.Text = "p/q Filter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.ComboBox mcmbBoxData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox mlstBoxpvals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mtxtBoxCutoff;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton mrBtnGT;
        private System.Windows.Forms.RadioButton mrBtnLT;
    }
}