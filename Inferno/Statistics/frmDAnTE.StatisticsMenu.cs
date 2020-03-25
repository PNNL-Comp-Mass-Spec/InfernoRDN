using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Purgatorio;
using DAnTE.Tools;
using System.Collections.Generic;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        private List<clsDatasetInfo> marrDatasetInfo = new List<clsDatasetInfo>();
        private List<clsFactorInfo> marrFactorInfo = new List<clsFactorInfo>();
        private clsAnovaPar mclsAnovaPar;
        private clsTamuQPar mclsTamuQPar;
        private clsKruskalWPar mclsKWpar;
        private clsWilcoxonPar mclsWilcoxPar;
        private clsShapiroWilksPar mclsShapiroWilksPar;
        private clsFoldChangePar mclsFoldChangePar;
        private clsOneSampleTtestPar mclsOneSampleTtestPar;
        string RDataset;

        #region Private Methods

        /// <summary>
        /// Extract Factor information from a datatable
        /// to a list of clsDatasetInfo type.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="factorsLoaded"></param>
        private void DatasetFactorInfo(DataTable dt, bool factorsLoaded)
        {
            var factorNames = clsDataTable.DataTableColumns(dt, true); // get data only columns
            var factorList = new List<string>();

            marrDatasetInfo.Clear();
            for (var i = 0; i < factorNames.Count; i++)
            {
                var dsetItem = new clsDatasetInfo(factorNames[i]);
                if (factorsLoaded)
                {
                    for (var k = 0; k < dt.Rows.Count; k++) // go thru each row
                    {
                        var dataRow = dt.Rows[k];
                        if (i == 0)
                        {
                            var factorName = dataRow.ItemArray[i].ToString();
                            factorList.Add(factorName);
                        }
                        var currFactor = new Factor(factorList[k], dataRow.ItemArray[i + 1].ToString());
                        dsetItem.marrFactorAssnmnts.Add(currFactor);
                    }
                    //dsetItem.marrFactors = marrFactors;
                    dsetItem.factorsSET = true;
                }
                marrDatasetInfo.Add(dsetItem);
            }
        }

        /// <summary>
        /// Transform an Arraylist of DatasetInfo to a Datatable of Factor info.
        /// </summary>
        /// <returns></returns>
        private DataTable DatasetArr2DT()
        {
            var convertedData = new DataTable();

            var factorDataColumn = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Factors"
            };
            convertedData.Columns.Add(factorDataColumn);

            foreach (var dataset in marrDatasetInfo)
            {
                var datasetDataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = dataset.mstrDataSetName
                };
                convertedData.Columns.Add(datasetDataColumn);
            }

            for (var i = 0; i < marrDatasetInfo[0].marrFactorAssnmnts.Count; i++)
            {
                var dataRow = convertedData.NewRow();
                dataRow[0] = marrDatasetInfo[0].marrFactorAssnmnts[i].Name;
                for (var j = 0; j < marrDatasetInfo.Count; j++)
                {
                    dataRow[j + 1] = marrDatasetInfo[j].marrFactorAssnmnts[i].Value;
                }
                convertedData.Rows.Add(dataRow);
            }
            return convertedData;
        }

        private List<clsDatasetInfo> MakeDeepCopy(IEnumerable<clsDatasetInfo> sourceList)
        {
            var newList = new List<clsDatasetInfo>();
            foreach (var item in sourceList)
            {
                newList.Add((clsDatasetInfo)(item.Clone()));
            }
            return newList;
        }

        private List<clsFactorInfo> MakeDeepCopy(IEnumerable<clsFactorInfo> sourceList)
        {
            var newList = new List<clsFactorInfo>();
            foreach (var item in sourceList)
            {
                newList.Add((clsFactorInfo)(item.Clone()));
            }
            return newList;
        }

        private bool t_testPossible()
        {
            var ttest = false;
            foreach (var factor in marrFactorInfo)
            {
                if (factor.marrValues.Count == 2)
                    ttest = true;
            }
            return ttest;
        }

        private void ChangeDatasetOrder(IList<string> newNameOrder)
        {
            var oldNameOrder = new List<string>();
            var itemsToRemove = new List<string>();

            if (!ValidateExpressionsLoaded("change dataset order"))
            {
                return;
            }

            var expTable = (mhtDatasets["Expressions"]).mDTable;
            for (var num = 1; num < expTable.Columns.Count; num++)
            {
                oldNameOrder.Add(expTable.Columns[num].ColumnName);
            }

            foreach (var item in oldNameOrder)
            {
                if (!newNameOrder.Contains(item))
                    itemsToRemove.Add(item);
            }

            foreach (var strKey in mhtDatasets.Keys)
            {
                var currentNode = (mhtDatasets[strKey]);
                if (currentNode.IsNumeric)
                {
                    expTable = currentNode.mDTable;
                    foreach (var colName in itemsToRemove)
                    {
                        expTable.Columns.Remove(colName);
                    }

                    //reorder columns
                    for (var num = 0; num < newNameOrder.Count; num++)
                    {
                        if (expTable.Columns.Contains("PepCount"))
                            expTable.Columns[newNameOrder[num]].SetOrdinal(num + 2);
                        else
                            expTable.Columns[newNameOrder[num]].SetOrdinal(num + 1);
                    }
                }
            }
        }

        private void ChangeDatasetOrderR(IEnumerable<int> newIndexOrder)
        {
            var newOrderCommand = new StringBuilder();
            newOrderCommand.Append(@"newOrder=""c(");

            var vars = "numvars=" + NumericVars2R();

            foreach (var index in newIndexOrder)
            {
                var currentVal = index + 1;
                newOrderCommand.Append(currentVal + ",");
            }

            // Remove the trailing comma, then add the closing parenthesis and a double quote
            if (newOrderCommand.Length > 0 && newOrderCommand[newOrderCommand.Length - 1] == ',')
                newOrderCommand.Remove(newOrderCommand.Length - 1, 1);

            newOrderCommand.Append(")" + '"');

            var rcmd = "arrangeColumns(" + vars + "," + newOrderCommand + ")";
            Console.WriteLine(rcmd);
            try
            {
                mRConnector.EvaluateNoReturn("ls()");
                mRConnector.EvaluateNoReturn(rcmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
            }
        }

        #endregion

        #region Statistics Menu items

        private void mnuItemDefFactors_Click(object sender, EventArgs e)
        {
            var tmpDatasets = MakeDeepCopy(marrDatasetInfo); // keep copies
            var tmpFactors = MakeDeepCopy(marrFactorInfo); // keep copies

            var senderName = sender.ToString();

            if (!mhtDatasets.ContainsKey("Factors"))
            {
                if (!ValidateExpressionsLoaded("define factors; load some data using File->Open"))
                {
                    return;
                }
            }

            var factorInputForm = new frmFactorInformation
            {
                DatasetInfo = marrDatasetInfo,
                FactorsLoaded = mhtDatasets.ContainsKey("Factors"),
                FactorInfo = marrFactorInfo
            };

            if (senderName.Equals("Arrange Columns"))
            {
                factorInputForm.OrderChangeOnly = true;
                factorInputForm.Title = "Dataset Order";
                factorInputForm.SubTitle = "Change Dataset Order, Delete Datasets";
                factorInputForm.WinTitle = "Dataset Order";
            }

            if (factorInputForm.ShowDialog() == DialogResult.OK)
            {
                marrDatasetInfo = factorInputForm.DatasetInfo;
                if (factorInputForm.OrderChanged)
                {
                    ChangeDatasetOrder(factorInputForm.NewDatasetNameOrder);
                    ChangeDatasetOrderR(factorInputForm.NewDatasetOrder);
                }
                marrFactorInfo = factorInputForm.FactorInfo;
                if (marrFactorInfo.Count <= 0)
                    return;

                var factorTable = DatasetArr2DT();
                factorTable.Columns[0].ColumnName = "Factors";
                factorTable.TableName = "factors";
                AddDataset2HashTable(factorTable);
                if (mhtDatasets.ContainsKey("Factors"))
                {
                    AddDataNode(mhtDatasets["Factors"]);
                }

                if (mRConnector.SendTable2RmatrixNonNumeric("factors", factorTable))
                {
                    try
                    {
                        mRConnector.EvaluateNoReturn("print(factors)");
                        mRConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                    }
                }
            }
            else
            {
                //User cancelled the factor changes, so revert to previous.
                marrDatasetInfo = tmpDatasets; //Should we do a DeepCopy here?
                marrFactorInfo = tmpFactors;
            }
        }

        private void menuItemANOVA_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use ANOVA"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_ANOVA;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsAnovaPar = new clsAnovaPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsAnovaPar.RDataset = RDataset;
            mclsAnovaPar.mstrDatasetName = selectedNodeTag.DataText;
            var factorTable = mhtDatasets["Factors"];
            clsDataTable.DataTableRows(factorTable.mDTable);

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            var anovaParams = new frmANOVApar(mclsAnovaPar);
            if (factorTable.mDTable != null)
            {
                anovaParams.PopulateListBox = clsDataTable.DataTableRows(factorTable.mDTable);
                if (anovaParams.ShowDialog() == DialogResult.OK)
                {
                    mclsAnovaPar = anovaParams.clsAnovaPar;

                    Add2AnalysisHTable(mclsAnovaPar, "ANOVA");

                    m_BackgroundWorker.RunWorkerAsync(mclsAnovaPar.RCommand);
                    mProgressForm.Reset("Performing ANOVA ...");
                    mProgressForm.ShowDialog();
                }
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_ANOVA;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion
        }

        [Obsolete("Unused")]
        private void menuItemTamuQ_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use TamuQ"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_TamuQ;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_TamuQCompleted;

            #endregion

            mclsTamuQPar = new clsTamuQPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsTamuQPar.RDataset = RDataset;
            mclsTamuQPar.mstrDatasetName = selectedNodeTag.DataText;
            var factorTable = mhtDatasets["Factors"];
            clsDataTable.DataTableRows(factorTable.mDTable);

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            var tamuqParams = new frmTamuQpar(mclsTamuQPar);
            if (factorTable.mDTable != null)
            {
                tamuqParams.PopulateListBox = clsDataTable.DataTableRows(factorTable.mDTable);
                if (tamuqParams.ShowDialog() == DialogResult.OK)
                {
                    mclsTamuQPar = tamuqParams.clsTamuQPar;

                    Add2AnalysisHTable(mclsAnovaPar, "TamuQ");

                    m_BackgroundWorker.RunWorkerAsync(mclsTamuQPar.RCommand);
                    mProgressForm.Reset("Performing TamuQ ...");
                    mProgressForm.ShowDialog();
                }
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_TamuQ;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_TamuQCompleted;

            #endregion
        }

        private void menuItemKW_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use the Kruskal-Walis Test"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_KW;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsKWpar = new clsKruskalWPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsKWpar.RDataset = RDataset;
            mclsKWpar.mstrDatasetName = selectedNodeTag.DataText;
            var factorTable = mhtDatasets["Factors"];
            clsDataTable.DataTableRows(factorTable.mDTable);

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            var kwParams = new frmKruskalWpar(mclsKWpar);
            if (factorTable.mDTable != null)
            {
                kwParams.PopulateListBox = clsDataTable.DataTableRows(factorTable.mDTable);
                if (kwParams.ShowDialog() == DialogResult.OK)
                {
                    mclsKWpar = kwParams.clsKruskalWPar;

                    Add2AnalysisHTable(mclsKWpar, "Kruskal-Walis_Test");

                    m_BackgroundWorker.RunWorkerAsync(mclsKWpar.RCommand);
                    mProgressForm.Reset("Performing KW test ...");
                    mProgressForm.ShowDialog();
                }
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_KW;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion
        }

        private void menuItemWilcox_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!t_testPossible() || !mhtDatasets.ContainsKey("Factors"))
            {
                MessageBox.Show("There is not a single factor with exactly two levels; " +
                                Environment.NewLine + " cannot perform the Wilcoxon Test.", "No suitable factors",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Wilcox;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsWilcoxPar = new clsWilcoxonPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsWilcoxPar.RDataset = RDataset;
            mclsWilcoxPar.mstrDatasetName = selectedNodeTag.DataText;
            var factorTable = mhtDatasets["Factors"];
            clsDataTable.DataTableRows(factorTable.mDTable);

            if (!ValidateDataMatrixTableSelected(selectedNodeTag, true))
            {
                return;
            }

            var wilcoxParams = new frmWilcoxonPar(mclsWilcoxPar);
            if (factorTable.mDTable != null)
            {
                wilcoxParams.FactorList = marrFactorInfo;
                wilcoxParams.PopulateListBox = clsDataTable.DataTableRows(factorTable.mDTable);

                if (wilcoxParams.ShowDialog() == DialogResult.OK)
                {
                    mclsWilcoxPar = wilcoxParams.clsWilcoxonPar;

                    Add2AnalysisHTable(mclsWilcoxPar, "Wilcoxon_Test");

                    m_BackgroundWorker.RunWorkerAsync(mclsWilcoxPar.RCommand);
                    mProgressForm.Reset("Performing Wilcoxon test ...");
                    mProgressForm.ShowDialog();
                }
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Wilcox;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion
        }

        private void menuItemShapiroWilks_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_ShapiroWilks;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsShapiroWilksPar = new clsShapiroWilksPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsShapiroWilksPar.RDataset = RDataset;
            mclsShapiroWilksPar.mstrDatasetName = selectedNodeTag.DataText;

            if (!ValidateDataMatrixTableSelected(selectedNodeTag, true))
            {
                return;
            }

            var shapiroWilksParams = new frmShapiroWilksPar(mclsShapiroWilksPar);
            if (shapiroWilksParams.ShowDialog() == DialogResult.OK)
            {
                mclsShapiroWilksPar = shapiroWilksParams.clsShapiroWilksPar;

                Add2AnalysisHTable(mclsShapiroWilksPar, "Shapiro-Wilks_Test");

                m_BackgroundWorker.RunWorkerAsync(mclsShapiroWilksPar.RCommand);
                mProgressForm.Reset("Performing Shapiro-Wilks test ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_ShapiroWilks;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion
        }

        private void mnuItemFC_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("calculate fold changes"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("compute fold change"))
            {
                return;
            }

            mclsFoldChangePar = new clsFoldChangePar();
            RDataset = selectedNodeTag.RDatasetName;
            mclsFoldChangePar.RDataset = RDataset;
            mclsFoldChangePar.mstrDatasetName = selectedNodeTag.DataText;
            mclsFoldChangePar.marrFactors = marrFactorInfo;

            var foldChangeParams = new frmFoldChangePar(mclsFoldChangePar);

            if (foldChangeParams.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            mclsFoldChangePar = foldChangeParams.clsFoldChangePar;
            Add2AnalysisHTable(mclsFoldChangePar, "FoldChanges");

            try
            {
                mRConnector.EvaluateNoReturn(mclsFoldChangePar.RCommand);
                if (mRConnector.GetTableFromRmatrix("foldChanges"))
                {
                    var foldChangeData = mRConnector.DataTable.Copy();
                    foldChangeData.TableName = "foldChanges";
                    AddDataset2HashTable(foldChangeData);
                    AddDataNode(mhtDatasets["Fold Changes"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }
        }

        private void menuItemOneSampleTtest_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Ttest;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsOneSampleTtestPar = new clsOneSampleTtestPar();
            RDataset = selectedNodeTag.RDatasetName;

            mclsOneSampleTtestPar.RDataset = RDataset;
            mclsOneSampleTtestPar.mstrDatasetName = selectedNodeTag.DataText;

            if (!ValidateDataMatrixTableSelected(selectedNodeTag, true))
            {
                return;
            }

            var tTestParams = new frmOneSampleTtestPar(mclsOneSampleTtestPar);
            if (tTestParams.ShowDialog() == DialogResult.OK)
            {
                mclsOneSampleTtestPar = tTestParams.clsTtestPar;

                Add2AnalysisHTable(mclsOneSampleTtestPar, "T_Test");

                m_BackgroundWorker.RunWorkerAsync(mclsOneSampleTtestPar.RCommand);
                mProgressForm.Reset("Performing One Sample T-test ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Ttest;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion
        }

        private void contextMenuItemFilter_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (mExpressionsTab == null || selectedNodeTag?.mDTable == null)
            {
                MessageBox.Show("Data not loaded", "Nothing to do");
                return;
            }

            var selectedRowData = new StringBuilder();
            selectedRowData.Append("c(");

            var currGrid = ((ucDataGridView)mExpressionsTab.Controls[0]).TableGrid;
            var selectedRows = currGrid.SelectedRows;

            if (selectedRows.Count < 2)
            {
                MessageBox.Show("At least two rows of data must be selected in order to filter using Row IDs",
                                "Not enough rows");
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

            var rowFilterParams = new frmFilterBasedOnRowIDs
            {
                PopulateDataComboBox = AvailableDataSources()
            };

            if (rowFilterParams.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var datasetNameInR = (mhtDatasets[rowFilterParams.Dataset]).RDatasetName;

            mintFilterTblNum++;
            var filtTableName = "filteredData" + mintFilterTblNum;
            var rcmd = filtTableName + "<- filterOnRowIds(" + datasetNameInR + "," + selectedRowData + ")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                if (mRConnector.GetTableFromRmatrix(filtTableName))
                {
                    var filteredData = mRConnector.DataTable.Copy();

                    filteredData.TableName = filtTableName;
                    filteredData.Columns[0].ColumnName = "ID";
                    AddDataset2HashTable(filteredData);
                    if (mhtDatasets.ContainsKey("Filtered Data" + mintFilterTblNum.ToString()))
                        AddDataNode(mhtDatasets["Filtered Data" + mintFilterTblNum.ToString()]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool DoAnova(string rcmd)
        {
            bool anovaUsedAllData;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-anovaR$pvals");
                mRConnector.EvaluateNoReturn("notused<-anovaR$miss");
                mRConnector.EvaluateNoReturn("allused<-anovaR$allused");
                anovaUsedAllData = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var anovaParams = mRConnector.DataTable.Copy();
                anovaParams.TableName = "pvalues";
                anovaParams.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(anovaParams);
            }
            else
            {
                return false;
            }

            if (anovaUsedAllData)
                return true;

            mRConnector.GetTableFromRmatrix("notused");
            var anovaUnusedData = mRConnector.DataTable.Copy();
            anovaUnusedData.TableName = "notused";
            AddDataset2HashTable(anovaUnusedData);

            return true;
        }

        private bool DoTamuQ(string rcmd)
        {
            /*bool anovaUsedAllData = false;*/

            //rConnector.EvaluateNoReturn("a <- DoAnova");
            //rConnector.EvaluateNoReturn("a <- DoTamuQ");
            //rConnector.EvaluateNoReturn("pvalues<-1");
            //int a = 1;
            //rConnector.EvaluateNoReturn("pvalues<-DoAnova");

            try
            {
                //  rConnector.EvaluateNoReturn("pvalues<-DoAnova");

                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("yimputed<-tamuQ$y");
                mRConnector.EvaluateNoReturn("pvalues<-tamuQ$pvals");
                //rConnector.EvaluateNoReturn("notused<-TamuQ$miss");
                //rConnector.EvaluateNoReturn("allused<-TamuQ$allused");
                /*anovaUsedAllData = rConnector.GetSymbolAsBool("allused");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("yimputed"))
            {
                var yImputedData = mRConnector.DataTable.Copy();
                yImputedData.TableName = "yimputed";
                yImputedData.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(yImputedData);
            }
            else
            {
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var pValuesFromR = mRConnector.DataTable.Copy();
                pValuesFromR.TableName = "pvalues";
                pValuesFromR.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(pValuesFromR);
            }
            else
            {
                return false;
            }

            /*if (!anovaUsedAllData)
             {
                rConnector.GetTableFromRmatrix("notused");
                mDTamuQX = rConnector.DataTable.Copy();
                mDTamuQX.TableName = "notused";
                AddDataset2HashTable(mDTamuQX);
             } */

            return true;
        }

        private bool DoKWtest(string rcmd)
        {
            bool anovaUsedAllData;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-kwtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-kwtest$miss");
                mRConnector.EvaluateNoReturn("allused<-kwtest$allused");
                anovaUsedAllData = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var pValuesFromR = mRConnector.DataTable.Copy();
                pValuesFromR.TableName = "pvalues";
                pValuesFromR.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(pValuesFromR);
            }
            else
            {
                return false;
            }

            if (anovaUsedAllData)
                return true;

            mRConnector.GetTableFromRmatrix("notused");
            var kwUnusedData = mRConnector.DataTable.Copy();
            kwUnusedData.TableName = "notused";
            AddDataset2HashTable(kwUnusedData);

            return true;
        }

        private bool DoWilcoxtest(string rcmd)
        {
            bool anovaUsedAllData;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-wilcoxtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-wilcoxtest$miss");
                mRConnector.EvaluateNoReturn("allused<-wilcoxtest$allused");
                anovaUsedAllData = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var pValuesFromR = mRConnector.DataTable.Copy();
                pValuesFromR.TableName = "pvalues";
                pValuesFromR.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(pValuesFromR);
            }
            else
            {
                return false;
            }

            if (anovaUsedAllData)
                return true;

            mRConnector.GetTableFromRmatrix("notused");
            var wilcoxUnusedData = mRConnector.DataTable.Copy();
            wilcoxUnusedData.TableName = "notused";
            AddDataset2HashTable(wilcoxUnusedData);

            return true;
        }

        private bool DoShapiroWilkstest(string rcmd)
        {
            bool anovaUsedAllData;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-swtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-swtest$miss");
                mRConnector.EvaluateNoReturn("allused<-swtest$allused");
                anovaUsedAllData = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var pValuesFromR = mRConnector.DataTable.Copy();
                pValuesFromR.TableName = "pvalues";
                pValuesFromR.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(pValuesFromR);
            }
            else
            {
                return false;
            }

            if (anovaUsedAllData)
                return true;

            mRConnector.GetTableFromRmatrix("notused");
            var shapiroWilksUnusedData = mRConnector.DataTable.Copy();
            shapiroWilksUnusedData.TableName = "notused";
            AddDataset2HashTable(shapiroWilksUnusedData);

            return true;
        }

        private bool DoOneSampleTtest(string rcmd)
        {
            bool anovaUsedAllData;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-ttest$pvals");
                mRConnector.EvaluateNoReturn("notused<-ttest$miss");
                mRConnector.EvaluateNoReturn("allused<-ttest$allused");
                anovaUsedAllData = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                return false;
            }

            if (mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var pValuesFromR = mRConnector.DataTable.Copy();
                pValuesFromR.TableName = "pvalues";
                pValuesFromR.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(pValuesFromR);
            }
            else
            {
                return false;
            }

            if (anovaUsedAllData)
                return true;

            mRConnector.GetTableFromRmatrix("notused");
            var anovaUnusedData = mRConnector.DataTable.Copy();
            anovaUnusedData.TableName = "notused";
            AddDataset2HashTable(anovaUnusedData);

            return true;
        }

        #endregion
    }
}