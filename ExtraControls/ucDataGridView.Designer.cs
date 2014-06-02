namespace DAnTE.ExtraControls
{
    partial class ucDataGridView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dAnTEdatagridview1 = new DAnTE.ExtraControls.DAnTEdatagridview();
            ((System.ComponentModel.ISupportInitialize)(this.dAnTEdatagridview1)).BeginInit();
            this.SuspendLayout();
            // 
            // dAnTEdatagridview1
            // 
            this.dAnTEdatagridview1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dAnTEdatagridview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dAnTEdatagridview1.Location = new System.Drawing.Point(0, 0);
            this.dAnTEdatagridview1.Name = "dAnTEdatagridview1";
            this.dAnTEdatagridview1.Size = new System.Drawing.Size(582, 613);
            this.dAnTEdatagridview1.TabIndex = 0;
            this.dAnTEdatagridview1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dAnTEdatagridview1_CellMouseDown);
            // 
            // ucDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dAnTEdatagridview1);
            this.Name = "ucDataGridView";
            this.Size = new System.Drawing.Size(582, 613);
            ((System.ComponentModel.ISupportInitialize)(this.dAnTEdatagridview1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DAnTEdatagridview dAnTEdatagridview1;

    }
}
