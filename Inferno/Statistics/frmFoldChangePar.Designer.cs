namespace DAnTE.Inferno
{
    partial class frmFoldChangePar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFoldChangePar));
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mlblPickFactor = new System.Windows.Forms.Label();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mlstBoxFactrVals = new System.Windows.Forms.ListBox();
            this.mtxtBoxVal1 = new System.Windows.Forms.TextBox();
            this.mtxtBoxVal2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mBtnVal1 = new System.Windows.Forms.Button();
            this.mBtnVal2 = new System.Windows.Forms.Button();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mchkBoxLogScale = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Calculate Fold Change";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(173, 292);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 37;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(45, 292);
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
            this.mlblDataName.Location = new System.Drawing.Point(88, 53);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 57;
            this.mlblDataName.Text = "label8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Data Source:";
            // 
            // mlblPickFactor
            // 
            this.mlblPickFactor.AutoSize = true;
            this.mlblPickFactor.Location = new System.Drawing.Point(9, 114);
            this.mlblPickFactor.Name = "mlblPickFactor";
            this.mlblPickFactor.Size = new System.Drawing.Size(73, 13);
            this.mlblPickFactor.TabIndex = 58;
            this.mlblPickFactor.Text = "Select Factor:";
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(12, 130);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(120, 21);
            this.mcmbBoxFactors.TabIndex = 59;
            this.mcmbBoxFactors.SelectedIndexChanged += new System.EventHandler(this.mcmbBoxFactors_SelectedIndexChanged);
            // 
            // mlstBoxFactrVals
            // 
            this.mlstBoxFactrVals.FormattingEnabled = true;
            this.mlstBoxFactrVals.Location = new System.Drawing.Point(12, 157);
            this.mlstBoxFactrVals.Name = "mlstBoxFactrVals";
            this.mlstBoxFactrVals.Size = new System.Drawing.Size(120, 108);
            this.mlstBoxFactrVals.TabIndex = 60;
            // 
            // mtxtBoxVal1
            // 
            this.mtxtBoxVal1.Location = new System.Drawing.Point(181, 175);
            this.mtxtBoxVal1.Name = "mtxtBoxVal1";
            this.mtxtBoxVal1.ReadOnly = true;
            this.mtxtBoxVal1.Size = new System.Drawing.Size(75, 20);
            this.mtxtBoxVal1.TabIndex = 61;
            // 
            // mtxtBoxVal2
            // 
            this.mtxtBoxVal2.Location = new System.Drawing.Point(181, 228);
            this.mtxtBoxVal2.Name = "mtxtBoxVal2";
            this.mtxtBoxVal2.ReadOnly = true;
            this.mtxtBoxVal2.Size = new System.Drawing.Size(75, 20);
            this.mtxtBoxVal2.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Vs.";
            // 
            // mBtnVal1
            // 
            this.mBtnVal1.Location = new System.Drawing.Point(138, 173);
            this.mBtnVal1.Name = "mBtnVal1";
            this.mBtnVal1.Size = new System.Drawing.Size(37, 23);
            this.mBtnVal1.TabIndex = 64;
            this.mBtnVal1.Text = ">>";
            this.mBtnVal1.UseVisualStyleBackColor = true;
            this.mBtnVal1.Click += new System.EventHandler(this.mBtnVal1_Click);
            // 
            // mBtnVal2
            // 
            this.mBtnVal2.Location = new System.Drawing.Point(138, 226);
            this.mBtnVal2.Name = "mBtnVal2";
            this.mBtnVal2.Size = new System.Drawing.Size(37, 23);
            this.mBtnVal2.TabIndex = 65;
            this.mBtnVal2.Text = ">>";
            this.mBtnVal2.UseVisualStyleBackColor = true;
            this.mBtnVal2.Click += new System.EventHandler(this.mBtnVal2_Click);
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(9, 271);
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
            // mchkBoxLogScale
            // 
            this.mchkBoxLogScale.AutoSize = true;
            this.mchkBoxLogScale.Checked = true;
            this.mchkBoxLogScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxLogScale.Location = new System.Drawing.Point(15, 82);
            this.mchkBoxLogScale.Name = "mchkBoxLogScale";
            this.mchkBoxLogScale.Size = new System.Drawing.Size(115, 17);
            this.mchkBoxLogScale.TabIndex = 66;
            this.mchkBoxLogScale.Text = "Data is in log scale";
            this.mchkBoxLogScale.UseVisualStyleBackColor = true;
            // 
            // frmFoldChangePar
            // 
            this.AcceptButton = this.mbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(292, 328);
            this.Controls.Add(this.mchkBoxLogScale);
            this.Controls.Add(this.mBtnVal2);
            this.Controls.Add(this.mBtnVal1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mtxtBoxVal2);
            this.Controls.Add(this.mtxtBoxVal1);
            this.Controls.Add(this.mlstBoxFactrVals);
            this.Controls.Add(this.mlblPickFactor);
            this.Controls.Add(this.mcmbBoxFactors);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFoldChangePar";
            this.ShowInTaskbar = false;
            this.Text = "Fold Change";
            this.Load += new System.EventHandler(this.frmFoldChangePar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mlblPickFactor;
        private System.Windows.Forms.ComboBox mcmbBoxFactors;
        private System.Windows.Forms.ListBox mlstBoxFactrVals;
        private System.Windows.Forms.TextBox mtxtBoxVal1;
        private System.Windows.Forms.TextBox mtxtBoxVal2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mBtnVal1;
        private System.Windows.Forms.Button mBtnVal2;
        private System.Windows.Forms.CheckBox mchkBoxLogScale;
    }
}