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
        clsPCAplotPar mclsPCApar;
        clsHeatmapPar mclsHeatmapPar;
        clsPatternSearchPar mclsPatternPar;

        bool doClust;

        #region Explore Menu items

        private void menuItemPCAPlot_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlottable(selectedNodeTag, 2))
            {
                return;
            }

            mclsPCApar = new clsPCAplotPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsPCApar.tempFile = mRTempFilePath;
            mclsPCApar.Rdataset = datasetNameInR;
            mclsPCApar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsPCApar.mstrDatasetName = selectedNodeTag.mstrDataText;

            PlotPCA(mclsPCApar);
        }

        /// <summary>
        /// This will be called from the plot forms. thus the reason to be public
        /// </summary>
        /// <param name="mclsPCA"></param>
        public void PlotPCA(clsPCAplotPar mclsPCA)      
        {
            if (mtabControlData.Controls.Count == 0)
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_PCAPlotCompleted;
            #endregion

            var frmPCA = new frmPCAplotPar(mclsPCA);
            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                if ((factorTable.mDTable.Columns.Count - 1) == mclsPCA.Datasets.Count)
                    frmPCA.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);
                else
                    frmPCA.PopulateFactorComboBox = null;
            }
            else
                frmPCA.PopulateFactorComboBox = null;

            if (frmPCA.ShowDialog() == DialogResult.OK)
            {
                mclsPCApar = frmPCA.clsPCApar;
                var pcaPlots = new clsRplotData(mclsPCApar.Rcmd, "PCA");

                m_BackgroundWorker.RunWorkerAsync(pcaPlots);
                mfrmShowProgress.Message = "Generating PCA Plots ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_PCAPlotCompleted;
            #endregion
        }

        private int maxRow;

        private void mnuItemHeatmap_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            maxRow = selectedNodeTag.mDTable.Rows.Count;

            if (!ValidateIsPlottable(selectedNodeTag, 2))
            {
                return;
            }

            mclsHeatmapPar = new clsHeatmapPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            var selectedRowKeys = new List<string>();

            var selectedRows = ((ucDataGridView)ctltabPage.Controls[0]).SelectedRows;

            //if (((ucDataGridView)this.ctltabPage.Controls[0]).SelectedRows.Count > 1000)
            //    MessageBox.Show("Maximum number of rows is set to 1000." + Environment.NewLine +
            //        "Select less than 1000 rows.", "Too many rows to plot",
            //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //else
            //{
            foreach (DataGridViewRow row in selectedRows)
            {
                selectedRowKeys.Add(row.Cells[0].Value.ToString());
            }

            mclsHeatmapPar.tempFile = mRTempFilePath;
            mclsHeatmapPar.Rdataset = datasetNameInR;
            mclsHeatmapPar.mstrDatasetName = selectedNodeTag.mstrDataText;
            mclsHeatmapPar.SelectedRows = selectedRowKeys;

            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                mclsHeatmapPar.Factors = clsDataTable.DataTableRows(factorTable.mDTable);
            }
            else
                mclsHeatmapPar.Factors = null;

            PlotHeatmap(mclsHeatmapPar);

        }

        public void PlotHeatmap(clsHeatmapPar mclsHmapPar)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += m_BackgroundWorker_GenerateHeatmap;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_HeatMapCompleted;
                #endregion

                var frmHmapParams = new frmHeatMapPar(mclsHmapPar)
                {
                    maxRowCount = maxRow
                };

                var res = frmHmapParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsHeatmapPar = frmHmapParams.clsHmapPar;
                    doClust = frmHmapParams.DoClust;
                    var heatmapPlot = new clsRplotData(mclsHeatmapPar.Rcmd, "Hmap");

                    Add2AnalysisHTable(mclsHeatmapPar, "Heatmap_Clustering");

                    m_BackgroundWorker.RunWorkerAsync(heatmapPlot);
                    mfrmShowProgress.Message = "Generating Heatmap ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GenerateHeatmap;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_HeatMapCompleted;
                #endregion
            }
        }

        private void mnuItemPatterns_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlottable(selectedNodeTag, 2))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_SearchPatterns;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_PatternSearchCompleted;
            #endregion

            mclsPatternPar = new clsPatternSearchPar();
            
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;
            mclsPatternPar.Rdataset = datasetNameInR;
            mclsPatternPar.mstrDatasetName = selectedNodeTag.mstrDataText;
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

                        m_BackgroundWorker.RunWorkerAsync(mclsPatternPar.Rcmd);
                        mfrmShowProgress.Message = "Pattern Searching ...";
                        mfrmShowProgress.ShowDialog();
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
                mRConnector.EvaluateNoReturn(mclsPatternPar.Rcmd);
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