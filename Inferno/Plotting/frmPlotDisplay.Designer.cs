namespace DAnTE.Inferno
{
    partial class frmPlotDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlotDisplay));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mpictureBoxEx = new DAnTE.ExtraControls.PictureBoxEx();
            this.mctxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPrintPrvw = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.plotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPara = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemOrgSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemFitScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrDante = new System.Windows.Forms.Timer(this.components);
            this.mtoolTipPlot = new System.Windows.Forms.ToolTip(this.components);
            this.mToolStripPlot = new System.Windows.Forms.ToolStrip();
            this.mtBtnSavePlot = new System.Windows.Forms.ToolStripButton();
            this.mtBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.mtBtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnParam = new System.Windows.Forms.ToolStripButton();
            this.mtBtnOrgSize = new System.Windows.Forms.ToolStripButton();
            this.mtBtnFitScreen = new System.Windows.Forms.ToolStripButton();
            this.mtBtnTimeStamp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1.SuspendLayout();
            this.mctxtMenu.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.mToolStripPlot.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mpictureBoxEx);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 722);
            this.panel1.TabIndex = 0;
            // 
            // mpictureBoxEx
            // 
            this.mpictureBoxEx.AutoScroll = true;
            this.mpictureBoxEx.BackColor = System.Drawing.Color.White;
            this.mpictureBoxEx.ContextMenuStrip = this.mctxtMenu;
            this.mpictureBoxEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mpictureBoxEx.Location = new System.Drawing.Point(0, 0);
            this.mpictureBoxEx.Name = "mpictureBoxEx";
            this.mpictureBoxEx.Size = new System.Drawing.Size(1016, 722);
            this.mpictureBoxEx.TabIndex = 0;
            this.mpictureBoxEx.Text = "pictureBoxEx1";
            this.mtoolTipPlot.SetToolTip(this.mpictureBoxEx, "Double click on plot to resize to the actual size.");
            // 
            // mctxtMenu
            // 
            this.mctxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stretchToolStripMenuItem,
            this.restoreSizeToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.StampToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.printToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.mctxtMenu.Name = "mctxtMenu";
            this.mctxtMenu.Size = new System.Drawing.Size(185, 158);
            // 
            // stretchToolStripMenuItem
            // 
            this.stretchToolStripMenuItem.Image = global::DAnTE.Properties.Resources.AlignToGridHS;
            this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
            this.stretchToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.stretchToolStripMenuItem.Text = "Fit Screen";
            this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
            // 
            // restoreSizeToolStripMenuItem
            // 
            this.restoreSizeToolStripMenuItem.Name = "restoreSizeToolStripMenuItem";
            this.restoreSizeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.restoreSizeToolStripMenuItem.Text = "Show in Original Size";
            this.restoreSizeToolStripMenuItem.Click += new System.EventHandler(this.RestoreSize_event);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Image = global::DAnTE.Properties.Resources.EditInformationHS;
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.parametersToolStripMenuItem.Text = "Parameters";
            // 
            // StampToolStripMenuItem
            // 
            this.StampToolStripMenuItem.Image = global::DAnTE.Properties.Resources.ExpirationHS;
            this.StampToolStripMenuItem.Name = "StampToolStripMenuItem";
            this.StampToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.StampToolStripMenuItem.Text = "Date/Name Stamp";
            this.StampToolStripMenuItem.Click += new System.EventHandler(this.addStampToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::DAnTE.Properties.Resources.PrintHS;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::DAnTE.Properties.Resources.DeleteHS;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.plotToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemSave,
            this.mnuItemPrintPrvw,
            this.mnuItemPrint,
            this.mnuItemClose});
            this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.fileToolStripMenuItem.MergeIndex = 0;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuItemSave
            // 
            this.mnuItemSave.Image = global::DAnTE.Properties.Resources.saveHS;
            this.mnuItemSave.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.mnuItemSave.MergeIndex = 0;
            this.mnuItemSave.Name = "mnuItemSave";
            this.mnuItemSave.Size = new System.Drawing.Size(172, 22);
            this.mnuItemSave.Text = "Save";
            this.mnuItemSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuItemPrintPrvw
            // 
            this.mnuItemPrintPrvw.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemPrintPrvw.MergeIndex = 1;
            this.mnuItemPrintPrvw.Name = "mnuItemPrintPrvw";
            this.mnuItemPrintPrvw.Size = new System.Drawing.Size(172, 22);
            this.mnuItemPrintPrvw.Text = "Print Preview";
            this.mnuItemPrintPrvw.Click += new System.EventHandler(this.mnuPrintPrvw_Click);
            // 
            // mnuItemPrint
            // 
            this.mnuItemPrint.Image = global::DAnTE.Properties.Resources.PrintHS;
            this.mnuItemPrint.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemPrint.MergeIndex = 2;
            this.mnuItemPrint.Name = "mnuItemPrint";
            this.mnuItemPrint.Size = new System.Drawing.Size(172, 22);
            this.mnuItemPrint.Text = "Print";
            this.mnuItemPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuItemClose
            // 
            this.mnuItemClose.Image = global::DAnTE.Properties.Resources.DeleteHS;
            this.mnuItemClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemClose.MergeIndex = 3;
            this.mnuItemClose.Name = "mnuItemClose";
            this.mnuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mnuItemClose.Size = new System.Drawing.Size(172, 22);
            this.mnuItemClose.Text = "&Close Plot";
            this.mnuItemClose.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // plotToolStripMenuItem
            // 
            this.plotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemPara,
            this.mnuItemOrgSize,
            this.mnuItemFitScreen,
            this.mnuItemStamp});
            this.plotToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.plotToolStripMenuItem.MergeIndex = 1;
            this.plotToolStripMenuItem.Name = "plotToolStripMenuItem";
            this.plotToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.plotToolStripMenuItem.Text = "&Plot";
            // 
            // mnuItemPara
            // 
            this.mnuItemPara.Image = global::DAnTE.Properties.Resources.EditInformationHS;
            this.mnuItemPara.Name = "mnuItemPara";
            this.mnuItemPara.Size = new System.Drawing.Size(184, 22);
            this.mnuItemPara.Text = "Parameters";
            // 
            // mnuItemOrgSize
            // 
            this.mnuItemOrgSize.Image = global::DAnTE.Properties.Resources.ResizeHS;
            this.mnuItemOrgSize.Name = "mnuItemOrgSize";
            this.mnuItemOrgSize.Size = new System.Drawing.Size(184, 22);
            this.mnuItemOrgSize.Text = "Show in Original Size";
            this.mnuItemOrgSize.Click += new System.EventHandler(this.RestoreSize_event);
            // 
            // mnuItemFitScreen
            // 
            this.mnuItemFitScreen.Image = global::DAnTE.Properties.Resources.AlignToGridHS;
            this.mnuItemFitScreen.Name = "mnuItemFitScreen";
            this.mnuItemFitScreen.Size = new System.Drawing.Size(184, 22);
            this.mnuItemFitScreen.Text = "Fit Screen";
            this.mnuItemFitScreen.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
            // 
            // mnuItemStamp
            // 
            this.mnuItemStamp.Image = global::DAnTE.Properties.Resources.ExpirationHS;
            this.mnuItemStamp.Name = "mnuItemStamp";
            this.mnuItemStamp.Size = new System.Drawing.Size(184, 22);
            this.mnuItemStamp.Text = "Add Stamp";
            this.mnuItemStamp.Click += new System.EventHandler(this.addStampToolStripMenuItem_Click);
            // 
            // tmrDante
            // 
            this.tmrDante.Tick += new System.EventHandler(this.tmrDante_Tick);
            // 
            // mToolStripPlot
            // 
            this.mToolStripPlot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtBtnSavePlot,
            this.mtBtnPrint,
            this.mtBtnClose,
            this.toolStripSeparator1,
            this.mtBtnParam,
            this.mtBtnOrgSize,
            this.mtBtnFitScreen,
            this.mtBtnTimeStamp,
            this.toolStripSeparator2});
            this.mToolStripPlot.Location = new System.Drawing.Point(0, 24);
            this.mToolStripPlot.Name = "mToolStripPlot";
            this.mToolStripPlot.Size = new System.Drawing.Size(1016, 25);
            this.mToolStripPlot.TabIndex = 1;
            this.mToolStripPlot.Text = "toolStrip1";
            // 
            // mtBtnSavePlot
            // 
            this.mtBtnSavePlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnSavePlot.Image = global::DAnTE.Properties.Resources.saveHS;
            this.mtBtnSavePlot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnSavePlot.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnSavePlot.MergeIndex = 3;
            this.mtBtnSavePlot.Name = "mtBtnSavePlot";
            this.mtBtnSavePlot.Size = new System.Drawing.Size(23, 22);
            this.mtBtnSavePlot.Text = "toolStripButton1";
            this.mtBtnSavePlot.ToolTipText = "Save Plot";
            this.mtBtnSavePlot.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mtBtnPrint
            // 
            this.mtBtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnPrint.Image = global::DAnTE.Properties.Resources.PrintHS;
            this.mtBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnPrint.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnPrint.MergeIndex = 4;
            this.mtBtnPrint.Name = "mtBtnPrint";
            this.mtBtnPrint.Size = new System.Drawing.Size(23, 22);
            this.mtBtnPrint.Text = "toolStripButton1";
            this.mtBtnPrint.ToolTipText = "Print Plot";
            this.mtBtnPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mtBtnClose
            // 
            this.mtBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnClose.Image = global::DAnTE.Properties.Resources.DeleteHS;
            this.mtBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnClose.MergeIndex = 5;
            this.mtBtnClose.Name = "mtBtnClose";
            this.mtBtnClose.Size = new System.Drawing.Size(23, 22);
            this.mtBtnClose.Text = "toolStripButton1";
            this.mtBtnClose.ToolTipText = "Close Plot";
            this.mtBtnClose.Click += new System.EventHandler(this.mbtnOK_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 6;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mtBtnParam
            // 
            this.mtBtnParam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnParam.Image = global::DAnTE.Properties.Resources.EditInformationHS;
            this.mtBtnParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnParam.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnParam.MergeIndex = 7;
            this.mtBtnParam.Name = "mtBtnParam";
            this.mtBtnParam.Size = new System.Drawing.Size(23, 22);
            this.mtBtnParam.Text = "toolStripButton1";
            this.mtBtnParam.ToolTipText = "Plot Parameters";
            // 
            // mtBtnOrgSize
            // 
            this.mtBtnOrgSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnOrgSize.Image = global::DAnTE.Properties.Resources.ResizeHS;
            this.mtBtnOrgSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnOrgSize.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnOrgSize.MergeIndex = 8;
            this.mtBtnOrgSize.Name = "mtBtnOrgSize";
            this.mtBtnOrgSize.Size = new System.Drawing.Size(23, 22);
            this.mtBtnOrgSize.Text = "toolStripButton1";
            this.mtBtnOrgSize.ToolTipText = "Show in Original Size";
            this.mtBtnOrgSize.Click += new System.EventHandler(this.RestoreSize_event);
            // 
            // mtBtnFitScreen
            // 
            this.mtBtnFitScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnFitScreen.Image = global::DAnTE.Properties.Resources.AlignToGridHS;
            this.mtBtnFitScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnFitScreen.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnFitScreen.MergeIndex = 9;
            this.mtBtnFitScreen.Name = "mtBtnFitScreen";
            this.mtBtnFitScreen.Size = new System.Drawing.Size(23, 22);
            this.mtBtnFitScreen.Text = "toolStripButton1";
            this.mtBtnFitScreen.ToolTipText = "Fit Plot to Window";
            this.mtBtnFitScreen.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
            // 
            // mtBtnTimeStamp
            // 
            this.mtBtnTimeStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnTimeStamp.Image = global::DAnTE.Properties.Resources.ExpirationHS;
            this.mtBtnTimeStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnTimeStamp.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnTimeStamp.MergeIndex = 10;
            this.mtBtnTimeStamp.Name = "mtBtnTimeStamp";
            this.mtBtnTimeStamp.Size = new System.Drawing.Size(23, 22);
            this.mtBtnTimeStamp.Text = "toolStripButton1";
            this.mtBtnTimeStamp.ToolTipText = "Add Time Stamp";
            this.mtBtnTimeStamp.Click += new System.EventHandler(this.addStampToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator2.MergeIndex = 11;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // frmPlotDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 722);
            this.Controls.Add(this.mToolStripPlot);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 320);
            this.Name = "frmPlotDisplay";
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.frmPlotDisplay_Load);
            this.Activated += new System.EventHandler(this.frmPlotDisplay_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed_event);
            this.Resize += new System.EventHandler(this.frmPlotDisplay_Resize);
            this.panel1.ResumeLayout(false);
            this.mctxtMenu.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.mToolStripPlot.ResumeLayout(false);
            this.mToolStripPlot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ExtraControls.PictureBoxEx mpictureBoxEx;
        private System.Windows.Forms.ContextMenuStrip mctxtMenu;
        private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreSizeToolStripMenuItem;
        private System.Windows.Forms.Timer tmrDante;
        protected System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemSave;
        private System.Windows.Forms.ToolStripMenuItem mnuItemPrintPrvw;
        private System.Windows.Forms.ToolStripMenuItem mnuItemPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuItemClose;
        private System.Windows.Forms.ToolStripMenuItem plotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemStamp;
        protected System.Windows.Forms.ToolStripMenuItem mnuItemPara;
        protected System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemOrgSize;
        private System.Windows.Forms.ToolStripMenuItem mnuItemFitScreen;
        private System.Windows.Forms.ToolTip mtoolTipPlot;
        private System.Windows.Forms.ToolStrip mToolStripPlot;
        private System.Windows.Forms.ToolStripButton mtBtnSavePlot;
        private System.Windows.Forms.ToolStripButton mtBtnPrint;
        private System.Windows.Forms.ToolStripButton mtBtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton mtBtnParam;
        private System.Windows.Forms.ToolStripButton mtBtnFitScreen;
        private System.Windows.Forms.ToolStripButton mtBtnTimeStamp;
        private System.Windows.Forms.ToolStripButton mtBtnOrgSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}