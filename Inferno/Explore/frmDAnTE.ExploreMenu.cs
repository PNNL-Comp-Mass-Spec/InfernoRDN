using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
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

        bool doClust = false;

        #region Explore Menu items

        private void menuItemPCAPlot_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateIsPlottable(mclsSelected, 2))
            {
                return;
            }

            mclsPCApar = new clsPCAplotPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsPCApar.tempFile = mRTempFilePath;
            mclsPCApar.Rdataset = datasetNameInR;
            mclsPCApar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsPCApar.mstrDatasetName = mclsSelected.mstrDataText;

            PlotPCA(mclsPCApar);
        }

        public void PlotPCA(clsPCAplotPar mclsPCA)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_PCAPlotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmPCAplotPar mfrmPCA;

                mfrmPCA = new frmPCAplotPar(mclsPCA);
                if (mhtDatasets.ContainsKey("Factors"))
                {
                    clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
                    if ((mclsFactors.mDTable.Columns.Count - 1) == mclsPCA.Datasets.Count)
                        mfrmPCA.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                    else
                        mfrmPCA.PopulateFactorComboBox = null;
                }
                else
                    mfrmPCA.PopulateFactorComboBox = null;

                if (mfrmPCA.ShowDialog() == DialogResult.OK)
                {
                    mclsPCApar = mfrmPCA.clsPCApar;
                    mclsRplots = new clsRplotData(mclsPCApar.Rcmd, "PCA");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating PCA Plots ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_PCAPlotCompleted);
                #endregion
            }
        }

        private int maxRow = 0;

        private void mnuItemHeatmap_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            maxRow = mclsSelected.mDTable.Rows.Count;

            if (!ValidateIsPlottable(mclsSelected, 2))
            {
                return;
            }

            mclsHeatmapPar = new clsHeatmapPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            ArrayList marrSelRows = new ArrayList();

            DataGridViewSelectedRowCollection selectedRows =
                ((ucDataGridView)this.ctltabPage.Controls[0]).SelectedRows;

            //if (((ucDataGridView)this.ctltabPage.Controls[0]).SelectedRows.Count > 1000)
            //    MessageBox.Show("Maximum number of rows is set to 1000." + Environment.NewLine +
            //        "Select less than 1000 rows.", "Too many rows to plot",
            //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //else
            //{
            foreach (DataGridViewRow row in selectedRows)
            {
                marrSelRows.Add(row.Cells[0].Value);
            }

            mclsHeatmapPar.tempFile = mRTempFilePath;
            mclsHeatmapPar.Rdataset = datasetNameInR;
            mclsHeatmapPar.mstrDatasetName = mclsSelected.mstrDataText;
            mclsHeatmapPar.SelectedRows = marrSelRows;

            if (mhtDatasets.ContainsKey("Factors"))
            {
                clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
                mclsHeatmapPar.Factors = clsDataTable.DataTableRows(mclsFactors.mDTable);
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
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GenerateHeatmap);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_HeatMapCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmHeatMapPar mfrmHmapPar;

                mfrmHmapPar = new frmHeatMapPar(mclsHmapPar);
                mfrmHmapPar.maxRowCount = maxRow;
                DialogResult res = mfrmHmapPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsHeatmapPar = mfrmHmapPar.clsHmapPar;
                    doClust = mfrmHmapPar.DoClust;
                    mclsRplots = new clsRplotData(mclsHeatmapPar.Rcmd, "Hmap");

                    Add2AnalysisHTable(mclsHeatmapPar, "Heatmap_Clustering");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating Heatmap ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GenerateHeatmap);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_HeatMapCompleted);
                #endregion
            }
        }

        private void mnuItemPatterns_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateIsPlottable(mclsSelected, 2))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_SearchPatterns);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_PatternSearchCompleted);
            #endregion

            mclsPatternPar = new clsPatternSearchPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;
            mclsPatternPar.Rdataset = datasetNameInR;
            mclsPatternPar.mstrDatasetName = mclsSelected.mstrDataText;
            mclsPatternPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);

            string mstrnum = DAnTE.ExtraControls.InputBox.Show("How many patterns (1 ~ 6)?", "Number", "2");
            if (mstrnum.Length > 0)
            {
                var mfrmPatterns = new frmPatterns(mclsPatternPar);

                try
                {
                    int N = Convert.ToInt16(mstrnum);
                    if (N < 7)
                        mfrmPatterns.NumPatterns = N;
                    else
                        throw new System.ArgumentException("Number of patterns should be at most 6", "N_Patterns");

                    DialogResult res = mfrmPatterns.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        mclsPatternPar = mfrmPatterns.clsPatternPar;
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
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_SearchPatterns);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_PatternSearchCompleted);
            #endregion

        }

        private bool SearchPatterns(string rcmd)
        {
            bool success = true;
            DataTable mDTPatterns;
            try
            {
                mRConnector.EvaluateNoReturn(mclsPatternPar.Rcmd);
                if (mRConnector.GetTableFromRmatrix("patternData"))
                {
                    mDTPatterns = mRConnector.DataTable.Copy();
                    mDTPatterns.TableName = "patternData";
                    mRConnector.EvaluateNoReturn("cat(\"Pattern searching done.\n\")");
                    AddDataset2HashTable(mDTPatterns);
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