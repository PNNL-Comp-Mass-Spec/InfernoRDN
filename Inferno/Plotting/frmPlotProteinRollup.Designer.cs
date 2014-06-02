namespace DAnTE.Inferno
{
    partial class frmPlotProteinRollup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlotProteinRollup));
            this.mbtnPlot = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mlstBoxProteins = new System.Windows.Forms.ListBox();
            this.mcmbBoxPData = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mcmbBoxData = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mchkBoxXlabels = new System.Windows.Forms.CheckBox();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.mNiceLineProts = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.SuspendLayout();
            // 
            // mbtnPlot
            // 
            this.mbtnPlot.Location = new System.Drawing.Point(40, 455);
            this.mbtnPlot.Name = "mbtnPlot";
            this.mbtnPlot.Size = new System.Drawing.Size(75, 23);
            this.mbtnPlot.TabIndex = 1;
            this.mbtnPlot.Text = "Plot";
            this.mbtnPlot.UseVisualStyleBackColor = true;
            this.mbtnPlot.Click += new System.EventHandler(this.mbtnPlot_Click);
            // 
            // mbtnClose
            // 
            this.mbtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnClose.Location = new System.Drawing.Point(149, 455);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(75, 23);
            this.mbtnClose.TabIndex = 2;
            this.mbtnClose.Text = "Close";
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            // 
            // mlstBoxProteins
            // 
            this.mlstBoxProteins.Location = new System.Drawing.Point(13, 134);
            this.mlstBoxProteins.Name = "mlstBoxProteins";
            this.mlstBoxProteins.Size = new System.Drawing.Size(234, 277);
            this.mlstBoxProteins.TabIndex = 4;
            this.mlstBoxProteins.DoubleClick += new System.EventHandler(this.mbtnPlot_Click);
            // 
            // mcmbBoxPData
            // 
            this.mcmbBoxPData.FormattingEnabled = true;
            this.mcmbBoxPData.Location = new System.Drawing.Point(122, 85);
            this.mcmbBoxPData.Name = "mcmbBoxPData";
            this.mcmbBoxPData.Size = new System.Drawing.Size(124, 21);
            this.mcmbBoxPData.TabIndex = 23;
            this.mcmbBoxPData.SelectionChangeCommitted += new System.EventHandler(this.mcmbBoxData_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Protein Data:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Plot Protein Rollup Data";
            // 
            // mcmbBoxData
            // 
            this.mcmbBoxData.FormattingEnabled = true;
            this.mcmbBoxData.Location = new System.Drawing.Point(122, 50);
            this.mcmbBoxData.Name = "mcmbBoxData";
            this.mcmbBoxData.Size = new System.Drawing.Size(124, 21);
            this.mcmbBoxData.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Expression Data used:";
            // 
            // mchkBoxXlabels
            // 
            this.mchkBoxXlabels.AutoSize = true;
            this.mchkBoxXlabels.Checked = true;
            this.mchkBoxXlabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mchkBoxXlabels.Location = new System.Drawing.Point(14, 421);
            this.mchkBoxXlabels.Name = "mchkBoxXlabels";
            this.mchkBoxXlabels.Size = new System.Drawing.Size(175, 17);
            this.mchkBoxXlabels.TabIndex = 28;
            this.mchkBoxXlabels.Text = "Show Dataset Names on X-axis";
            this.mchkBoxXlabels.UseVisualStyleBackColor = true;
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 24);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(234, 15);
            this.niceLine2.TabIndex = 24;
            // 
            // mNiceLineProts
            // 
            this.mNiceLineProts.Caption = "Available Proteins";
            this.mNiceLineProts.Location = new System.Drawing.Point(12, 112);
            this.mNiceLineProts.Name = "mNiceLineProts";
            this.mNiceLineProts.Size = new System.Drawing.Size(236, 15);
            this.mNiceLineProts.TabIndex = 5;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(14, 434);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(234, 15);
            this.niceLine1.TabIndex = 3;
            // 
            // frmPlotProteinRollup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 490);
            this.Controls.Add(this.mchkBoxXlabels);
            this.Controls.Add(this.mcmbBoxData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.mcmbBoxPData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mNiceLineProts);
            this.Controls.Add(this.mlstBoxProteins);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnClose);
            this.Controls.Add(this.mbtnPlot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlotProteinRollup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plot Protein Rollup Values";
            this.Load += new System.EventHandler(this.frmPlotProteinRollup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mbtnPlot;
        private System.Windows.Forms.Button mbtnClose;
        private ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.ListBox mlstBoxProteins;
        private ExtraControls.NiceLine mNiceLineProts;
        private System.Windows.Forms.ComboBox mcmbBoxPData;
        private System.Windows.Forms.Label label5;
        private ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mcmbBoxData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox mchkBoxXlabels;
    }
}