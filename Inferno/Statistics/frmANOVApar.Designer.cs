namespace DAnTE.Inferno
{
    partial class frmANOVApar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmANOVApar));
            this.label1 = new System.Windows.Forms.Label();
            this.mlstBoxFactors = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mbtnFixedUnselect = new System.Windows.Forms.Button();
            this.mbtnFixedSelect = new System.Windows.Forms.Button();
            this.mlstBoxFixed = new System.Windows.Forms.ListBox();
            this.mBtnRandomUnselect = new System.Windows.Forms.Button();
            this.mBtnRandomSelect = new System.Windows.Forms.Button();
            this.mlstBoxRandom = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mchkBoxRandom = new System.Windows.Forms.CheckBox();
            this.mNumUpDthres = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.mchkBoxREML = new System.Windows.Forms.CheckBox();
            this.mchkBoxUnbalanced = new System.Windows.Forms.CheckBox();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mchkBoxInteractions = new System.Windows.Forms.CheckBox();
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
            this.label1.Size = new System.Drawing.Size(315, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Parameters for Hypothesis Testing:";
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
            this.mlstBoxFixed.Size = new System.Drawing.Size(247, 100);
            this.mlstBoxFixed.TabIndex = 14;
            // 
            // mBtnRandomUnselect
            // 
            this.mBtnRandomUnselect.Enabled = false;
            this.mBtnRandomUnselect.Location = new System.Drawing.Point(309, 330);
            this.mBtnRandomUnselect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mBtnRandomUnselect.Name = "mBtnRandomUnselect";
            this.mBtnRandomUnselect.Size = new System.Drawing.Size(56, 28);
            this.mBtnRandomUnselect.TabIndex = 19;
            this.mBtnRandomUnselect.Text = "<<";
            this.mBtnRandomUnselect.UseVisualStyleBackColor = true;
            this.mBtnRandomUnselect.Click += new System.EventHandler(this.mbtnRandomUnselect_Click);
            // 
            // mBtnRandomSelect
            // 
            this.mBtnRandomSelect.Location = new System.Drawing.Point(309, 294);
            this.mBtnRandomSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mBtnRandomSelect.Name = "mBtnRandomSelect";
            this.mBtnRandomSelect.Size = new System.Drawing.Size(56, 28);
            this.mBtnRandomSelect.TabIndex = 18;
            this.mBtnRandomSelect.Text = ">>";
            this.mBtnRandomSelect.UseVisualStyleBackColor = true;
            this.mBtnRandomSelect.Click += new System.EventHandler(this.mbtnRandomSelect_Click);
            // 
            // mlstBoxRandom
            // 
            this.mlstBoxRandom.FormattingEnabled = true;
            this.mlstBoxRandom.HorizontalScrollbar = true;
            this.mlstBoxRandom.ItemHeight = 16;
            this.mlstBoxRandom.Location = new System.Drawing.Point(401, 276);
            this.mlstBoxRandom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstBoxRandom.Name = "mlstBoxRandom";
            this.mlstBoxRandom.Size = new System.Drawing.Size(247, 100);
            this.mlstBoxRandom.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Fixed Effects:";
            // 
            // mchkBoxRandom
            // 
            this.mchkBoxRandom.AutoSize = true;
            this.mchkBoxRandom.Location = new System.Drawing.Point(384, 247);
            this.mchkBoxRandom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mchkBoxRandom.Name = "mchkBoxRandom";
            this.mchkBoxRandom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mchkBoxRandom.Size = new System.Drawing.Size(134, 21);
            this.mchkBoxRandom.TabIndex = 22;
            this.mchkBoxRandom.Text = "Random Effects:";
            this.mchkBoxRandom.UseVisualStyleBackColor = true;
            this.mchkBoxRandom.Click += new System.EventHandler(this.SelectRandomEff_event);
            // 
            // mNumUpDthres
            // 
            this.mNumUpDthres.Location = new System.Drawing.Point(208, 409);
            this.mNumUpDthres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.label4.Location = new System.Drawing.Point(25, 401);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(175, 43);
            this.label4.TabIndex = 61;
            this.label4.Text = "Minimum Number of Data Points per Factor Level";
            // 
            // mchkBoxREML
            // 
            this.mchkBoxREML.AutoSize = true;
            this.mchkBoxREML.Checked = true;
            this.mchkBoxREML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxREML.Location = new System.Drawing.Point(401, 384);
            this.mchkBoxREML.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mchkBoxREML.Name = "mchkBoxREML";
            this.mchkBoxREML.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxREML.Size = new System.Drawing.Size(194, 21);
            this.mchkBoxREML.TabIndex = 63;
            this.mchkBoxREML.Text = "Use REML (otherwise ML)";
            this.mchkBoxREML.UseVisualStyleBackColor = true;
            // 
            // mchkBoxUnbalanced
            // 
            this.mchkBoxUnbalanced.AutoSize = true;
            this.mchkBoxUnbalanced.Location = new System.Drawing.Point(36, 490);
            this.mchkBoxUnbalanced.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mchkBoxUnbalanced.Name = "mchkBoxUnbalanced";
            this.mchkBoxUnbalanced.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxUnbalanced.Size = new System.Drawing.Size(504, 21);
            this.mchkBoxUnbalanced.TabIndex = 64;
            this.mchkBoxUnbalanced.Text = "Treat Data as Unbalanced (use \'Marginal Sums of Squares\' i.e. Type III SS)";
            this.mchkBoxUnbalanced.UseVisualStyleBackColor = true;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(375, 544);
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
            this.mbtnOK.Location = new System.Drawing.Point(208, 544);
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
            // mchkBoxInteractions
            // 
            this.mchkBoxInteractions.AutoSize = true;
            this.mchkBoxInteractions.Location = new System.Drawing.Point(36, 462);
            this.mchkBoxInteractions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mchkBoxInteractions.Name = "mchkBoxInteractions";
            this.mchkBoxInteractions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxInteractions.Size = new System.Drawing.Size(152, 21);
            this.mchkBoxInteractions.TabIndex = 71;
            this.mchkBoxInteractions.Text = "Include Interactions";
            this.mchkBoxInteractions.UseVisualStyleBackColor = true;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(20, 518);
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
            // frmANOVApar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(675, 587);
            this.Controls.Add(this.mchkBoxInteractions);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mchkBoxUnbalanced);
            this.Controls.Add(this.mchkBoxREML);
            this.Controls.Add(this.mNumUpDthres);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mchkBoxRandom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mBtnRandomUnselect);
            this.Controls.Add(this.mBtnRandomSelect);
            this.Controls.Add(this.mlstBoxRandom);
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
            this.Name = "frmANOVApar";
            this.ShowInTaskbar = false;
            this.Text = "ANOVA";
            this.Load += new System.EventHandler(this.frmANOVApar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mNumUpDthres)).EndInit();
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
        private System.Windows.Forms.Button mBtnRandomUnselect;
        private System.Windows.Forms.Button mBtnRandomSelect;
        private System.Windows.Forms.ListBox mlstBoxRandom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox mchkBoxRandom;
        private System.Windows.Forms.NumericUpDown mNumUpDthres;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox mchkBoxREML;
        private System.Windows.Forms.CheckBox mchkBoxUnbalanced;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblDataName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox mchkBoxInteractions;
    }
}