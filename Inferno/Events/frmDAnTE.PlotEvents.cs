using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    #region Threading events for plots --------------------------

    void m_BackgroundWorker_QQPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmQQplotDisplay mfrmQQplotDisplay = new frmQQplotDisplay(mclsQQPar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        // Note that due to a race condition in 
        // the DoWork event handler, the Cancelled
        // flag may not have been set, even though
        // CancelAsync was called.
        Console.WriteLine("Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        //mfrmPlot.Image = e.Result as Image;
        //DialogResult dres = mfrmPlot.ShowDialog();
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmQQplotDisplay.Image = mclsPlotResult.mImage;
        mfrmQQplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmQQplotDisplay.DAnTEinstance = this;
        mfrmQQplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmQQplotDisplay.Title = "Q-Q Plot";
        mfrmQQplotDisplay.Show();
      }
    }

    void m_BackgroundWorker_HistPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmHistDisplay mfrmHistDisplay = new frmHistDisplay(mclsHistPar);
      //mfrmShowProgress.Hide();
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;
      //this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        // Note that due to a race condition in 
        // the DoWork event handler, the Cancelled
        // flag may not have been set, even though
        // CancelAsync was called.
        Console.WriteLine("Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        //mfrmPlot.Image = e.Result as Image;
        //DialogResult dres = mfrmPlot.ShowDialog();
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmHistDisplay.Image = mclsPlotResult.mImage;
        mfrmHistDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmHistDisplay.DAnTEinstance = this;
        mfrmHistDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmHistDisplay.Title = "Histograms";
        mfrmHistDisplay.Show();
      }
    }

    void m_BackgroundWorker_CorrPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmCorrDisplay mfrmCorrDisplay = new frmCorrDisplay(mclsCorrPar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;
      //this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        // Note that due to a race condition in 
        // the DoWork event handler, the Cancelled
        // flag may not have been set, even though
        // CancelAsync was called.
        Console.WriteLine("Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        //mfrmPlot.Image = e.Result as Image;
        //DialogResult dres = mfrmPlot.ShowDialog();
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmCorrDisplay.Image = mclsPlotResult.mImage;
        mfrmCorrDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmCorrDisplay.DAnTEinstance = this;
        mfrmCorrDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmCorrDisplay.Title = "Correlations";
        mfrmCorrDisplay.Show();
      }
    }

    void m_BackgroundWorker_BoxPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmBoxPlotDisplay mfrmBoxPlotDisplay = new frmBoxPlotDisplay(mclsBoxPlotPar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;

      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        Console.WriteLine("Canceled");
      } else {
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmBoxPlotDisplay.Image = mclsPlotResult.mImage;
        mfrmBoxPlotDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmBoxPlotDisplay.DAnTEinstance = this;
        mfrmBoxPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmBoxPlotDisplay.Title = "Box Plots";
        mfrmBoxPlotDisplay.Show();
      }
    }

    void m_BackgroundWorker_MAplotCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmMAplotDisplay mfrmMAplotDisplay = new frmMAplotDisplay(mclsMApar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;

      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        Console.WriteLine("Canceled");
      } else {
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmMAplotDisplay.Image = mclsPlotResult.mImage;
        mfrmMAplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmMAplotDisplay.DAnTEinstance = this;
        mfrmMAplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmMAplotDisplay.Title = "MA Plots";
        mfrmMAplotDisplay.Show();
      }
    }

    void m_BackgroundWorker_VennCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      clsPlotResult mclsPlotResult;
      frmVennDisplay mfrmVennDisplay = new frmVennDisplay(mclsVennPar);
      mfrmShowProgress.Close();
      mfrmShowProgress.DialogResult = DialogResult.Cancel;
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        // Note that due to a race condition in 
        // the DoWork event handler, the Cancelled
        // flag may not have been set, even though
        // CancelAsync was called.
        Console.WriteLine("Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
        //mfrmPlot.Image = e.Result as Image;
        //DialogResult dres = mfrmPlot.ShowDialog();
        mclsPlotResult = (clsPlotResult)e.Result;
        mfrmVennDisplay.Image = mclsPlotResult.mImage;
        mfrmVennDisplay.PlotName = mclsPlotResult.mstrPlotName;
        mfrmVennDisplay.DAnTEinstance = this;
        mfrmVennDisplay.MdiParent = m_frmDAnTE.MdiParent;
        mfrmVennDisplay.Title = "Venn Diagram";
        mfrmVennDisplay.Show();
      }
    }


    void m_BackgroundWorker_GeneratePlots(object sender, DoWorkEventArgs e)
    {
      clsRplotData arg = (clsRplotData)e.Argument;
      string rcmd = arg.mstrRcmd;
      string plotname = arg.mstrPlotName;
      clsPlotResult mclsPlotResult;

      try {

        mRConnector.EvaluateNoReturn(rcmd);
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

    #endregion
  }

  #region Class for Plotting results
  class clsPlotResult
  {
    public Image mImage;
    public string mstrPlotName;

    public clsPlotResult()
    {
      mImage = null;
      mstrPlotName = null;
    }

    public clsPlotResult(Image image, string plot)
    {
      mImage = image;
      mstrPlotName = plot;
    }
  }

  class clsRplotData
  {
    public string mstrRcmd;
    public string mstrPlotName;

    public clsRplotData()
    {
      mstrPlotName = null;
      mstrRcmd = null;
    }

    public clsRplotData(string rcmd, string plotname)
    {
      mstrRcmd = rcmd;
      mstrPlotName = plotname;
    }
  }
  #endregion
}
