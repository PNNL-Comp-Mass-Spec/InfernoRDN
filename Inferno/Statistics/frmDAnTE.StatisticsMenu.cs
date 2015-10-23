using System;
using System.ComponentModel;
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
        string Rdataset;

        #region Private Methods

        /// <summary>
        /// Extract Factor information from a datatable 
        /// to an arraylist of clsDatasetInfo type.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="factorsLoaded"></param>
        private void DatasetFactorInfo(DataTable dt, bool factorsLoaded)
        {
            var factorNames = clsDataTable.DataTableColumns(dt, true); // get data only columns
            var marrFactors = new List<string>();

            marrDatasetInfo.Clear();
            for (var i = 0; i < factorNames.Count; i++)
            {
                var dsetItem = new clsDatasetInfo(factorNames[i]);
                if (factorsLoaded)
                {

                    for (var k = 0; k < dt.Rows.Count; k++) // go thru each row
                    {
                        var mDrow = dt.Rows[k];
                        if (i == 0)
                        {
                            var factorname = mDrow.ItemArray[i].ToString();
                            marrFactors.Add(factorname);
                        }
                        var currFactor = new Factor(marrFactors[k], mDrow.ItemArray[i + 1].ToString());
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
            var mDataTable = new DataTable();

            var mDataColumn = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Factors"
            };
            mDataTable.Columns.Add(mDataColumn);

            foreach (var dataset in marrDatasetInfo)
            {
                mDataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = dataset.mstrDataSetName
                };
                mDataTable.Columns.Add(mDataColumn);
            }

            for (var i = 0; i < marrDatasetInfo[0].marrFactorAssnmnts.Count; i++)
            {
                var mDataRow = mDataTable.NewRow();
                mDataRow[0] = marrDatasetInfo[0].marrFactorAssnmnts[i].Name;
                for (var j = 0; j < marrDatasetInfo.Count; j++)
                {
                    mDataRow[j + 1] = marrDatasetInfo[j].marrFactorAssnmnts[i].Value;
                }
                mDataTable.Rows.Add(mDataRow);
            }
            return mDataTable;
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

        private void ChangeDatasetOrder(List<string> newNameOrder)
        {
            var marrOldOrder = new List<string>();
            var marr2Remove = new List<string>();

            if (!ValidateExpressionsLoaded("change dataset order"))
            {
                return;
            }

            var expTable = (mhtDatasets["Expressions"]).mDTable;
            for (var num = 1; num < expTable.Columns.Count; num++)
            {
                marrOldOrder.Add(expTable.Columns[num].ColumnName);
            }

            foreach (var item in marrOldOrder)
            {
                if (!newNameOrder.Contains(item))
                    marr2Remove.Add(item);
            }

            foreach (var strKey in mhtDatasets.Keys)
            {
                var currentNode = (mhtDatasets[strKey]);
                if (currentNode.mblIsNumeric)
                {
                    expTable = currentNode.mDTable;
                    foreach (var colName in marr2Remove)
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

        private void ChangeDatasetOrderR(List<int> newIndexOrder)
        {
            var mstrOrder = @"newOrder=""c(";
            var vars = "numvars=" + NumericVars2R();

            foreach (var index in newIndexOrder)
            {
                var currVal = index + 1;
                mstrOrder += currVal + ",";
            }
            mstrOrder = mstrOrder.Substring(0, mstrOrder.LastIndexOf(',')) + @")""";

            var rcmd = "arrangeColumns(" + vars + "," + mstrOrder + ")";
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
            
            var mstrFrom = sender.ToString();

            if (!mhtDatasets.ContainsKey("Factors"))
            {
                if (!ValidateExpressionsLoaded("define factors; load some data using File->Open"))
                {
                    return;
                }
            }

            var mfrmFactorInfo = new frmFactorInformation
            {
                DatasetInfo = marrDatasetInfo,
                FactorsLoaded = mhtDatasets.ContainsKey("Factors"),
                FactorInfo = marrFactorInfo
            };

            if (mstrFrom.Equals("Arrange Columns"))
            {
                mfrmFactorInfo.OrderChangeOnly = true;
                mfrmFactorInfo.Title = "Dataset Order";
                mfrmFactorInfo.SubTitle = "Change Dataset Order, Delete Datasets";
                mfrmFactorInfo.WinTitle = "Dataset Order";
            }
            if (mfrmFactorInfo.ShowDialog() == DialogResult.OK)
            {
                marrDatasetInfo = mfrmFactorInfo.DatasetInfo;
                if (mfrmFactorInfo.OrderChanged)
                {
                    ChangeDatasetOrder(mfrmFactorInfo.NewDatasetNameOrder);
                    ChangeDatasetOrderR(mfrmFactorInfo.NewDatasetOrder);
                }
                marrFactorInfo = mfrmFactorInfo.FactorInfo;
                if (marrFactorInfo.Count > 0)
                {
                    var mDTFactors = DatasetArr2DT();
                    mDTFactors.Columns[0].ColumnName = "Factors";
                    mDTFactors.TableName = "factors";
                    AddDataset2HashTable(mDTFactors);
                    if (mhtDatasets.ContainsKey("Factors"))
                    {
                        AddDataNode(mhtDatasets["Factors"]);
                    }

                    if (mRConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors))
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
            }
            else //User cancels the factor changes, so revert to previous.
            {
                marrDatasetInfo = tmpDatasets; //Should we do a DeepCopy here?
                marrFactorInfo = tmpFactors;
            }
        }

        private void menuItemANOVA_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use ANOVA"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_ANOVA;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;
            #endregion

            mclsAnovaPar = new clsAnovaPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsAnovaPar.tempFile = mRTempFilePath;
            mclsAnovaPar.Rdataset = Rdataset;
            mclsAnovaPar.mstrDatasetName = mclsSelected.mstrDataText;
            var mclsFactors = mhtDatasets["Factors"];
            mclsAnovaPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            var mfrmAnovaPar = new frmANOVApar(mclsAnovaPar);
            if (mclsFactors.mDTable != null)
            {
                mfrmAnovaPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                if (mfrmAnovaPar.ShowDialog() == DialogResult.OK)
                {
                    mclsAnovaPar = mfrmAnovaPar.clsAnovaPar;

                    Add2AnalysisHTable(mclsAnovaPar, "ANOVA");

                    m_BackgroundWorker.RunWorkerAsync(mclsAnovaPar.Rcmd);
                    mfrmShowProgress.Message = "Performing ANOVA ...";
                    mfrmShowProgress.ShowDialog();
                }
            }


            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_ANOVA;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;
            #endregion

        }

        private void menuItemTamuQ_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use TamuQ"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_TamuQ;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_TamuQCompleted;
            #endregion

            mclsTamuQPar = new clsTamuQPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsTamuQPar.tempFile = mRTempFilePath;
            mclsTamuQPar.Rdataset = Rdataset;
            mclsTamuQPar.mstrDatasetName = mclsSelected.mstrDataText;
            var mclsFactors = mhtDatasets["Factors"];
            mclsTamuQPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            var mfrmTamuQPar = new frmTamuQpar(mclsTamuQPar);
            if (mclsFactors.mDTable != null)
            {
                mfrmTamuQPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                if (mfrmTamuQPar.ShowDialog() == DialogResult.OK)
                {
                    mclsTamuQPar = mfrmTamuQPar.clsTamuQPar;

                    Add2AnalysisHTable(mclsAnovaPar, "TamuQ");

                    m_BackgroundWorker.RunWorkerAsync(mclsTamuQPar.Rcmd);
                    mfrmShowProgress.Message = "Performing TamuQ ...";
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_TamuQ;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_TamuQCompleted;
            #endregion

        }

        private void menuItemKW_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("use the Kruskal-Walis Test"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_KW;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;
            #endregion

            mclsKWpar = new clsKruskalWPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsKWpar.tempFile = mRTempFilePath;
            mclsKWpar.Rdataset = Rdataset;
            mclsKWpar.mstrDatasetName = mclsSelected.mstrDataText;
            var mclsFactors = mhtDatasets["Factors"];
            mclsKWpar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            var mfrmKWPar = new frmKruskalWpar(mclsKWpar);
            if (mclsFactors.mDTable != null)
            {
                mfrmKWPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                if (mfrmKWPar.ShowDialog() == DialogResult.OK)
                {
                    mclsKWpar = mfrmKWPar.clsKruskalWPar;

                    Add2AnalysisHTable(mclsKWpar, "Kruskal-Walis_Test");

                    m_BackgroundWorker.RunWorkerAsync(mclsKWpar.Rcmd);
                    mfrmShowProgress.Message = "Performing KW test ...";
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_KW;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;
            #endregion

        }

        private void menuItemWilcox_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!t_testPossible() || !mhtDatasets.ContainsKey("Factors"))
            {
                MessageBox.Show("There is not a single factor with exactly two levels; " +
                                Environment.NewLine + " cannot perform the Wilcoxon Test.", "No suitable factors",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Wilcox;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;

            #endregion

            mclsWilcoxPar = new clsWilcoxonPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsWilcoxPar.tempFile = mRTempFilePath;
            mclsWilcoxPar.Rdataset = Rdataset;
            mclsWilcoxPar.mstrDatasetName = mclsSelected.mstrDataText;
            var mclsFactors = mhtDatasets["Factors"];
            mclsWilcoxPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

            if (!ValidateDataMatrixTableSelected(mclsSelected, true))
            {
                return;
            }

            var mfrmWilcoxPar = new frmWilcoxonPar(mclsWilcoxPar);
            if (mclsFactors.mDTable != null)
            {
                mfrmWilcoxPar.PopulateListBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                mfrmWilcoxPar.FactorList = marrFactorInfo;
                if (mfrmWilcoxPar.ShowDialog() == DialogResult.OK)
                {
                    mclsWilcoxPar = mfrmWilcoxPar.clsWilcoxonPar;

                    Add2AnalysisHTable(mclsWilcoxPar, "Wilcoxon_Test");

                    m_BackgroundWorker.RunWorkerAsync(mclsWilcoxPar.Rcmd);
                    mfrmShowProgress.Message = "Performing Wilcoxon test ...";
                    mfrmShowProgress.ShowDialog();
                }
            }


            #region Unhook Threading Events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Wilcox;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;

            #endregion

        }

        private void menuItemShapiroWilks_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_ShapiroWilks;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;
            #endregion

            mclsShapiroWilksPar = new clsShapiroWilksPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsShapiroWilksPar.Rdataset = Rdataset;
            mclsShapiroWilksPar.mstrDatasetName = mclsSelected.mstrDataText;

            if (!ValidateDataMatrixTableSelected(mclsSelected, true))
            {
                return;
            }

            var mfrmShapiroWilksPar = new frmShapiroWilksPar(mclsShapiroWilksPar);
            if (mfrmShapiroWilksPar.ShowDialog() == DialogResult.OK)
            {
                mclsShapiroWilksPar = mfrmShapiroWilksPar.clsShapiroWilksPar;

                Add2AnalysisHTable(mclsShapiroWilksPar, "Shapiro-Wilks_Test");

                m_BackgroundWorker.RunWorkerAsync(mclsShapiroWilksPar.Rcmd);
                mfrmShowProgress.Message = "Performing Shapiro-Wilks test ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_ShapiroWilks;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;
            #endregion

        }

        private void mnuItemFC_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateFactorsDefined("Calculate Fold Changes"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("compute fold change"))
            {
                return;
            }

            mclsFoldChangePar = new clsFoldChangePar();
            Rdataset = mclsSelected.mstrRdatasetName;
            mclsFoldChangePar.Rdataset = Rdataset;
            mclsFoldChangePar.mstrDatasetName = mclsSelected.mstrDataText;
            mclsFoldChangePar.marrFactors = marrFactorInfo;

            var mfrmFC = new frmFoldChangePar(mclsFoldChangePar);

            if (mfrmFC.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            mclsFoldChangePar = mfrmFC.clsFoldChangePar;
            Add2AnalysisHTable(mclsFoldChangePar, "FoldChanges");

            try
            {
                mRConnector.EvaluateNoReturn(mclsFoldChangePar.Rcmd);
                if (mRConnector.GetTableFromRmatrix("foldChanges"))
                {
                    var mDTFoldChange = mRConnector.DataTable.Copy();
                    if (mDTFoldChange != null)
                    {
                        mDTFoldChange.TableName = "foldChanges";
                        AddDataset2HashTable(mDTFoldChange);
                        AddDataNode(mhtDatasets["Fold Changes"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }
        }

        private void menuItemOneSampleTtest_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading Events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_Ttest;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ANOVACompleted;
            #endregion

            mclsOneSampleTtestPar = new clsOneSampleTtestPar();
            Rdataset = mclsSelected.mstrRdatasetName;

            mclsOneSampleTtestPar.Rdataset = Rdataset;
            mclsOneSampleTtestPar.mstrDatasetName = mclsSelected.mstrDataText;

            if (!ValidateDataMatrixTableSelected(mclsSelected, true))
            {
                return;
            }

            var mfrmTtestPar = new frmOneSampleTtestPar(mclsOneSampleTtestPar);
            if (mfrmTtestPar.ShowDialog() == DialogResult.OK)
            {
                mclsOneSampleTtestPar = mfrmTtestPar.clsTtestPar;

                Add2AnalysisHTable(mclsOneSampleTtestPar, "T_Test");

                m_BackgroundWorker.RunWorkerAsync(mclsOneSampleTtestPar.Rcmd);
                mfrmShowProgress.Message = "Performing One Sample T-test ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading Events
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Ttest;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ANOVACompleted;
            #endregion
        }

        private void ctxtMnuItemFilter_Click(object sender, EventArgs e)
        {

            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (ctltabPage == null || mclsSelected == null || mclsSelected.mDTable == null)
            {
                MessageBox.Show("Data not loaded", "Nothing to do");
                return;
            }

            var selectedRowData = new StringBuilder();
            selectedRowData.Append("c(");

            var currGrid = ((ucDataGridView)this.ctltabPage.Controls[0]).TableGrid;
            var selectedRows = currGrid.SelectedRows;

            if (selectedRows.Count < 2)
            {
                MessageBox.Show("At least two rows of data must be selected in order to filter using Row IDs", "Not enough rows");
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

            var mfrmRowFilter = new frmFilterBasedOnRowIDs
            {
                PopulateDataComboBox = AvailableDataSources()
            };

            if (mfrmRowFilter.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var datasetNameInR = (mhtDatasets[mfrmRowFilter.Dataset]).mstrRdatasetName;

            mintFilterTblNum++;
            var filtTableName = "filteredData" + mintFilterTblNum;
            var rcmd = filtTableName + "<- filterOnRowIds(" + datasetNameInR + "," + selectedRowData + ")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                if (mRConnector.GetTableFromRmatrix(filtTableName))
                {
                    var mDTfiltered = mRConnector.DataTable.Copy();
                    
                    mDTfiltered.TableName = filtTableName;
                    mDTfiltered.Columns[0].ColumnName = "ID";
                    AddDataset2HashTable(mDTfiltered);
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
            var mblAnovaAllUsed = false;
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-anovaR$pvals");
                mRConnector.EvaluateNoReturn("notused<-anovaR$miss");
                mRConnector.EvaluateNoReturn("allused<-anovaR$allused");
                mblAnovaAllUsed = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTanovaP = mRConnector.DataTable.Copy();
                mDTanovaP.TableName = "pvalues";
                mDTanovaP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTanovaP);
            }
            else
                success = false;
            if (success)
            {
                if (!mblAnovaAllUsed)
                {
                    mRConnector.GetTableFromRmatrix("notused");
                    var mDTanovaX = mRConnector.DataTable.Copy();
                    mDTanovaX.TableName = "notused";
                    AddDataset2HashTable(mDTanovaX);
                }
            }
            else
                success = false;
            return success;
        }

        private bool DoTamuQ(string rcmd)
        {

            /*bool mblAnovaAllUsed = false;*/
            var success = true;

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
                /*mblAnovaAllUsed = rConnector.GetSymbolAsBool("allused");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("yimputed"))
            {
                var mDTamuQYImputed = mRConnector.DataTable.Copy();
                mDTamuQYImputed.TableName = "yimputed";
                mDTamuQYImputed.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTamuQYImputed);
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTamuQP = mRConnector.DataTable.Copy();
                mDTamuQP.TableName = "pvalues";
                mDTamuQP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTamuQP);
            }
            else
                success = false;
            if (success)
            {
                /*if (!mblAnovaAllUsed)
                {
                    rConnector.GetTableFromRmatrix("notused");
                    mDTamuQX = rConnector.DataTable.Copy();
                    mDTamuQX.TableName = "notused";
                    AddDataset2HashTable(mDTamuQX);
                 } */
            }
            else
                success = false;
            return success;
        }

        private bool DoKWtest(string rcmd)
        {
            var mblAnovaAllUsed = false;
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-kwtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-kwtest$miss");
                mRConnector.EvaluateNoReturn("allused<-kwtest$allused");
                mblAnovaAllUsed = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTanovaP = mRConnector.DataTable.Copy();
                mDTanovaP.TableName = "pvalues";
                mDTanovaP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTanovaP);
            }
            else
                success = false;
            if (success)
            {
                if (!mblAnovaAllUsed)
                {
                    mRConnector.GetTableFromRmatrix("notused");
                    var mDTanovaX = mRConnector.DataTable.Copy();
                    mDTanovaX.TableName = "notused";
                    AddDataset2HashTable(mDTanovaX);
                }
            }
            else
                success = false;
            return success;
        }

        private bool DoWilcoxtest(string rcmd)
        {
            var mblAnovaAllUsed = false;
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-wilcoxtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-wilcoxtest$miss");
                mRConnector.EvaluateNoReturn("allused<-wilcoxtest$allused");
                mblAnovaAllUsed = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTanovaP = mRConnector.DataTable.Copy();
                mDTanovaP.TableName = "pvalues";
                mDTanovaP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTanovaP);
            }
            else
                success = false;
            if (success)
            {
                if (!mblAnovaAllUsed)
                {
                    mRConnector.GetTableFromRmatrix("notused");
                    var mDTanovaX = mRConnector.DataTable.Copy();
                    mDTanovaX.TableName = "notused";
                    AddDataset2HashTable(mDTanovaX);
                }
            }
            else
                success = false;
            return success;
        }

        private bool DoShapiroWilkstest(string rcmd)
        {
            var mblAnovaAllUsed = false;
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-swtest$pvals");
                mRConnector.EvaluateNoReturn("notused<-swtest$miss");
                mRConnector.EvaluateNoReturn("allused<-swtest$allused");
                mblAnovaAllUsed = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTanovaP = mRConnector.DataTable.Copy();
                mDTanovaP.TableName = "pvalues";
                mDTanovaP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTanovaP);
            }
            else
                success = false;
            if (success)
            {
                if (!mblAnovaAllUsed)
                {
                    mRConnector.GetTableFromRmatrix("notused");
                    var mDTanovaX = mRConnector.DataTable.Copy();
                    mDTanovaX.TableName = "notused";
                    AddDataset2HashTable(mDTanovaX);
                }
            }
            else
                success = false;
            return success;
        }

        private bool DoOneSampleTtest(string rcmd)
        {
            var mblAnovaAllUsed = false;
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("pvalues<-ttest$pvals");
                mRConnector.EvaluateNoReturn("notused<-ttest$miss");
                mRConnector.EvaluateNoReturn("allused<-ttest$allused");
                mblAnovaAllUsed = mRConnector.GetSymbolAsBool("allused");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            if (success && mRConnector.GetTableFromRmatrix("pvalues"))
            {
                var mDTanovaP = mRConnector.DataTable.Copy();
                mDTanovaP.TableName = "pvalues";
                mDTanovaP.Columns[0].ColumnName = "ID";
                AddDataset2HashTable(mDTanovaP);
            }
            else
                success = false;
            if (success)
            {
                if (!mblAnovaAllUsed)
                {
                    mRConnector.GetTableFromRmatrix("notused");
                    var mDTanovaX = mRConnector.DataTable.Copy();
                    mDTanovaX.TableName = "notused";
                    AddDataset2HashTable(mDTanovaX);
                }
            }
            else
                success = false;
            return success;
        }
        #endregion
    }
}