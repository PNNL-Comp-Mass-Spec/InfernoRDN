namespace DAnTE.Inferno
{
    partial class frmFilterBasedOnRowIDs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFilterBasedOnRowIDs));
            this.label4 = new System.Windows.Forms.Label();
            this.mcmbBoxData = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "Filter based on Selected RowIDs";
            // 
            // mcmbBoxData
            // 
            this.mcmbBoxData.FormattingEnabled = true;
            this.mcmbBoxData.Location = new System.Drawing.Point(108, 60);
            this.mcmbBoxData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mcmbBoxData.Name = "mcmbBoxData";
            this.mcmbBoxData.Size = new System.Drawing.Size(228, 24);
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
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(224, 119);
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
            this.mbtnOK.Location = new System.Drawing.Point(60, 119);
            this.mbtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(100, 28);
            this.mbtnOK.TabIndex = 38;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // niceLine2
            // 
            this.niceLine2.Location = new System.Drawing.Point(16, 94);
            this.niceLine2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(359, 17);
            this.niceLine2.TabIndex = 37;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 31);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(359, 17);
            this.niceLine1.TabIndex = 31;
            // 
            // frmFilterBasedOnRowIDs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(391, 164);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mcmbBoxData);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFilterBasedOnRowIDs";
            this.ShowInTaskbar = false;
            this.Text = "Filter on Row IDs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.ComboBox mcmbBoxData;
        private System.Windows.Forms.Label label5;
        private DAnTE.ExtraControls.NiceLine niceLine2;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
    }
}