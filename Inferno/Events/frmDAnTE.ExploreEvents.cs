using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    #region Threading events for Explore

    void m_BackgroundWorker_PCAPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      DataTable mDTpcaLoads = new DataTable();
      DataTable mDTplsLoads = new DataTable();
      string mstrType = "PCA";
      clsPlotResult mclsPlotResult;
      frmPCAPlotDisplay mfrmPCAPlotDisplay = new frmPCAPlotDisplay(mclsPCApar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        Console.WriteLine("Canceled");
      } else {
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmPCAPlotDisplay.Image = mclsPlotResult.mImage;
        mfrmPCAPlotDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmPCAPlotDisplay.DAnTEinstance = this;
        mfrmPCAPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmPCAPlotDisplay.Title = "PCA/PLS Plot";
        mfrmPCAPlotDisplay.Show();
        try {
          mRConnector.EvaluateNoReturn("Mode <- weights$Mode");
          var pcmode = mRConnector.GetSymbolAsStrings("Mode");
          mstrType = pcmode[0];

          if (mstrType.Equals("PCA")) {
            mRConnector.EvaluateNoReturn("PCAweights <- weights$X");
            if (mRConnector.GetTableFromRmatrix("PCAweights")) {
              mDTpcaLoads = mRConnector.DataTable.Copy();
              mDTpcaLoads.TableName = "PCAweights";
              mDTpcaLoads.Columns[0].ColumnName = "ID";
              mRConnector.EvaluateNoReturn("cat(\"PCA calculated.\n\")");
              AddDataset2HashTable(mDTpcaLoads);
              if (mhtDatasets.Contains("PCA Weights"))
                AddDataNode((clsDatasetTreeNode)mhtDatasets["PCA Weights"]);
            }
          }
          if (mstrType.Equals("PLS")) {
            mRConnector.EvaluateNoReturn("PLSweights <- weights$X");
            if (mRConnector.GetTableFromRmatrix("PLSweights")) {
              mDTplsLoads = mRConnector.DataTable.Copy();
              mDTplsLoads.TableName = "PLSweights";
              mDTplsLoads.Columns[0].ColumnName = "ID";
              mRConnector.EvaluateNoReturn("cat(\"PLS calculated.\n\")");
              AddDataset2HashTable(mDTplsLoads);
              if (mhtDatasets.Contains("PLS Weights"))
                AddDataNode((clsDatasetTreeNode)mhtDatasets["PLS Weights"]);
            }
          }
        }
        catch (Exception ex) {
          Console.WriteLine(ex.Message);
        }
      }
    }

    void m_BackgroundWorker_HeatMapCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmHeatmapDisplay mfrmHmapDisplay = new frmHeatmapDisplay(mclsHeatmapPar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;

      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        Console.WriteLine("Canceled");
      } else {
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmHmapDisplay.Image = mclsPlotResult.mImage;
        mfrmHmapDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmHmapDisplay.DAnTEinstance = this;
        mfrmHmapDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmHmapDisplay.Title = "Heatmap";
        mfrmHmapDisplay.Show();
        if (doClust) {
          if (mhtDatasets.Contains("Heatmap Clusters"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Heatmap Clusters"]);
        }
      }
    }

    void m_BackgroundWorker_GenerateHeatmap(object sender, DoWorkEventArgs e)
    {
      DataTable mDTclusters = new DataTable();
      clsRplotData arg = (clsRplotData)e.Argument;
      string rcmd = arg.mstrRcmd;
      string plotname = arg.mstrPlotName;
      clsPlotResult mclsPlotResult;

      try {

        mRConnector.EvaluateNoReturn(rcmd);
        if (doClust)
          if (mRConnector.GetTableFromRvector("clusterResults")) {
            mDTclusters = mRConnector.DataTable.Copy();
            mDTclusters.TableName = "clusterResults";
            AddDataset2HashTable(mDTclusters);
          }
        mRConnector.EvaluateNoReturn("cat(\"Heatmap done.\n\")");
        mclsPlotResult = new clsPlotResult(LoadImage(mRTempFilePath), plotname);
        e.Result = mclsPlotResult;
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        e.Result = null;
        e.Cancel = true;
        DeleteTempFile(mRTempFilePath);
      }
    }

    void m_BackgroundWorker_PatternSearchCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Pattern Search Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Pattern Corr"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Pattern Corr"]);
        } else
          MessageBox.Show("Pattern Search failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_SearchPatterns(object sender, DoWorkEventArgs e)
    {
      e.Result = SearchPatterns((string)e.Argument);
    }

    #endregion
  }
}
