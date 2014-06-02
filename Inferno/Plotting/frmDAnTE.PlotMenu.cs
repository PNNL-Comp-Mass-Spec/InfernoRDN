using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    clsHistogramPar mclsHistPar;
    clsQQPar mclsQQPar;
    clsCorrelationPar mclsCorrPar;
    clsBoxPlotPar mclsBoxPlotPar;
    clsMAplotsPar mclsMApar;
    clsVennPar mclsVennPar;

    #region Plot Menu items

    private void mnuItemQQplot_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsQQPar = new clsQQPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsQQPar.tempFile = tempFile;
          mclsQQPar.Rdataset = Rdataset;
          mclsQQPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, Rdataset);
          mclsQQPar.mstrDatasetName = mclsSelected.mstrDataText;

          PlotQQ(mclsQQPar);
        }
      }
    }

    public void PlotQQ(clsQQPar clsQQPar)
    // this will be called from the plot forms.
    // thus the reason to be public
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_QQPlotCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmQQPar mfrmQQPar;

        mfrmQQPar = new frmQQPar(clsQQPar);

        DialogResult res = mfrmQQPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsQQPar = mfrmQQPar.clsQQPar;

          mclsRplots = new clsRplotData(clsQQPar.Rcmd, "QQ");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating Q-Q Plots ...";
          mfrmShowProgress.ShowDialog();
        }
        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_QQPlotCompleted);
        #endregion
      }
    }

    /// <summary>
    /// Plot histograms
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void mnuHistogrms_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsHistPar = new clsHistogramPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsHistPar.tempFile = tempFile;
          mclsHistPar.Rdataset = Rdataset;
          mclsHistPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, Rdataset);
          mclsHistPar.mstrDatasetName = mclsSelected.mstrDataText;

          PlotHistograms(mclsHistPar);
        }
      }
    }

    public void PlotHistograms(clsHistogramPar clsHistPar)
    // this will be called from the plot forms.
    // thus the reason to be public
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_HistPlotCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmHistogramPar mfrmHistPar;

        mfrmHistPar = new frmHistogramPar(clsHistPar);

        DialogResult res = mfrmHistPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsHistPar = mfrmHistPar.clsHistPar;

          mclsRplots = new clsRplotData(clsHistPar.Rcmd, "Hist");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating Histograms ...";
          mfrmShowProgress.ShowDialog();
        }
        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_HistPlotCompleted);
        #endregion
      }
    }

    private void menuItemCorr_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsCorrPar = new clsCorrelationPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsCorrPar.tempFile = tempFile;
          mclsCorrPar.Rdataset = Rdataset;
          mclsCorrPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, Rdataset);
          mclsCorrPar.mstrDatasetName = mclsSelected.mstrDataText;

          PlotCorrelation(mclsCorrPar);
        }
      }
    }

    public void PlotCorrelation(clsCorrelationPar clsCorrPar)
    // this will be called from the plot forms.
    // thus the reason to be public
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_CorrPlotCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmCorrelationPar mfrmCorrPar;
        mfrmCorrPar = new frmCorrelationPar(clsCorrPar);

        DialogResult res = mfrmCorrPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsCorrPar = mfrmCorrPar.clsCorrPar;
          mclsRplots = new clsRplotData(mclsCorrPar.Rcmd, "Corr");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating Correlation Plot ...";
          mfrmShowProgress.ShowDialog();
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_CorrPlotCompleted);
        #endregion
      }
    }

    private void menuItemBoxPlot_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsBoxPlotPar = new clsBoxPlotPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsBoxPlotPar.tempFile = tempFile;
          mclsBoxPlotPar.Rdataset = Rdataset;
          mclsBoxPlotPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, Rdataset);
          mclsBoxPlotPar.mstrDatasetName = mclsSelected.mstrDataText;
          if (mhtDatasets.ContainsKey("Factors")) {
            clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
            mclsBoxPlotPar.Factors = clsDataTable.DataTableRows(mclsFactors.mDTable);
          } else
            mclsBoxPlotPar.Factors = null;

          PlotBoxPlots(mclsBoxPlotPar);
        }
      }
    }

    public void PlotBoxPlots(clsBoxPlotPar clsBoxPlotPar)
    // this will be called from the plot forms.
    // thus the reason to be public
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_BoxPlotCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmBoxPlotPar mfrmBoxplotPar;

        mfrmBoxplotPar = new frmBoxPlotPar(clsBoxPlotPar);

        DialogResult res = mfrmBoxplotPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsBoxPlotPar = mfrmBoxplotPar.clsBoxPlotPar;
          mclsRplots = new clsRplotData(mclsBoxPlotPar.Rcmd, "Box");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating the Box Plot ...";
          mfrmShowProgress.ShowDialog();
        }

        #region Unhook Threading events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_BoxPlotCompleted);
        #endregion
      }
    }

    private void mnuProteinRollupPlot_Click(object sender, EventArgs e)
    {
      ArrayList marrProtSets = new ArrayList();
      bool mblPlot = false;

      if (mhtDatasets.ContainsKey("QRollup")) {
        marrProtSets.Add("QRollup");
        mblPlot = true;
      }
      if (mhtDatasets.ContainsKey("RRollup")) {
        marrProtSets.Add("RRollup");
        mblPlot = true;
      }
      if (mhtDatasets.ContainsKey("ZRollup")) {
        marrProtSets.Add("ZRollup");
        mblPlot = true;
      }
      if (mhtDatasets.ContainsKey("Protein Info")) {
        marrProtSets.Add("None<raw>");
        mblPlot = true;
      }

      if (mblPlot) {
        frmPlotProteinRollup mfrmPlotRollup = new frmPlotProteinRollup();
        mfrmPlotRollup.PopulateDataComboBox = AvailableDataSources();
        mfrmPlotRollup.PopulatePDataComboBox = marrProtSets;
        mfrmPlotRollup.TempFile = tempFile;
        mfrmPlotRollup.RConnect = rConnector;
        mfrmPlotRollup.ParentRef = (frmDAnTEmdi)this.MdiParent;
        mfrmPlotRollup.m_frmDAnTE = this;
        mfrmPlotRollup.Show();
      }
      //else
      //    MessageBox.Show("No Protein Information found.","Error!",MessageBoxButtons.OK,
      //        MessageBoxIcon.Error);
    }

    private void menuItemMAPlot_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsMApar = new clsMAplotsPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsMApar.tempFile = tempFile;
          mclsMApar.Rdataset = Rdataset;
          mclsMApar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, Rdataset);
          mclsMApar.mstrDatasetName = mclsSelected.mstrDataText;

          PlotMA(mclsMApar);
        }
      }
    }

    public void PlotMA(clsMAplotsPar clsMApar)
    // this will be called from the plot forms.
    // thus the reason to be public
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_MAplotCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmMAplotsPar mfrmMAPar;

        mfrmMAPar = new frmMAplotsPar(clsMApar);

        DialogResult res = mfrmMAPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsMApar = mfrmMAPar.clsMAplotPar;
          mclsRplots = new clsRplotData(mclsMApar.Rcmd, "MA");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating MA Plots ...";
          mfrmShowProgress.ShowDialog();
        }
        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_MAplotCompleted);
        #endregion
      }
    }

    private void mnuItemPlotRows_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;
        frmPlotDisplay mfrmPlotDisplay = new frmPlotDisplay();
        DataGridView currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable) {
          string Rdataset = mclsSelected.mstrRdatasetName;
          string rcmd = null;
          string selctedRows = "c(";

          DataGridViewSelectedRowCollection selectedRows = currGrid.SelectedRows;

          if (currGrid.SelectedRows.Count > 100)
            MessageBox.Show("Maximum number of rows is set to 100." + Environment.NewLine +
                "Select less than 100 rows.", "Too many rows to plot",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          else {
            foreach (DataGridViewRow row in selectedRows) {
              selctedRows += @"""" + row.Cells[0].Value + @""",";
            }
            selctedRows = selctedRows.Substring(0, selctedRows.Length - 1) + ")";

            rcmd = "PlotRows(" + Rdataset + "," + selctedRows + @",file=""" + tempFile + @""")";
            try {
              rConnector.EvaluateNoReturn(rcmd);
              mfrmPlotDisplay.Image = LoadImage(tempFile);
              mfrmPlotDisplay.EnableParameterMenu = false;
              mfrmPlotDisplay.MdiParent = m_frmDAnTEmdi;
              mfrmPlotDisplay.Title = "Table Rows";
              mfrmPlotDisplay.Show();
            }
            catch (Exception ex) {
              Console.WriteLine(ex.Message);
            }

          }
        }
      }
    }

    private void mnuItemVenn_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mclsSelected.mDTable != null && mclsSelected.mblIsPlottable &&
            (mclsSelected.mDTable.Columns.Count > 1)) {
          mclsVennPar = new clsVennPar();
          string Rdataset = mclsSelected.mstrRdatasetName;

          mclsVennPar.tempFile = tempFile;
          mclsVennPar.Rdataset = Rdataset;
          mclsVennPar.mhtDatasets = mhtDatasets;
          mclsVennPar.marrDatasets = AvailableDataSources();

          mclsVennPar.mstrDatasetName = mclsSelected.mstrDataText;
          if (mhtDatasets.ContainsKey("Factors")) {
            clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
            mclsVennPar.marrFactorNames = clsDataTable.DataTableRows(mclsFactors.mDTable);
            mclsVennPar.marrFactors = marrFactorInfo;
          } else
            mclsVennPar.marrFactorNames = null;

          PlotVenn(mclsVennPar);
        }
      }
    }

    public void PlotVenn(clsVennPar mclsVennPar)
    {
      if (mtabControlData.Controls.Count != 0) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_VennCompleted);
        #endregion

        clsRplotData mclsRplots;
        frmVennDiagramPar mfrmVennPar;

        mfrmVennPar = new frmVennDiagramPar(mclsVennPar);
        DialogResult res = mfrmVennPar.ShowDialog();
        if (res == DialogResult.OK) {
          mclsVennPar = mfrmVennPar.clsVennPar;
          mclsRplots = new clsRplotData(mclsVennPar.Rcmd, "Venn");

          m_BackgroundWorker.RunWorkerAsync(mclsRplots);
          mfrmShowProgress.Message = "Generating Venn Diagram ...";
          mfrmShowProgress.ShowDialog();
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_VennCompleted);
        #endregion
      }
    }

    #endregion
  }
}