using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DAnTE.ExtraControls;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    public enum enmDataType
    {
        /// <summary>
        /// Flat file of expression data (expression set)
        /// </summary>
        ESET,

        /// <summary>
        /// Protein metadata
        /// </summary>
        PROTINFO,

        /// <summary>
        /// Dataset factors
        /// </summary>
        FACTORS
    };

    partial class frmDAnTE
    {
        public string LastSessionLoadError { get; private set; }

        private void AddDataset2HashTable(DataTable dt)
        {
            var dataTableNameInR = dt.TableName;
            var datasetNode = new clsDatasetTreeNode(dt);

            switch (dataTableNameInR)
            {
                case "Eset":

                    #region Eset

                    datasetNode.TNode = new TreeNode("Expressions", 1, 2);
                    datasetNode.DataText = "Expressions";
                    datasetNode.Message = "Expression values selected.";
                    datasetNode.RDatasetName = "Eset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Expressions"))
                        mhtDatasets["Expressions"] = datasetNode;
                    else
                        mhtDatasets.Add("Expressions", datasetNode);

                    if (!mhtDatasets.ContainsKey("Factors"))
                        DatasetFactorInfo(dt, false);
                    break;

                    #endregion

                case "ProtInfo":

                    #region ProtInfo

                    datasetNode.TNode = new TreeNode("Protein Info", 3, 4);
                    datasetNode.DataText = "Protein Info";
                    datasetNode.Message = "Protein Info selected.";
                    datasetNode.RDatasetName = "ProtInfo";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Protein Info"))
                        mhtDatasets["Protein Info"] = datasetNode;
                    else
                        mhtDatasets.Add("Protein Info", datasetNode);
                    break;

                    #endregion

                case "factors":

                    #region Factors

                    datasetNode.TNode = new TreeNode("Factors", 3, 4);
                    datasetNode.DataText = "Factors";
                    datasetNode.Message = "Factors selected.";
                    datasetNode.RDatasetName = "factors";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Factors"))
                        mhtDatasets["Factors"] = datasetNode;
                    else
                        mhtDatasets.Add("Factors", datasetNode);
                    marrDatasetInfo.Clear();
                    DatasetFactorInfo(dt, true);
                    break;

                    #endregion

                case "logEset":

                    #region logEset

                    datasetNode.TNode = new TreeNode("Log Expressions", 1, 2);
                    datasetNode.DataText = "Log Expressions";
                    datasetNode.Message = "Log Expressions selected.";
                    datasetNode.RDatasetName = "logEset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Log Expressions"))
                        mhtDatasets["Log Expressions"] = datasetNode;
                    else
                        mhtDatasets.Add("Log Expressions", datasetNode);
                    break;

                    #endregion

                case "loessData":

                    #region loessData

                    datasetNode.TNode = new TreeNode("LOESS Data", 1, 2);
                    datasetNode.DataText = "LOESS Data";
                    datasetNode.Message = "LOESS Data selected.";
                    datasetNode.RDatasetName = "loessData";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("LOESS Data"))
                        mhtDatasets["LOESS Data"] = datasetNode;
                    else
                        mhtDatasets.Add("LOESS Data", datasetNode);
                    break;

                    #endregion

                case "quaNormEset":

                    #region quaNormEset

                    datasetNode.TNode = new TreeNode("Quantile Normalized", 1, 2);
                    datasetNode.DataText = "Quantile Normalized";
                    datasetNode.Message = "Quantile normalized data selected.";
                    datasetNode.RDatasetName = "quaNormEset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Quantile Normalized"))
                        mhtDatasets["Quantile Normalized"] = datasetNode;
                    else
                        mhtDatasets.Add("Quantile Normalized", datasetNode);
                    break;

                    #endregion

                case "meanCEset":

                    #region MeanCEset

                    datasetNode.TNode = new TreeNode("Mean Centered", 1, 2);
                    datasetNode.DataText = "Mean Centered";
                    datasetNode.Message = "Mean centered data selected.";
                    datasetNode.RDatasetName = "meanCEset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Mean Centered"))
                        mhtDatasets["Mean Centered"] = datasetNode;
                    else
                        mhtDatasets.Add("Mean Centered", datasetNode);
                    break;

                    #endregion

                case "medianCEset":

                    #region MedianCEset

                    datasetNode.TNode = new TreeNode("Median Centered", 1, 2);
                    datasetNode.DataText = "Median Centered";
                    datasetNode.Message = "Median centered data selected.";
                    datasetNode.RDatasetName = "medianCEset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Median Centered"))
                        mhtDatasets["Median Centered"] = datasetNode;
                    else
                        mhtDatasets.Add("Median Centered", datasetNode);
                    break;

                    #endregion

                case "madEset":

                    #region madEset

                    datasetNode.TNode = new TreeNode("MAD Adjusted", 1, 2);
                    datasetNode.DataText = "MAD Adjusted";
                    datasetNode.Message = "MAD Adjusted data selected.";
                    datasetNode.RDatasetName = "madEset";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("MAD Adjusted"))
                        mhtDatasets["MAD Adjusted"] = datasetNode;
                    else
                        mhtDatasets.Add("MAD Adjusted", datasetNode);
                    break;

                    #endregion

                case "linregData":

                    #region linregData

                    datasetNode.TNode = new TreeNode("Linear Regressed", 1, 2);
                    datasetNode.DataText = "Linear Regressed";
                    datasetNode.Message = "Linear Regressed Data selected.";
                    datasetNode.RDatasetName = "linregData";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Linear Regressed"))
                        mhtDatasets["Linear Regressed"] = datasetNode;
                    else
                        mhtDatasets.Add("Linear Regressed", datasetNode);
                    break;

                    #endregion

                case "imputedData":

                    #region imputedData

                    datasetNode.TNode = new TreeNode("Imputed Data", 1, 2);
                    datasetNode.DataText = "Imputed Data";
                    datasetNode.Message = "Imputed Data selected.";
                    datasetNode.RDatasetName = "imputedData";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    if (mhtDatasets.ContainsKey("Imputed Data"))
                        mhtDatasets["Imputed Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Imputed Data", datasetNode);
                    break;

                    #endregion

                case "mergedData":

                    #region mergedData

                    datasetNode.TNode = new TreeNode("Merged Data", 1, 2);
                    datasetNode.DataText = "Merged Data";
                    datasetNode.Message = "Merged Data selected.";
                    datasetNode.RDatasetName = "mergedData";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    datasetNode.IsNumeric = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Merged Data"))
                        mhtDatasets["Merged Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Merged Data", datasetNode);
                    break;

                    #endregion

                case "pData11":

                    #region pData11 (RRollup)

                    datasetNode.TNode = new TreeNode("RRollup", 1, 2);
                    datasetNode.DataText = "RRollup";
                    datasetNode.Message = "RRollup selected.";
                    datasetNode.RDatasetName = "pData11";
                    datasetNode.RProteinDatasetName = @"pScaled1"",""pData1";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("RRollup"))
                        mhtDatasets["RRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("RRollup", datasetNode);
                    break;

                    #endregion

                case "sData1":

                    #region sData1 (RRollup)

                    datasetNode.TNode = new TreeNode("ScaledData", 1, 2);
                    datasetNode.DataText = "ScaledData";
                    datasetNode.Message = "Scaled data selected.";
                    datasetNode.RDatasetName = "sData1";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    datasetNode.ParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("ScaledData"))
                        mhtDatasets["ScaledData"] = datasetNode;
                    else
                        mhtDatasets.Add("ScaledData", datasetNode);
                    break;

                    #endregion

                case "orData1":

                    #region orData1 (RRollup)

                    datasetNode.TNode = new TreeNode("OutliersRemoved", 1, 2);
                    datasetNode.DataText = "OutliersRemoved";
                    datasetNode.Message = "Outliers removed data selected.";
                    datasetNode.RDatasetName = "orData1";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    datasetNode.ParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("OutliersRemoved"))
                        mhtDatasets["OutliersRemoved"] = datasetNode;
                    else
                        mhtDatasets.Add("OutliersRemoved", datasetNode);
                    break;

                    #endregion

                case "pData22":

                    #region pData2 (ZRollup)

                    datasetNode.TNode = new TreeNode("ZRollup", 1, 2);
                    datasetNode.DataText = "ZRollup";
                    datasetNode.Message = "ZRollup selected.";
                    datasetNode.RDatasetName = "pData22";
                    datasetNode.RProteinDatasetName = @"pScaled2"",""pData2";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("ZRollup"))
                        mhtDatasets["ZRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("ZRollup", datasetNode);
                    break;

                    #endregion

                case "qrollupP1":

                    #region QRollupP1

                    datasetNode.TNode = new TreeNode("QRollup", 1, 2);
                    datasetNode.DataText = "QRollup";
                    datasetNode.Message = "QRollup selected.";
                    datasetNode.RDatasetName = "qrollupP1";
                    datasetNode.RProteinDatasetName = "qrollupP";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("QRollup"))
                        mhtDatasets["QRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("QRollup", datasetNode);
                    break;

                    #endregion

                case "PCAweights":

                    #region PCA weights

                    datasetNode.TNode = new TreeNode("PCA Weights", 3, 4);
                    datasetNode.DataText = "PCA Weights";
                    datasetNode.Message = "PCA Weights selected.";
                    datasetNode.RDatasetName = "PCAweights";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("PCA Weights"))
                        mhtDatasets["PCA Weights"] = datasetNode;
                    else
                        mhtDatasets.Add("PCA Weights", datasetNode);
                    break;

                    #endregion

                case "PLSweights":

                    #region PCA weights

                    datasetNode.TNode = new TreeNode("PLS Weights", 3, 4);
                    datasetNode.DataText = "PLS Weights";
                    datasetNode.Message = "PLS Weights selected.";
                    datasetNode.RDatasetName = "PLSweights";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("PLS Weights"))
                        mhtDatasets["PLS Weights"] = datasetNode;
                    else
                        mhtDatasets.Add("PLS Weights", datasetNode);
                    break;

                    #endregion

                case "clusterResults":

                    #region PCA weights

                    datasetNode.TNode = new TreeNode("Heatmap Clusters", 3, 4);
                    datasetNode.DataText = "Heatmap Clusters";
                    datasetNode.Message = "Heatmap Clusters selected.";
                    datasetNode.RDatasetName = "clusterResults";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Heatmap Clusters"))
                        mhtDatasets["Heatmap Clusters"] = datasetNode;
                    else
                        mhtDatasets.Add("Heatmap Clusters", datasetNode);
                    break;

                    #endregion

                case "pvalues":

                    #region p-values

                    datasetNode.TNode = new TreeNode("p-Values", 3, 4);
                    datasetNode.DataText = "p-Values";
                    datasetNode.Message = "p-Values selected.";
                    datasetNode.RDatasetName = "pvalues";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("p-Values"))
                        mhtDatasets["p-Values"] = datasetNode;
                    else
                        mhtDatasets.Add("p-Values", datasetNode);
                    break;

                    #endregion

                case "yimputed":

                    #region yimputed

                    datasetNode.TNode = new TreeNode("Imputed Values", 1, 2);
                    datasetNode.DataText = "Imputed Values";
                    datasetNode.Message = "Imputed Values selected.";
                    datasetNode.RDatasetName = "yimputed";
                    datasetNode.AddDGridContextMenu = true;
                    datasetNode.AddTVContextMenu = true;
                    datasetNode.IsNumeric = true;
                    datasetNode.IsPlotTable = true;
                    datasetNode.RollupPossible = true;
                    if (mhtDatasets.ContainsKey("Imputed Values"))
                        mhtDatasets["Imputed Values"] = datasetNode;
                    else
                        mhtDatasets.Add("Imputed Values", datasetNode);
                    break;

                    #endregion

                case "notused":

                    #region Unused data for ANOVA

                    datasetNode.TNode = new TreeNode("Unused Data", 3, 4);
                    datasetNode.DataText = "Unused Data";
                    datasetNode.Message = "Unused Data selected.";
                    datasetNode.RDatasetName = "notused";
                    datasetNode.ParentNode = "p-Values";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = true;
                    datasetNode.IsPlotTable = true;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Unused Data"))
                        mhtDatasets["Unused Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Unused Data", datasetNode);
                    break;

                    #endregion

                case "foldChanges":

                    #region Fold Changes

                    datasetNode.TNode = new TreeNode("Fold Changes", 3, 4);
                    datasetNode.DataText = "Fold Changes";
                    datasetNode.Message = "Fold Changes selected.";
                    datasetNode.RDatasetName = "foldChanges";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Fold Changes"))
                        mhtDatasets["Fold Changes"] = datasetNode;
                    else
                        mhtDatasets.Add("Fold Changes", datasetNode);
                    break;

                    #endregion

                case "patternData":

                    #region Pattern Search

                    datasetNode.TNode = new TreeNode("Pattern Corr", 3, 4);
                    datasetNode.DataText = "Pattern Corr";
                    datasetNode.Message = "Pattern Correlations selected.";
                    datasetNode.RDatasetName = "patternData";
                    datasetNode.AddDGridContextMenu = false;
                    datasetNode.AddTVContextMenu = false;
                    datasetNode.IsNumeric = false;
                    datasetNode.IsPlotTable = false;
                    datasetNode.RollupPossible = false;
                    if (mhtDatasets.ContainsKey("Pattern Corr"))
                        mhtDatasets["Pattern Corr"] = datasetNode;
                    else
                        mhtDatasets.Add("Pattern Corr", datasetNode);
                    break;

                    #endregion

                default:

                    #region All other tables (filteredData)

                    if (dataTableNameInR.Contains("filteredData"))
                    {
                        var setNum = dataTableNameInR.Substring(12);
                        var nodeTxt = "Filtered Data" + setNum;
                        datasetNode.TNode = new TreeNode(nodeTxt, 1, 2);
                        datasetNode.DataText = nodeTxt;
                        datasetNode.Message = "Filtered Data selected.";
                        datasetNode.RDatasetName = dataTableNameInR;
                        datasetNode.AddDGridContextMenu = true;
                        datasetNode.AddTVContextMenu = false;
                        if (mhtDatasets.ContainsKey(nodeTxt))
                            mhtDatasets[nodeTxt] = datasetNode;
                        else
                            mhtDatasets.Add(nodeTxt, datasetNode);
                    }
                    break;

                    #endregion
            }
        }

        /// <summary>
        /// Opens a previously saved session in *.dnt file
        /// There are few special cases (reading a vector, non numeric etc).
        /// But most of the tables are in R matrices (default section in the switch statement).
        /// </summary>
        /// <param name="dataFilePath"></param>
        /// <returns>True if success, false if an error</returns>
        private bool OpenSession(string dataFilePath)
        {
            object vars;
            var success = true;

            mhtDatasets.Clear();

            LastSessionLoadError = string.Empty;

            var fiDatafile = new FileInfo(dataFilePath);
            if (!fiDatafile.Exists)
            {
                LastSessionLoadError = "File not found";
                MessageBox.Show("File not found: " + dataFilePath, "Missing File");
                return false;
            }

            var rCmd = "load(file=\"" + dataFilePath.Replace("\\", "/") + "\")";
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                vars = mRConnector.GetSymbolAsStrings("vars");
                mRConnector.EvaluateNoReturn("print(vars)");
            }
            catch (Exception ex)
            {
                LastSessionLoadError = ex.Message;
                if (!LastSessionLoadError.StartsWith("Value cannot be null", StringComparison.OrdinalIgnoreCase))
                    MessageBox.Show("R access failed loading file " + fiDatafile.FullName + ": " + ex.Message, "Error!");

                return false;
            }

            var variables = (string[])vars;

            for (var i = 1; i < variables.Length; i++)
            {
                try
                {
                    Console.WriteLine("frmDAnTE.OpenSession:{0} -> {1}", "", variables[i]);

                    var dataToAdd = new DataTable();

                    switch (variables[i])
                    {
                        case ("ProtInfo"):

                            #region ProtInfo

                            if ((dataToAdd = GetProtInfoMatrix()) != null)
                                dataToAdd.TableName = "ProtInfo";
                            else
                                success = false;
                            break;

                            #endregion

                        case ("factors"):

                            #region Factors

                            if (mRConnector.GetTableFromRmatrixNonNumeric("factors"))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.Columns[0].ColumnName = "Factors";
                                dataToAdd.TableName = "factors";
                                DatasetFactorInfo(dataToAdd, true);
                                UpdateFactorInfoArray();
                            }
                            else
                                success = false;
                            break;

                            #endregion

                        case ("pData1"):

                            #region RRollup

                            mRConnector.EvaluateNoReturn("pData1 <- pScaled1$pData");
                            if (mRConnector.GetTableFromRproteinMatrix("pData1"))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("pData11 <- pData1[,-c(1,2)]");
                                dataToAdd.TableName = "pData11";
                            }
                            else
                                success = false;

                            #endregion

                            break;
                        case ("pData2"):

                            #region ZRollup

                            mRConnector.EvaluateNoReturn("pData2 <- pScaled2$pData");
                            if (mRConnector.GetTableFromRproteinMatrix("pData2"))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("pData22 <- pData2[,-c(1,2)]");
                                dataToAdd.TableName = "pData22";
                            }

                            #endregion

                            break;
                        case ("qrollupP"):

                            #region Qrollup

                            if (mRConnector.GetTableFromRproteinMatrix("qrollupP"))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("qrollupP1 <- qrollupP[,-c(1,2)]");
                                dataToAdd.TableName = "qrollupP1";
                            }
                            else
                                success = false;

                            #endregion

                            break;
                        case ("clusterResults"):

                            #region Cluster results

                            if (mRConnector.GetTableFromRvector("clusterResults"))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.TableName = "clusterResults";
                            }
                            else
                                success = false;

                            #endregion

                            break;
                        case "pScaled1": // R list variable that holds RRollup information
                            dataToAdd = null;
                            break;
                        case "pScaled2": // R list variable that holds ZRollup information
                            dataToAdd = null;
                            break;
                        default:

                            #region Everything else

                            if (mRConnector.GetTableFromRmatrix(variables[i]))
                            {
                                dataToAdd = mRConnector.DataTable.Copy();
                                dataToAdd.TableName = variables[i];
                            }
                            else
                                success = false;

                            #endregion

                            break;
                    }
                    if (dataToAdd != null)
                    {
                        AddDataset2HashTable(dataToAdd);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("R access failed: " + ex.Message, "Error!");
                    success = false;
                }
            }
            return success;
        }


        /// <summary>
        /// Add a node to the TreeView
        /// Node Tag points to all the information related to that node
        /// in a class of type 'clsDatasetTreeNode'
        /// </summary>
        /// <param name="mdataNode"></param>
        private void AddDataNode(clsDatasetTreeNode dataNodeToAdd)
        {
            TreeNode tn;
            if ((mDataTab.Controls.Count == 0) &&
                (mhtDatasets.ContainsKey("Expressions") ||
                 mhtDatasets.ContainsKey("Protein Info") ||
                 mhtDatasets.ContainsKey("Factors")))
            {
                #region Add Controls on tab page

                var dataGridTab = new ucDataGridView();
                mExpressionsTab = new TabPage();
                mDataTab.Controls.Add(mExpressionsTab);
                mExpressionsTab.Name = "ctltabPageData";
                mExpressionsTab.Controls.Add(dataGridTab);
                dataGridTab.Dock = DockStyle.Fill;

                #endregion
            }

            if (dataNodeToAdd.mDTable == null)
            {
                return;
            }

            //dataNodeToAdd.mDTable.rea
            if ((tn = GetNode(dataNodeToAdd.DataText)) == null)
            {
                mExpressionsTab.TabIndex = 0;
                mExpressionsTab.Text = dataNodeToAdd.DataText;
                ((ucDataGridView)mExpressionsTab.Controls[0]).SetDataSource = dataNodeToAdd.mDTable;

                if (dataNodeToAdd.AddDGridContextMenu)
                    ((ucDataGridView)mExpressionsTab.Controls[0]).CxMenu = mCntxtMnuGrid;
                if (dataNodeToAdd.AddTVContextMenu)
                    dataNodeToAdd.TNode.ContextMenuStrip = mContextMenuTreeV;

                if (dataNodeToAdd.ParentNode.Equals("DAnTE"))
                    ctlTreeView.Nodes[0].Nodes.Add(dataNodeToAdd.TNode);
                else
                {
                    var parentNode = (mhtDatasets[dataNodeToAdd.ParentNode]).TNode;
                    parentNode.Nodes.Add(dataNodeToAdd.TNode);
                }
                ctlTreeView.ExpandAll();
                ctlTreeView.SelectedNode = dataNodeToAdd.TNode;
                statusBarPanelMsg.Text = dataNodeToAdd.Message;
                statusBarPanelRowNum.Text = dataNodeToAdd.mDTable.Rows.Count + " Rows/" +
                                            dataNodeToAdd.mDTable.Columns.Count + " Columns.";
                dataNodeToAdd.TNode.Tag = dataNodeToAdd;
            }
            else
            {
                tn.Tag = dataNodeToAdd;
                ctlTreeView.SelectedNode = tn;
                NodeSelect(tn);
            }
        }

        /// <summary>
        /// Select a node by clicking on the TreeView (the event handler calls this method).
        /// </summary>
        /// <param name="tn"></param>
        private void NodeSelect(TreeNode tn)
        {
            if (tn.Tag == null)
            {
                return;
            }

            var selectedNodeTag = (clsDatasetTreeNode)tn.Tag;
            if (mExpressionsTab == null)
            {
                return;
            }

            mExpressionsTab.Text = selectedNodeTag.DataText;
            ((ucDataGridView)mExpressionsTab.Controls[0]).SetDataSource = selectedNodeTag.mDTable;
            if (selectedNodeTag.AddDGridContextMenu)
                ((ucDataGridView)mExpressionsTab.Controls[0]).CxMenu = mCntxtMnuGrid;
            if (selectedNodeTag.AddTVContextMenu)
                tn.ContextMenuStrip = mContextMenuTreeV;
            statusBarPanelMsg.Text = selectedNodeTag.Message;
            statusBarPanelRowNum.Text = selectedNodeTag.mDTable.Rows.Count + " Rows/" +
                                        selectedNodeTag.mDTable.Columns.Count + " Columns.";
            //mDTselected = mdataNode.mDTable;
        }


        /// <summary>
        /// Get all the datasets that can be used for numerical manipulations
        /// Exceptions are factors, ProtInfo etc.
        /// This is set in a bool variable in each node class
        /// </summary>
        /// <returns>List of dataset names</returns>
        private List<string> AvailableDataSources()
        {
            var datasetNames = new List<string>();

            foreach (var dataset in mhtDatasets)
            {
                var datasetName = dataset.Key;
                var datasetTable = dataset.Value;
                if (datasetTable.IsNumeric)
                    datasetNames.Add(datasetName);
            }
            return datasetNames;
        }

        /// <summary>
        /// Given the TreeView node name, get the R variable name
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public string CorrespondingRdataset(string selected)
        {
            if (mhtDatasets.ContainsKey(selected))
                return (mhtDatasets[selected]).RDatasetName;

            return "Eset";
        }

        /// <summary>
        /// Variables to be saved in the session file.
        /// Note that we need to save extra Protein dataset variables that are used in
        /// protein profile plotting.
        /// </summary>
        /// <returns></returns>
        private string Vars2Save()
        {
            string rCmd = null;
            foreach (var item in mhtDatasets)
            {
                var datasetItem = item.Value;
                if (!string.IsNullOrWhiteSpace(datasetItem.RProteinDatasetName))
                    rCmd += '"' + datasetItem.RProteinDatasetName + '"' + ",";
                else
                    rCmd += '"' + datasetItem.RDatasetName + '"' + ",";
            }
            return rCmd;
        }

        /// <summary>
        /// Variables with actual (abundance) data.
        /// Used in dataset re-arrangement.
        /// </summary>
        /// <returns></returns>
        private string NumericVars()
        {
            string rCmd = null;
            foreach (var item in mhtDatasets)
            {
                var datasetItem = item.Value;
                if (datasetItem.IsNumeric)
                    rCmd += '"' + datasetItem.RDatasetName + '"' + ",";
            }
            return rCmd;
        }

        /// <summary>
        /// Remove datasets from the hashtable when a node is removed from the TreeView.
        /// Also remove the datasets corresponding to subnodes.
        /// </summary>
        /// <param name="tn"></param>
        private void RemoveNodeTree(TreeNode tn)
        {
            if (tn.Nodes.Count > 0)
            {
                foreach (TreeNode t in tn.Nodes)
                {
                    mhtDatasets.Remove(t.Text);
                }
            }
            mhtDatasets.Remove(tn.Text);
        }

        private TreeNode GetNode(string nodeText)
        {
            var allNodes = ctlTreeView.Nodes[0].Nodes;

            foreach (TreeNode node in allNodes)
            {
                if (nodeText.Equals(node.Text))
                    return node;

                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode nd in node.Nodes)
                    {
                        if (nodeText.Equals(nd.Text))
                            return nd;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Deleting each DataTable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemClose_Click(object sender, EventArgs e)
        {
            if (mhtDatasets.Count > 0)
            {
                var res = MessageBox.Show("This will delete the current table." +
                                          Environment.NewLine + "Are you sure?", "Delete table?",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                    DeleteCurrentNode();
            }
        }

        private void DeleteCurrentNode()
        {
            if (mDataTab.Controls.Count != 0)
            {
                var currentNode = ctlTreeView.SelectedNode;
                var strSelectedNode = currentNode.Text;

                ctlTreeView.Nodes.Remove(currentNode);
                RemoveNodeTree(currentNode);

                if (ctlTreeView.Nodes[0].Nodes.Count > 0)
                {
                    var idx = ctlTreeView.Nodes[0].Nodes.Count;
                    ctlTreeView.SelectedNode = ctlTreeView.Nodes[0].Nodes[idx - 1];
                }
                if (ctlTreeView.Nodes[0].Nodes.Count == 0)
                {
                    mDataTab.Controls.RemoveAt(0);
                    Title = "Main";
                    Settings.Default.SessionFileName = null;
                    Settings.Default.Save();
                }
                statusBarPanelMsg.Text = strSelectedNode + " removed.";
                statusBarPanelRowNum.Text = "";
            }
        }

        private void Add2AnalysisHTable(object o, string operation)
        {
            var i = 1;
            var strKey = operation + "_" + i;
            while (mhtAnalysisObjects.ContainsKey(strKey))
            {
                i++;
                strKey = operation + "_" + i;
            }
            mhtAnalysisObjects.Add(strKey, "");
            var analysisObject = new clsAnalysisObject(strKey, o);
            marrAnalysisObjects.Add(analysisObject);
        }
    }
}