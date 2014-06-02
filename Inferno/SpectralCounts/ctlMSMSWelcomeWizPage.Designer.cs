namespace DAnTE.Inferno
{
    partial class ctlMSMSWelcomeWizPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.mrBtnSynOut = new System.Windows.Forms.RadioButton();
            this.mrBtnDBase = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mrBtnSeqOut = new System.Windows.Forms.RadioButton();
            this.mtxtBoxSeqOutFolder = new System.Windows.Forms.TextBox();
            this.mBtnOutBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(195, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select a Method to Load Files:";
            // 
            // mrBtnSynOut
            // 
            this.mrBtnSynOut.AutoSize = true;
            this.mrBtnSynOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrBtnSynOut.Location = new System.Drawing.Point(217, 215);
            this.mrBtnSynOut.Name = "mrBtnSynOut";
            this.mrBtnSynOut.Size = new System.Drawing.Size(427, 19);
            this.mrBtnSynOut.TabIndex = 3;
            this.mrBtnSynOut.Text = "Select a Series of *_out.txt or Synopsis (*_syn.txt) Files from a Local Folder";
            this.mrBtnSynOut.UseVisualStyleBackColor = true;
            // 
            // mrBtnDBase
            // 
            this.mrBtnDBase.AutoSize = true;
            this.mrBtnDBase.Checked = true;
            this.mrBtnDBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrBtnDBase.Location = new System.Drawing.Point(217, 162);
            this.mrBtnDBase.Name = "mrBtnDBase";
            this.mrBtnDBase.Size = new System.Drawing.Size(236, 19);
            this.mrBtnDBase.TabIndex = 4;
            this.mrBtnDBase.TabStop = true;
            this.mrBtnDBase.Text = "Select Files from the LabKey Database";
            this.mrBtnDBase.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(195, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(332, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "This Wizard will guid you to load a series of Sequest output files to obtain the " +
                "Spectral count data.";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 237);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(406, 34);
            this.label3.TabIndex = 6;
            this.label3.Text = "*_out.txt files and Synopsis files (*_syn.txt) are generated from *.Out files usi" +
                "ng the \"Peptide File Extractor\" application (available at http://omics.pnl.gov/s" +
                "oftware).";
            // 
            // mrBtnSeqOut
            // 
            this.mrBtnSeqOut.AutoSize = true;
            this.mrBtnSeqOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrBtnSeqOut.Location = new System.Drawing.Point(217, 285);
            this.mrBtnSeqOut.Name = "mrBtnSeqOut";
            this.mrBtnSeqOut.Size = new System.Drawing.Size(283, 19);
            this.mrBtnSeqOut.TabIndex = 7;
            this.mrBtnSeqOut.Text = "Choose a Local Folder with Sequest *.Out Files:";
            this.mrBtnSeqOut.UseVisualStyleBackColor = true;
            this.mrBtnSeqOut.CheckedChanged += new System.EventHandler(this.mrBtnSeqOut_CheckedChanged);
            // 
            // mtxtBoxSeqOutFolder
            // 
            this.mtxtBoxSeqOutFolder.Location = new System.Drawing.Point(249, 322);
            this.mtxtBoxSeqOutFolder.Name = "mtxtBoxSeqOutFolder";
            this.mtxtBoxSeqOutFolder.Size = new System.Drawing.Size(380, 20);
            this.mtxtBoxSeqOutFolder.TabIndex = 8;
            // 
            // mBtnOutBrowse
            // 
            this.mBtnOutBrowse.Location = new System.Drawing.Point(635, 320);
            this.mBtnOutBrowse.Name = "mBtnOutBrowse";
            this.mBtnOutBrowse.Size = new System.Drawing.Size(29, 23);
            this.mBtnOutBrowse.TabIndex = 9;
            this.mBtnOutBrowse.Text = "...";
            this.mBtnOutBrowse.UseVisualStyleBackColor = true;
            this.mBtnOutBrowse.Click += new System.EventHandler(this.mBtnOutBrowse_Click);
            // 
            // ctlMSMSWelcomeWizPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mBtnOutBrowse);
            this.Controls.Add(this.mtxtBoxSeqOutFolder);
            this.Controls.Add(this.mrBtnSeqOut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mrBtnDBase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mrBtnSynOut);
            this.Name = "ctlMSMSWelcomeWizPage";
            this.Size = new System.Drawing.Size(784, 421);
            this.SetActive += new System.ComponentModel.CancelEventHandler(this.ctlAnovaWelcomeWizPage_SetActive);
            this.Controls.SetChildIndex(this.mrBtnSynOut, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.mrBtnDBase, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.mrBtnSeqOut, 0);
            this.Controls.SetChildIndex(this.mtxtBoxSeqOutFolder, 0);
            this.Controls.SetChildIndex(this.mBtnOutBrowse, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton mrBtnSynOut;
        private System.Windows.Forms.RadioButton mrBtnDBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton mrBtnSeqOut;
        private System.Windows.Forms.TextBox mtxtBoxSeqOutFolder;
        private System.Windows.Forms.Button mBtnOutBrowse;
    }
}