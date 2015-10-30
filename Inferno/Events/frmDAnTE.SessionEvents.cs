using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading Events for Save/Open session

        void m_BackgroundWorker_SessionOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var cancelled = e.Cancelled;
            var success = (bool)e.Result;

            var errorMessage = string.Empty;

            if (e.Error != null)
                errorMessage = e.Error.Message;

            SessionFileOpenFinalize(success, cancelled, errorMessage);
        }

        void m_BackgroundWorker_OpenSession(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = OpenSession((string)e.Argument);
            }
            catch (Exception ex)
            {
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
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Cancelled");
            }
            else
            {
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

            try
            {

                mRConnector.EvaluateNoReturn(arg);
                e.Result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                e.Result = false;
                e.Cancel = true;
            }
        }

        #endregion
    }
}
