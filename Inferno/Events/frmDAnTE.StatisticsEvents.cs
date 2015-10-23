using System;
using System.ComponentModel;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for Statistics

        void m_BackgroundWorker_ANOVACompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Hide();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("ANOVA Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("p-Values"))
                        AddDataNode(mhtDatasets["p-Values"]);
                    if (mhtDatasets.ContainsKey("Unused Data"))
                        AddDataNode(mhtDatasets["Unused Data"]);
                }
                else
                    MessageBox.Show("ANOVA failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.  Also consider using a smaller value for 'Minimum Number of Data Points per Factor Level'", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_ANOVA(object sender, DoWorkEventArgs e)
        {
            e.Result = DoAnova((string)e.Argument);
        }

        void m_BackgroundWorker_KW(object sender, DoWorkEventArgs e)
        {
            e.Result = DoKWtest((string)e.Argument);
        }

        void m_BackgroundWorker_Wilcox(object sender, DoWorkEventArgs e)
        {
            e.Result = DoWilcoxtest((string)e.Argument);
        }

        void m_BackgroundWorker_ShapiroWilks(object sender, DoWorkEventArgs e)
        {
            e.Result = DoShapiroWilkstest((string)e.Argument);
        }

        void m_BackgroundWorker_Ttest(object sender, DoWorkEventArgs e)
        {
            e.Result = DoOneSampleTtest((string)e.Argument);
        }

	void m_BackgroundWorker_TamuQPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
	    var mfrmTamuQplotDisplay = new frmQQplotDisplay(mclsQQPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                var mclsPlotResult = (clsPlotResult)e.Result;
                mfrmTamuQplotDisplay.Image = mclsPlotResult.mImage;
                mfrmTamuQplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmTamuQplotDisplay.DAnTEinstance = this;
                mfrmTamuQplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmTamuQplotDisplay.Title = "Tamu-Q Plot";
                mfrmTamuQplotDisplay.Show();
            }
        }

	void m_BackgroundWorker_TamuQCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Hide();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("TamuQ Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("p-Values"))
                        AddDataNode(mhtDatasets["p-Values"]);
                    if (mhtDatasets.ContainsKey("Imputed Values"))
                        AddDataNode(mhtDatasets["Imputed Values"]);
                    if (mhtDatasets.ContainsKey("Unused Data"))
                        AddDataNode(mhtDatasets["Unused Data"]);
                }
                else
                    MessageBox.Show("TamuQ failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

	 void m_BackgroundWorker_TamuQ(object sender, DoWorkEventArgs e)
        {
            e.Result = DoTamuQ((string)e.Argument);
        }
        #endregion
    }
}
