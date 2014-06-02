using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {

    private ArrayList marrDatasetInfo = new ArrayList(); //of type clsDatasetInfo
    private ArrayList marrFactorInfo = new ArrayList(); // of type clsFactorInfo
    private clsAnovaPar mclsAnovaPar;
    private clsTamuQPar mclsTamuQPar;
    private clsKruskalWPar mclsKWpar;
    private clsWilcoxonPar mclsWilcoxPar;
    private clsShapiroWilksPar mclsShapiroWilksPar;
    private clsFoldChangePar mclsFoldChangePar;
    private clsOneSampleTtestPar mclsOneSampleTtestPar;
    string Rdataset = null;

    #region Private Methods
    /// <summary>
    /// Extract Factor information from a datatable 
    /// to an arraylist of clsDatasetInfo type.
    /// </summary>
    /// <param name="dt"></param>
    private void DatasetFactorInfo(DataTable dt, bool factorsLoaded)
    {
      ArrayList names = clsDataTable.DataTableColumns(dt, true); // get data only columns
      ArrayList marrFactors = new ArrayList();
      clsDatasetInfo dsetItem;
      string factorname = null;

      marrDatasetInfo.Clear();
      for (int i = 0; i < names.Count; i++) {
        dsetItem = new clsDatasetInfo(names[i].ToString());
        if (factorsLoaded) {
          clsFactorInfo tmpFactorInfo = new clsFactorInfo();
          for (int k = 0; k < dt.Rows.Count; k++) // go thru each row
                    {
            DataRow mDrow = dt.Rows[k];
            if (i == 0) {
              factorname = mDrow.ItemArray[i].ToString(); // factor name at 0th position
              marrFactors.Add(factorname);
            }
            Factor currFactor = new Factor(marrFactors[k].ToString(), mDrow.ItemArray[i + 1].ToString());
            dsetItem.marrFactorAssnmnts.Add(currFactor);
          }
          //dsetItem.marrFactors = marrFactors;
          dsetItem.factorsSET = true;
        }
        marrDatasetInfo.Add(dsetItem);
      }
    }

    /// <summary>
    /// Transform an Arraylist of DatasetInfo to a Datatable of Factor info.
    /// </summary>
    /// <returns></returns>
    private DataTable DatasetArr2DT()
    {
      DataTable mDataTable = new DataTable();
      DataColumn mDataColumn;
      DataRow mDataRow;

      mDataColumn = new DataColumn();
      mDataColumn.DataType = System.Type.GetType("System.String");
      mDataColumn.ColumnName = "Factors";
      mDataTable.Columns.Add(mDataColumn);

      for (int i = 0; i < marrDatasetInfo.Count; i++) {
        mDataColumn = new DataColumn();
        mDataColumn.DataType = System.Type.GetType("System.String");
        mDataColumn.ColumnName = ((clsDatasetInfo)marrDatasetInfo[i]).mstrDataSetName;
        mDataTable.Columns.Add(mDataColumn);
      }
      for (int i = 0; i < ((clsDatasetInfo)marrDatasetInfo[0]).marrFactorAssnmnts.Count; i++) {
        mDataRow = mDataTable.NewRow();
        mDataRow[0] = ((Factor)((clsDatasetInfo)marrDatasetInfo[0]).marrFactorAssnmnts[i]).Name;
        for (int j = 0; j < marrDatasetInfo.Count; j++) {
          mDataRow[j + 1] = ((Factor)((clsDatasetInfo)marrDatasetInfo[j]).marrFactorAssnmnts[i]).Value;
        }
        mDataTable.Rows.Add(mDataRow);
      }
      return mDataTable;
    }

    private ArrayList MakeDeepCopy(ArrayList marrIN) //Deep copy arraylists of classes
    {
      ArrayList copyTo = new ArrayList();
      foreach (object obj in marrIN) {
        copyTo.Add(((ICloneable)obj).Clone());
      }
      return copyTo;
    }

    private bool t_testPossible()
    {
      bool ttest = false;
      for (int i = 0; i < marrFactorInfo.Count; i++) {
        if (((clsFactorInfo)marrFactorInfo[i]).marrValues.Count == 2)
          ttest = true;
      }
      return ttest;
    }

    private void ChangeDatasetOrder(ArrayList marrNewNameOrder)
    {
      ArrayList marrOldOrder = new ArrayList();
      ArrayList marr2Remove = new ArrayList();
      DataTable expTable = new DataTable();
      clsDatasetTreeNode currentNode = new clsDatasetTreeNode();

      if (mhtDatasets.ContainsKey("Expressions")) {
        expTable = ((clsDatasetTreeNode)mhtDatasets["Expressions"]).mDTable;
        for (int num = 1; num < expTable.Columns.Count; num++) {
          marrOldOrder.Add(expTable.Columns[num].ColumnName);
        }
        marr2Remove.Clear();
        for (int num = 0; num < marrOldOrder.Count; num++) {
          if (!marrNewNameOrder.Contains(marrOldOrder[num]))
            marr2Remove.Add(marrOldOrder[num]);
        }

        foreach (string strKey in mhtDatasets.Keys) {
          currentNode = ((clsDatasetTreeNode)mhtDatasets[strKey]);
          if (currentNode.mblIsNumeric) {
            expTable = currentNode.mDTable;
            for (int num = 0; num < marr2Remove.Count; num++) //remove columns
                        {
              expTable.Columns.Remove((string)marr2Remove[num]);
            }
            for (int num = 0; num < marrNewNameOrder.Count; num++) //reorder columns
                        {
              if (expTable.Columns.Contains("PepCount"))
                expTable.Columns[(string)marrNewNameOrder[num]].SetOrdinal(num + 2);
              else
                expTable.Columns[(string)marrNewNameOrder[num]].SetOrdinal(num + 1);
            }
          }
        }
      }
    }

    private void ChangeDatasetOrderR(ArrayList marrNewOrder)
    {
      string mstrOrder = @"newOrder=""c(";
      string rcmd = null;
      string vars = "numvars=" + NumericVars2R();

      for (int num = 0; num < marrNewOrder.Count; num++) {
        int currVal = (int)marrNewOrder[num] + 1;
        mstrOrder += currVal.ToString() + ",";
      }
      mstrOrder = mstrOrder.Substring(0, mstrOrder.LastIndexOf(",")) + @")""";
      rcmd = "arrangeColumns(" + vars + "," + mstrOrder + ")";
      Console.WriteLine(rcmd);
      try {
        rConnector.EvaluateNoReturn("ls()");
        rConnector.EvaluateNoReturn(rcmd);
      }
      catch (Exception ex) {
        MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
      }
    }

    #endregion

    #region Statistics Menu items

    private void mnuItemDefFactors_Click(object sender, EventArgs e)
    {
      ArrayList tmpDatasets = MakeDeepCopy(marrDatasetInfo); // keep copies
      ArrayList tmpFactors = MakeDeepCopy(marrFactorInfo); // keep copies
      //ArrayList newOrder = new ArrayList();
      DataTable mDTFactors = new DataTable();
      string mstrFrom = sender.ToString();

      if (mhtDatasets.ContainsKey("Factors") || mhtDatasets.ContainsKey("Expressions")) {
        frmFactorInformation mfrmFactorInfo = new frmFactorInformation();
        mfrmFactorInfo.DatasetInfo = marrDatasetInfo;
        mfrmFactorInfo.FactorsLoaded = mhtDatasets.ContainsKey("Factors");
        mfrmFactorInfo.FactorInfo = marrFactorInfo;
        if (mstrFrom.Equals("Arrange Columns")) {
          mfrmFactorInfo.OrderChangeOnly = true;
          mfrmFactorInfo.Title = "Dataset Order";
          mfrmFactorInfo.SubTitle = "Change Dataset Order, Delete Datasets";
          mfrmFactorInfo.WinTitle = "Dataset Order";
        }
        if (mfrmFactorInfo.ShowDialog() == DialogResult.OK) {
          marrDatasetInfo = mfrmFactorInfo.DatasetInfo;
          if (mfrmFactorInfo.OrderChanged) {
            ChangeDatasetOrder(mfrmFactorInfo.NewDatasetNameOrder);
            ChangeDatasetOrderR(mfrmFactorInfo.NewDatasetOrder);
          }
          marrFactorInfo = mfrmFactorInfo.FactorInfo;
          if (marrFactorInfo.Count > 0) {
            mDTFactors = DatasetArr2DT();
            mDTFactors.Columns[0].ColumnName = "Factors";
            mDTFactors.TableName = "factors";
            AddDataset2HashTable(mDTFactors);
            if (mhtDatasets.Contains("Factors"))
              AddDataNode((clsDatasetTreeNode)mhtDatasets["Factors"]);

            if (rConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors)) {
              try {
                rConnector.EvaluateNoReturn("print(factors)");
                rConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
              }
              catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
              }
            }
          }
        } else //User cancels the factor changes, so revert to previous.
                {
          marrDatasetInfo = tmpDatasets; //Should we do a DeepCopy here?
          marrFactorInfo = tmpFactors;
        }
      } else
        MessageBox.Show("First load some data!", "No data yet!", MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
    }

    private void menuItemANOVA_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mclsSelected != null && mclsSelected.mDTable != null && mclsSelected.mblIsNumeric &&
          mhtDatasets.ContainsKey("Factors")) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_ANOVA);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion

        mclsAnovaPar = new clsAnovaPar();
        Rdataset = mclsSelected.mstrRdatasetName;

        mclsAnovaPar.tempFile = tempFile;
        mclsAnovaPar.Rdataset = Rdataset;
        mclsAnovaPar.mstrDatasetName = mclsSelected.mstrDataText;
        clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
        mclsAnovaPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

        frmANOVApar mfrmAnovaPar;

        int numCols = mclsSelected.mDTable.Columns.Count - 1;
        if (numCols > 0 && mclsSelected.mblIsNumeric) {
          mfrmAnovaPar = new frmANOVApar(mclsAnovaPar);
          if (mclsFactors.mDTable != null) {
            mfrmAnovaPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
            if (mfrmAnovaPar.ShowDialog() == DialogResult.OK) {
              mclsAnovaPar = mfrmAnovaPar.clsAnovaPar;

              Add2AnalysisHTable(mclsAnovaPar, "ANOVA");

              m_BackgroundWorker.RunWorkerAsync(mclsAnovaPar.Rcmd);
              mfrmShowProgress.Message = "Performing ANOVA ...";
              mfrmShowProgress.ShowDialog();
            }
          }
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_ANOVA);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion
      }
    }

    private void menuItemTamuQ_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mclsSelected != null && mclsSelected.mDTable != null && mclsSelected.mblIsNumeric &&
          mhtDatasets.ContainsKey("Factors")) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_TamuQ);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_TamuQCompleted);
        #endregion

        mclsTamuQPar = new clsTamuQPar();
        Rdataset = mclsSelected.mstrRdatasetName;

        mclsTamuQPar.tempFile = tempFile;
        mclsTamuQPar.Rdataset = Rdataset;
        mclsTamuQPar.mstrDatasetName = mclsSelected.mstrDataText;
        clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
        mclsTamuQPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

        frmTamuQpar mfrmTamuQPar;

        int numCols = mclsSelected.mDTable.Columns.Count - 1;
        if (numCols > 0 && mclsSelected.mblIsNumeric) {
          mfrmTamuQPar = new frmTamuQpar(mclsTamuQPar);
          if (mclsFactors.mDTable != null) {
            mfrmTamuQPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
            if (mfrmTamuQPar.ShowDialog() == DialogResult.OK) {
              mclsTamuQPar = mfrmTamuQPar.clsTamuQPar;

              Add2AnalysisHTable(mclsAnovaPar, "TamuQ");

              m_BackgroundWorker.RunWorkerAsync(mclsTamuQPar.Rcmd);
              mfrmShowProgress.Message = "Performing TamuQ ...";
              mfrmShowProgress.ShowDialog();
            }
          }
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_TamuQ);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_TamuQCompleted);
        #endregion
      }
    }

    private void menuItemKW_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mclsSelected.mDTable != null && mclsSelected.mblIsNumeric &&
          mhtDatasets.ContainsKey("Factors")) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_KW);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion

        mclsKWpar = new clsKruskalWPar();
        Rdataset = mclsSelected.mstrRdatasetName;

        mclsKWpar.tempFile = tempFile;
        mclsKWpar.Rdataset = Rdataset;
        mclsKWpar.mstrDatasetName = mclsSelected.mstrDataText;
        clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
        mclsKWpar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

        frmKruskalWpar mfrmKWPar;

        int numCols = mclsSelected.mDTable.Columns.Count - 1;
        if (numCols > 0 && mclsSelected.mblIsNumeric) {
          mfrmKWPar = new frmKruskalWpar(mclsKWpar);
          if (mclsFactors.mDTable != null) {
            mfrmKWPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
            if (mfrmKWPar.ShowDialog() == DialogResult.OK) {
              mclsKWpar = mfrmKWPar.clsKruskalWPar;

              Add2AnalysisHTable(mclsKWpar, "Kruskal-Walis_Test");

              m_BackgroundWorker.RunWorkerAsync(mclsKWpar.Rcmd);
              mfrmShowProgress.Message = "Performing KW test ...";
              mfrmShowProgress.ShowDialog();
            }
          }
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_KW);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion
      }
    }

    private void menuItemWilcox_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (!t_testPossible() || !mhtDatasets.ContainsKey("Factors"))
        MessageBox.Show("There is not a single factor with two levels" +
            Environment.NewLine + " to perform the Wilcoxon Test.", "No suitable factors",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      else
        if (mclsSelected.mDTable != null && mclsSelected.mblIsNumeric) {
          #region Hook Threading Events
          m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_Wilcox);
          m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
              m_BackgroundWorker_ANOVACompleted);
          #endregion

          mclsWilcoxPar = new clsWilcoxonPar();
          Rdataset = mclsSelected.mstrRdatasetName;

          mclsWilcoxPar.tempFile = tempFile;
          mclsWilcoxPar.Rdataset = Rdataset;
          mclsWilcoxPar.mstrDatasetName = mclsSelected.mstrDataText;
          clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
          mclsWilcoxPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

          frmWilcoxonPar mfrmWilcoxPar;

          int numCols = mclsSelected.mDTable.Columns.Count - 1;
          if (numCols > 0 && mclsSelected.mblIsNumeric) {
            mfrmWilcoxPar = new frmWilcoxonPar(mclsWilcoxPar);
            if (mclsFactors.mDTable != null) {
              mfrmWilcoxPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
              mfrmWilcoxPar.FactorList = marrFactorInfo;
              if (mfrmWilcoxPar.ShowDialog() == DialogResult.OK) {
                mclsWilcoxPar = mfrmWilcoxPar.clsWilcoxonPar;

                Add2AnalysisHTable(mclsWilcoxPar, "Wilcoxon_Test");

                m_BackgroundWorker.RunWorkerAsync(mclsWilcoxPar.Rcmd);
                mfrmShowProgress.Message = "Performing Wilcoxon test ...";
                mfrmShowProgress.ShowDialog();
              }
            }
          }

          #region Unhook Threading Events
          m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_Wilcox);
          m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
              m_BackgroundWorker_ANOVACompleted);
          #endregion
        }
    }

    private void menuItemShapiroWilks_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mclsSelected.mDTable != null && mclsSelected.mblIsNumeric) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_ShapiroWilks);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion

        mclsShapiroWilksPar = new clsShapiroWilksPar();
        Rdataset = mclsSelected.mstrRdatasetName;

        mclsShapiroWilksPar.Rdataset = Rdataset;
        mclsShapiroWilksPar.mstrDatasetName = mclsSelected.mstrDataText;

        frmShapiroWilksPar mfrmShapiroWilksPar;

        int numCols = mclsSelected.mDTable.Columns.Count - 1;
        if (numCols > 0 && mclsSelected.mblIsNumeric) {
          mfrmShapiroWilksPar = new frmShapiroWilksPar(mclsShapiroWilksPar);
          if (mfrmShapiroWilksPar.ShowDialog() == DialogResult.OK) {
            mclsShapiroWilksPar = mfrmShapiroWilksPar.clsShapiroWilksPar;

            Add2AnalysisHTable(mclsShapiroWilksPar, "Shapiro-Wilks_Test");

            m_BackgroundWorker.RunWorkerAsync(mclsShapiroWilksPar.Rcmd);
            mfrmShowProgress.Message = "Performing Shapiro-Wilks test ...";
            mfrmShowProgress.ShowDialog();
          }
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_ShapiroWilks);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion
      }
    }

    private void mnuItemFC_Click(object sender, EventArgs e)
    {
      DataTable mDTFoldChange = new DataTable();
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mhtDatasets.ContainsKey("Expressions") && mhtDatasets.ContainsKey("Factors") &&
          mclsSelected.mblIsNumeric) {
        mclsFoldChangePar = new clsFoldChangePar();
        Rdataset = mclsSelected.mstrRdatasetName;
        mclsFoldChangePar.Rdataset = Rdataset;
        mclsFoldChangePar.mstrDatasetName = mclsSelected.mstrDataText;
        mclsFoldChangePar.marrFactors = marrFactorInfo;

        frmFoldChangePar mfrmFC = new frmFoldChangePar(mclsFoldChangePar);

        if (mfrmFC.ShowDialog() == DialogResult.OK) {
          mclsFoldChangePar = mfrmFC.clsFoldChangePar;
          Add2AnalysisHTable(mclsFoldChangePar, "FoldChanges");

          try {
            rConnector.EvaluateNoReturn(mclsFoldChangePar.Rcmd);
            if (rConnector.GetTableFromRmatrix("foldChanges")) {
              mDTFoldChange = rConnector.DataTable.Copy();
              if (mDTFoldChange != null) {
                mDTFoldChange.TableName = "foldChanges";
                AddDataset2HashTable(mDTFoldChange);
                AddDataNode((clsDatasetTreeNode)mhtDatasets["Fold Changes"]);
              }
            }
          }
          catch (Exception ex) {
            MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
          }

        }
      }
    }

    private void menuItemOneSampleTtest_Click(object sender, EventArgs e)
    {
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

      if (mclsSelected.mDTable != null && mclsSelected.mblIsNumeric) {
        #region Hook Threading Events
        m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_Ttest);
        m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion

        mclsOneSampleTtestPar = new clsOneSampleTtestPar();
        Rdataset = mclsSelected.mstrRdatasetName;

        mclsOneSampleTtestPar.Rdataset = Rdataset;
        mclsOneSampleTtestPar.mstrDatasetName = mclsSelected.mstrDataText;

        frmOneSampleTtestPar mfrmTtestPar;

        int numCols = mclsSelected.mDTable.Columns.Count - 1;
        if (numCols > 0 && mclsSelected.mblIsNumeric) {
          mfrmTtestPar = new frmOneSampleTtestPar(mclsOneSampleTtestPar);
          if (mfrmTtestPar.ShowDialog() == DialogResult.OK) {
            mclsOneSampleTtestPar = mfrmTtestPar.clsTtestPar;

            Add2AnalysisHTable(mclsOneSampleTtestPar, "T_Test");

            m_BackgroundWorker.RunWorkerAsync(mclsOneSampleTtestPar.Rcmd);
            mfrmShowProgress.Message = "Performing One Sample T-test ...";
            mfrmShowProgress.ShowDialog();
          }
        }

        #region Unhook Threading Events
        m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_Ttest);
        m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
            m_BackgroundWorker_ANOVACompleted);
        #endregion
      }
    }

    private void ctxtMnuItemFilter_Click(object sender, EventArgs e)
    {
      DataTable mDTfiltered = new DataTable();
      clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;
      DataGridView currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;

      if (mclsSelected.mDTable != null) {
        string Rdataset = mclsSelected.mstrRdatasetName;
        string rcmd = null;
        string selctedRows = "c(";

        DataGridViewSelectedRowCollection selectedRows = currGrid.SelectedRows;

        foreach (DataGridViewRow row in selectedRows) {
          selctedRows += @"""" + row.Cells[0].Value + @""",";
        }
        selctedRows = selctedRows.Substring(0, selctedRows.Length - 1) + ")";

        frmFilterBasedOnRowIDs mfrmRowFilter = new frmFilterBasedOnRowIDs();
        mfrmRowFilter.PopulateDataComboBox = AvailableDataSources();
        if (mfrmRowFilter.ShowDialog() == DialogResult.OK) {
          string rdataset = ((clsDatasetTreeNode)mhtDatasets[mfrmRowFilter.Dataset]).mstrRdatasetName;

          mintFilterTblNum++;
          string filtTableName = "filteredData" + mintFilterTblNum.ToString();
          rcmd = filtTableName + "<- filterOnRowIds(" + rdataset + "," + selctedRows + ")";
          try {
            rConnector.EvaluateNoReturn(rcmd);
            if (rConnector.GetTableFromRmatrix(filtTableName)) {
              mDTfiltered = rConnector.DataTable.Copy();
              if (mDTfiltered != null) {
                mDTfiltered.TableName = filtTableName;
                mDTfiltered.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTfiltered);
                if (mhtDatasets.Contains("Filtered Data" + mintFilterTblNum.ToString()))
                  AddDataNode((clsDatasetTreeNode)mhtDatasets["Filtered Data" + mintFilterTblNum.ToString()]);
              }
            }
          }
          catch (Exception ex) {
            Console.WriteLine(ex.Message);
          }
        }
      }
    }

    private bool DoAnova(string rcmd)
    {
      DataTable mDTanovaP = new DataTable();
      DataTable mDTanovaX = new DataTable();
      bool mblAnovaAllUsed = false;
      bool success = true;

      try {
        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("pvalues<-anovaR$pvals");
        rConnector.EvaluateNoReturn("notused<-anovaR$miss");
        rConnector.EvaluateNoReturn("allused<-anovaR$allused");
        mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTanovaP = rConnector.DataTable.Copy();
        mDTanovaP.TableName = "pvalues";
        mDTanovaP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTanovaP);
      } else
        success = false;
      if (success) {
        if (!mblAnovaAllUsed) {
          rConnector.GetTableFromRmatrix("notused");
          mDTanovaX = rConnector.DataTable.Copy();
          mDTanovaX.TableName = "notused";
          AddDataset2HashTable(mDTanovaX);
        }
      } else
        success = false;
      return success;
    }

    private bool DoTamuQ(string rcmd)
    {
      DataTable mDTamuQP = new DataTable();
      DataTable mDTamuQYImputed = new DataTable();
      DataTable mDTamuQX = new DataTable();
      /*bool mblAnovaAllUsed = false;*/
      bool success = true;
      //rConnector.EvaluateNoReturn("a <- DoAnova");
      //rConnector.EvaluateNoReturn("a <- DoTamuQ");
      //rConnector.EvaluateNoReturn("pvalues<-1");
      //int a = 1;
      //rConnector.EvaluateNoReturn("pvalues<-DoAnova");

      try {
        //  rConnector.EvaluateNoReturn("pvalues<-DoAnova");

        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("yimputed<-tamuQ$y");
        rConnector.EvaluateNoReturn("pvalues<-tamuQ$pvals");
        //rConnector.EvaluateNoReturn("notused<-TamuQ$miss");
        //rConnector.EvaluateNoReturn("allused<-TamuQ$allused");
        /*mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");*/
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("yimputed")) {
        mDTamuQYImputed = rConnector.DataTable.Copy();
        mDTamuQYImputed.TableName = "yimputed";
        mDTamuQYImputed.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTamuQYImputed);
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTamuQP = rConnector.DataTable.Copy();
        mDTamuQP.TableName = "pvalues";
        mDTamuQP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTamuQP);
      } else
        success = false;
      if (success) {
        /*if (!mblAnovaAllUsed)
        {
            rConnector.GetTableFromRmatrix("notused");
            mDTamuQX = rConnector.DataTable.Copy();
            mDTamuQX.TableName = "notused";
            AddDataset2HashTable(mDTamuQX);
         } */
      } else
        success = false;
      return success;
    }

    private bool DoKWtest(string rcmd)
    {
      DataTable mDTanovaP = new DataTable();
      DataTable mDTanovaX = new DataTable();
      bool mblAnovaAllUsed = false;
      bool success = true;

      try {
        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("pvalues<-kwtest$pvals");
        rConnector.EvaluateNoReturn("notused<-kwtest$miss");
        rConnector.EvaluateNoReturn("allused<-kwtest$allused");
        mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTanovaP = rConnector.DataTable.Copy();
        mDTanovaP.TableName = "pvalues";
        mDTanovaP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTanovaP);
      } else
        success = false;
      if (success) {
        if (!mblAnovaAllUsed) {
          rConnector.GetTableFromRmatrix("notused");
          mDTanovaX = rConnector.DataTable.Copy();
          mDTanovaX.TableName = "notused";
          AddDataset2HashTable(mDTanovaX);
        }
      } else
        success = false;
      return success;
    }

    private bool DoWilcoxtest(string rcmd)
    {
      DataTable mDTanovaP = new DataTable();
      DataTable mDTanovaX = new DataTable();
      bool mblAnovaAllUsed = false;
      bool success = true;

      try {
        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("pvalues<-wilcoxtest$pvals");
        rConnector.EvaluateNoReturn("notused<-wilcoxtest$miss");
        rConnector.EvaluateNoReturn("allused<-wilcoxtest$allused");
        mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTanovaP = rConnector.DataTable.Copy();
        mDTanovaP.TableName = "pvalues";
        mDTanovaP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTanovaP);
      } else
        success = false;
      if (success) {
        if (!mblAnovaAllUsed) {
          rConnector.GetTableFromRmatrix("notused");
          mDTanovaX = rConnector.DataTable.Copy();
          mDTanovaX.TableName = "notused";
          AddDataset2HashTable(mDTanovaX);
        }
      } else
        success = false;
      return success;
    }

    private bool DoShapiroWilkstest(string rcmd)
    {
      DataTable mDTanovaP = new DataTable();
      DataTable mDTanovaX = new DataTable();
      bool mblAnovaAllUsed = false;
      bool success = true;

      try {
        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("pvalues<-swtest$pvals");
        rConnector.EvaluateNoReturn("notused<-swtest$miss");
        rConnector.EvaluateNoReturn("allused<-swtest$allused");
        mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTanovaP = rConnector.DataTable.Copy();
        mDTanovaP.TableName = "pvalues";
        mDTanovaP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTanovaP);
      } else
        success = false;
      if (success) {
        if (!mblAnovaAllUsed) {
          rConnector.GetTableFromRmatrix("notused");
          mDTanovaX = rConnector.DataTable.Copy();
          mDTanovaX.TableName = "notused";
          AddDataset2HashTable(mDTanovaX);
        }
      } else
        success = false;
      return success;
    }

    private bool DoOneSampleTtest(string rcmd)
    {
      DataTable mDTanovaP = new DataTable();
      DataTable mDTanovaX = new DataTable();
      bool mblAnovaAllUsed = false;
      bool success = true;

      try {
        rConnector.EvaluateNoReturn(rcmd);
        rConnector.EvaluateNoReturn("pvalues<-ttest$pvals");
        rConnector.EvaluateNoReturn("notused<-ttest$miss");
        rConnector.EvaluateNoReturn("allused<-ttest$allused");
        mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        success = false;
      }
      if (success && rConnector.GetTableFromRmatrix("pvalues")) {
        mDTanovaP = rConnector.DataTable.Copy();
        mDTanovaP.TableName = "pvalues";
        mDTanovaP.Columns[0].ColumnName = "ID";
        AddDataset2HashTable(mDTanovaP);
      } else
        success = false;
      if (success) {
        if (!mblAnovaAllUsed) {
          rConnector.GetTableFromRmatrix("notused");
          mDTanovaX = rConnector.DataTable.Copy();
          mDTanovaX.TableName = "notused";
          AddDataset2HashTable(mDTanovaX);
        }
      } else
        success = false;
      return success;
    }
    #endregion
  }
}