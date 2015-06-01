namespace DAnTE.Inferno
{
    partial class frmAnalysisSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalysisSummary));
            this.mlstViewSummary = new System.Windows.Forms.ListView();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.label4 = new System.Windows.Forms.Label();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mBtnSave = new System.Windows.Forms.Button();
            this.mlblDataFile = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mlblTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mlstViewSummary
            // 
            this.mlstViewSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mlstViewSummary.Location = new System.Drawing.Point(16, 122);
            this.mlstViewSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstViewSummary.Name = "mlstViewSummary";
            this.mlstViewSummary.Size = new System.Drawing.Size(1035, 473);
            this.mlstViewSummary.TabIndex = 0;
            this.mlstViewSummary.UseCompatibleStateImageBehavior = false;
            this.mlstViewSummary.View = System.Windows.Forms.View.Details;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(336, 635);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(100, 28);
            this.mbtnOK.TabIndex = 1;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            // 
            // niceLine2
            // 
            this.niceLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine2.Location = new System.Drawing.Point(16, 603);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(1036, 17);
            this.niceLine2.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 17);
            this.label4.TabIndex = 46;
            this.label4.Text = "Summary of Your Analysis";
            // 
            // niceLine1
            // 
            this.niceLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.niceLine1.Location = new System.Drawing.Point(20, 31);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(1032, 17);
            this.niceLine1.TabIndex = 45;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(623, 635);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 44;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            // 
            // mBtnSave
            // 
            this.mBtnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mBtnSave.Location = new System.Drawing.Point(461, 635);
            this.mBtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mBtnSave.Name = "mBtnSave";
            this.mBtnSave.Size = new System.Drawing.Size(137, 28);
            this.mBtnSave.TabIndex = 48;
            this.mBtnSave.Text = "Save Summary";
            this.mBtnSave.UseVisualStyleBackColor = true;
            this.mBtnSave.Click += new System.EventHandler(this.mBtnSave_Click);
            // 
            // mlblDataFile
            // 
            this.mlblDataFile.AutoSize = true;
            this.mlblDataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataFile.Location = new System.Drawing.Point(109, 53);
            this.mlblDataFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblDataFile.Name = "mlblDataFile";
            this.mlblDataFile.Size = new System.Drawing.Size(46, 17);
            this.mlblDataFile.TabIndex = 59;
            this.mlblDataFile.Text = "label8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 58;
            this.label5.Text = "Data File:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 60;
            this.label1.Text = "Time:";
            // 
            // mlblTime
            // 
            this.mlblTime.AutoSize = true;
            this.mlblTime.Location = new System.Drawing.Point(109, 79);
            this.mlblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblTime.Name = "mlblTime";
            this.mlblTime.Size = new System.Drawing.Size(35, 17);
            this.mlblTime.TabIndex = 61;
            this.mlblTime.Text = "Now";
            // 
            // frmAnalysisSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(1068, 689);
            this.Controls.Add(this.mlblTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mlblDataFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mBtnSave);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.mlstViewSummary);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmAnalysisSummary";
            this.Text = "Analysis Summary";
            this.Load += new System.EventHandler(this.frmAnalysisSummary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView mlstViewSummary;
        private System.Windows.Forms.Button mbtnOK;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mBtnSave;
        private System.Windows.Forms.Label mlblDataFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label mlblTime;
    }
}