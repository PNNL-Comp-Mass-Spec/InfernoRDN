using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DAnTE.Inferno.Plotting;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for plots --------------------------

        void m_BackgroundWorker_QQPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var qqPlotOptions = new frmQQplotDisplay(mclsQQPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

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
                var plotResult = (clsPlotResult)e.Result;
                qqPlotOptions.Image = plotResult.mImage;
                qqPlotOptions.PlotName = plotResult.mstrPlotName;
                qqPlotOptions.DAnTEinstance = this;
                qqPlotOptions.MdiParent = m_frmDAnTE.MdiParent;
                qqPlotOptions.Title = "Q-Q Plot";
                qqPlotOptions.Show();
            }
        }

        void m_BackgroundWorker_HistPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var histogramPlotOptions = new frmHistDisplay(mclsHistPar);

            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

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
                var plotResult = (clsPlotResult)e.Result;
                histogramPlotOptions.Image = plotResult.mImage;
                histogramPlotOptions.PlotName = plotResult.mstrPlotName;
                histogramPlotOptions.DAnTEinstance = this;
                histogramPlotOptions.MdiParent = m_frmDAnTE.MdiParent;
                histogramPlotOptions.Title = "Histograms";
                histogramPlotOptions.Show();
            }
        }

        void m_BackgroundWorker_CorrPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var correlationPlotOptions = new frmCorrDisplay(mclsCorrPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

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
                var plotResult = (clsPlotResult)e.Result;
                correlationPlotOptions.Image = plotResult.mImage;
                correlationPlotOptions.PlotName = plotResult.mstrPlotName;
                correlationPlotOptions.DAnTEinstance = this;
                correlationPlotOptions.MdiParent = m_frmDAnTE.MdiParent;
                correlationPlotOptions.Title = "Correlations";
                correlationPlotOptions.Show();
            }
        }

        void m_BackgroundWorker_BoxPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var boxPlotOptions = new frmBoxPlotDisplay(mclsBoxPlotPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Cancelled");
            }
            else
            {
                var plotResult = (clsPlotResult)e.Result;
                boxPlotOptions.Image = plotResult.mImage;
                boxPlotOptions.PlotName = plotResult.mstrPlotName;
                boxPlotOptions.DAnTEinstance = this;
                boxPlotOptions.MdiParent = m_frmDAnTE.MdiParent;
                boxPlotOptions.Title = "Box Plots";
                boxPlotOptions.Show();
            }
        }

        void m_BackgroundWorker_MAplotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var maPlotOptions = new frmMAplotDisplay(mclsMApar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Cancelled");
            }
            else
            {
                var plotResult = (clsPlotResult)e.Result;
                maPlotOptions.Image = plotResult.mImage;
                maPlotOptions.PlotName = plotResult.mstrPlotName;
                maPlotOptions.DAnTEinstance = this;
                maPlotOptions.MdiParent = m_frmDAnTE.MdiParent;
                maPlotOptions.Title = "MA Plots";
                maPlotOptions.Show();
            }
        }

        void m_BackgroundWorker_VennCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var vennDiagramsOptions = new frmVennDisplay(mclsVennPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

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
                var plotResult = (clsPlotResult)e.Result;
                vennDiagramsOptions.Image = plotResult.mImage;
                vennDiagramsOptions.PlotName = plotResult.mstrPlotName;
                vennDiagramsOptions.DAnTEinstance = this;
                vennDiagramsOptions.MdiParent = m_frmDAnTE.MdiParent;
                vennDiagramsOptions.Title = "Venn Diagram";
                vennDiagramsOptions.Show();
            }
        }


        void m_BackgroundWorker_GeneratePlots(object sender, DoWorkEventArgs e)
        {
            var arg = (clsRplotData)e.Argument;
            var rcmd = arg.mstrRcmd;
            var plotname = arg.mstrPlotName;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                var plotResult = new clsPlotResult(LoadImage(mRTempFilePath), plotname);
                e.Result = plotResult;
            }
            catch (Exception ex)
            {
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
        public readonly Image mImage;
        public readonly string mstrPlotName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="image"></param>
        /// <param name="plot"></param>
        public clsPlotResult(Image image, string plot)
        {
            mImage = image;
            mstrPlotName = plot;
        }
    }

    class clsRplotData
    {
        public readonly string mstrRcmd;
        public readonly string mstrPlotName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rcmd"></param>
        /// <param name="plotname"></param>
        public clsRplotData(string rcmd, string plotname)
        {
            mstrRcmd = rcmd;
            mstrPlotName = plotname;
        }
    }

    #endregion
}