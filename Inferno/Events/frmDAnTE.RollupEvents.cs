using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using DAnTE.Tools;
using DAnTE.ExtraControls;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for Rollup

        void m_BackgroundWorker_RRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Ref. Scaling Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("RRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["RRollup"]);
                    if (mhtDatasets.Contains("ScaledData"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["ScaledData"]);
                    if (mhtDatasets.Contains("OutliersRemoved"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["OutliersRemoved"]);
                }
                else
                    MessageBox.Show("Ref. Scaling/Rolling up failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_RRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoRRollup((string)e.Argument);
        }


        void m_BackgroundWorker_ZRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Scaling Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("ZRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["ZRollup"]);
                }
                else
                    MessageBox.Show("Scaling/Rolling up failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_ZRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoZRollup((string)e.Argument);
        }

        void m_BackgroundWorker_QRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("QRollup Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("QRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["QRollup"]);
                }
                else
                    MessageBox.Show("QRollup failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_QRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoQRollup((string)e.Argument);
        }

        #endregion
    }
}
