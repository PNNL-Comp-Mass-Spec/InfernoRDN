namespace DAnTE.Inferno
{
    partial class frmArrangeColumns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArrangeColumns));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.niceLine1 = new DAnTE.ExtraControls.NiceLine();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mbtnDel = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnDown = new System.Windows.Forms.Button();
            this.mbtnUp = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mlistViewDatasets = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.niceLine1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(627, 55);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Arrange Dataset Order";
            // 
            // niceLine1
            // 
            this.niceLine1.Location = new System.Drawing.Point(12, 27);
            this.niceLine1.Name = "niceLine1";
            this.niceLine1.Size = new System.Drawing.Size(603, 15);
            this.niceLine1.TabIndex = 33;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mbtnDel);
            this.panel2.Controls.Add(this.mbtnCancel);
            this.panel2.Controls.Add(this.mbtnOK);
            this.panel2.Controls.Add(this.mbtnDown);
            this.panel2.Controls.Add(this.mbtnUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(507, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 406);
            this.panel2.TabIndex = 1;
            // 
            // mbtnDel
            // 
            this.mbtnDel.Image = ((System.Drawing.Image)(resources.GetObject("mbtnDel.Image")));
            this.mbtnDel.Location = new System.Drawing.Point(39, 99);
            this.mbtnDel.Name = "mbtnDel";
            this.mbtnDel.Size = new System.Drawing.Size(45, 23);
            this.mbtnDel.TabIndex = 4;
            this.mbtnDel.UseVisualStyleBackColor = true;
            this.mbtnDel.Click += new System.EventHandler(this.mbtnDel_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mbtnCancel.Location = new System.Drawing.Point(25, 368);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 3;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.mbtnOK.Location = new System.Drawing.Point(25, 319);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 2;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnDown
            // 
            this.mbtnDown.Image = ((System.Drawing.Image)(resources.GetObject("mbtnDown.Image")));
            this.mbtnDown.Location = new System.Drawing.Point(39, 58);
            this.mbtnDown.Name = "mbtnDown";
            this.mbtnDown.Size = new System.Drawing.Size(45, 23);
            this.mbtnDown.TabIndex = 1;
            this.mbtnDown.UseVisualStyleBackColor = true;
            this.mbtnDown.Click += new System.EventHandler(this.mbtnDown_Click);
            // 
            // mbtnUp
            // 
            this.mbtnUp.Image = ((System.Drawing.Image)(resources.GetObject("mbtnUp.Image")));
            this.mbtnUp.Location = new System.Drawing.Point(39, 16);
            this.mbtnUp.Name = "mbtnUp";
            this.mbtnUp.Size = new System.Drawing.Size(45, 23);
            this.mbtnUp.TabIndex = 0;
            this.mbtnUp.UseVisualStyleBackColor = true;
            this.mbtnUp.Click += new System.EventHandler(this.mbtnUp_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mlistViewDatasets);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 406);
            this.panel3.TabIndex = 2;
            // 
            // mlistViewDatasets
            // 
            this.mlistViewDatasets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.mlistViewDatasets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlistViewDatasets.GridLines = true;
            this.mlistViewDatasets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.mlistViewDatasets.HideSelection = false;
            this.mlistViewDatasets.Location = new System.Drawing.Point(0, 0);
            this.mlistViewDatasets.MultiSelect = false;
            this.mlistViewDatasets.Name = "mlistViewDatasets";
            this.mlistViewDatasets.Size = new System.Drawing.Size(507, 406);
            this.mlistViewDatasets.TabIndex = 0;
            this.mlistViewDatasets.UseCompatibleStateImageBehavior = false;
            this.mlistViewDatasets.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Dataset Name";
            this.columnHeader1.Width = 200;
            // 
            // frmArrangeColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 461);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmArrangeColumns";
            this.Text = "Arrange Datasets";
            this.Load += new System.EventHandler(this.frmArrangeColumns_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private DAnTE.ExtraControls.NiceLine niceLine1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button mbtnDel;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnDown;
        private System.Windows.Forms.Button mbtnUp;
        private System.Windows.Forms.ListView mlistViewDatasets;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}