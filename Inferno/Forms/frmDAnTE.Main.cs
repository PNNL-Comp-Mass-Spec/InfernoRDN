using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public partial class frmDAnTE : System.Windows.Forms.Form
  {
    #region Other Variables

    public const int MAX = 20;
    private IContainer components;
    private TabControl mtabControlData;
    // Tab Page controls 
    private TabPage ctltabPage;
    // Tab controls for Expressions

    private frmShowProgress mfrmShowProgress;

    private ArrayList marrDataSetNames = new ArrayList();

    private string[] mstrArrProteins = null;
    private string[] mstrArrMassTags = null;

    private string mstrLoadedfileName; //filename of the loaded data

    private string sessionFile = null;
    private ArrayList marrSessionVarType = new ArrayList();

    private string tempPath = @"c:";
    private string tempFile = "";
    private clsRconnect rConnector;

    private string mstrFldgTitle;

    private enmDataType dataSetType = enmDataType.ESET;
    private static frmDAnTE m_frmDAnTE;
    private BackgroundWorker m_BackgroundWorker;

    private frmDAnTEmdi m_frmDAnTEmdi;
    private ToolStripMenuItem mnuItemMissFilt;
    private ToolStripMenuItem mnuItemFC;
    private ToolStripMenuItem ctxtMnuItemFilter;
    private ToolStripSeparator toolStripSeparator13;

    private Hashtable mhtDatasets = new Hashtable();
    private Hashtable mhtAnalysisObjects = new Hashtable();
    private ArrayList marrAnalysisObjects = new ArrayList();
    private ToolStripMenuItem mnuItemVenn;

    private int mintFilterTblNum = 0;

    #endregion

    #region Form Constructor

    public frmDAnTE()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      Settings.Default.SessionFileName = null;
      Settings.Default.Save();

      if (IsMdiChild) {
        //ToolStripManager.Merge(this.mtoolStripDAnTE, "mtoolStripMDI");
        this.mtoolStripDAnTE.Visible = false;
      }

      mfrmShowProgress = new frmShowProgress();

      //Threading -----------------------------------
      this.m_BackgroundWorker = new BackgroundWorker();
    }

    #endregion

    #region Private methods

    public static frmDAnTE GetChildInstance()
    {
      if (m_frmDAnTE == null || m_frmDAnTE.IsDisposed) //if not created yet, Create an instance
        m_frmDAnTE = new frmDAnTE();
      return m_frmDAnTE;  //just created or created earlier.Return it
    }

    #endregion

    #region Event handlers --------------

    private void OnLoad_event(object sender, System.EventArgs e)
    {
      //if (IsMdiChild)
      //{
      //    mnuStripDAnTE.Visible = false;
      //    mtoolStripDAnTE.Visible = false;
      //    frmDAnTEmdi mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
      //    ToolStripManager.RevertMerge(mp.mtoolStripMDI); //toolstrip refere to parent toolstrip
      //    ToolStripManager.Merge(this.mtoolStripDAnTE, mp.mtoolStripMDI);
      //}
      if (sessionFile != null) {
        OpenSessionThreaded(sessionFile);
      }
    }

    private void OnClosed_event(object sender, System.EventArgs e)
    {
      ToolStripManager.RevertMerge("mtoolStripMDI");
    }

    /// <summary>
    /// What to do when an item from the treeview control is selected
    /// </summary>
    private void ctltreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
        ctltreeView.SelectedNode = ctltreeView.GetNodeAt(e.X, e.Y);
      NodeSelect(e.Node);
    }

    private void ctltreeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      NodeSelect(e.Node);
    }

    private void Form_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.All;
      else
        e.Effect = DragDropEffects.None;
    }

    private void Form_DragDrop(object sender, DragEventArgs e)
    {
      string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

      if (s.Length > 1)
        MessageBox.Show("Only one file at a time!", "One file please...",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
      else {
        string fExt = Path.GetExtension(s[0]);
        if (fExt.Equals(".dnt"))
          OpenSessionThreaded(s[0]);
        else if (fExt.Equals(".csv")) {
          dataSetType = enmDataType.ESET;
          mstrLoadedfileName = s[0];
          DataFileOpenThreaded(s[0], "Opening data in a flat file...");
        } else {
          MessageBox.Show("Wrong file type!", "Use only .dnt files...",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
      }

    }

    private void helpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, this.mhelpProviderDAnTE.HelpNamespace);
    }

    #endregion

    #region Properties

    public clsRconnect RConnector
    {
      set
      {
        rConnector = value;
      }
    }

    public string TempLocation
    {
      set { tempPath = value; }
    }

    public string TempFile
    {
      set { tempFile = value; }
    }

    public frmDAnTEmdi ParentInstance
    {
      set
      {
        m_frmDAnTEmdi = value;
      }
    }

    public ToolStrip ToolStripDAnTE
    {
      get
      {
        return mtoolStripDAnTE;
      }
    }

    public string Title
    {
      get
      {
        return this.Text;
      }
      set
      {
        this.Text = value;
      }
    }

    public string SessionFile
    {
      set
      {
        sessionFile = value;
      }
    }

    #endregion

    private void frmDAnTE_Activated(object sender, EventArgs e)
    {
      if (IsMdiChild) {
        mnuStripDAnTE.Visible = false;
        mtoolStripDAnTE.Visible = false;
        frmDAnTEmdi mp = (frmDAnTEmdi)Application.OpenForms["frmDAnTEmdi"];
        ToolStripManager.RevertMerge(mp.mtoolStripMDI); //toolstrip refere to parent toolstrip
        ToolStripManager.Merge(this.mtoolStripDAnTE, mp.mtoolStripMDI);
      }
    }
  }
}
