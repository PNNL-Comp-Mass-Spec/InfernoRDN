using System.Windows.Forms;
namespace DAnTE.Inferno
{
    partial class frmFactorInformation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFactorInformation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mlblSubTitle = new System.Windows.Forms.Label();
            this.mlblTitle = new System.Windows.Forms.Label();
            this.mbtnDefFac = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbBox = new System.Windows.Forms.ComboBox();
            this.mlstViewDataSets = new System.Windows.Forms.ListView();
            this.mColHeadDS = new System.Windows.Forms.ColumnHeader();
            this.cntxtMnuFactors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fillRowsBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillnBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillnBlocksCyclicallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillRandomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnDel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.mbtnDown = new System.Windows.Forms.Button();
            this.mbtnUp = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.cntxtMnuFactors.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.mlblSubTitle);
            this.panel1.Controls.Add(this.mlblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(909, 66);
            this.panel1.TabIndex = 0;
            // 
            // mlblSubTitle
            // 
            this.mlblSubTitle.AutoSize = true;
            this.mlblSubTitle.Location = new System.Drawing.Point(26, 37);
            this.mlblSubTitle.Name = "mlblSubTitle";
            this.mlblSubTitle.Size = new System.Drawing.Size(359, 13);
            this.mlblSubTitle.TabIndex = 4;
            this.mlblSubTitle.Text = "Add/Remove Factors, Set Factor Assignments, and Change Dataset Order";
            // 
            // mlblTitle
            // 
            this.mlblTitle.AutoSize = true;
            this.mlblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblTitle.Location = new System.Drawing.Point(12, 9);
            this.mlblTitle.Name = "mlblTitle";
            this.mlblTitle.Size = new System.Drawing.Size(195, 16);
            this.mlblTitle.TabIndex = 3;
            this.mlblTitle.Text = "Factors and Dataset Order:";
            // 
            // mbtnDefFac
            // 
            this.mbtnDefFac.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnDefFac.Location = new System.Drawing.Point(17, 328);
            this.mbtnDefFac.Name = "mbtnDefFac";
            this.mbtnDefFac.Size = new System.Drawing.Size(90, 40);
            this.mbtnDefFac.TabIndex = 2;
            this.mbtnDefFac.Text = "Add/Remove Factors";
            this.mbtnDefFac.UseVisualStyleBackColor = true;
            this.mbtnDefFac.Click += new System.EventHandler(this.mbtnDefFac_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(909, 458);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbBox);
            this.panel3.Controls.Add(this.mlstViewDataSets);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(790, 458);
            this.panel3.TabIndex = 3;
            // 
            // cmbBox
            // 
            this.cmbBox.FormattingEnabled = true;
            this.cmbBox.Location = new System.Drawing.Point(128, 165);
            this.cmbBox.Name = "cmbBox";
            this.cmbBox.Size = new System.Drawing.Size(121, 21);
            this.cmbBox.TabIndex = 1;
            this.cmbBox.Text = "FactorCmbBx";
            this.cmbBox.Visible = false;
            this.cmbBox.LostFocus += new System.EventHandler(this.cmbBoxFocusOver);
            this.cmbBox.SelectedIndexChanged += new System.EventHandler(this.cmbBoxSelectedIndexChanged);
            this.cmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbBoxKeyPress);
            // 
            // mlstViewDataSets
            // 
            this.mlstViewDataSets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mColHeadDS});
            this.mlstViewDataSets.ContextMenuStrip = this.cntxtMnuFactors;
            this.mlstViewDataSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlstViewDataSets.FullRowSelect = true;
            this.mlstViewDataSets.GridLines = true;
            this.mlstViewDataSets.HideSelection = false;
            this.mlstViewDataSets.Location = new System.Drawing.Point(0, 0);
            this.mlstViewDataSets.MultiSelect = false;
            this.mlstViewDataSets.Name = "mlstViewDataSets";
            this.mlstViewDataSets.Size = new System.Drawing.Size(790, 458);
            this.mlstViewDataSets.TabIndex = 0;
            this.mlstViewDataSets.UseCompatibleStateImageBehavior = false;
            this.mlstViewDataSets.View = System.Windows.Forms.View.Details;
            this.mlstViewDataSets.DoubleClick += new System.EventHandler(this.doubleClick_event);
            this.mlstViewDataSets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown_event);
            // 
            // mColHeadDS
            // 
            this.mColHeadDS.Text = "Dataset Name";
            this.mColHeadDS.Width = 150;
            // 
            // cntxtMnuFactors
            // 
            this.cntxtMnuFactors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillRowsBelowToolStripMenuItem,
            this.fillnBlocksToolStripMenuItem,
            this.fillnBlocksCyclicallyToolStripMenuItem,
            this.fillRandomToolStripMenuItem});
            this.cntxtMnuFactors.Name = "contextMenuStrip1";
            this.cntxtMnuFactors.Size = new System.Drawing.Size(204, 92);
            // 
            // fillRowsBelowToolStripMenuItem
            // 
            this.fillRowsBelowToolStripMenuItem.Name = "fillRowsBelowToolStripMenuItem";
            this.fillRowsBelowToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fillRowsBelowToolStripMenuItem.Text = "Fill rows below";
            this.fillRowsBelowToolStripMenuItem.Click += new System.EventHandler(this.menuItemFillBelow_Click);
            // 
            // fillnBlocksToolStripMenuItem
            // 
            this.fillnBlocksToolStripMenuItem.Name = "fillnBlocksToolStripMenuItem";
            this.fillnBlocksToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fillnBlocksToolStripMenuItem.Text = "Fill <n> blocks";
            this.fillnBlocksToolStripMenuItem.Click += new System.EventHandler(this.menuItemFillNBelow_Click);
            // 
            // fillnBlocksCyclicallyToolStripMenuItem
            // 
            this.fillnBlocksCyclicallyToolStripMenuItem.Name = "fillnBlocksCyclicallyToolStripMenuItem";
            this.fillnBlocksCyclicallyToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fillnBlocksCyclicallyToolStripMenuItem.Text = "Fill <n> blocks cyclically";
            this.fillnBlocksCyclicallyToolStripMenuItem.Click += new System.EventHandler(this.menuItemFillNCycl_Click);
            // 
            // fillRandomToolStripMenuItem
            // 
            this.fillRandomToolStripMenuItem.Name = "fillRandomToolStripMenuItem";
            this.fillRandomToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.fillRandomToolStripMenuItem.Text = "Fill random";
            this.fillRandomToolStripMenuItem.Click += new System.EventHandler(this.menuItemFillRand_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mbtnCancel);
            this.panel4.Controls.Add(this.mbtnDel);
            this.panel4.Controls.Add(this.mbtnOK);
            this.panel4.Controls.Add(this.mbtnDefFac);
            this.panel4.Controls.Add(this.mbtnDown);
            this.panel4.Controls.Add(this.mbtnUp);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(790, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(119, 458);
            this.panel4.TabIndex = 2;
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(17, 406);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(90, 26);
            this.mbtnCancel.TabIndex = 1;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnDel
            // 
            this.mbtnDel.Image = global::DAnTE.Properties.Resources.DeleteHS;
            this.mbtnDel.Location = new System.Drawing.Point(36, 218);
            this.mbtnDel.Name = "mbtnDel";
            this.mbtnDel.Size = new System.Drawing.Size(45, 24);
            this.mbtnDel.TabIndex = 7;
            this.toolTip1.SetToolTip(this.mbtnDel, "Delete Dataset");
            this.mbtnDel.UseVisualStyleBackColor = true;
            this.mbtnDel.Click += new System.EventHandler(this.mbtnDel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.mbtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mbtnOK.Location = new System.Drawing.Point(17, 374);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(90, 26);
            this.mbtnOK.TabIndex = 0;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // mbtnDown
            // 
            this.mbtnDown.Image = global::DAnTE.Properties.Resources.downArrow2;
            this.mbtnDown.Location = new System.Drawing.Point(36, 174);
            this.mbtnDown.Name = "mbtnDown";
            this.mbtnDown.Size = new System.Drawing.Size(45, 26);
            this.mbtnDown.TabIndex = 6;
            this.toolTip1.SetToolTip(this.mbtnDown, "Move Dataset Down");
            this.mbtnDown.UseVisualStyleBackColor = true;
            this.mbtnDown.Click += new System.EventHandler(this.mbtnDown_Click);
            // 
            // mbtnUp
            // 
            this.mbtnUp.Image = global::DAnTE.Properties.Resources.upArrow2;
            this.mbtnUp.Location = new System.Drawing.Point(36, 134);
            this.mbtnUp.Name = "mbtnUp";
            this.mbtnUp.Size = new System.Drawing.Size(45, 25);
            this.mbtnUp.TabIndex = 5;
            this.toolTip1.SetToolTip(this.mbtnUp, "Move Dataset Up");
            this.mbtnUp.UseVisualStyleBackColor = true;
            this.mbtnUp.Click += new System.EventHandler(this.mbtnUp_Click);
            // 
            // frmFactorInformation
            // 
            this.AcceptButton = this.mbtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(909, 524);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFactorInformation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factors / Dataset Order";
            this.Load += new System.EventHandler(this.frmFactorInformation_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.cntxtMnuFactors.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mbtnDefFac;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView mlstViewDataSets;
        private System.Windows.Forms.ColumnHeader mColHeadDS;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Label mlblTitle;

        #region Variables for listview editing related -----------
        private ListViewItem li = null;
        private int X = 0;
        private int Y = 0;
        private string subItemText;
        //private System.Windows.Forms.ComboBox cmbBox;
        private int subItemSelected = 0;
        #endregion
        private ContextMenuStrip cntxtMnuFactors;
        private ToolStripMenuItem fillRowsBelowToolStripMenuItem;
        private ToolStripMenuItem fillnBlocksToolStripMenuItem;
        private ToolStripMenuItem fillnBlocksCyclicallyToolStripMenuItem;
        private ToolStripMenuItem fillRandomToolStripMenuItem;
        private ComboBox cmbBox;
        private Panel panel4;
        private Button mbtnDel;
        private Button mbtnDown;
        private Button mbtnUp;
        private Panel panel3;
        private ToolTip toolTip1;
        private Label mlblSubTitle;
    }
}