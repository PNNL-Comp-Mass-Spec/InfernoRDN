using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    #region Threading events for Pre-Process --------------------------

    void m_BackgroundWorker_Log2Completed(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Log2 Operation Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Log Expressions"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Log Expressions"]);
        } else
          MessageBox.Show("Unknown error in data. Check if your file matches the format required",
              "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_Log2(object sender, DoWorkEventArgs e)
    {
      DataTable mDTLogEset1 = new DataTable();

      string rcmd = (string)e.Argument;
      try {
        mRConnector.EvaluateNoReturn(rcmd);
        if (mRConnector.GetTableFromRmatrix("logEset")) {
          mDTLogEset1 = mRConnector.DataTable.Copy();
          mDTLogEset1.TableName = "logEset";
          mRConnector.EvaluateNoReturn("cat(\"Log Expressions calculated.\n\")");
          //--------------------------------------
          AddDataset2HashTable(mDTLogEset1);
          e.Result = true;
        } else
          e.Result = false;
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        e.Result = false;
        e.Cancel = true;
      }
    }

    void m_BackgroundWorker_LowessCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Loess Normalization Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("LOESS Data"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["LOESS Data"]);
        } else
          MessageBox.Show("Loess failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_Lowess(object sender, DoWorkEventArgs e)
    {
      e.Result = DoLowess((string)e.Argument);
    }

    void m_BackgroundWorker_LinRegCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Linear Regression Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Linear Regressed"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Linear Regressed"]);
        } else
          MessageBox.Show("Linear Regression failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_LinReg(object sender, DoWorkEventArgs e)
    {
      e.Result = DoLinReg((string)e.Argument);
    }

    void m_BackgroundWorker_MeanCCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Mean/Median Centering Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Mean Centered"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Mean Centered"]);
          if (mhtDatasets.Contains("Median Centered"))
              AddDataNode((clsDatasetTreeNode)mhtDatasets["Median Centered"]);
        } else
          MessageBox.Show("Mean/Median Centering failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_MeanC(object sender, DoWorkEventArgs e)
    {
      e.Result = DoMeanCenter((string)e.Argument);
    }

    void m_BackgroundWorker_QuantileCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Quantile Normalization Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Quantile Normalized"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Quantile Normalized"]);
        } else
          MessageBox.Show("Quantile Normalization failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_Quantile(object sender, DoWorkEventArgs e)
    {
      e.Result = DoQuantileN((string)e.Argument);
    }

    void m_BackgroundWorker_MADCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("MAD Adjustment Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("MAD Adjusted"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["MAD Adjusted"]);
        } else
          MessageBox.Show("MAD Adjustment failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_MAD(object sender, DoWorkEventArgs e)
    {
      e.Result = DoMADAdjustment((string)e.Argument);
    }

    void m_BackgroundWorker_ImputeCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Imputation Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Imputed Data"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Imputed Data"]);
        } else
          MessageBox.Show("Imputation failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_Impute(object sender, DoWorkEventArgs e)
    {
      e.Result = DoImpute((string)e.Argument);
    }

    void m_BackgroundWorker_MergeCCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Close();
      this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Imputation Canceled", "Error!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        if ((bool)e.Result) {
          if (mhtDatasets.Contains("Merged Data"))
            AddDataNode((clsDatasetTreeNode)mhtDatasets["Merged Data"]);
        } else
          MessageBox.Show("Imputation failed." + Environment.NewLine +
              "Check if you have all data requirements and in correct format.", "Error!",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    void m_BackgroundWorker_MergeC(object sender, DoWorkEventArgs e)
    {
      e.Result = DoMergeColumns((string)e.Argument);
    }
    #endregion
  }
}
