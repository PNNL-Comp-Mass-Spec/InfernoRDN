namespace DAnTE.Inferno
{
    partial class frmPlotDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlotDev));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mucPicVwrRPlot = new DAnTE.ExtraControls.ucPicViewer();
            this.mCtxtMnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuStretch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnCancel = new System.Windows.Forms.Button();
            this.mbtnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.mCtxtMnu.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mucPicVwrRPlot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 610);
            this.panel1.TabIndex = 0;
            // 
            // mucPicVwrRPlot
            // 
            this.mucPicVwrRPlot.BackColor = System.Drawing.Color.White;
            this.mucPicVwrRPlot.ContextMenuStrip = this.mCtxtMnu;
            this.mucPicVwrRPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mucPicVwrRPlot.Image = null;
            this.mucPicVwrRPlot.ImageSizeMode = DAnTE.ExtraControls.SizeMode.RatioStretch;
            this.mucPicVwrRPlot.Location = new System.Drawing.Point(0, 0);
            this.mucPicVwrRPlot.Name = "mucPicVwrRPlot";
            this.mucPicVwrRPlot.Size = new System.Drawing.Size(719, 610);
            this.mucPicVwrRPlot.TabIndex = 0;
            // 
            // mCtxtMnu
            // 
            this.mCtxtMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStretch,
            this.mnuScroll,
            this.mnuSave});
            this.mCtxtMnu.Name = "contextMenuStrip1";
            this.mCtxtMnu.Size = new System.Drawing.Size(156, 70);
            // 
            // mnuStretch
            // 
            this.mnuStretch.Name = "mnuStretch";
            this.mnuStretch.Size = new System.Drawing.Size(155, 22);
            this.mnuStretch.Text = "Stretch w/Ratio";
            this.mnuStretch.Click += new System.EventHandler(this.mnuStretch_Click);
            // 
            // mnuScroll
            // 
            this.mnuScroll.Name = "mnuScroll";
            this.mnuScroll.Size = new System.Drawing.Size(155, 22);
            this.mnuScroll.Text = "Scrollable";
            this.mnuScroll.Click += new System.EventHandler(this.mnuScroll_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(155, 22);
            this.mnuSave.Text = "Save As";
            this.mnuSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mbtnSave);
            this.panel2.Controls.Add(this.mbtnCancel);
            this.panel2.Controls.Add(this.mbtnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 610);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(719, 55);
            this.panel2.TabIndex = 1;
            // 
            // mbtnSave
            // 
            this.mbtnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mbtnSave.Location = new System.Drawing.Point(322, 16);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(75, 23);
            this.mbtnSave.TabIndex = 3;
            this.mbtnSave.Text = "Save";
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // mbtnCancel
            // 
            this.mbtnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mbtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mbtnCancel.Location = new System.Drawing.Point(420, 16);
            this.mbtnCancel.Name = "mbtnCancel";
            this.mbtnCancel.Size = new System.Drawing.Size(75, 23);
            this.mbtnCancel.TabIndex = 2;
            this.mbtnCancel.Text = "Cancel";
            this.mbtnCancel.UseVisualStyleBackColor = true;
            this.mbtnCancel.Click += new System.EventHandler(this.mbtnCancel_Click);
            // 
            // mbtnOK
            // 
            this.mbtnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mbtnOK.Location = new System.Drawing.Point(223, 16);
            this.mbtnOK.Name = "mbtnOK";
            this.mbtnOK.Size = new System.Drawing.Size(75, 23);
            this.mbtnOK.TabIndex = 0;
            this.mbtnOK.Text = "OK";
            this.mbtnOK.UseVisualStyleBackColor = true;
            this.mbtnOK.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // frmPlotDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mbtnCancel;
            this.ClientSize = new System.Drawing.Size(719, 665);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 320);
            this.Name = "frmPlotDev";
            this.Text = "Charts";
            this.panel1.ResumeLayout(false);
            this.mCtxtMnu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button mbtnOK;
        private System.Windows.Forms.Button mbtnCancel;
        private System.Windows.Forms.Button mbtnSave;
        private ExtraControls.ucPicViewer mucPicVwrRPlot;
        private System.Windows.Forms.ContextMenuStrip mCtxtMnu;
        private System.Windows.Forms.ToolStripMenuItem mnuStretch;
        private System.Windows.Forms.ToolStripMenuItem mnuScroll;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
    }
}