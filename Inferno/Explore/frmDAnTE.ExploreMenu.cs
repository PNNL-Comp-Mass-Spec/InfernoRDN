using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        clsPCAplotPar mPCAOptions;
        clsHeatmapPar mHeatmapOptions;
        clsPatternSearchPar mclsPatternPar;

        bool mDoClustering;

        #region Explore Menu items

        private void menuItemPCAPlot_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlotTable(selectedNodeTag, 2))
            {
                return;
            }

            mPCAOptions = new clsPCAplotPar();
            var datasetNameInR = selectedNodeTag.RDatasetName;

            mPCAOptions.tempFile = mRTempFilePath;
            mPCAOptions.RDataset = datasetNameInR;
            mPCAOptions.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mPCAOptions.mstrDatasetName = selectedNodeTag.DataText;

            PlotPCA(mPCAOptions);
        }

        /// <summary>
        /// This will be called from the plot forms. thus the reason to be public
        /// </summary>
        /// <param name="mclsPCA"></param>
        public void PlotPCA(clsPCAplotPar pcaPlottingOptions)
        {
            if (mDataTab.Controls.Count == 0)
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_PCAPlotCompleted;

            #endregion

            var frmPCA = new frmPCAplotPar(pcaPlottingOptions);
            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                if (factorTable.mDTable.Columns.Count - 1 == pcaPlottingOptions.Datasets.Count)
                    frmPCA.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);
                else
                    frmPCA.PopulateFactorComboBox = null;
            }
            else
                frmPCA.PopulateFactorComboBox = null;

            if (frmPCA.ShowDialog() == DialogResult.OK)
            {
                mPCAOptions = frmPCA.PCAOptions;
                var pcaPlots = new clsRplotData(mPCAOptions.RCommand, "PCA");

                m_BackgroundWorker.RunWorkerAsync(pcaPlots);
                mProgressForm.Reset("Generating PCA Plots ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_PCAPlotCompleted;

            #endregion
        }

        private int maxRow;

        private void mnuItemHeatmap_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            maxRow = selectedNodeTag.mDTable.Rows.Count;

            if (!ValidateIsPlotTable(selectedNodeTag, 2))
            {
                return;
            }

            mHeatmapOptions = new clsHeatmapPar();
            var datasetNameInR = selectedNodeTag.RDatasetName;

            var selectedRowKeys = new List<string>();

            var selectedRows = ((ucDataGridView)mExpressionsTab.Controls[0]).SelectedRows;

            //if (((ucDataGridView)this.mExpressionsTab.Controls[0]).SelectedRows.Count > 1000)
            //    MessageBox.Show("Maximum number of rows is set to 1000." + Environment.NewLine +
            //        "Select less than 1000 rows.", "Too many rows to plot",
            //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //else
            //{
            foreach (DataGridViewRow row in selectedRows)
            {
                selectedRowKeys.Add(row.Cells[0].Value.ToString());
            }

            mHeatmapOptions.tempFile = mRTempFilePath;
            mHeatmapOptions.RDataset = datasetNameInR;
            mHeatmapOptions.mstrDatasetName = selectedNodeTag.DataText;
            mHeatmapOptions.SelectedRows = selectedRowKeys;

            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                mHeatmapOptions.Factors = clsDataTable.DataTableRows(factorTable.mDTable);
            }
            else
                mHeatmapOptions.Factors = null;

            PlotHeatmap(mHeatmapOptions);
        }

        public void PlotHeatmap(clsHeatmapPar heatmapOptions)
        {
            if (mDataTab.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GenerateHeatmap;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_HeatMapCompleted;

                #endregion

                var frmHeatmapParams = new frmHeatMapPar(heatmapOptions)
                {
                    maxRowCount = maxRow
                };

                var res = frmHeatmapParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mHeatmapOptions = frmHeatmapParams.clsHmapPar;
                    mDoClustering = frmHeatmapParams.DoClust;
                    var heatmapPlot = new clsRplotData(mHeatmapOptions.RCommand, "Hmap");

                    Add2AnalysisHTable(mHeatmapOptions, "Heatmap_Clustering");

                    m_BackgroundWorker.RunWorkerAsync(heatmapPlot);
                    mProgressForm.Reset("Generating Heatmap ...");
                    mProgressForm.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GenerateHeatmap;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_HeatMapCompleted;

                #endregion
            }
        }

        private void mnuItemPatterns_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlotTable(selectedNodeTag, 2))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_SearchPatterns;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_PatternSearchCompleted;

            #endregion

            mclsPatternPar = new clsPatternSearchPar();

            var datasetNameInR = selectedNodeTag.RDatasetName;
            mclsPatternPar.RDataset = datasetNameInR;
            mclsPatternPar.mstrDatasetName = selectedNodeTag.DataText;
            mclsPatternPar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);

            var patternCount = DAnTE.ExtraControls.InputBox.Show("How many patterns (1 ~ 6)?", "Number", "2");
            if (patternCount.Length > 0)
            {
                var patternDefForm = new frmPatterns(mclsPatternPar);

                try
                {
                    int N = Convert.ToInt16(patternCount);
                    if (N < 7)
                        patternDefForm.NumPatterns = N;
                    else
                        throw new ArgumentException("Number of patterns should be at most 6");

                    var res = patternDefForm.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        mclsPatternPar = patternDefForm.clsPatternPar;
                        Add2AnalysisHTable(mclsPatternPar, "Pattern_Search");

                        m_BackgroundWorker.RunWorkerAsync(mclsPatternPar.RCommand);
                        mProgressForm.Reset("Pattern Searching ...");
                        mProgressForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid entry ;-(" + Environment.NewLine + ex.Message, "Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_SearchPatterns;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_PatternSearchCompleted;

            #endregion
        }

        private bool SearchPatterns()
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(mclsPatternPar.RCommand);
                if (mRConnector.GetTableFromRmatrix("patternData"))
                {
                    var patternTable = mRConnector.DataTable.Copy();
                    patternTable.TableName = "patternData";
                    mRConnector.EvaluateNoReturn("cat(\"Pattern searching done.\n\")");
                    AddDataset2HashTable(patternTable);
                }
                else
                    success = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            return success;
        }

        #endregion
    }
}