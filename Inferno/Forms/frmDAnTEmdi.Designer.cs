namespace DAnTE.Inferno
{
    partial class frmDAnTEmdi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDAnTEmdi));
			this.mmnuStrip = new System.Windows.Forms.MenuStrip();
			this.mnuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemClearTmp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemResource = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemUpgradeRPacks = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemStdPlots = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemggplots = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuItemBugs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mtoolStripMDI = new System.Windows.Forms.ToolStrip();
			this.mtoolBtnNew = new System.Windows.Forms.ToolStripButton();
			this.mtBtnExit = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mtoolBtnRlog = new System.Windows.Forms.ToolStripButton();
			this.mtoolBtnHelp = new System.Windows.Forms.ToolStripButton();
			this.mhelpProviderDAnTE = new System.Windows.Forms.HelpProvider();
			this.mmnuStrip.SuspendLayout();
			this.mtoolStripMDI.SuspendLayout();
			this.SuspendLayout();
			// 
			// mmnuStrip
			// 
			this.mmnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemFile,
            this.mnuItemTools,
            this.mnuWindow,
            this.helpToolStripMenuItem});
			this.mmnuStrip.Location = new System.Drawing.Point(0, 0);
			this.mmnuStrip.MdiWindowListItem = this.mnuWindow;
			this.mmnuStrip.Name = "mmnuStrip";
			this.mmnuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.mmnuStrip.Size = new System.Drawing.Size(920, 28);
			this.mmnuStrip.TabIndex = 0;
			this.mmnuStrip.Text = "menuStrip1";
			// 
			// mnuItemFile
			// 
			this.mnuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemNew,
            this.toolStripSeparator2,
            this.mnuItemExit});
			this.mnuItemFile.MergeIndex = 1;
			this.mnuItemFile.Name = "mnuItemFile";
			this.mnuItemFile.Size = new System.Drawing.Size(44, 24);
			this.mnuItemFile.Text = "&File";
			// 
			// mnuItemNew
			// 
			this.mnuItemNew.MergeIndex = 0;
			this.mnuItemNew.Name = "mnuItemNew";
			this.mnuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuItemNew.Size = new System.Drawing.Size(161, 24);
			this.mnuItemNew.Text = "&New";
			this.mnuItemNew.Click += new System.EventHandler(this.mnuItemNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.MergeIndex = 8;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
			// 
			// mnuItemExit
			// 
			this.mnuItemExit.MergeIndex = 9;
			this.mnuItemExit.Name = "mnuItemExit";
			this.mnuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.mnuItemExit.Size = new System.Drawing.Size(161, 24);
			this.mnuItemExit.Text = "E&xit";
			this.mnuItemExit.Click += new System.EventHandler(this.mnuItemExit_Click);
			// 
			// mnuItemTools
			// 
			this.mnuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemClearTmp,
            this.mnuItemResource,
            this.mnuItemUpgradeRPacks,
            this.mnuItemStdPlots,
            this.mnuItemggplots});
			this.mnuItemTools.MergeIndex = 5;
			this.mnuItemTools.Name = "mnuItemTools";
			this.mnuItemTools.Size = new System.Drawing.Size(57, 24);
			this.mnuItemTools.Text = "&Tools";		
			// 
			// mnuItemClearTmp
			// 
			this.mnuItemClearTmp.Name = "mnuItemClearTmp";
			this.mnuItemClearTmp.Size = new System.Drawing.Size(223, 24);
			this.mnuItemClearTmp.Text = "Clear Temporary Files";
			this.mnuItemClearTmp.Click += new System.EventHandler(this.mnuItemClearTmp_Click);
			// 
			// mnuItemResource
			// 
			this.mnuItemResource.Name = "mnuItemResource";
			this.mnuItemResource.Size = new System.Drawing.Size(223, 24);
			this.mnuItemResource.Text = "Re-source R Functions";
			this.mnuItemResource.Click += new System.EventHandler(this.mnuItemResource_Click);
			// 
			// mnuItemUpgradeRPacks
			// 
			this.mnuItemUpgradeRPacks.Name = "mnuItemUpgradeRPacks";
			this.mnuItemUpgradeRPacks.Size = new System.Drawing.Size(223, 24);
			this.mnuItemUpgradeRPacks.Text = "Upgrade R Packages";
			this.mnuItemUpgradeRPacks.Click += new System.EventHandler(this.mnuItemUpgradeRPacks_Click);
			// 
			// mnuItemStdPlots
			// 
			this.mnuItemStdPlots.Name = "mnuItemStdPlots";
			this.mnuItemStdPlots.Size = new System.Drawing.Size(223, 24);
			this.mnuItemStdPlots.Text = "Use Standard R Plots";
			this.mnuItemStdPlots.Click += new System.EventHandler(this.mnuItemStdPlots_Click);
			// 
			// mnuItemggplots
			// 
			this.mnuItemggplots.Enabled = false;
			this.mnuItemggplots.Name = "mnuItemggplots";
			this.mnuItemggplots.Size = new System.Drawing.Size(223, 24);
			this.mnuItemggplots.Text = "Use \'ggplot2\' R Plots";
			this.mnuItemggplots.Click += new System.EventHandler(this.mnuItemggplots_Click);
			// 
			// mnuWindow
			// 
			this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.arToolStripMenuItem});
			this.mnuWindow.MergeIndex = 6;
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Size = new System.Drawing.Size(76, 24);
			this.mnuWindow.Text = "&Window";
			// 
			// cascadeToolStripMenuItem
			// 
			this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
			this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
			this.cascadeToolStripMenuItem.Tag = "Cascade";
			this.cascadeToolStripMenuItem.Text = "&Cascade";
			this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.mnuWindowItem_Click);
			// 
			// tileHorizontalToolStripMenuItem
			// 
			this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
			this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
			this.tileHorizontalToolStripMenuItem.Tag = "TileHorizontal";
			this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
			this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.mnuWindowItem_Click);
			// 
			// tileVerticalToolStripMenuItem
			// 
			this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
			this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
			this.tileVerticalToolStripMenuItem.Tag = "TileVertical";
			this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
			this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.mnuWindowItem_Click);
			// 
			// arToolStripMenuItem
			// 
			this.arToolStripMenuItem.Name = "arToolStripMenuItem";
			this.arToolStripMenuItem.Size = new System.Drawing.Size(176, 24);
			this.arToolStripMenuItem.Tag = "ArrangeIcons";
			this.arToolStripMenuItem.Text = "&Arrange Icons";
			this.arToolStripMenuItem.Click += new System.EventHandler(this.mnuWindowItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemHelp,
            this.mnuItemBugs,
            this.toolStripSeparator1,
            this.mnuItemAbout});
			this.helpToolStripMenuItem.MergeIndex = 7;
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// mnuItemHelp
			// 
			this.mnuItemHelp.Name = "mnuItemHelp";
			this.mnuItemHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.mnuItemHelp.Size = new System.Drawing.Size(252, 24);
			this.mnuItemHelp.Text = "&Help";
			this.mnuItemHelp.Click += new System.EventHandler(this.mnuItemHelp_Click);
			// 
			// mnuItemBugs
			// 
			this.mnuItemBugs.Name = "mnuItemBugs";
			this.mnuItemBugs.Size = new System.Drawing.Size(252, 24);
			this.mnuItemBugs.Text = "Bugs and Feature &Tracking";
			this.mnuItemBugs.Click += new System.EventHandler(this.mnuItemBugs_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
			// 
			// mnuItemAbout
			// 
			this.mnuItemAbout.Name = "mnuItemAbout";
			this.mnuItemAbout.Size = new System.Drawing.Size(252, 24);
			this.mnuItemAbout.Text = "&About";
			this.mnuItemAbout.Click += new System.EventHandler(this.mnuItemAbout_Click);
			// 
			// mtoolStripMDI
			// 
			this.mtoolStripMDI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtoolBtnNew,
            this.mtBtnExit,
            this.toolStripSeparator3,
            this.mtoolBtnRlog,
            this.mtoolBtnHelp});
			this.mtoolStripMDI.Location = new System.Drawing.Point(0, 28);
			this.mtoolStripMDI.Name = "mtoolStripMDI";
			this.mtoolStripMDI.Size = new System.Drawing.Size(920, 25);
			this.mtoolStripMDI.TabIndex = 1;
			this.mtoolStripMDI.Text = "toolStrip1";
			// 
			// mtoolBtnNew
			// 
			this.mtoolBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mtoolBtnNew.Image = ((System.Drawing.Image)(resources.GetObject("mtoolBtnNew.Image")));
			this.mtoolBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mtoolBtnNew.MergeIndex = 0;
			this.mtoolBtnNew.Name = "mtoolBtnNew";
			this.mtoolBtnNew.Size = new System.Drawing.Size(23, 22);
			this.mtoolBtnNew.Text = "&New";
			this.mtoolBtnNew.ToolTipText = "New";
			this.mtoolBtnNew.Click += new System.EventHandler(this.mnuItemNew_Click);
			// 
			// mtBtnExit
			// 
			this.mtBtnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mtBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnExit.Image")));
			this.mtBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mtBtnExit.MergeIndex = 1;
			this.mtBtnExit.Name = "mtBtnExit";
			this.mtBtnExit.Size = new System.Drawing.Size(23, 22);
			this.mtBtnExit.Text = "Exit";
			this.mtBtnExit.Click += new System.EventHandler(this.mnuItemExit_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.MergeIndex = 2;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);			
			// 
			// mtoolBtnHelp
			// 
			this.mtoolBtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.mtoolBtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("mtoolBtnHelp.Image")));
			this.mtoolBtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.mtoolBtnHelp.MergeIndex = 51;
			this.mtoolBtnHelp.Name = "mtoolBtnHelp";
			this.mtoolBtnHelp.Size = new System.Drawing.Size(23, 22);
			this.mtoolBtnHelp.Text = "Help";
			this.mtoolBtnHelp.Click += new System.EventHandler(this.mnuItemHelp_Click);
			// 
			// mhelpProviderDAnTE
			// 
			this.mhelpProviderDAnTE.HelpNamespace = "";
			// 
			// frmDAnTEmdi
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 773);
			this.Controls.Add(this.mtoolStripMDI);
			this.Controls.Add(this.mmnuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.mmnuStrip;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "frmDAnTEmdi";
			this.Text = "Inferno";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.frmDAnTEmdi_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
			this.mmnuStrip.ResumeLayout(false);
			this.mmnuStrip.PerformLayout();
			this.mtoolStripMDI.ResumeLayout(false);
			this.mtoolStripMDI.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mmnuStrip;
        public System.Windows.Forms.ToolStrip mtoolStripMDI;
        private System.Windows.Forms.ToolStripMenuItem mnuItemFile;
        private System.Windows.Forms.ToolStripMenuItem mnuItemNew;
        private System.Windows.Forms.ToolStripMenuItem mnuItemExit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton mtoolBtnNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuItemBugs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuItemTools;
        private System.Windows.Forms.ToolStripMenuItem mnuItemResource;
        private System.Windows.Forms.ToolStripMenuItem mnuItemClearTmp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton mtoolBtnRlog;
        private System.Windows.Forms.ToolStripButton mtoolBtnHelp;
        private System.Windows.Forms.HelpProvider mhelpProviderDAnTE;
        private System.Windows.Forms.ToolStripButton mtBtnExit;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItemUpgradeRPacks;
        private System.Windows.Forms.ToolStripMenuItem mnuItemStdPlots;
        private System.Windows.Forms.ToolStripMenuItem mnuItemggplots;
    }
}