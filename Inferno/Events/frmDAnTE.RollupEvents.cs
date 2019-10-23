using System;
using System.ComponentModel;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for Rollup

        void m_BackgroundWorker_RRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mProgressForm.Hide();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Ref. Scaling Operation Cancelled", "Error!", MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("RRollup"))
                        AddDataNode(mhtDatasets["RRollup"]);
                    if (mhtDatasets.ContainsKey("ScaledData"))
                        AddDataNode(mhtDatasets["ScaledData"]);
                    if (mhtDatasets.ContainsKey("OutliersRemoved"))
                        AddDataNode(mhtDatasets["OutliersRemoved"]);
                }
                else
                    MessageBox.Show("Ref. Scaling/Rolling up failed." + Environment.NewLine +
                                    "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_RRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoRRollup((string)e.Argument);
        }


        void m_BackgroundWorker_ZRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mProgressForm.Hide();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Scaling Operation Cancelled", "Error!", MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("ZRollup"))
                        AddDataNode(mhtDatasets["ZRollup"]);
                }
                else
                    MessageBox.Show("Scaling/Rolling up failed." + Environment.NewLine +
                                    "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_ZRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoZRollup((string)e.Argument);
        }

        void m_BackgroundWorker_QRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mProgressForm.Hide();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("QRollup Operation Cancelled", "Error!", MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("QRollup"))
                        AddDataNode(mhtDatasets["QRollup"]);
                }
                else
                    MessageBox.Show("QRollup failed." + Environment.NewLine +
                                    "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_QRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoQRollup((string)e.Argument);
        }

        #endregion
    }
}