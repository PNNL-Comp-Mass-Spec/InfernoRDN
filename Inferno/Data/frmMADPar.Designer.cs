using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    partial class frmMADPar
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

        private Button mbtnOK;
        private Button mbtnCancel;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMADPar));
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.mlblDataName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mcmbBoxFactors = new System.Windows.Forms.ComboBox();
            this.mchkBoxMeanAdj = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(60, 177);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 2;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(197, 177);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 3;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Data Source:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Median Absolute Deviation (MAD)";
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(12, 25);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(315, 15);
            this.niceLine2.TabIndex = 21;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 156);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(315, 15);
            this.niceLine1.TabIndex = 4;
            // 
            // mlblDataName
            // 
            this.mlblDataName.AutoSize = true;
            this.mlblDataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblDataName.Location = new System.Drawing.Point(94, 43);
            this.mlblDataName.Name = "mlblDataName";
            this.mlblDataName.Size = new System.Drawing.Size(41, 13);
            this.mlblDataName.TabIndex = 55;
            this.mlblDataName.Text = "label8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Within a Factor:";
            // 
            // mcmbBoxFactors
            // 
            this.mcmbBoxFactors.FormattingEnabled = true;
            this.mcmbBoxFactors.Location = new System.Drawing.Point(109, 77);
            this.mcmbBoxFactors.Name = "mcmbBoxFactors";
            this.mcmbBoxFactors.Size = new System.Drawing.Size(127, 21);
            this.mcmbBoxFactors.TabIndex = 57;
            // 
            // mchkBoxMeanAdj
            // 
            this.mchkBoxMeanAdj.AutoSize = true;
            this.mchkBoxMeanAdj.Location = new System.Drawing.Point(24, 122);
            this.mchkBoxMeanAdj.Name = "mchkBoxMeanAdj";
            this.mchkBoxMeanAdj.Size = new System.Drawing.Size(197, 17);
            this.mchkBoxMeanAdj.TabIndex = 58;
            this.mchkBoxMeanAdj.Text = "Mean of the Datasets is around zero";
            this.mchkBoxMeanAdj.UseVisualStyleBackColor = true;
            // 
            // frmMADPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(341, 213);
            this.Controls.Add(this.mchkBoxMeanAdj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mcmbBoxFactors);
            this.Controls.Add(this.mlblDataName);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMADPar";
            this.ShowInTaskbar = false;
            this.Text = "MAD";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ExtraControls.NiceLine niceLine1;
        #endregion

        private Label label2;
        private Label label3;
        private ExtraControls.NiceLine niceLine2;
        private Label mlblDataName;
        private Label label1;
        private ComboBox mcmbBoxFactors;
        private CheckBox mchkBoxMeanAdj;
    }
}