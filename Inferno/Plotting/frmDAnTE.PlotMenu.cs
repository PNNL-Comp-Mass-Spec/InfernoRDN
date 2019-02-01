using System;
using System.Collections.Generic;
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
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlotTable(selectedNodeTag, 2))
            {
                return;
            }

            mclsQQPar = new clsQQPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsQQPar.tempFile = mRTempFilePath;
            mclsQQPar.Rdataset = datasetNameInR;
            mclsQQPar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsQQPar.mstrDatasetName = selectedNodeTag.mstrDataText;

            PlotQQ(mclsQQPar);
        }

        public void PlotQQ(clsQQPar clsQQPar)
            // this will be called from the plot forms.
            // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_QQPlotCompleted;

                #endregion

                var qqParams = new frmQQPar(clsQQPar);

                var res = qqParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsQQPar = qqParams.clsQQPar;
                    var qqPlot = new clsRplotData(clsQQPar.RCommand, "QQ");

                    m_BackgroundWorker.RunWorkerAsync(qqPlot);
                    mfrmShowProgress.Reset("Generating Q-Q Plots ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_QQPlotCompleted;

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
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateIsPlotTable(selectedNodeTag, 2))
            {
                return;
            }

            mclsHistPar = new clsHistogramPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsHistPar.tempFile = mRTempFilePath;
            mclsHistPar.Rdataset = datasetNameInR;
            mclsHistPar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsHistPar.mstrDatasetName = selectedNodeTag.mstrDataText;

            PlotHistograms(mclsHistPar);
        }

        public void PlotHistograms(clsHistogramPar clsHistPar)
            // this will be called from the plot forms.
            // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_HistPlotCompleted;

                #endregion

                var histogramParams = new frmHistogramPar(clsHistPar);

                var res = histogramParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsHistPar = histogramParams.clsHistPar;
                    var histogramPlot = new clsRplotData(clsHistPar.RCommand, "Hist");

                    m_BackgroundWorker.RunWorkerAsync(histogramPlot);
                    mfrmShowProgress.Reset("Generating Histograms ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_HistPlotCompleted;

                #endregion
            }
        }

        private void menuItemCorr_Click(object sender, EventArgs e)
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

            mclsCorrPar = new clsCorrelationPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsCorrPar.tempFile = mRTempFilePath;
            mclsCorrPar.Rdataset = datasetNameInR;
            mclsCorrPar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsCorrPar.mstrDatasetName = selectedNodeTag.mstrDataText;

            PlotCorrelation(mclsCorrPar);
        }

        public void PlotCorrelation(clsCorrelationPar clsCorrPar)
            // this will be called from the plot forms.
            // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_CorrPlotCompleted;

                #endregion

                var correlationParams = new frmCorrelationPar(clsCorrPar);

                var res = correlationParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsCorrPar = correlationParams.clsCorrPar;
                    var correlationPlot = new clsRplotData(mclsCorrPar.RCommand, "Corr");

                    m_BackgroundWorker.RunWorkerAsync(correlationPlot);
                    mfrmShowProgress.Reset("Generating Correlation Plot ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_CorrPlotCompleted;

                #endregion
            }
        }

        private void menuItemBoxPlot_Click(object sender, EventArgs e)
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

            mclsBoxPlotPar = new clsBoxPlotPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsBoxPlotPar.tempFile = mRTempFilePath;
            mclsBoxPlotPar.Rdataset = datasetNameInR;
            mclsBoxPlotPar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsBoxPlotPar.mstrDatasetName = selectedNodeTag.mstrDataText;
            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorDataset = mhtDatasets["Factors"];
                mclsBoxPlotPar.Factors = clsDataTable.DataTableRows(factorDataset.mDTable);
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

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_BoxPlotCompleted;

                #endregion

                var boxPlotParams = new frmBoxPlotPar(clsBoxPlotPar);

                var res = boxPlotParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsBoxPlotPar = boxPlotParams.clsBoxPlotPar;
                    var boxPlot = new clsRplotData(mclsBoxPlotPar.RCommand, "Box");

                    m_BackgroundWorker.RunWorkerAsync(boxPlot);
                    mfrmShowProgress.Reset("Generating the Box Plot ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_BoxPlotCompleted;

                #endregion
            }
        }

        private void mnuProteinRollupPlot_Click(object sender, EventArgs e)
        {
            var dataSourceList = new List<string>();
            var validPlot = false;

            if (mhtDatasets.ContainsKey("QRollup"))
            {
                dataSourceList.Add("QRollup");
                validPlot = true;
            }
            if (mhtDatasets.ContainsKey("RRollup"))
            {
                dataSourceList.Add("RRollup");
                validPlot = true;
            }
            if (mhtDatasets.ContainsKey("ZRollup"))
            {
                dataSourceList.Add("ZRollup");
                validPlot = true;
            }
            if (mhtDatasets.ContainsKey("Protein Info"))
            {
                dataSourceList.Add("None<raw>");
                validPlot = true;
            }

            if (!validPlot)
            {
                MessageBox.Show("No Protein Information found", "Nothing to do");
                return;
            }

            var frmRollup = new frmPlotProteinRollup
            {
                PopulateDataComboBox = AvailableDataSources(),
                PopulatePDataComboBox = dataSourceList,
                TempFile = mRTempFilePath,
                RConnect = mRConnector,
                ParentRef = (frmDAnTEmdi)this.MdiParent,
                m_frmDAnTE = this
            };
            frmRollup.Show();
        }

        private void menuItemMAPlot_Click(object sender, EventArgs e)
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

            mclsMApar = new clsMAplotsPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsMApar.tempFile = mRTempFilePath;
            mclsMApar.Rdataset = datasetNameInR;
            mclsMApar.Datasets = clsDataTable.DataTableColumns(selectedNodeTag.mDTable, datasetNameInR);
            mclsMApar.mstrDatasetName = selectedNodeTag.mstrDataText;

            PlotMA(mclsMApar);
        }

        public void PlotMA(clsMAplotsPar clsMApar)
            // this will be called from the plot forms.
            // thus the reason to be public
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MAplotCompleted;

                #endregion

                var maPlotParams = new frmMAplotsPar(clsMApar);

                var res = maPlotParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    mclsMApar = maPlotParams.clsMAplotPar;
                    var maPlot = new clsRplotData(mclsMApar.RCommand, "MA");

                    m_BackgroundWorker.RunWorkerAsync(maPlot);
                    mfrmShowProgress.Reset("Generating MA Plots ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MAplotCompleted;

                #endregion
            }
        }

        private void CopyDataGridRowsToClipboard(bool copyAll)
        {
            try
            {
                var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

                if (!ValidateNodeIsSelected(selectedNodeTag))
                {
                    return;
                }

                var currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;

                var startTime = DateTime.UtcNow;

                if (copyAll)
                    currGrid.SelectAll();

                var selectedRowCount = currGrid.SelectedRows.Count;

                var dataToCopy = currGrid.GetClipboardContent();
                if (dataToCopy != null)
                    Clipboard.SetDataObject(dataToCopy);

                if (DateTime.UtcNow.Subtract(startTime).TotalMilliseconds > 300)
                {
                    // The copy operation took over 300 msec
                    // Show a status dialog to the user
                    MessageBox.Show("Copy complete: " + selectedRowCount + " rows", "Done", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying data to the clipboard: " + ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        private void ctxtMnuItemCopyAll_Click(object sender, EventArgs e)
        {
            const bool copyAll = true;
            CopyDataGridRowsToClipboard(copyAll);
        }

        private void ctxtMnuItemCopySelected_Click(object sender, EventArgs e)
        {
            const bool copyAll = false;
            CopyDataGridRowsToClipboard(copyAll);
        }

        private void ctxtMnuItemPlotRows_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            var plotDisplay = new frmPlotDisplay();
            var currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;

            if (!ValidateIsPlotTable(selectedNodeTag))
            {
                return;
            }

            var datasetNameInR = selectedNodeTag.mstrRdatasetName;
            var selectedRowData = new StringBuilder();
            selectedRowData.Append("c(");

            var selectedRows = GetSelectedRows(currGrid);
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

            var addComma = false;
            foreach (DataGridViewRow row in selectedRows)
            {
                if (addComma)
                    selectedRowData.Append(",");

                selectedRowData.Append(@"""" + row.Cells[0].Value + @"""");
                addComma = true;
            }
            selectedRowData.Append(")");

            var rcmd = "PlotRows(" + datasetNameInR + "," + selectedRowData + @",file=""" + mRTempFilePath + @""")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                plotDisplay.Image = LoadImage(mRTempFilePath);
                plotDisplay.EnableParameterMenu = false;
                plotDisplay.MdiParent = m_frmDAnTEmdi;
                plotDisplay.Title = "Table Rows";
                plotDisplay.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void mnuItemVenn_Click(object sender, EventArgs e)
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

            mclsVennPar = new clsVennPar();
            var datasetNameInR = selectedNodeTag.mstrRdatasetName;

            mclsVennPar.tempFile = mRTempFilePath;
            mclsVennPar.Rdataset = datasetNameInR;
            mclsVennPar.mhtDatasets = mhtDatasets;
            mclsVennPar.marrDatasets = AvailableDataSources();

            mclsVennPar.mstrDatasetName = selectedNodeTag.mstrDataText;
            if (mhtDatasets.ContainsKey("Factors"))
            {
                mclsVennPar.marrFactors = marrFactorInfo;
            }

            PlotVenn(mclsVennPar);
        }

        public void PlotVenn(clsVennPar vennParameters)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Hook Threading Events

                m_BackgroundWorker.DoWork += m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_VennCompleted;

                #endregion

                var vennDiagramParams = new frmVennDiagramPar(vennParameters);
                var res = vennDiagramParams.ShowDialog();
                if (res == DialogResult.OK)
                {
                    vennParameters = vennDiagramParams.clsVennPar;
                    var vennDiagramPlot = new clsRplotData(vennParameters.RCommand, "Venn");

                    m_BackgroundWorker.RunWorkerAsync(vennDiagramPlot);
                    mfrmShowProgress.Reset("Generating Venn Diagram ...");
                    mfrmShowProgress.ShowDialog();
                }

                #region Unhook Threading Events

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_GeneratePlots;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_VennCompleted;

                #endregion
            }
        }

        #endregion
    }
}