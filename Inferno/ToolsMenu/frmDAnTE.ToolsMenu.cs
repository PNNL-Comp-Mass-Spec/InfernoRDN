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

            var mfrmFilterANOVA = new frmFilterAnova
            {
                PopulateDataComboBox = AvailableDataSources()
            };

            var mclsAnova = mhtDatasets["p-Values"];
            mfrmFilterANOVA.PopulateListBox = clsDataTable.DataTableColumns(mclsAnova.mDTable, true);

            if (mfrmFilterANOVA.ShowDialog() == DialogResult.OK)
            {
                var column = mfrmFilterANOVA.SelectedColumn + 1;
                var datasetNameInR = (mhtDatasets[mfrmFilterANOVA.Dataset]).mstrRdatasetName;
                var thres = mfrmFilterANOVA.Thres.ToString(CultureInfo.InvariantCulture);
                string ltgt;
                if (mfrmFilterANOVA.LessThan)
                    ltgt = @"smode=""LT""";
                else
                    ltgt = @"smode=""GT""";

                var rcmd = "filterResult <- filterAnova(pvalues," + datasetNameInR + "," + thres + "," +
                              column.ToString() + "," + ltgt + ")";
                var mblErr = true;
                var mblNoData = true;
                try
                {
                    mintFilterTblNum++;
                    filtTableName = "filteredData" + mintFilterTblNum.ToString();
                    mRConnector.EvaluateNoReturn(rcmd);
                    mRConnector.EvaluateNoReturn("err<-filterResult$error");
                    mRConnector.EvaluateNoReturn("nodata<-filterResult$NoData");
                    mRConnector.EvaluateNoReturn(filtTableName + "<-filterResult$Filtered");
                    mblErr = mRConnector.GetSymbolAsBool("err");
                    mblNoData = mRConnector.GetSymbolAsBool("nodata");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mblErr = true;
                    mblNoData = true;
                }
                if (mblErr || mblNoData)
                    MessageBox.Show("No matches found. Check if you selected the correct dataset or" +
                                    Environment.NewLine + "if your cutoff is too conservative.",
                                    "Problem...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (mRConnector.GetTableFromRmatrix(filtTableName))
                {
                    var mDTfiltered = mRConnector.DataTable.Copy();
                    mDTfiltered.TableName = filtTableName;
                    mDTfiltered.Columns[0].ColumnName = "ID";
                    AddDataset2HashTable(mDTfiltered);
                    if (mhtDatasets.ContainsKey("Filtered Data" + mintFilterTblNum))
                        AddDataNode(mhtDatasets["Filtered Data" + mintFilterTblNum]);
                }
            }
        }

        private void mnuItemMissFilt_Click(object sender, EventArgs e)
        {

            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("filter data"))
            {
                return;
            }

            var dataset = mclsSelected.mstrRdatasetName;

            var mfrmMissing = new frmMissingFilter
            {
                DataSetName = mclsSelected.mstrDataText
            };

            if (mfrmMissing.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filterCutoff = mfrmMissing.CutOff;
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

                var mDTmissFilt = mRConnector.DataTable.Copy();
                mDTmissFilt.TableName = filtTableName;
                AddDataset2HashTable(mDTmissFilt);
                AddDataNode(mhtDatasets["Filtered Data" + mintFilterTblNum]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }
        }

        private void mnuItemMergeCols_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            var dataset = mclsSelected.mstrRdatasetName;

            if (!ValidateExpressionsLoaded("merge columns"))
            {
                return;
            }

            if (!ValidateFactorsDefined("merge columns"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_MergeC;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MergeCCompleted;

            #endregion

            var mfrmMergeC = new frmMergeColsPar();
            var mclsFactors = mhtDatasets["Factors"];
            mfrmMergeC.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
            mfrmMergeC.DataSetName = mclsSelected.mstrDataText;

            if (mfrmMergeC.ShowDialog() == DialogResult.OK)
            {
                var pmode = mfrmMergeC.pMode;
                var factor = mfrmMergeC.SelectedFactor;

                var rcmd = "mergedData <- mergeColumns(" + dataset + "," + factor + ",";
                rcmd += pmode + ")";
                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "Merging Columns ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MergeC;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MergeCCompleted;

            #endregion

        }

        private void mnuArrangeColumns_Click(object sender, EventArgs e)
        {

            if (!ValidateExpressionsLoaded("impute"))
            {
                return;
            }

            var mfrmArrCols = new frmArrangeColumns
            {
                DatasetInfo = marrDatasetInfo
            };
            if (mfrmArrCols.ShowDialog() == DialogResult.OK)
            {
                var newOrder = mfrmArrCols.NewDatasetOrder;
            }
        }

        private void mnuAnalysisSummary_Click(object sender, EventArgs e)
        {
            var frmSummary = new frmAnalysisSummary
            {
                SummaryArrayList = marrAnalysisObjects,
                DataFileName = mstrLoadedfileName
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

