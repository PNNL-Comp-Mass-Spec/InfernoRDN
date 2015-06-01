namespace DAnTE.Inferno
{
    partial class frmTamuQpar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTamuQpar));
            this.label1 = new System.Windows.Forms.Label();
            this.mlstBoxFactors = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mbtnFixedUnselect = new System.Windows.Forms.Button();
            this.mbtnFixedSelect = new System.Windows.Forms.Button();
            this.mlstBoxFixed = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select a Factor:";
            // 
            // mlstBoxFactors
            // 
            this.mlstBoxFactors.FormattingEnabled = true;
            this.mlstBoxFactors.ItemHeight = 16;
            this.mlstBoxFactors.Location = new System.Drawing.Point(36, 132);
            this.mlstBoxFactors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstBoxFactors.Name = "mlstBoxFactors";
            this.mlstBoxFactors.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.mlstBoxFactors.Size = new System.Drawing.Size(237, 244);
            this.mlstBoxFactors.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Available Factors:";
            // 
            // mbtnFixedUnselect
            // 
            this.mbtnFixedUnselect.Enabled = false;
            this.mbtnFixedUnselect.Location = new System.Drawing.Point(309, 182);
            this.mbtnFixedUnselect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnFixedUnselect.Name = "mbtnFixedUnselect";
            this.mbtnFixedUnselect.Size = new System.Drawing.Size(56, 28);
            this.mbtnFixedUnselect.TabIndex = 16;
            this.mbtnFixedUnselect.Text = "<<";
            this.mbtnFixedUnselect.UseVisualStyleBackColor = true;
            this.mbtnFixedUnselect.Click += new System.EventHandler(this.mbtnFixedUnselect_Click);
            // 
            // mbtnFixedSelect
            // 
            this.mbtnFixedSelect.Location = new System.Drawing.Point(309, 146);
            this.mbtnFixedSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnFixedSelect.Name = "mbtnFixedSelect";
            this.mbtnFixedSelect.Size = new System.Drawing.Size(56, 28);
            this.mbtnFixedSelect.TabIndex = 15;
            this.mbtnFixedSelect.Text = ">>";
            this.mbtnFixedSelect.UseVisualStyleBackColor = true;
            this.mbtnFixedSelect.Click += new System.EventHandler(this.mbtnFixedSelect_Click);
            // 
            // mlstBoxFixed
            // 
            this.mlstBoxFixed.FormattingEnabled = true;
            this.mlstBoxFixed.HorizontalScrollbar = true;
            this.mlstBoxFixed.ItemHeight = 16;
            this.mlstBoxFixed.Location = new System.Drawing.Point(401, 128);
            this.mlstBoxFixed.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstBoxFixed.Name = "mlstBoxFixed";
            this.mlstBoxFixed.Size = new System.Drawing.Size(247, 244);
            this.mlstBoxFixed.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Selected Effects (only one please):";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(375, 410);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 68;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(208, 410);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(20, 384);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(629, 17);
            this.niceLine2.TabIndex = 66;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 44);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(629, 17);
            this.niceLine1.TabIndex = 3;
            // 
            // frmTamuQpar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(675, 454);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mbtnFixedUnselect);
            this.Controls.Add(this.mbtnFixedSelect);
            this.Controls.Add(this.mlstBoxFixed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mlstBoxFactors);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTamuQpar";
            this.ShowInTaskbar = false;
            this.Text = "TAMUQ";
            this.Load += new System.EventHandler(this.frmANOVApar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox mlstBoxFactors;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button mbtnFixedUnselect;
        private System.Windows.Forms.Button mbtnFixedSelect;
        private System.Windows.Forms.ListBox mlstBoxFixed;
        private System.Windows.Forms.Label label3;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label5;
    }
}