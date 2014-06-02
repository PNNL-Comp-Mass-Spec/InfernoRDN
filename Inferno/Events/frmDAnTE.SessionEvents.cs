using System;
using System.Collections;
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
      mfrmShowProgress.Close();
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
        if ((bool)e.Result) {
          ctltreeView.Nodes[0].Nodes.Clear();

          IDictionaryEnumerator _enumerator = mhtDatasets.GetEnumerator();
          while (_enumerator.MoveNext()) {
            AddDataNode((clsDatasetTreeNode)_enumerator.Value);

          }

          statusBarPanelMsg.Text = "Session opened successfully.";
          if (mstrLoadedfileName != null)
            this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
        } else
          MessageBox.Show("Error in loading session.", "Error", MessageBoxButtons.OK,
          MessageBoxIcon.Error);
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
      mfrmShowProgress.Close();
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
      string arg = (string)e.Argument;

      try {

        rConnector.EvaluateNoReturn(arg);
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
