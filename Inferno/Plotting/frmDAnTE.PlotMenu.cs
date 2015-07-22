using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Purgatorio;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        clsHistogramPar mclsHistPar;
        clsQQPar mclsQQPar;
        clsCorrelationPar mclsCorrPar;
        clsBoxPlotPar mclsBoxPlotPar;
        clsMAplotsPar mclsMApar;
        clsVennPar mclsVennPar;

        #region Plot Menu items

        private void mnuItemQQplot_Click(object sender, EventArgs e)
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

            mclsQQPar = new clsQQPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsQQPar.tempFile = mRTempFilePath;
            mclsQQPar.Rdataset = datasetNameInR;
            mclsQQPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsQQPar.mstrDatasetName = mclsSelected.mstrDataText;

            PlotQQ(mclsQQPar);

        }

        public void PlotQQ(clsQQPar clsQQPar)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_QQPlotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmQQPar mfrmQQPar;

                mfrmQQPar = new frmQQPar(clsQQPar);

                DialogResult res = mfrmQQPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsQQPar = mfrmQQPar.clsQQPar;

                    mclsRplots = new clsRplotData(clsQQPar.Rcmd, "QQ");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating Q-Q Plots ...";
                    mfrmShowProgress.ShowDialog();
                }
                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_QQPlotCompleted);
                #endregion
            }
        }

        /// <summary>
        /// Plot histograms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuHistogrms_Click(object sender, EventArgs e)
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

            mclsHistPar = new clsHistogramPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsHistPar.tempFile = mRTempFilePath;
            mclsHistPar.Rdataset = datasetNameInR;
            mclsHistPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsHistPar.mstrDatasetName = mclsSelected.mstrDataText;

            PlotHistograms(mclsHistPar);

        }

        public void PlotHistograms(clsHistogramPar clsHistPar)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_HistPlotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmHistogramPar mfrmHistPar;

                mfrmHistPar = new frmHistogramPar(clsHistPar);

                DialogResult res = mfrmHistPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsHistPar = mfrmHistPar.clsHistPar;

                    mclsRplots = new clsRplotData(clsHistPar.Rcmd, "Hist");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating Histograms ...";
                    mfrmShowProgress.ShowDialog();
                }
                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_HistPlotCompleted);
                #endregion
            }
        }

        private void menuItemCorr_Click(object sender, EventArgs e)
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

            mclsCorrPar = new clsCorrelationPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsCorrPar.tempFile = mRTempFilePath;
            mclsCorrPar.Rdataset = datasetNameInR;
            mclsCorrPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsCorrPar.mstrDatasetName = mclsSelected.mstrDataText;

            PlotCorrelation(mclsCorrPar);

        }

        public void PlotCorrelation(clsCorrelationPar clsCorrPar)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_CorrPlotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmCorrelationPar mfrmCorrPar;
                mfrmCorrPar = new frmCorrelationPar(clsCorrPar);

                DialogResult res = mfrmCorrPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsCorrPar = mfrmCorrPar.clsCorrPar;
                    mclsRplots = new clsRplotData(mclsCorrPar.Rcmd, "Corr");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating Correlation Plot ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_CorrPlotCompleted);
                #endregion
            }
        }

        private void menuItemBoxPlot_Click(object sender, EventArgs e)
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

            mclsBoxPlotPar = new clsBoxPlotPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsBoxPlotPar.tempFile = mRTempFilePath;
            mclsBoxPlotPar.Rdataset = datasetNameInR;
            mclsBoxPlotPar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsBoxPlotPar.mstrDatasetName = mclsSelected.mstrDataText;
            if (mhtDatasets.ContainsKey("Factors"))
            {
                clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
                mclsBoxPlotPar.Factors = clsDataTable.DataTableRows(mclsFactors.mDTable);
            }
            else
                mclsBoxPlotPar.Factors = null;

            PlotBoxPlots(mclsBoxPlotPar);

        }

        public void PlotBoxPlots(clsBoxPlotPar clsBoxPlotPar)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_BoxPlotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmBoxPlotPar mfrmBoxplotPar;

                mfrmBoxplotPar = new frmBoxPlotPar(clsBoxPlotPar);

                DialogResult res = mfrmBoxplotPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsBoxPlotPar = mfrmBoxplotPar.clsBoxPlotPar;
                    mclsRplots = new clsRplotData(mclsBoxPlotPar.Rcmd, "Box");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating the Box Plot ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_BoxPlotCompleted);
                #endregion
            }
        }

        private void mnuProteinRollupPlot_Click(object sender, EventArgs e)
        {
            var dataSourceList = new ArrayList();
            bool mblPlot = false;

            if (mhtDatasets.ContainsKey("QRollup"))
            {
                dataSourceList.Add("QRollup");
                mblPlot = true;
            }
            if (mhtDatasets.ContainsKey("RRollup"))
            {
                dataSourceList.Add("RRollup");
                mblPlot = true;
            }
            if (mhtDatasets.ContainsKey("ZRollup"))
            {
                dataSourceList.Add("ZRollup");
                mblPlot = true;
            }
            if (mhtDatasets.ContainsKey("Protein Info"))
            {
                dataSourceList.Add("None<raw>");
                mblPlot = true;
            }

            if (!mblPlot)
            {
                MessageBox.Show("No Protein Information found", "Nothing to do");
                return;
            }

            var mfrmPlotRollup = new frmPlotProteinRollup
            {
                PopulateDataComboBox = AvailableDataSources(),
                PopulatePDataComboBox = dataSourceList,
                TempFile = mRTempFilePath,
                RConnect = mRConnector,
                ParentRef = (frmDAnTEmdi)this.MdiParent,
                m_frmDAnTE = this
            };
            mfrmPlotRollup.Show();
        }

        private void menuItemMAPlot_Click(object sender, EventArgs e)
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

            mclsMApar = new clsMAplotsPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsMApar.tempFile = mRTempFilePath;
            mclsMApar.Rdataset = datasetNameInR;
            mclsMApar.Datasets = clsDataTable.DataTableColumns(mclsSelected.mDTable, datasetNameInR);
            mclsMApar.mstrDatasetName = mclsSelected.mstrDataText;

            PlotMA(mclsMApar);

        }

        public void PlotMA(clsMAplotsPar clsMApar)
        // this will be called from the plot forms.
        // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_MAplotCompleted);
                #endregion

                clsRplotData mclsRplots;
                frmMAplotsPar mfrmMAPar;

                mfrmMAPar = new frmMAplotsPar(clsMApar);

                DialogResult res = mfrmMAPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsMApar = mfrmMAPar.clsMAplotPar;
                    mclsRplots = new clsRplotData(mclsMApar.Rcmd, "MA");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating MA Plots ...";
                    mfrmShowProgress.ShowDialog();
                }
                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_MAplotCompleted);
                #endregion
            }
        }

        private void ctxtMnuItemPlotRows_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            frmPlotDisplay mfrmPlotDisplay = new frmPlotDisplay();
            DataGridView currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;

            if (!ValidateIsPlottable(mclsSelected))
            {
                return;
            }

            string datasetNameInR = mclsSelected.mstrRdatasetName;
            string rcmd = null;
            var selectedRowData = new StringBuilder();
            selectedRowData.Append("c(");

            DataGridViewSelectedRowCollection selectedRows = GetSelectedRows(currGrid);
            if (selectedRows.Count < 1)
            {
                MessageBox.Show("No rows have been selected", "Nothing to plot");
                return;
            }

            if (selectedRows.Count > 100)
            {
                MessageBox.Show("Cannot plot more than 100 rows; please select fewer rows then try again",
                                "Too many rows to plot",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool addComma = false;
            foreach (DataGridViewRow row in selectedRows)
            {
                if (addComma)
                    selectedRowData.Append(",");

                selectedRowData.Append(@"""" + row.Cells[0].Value + @"""");
                addComma = true;
            }
            selectedRowData.Append(")");

            rcmd = "PlotRows(" + datasetNameInR + "," + selectedRowData + @",file=""" + mRTempFilePath + @""")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mfrmPlotDisplay.Image = LoadImage(mRTempFilePath);
                mfrmPlotDisplay.EnableParameterMenu = false;
                mfrmPlotDisplay.MdiParent = m_frmDAnTEmdi;
                mfrmPlotDisplay.Title = "Table Rows";
                mfrmPlotDisplay.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void mnuItemVenn_Click(object sender, EventArgs e)
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

            mclsVennPar = new clsVennPar();
            string datasetNameInR = mclsSelected.mstrRdatasetName;

            mclsVennPar.tempFile = mRTempFilePath;
            mclsVennPar.Rdataset = datasetNameInR;
            mclsVennPar.mhtDatasets = mhtDatasets;
            mclsVennPar.marrDatasets = AvailableDataSources();

            mclsVennPar.mstrDatasetName = mclsSelected.mstrDataText;
            if (mhtDatasets.ContainsKey("Factors"))
            {
                clsDatasetTreeNode mclsFactors = (clsDatasetTreeNode)mhtDatasets["Factors"];
                mclsVennPar.marrFactorNames = clsDataTable.DataTableRows(mclsFactors.mDTable);
                mclsVennPar.marrFactors = marrFactorInfo;
            }
            else
                mclsVennPar.marrFactorNames = null;

            PlotVenn(mclsVennPar);

        }

        public void PlotVenn(clsVennPar vennParameters)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_VennCompleted);
                #endregion

                frmVennDiagramPar mfrmVennPar = new frmVennDiagramPar(vennParameters);
                DialogResult res = mfrmVennPar.ShowDialog();
                if (res == DialogResult.OK)
                {
                    vennParameters = mfrmVennPar.clsVennPar;
                    var mclsRplots = new clsRplotData(vennParameters.Rcmd, "Venn");

                    m_BackgroundWorker.RunWorkerAsync(mclsRplots);
                    mfrmShowProgress.Message = "Generating Venn Diagram ...";
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_GeneratePlots);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_VennCompleted);
                #endregion
            }
        }

        #endregion
    }
}