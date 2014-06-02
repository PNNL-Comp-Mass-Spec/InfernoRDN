using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    private void mnuItemFilterpq_Click(object sender, EventArgs e)
    {
      DataTable mDTfiltered = new DataTable();
      string rdataset = null, thres = null, ltgt = null, rcmd = null;
      string filtTableName = null;
      int column = 1;
      bool mblErr = true, mblNoData = true;
      if (mhtDatasets.ContainsKey("p-Values")) {
        frmFilterAnova mfrmFilterANOVA = new frmFilterAnova();
        mfrmFilterANOVA.PopulateDataComboBox = AvailableDataSources();
        clsDatasetTreeNode mclsAnova = (clsDatasetTreeNode)mhtDatasets["p-Values"];
        mfrmFilterANOVA.PopulateListBox = clsDataTable.DataTableColumns(mclsAnova.mDTable, true);
        if (mfrmFilterANOVA.ShowDialog() == DialogResult.OK) {
          column = mfrmFilterANOVA.SelectedColumn + 1;
          rdataset = ((clsDatasetTreeNode)mhtDatasets[mfrmFilterANOVA.Dataset]).mstrRdatasetName;
          thres = mfrmFilterANOVA.Thres.ToString();
          if (mfrmFilterANOVA.LessThan)
            ltgt = @"smode=""LT""";
          else
            ltgt = @"smode=""GT""";

          rcmd = "filterResult <- filterAnova(pvalues," + rdataset + "," + thres + "," +
                  column.ToString() + "," + ltgt + ")";
          try {
            mintFilterTblNum++;
            filtTableName = "filteredData" + mintFilterTblNum.ToString();
            rConnector.EvaluateNoReturn(rcmd);
            rConnector.EvaluateNoReturn("err<-filterResult$error");
            rConnector.EvaluateNoReturn("nodata<-filterResult$NoData");
            rConnector.EvaluateNoReturn(filtTableName + "<-filterResult$Filtered");
            mblErr = rConnector.GetSymbolAsBool("err");
            mblNoData = rConnector.GetSymbolAsBool("nodata");
          }
          catch (Exception ex) {
            Console.WriteLine(ex.Message);
            mblErr = true;
            mblNoData = true;
          }
          if (mblErr || mblNoData)
            MessageBox.Show("No matches found. Check if you selected the correct dataset or" +
                Environment.NewLine + "if your cutoff is too conservative.",
                "Problem...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          else if (rConnector.GetTableFromRmatrix(filtTableName)) {
            mDTfiltered = rConnector.DataTable.Copy();
            mDTfiltered.TableName = filtTableName;
            mDTfiltered.Columns[0].ColumnName = "ID";
            AddDataset2HashTable(mDTfiltered);
            if (mhtDatasets.Contains("Filtered Data" + mintFilterTblNum.ToString()))
              AddDataNode((clsDatasetTreeNode)mhtDatasets["Filtered Data" + mintFilterTblNum.ToString()]);
          }
        }
      }
    }

    private void mnuItemMissFilt_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        DataTable mDTmissFilt = new DataTable();
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

        if (mhtDatasets.ContainsKey("Expressions") && mclsSelected.mblIsPlottable) {
          string filterCutoff = null;
          string dataset = mclsSelected.mstrRdatasetName;
          string rcmd = null;
          string filtTableName = null;

          frmMissingFilter mfrmMissing = new frmMissingFilter();
          mfrmMissing.DataSetName = mclsSelected.mstrDataText;

          if (mfrmMissing.ShowDialog() == DialogResult.OK) {
            filterCutoff = mfrmMissing.CutOff;
            mintFilterTblNum++;
            filtTableName = "filteredData" + mintFilterTblNum.ToString();
            rcmd = filtTableName + "<- filterMissing(" + dataset + "," + filterCutoff + ")";
            try {
              rConnector.EvaluateNoReturn(rcmd);
              if (rConnector.GetTableFromRmatrix(filtTableName)) {
                mDTmissFilt = rConnector.DataTable.Copy();
                if (mDTmissFilt != null) {
                  mDTmissFilt.TableName = filtTableName;
                  AddDataset2HashTable(mDTmissFilt);
                  AddDataNode((clsDatasetTreeNode)mhtDatasets["Filtered Data" + mintFilterTblNum.ToString()]);
                }
              }
            }
            catch (Exception ex) {
              MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }

          }
        }
      }
    }

    private void mnuItemMergeCols_Click(object sender, EventArgs e)
    {
      if (ctltreeView.SelectedNode.Tag != null) {
        clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;
        string dataset = mclsSelected.mstrRdatasetName;

        if (mhtDatasets.ContainsKey("Expressions") && mhtDatasets.ContainsKey("Factors") &&
            mclsSelected.mblIsNumeric) {
          #region Hook Threading events
          m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_MergeC);
          m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
              m_BackgroundWorker_MergeCCompleted);
          #endregion

          string pmode = "pmode='sum'", factor = null;
          string rcmd = "";

          frmMergeColsPar mfrmMergeC = new frmMergeColsPar();
          clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
          mfrmMergeC.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
          mfrmMergeC.DataSetName = mclsSelected.mstrDataText;

          if (mfrmMergeC.ShowDialog() == DialogResult.OK) {
            pmode = mfrmMergeC.pMode;
            factor = mfrmMergeC.SelectedFactor;

            rcmd = "mergedData <- mergeColumns(" + dataset + "," + factor + ",";
            rcmd += pmode + ")";
            m_BackgroundWorker.RunWorkerAsync(rcmd);
            mfrmShowProgress.Message = "Merging Columns ...";
            mfrmShowProgress.ShowDialog();
          }

          #region Unhook Threading events
          m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_MergeC);
          m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
              m_BackgroundWorker_MergeCCompleted);
          #endregion
        } else
          MessageBox.Show("Check if you have selected a valid data table and defined factors.",
              "Not valid..", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void mnuArrangeColumns_Click(object sender, EventArgs e)
    {
      ArrayList newOrder = new ArrayList();

      if (mhtDatasets.ContainsKey("Expressions")) {
        frmArrangeColumns mfrmArrCols = new frmArrangeColumns();
        mfrmArrCols.DatasetInfo = marrDatasetInfo;
        if (mfrmArrCols.ShowDialog() == DialogResult.OK) {
          newOrder = mfrmArrCols.NewDatasetOrder;
        }
      }
    }

    private void mnuAnalysisSummary_Click(object sender, EventArgs e)
    {
      frmAnalysisSummary frmSummary = new frmAnalysisSummary();
      frmSummary.SummaryHashTable = mhtAnalysisObjects;
      frmSummary.SummaryArrayList = marrAnalysisObjects;
      frmSummary.DataFileName = this.mstrLoadedfileName;
      frmSummary.ShowDialog();
    }
  }
}

