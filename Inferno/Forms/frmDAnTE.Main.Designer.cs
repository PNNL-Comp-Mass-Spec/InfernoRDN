using System.Windows.Forms;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Designer Variables
        private System.Windows.Forms.MenuStrip mnuStripDAnTE;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemEset;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemAnalysis;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView ctlTreeView;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitterVert;
        private System.Windows.Forms.Splitter splitterBottom;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.StatusBarPanel statusBarPanelMsg;
        private System.Windows.Forms.ToolStripMenuItem menuItemLog;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenFactors;
        private System.Windows.Forms.ToolStripMenuItem menuItemAcrossD;
        private System.Windows.Forms.ToolStripMenuItem menuItemPlot;
        private System.Windows.Forms.ToolStripMenuItem menuItemCorr;
        private System.Windows.Forms.ToolStripMenuItem menuItemMAPlot;
        private System.Windows.Forms.ToolStripMenuItem menuItemBoxPlot;
        private ToolStripMenuItem mnuOpenSession;
        private ToolStripMenuItem mnuSaveSession;
        private ImageList mImListTreeView;
        private ToolStripContainer toolStripContainer1;
        private StatusBarPanel statusBarPanelRowNum;
        private ToolStripMenuItem menuItemStat;
        private ToolStripMenuItem menuItemANOVA;
        private ToolStripMenuItem menuItemNormRepl;
        private ToolStripMenuItem menuItemMTIPI;

        private ToolStripMenuItem mnuProteinRollupPlot;
        private ToolStripMenuItem menuItemMeanC;

        private ContextMenuStrip mContextMenuTreeV;
        private ToolStripMenuItem mCtxtMenuSave;
        private ToolStripMenuItem menuItemDeleteColumns;
        private ToolStripMenuItem menuItemRollUp;
        private ToolStripMenuItem menuItemRollupRef;
        private ToolStripMenuItem menuItemRollupMedStDev;
        private ToolStripMenuItem menuItemQRup;
        private ToolStripMenuItem mnuHistograms;
        private ToolStripMenuItem menuItemLinReg;
        private HelpProvider mhelpProviderDAnTE;
        private ToolStripMenuItem menuItemImputeMissing;

        private ToolStripMenuItem mnuItemQnorm;

        private ToolStrip mtoolStripDAnTE;
        private ToolStripButton mtBtnOpenS;
        private ToolStripDropDownButton mtBtnOpen;
        private ToolStripMenuItem mtBtnMnuExpr;
        private ToolStripMenuItem mtBtnMnuMTprot;
        private ToolStripMenuItem mtBtnMnuFactors;
        private ToolStripButton mtBtnSaveS;
        private ToolStripButton mtBtnSave;
        private ToolStripButton mtBtnClose;
        private ToolStripButton mtBtnLog;
        private ToolStripDropDownButton mtBtnNorm;
        private ToolStripMenuItem mtBtnMnuLOWESS;
        private ToolStripMenuItem mtBtnMnuLinReg;
        private ToolStripMenuItem mtBtnMnuMAD;
        private ToolStripMenuItem mtBtnMnuMeanC;
        private ToolStripButton mtBtnRefRollup;
        private ToolStripButton mtBtnMedStdRup;
        private ToolStripButton mtBtnQrollUp;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton mtBtnHist;
        private ToolStripButton mtBtnCorr;
        private ToolStripButton mtBtnBoxPlots;
        private ToolStripButton mtBtnPCAPLS;
        private ToolStripButton mtBtnRollupPlot;
        private ToolStripMenuItem mtBtnMnuQuan;
        private ToolStripSeparator toolStripSeparator2;

        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem mnuItemFilterpq;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripButton mtBtnHeatmap;
        private ToolStripMenuItem mnuItemSaveSessionAs;
        private ContextMenuStrip mCntxtMnuGrid;
        private ToolStripMenuItem contextMenuItemPlotRows;
        private ToolStripMenuItem mnuItemQQplot;
        private ToolStripButton toolStripButton1;
        private ToolStripMenuItem mnuItemNormal;
        private ToolStripMenuItem menuItemMSMS;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem spectralCountDataToolStripMenuItem;
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDAnTE));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Inferno");
            this.mnuStripDAnTE = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEset = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMTIPI = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenFactors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemMSMS = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenSession = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveSession = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemSaveSessionAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLinReg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNormRepl = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemQnorm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAcrossD = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMeanC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemImputeMissing = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHistograms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemQQplot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCorr = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBoxPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMAPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProteinRollupPlot = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemVenn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPlotSelectedRows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRollupRef = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRollupMedStDev = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemQRup = new System.Windows.Forms.ToolStripMenuItem();
            this.groupingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemDefFactors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemMergeCols = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuArrangeColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemANOVA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemOneSTtest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemWilcox = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemKruskal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemFC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPCAPLS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemHeatmap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemPatterns = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemFilterpq = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemMissFilt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowRCommandLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAnalysisSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuItemFilterUsingSelectedRowIDs = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mDataTab = new System.Windows.Forms.TabControl();
            this.splitterVert = new System.Windows.Forms.Splitter();
            this.ctlTreeView = new System.Windows.Forms.TreeView();
            this.mImListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.mContextMenuTreeV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mCtxtMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitterBottom = new System.Windows.Forms.Splitter();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanelMsg = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelRowNum = new System.Windows.Forms.StatusBarPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.mtoolStripDAnTE = new System.Windows.Forms.ToolStrip();
            this.mtBtnOpenS = new System.Windows.Forms.ToolStripButton();
            this.mtBtnOpen = new System.Windows.Forms.ToolStripDropDownButton();
            this.mtBtnMnuExpr = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuMTprot = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuFactors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.spectralCountDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnSaveS = new System.Windows.Forms.ToolStripButton();
            this.mtBtnSave = new System.Windows.Forms.ToolStripButton();
            this.mtBtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnLog = new System.Windows.Forms.ToolStripButton();
            this.mtBtnNorm = new System.Windows.Forms.ToolStripDropDownButton();
            this.mtBtnMnuLOWESS = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuLinReg = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuMAD = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuMeanC = new System.Windows.Forms.ToolStripMenuItem();
            this.mtBtnMnuQuan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnHist = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.mtBtnCorr = new System.Windows.Forms.ToolStripButton();
            this.mtBtnBoxPlots = new System.Windows.Forms.ToolStripButton();
            this.mtBtnMA = new System.Windows.Forms.ToolStripButton();
            this.mtBtnRollupPlot = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnPCAPLS = new System.Windows.Forms.ToolStripButton();
            this.mtBtnHeatmap = new System.Windows.Forms.ToolStripButton();
            this.mtBtnPatterns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnRefRollup = new System.Windows.Forms.ToolStripButton();
            this.mtBtnMedStdRup = new System.Windows.Forms.ToolStripButton();
            this.mtBtnQrollUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mtBtnDefFac = new System.Windows.Forms.ToolStripButton();
            this.mtBtnANOVA = new System.Windows.Forms.ToolStripButton();
            this.mtBtnpqFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.mhelpProviderDAnTE = new System.Windows.Forms.HelpProvider();
            this.mCntxtMnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuItemPlotRows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuItemFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuItemCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuItemCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStripDAnTE.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mContextMenuTreeV.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelRowNum)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.mtoolStripDAnTE.SuspendLayout();
            this.mCntxtMnuGrid.SuspendLayout();
            this.SuspendLayout();
            //
            // mnuStripDAnTE
            //
            this.mnuStripDAnTE.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuStripDAnTE.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuStripDAnTE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemAnalysis,
            this.menuItemPlot,
            this.menuItemRollUp,
            this.groupingToolStripMenuItem,
            this.menuItemStat,
            this.mnuItemExplore,
            this.toolsToolStripMenuItem});
            this.mnuStripDAnTE.Location = new System.Drawing.Point(0, 0);
            this.mnuStripDAnTE.Name = "mnuStripDAnTE";
            this.mnuStripDAnTE.Size = new System.Drawing.Size(762, 28);
            this.mnuStripDAnTE.TabIndex = 2;
            //
            // menuItemFile
            //
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemClose,
            this.menuItemSave,
            this.toolStripSeparator9,
            this.mnuOpenSession,
            this.mnuSaveSession,
            this.mnuItemSaveSessionAs,
            this.menuItemExit});
            this.menuItemFile.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuItemFile.MergeIndex = 0;
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(44, 24);
            this.menuItemFile.Text = "&File";
            //
            // menuItemOpen
            //
            this.menuItemOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEset,
            this.menuItemMTIPI,
            this.menuItemOpenFactors,
            this.toolStripSeparator11,
            this.menuItemMSMS});
            this.menuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpen.Image")));
            this.menuItemOpen.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.menuItemOpen.MergeIndex = 0;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(244, 26);
            this.menuItemOpen.Text = "&Open";
            //
            // menuItemEset
            //
            this.menuItemEset.MergeIndex = 0;
            this.menuItemEset.Name = "menuItemEset";
            this.menuItemEset.Size = new System.Drawing.Size(222, 24);
            this.menuItemEset.Text = "Expression File";
            this.menuItemEset.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // menuItemMTIPI
            //
            this.menuItemMTIPI.Name = "menuItemMTIPI";
            this.menuItemMTIPI.Size = new System.Drawing.Size(222, 24);
            this.menuItemMTIPI.Text = "Peptide - Protein File";
            this.menuItemMTIPI.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // menuItemOpenFactors
            //
            this.menuItemOpenFactors.MergeIndex = 2;
            this.menuItemOpenFactors.Name = "menuItemOpenFactors";
            this.menuItemOpenFactors.Size = new System.Drawing.Size(222, 24);
            this.menuItemOpenFactors.Text = "Factor Definitions File";
            this.menuItemOpenFactors.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // toolStripSeparator11
            //
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(219, 6);
            //
            // menuItemMSMS
            //
            this.menuItemMSMS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuItemMSMS.Name = "menuItemMSMS";
            this.menuItemMSMS.Size = new System.Drawing.Size(222, 24);
            this.menuItemMSMS.Text = "Spectral Count Data";
            this.menuItemMSMS.Click += new System.EventHandler(this.menuItemMSMS_Click);
            //
            // menuItemClose
            //
            this.menuItemClose.Image = ((System.Drawing.Image)(resources.GetObject("menuItemClose.Image")));
            this.menuItemClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemClose.MergeIndex = 1;
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuItemClose.Size = new System.Drawing.Size(244, 26);
            this.menuItemClose.Text = "&Close Table";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            //
            // menuItemSave
            //
            this.menuItemSave.Image = ((System.Drawing.Image)(resources.GetObject("menuItemSave.Image")));
            this.menuItemSave.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemSave.MergeIndex = 2;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.S)));
            this.menuItemSave.Size = new System.Drawing.Size(244, 26);
            this.menuItemSave.Text = "Save &Table";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            //
            // toolStripSeparator9
            //
            this.toolStripSeparator9.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator9.MergeIndex = 3;
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(241, 6);
            //
            // mnuOpenSession
            //
            this.mnuOpenSession.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenSession.Image")));
            this.mnuOpenSession.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuOpenSession.MergeIndex = 4;
            this.mnuOpenSession.Name = "mnuOpenSession";
            this.mnuOpenSession.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenSession.Size = new System.Drawing.Size(244, 26);
            this.mnuOpenSession.Text = "Open &Session";
            this.mnuOpenSession.Click += new System.EventHandler(this.mnuOpenSession_Click);
            //
            // mnuSaveSession
            //
            this.mnuSaveSession.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveSession.Image")));
            this.mnuSaveSession.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuSaveSession.MergeIndex = 5;
            this.mnuSaveSession.Name = "mnuSaveSession";
            this.mnuSaveSession.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSaveSession.Size = new System.Drawing.Size(244, 26);
            this.mnuSaveSession.Text = "&Save Session";
            this.mnuSaveSession.Click += new System.EventHandler(this.mnuSaveSession_Click);
            //
            // mnuItemSaveSessionAs
            //
            this.mnuItemSaveSessionAs.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemSaveSessionAs.Image")));
            this.mnuItemSaveSessionAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemSaveSessionAs.MergeIndex = 6;
            this.mnuItemSaveSessionAs.Name = "mnuItemSaveSessionAs";
            this.mnuItemSaveSessionAs.Size = new System.Drawing.Size(244, 26);
            this.mnuItemSaveSessionAs.Text = "Save Session As";
            this.mnuItemSaveSessionAs.Click += new System.EventHandler(this.mnuSaveSessionAs_Click);
            //
            // menuItemExit
            //
            this.menuItemExit.Image = ((System.Drawing.Image)(resources.GetObject("menuItemExit.Image")));
            this.menuItemExit.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemExit.MergeIndex = 7;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(244, 26);
            this.menuItemExit.Text = "&Close Session";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            //
            // menuItemAnalysis
            //
            this.menuItemAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLog,
            this.toolStripSeparator3,
            this.menuItemLinReg,
            this.menuItemNormRepl,
            this.mnuItemQnorm,
            this.toolStripSeparator4,
            this.menuItemAcrossD,
            this.menuItemMeanC,
            this.toolStripSeparator7,
            this.menuItemImputeMissing});
            this.menuItemAnalysis.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemAnalysis.MergeIndex = 1;
            this.menuItemAnalysis.Name = "menuItemAnalysis";
            this.menuItemAnalysis.Size = new System.Drawing.Size(53, 24);
            this.menuItemAnalysis.Text = "&Data";
            //
            // menuItemLog
            //
            this.menuItemLog.Image = ((System.Drawing.Image)(resources.GetObject("menuItemLog.Image")));
            this.menuItemLog.MergeIndex = 1;
            this.menuItemLog.Name = "menuItemLog";
            this.menuItemLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuItemLog.Size = new System.Drawing.Size(297, 26);
            this.menuItemLog.Text = "&Log Transform";
            this.menuItemLog.Click += new System.EventHandler(this.menuItemLogEset_Click);
            //
            // toolStripSeparator3
            //
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(294, 6);
            //
            // menuItemLinReg
            //
            this.menuItemLinReg.Name = "menuItemLinReg";
            this.menuItemLinReg.Size = new System.Drawing.Size(297, 26);
            this.menuItemLinReg.Text = "Li&near Regression Normalization";
            this.menuItemLinReg.Click += new System.EventHandler(this.menuItemLinReg_Click);
            //
            // menuItemNormRepl
            //
            this.menuItemNormRepl.Image = ((System.Drawing.Image)(resources.GetObject("menuItemNormRepl.Image")));
            this.menuItemNormRepl.MergeIndex = 2;
            this.menuItemNormRepl.Name = "menuItemNormRepl";
            this.menuItemNormRepl.Size = new System.Drawing.Size(297, 26);
            this.menuItemNormRepl.Text = "L&oess Normalization";
            this.menuItemNormRepl.Click += new System.EventHandler(this.menuItemLOESSNorm_Click);
            //
            // mnuItemQnorm
            //
            this.mnuItemQnorm.Name = "mnuItemQnorm";
            this.mnuItemQnorm.Size = new System.Drawing.Size(297, 26);
            this.mnuItemQnorm.Text = "&Quantile Normalization";
            this.mnuItemQnorm.Click += new System.EventHandler(this.mnuItemQnorm_Click);
            //
            // toolStripSeparator4
            //
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(294, 6);
            //
            // menuItemAcrossD
            //
            this.menuItemAcrossD.MergeIndex = 4;
            this.menuItemAcrossD.Name = "menuItemAcrossD";
            this.menuItemAcrossD.Size = new System.Drawing.Size(297, 26);
            this.menuItemAcrossD.Text = "&Median Absolute Deviation";
            this.menuItemAcrossD.Click += new System.EventHandler(this.menuItemMAD_Click);
            //
            // menuItemMeanC
            //
            this.menuItemMeanC.Name = "menuItemMeanC";
            this.menuItemMeanC.Size = new System.Drawing.Size(297, 26);
            this.menuItemMeanC.Text = "&Central Tendancy";
            this.menuItemMeanC.Click += new System.EventHandler(this.menuItemMeanC_Click);
            //
            // toolStripSeparator7
            //
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(294, 6);
            //
            // menuItemImputeMissing
            //
            this.menuItemImputeMissing.Name = "menuItemImputeMissing";
            this.menuItemImputeMissing.Size = new System.Drawing.Size(297, 26);
            this.menuItemImputeMissing.Text = "&Impute Missing Values";
            this.menuItemImputeMissing.Click += new System.EventHandler(this.menuItemImpute_Click);
            //
            // menuItemPlot
            //
            this.menuItemPlot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHistograms,
            this.mnuItemQQplot,
            this.menuItemCorr,
            this.menuItemBoxPlot,
            this.menuItemMAPlot,
            this.mnuProteinRollupPlot,
            this.mnuItemVenn,
            this.mnuItemPlotSelectedRows});
            this.menuItemPlot.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemPlot.MergeIndex = 2;
            this.menuItemPlot.Name = "menuItemPlot";
            this.menuItemPlot.Size = new System.Drawing.Size(47, 24);
            this.menuItemPlot.Text = "P&lot";
            //
            // mnuHistograms
            //
            this.mnuHistograms.Image = ((System.Drawing.Image)(resources.GetObject("mnuHistograms.Image")));
            this.mnuHistograms.MergeIndex = 0;
            this.mnuHistograms.Name = "mnuHistograms";
            this.mnuHistograms.Size = new System.Drawing.Size(202, 26);
            this.mnuHistograms.Text = "&Histograms";
            this.mnuHistograms.Click += new System.EventHandler(this.mnuHistograms_Click);
            //
            // mnuItemQQplot
            //
            this.mnuItemQQplot.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemQQplot.Image")));
            this.mnuItemQQplot.MergeIndex = 1;
            this.mnuItemQQplot.Name = "mnuItemQQplot";
            this.mnuItemQQplot.Size = new System.Drawing.Size(202, 26);
            this.mnuItemQQplot.Text = "&Q-Q Plot";
            this.mnuItemQQplot.Click += new System.EventHandler(this.mnuItemQQplot_Click);
            //
            // menuItemCorr
            //
            this.menuItemCorr.Image = ((System.Drawing.Image)(resources.GetObject("menuItemCorr.Image")));
            this.menuItemCorr.MergeIndex = 2;
            this.menuItemCorr.Name = "menuItemCorr";
            this.menuItemCorr.Size = new System.Drawing.Size(202, 26);
            this.menuItemCorr.Text = "&Correlation";
            this.menuItemCorr.Click += new System.EventHandler(this.menuItemCorr_Click);
            //
            // menuItemBoxPlot
            //
            this.menuItemBoxPlot.Image = ((System.Drawing.Image)(resources.GetObject("menuItemBoxPlot.Image")));
            this.menuItemBoxPlot.MergeIndex = 3;
            this.menuItemBoxPlot.Name = "menuItemBoxPlot";
            this.menuItemBoxPlot.Size = new System.Drawing.Size(202, 26);
            this.menuItemBoxPlot.Text = "&Boxplots";
            this.menuItemBoxPlot.Click += new System.EventHandler(this.menuItemBoxPlot_Click);
            //
            // menuItemMAPlot
            //
            this.menuItemMAPlot.Image = ((System.Drawing.Image)(resources.GetObject("menuItemMAPlot.Image")));
            this.menuItemMAPlot.MergeIndex = 4;
            this.menuItemMAPlot.Name = "menuItemMAPlot";
            this.menuItemMAPlot.Size = new System.Drawing.Size(202, 26);
            this.menuItemMAPlot.Text = "&M-A plot";
            this.menuItemMAPlot.Click += new System.EventHandler(this.menuItemMAPlot_Click);
            //
            // mnuProteinRollupPlot
            //
            this.mnuProteinRollupPlot.Image = ((System.Drawing.Image)(resources.GetObject("mnuProteinRollupPlot.Image")));
            this.mnuProteinRollupPlot.MergeIndex = 5;
            this.mnuProteinRollupPlot.Name = "mnuProteinRollupPlot";
            this.mnuProteinRollupPlot.Size = new System.Drawing.Size(202, 26);
            this.mnuProteinRollupPlot.Text = "&Protein Rollup";
            this.mnuProteinRollupPlot.Click += new System.EventHandler(this.mnuProteinRollupPlot_Click);
            //
            // mnuItemVenn
            //
            this.mnuItemVenn.Image = global::DAnTE.Properties.Resources.venn2;
            this.mnuItemVenn.Name = "mnuItemVenn";
            this.mnuItemVenn.Size = new System.Drawing.Size(202, 26);
            this.mnuItemVenn.Text = "&Venn Diagrams";
            this.mnuItemVenn.Click += new System.EventHandler(this.mnuItemVenn_Click);
            //
            // mnuItemPlotSelectedRows
            //
            this.mnuItemPlotSelectedRows.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemPlotSelectedRows.Image")));
            this.mnuItemPlotSelectedRows.Name = "mnuItemPlotSelectedRows";
            this.mnuItemPlotSelectedRows.Size = new System.Drawing.Size(202, 26);
            this.mnuItemPlotSelectedRows.Text = "Plot &selected rows";
            this.mnuItemPlotSelectedRows.Click += new System.EventHandler(this.contextMenuItemPlotRows_Click);
            //
            // menuItemRollUp
            //
            this.menuItemRollUp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRollupRef,
            this.menuItemRollupMedStDev,
            this.menuItemQRup});
            this.menuItemRollUp.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemRollUp.MergeIndex = 3;
            this.menuItemRollUp.Name = "menuItemRollUp";
            this.menuItemRollUp.Size = new System.Drawing.Size(74, 24);
            this.menuItemRollUp.Text = "&Proteins";
            //
            // menuItemRollupRef
            //
            this.menuItemRollupRef.Image = ((System.Drawing.Image)(resources.GetObject("menuItemRollupRef.Image")));
            this.menuItemRollupRef.Name = "menuItemRollupRef";
            this.menuItemRollupRef.Size = new System.Drawing.Size(380, 26);
            this.menuItemRollupRef.Text = "1. &RRollup: Normalize to a Reference Peptide";
            this.menuItemRollupRef.Click += new System.EventHandler(this.menuItemRRollup_Click);
            //
            // menuItemRollupMedStDev
            //
            this.menuItemRollupMedStDev.Image = ((System.Drawing.Image)(resources.GetObject("menuItemRollupMedStDev.Image")));
            this.menuItemRollupMedStDev.Name = "menuItemRollupMedStDev";
            this.menuItemRollupMedStDev.Size = new System.Drawing.Size(380, 26);
            this.menuItemRollupMedStDev.Text = "2. &ZRollup: Median/StDev Scaling";
            this.menuItemRollupMedStDev.Click += new System.EventHandler(this.menuItemZRollup_Click);
            //
            // menuItemQRup
            //
            this.menuItemQRup.Image = ((System.Drawing.Image)(resources.GetObject("menuItemQRup.Image")));
            this.menuItemQRup.Name = "menuItemQRup";
            this.menuItemQRup.Size = new System.Drawing.Size(380, 26);
            this.menuItemQRup.Text = "3. &QRollup: Use Most Intense Peptides";
            this.menuItemQRup.Click += new System.EventHandler(this.menuItemQRup_Click);
            //
            // groupingToolStripMenuItem
            //
            this.groupingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemDefFactors,
            this.mnuItemMergeCols,
            this.mnuArrangeColumns});
            this.groupingToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.groupingToolStripMenuItem.MergeIndex = 4;
            this.groupingToolStripMenuItem.Name = "groupingToolStripMenuItem";
            this.groupingToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.groupingToolStripMenuItem.Text = "&Grouping";
            //
            // mnuItemDefFactors
            //
            this.mnuItemDefFactors.Image = global::DAnTE.Properties.Resources.OrgChartHS;
            this.mnuItemDefFactors.Name = "mnuItemDefFactors";
            this.mnuItemDefFactors.Size = new System.Drawing.Size(196, 26);
            this.mnuItemDefFactors.Text = "Define &Factors";
            this.mnuItemDefFactors.Click += new System.EventHandler(this.mnuItemDefFactors_Click);
            //
            // mnuItemMergeCols
            //
            this.mnuItemMergeCols.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemMergeCols.Image")));
            this.mnuItemMergeCols.Name = "mnuItemMergeCols";
            this.mnuItemMergeCols.Size = new System.Drawing.Size(196, 26);
            this.mnuItemMergeCols.Text = "&Merge Columns";
            this.mnuItemMergeCols.Click += new System.EventHandler(this.mnuItemMergeCols_Click);
            //
            // mnuArrangeColumns
            //
            this.mnuArrangeColumns.Name = "mnuArrangeColumns";
            this.mnuArrangeColumns.Size = new System.Drawing.Size(196, 26);
            this.mnuArrangeColumns.Text = "&Arrange Columns";
            this.mnuArrangeColumns.Click += new System.EventHandler(this.mnuItemDefFactors_Click);
            //
            // menuItemStat
            //
            this.menuItemStat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemNormal,
            this.toolStripSeparator15,
            this.menuItemANOVA,
            this.mnuItemOneSTtest,
            this.toolStripSeparator16,
            this.mnuItemWilcox,
            this.mnuItemKruskal,
            this.toolStripSeparator17,
            this.mnuItemFC});
            this.menuItemStat.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuItemStat.MergeIndex = 5;
            this.menuItemStat.Name = "menuItemStat";
            this.menuItemStat.Size = new System.Drawing.Size(82, 24);
            this.menuItemStat.Text = "&Compare";
            //
            // mnuItemNormal
            //
            this.mnuItemNormal.Name = "mnuItemNormal";
            this.mnuItemNormal.Size = new System.Drawing.Size(292, 26);
            this.mnuItemNormal.Text = "&Shapiro-Wilk Test for Normality";
            this.mnuItemNormal.Click += new System.EventHandler(this.menuItemShapiroWilks_Click);
            //
            // toolStripSeparator15
            //
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(289, 6);
            //
            // menuItemANOVA
            //
            this.menuItemANOVA.Image = ((System.Drawing.Image)(resources.GetObject("menuItemANOVA.Image")));
            this.menuItemANOVA.Name = "menuItemANOVA";
            this.menuItemANOVA.Size = new System.Drawing.Size(292, 26);
            this.menuItemANOVA.Text = "&ANOVA";
            this.menuItemANOVA.Click += new System.EventHandler(this.menuItemANOVA_Click);
            //
            // mnuItemOneSTtest
            //
            this.mnuItemOneSTtest.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemOneSTtest.Image")));
            this.mnuItemOneSTtest.Name = "mnuItemOneSTtest";
            this.mnuItemOneSTtest.Size = new System.Drawing.Size(292, 26);
            this.mnuItemOneSTtest.Text = "One Sample &T-test";
            this.mnuItemOneSTtest.Click += new System.EventHandler(this.menuItemOneSampleTtest_Click);
            //
            // toolStripSeparator16
            //
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(289, 6);
            //
            // mnuItemWilcox
            //
            this.mnuItemWilcox.Name = "mnuItemWilcox";
            this.mnuItemWilcox.Size = new System.Drawing.Size(292, 26);
            this.mnuItemWilcox.Text = "&Wilcoxon (Mann-Whitney) Test";
            this.mnuItemWilcox.Click += new System.EventHandler(this.menuItemWilcox_Click);
            //
            // mnuItemKruskal
            //
            this.mnuItemKruskal.Name = "mnuItemKruskal";
            this.mnuItemKruskal.Size = new System.Drawing.Size(292, 26);
            this.mnuItemKruskal.Text = "&Kruskal-Walis Test";
            this.mnuItemKruskal.Click += new System.EventHandler(this.menuItemKW_Click);
            //
            // toolStripSeparator17
            //
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(289, 6);
            //
            // mnuItemFC
            //
            this.mnuItemFC.Name = "mnuItemFC";
            this.mnuItemFC.Size = new System.Drawing.Size(292, 26);
            this.mnuItemFC.Text = "Calculate &Fold Changes";
            this.mnuItemFC.Click += new System.EventHandler(this.mnuItemFC_Click);
            //
            // mnuItemExplore
            //
            this.mnuItemExplore.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemPCAPLS,
            this.mnuItemHeatmap,
            this.mnuItemPatterns});
            this.mnuItemExplore.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemExplore.MergeIndex = 6;
            this.mnuItemExplore.Name = "mnuItemExplore";
            this.mnuItemExplore.Size = new System.Drawing.Size(71, 24);
            this.mnuItemExplore.Text = "&Explore";
            //
            // mnuItemPCAPLS
            //
            this.mnuItemPCAPLS.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemPCAPLS.Image")));
            this.mnuItemPCAPLS.MergeIndex = 0;
            this.mnuItemPCAPLS.Name = "mnuItemPCAPLS";
            this.mnuItemPCAPLS.Size = new System.Drawing.Size(193, 26);
            this.mnuItemPCAPLS.Text = "&PCA/PLS";
            this.mnuItemPCAPLS.Click += new System.EventHandler(this.menuItemPCAPlot_Click);
            //
            // mnuItemHeatmap
            //
            this.mnuItemHeatmap.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemHeatmap.Image")));
            this.mnuItemHeatmap.MergeIndex = 1;
            this.mnuItemHeatmap.Name = "mnuItemHeatmap";
            this.mnuItemHeatmap.Size = new System.Drawing.Size(193, 26);
            this.mnuItemHeatmap.Text = "Cluster &Heatmap";
            this.mnuItemHeatmap.Click += new System.EventHandler(this.mnuItemHeatmap_Click);
            //
            // mnuItemPatterns
            //
            this.mnuItemPatterns.Image = ((System.Drawing.Image)(resources.GetObject("mnuItemPatterns.Image")));
            this.mnuItemPatterns.MergeIndex = 2;
            this.mnuItemPatterns.Name = "mnuItemPatterns";
            this.mnuItemPatterns.Size = new System.Drawing.Size(193, 26);
            this.mnuItemPatterns.Text = "Pattern &Search";
            this.mnuItemPatterns.Click += new System.EventHandler(this.mnuItemPatterns_Click);
            //
            // toolsToolStripMenuItem
            //
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemFilterpq,
            this.mnuItemMissFilt,
            this.mnuShowRCommandLog,
            this.mnuAnalysisSummary,
            this.toolStripSeparator8,
            this.toolStripSeparator18,
            this.mnuItemFilterUsingSelectedRowIDs});
            this.toolsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.toolsToolStripMenuItem.MergeIndex = 7;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "&Tools";
            //
            // mnuItemFilterpq
            //
            this.mnuItemFilterpq.Image = global::DAnTE.Properties.Resources.Filter2HS;
            this.mnuItemFilterpq.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemFilterpq.MergeIndex = 1;
            this.mnuItemFilterpq.Name = "mnuItemFilterpq";
            this.mnuItemFilterpq.Size = new System.Drawing.Size(267, 26);
            this.mnuItemFilterpq.Text = "&p/q-value Filter";
            this.mnuItemFilterpq.Click += new System.EventHandler(this.mnuItemFilterpq_Click);
            //
            // mnuItemMissFilt
            //
            this.mnuItemMissFilt.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuItemMissFilt.MergeIndex = 2;
            this.mnuItemMissFilt.Name = "mnuItemMissFilt";
            this.mnuItemMissFilt.Size = new System.Drawing.Size(267, 26);
            this.mnuItemMissFilt.Text = "&Missing Value Filter";
            this.mnuItemMissFilt.Click += new System.EventHandler(this.mnuItemMissFilt_Click);
            //
            // mnuShowRCommandLog
            //
            this.mnuShowRCommandLog.MergeIndex = 4;
            this.mnuShowRCommandLog.Name = "mnuShowRCommandLog";
            this.mnuShowRCommandLog.Size = new System.Drawing.Size(267, 26);
            this.mnuShowRCommandLog.Text = "View &R Command Log";
            this.mnuShowRCommandLog.Click += new System.EventHandler(this.mnuShowRCommandLog_Click);
            //
            // mnuAnalysisSummary
            //
            this.mnuAnalysisSummary.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuAnalysisSummary.MergeIndex = 5;
            this.mnuAnalysisSummary.Name = "mnuAnalysisSummary";
            this.mnuAnalysisSummary.Size = new System.Drawing.Size(267, 26);
            this.mnuAnalysisSummary.Text = "&Analysis Summary";
            this.mnuAnalysisSummary.Click += new System.EventHandler(this.mnuAnalysisSummary_Click);
            //
            // toolStripSeparator8
            //
            this.toolStripSeparator8.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator8.MergeIndex = 6;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(264, 6);
            //
            // toolStripSeparator18
            //
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(264, 6);
            //
            // mnuItemFilterUsingSelectedRowIDs
            //
            this.mnuItemFilterUsingSelectedRowIDs.Name = "mnuItemFilterUsingSelectedRowIDs";
            this.mnuItemFilterUsingSelectedRowIDs.Size = new System.Drawing.Size(267, 26);
            this.mnuItemFilterUsingSelectedRowIDs.Text = "&Filter using selected RowIDs";
            this.mnuItemFilterUsingSelectedRowIDs.Click += new System.EventHandler(this.contextMenuItemFilter_Click);
            //
            // panel1
            //
            this.panel1.Controls.Add(this.mDataTab);
            this.panel1.Controls.Add(this.splitterVert);
            this.panel1.Controls.Add(this.ctlTreeView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 410);
            this.panel1.TabIndex = 0;
            //
            // mDataTab
            //
            this.mDataTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mDataTab.Location = new System.Drawing.Point(314, 0);
            this.mDataTab.Name = "mDataTab";
            this.mDataTab.SelectedIndex = 0;
            this.mDataTab.Size = new System.Drawing.Size(448, 410);
            this.mDataTab.TabIndex = 2;
            //
            // splitterVert
            //
            this.splitterVert.Location = new System.Drawing.Point(312, 0);
            this.splitterVert.Name = "splitterVert";
            this.splitterVert.Size = new System.Drawing.Size(2, 410);
            this.splitterVert.TabIndex = 1;
            this.splitterVert.TabStop = false;
            //
            // ctlTreeView
            //
            this.ctlTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlTreeView.ImageIndex = 0;
            this.ctlTreeView.ImageList = this.mImListTreeView;
            this.ctlTreeView.Location = new System.Drawing.Point(0, 0);
            this.ctlTreeView.Name = "ctlTreeView";
            treeNode1.Name = "";
            treeNode1.Text = "Inferno";
            this.ctlTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.ctlTreeView.SelectedImageIndex = 0;
            this.ctlTreeView.Size = new System.Drawing.Size(312, 410);
            this.ctlTreeView.TabIndex = 0;
            this.ctlTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctlTreeView_AfterSelect);
            this.ctlTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ctlTreeView_NodeMouseClick);
            //
            // mImListTreeView
            //
            this.mImListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mImListTreeView.ImageStream")));
            this.mImListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.mImListTreeView.Images.SetKeyName(0, "inferno_small.ico");
            this.mImListTreeView.Images.SetKeyName(1, "Tunselect.png");
            this.mImListTreeView.Images.SetKeyName(2, "Tselect.png");
            this.mImListTreeView.Images.SetKeyName(3, "NOTE11.ICO");
            this.mImListTreeView.Images.SetKeyName(4, "NOTE12.ICO");
            //
            // mContextMenuTreeV
            //
            this.mContextMenuTreeV.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mContextMenuTreeV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mCtxtMenuSave,
            this.menuItemDeleteColumns});
            this.mContextMenuTreeV.Name = "mContextMenuTreeV";
            this.mContextMenuTreeV.Size = new System.Drawing.Size(223, 52);
            //
            // mCtxtMenuSave
            //
            this.mCtxtMenuSave.Name = "mCtxtMenuSave";
            this.mCtxtMenuSave.Size = new System.Drawing.Size(222, 24);
            this.mCtxtMenuSave.Text = "Save with Protein Info";
            this.mCtxtMenuSave.Click += new System.EventHandler(this.mCtxtMenuSave_Click);
            //
            // menuItemDeleteColumns
            //
            this.menuItemDeleteColumns.Enabled = false;
            this.menuItemDeleteColumns.Name = "menuItemDeleteColumns";
            this.menuItemDeleteColumns.Size = new System.Drawing.Size(222, 24);
            this.menuItemDeleteColumns.Text = "Delete Columns";
            this.menuItemDeleteColumns.Click += new System.EventHandler(this.menuItemDeleteColumns_Click);
            //
            // panel2
            //
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.splitterBottom);
            this.panel2.Controls.Add(this.statusBar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(762, 439);
            this.panel2.TabIndex = 1;
            //
            // splitterBottom
            //
            this.splitterBottom.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterBottom.Location = new System.Drawing.Point(0, 410);
            this.splitterBottom.Name = "splitterBottom";
            this.splitterBottom.Size = new System.Drawing.Size(762, 3);
            this.splitterBottom.TabIndex = 2;
            this.splitterBottom.TabStop = false;
            //
            // statusBar
            //
            this.statusBar.Location = new System.Drawing.Point(0, 413);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanelMsg,
            this.statusBarPanelRowNum});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(762, 26);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "Ready";
            //
            // statusBarPanelMsg
            //
            this.statusBarPanelMsg.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanelMsg.Name = "statusBarPanelMsg";
            this.statusBarPanelMsg.Text = "Ready";
            this.statusBarPanelMsg.Width = 591;
            //
            // statusBarPanelRowNum
            //
            this.statusBarPanelRowNum.Name = "statusBarPanelRowNum";
            this.statusBarPanelRowNum.Width = 150;
            //
            // toolStripContainer1
            //
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            //
            // toolStripContainer1.ContentPanel
            //
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(762, 439);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(762, 494);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            //
            // toolStripContainer1.TopToolStripPanel
            //
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mnuStripDAnTE);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mtoolStripDAnTE);
            //
            // mtoolStripDAnTE
            //
            this.mtoolStripDAnTE.Dock = System.Windows.Forms.DockStyle.None;
            this.mtoolStripDAnTE.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mtoolStripDAnTE.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtBtnOpenS,
            this.mtBtnOpen,
            this.mtBtnSaveS,
            this.mtBtnSave,
            this.mtBtnClose,
            this.toolStripSeparator1,
            this.mtBtnLog,
            this.mtBtnNorm,
            this.toolStripSeparator5,
            this.mtBtnHist,
            this.toolStripButton1,
            this.mtBtnCorr,
            this.mtBtnBoxPlots,
            this.mtBtnMA,
            this.mtBtnRollupPlot,
            this.toolStripSeparator10,
            this.mtBtnPCAPLS,
            this.mtBtnHeatmap,
            this.mtBtnPatterns,
            this.toolStripSeparator2,
            this.mtBtnRefRollup,
            this.mtBtnMedStdRup,
            this.mtBtnQrollUp,
            this.toolStripSeparator6,
            this.mtBtnDefFac,
            this.mtBtnANOVA,
            this.mtBtnpqFilter,
            this.toolStripSeparator14});
            this.mtoolStripDAnTE.Location = new System.Drawing.Point(3, 28);
            this.mtoolStripDAnTE.Name = "mtoolStripDAnTE";
            this.mtoolStripDAnTE.Size = new System.Drawing.Size(596, 27);
            this.mtoolStripDAnTE.TabIndex = 4;
            //
            // mtBtnOpenS
            //
            this.mtBtnOpenS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnOpenS.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnOpenS.Image")));
            this.mtBtnOpenS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnOpenS.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnOpenS.MergeIndex = 3;
            this.mtBtnOpenS.Name = "mtBtnOpenS";
            this.mtBtnOpenS.Size = new System.Drawing.Size(24, 24);
            this.mtBtnOpenS.Text = "&Open";
            this.mtBtnOpenS.ToolTipText = "Open Session";
            this.mtBtnOpenS.Click += new System.EventHandler(this.mnuOpenSession_Click);
            //
            // mtBtnOpen
            //
            this.mtBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtBtnMnuExpr,
            this.mtBtnMnuMTprot,
            this.mtBtnMnuFactors,
            this.toolStripSeparator12,
            this.spectralCountDataToolStripMenuItem});
            this.mtBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnOpen.Image")));
            this.mtBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnOpen.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnOpen.MergeIndex = 4;
            this.mtBtnOpen.Name = "mtBtnOpen";
            this.mtBtnOpen.Size = new System.Drawing.Size(34, 24);
            this.mtBtnOpen.Text = "Load Data";
            //
            // mtBtnMnuExpr
            //
            this.mtBtnMnuExpr.Name = "mtBtnMnuExpr";
            this.mtBtnMnuExpr.Size = new System.Drawing.Size(224, 24);
            this.mtBtnMnuExpr.Text = "Expression File";
            this.mtBtnMnuExpr.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // mtBtnMnuMTprot
            //
            this.mtBtnMnuMTprot.Name = "mtBtnMnuMTprot";
            this.mtBtnMnuMTprot.Size = new System.Drawing.Size(224, 24);
            this.mtBtnMnuMTprot.Text = "MassTag - Protein File";
            this.mtBtnMnuMTprot.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // mtBtnMnuFactors
            //
            this.mtBtnMnuFactors.Name = "mtBtnMnuFactors";
            this.mtBtnMnuFactors.Size = new System.Drawing.Size(224, 24);
            this.mtBtnMnuFactors.Text = "Factor Definitions File";
            this.mtBtnMnuFactors.Click += new System.EventHandler(this.menuItemLoad_Click);
            //
            // toolStripSeparator12
            //
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(221, 6);
            //
            // spectralCountDataToolStripMenuItem
            //
            this.spectralCountDataToolStripMenuItem.Name = "spectralCountDataToolStripMenuItem";
            this.spectralCountDataToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.spectralCountDataToolStripMenuItem.Text = "Spectral Count Data";
            this.spectralCountDataToolStripMenuItem.Click += new System.EventHandler(this.menuItemMSMS_Click);
            //
            // mtBtnSaveS
            //
            this.mtBtnSaveS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnSaveS.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnSaveS.Image")));
            this.mtBtnSaveS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnSaveS.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnSaveS.MergeIndex = 5;
            this.mtBtnSaveS.Name = "mtBtnSaveS";
            this.mtBtnSaveS.Size = new System.Drawing.Size(24, 24);
            this.mtBtnSaveS.Text = "&Save";
            this.mtBtnSaveS.ToolTipText = "Save Session";
            this.mtBtnSaveS.Click += new System.EventHandler(this.mnuSaveSession_Click);
            //
            // mtBtnSave
            //
            this.mtBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnSave.Image")));
            this.mtBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnSave.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnSave.MergeIndex = 6;
            this.mtBtnSave.Name = "mtBtnSave";
            this.mtBtnSave.Size = new System.Drawing.Size(24, 24);
            this.mtBtnSave.Text = "Save Current Grid";
            this.mtBtnSave.Click += new System.EventHandler(this.menuItemSave_Click);
            //
            // mtBtnClose
            //
            this.mtBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnClose.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnClose.Image")));
            this.mtBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnClose.MergeIndex = 7;
            this.mtBtnClose.Name = "mtBtnClose";
            this.mtBtnClose.Size = new System.Drawing.Size(24, 24);
            this.mtBtnClose.Text = "Close Table";
            this.mtBtnClose.ToolTipText = "Close Selected Table";
            this.mtBtnClose.Click += new System.EventHandler(this.menuItemClose_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator1.MergeIndex = 8;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            //
            // mtBtnLog
            //
            this.mtBtnLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnLog.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnLog.Image")));
            this.mtBtnLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnLog.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnLog.MergeIndex = 9;
            this.mtBtnLog.Name = "mtBtnLog";
            this.mtBtnLog.Size = new System.Drawing.Size(24, 24);
            this.mtBtnLog.Text = "toolStripButton1";
            this.mtBtnLog.ToolTipText = "Log transform the data";
            this.mtBtnLog.Click += new System.EventHandler(this.menuItemLogEset_Click);
            //
            // mtBtnNorm
            //
            this.mtBtnNorm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnNorm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mtBtnMnuLOWESS,
            this.mtBtnMnuLinReg,
            this.mtBtnMnuMAD,
            this.mtBtnMnuMeanC,
            this.mtBtnMnuQuan});
            this.mtBtnNorm.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnNorm.Image")));
            this.mtBtnNorm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnNorm.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnNorm.MergeIndex = 10;
            this.mtBtnNorm.Name = "mtBtnNorm";
            this.mtBtnNorm.Size = new System.Drawing.Size(34, 24);
            this.mtBtnNorm.Text = "toolStripDropDownButton2";
            this.mtBtnNorm.ToolTipText = "Normalize (Loess,MAD, etc)";
            //
            // mtBtnMnuLOWESS
            //
            this.mtBtnMnuLOWESS.Name = "mtBtnMnuLOWESS";
            this.mtBtnMnuLOWESS.Size = new System.Drawing.Size(194, 24);
            this.mtBtnMnuLOWESS.Text = "LOWESS";
            this.mtBtnMnuLOWESS.Click += new System.EventHandler(this.menuItemLOESSNorm_Click);
            //
            // mtBtnMnuLinReg
            //
            this.mtBtnMnuLinReg.Name = "mtBtnMnuLinReg";
            this.mtBtnMnuLinReg.Size = new System.Drawing.Size(194, 24);
            this.mtBtnMnuLinReg.Text = "Linear Regression";
            this.mtBtnMnuLinReg.Click += new System.EventHandler(this.menuItemLinReg_Click);
            //
            // mtBtnMnuMAD
            //
            this.mtBtnMnuMAD.Name = "mtBtnMnuMAD";
            this.mtBtnMnuMAD.Size = new System.Drawing.Size(194, 24);
            this.mtBtnMnuMAD.Text = "MAD";
            this.mtBtnMnuMAD.Click += new System.EventHandler(this.menuItemMAD_Click);
            //
            // mtBtnMnuMeanC
            //
            this.mtBtnMnuMeanC.Name = "mtBtnMnuMeanC";
            this.mtBtnMnuMeanC.Size = new System.Drawing.Size(194, 24);
            this.mtBtnMnuMeanC.Text = "Central Tendancy";
            this.mtBtnMnuMeanC.Click += new System.EventHandler(this.menuItemMeanC_Click);
            //
            // mtBtnMnuQuan
            //
            this.mtBtnMnuQuan.Name = "mtBtnMnuQuan";
            this.mtBtnMnuQuan.Size = new System.Drawing.Size(194, 24);
            this.mtBtnMnuQuan.Text = "Quantile";
            this.mtBtnMnuQuan.Click += new System.EventHandler(this.mnuItemQnorm_Click);
            //
            // toolStripSeparator5
            //
            this.toolStripSeparator5.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator5.MergeIndex = 11;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            //
            // mtBtnHist
            //
            this.mtBtnHist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnHist.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnHist.Image")));
            this.mtBtnHist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnHist.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnHist.MergeIndex = 12;
            this.mtBtnHist.Name = "mtBtnHist";
            this.mtBtnHist.Size = new System.Drawing.Size(24, 24);
            this.mtBtnHist.Text = "Histograms";
            this.mtBtnHist.Click += new System.EventHandler(this.mnuHistograms_Click);
            //
            // toolStripButton1
            //
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripButton1.MergeIndex = 13;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "Q-Q Plot";
            this.toolStripButton1.Click += new System.EventHandler(this.mnuItemQQplot_Click);
            //
            // mtBtnCorr
            //
            this.mtBtnCorr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnCorr.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnCorr.Image")));
            this.mtBtnCorr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnCorr.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnCorr.MergeIndex = 14;
            this.mtBtnCorr.Name = "mtBtnCorr";
            this.mtBtnCorr.Size = new System.Drawing.Size(24, 24);
            this.mtBtnCorr.Text = "Correlation Plots";
            this.mtBtnCorr.Click += new System.EventHandler(this.menuItemCorr_Click);
            //
            // mtBtnBoxPlots
            //
            this.mtBtnBoxPlots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnBoxPlots.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnBoxPlots.Image")));
            this.mtBtnBoxPlots.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnBoxPlots.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnBoxPlots.MergeIndex = 15;
            this.mtBtnBoxPlots.Name = "mtBtnBoxPlots";
            this.mtBtnBoxPlots.Size = new System.Drawing.Size(24, 24);
            this.mtBtnBoxPlots.Text = "Box Plots";
            this.mtBtnBoxPlots.Click += new System.EventHandler(this.menuItemBoxPlot_Click);
            //
            // mtBtnMA
            //
            this.mtBtnMA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnMA.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnMA.Image")));
            this.mtBtnMA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnMA.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnMA.MergeIndex = 16;
            this.mtBtnMA.Name = "mtBtnMA";
            this.mtBtnMA.Size = new System.Drawing.Size(24, 24);
            this.mtBtnMA.Text = "MA Plot";
            this.mtBtnMA.ToolTipText = "MA Plot";
            this.mtBtnMA.Click += new System.EventHandler(this.menuItemMAPlot_Click);
            //
            // mtBtnRollupPlot
            //
            this.mtBtnRollupPlot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnRollupPlot.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnRollupPlot.Image")));
            this.mtBtnRollupPlot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnRollupPlot.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnRollupPlot.MergeIndex = 17;
            this.mtBtnRollupPlot.Name = "mtBtnRollupPlot";
            this.mtBtnRollupPlot.Size = new System.Drawing.Size(24, 24);
            this.mtBtnRollupPlot.Text = "toolStripButton1";
            this.mtBtnRollupPlot.ToolTipText = "Plot Rollup";
            this.mtBtnRollupPlot.Click += new System.EventHandler(this.mnuProteinRollupPlot_Click);
            //
            // toolStripSeparator10
            //
            this.toolStripSeparator10.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator10.MergeIndex = 18;
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 27);
            //
            // mtBtnPCAPLS
            //
            this.mtBtnPCAPLS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnPCAPLS.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnPCAPLS.Image")));
            this.mtBtnPCAPLS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnPCAPLS.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnPCAPLS.MergeIndex = 19;
            this.mtBtnPCAPLS.Name = "mtBtnPCAPLS";
            this.mtBtnPCAPLS.Size = new System.Drawing.Size(24, 24);
            this.mtBtnPCAPLS.Text = "toolStripButton3";
            this.mtBtnPCAPLS.ToolTipText = "Principal Component Plot";
            this.mtBtnPCAPLS.Click += new System.EventHandler(this.menuItemPCAPlot_Click);
            //
            // mtBtnHeatmap
            //
            this.mtBtnHeatmap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnHeatmap.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnHeatmap.Image")));
            this.mtBtnHeatmap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnHeatmap.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnHeatmap.MergeIndex = 20;
            this.mtBtnHeatmap.Name = "mtBtnHeatmap";
            this.mtBtnHeatmap.Size = new System.Drawing.Size(24, 24);
            this.mtBtnHeatmap.Text = "toolStripButton1";
            this.mtBtnHeatmap.ToolTipText = "Heatmap";
            this.mtBtnHeatmap.Click += new System.EventHandler(this.mnuItemHeatmap_Click);
            //
            // mtBtnPatterns
            //
            this.mtBtnPatterns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnPatterns.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnPatterns.Image")));
            this.mtBtnPatterns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnPatterns.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnPatterns.MergeIndex = 21;
            this.mtBtnPatterns.Name = "mtBtnPatterns";
            this.mtBtnPatterns.Size = new System.Drawing.Size(24, 24);
            this.mtBtnPatterns.Text = "Pattern Search";
            this.mtBtnPatterns.Click += new System.EventHandler(this.mnuItemPatterns_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator2.MergeIndex = 22;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            //
            // mtBtnRefRollup
            //
            this.mtBtnRefRollup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnRefRollup.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnRefRollup.Image")));
            this.mtBtnRefRollup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnRefRollup.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnRefRollup.MergeIndex = 23;
            this.mtBtnRefRollup.Name = "mtBtnRefRollup";
            this.mtBtnRefRollup.Size = new System.Drawing.Size(24, 24);
            this.mtBtnRefRollup.Text = "toolStripButton1";
            this.mtBtnRefRollup.ToolTipText = "RRollup - Ref. Peptide Based Rollup";
            this.mtBtnRefRollup.Click += new System.EventHandler(this.menuItemRRollup_Click);
            //
            // mtBtnMedStdRup
            //
            this.mtBtnMedStdRup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnMedStdRup.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnMedStdRup.Image")));
            this.mtBtnMedStdRup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnMedStdRup.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnMedStdRup.MergeIndex = 24;
            this.mtBtnMedStdRup.Name = "mtBtnMedStdRup";
            this.mtBtnMedStdRup.Size = new System.Drawing.Size(24, 24);
            this.mtBtnMedStdRup.Text = "toolStripButton3";
            this.mtBtnMedStdRup.ToolTipText = "ZRollup - Median/StDev Rollup";
            this.mtBtnMedStdRup.Click += new System.EventHandler(this.menuItemZRollup_Click);
            //
            // mtBtnQrollUp
            //
            this.mtBtnQrollUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnQrollUp.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnQrollUp.Image")));
            this.mtBtnQrollUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnQrollUp.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnQrollUp.MergeIndex = 25;
            this.mtBtnQrollUp.Name = "mtBtnQrollUp";
            this.mtBtnQrollUp.Size = new System.Drawing.Size(24, 24);
            this.mtBtnQrollUp.Text = "toolStripButton4";
            this.mtBtnQrollUp.ToolTipText = "QRrollup";
            this.mtBtnQrollUp.Click += new System.EventHandler(this.menuItemQRup_Click);
            //
            // toolStripSeparator6
            //
            this.toolStripSeparator6.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator6.MergeIndex = 26;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            //
            // mtBtnDefFac
            //
            this.mtBtnDefFac.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnDefFac.Image = global::DAnTE.Properties.Resources.OrgChartHS;
            this.mtBtnDefFac.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnDefFac.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnDefFac.MergeIndex = 27;
            this.mtBtnDefFac.Name = "mtBtnDefFac";
            this.mtBtnDefFac.Size = new System.Drawing.Size(24, 24);
            this.mtBtnDefFac.Text = "toolStripButton2";
            this.mtBtnDefFac.ToolTipText = "Define Factors";
            this.mtBtnDefFac.Click += new System.EventHandler(this.mnuItemDefFactors_Click);
            //
            // mtBtnANOVA
            //
            this.mtBtnANOVA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnANOVA.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnANOVA.Image")));
            this.mtBtnANOVA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnANOVA.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnANOVA.MergeIndex = 28;
            this.mtBtnANOVA.Name = "mtBtnANOVA";
            this.mtBtnANOVA.Size = new System.Drawing.Size(24, 24);
            this.mtBtnANOVA.Text = "toolStripButton2";
            this.mtBtnANOVA.ToolTipText = "Run ANOVA";
            this.mtBtnANOVA.Click += new System.EventHandler(this.menuItemANOVA_Click);
            //
            // mtBtnpqFilter
            //
            this.mtBtnpqFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mtBtnpqFilter.Image = ((System.Drawing.Image)(resources.GetObject("mtBtnpqFilter.Image")));
            this.mtBtnpqFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mtBtnpqFilter.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mtBtnpqFilter.MergeIndex = 29;
            this.mtBtnpqFilter.Name = "mtBtnpqFilter";
            this.mtBtnpqFilter.Size = new System.Drawing.Size(24, 24);
            this.mtBtnpqFilter.Text = "toolStripButton2";
            this.mtBtnpqFilter.ToolTipText = "Filter Data Based on p/q Values";
            this.mtBtnpqFilter.Click += new System.EventHandler(this.mnuItemFilterpq_Click);
            //
            // toolStripSeparator14
            //
            this.toolStripSeparator14.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.toolStripSeparator14.MergeIndex = 30;
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 27);
            //
            // mhelpProviderDAnTE
            //
            this.mhelpProviderDAnTE.HelpNamespace = "";
            //
            // mCntxtMnuGrid
            //
            this.mCntxtMnuGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mCntxtMnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuItemPlotRows,
            this.toolStripSeparator13,
            this.contextMenuItemFilter,
            this.toolStripSeparator19,
            this.contextMenuItemCopySelected,
            this.contextMenuItemCopyAll});
            this.mCntxtMnuGrid.Name = "mCntxtMnuGrid";
            this.mCntxtMnuGrid.Size = new System.Drawing.Size(314, 140);
            //
            // contextMenuItemPlotRows
            //
            this.contextMenuItemPlotRows.Name = "contextMenuItemPlotRows";
            this.contextMenuItemPlotRows.Size = new System.Drawing.Size(313, 24);
            this.contextMenuItemPlotRows.Text = "Plot Selected Rows";
            this.contextMenuItemPlotRows.Click += new System.EventHandler(this.contextMenuItemPlotRows_Click);
            //
            // toolStripSeparator13
            //
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(310, 6);
            //
            // contextMenuItemFilter
            //
            this.contextMenuItemFilter.Name = "contextMenuItemFilter";
            this.contextMenuItemFilter.Size = new System.Drawing.Size(313, 24);
            this.contextMenuItemFilter.Text = "Filter Based on Selected RowIDs";
            this.contextMenuItemFilter.Click += new System.EventHandler(this.contextMenuItemFilter_Click);
            //
            // toolStripSeparator19
            //
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(310, 6);
            //
            // contextMenuItemCopySelected
            //
            this.contextMenuItemCopySelected.Name = "contextMenuItemCopySelected";
            this.contextMenuItemCopySelected.Size = new System.Drawing.Size(313, 24);
            this.contextMenuItemCopySelected.Text = "Copy to clipboard (Ctrl+C)";
            this.contextMenuItemCopySelected.Click += new System.EventHandler(this.contextMenuItemCopySelected_Click);
            //
            // contextMenuItemCopyAll
            //
            this.contextMenuItemCopyAll.Name = "contextMenuItemCopyAll";
            this.contextMenuItemCopyAll.Size = new System.Drawing.Size(313, 24);
            this.contextMenuItemCopyAll.Text = "Copy all to clipboard";
            this.contextMenuItemCopyAll.Click += new System.EventHandler(this.contextMenuItemCopyAll_Click);
            //
            // frmDAnTE
            //
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(762, 494);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuStripDAnTE;
            this.Name = "frmDAnTE";
            this.Text = "Main";
            this.Activated += new System.EventHandler(this.frmDAnTE_Activated);
            this.Closed += new System.EventHandler(this.OnClosed_event);
            this.Load += new System.EventHandler(this.OnLoad_event);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.mnuStripDAnTE.ResumeLayout(false);
            this.mnuStripDAnTE.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.mContextMenuTreeV.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelRowNum)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.mtoolStripDAnTE.ResumeLayout(false);
            this.mtoolStripDAnTE.PerformLayout();
            this.mCntxtMnuGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private ToolStripButton mtBtnANOVA;
        private ToolStripButton mtBtnDefFac;
        private ToolStripButton mtBtnpqFilter;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem mnuAnalysisSummary;
        private ToolStripMenuItem mnuItemExplore;
        private ToolStripMenuItem mnuItemPCAPLS;
        private ToolStripMenuItem mnuItemHeatmap;
        private ToolStripMenuItem mnuItemPatterns;
        private ToolStripButton mtBtnMA;
        private ToolStripButton mtBtnPatterns;
        private ToolStripMenuItem mnuItemOneSTtest;
        private ToolStripMenuItem groupingToolStripMenuItem;
        private ToolStripMenuItem mnuItemDefFactors;
        private ToolStripMenuItem mnuItemMergeCols;
        private ToolStripMenuItem mnuArrangeColumns;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripMenuItem mnuItemWilcox;
        private ToolStripMenuItem mnuItemKruskal;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem mnuItemPlotSelectedRows;
        private ToolStripMenuItem mnuItemFilterUsingSelectedRowIDs;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripMenuItem mnuShowRCommandLog;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripMenuItem contextMenuItemCopySelected;
        private ToolStripMenuItem contextMenuItemCopyAll;
    }
}
