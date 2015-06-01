namespace DAnTE.Inferno
{
    partial class frmSelectColumns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectColumns));
            this.mlstBoxAllCols = new System.Windows.Forms.ListBox();
            this.mlstBoxMT = new System.Windows.Forms.ListBox();
            this.mlstBoxIPI = new System.Windows.Forms.ListBox();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mchkBoxIPI = new System.Windows.Forms.CheckBox();
            this.mlstBoxData = new System.Windows.Forms.ListBox();
            this.mbtnDataSelect = new System.Windows.Forms.Button();
            this.mbtnDataUnselect = new System.Windows.Forms.Button();
            this.mbtnMTUnselect = new System.Windows.Forms.Button();
            this.mbtnMTSelect = new System.Windows.Forms.Button();
            this.mbtnIPIUnselect = new System.Windows.Forms.Button();
            this.mbtnIPISelect = new System.Windows.Forms.Button();
            this.niceLine6 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine5 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine4 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine3 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine2 = new DAnTE.ExtraControls.NiceLine();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.SuspendLayout();
            // 
            // mlstBoxAllCols
            // 
            this.mlstBoxAllCols.FormattingEnabled = true;
            this.mlstBoxAllCols.HorizontalScrollbar = true;
            this.mlstBoxAllCols.Location = new System.Drawing.Point(30, 50);
            this.mlstBoxAllCols.Name = "mlstBoxAllCols";
            this.mlstBoxAllCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.mlstBoxAllCols.Size = new System.Drawing.Size(250, 407);
            this.mlstBoxAllCols.TabIndex = 0;
            // 
            // mlstBoxMT
            // 
            this.mlstBoxMT.FormattingEnabled = true;
            this.mlstBoxMT.HorizontalScrollbar = true;
            this.mlstBoxMT.Location = new System.Drawing.Point(365, 53);
            this.mlstBoxMT.Name = "mlstBoxMT";
            this.mlstBoxMT.Size = new System.Drawing.Size(242, 56);
            this.mlstBoxMT.TabIndex = 0;
            // 
            // mlstBoxIPI
            // 
            this.mlstBoxIPI.FormattingEnabled = true;
            this.mlstBoxIPI.HorizontalScrollbar = true;
            this.mlstBoxIPI.Location = new System.Drawing.Point(366, 154);
            this.mlstBoxIPI.Name = "mlstBoxIPI";
            this.mlstBoxIPI.Size = new System.Drawing.Size(242, 56);
            this.mlstBoxIPI.TabIndex = 1;
            // 
            // mbtnOK
            // 
            this.mbtnOK.Location = new System.Drawing.Point(223, 496);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 5;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(348, 496);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 6;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mchkBoxIPI
            // 
            this.mchkBoxIPI.AutoSize = true;
            this.mchkBoxIPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mchkBoxIPI.Location = new System.Drawing.Point(388, 122);
            this.mchkBoxIPI.Name = "mchkBoxIPI";
            this.mchkBoxIPI.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mchkBoxIPI.Size = new System.Drawing.Size(199, 17);
            this.mchkBoxIPI.TabIndex = 7;
            this.mchkBoxIPI.Text = "Protein ID (Only for Proteomics Data)";
            this.mchkBoxIPI.UseVisualStyleBackColor = true;
            this.mchkBoxIPI.CheckStateChanged += new System.EventHandler(this.SelectIPI_event);
            // 
            // mlstBoxData
            // 
            this.mlstBoxData.FormattingEnabled = true;
            this.mlstBoxData.HorizontalScrollbar = true;
            this.mlstBoxData.Location = new System.Drawing.Point(366, 259);
            this.mlstBoxData.Name = "mlstBoxData";
            this.mlstBoxData.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.mlstBoxData.Size = new System.Drawing.Size(242, 199);
            this.mlstBoxData.TabIndex = 8;
            // 
            // mbtnDataSelect
            // 
            this.mbtnDataSelect.Location = new System.Drawing.Point(305, 321);
            this.mbtnDataSelect.Name = "mbtnDataSelect";
            this.mbtnDataSelect.Size = new System.Drawing.Size(37, 23);
            this.mbtnDataSelect.TabIndex = 10;
            this.mbtnDataSelect.Text = ">>";
            this.mbtnDataSelect.UseVisualStyleBackColor = true;
            this.mbtnDataSelect.Click += new System.EventHandler(this.mbtnDataSelect_Click);
            // 
            // mbtnDataUnselect
            // 
            this.mbtnDataUnselect.Enabled = false;
            this.mbtnDataUnselect.Location = new System.Drawing.Point(305, 350);
            this.mbtnDataUnselect.Name = "mbtnDataUnselect";
            this.mbtnDataUnselect.Size = new System.Drawing.Size(37, 23);
            this.mbtnDataUnselect.TabIndex = 11;
            this.mbtnDataUnselect.Text = "<<";
            this.mbtnDataUnselect.UseVisualStyleBackColor = true;
            this.mbtnDataUnselect.Click += new System.EventHandler(this.mbtnDataUnselect_Click);
            // 
            // mbtnMTUnselect
            // 
            this.mbtnMTUnselect.Enabled = false;
            this.mbtnMTUnselect.Location = new System.Drawing.Point(305, 83);
            this.mbtnMTUnselect.Name = "mbtnMTUnselect";
            this.mbtnMTUnselect.Size = new System.Drawing.Size(37, 23);
            this.mbtnMTUnselect.TabIndex = 13;
            this.mbtnMTUnselect.Text = "<<";
            this.mbtnMTUnselect.UseVisualStyleBackColor = true;
            this.mbtnMTUnselect.Click += new System.EventHandler(this.mbtnMTUnselect_Click);
            // 
            // mbtnMTSelect
            // 
            this.mbtnMTSelect.Location = new System.Drawing.Point(305, 57);
            this.mbtnMTSelect.Name = "mbtnMTSelect";
            this.mbtnMTSelect.Size = new System.Drawing.Size(37, 23);
            this.mbtnMTSelect.TabIndex = 12;
            this.mbtnMTSelect.Text = ">>";
            this.mbtnMTSelect.UseVisualStyleBackColor = true;
            this.mbtnMTSelect.Click += new System.EventHandler(this.mbtnMTSelect_Click);
            // 
            // mbtnIPIUnselect
            // 
            this.mbtnIPIUnselect.Enabled = false;
            this.mbtnIPIUnselect.Location = new System.Drawing.Point(305, 183);
            this.mbtnIPIUnselect.Name = "mbtnIPIUnselect";
            this.mbtnIPIUnselect.Size = new System.Drawing.Size(37, 23);
            this.mbtnIPIUnselect.TabIndex = 15;
            this.mbtnIPIUnselect.Text = "<<";
            this.mbtnIPIUnselect.UseVisualStyleBackColor = true;
            this.mbtnIPIUnselect.Click += new System.EventHandler(this.mbtnIPIUnselect_Click);
            // 
            // mbtnIPISelect
            // 
            this.mbtnIPISelect.Location = new System.Drawing.Point(305, 158);
            this.mbtnIPISelect.Name = "mbtnIPISelect";
            this.mbtnIPISelect.Size = new System.Drawing.Size(37, 23);
            this.mbtnIPISelect.TabIndex = 14;
            this.mbtnIPISelect.Text = ">>";
            this.mbtnIPISelect.UseVisualStyleBackColor = true;
            this.mbtnIPISelect.Click += new System.EventHandler(this.mbtnIPISelect_Click);
            // 
            // niceLine6
            // 
            this.niceLine6.Location = new System.Drawing.Point(365, 122);
            this.niceLine6.Name = "niceLine6";
            this.niceLine6.Size = new System.Drawing.Size(17, 15);
            this.niceLine6.TabIndex = 21;
            // 
            // niceLine5
            // 
            this.niceLine5.Location = new System.Drawing.Point(553, 122);
            this.niceLine5.Name = "niceLine5";
            this.niceLine5.Size = new System.Drawing.Size(74, 15);
            this.niceLine5.TabIndex = 20;
            // 
            // niceLine4
            // 
            this.niceLine4.Caption = "Unique Row ID (ex: ID for peptides)";
            this.niceLine4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.niceLine4.Location = new System.Drawing.Point(366, 22);
            this.niceLine4.Name = "niceLine4";
            this.niceLine4.Size = new System.Drawing.Size(262, 15);
            this.niceLine4.TabIndex = 19;
            // 
            // niceLine3
            // 
            this.niceLine3.Caption = "Data Columns";
            this.niceLine3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.niceLine3.Location = new System.Drawing.Point(366, 231);
            this.niceLine3.Name = "niceLine3";
            this.niceLine3.Size = new System.Drawing.Size(262, 15);
            this.niceLine3.TabIndex = 18;
            // 
            // niceLine2
            // 
            this.niceLine2.Caption = "Available Columns";
            this.niceLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.niceLine2.Location = new System.Drawing.Point(12, 22);
            this.niceLine2.Name = "niceLine2";
            this.niceLine2.Size = new System.Drawing.Size(268, 15);
            this.niceLine2.TabIndex = 17;
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 463);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(616, 15);
            this.niceLine1.TabIndex = 16;
            // 
            // frmSelectColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(640, 536);
            this.Controls.Add(this.mchkBoxIPI);
            this.Controls.Add(this.niceLine6);
            this.Controls.Add(this.niceLine5);
            this.Controls.Add(this.niceLine4);
            this.Controls.Add(this.niceLine3);
            this.Controls.Add(this.niceLine2);
            this.Controls.Add(this.niceLine1);
            this.Controls.Add(this.mbtnIPIUnselect);
            this.Controls.Add(this.mbtnIPISelect);
            this.Controls.Add(this.mbtnMTUnselect);
            this.Controls.Add(this.mbtnMTSelect);
            this.Controls.Add(this.mbtnDataUnselect);
            this.Controls.Add(this.mbtnDataSelect);
            this.Controls.Add(this.mlstBoxData);
            this.Controls.Add(this.mbtnCancel);
            this.Controls.Add(this.mbtnOK);
            this.Controls.Add(this.mlstBoxIPI);
            this.Controls.Add(this.mlstBoxAllCols);
            this.Controls.Add(this.mlstBoxMT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectColumns";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Columns";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mlstBoxAllCols;
        private System.Windows.Forms.ListBox mlstBoxMT;
        private System.Windows.Forms.ListBox mlstBoxIPI;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.CheckBox mchkBoxIPI;
        private System.Windows.Forms.ListBox mlstBoxData;
        private System.Windows.Forms.Button mbtnDataSelect;
        private System.Windows.Forms.Button mbtnDataUnselect;
        private System.Windows.Forms.Button mbtnMTUnselect;
        private System.Windows.Forms.Button mbtnMTSelect;
        private System.Windows.Forms.Button mbtnIPIUnselect;
        private System.Windows.Forms.Button mbtnIPISelect;
        private ExtraControls.NiceLine niceLine1;
        private ExtraControls.NiceLine niceLine2;
        private ExtraControls.NiceLine niceLine3;
        private ExtraControls.NiceLine niceLine4;
        private ExtraControls.NiceLine niceLine5;
        private ExtraControls.NiceLine niceLine6;
    }
}