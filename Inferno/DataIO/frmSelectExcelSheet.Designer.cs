namespace DAnTE.Tools
{
    partial class frmSelectExcelSheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectExcelSheet));
            this.mBtnOK = new System.Windows.Forms.Button();
            this.mlstBoxSheets = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.SuspendLayout();
            // 
            // mBtnOK
            // 
            this.mBtnOK.Location = new System.Drawing.Point(440, 73);
            this.mBtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mBtnOK.Name = "mBtnOK";
            this.mBtnOK.Size = new System.Drawing.Size(100, 28);
            this.mBtnOK.TabIndex = 1;
            this.mBtnOK.Text = "OK";
            this.mBtnOK.UseVisualStyleBackColor = true;
            this.mBtnOK.Click += new System.EventHandler(this.mBtnOK_Click);
            // 
            // mlstBoxSheets
            // 
            this.mlstBoxSheets.FormattingEnabled = true;
            this.mlstBoxSheets.ItemHeight = 16;
            this.mlstBoxSheets.Location = new System.Drawing.Point(40, 57);
            this.mlstBoxSheets.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mlstBoxSheets.Name = "mlstBoxSheets";
            this.mlstBoxSheets.Size = new System.Drawing.Size(336, 132);
            this.mlstBoxSheets.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 48;
            this.label1.Text = "Select Excel Sheet";
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(440, 135);
            this.mbtnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(100, 28);
            this.mbtnCancel.TabIndex = 50;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(16, 197);
            this.niceLine1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(567, 17);
            this.niceLine1.TabIndex = 51;
            // 
            // niceLine4
            // 
            this.niceLine4.Location = new System.Drawing.Point(16, 31);
            this.niceLine4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(567, 17);
            this.niceLine4.TabIndex = 49;
            // 
            // frmSelectExcelSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(599, 224);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mlstBoxSheets);
            this.Controls.Add(this.mBtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectExcelSheet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Excel Sheet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mBtnOK;
        private System.Windows.Forms.ListBox mlstBoxSheets;
        private DAnTE.ExtraControls.NiceLine niceLine4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button mbtnCancel;
        private DAnTE.ExtraControls.NiceLine niceLine1;
    }
}