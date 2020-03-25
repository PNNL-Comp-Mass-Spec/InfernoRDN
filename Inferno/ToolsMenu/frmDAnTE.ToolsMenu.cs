using System;
using System.Globalization;
using System.Windows.Forms;
using DAnTE.Paradiso;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        private void mnuItemFilterpq_Click(object sender, EventArgs e)
        {
            string filtTableName = null;

            if (!mhtDatasets.ContainsKey("p-Values"))
            {
                MessageBox.Show("'p-Values' table not found; cannot filter using ANOVA results", "Error");
                return;
            }

            var anovaParams = new frmFilterAnova
            {
                PopulateDataComboBox = AvailableDataSources()
            };

            var pvaluesTable = mhtDatasets["p-Values"];
            anovaParams.PopulateListBox = clsDataTable.DataTableColumns(pvaluesTable.mDTable, true);

            if (anovaParams.ShowDialog() == DialogResult.OK)
            {
                var column = anovaParams.SelectedColumn + 1;
                var datasetNameInR = (mhtDatasets[anovaParams.Dataset]).RDatasetName;
                var thres = anovaParams.Thres.ToString(CultureInfo.InvariantCulture);
                string ltgt;
                if (anovaParams.LessThan)
                    ltgt = @"smode=""LT""";
                else
                    ltgt = @"smode=""GT""";

                var rcmd = "filterResult <- filterAnova(pvalues," + datasetNameInR + "," + thres + "," +
                           column + "," + ltgt + ")";

                bool executionError;
                bool dataNotFound;
                try
                {
                    mintFilterTblNum++;
                    filtTableName = "filteredData" + mintFilterTblNum.ToString();
                    mRConnector.EvaluateNoReturn(rcmd);
                    mRConnector.EvaluateNoReturn("err<-filterResult$error");
                    mRConnector.EvaluateNoReturn("nodata<-filterResult$NoData");
                    mRConnector.EvaluateNoReturn(filtTableName + "<-filterResult$Filtered");
                    executionError = mRConnector.GetSymbolAsBool("err");
                    dataNotFound = mRConnector.GetSymbolAsBool("nodata");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    executionError = true;
                    dataNotFound = true;
                }
                if (executionError || dataNotFound)
                {
                    MessageBox.Show("No matches found. Check if you selected the correct dataset or" +
                                    Environment.NewLine + "if your cutoff is too conservative.",
                                    "Problem...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (mRConnector.GetTableFromRmatrix(filtTableName))
                {
                    var filteredDataFromR = mRConnector.DataTable.Copy();
                    filteredDataFromR.TableName = filtTableName;
                    filteredDataFromR.Columns[0].ColumnName = "ID";
                    AddDataset2HashTable(filteredDataFromR);
                    if (mhtDatasets.ContainsKey("Filtered Data" + mintFilterTblNum))
                        AddDataNode(mhtDatasets["Filtered Data" + mintFilterTblNum]);
                }
            }
        }

        private void mnuItemMissFilt_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("filter data"))
            {
                return;
            }

            var dataset = selectedNodeTag.RDatasetName;

            var missingValueParams = new frmMissingFilter
            {
                DataSetName = selectedNodeTag.DataText
            };

            if (missingValueParams.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filterCutoff = missingValueParams.CutOff;
            mintFilterTblNum++;
            var filtTableName = "filteredData" + mintFilterTblNum.ToString();
            var rcmd = filtTableName + "<- filterMissing(" + dataset + "," + filterCutoff + ")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                if (!mRConnector.GetTableFromRmatrix(filtTableName))
                {
                    return;
                }

                var dataTableFromR = mRConnector.DataTable.Copy();
                dataTableFromR.TableName = filtTableName;
                AddDataset2HashTable(dataTableFromR);
                AddDataNode(mhtDatasets["Filtered Data" + mintFilterTblNum]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }
        }

        private void mnuItemMergeCols_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            var dataset = selectedNodeTag.RDatasetName;

            if (!ValidateExpressionsLoaded("merge columns"))
            {
                return;
            }

            if (!ValidateFactorsDefined("merge columns"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_MergeC;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MergeCCompleted;

            #endregion

            var mergeColumnsParams = new frmMergeColsPar();
            var factorTable = mhtDatasets["Factors"];
            mergeColumnsParams.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);
            mergeColumnsParams.DataSetName = selectedNodeTag.DataText;

            if (mergeColumnsParams.ShowDialog() == DialogResult.OK)
            {
                var pmode = mergeColumnsParams.pMode;
                var factor = mergeColumnsParams.SelectedFactor;

                var rcmd = "mergedData <- mergeColumns(" + dataset + "," + factor + "," + pmode + ")";
                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mProgressForm.Reset("Merging Columns ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MergeC;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MergeCCompleted;

            #endregion
        }

        [Obsolete("Unused")]
        private void mnuArrangeColumns_Click(object sender, EventArgs e)
        {
            if (!ValidateExpressionsLoaded("impute"))
            {
                return;
            }

            var arrangeColumnsParams = new frmArrangeColumns
            {
                DatasetInfo = marrDatasetInfo
            };

            if (arrangeColumnsParams.ShowDialog() == DialogResult.OK)
            {
                var newOrder = arrangeColumnsParams.NewDatasetOrder;
            }
        }

        private void mnuAnalysisSummary_Click(object sender, EventArgs e)
        {
            var frmSummary = new frmAnalysisSummary
            {
                SummaryArrayList = marrAnalysisObjects,
                DataFileName = mLoadedFilename
            };
            frmSummary.ShowDialog();
        }

        private void mnuShowRCommandLog_Click(object sender, EventArgs e)
        {
            var logViewerForm = new frmRCommandLog()
            {
                LogFilePath = clsRCmdLog.CurrentLogFilePath
            };

            logViewerForm.ShowDialog();
        }
    }
}