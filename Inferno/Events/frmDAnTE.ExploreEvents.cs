using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for Explore

        void m_BackgroundWorker_PCAPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var pcaPlotDisplay = new frmPCAPlotDisplay(mclsPCApar);
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
                var pcaPlotResult = (clsPlotResult)e.Result;
                pcaPlotDisplay.Image = pcaPlotResult.mImage;
                pcaPlotDisplay.PlotName = pcaPlotResult.mstrPlotName;
                pcaPlotDisplay.DAnTEinstance = this;
                pcaPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                pcaPlotDisplay.Title = "PCA/PLS Plot";
                pcaPlotDisplay.Show();
                try
                {
                    mRConnector.EvaluateNoReturn("Mode <- weights$Mode");
                    var pcaModeList = mRConnector.GetSymbolAsStrings("Mode");
                    var pcaMode = pcaModeList[0];

                    if (pcaMode.Equals("PCA"))
                    {
                        mRConnector.EvaluateNoReturn("PCAweights <- weights$X");
                        if (mRConnector.GetTableFromRmatrix("PCAweights"))
                        {
                            var pcaLoadsTable = mRConnector.DataTable.Copy();
                            pcaLoadsTable.TableName = "PCAweights";
                            pcaLoadsTable.Columns[0].ColumnName = "ID";
                            mRConnector.EvaluateNoReturn("cat(\"PCA calculated.\n\")");
                            AddDataset2HashTable(pcaLoadsTable);
                            if (mhtDatasets.ContainsKey("PCA Weights"))
                                AddDataNode(mhtDatasets["PCA Weights"]);
                        }
                    }
                    if (pcaMode.Equals("PLS"))
                    {
                        mRConnector.EvaluateNoReturn("PLSweights <- weights$X");
                        if (mRConnector.GetTableFromRmatrix("PLSweights"))
                        {
                            var plsLoadsTable = mRConnector.DataTable.Copy();
                            plsLoadsTable.TableName = "PLSweights";
                            plsLoadsTable.Columns[0].ColumnName = "ID";
                            mRConnector.EvaluateNoReturn("cat(\"PLS calculated.\n\")");
                            AddDataset2HashTable(plsLoadsTable);
                            if (mhtDatasets.ContainsKey("PLS Weights"))
                                AddDataNode(mhtDatasets["PLS Weights"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        void m_BackgroundWorker_HeatMapCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var heatmapDisplay = new frmHeatmapDisplay(mclsHeatmapPar);
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
                var heatmapResult = (clsPlotResult)e.Result;
                heatmapDisplay.Image = heatmapResult.mImage;
                heatmapDisplay.PlotName = heatmapResult.mstrPlotName;
                heatmapDisplay.DAnTEinstance = this;
                heatmapDisplay.MdiParent = m_frmDAnTE.MdiParent;
                heatmapDisplay.Title = "Heatmap";
                heatmapDisplay.Show();
                if (doClust)
                {
                    if (mhtDatasets.ContainsKey("Heatmap Clusters"))
                        AddDataNode(mhtDatasets["Heatmap Clusters"]);
                }
            }
        }

        void m_BackgroundWorker_GenerateHeatmap(object sender, DoWorkEventArgs e)
        {
            var arg = (clsRplotData)e.Argument;
            var rcmd = arg.mstrRcmd;
            var plotname = arg.mstrPlotName;

            try
            {

                mRConnector.EvaluateNoReturn(rcmd);
                if (doClust)
                    if (mRConnector.GetTableFromRvector("clusterResults"))
                    {
                        var clusterResultTable = mRConnector.DataTable.Copy();
                        clusterResultTable.TableName = "clusterResults";
                        AddDataset2HashTable(clusterResultTable);
                    }
                mRConnector.EvaluateNoReturn("cat(\"Heatmap done.\n\")");
                var heatmapResult = new clsPlotResult(LoadImage(mRTempFilePath), plotname);
                e.Result = heatmapResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                e.Result = null;
                e.Cancel = true;
                DeleteTempFile(mRTempFilePath);
            }
        }

        void m_BackgroundWorker_PatternSearchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Hide();
            Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Pattern Search Cancelled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.ContainsKey("Pattern Corr"))
                        AddDataNode(mhtDatasets["Pattern Corr"]);
                }
                else
                    MessageBox.Show("Pattern Search failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_SearchPatterns(object sender, DoWorkEventArgs e)
        {
            e.Result = SearchPatterns();
        }

        #endregion
    }
}
