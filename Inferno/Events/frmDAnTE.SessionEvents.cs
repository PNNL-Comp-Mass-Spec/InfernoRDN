using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
  partial class frmDAnTE
  {
    #region Threading Events for Save/Open session

    void m_BackgroundWorker_SessionOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Hide();
      //this.Focus();
      if (e.Error != null) {
        MessageBox.Show(e.Error.Message);
      } else if (e.Cancelled) {
        // Next, handle the case where the user canceled 
        // the operation.
        Console.WriteLine("Canceled");
      } else {
        // Finally, handle the case where the operation 
        // succeeded.
          if ((bool)e.Result)
          {
              ctltreeView.Nodes[0].Nodes.Clear();

              foreach (var dataset in mhtDatasets)
              {
                  AddDataNode(dataset.Value);
              }
             

              statusBarPanelMsg.Text = "Session opened successfully.";
              if (string.IsNullOrEmpty(mstrLoadedfileName))
                  this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
          }
          else
          {
              var errorMessage = "Error loading session file";

              if (string.Equals(LastSessionLoadError, "Value cannot be null."))
                  errorMessage += ". Try loading the file again -- in many cases the first load attempt fails, but the second load attempt succeeds.";

              MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }
    }

    void m_BackgroundWorker_OpenSession(object sender, DoWorkEventArgs e)
    {
      try {
        e.Result = OpenSession((string)e.Argument);
      }
      catch (Exception ex) {
        MessageBox.Show("File open failed: " + ex.Message, "Error!");
        e.Result = false;
        e.Cancel = true;
      }
    }

    void m_BackgroundWorker_SaveSessionCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      mfrmShowProgress.Hide();
      mfrmShowProgress.Hide();
      //mfrmShowProgress.DialogResult = DialogResult.Cancel;
      this.Focus();
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
        statusBarPanelMsg.Text = "Session saved successfully.";
      }
    }

    void m_BackgroundWorker_SaveSession(object sender, DoWorkEventArgs e)
    {
      var arg = (string)e.Argument;

      try {

        mRConnector.EvaluateNoReturn(arg);
        e.Result = true;
      }
      catch (Exception ex) {
        MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
        e.Result = false;
        e.Cancel = true;
      }
    }

    #endregion
  }
}
