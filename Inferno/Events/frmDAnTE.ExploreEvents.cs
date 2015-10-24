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
            var mfrmPCAPlotDisplay = new frmPCAPlotDisplay(mclsPCApar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                var mclsPlotResult = (clsPlotResult)e.Result;
                mfrmPCAPlotDisplay.Image = mclsPlotResult.mImage;
                mfrmPCAPlotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmPCAPlotDisplay.DAnTEinstance = this;
                mfrmPCAPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmPCAPlotDisplay.Title = "PCA/PLS Plot";
                mfrmPCAPlotDisplay.Show();
                try
                {
                    mRConnector.EvaluateNoReturn("Mode <- weights$Mode");
                    var pcmode = mRConnector.GetSymbolAsStrings("Mode");
                    var mstrType = pcmode[0];

                    if (mstrType.Equals("PCA"))
                    {
                        mRConnector.EvaluateNoReturn("PCAweights <- weights$X");
                        if (mRConnector.GetTableFromRmatrix("PCAweights"))
                        {
                            var mDTpcaLoads = mRConnector.DataTable.Copy();
                            mDTpcaLoads.TableName = "PCAweights";
                            mDTpcaLoads.Columns[0].ColumnName = "ID";
                            mRConnector.EvaluateNoReturn("cat(\"PCA calculated.\n\")");
                            AddDataset2HashTable(mDTpcaLoads);
                            if (mhtDatasets.ContainsKey("PCA Weights"))
                                AddDataNode(mhtDatasets["PCA Weights"]);
                        }
                    }
                    if (mstrType.Equals("PLS"))
                    {
                        mRConnector.EvaluateNoReturn("PLSweights <- weights$X");
                        if (mRConnector.GetTableFromRmatrix("PLSweights"))
                        {
                            var mDTplsLoads = mRConnector.DataTable.Copy();
                            mDTplsLoads.TableName = "PLSweights";
                            mDTplsLoads.Columns[0].ColumnName = "ID";
                            mRConnector.EvaluateNoReturn("cat(\"PLS calculated.\n\")");
                            AddDataset2HashTable(mDTplsLoads);
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
            var mfrmHmapDisplay = new frmHeatmapDisplay(mclsHeatmapPar);
            mfrmShowProgress.Hide();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                var mclsPlotResult = (clsPlotResult)e.Result;
                mfrmHmapDisplay.Image = mclsPlotResult.mImage;
                mfrmHmapDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmHmapDisplay.DAnTEinstance = this;
                mfrmHmapDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmHmapDisplay.Title = "Heatmap";
                mfrmHmapDisplay.Show();
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
                        var mDTclusters = mRConnector.DataTable.Copy();
                        mDTclusters.TableName = "clusterResults";
                        AddDataset2HashTable(mDTclusters);
                    }
                mRConnector.EvaluateNoReturn("cat(\"Heatmap done.\n\")");
                var mclsPlotResult = new clsPlotResult(LoadImage(mRTempFilePath), plotname);
                e.Result = mclsPlotResult;
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
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Pattern Search Canceled");
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
            e.Result = SearchPatterns((string)e.Argument);
        }

        #endregion
    }
}
