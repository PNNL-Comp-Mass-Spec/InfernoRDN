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
        /// Flat file of expression data
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

        private void AddDataset2HashTable(DataTable mDT)
        {
            var dataTableNameInR = mDT.TableName;
            var datasetNode = new clsDatasetTreeNode
            {
                mDTable = mDT
            };

            switch (dataTableNameInR)
            {
                case "Eset":
                    #region Eset
                    datasetNode.mTNode = new TreeNode("Expressions", 1, 2);
                    datasetNode.mstrDataText = "Expressions";
                    datasetNode.mstrMessage = "Expression values selected.";
                    datasetNode.mstrRdatasetName = "Eset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Expressions"))
                        mhtDatasets["Expressions"] = datasetNode;
                    else
                        mhtDatasets.Add("Expressions", datasetNode);

                    if (!mhtDatasets.ContainsKey("Factors"))
                        DatasetFactorInfo(mDT, false);
                    break;
                    #endregion
                case "ProtInfo":
                    #region ProtInfo
                    datasetNode.mTNode = new TreeNode("Protein Info", 3, 4);
                    datasetNode.mstrDataText = "Protein Info";
                    datasetNode.mstrMessage = "Protein Info selected.";
                    datasetNode.mstrRdatasetName = "ProtInfo";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Protein Info"))
                        mhtDatasets["Protein Info"] = datasetNode;
                    else
                        mhtDatasets.Add("Protein Info", datasetNode);
                    break;
                    #endregion
                case "factors":
                    #region Factors
                    datasetNode.mTNode = new TreeNode("Factors", 3, 4);
                    datasetNode.mstrDataText = "Factors";
                    datasetNode.mstrMessage = "Factors selected.";
                    datasetNode.mstrRdatasetName = "factors";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Factors"))
                        mhtDatasets["Factors"] = datasetNode;
                    else
                        mhtDatasets.Add("Factors", datasetNode);
                    marrDatasetInfo.Clear();
                    DatasetFactorInfo(mDT, true);
                    break;
                    #endregion
                case "logEset":
                    #region logEset
                    datasetNode.mTNode = new TreeNode("Log Expressions", 1, 2);
                    datasetNode.mstrDataText = "Log Expressions";
                    datasetNode.mstrMessage = "Log Expressions selected.";
                    datasetNode.mstrRdatasetName = "logEset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Log Expressions"))
                        mhtDatasets["Log Expressions"] = datasetNode;
                    else
                        mhtDatasets.Add("Log Expressions", datasetNode);
                    break;
                    #endregion
                case "loessData":
                    #region loessData
                    datasetNode.mTNode = new TreeNode("LOESS Data", 1, 2);
                    datasetNode.mstrDataText = "LOESS Data";
                    datasetNode.mstrMessage = "LOESS Data selected.";
                    datasetNode.mstrRdatasetName = "loessData";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("LOESS Data"))
                        mhtDatasets["LOESS Data"] = datasetNode;
                    else
                        mhtDatasets.Add("LOESS Data", datasetNode);
                    break;
                    #endregion
                case "quaNormEset":
                    #region quaNormEset
                    datasetNode.mTNode = new TreeNode("Quantile Normalized", 1, 2);
                    datasetNode.mstrDataText = "Quantile Normalized";
                    datasetNode.mstrMessage = "Quantile normalized data selected.";
                    datasetNode.mstrRdatasetName = "quaNormEset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Quantile Normalized"))
                        mhtDatasets["Quantile Normalized"] = datasetNode;
                    else
                        mhtDatasets.Add("Quantile Normalized", datasetNode);
                    break;
                    #endregion
                case "meanCEset":
                    #region MeanCEset
                    datasetNode.mTNode = new TreeNode("Mean Centered", 1, 2);
                    datasetNode.mstrDataText = "Mean Centered";
                    datasetNode.mstrMessage = "Mean centered data selected.";
                    datasetNode.mstrRdatasetName = "meanCEset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Mean Centered"))
                        mhtDatasets["Mean Centered"] = datasetNode;
                    else
                        mhtDatasets.Add("Mean Centered", datasetNode);
                    break;
                    #endregion
                case "medianCEset":
                    #region MedianCEset
                    datasetNode.mTNode = new TreeNode("Median Centered", 1, 2);
                    datasetNode.mstrDataText = "Median Centered";
                    datasetNode.mstrMessage = "Median centered data selected.";
                    datasetNode.mstrRdatasetName = "medianCEset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Median Centered"))
                        mhtDatasets["Median Centered"] = datasetNode;
                    else
                        mhtDatasets.Add("Median Centered", datasetNode);
                    break;
                    #endregion
                case "madEset":
                    #region madEset
                    datasetNode.mTNode = new TreeNode("MAD Adjusted", 1, 2);
                    datasetNode.mstrDataText = "MAD Adjusted";
                    datasetNode.mstrMessage = "MAD Adjusted data selected.";
                    datasetNode.mstrRdatasetName = "madEset";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("MAD Adjusted"))
                        mhtDatasets["MAD Adjusted"] = datasetNode;
                    else
                        mhtDatasets.Add("MAD Adjusted", datasetNode);
                    break;
                    #endregion
                case "linregData":
                    #region linregData
                    datasetNode.mTNode = new TreeNode("Linear Regressed", 1, 2);
                    datasetNode.mstrDataText = "Linear Regressed";
                    datasetNode.mstrMessage = "Linear Regressed Data selected.";
                    datasetNode.mstrRdatasetName = "linregData";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Linear Regressed"))
                        mhtDatasets["Linear Regressed"] = datasetNode;
                    else
                        mhtDatasets.Add("Linear Regressed", datasetNode);
                    break;
                    #endregion
                case "imputedData":
                    #region imputedData
                    datasetNode.mTNode = new TreeNode("Imputed Data", 1, 2);
                    datasetNode.mstrDataText = "Imputed Data";
                    datasetNode.mstrMessage = "Imputed Data selected.";
                    datasetNode.mstrRdatasetName = "imputedData";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Imputed Data"))
                        mhtDatasets["Imputed Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Imputed Data", datasetNode);
                    break;
                    #endregion
                case "mergedData":
                    #region mergedData
                    datasetNode.mTNode = new TreeNode("Merged Data", 1, 2);
                    datasetNode.mstrDataText = "Merged Data";
                    datasetNode.mstrMessage = "Merged Data selected.";
                    datasetNode.mstrRdatasetName = "mergedData";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Merged Data"))
                        mhtDatasets["Merged Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Merged Data", datasetNode);
                    break;
                    #endregion
                case "pData11":
                    #region pData11 (RRollup)
                    datasetNode.mTNode = new TreeNode("RRollup", 1, 2);
                    datasetNode.mstrDataText = "RRollup";
                    datasetNode.mstrMessage = "RRollup selected.";
                    datasetNode.mstrRdatasetName = "pData11";
                    datasetNode.mstrRProtDatasetName = @"pScaled1"",""pData1";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("RRollup"))
                        mhtDatasets["RRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("RRollup", datasetNode);
                    break;
                    #endregion
                case "sData1":
                    #region sData1 (RRollup)
                    datasetNode.mTNode = new TreeNode("ScaledData", 1, 2);
                    datasetNode.mstrDataText = "ScaledData";
                    datasetNode.mstrMessage = "Scaled data selected.";
                    datasetNode.mstrRdatasetName = "sData1";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    datasetNode.mstrParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("ScaledData"))
                        mhtDatasets["ScaledData"] = datasetNode;
                    else
                        mhtDatasets.Add("ScaledData", datasetNode);
                    break;
                    #endregion
                case "orData1":
                    #region orData1 (RRollup)
                    datasetNode.mTNode = new TreeNode("OutliersRemoved", 1, 2);
                    datasetNode.mstrDataText = "OutliersRemoved";
                    datasetNode.mstrMessage = "Outliers removed data selected.";
                    datasetNode.mstrRdatasetName = "orData1";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    datasetNode.mstrParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("OutliersRemoved"))
                        mhtDatasets["OutliersRemoved"] = datasetNode;
                    else
                        mhtDatasets.Add("OutliersRemoved", datasetNode);
                    break;
                    #endregion
                case "pData22":
                    #region pData2 (ZRollup)
                    datasetNode.mTNode = new TreeNode("ZRollup", 1, 2);
                    datasetNode.mstrDataText = "ZRollup";
                    datasetNode.mstrMessage = "ZRollup selected.";
                    datasetNode.mstrRdatasetName = "pData22";
                    datasetNode.mstrRProtDatasetName = @"pScaled2"",""pData2";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("ZRollup"))
                        mhtDatasets["ZRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("ZRollup", datasetNode);
                    break;
                    #endregion
                case "qrollupP1":
                    #region QRollupP1
                    datasetNode.mTNode = new TreeNode("QRollup", 1, 2);
                    datasetNode.mstrDataText = "QRollup";
                    datasetNode.mstrMessage = "QRollup selected.";
                    datasetNode.mstrRdatasetName = "qrollupP1";
                    datasetNode.mstrRProtDatasetName = "qrollupP";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("QRollup"))
                        mhtDatasets["QRollup"] = datasetNode;
                    else
                        mhtDatasets.Add("QRollup", datasetNode);
                    break;
                    #endregion
                case "PCAweights":
                    #region PCA weights
                    datasetNode.mTNode = new TreeNode("PCA Weights", 3, 4);
                    datasetNode.mstrDataText = "PCA Weights";
                    datasetNode.mstrMessage = "PCA Weights selected.";
                    datasetNode.mstrRdatasetName = "PCAweights";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("PCA Weights"))
                        mhtDatasets["PCA Weights"] = datasetNode;
                    else
                        mhtDatasets.Add("PCA Weights", datasetNode);
                    break;
                    #endregion
                case "PLSweights":
                    #region PCA weights
                    datasetNode.mTNode = new TreeNode("PLS Weights", 3, 4);
                    datasetNode.mstrDataText = "PLS Weights";
                    datasetNode.mstrMessage = "PLS Weights selected.";
                    datasetNode.mstrRdatasetName = "PLSweights";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("PLS Weights"))
                        mhtDatasets["PLS Weights"] = datasetNode;
                    else
                        mhtDatasets.Add("PLS Weights", datasetNode);
                    break;
                    #endregion
                case "clusterResults":
                    #region PCA weights
                    datasetNode.mTNode = new TreeNode("Heatmap Clusters", 3, 4);
                    datasetNode.mstrDataText = "Heatmap Clusters";
                    datasetNode.mstrMessage = "Heatmap Clusters selected.";
                    datasetNode.mstrRdatasetName = "clusterResults";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Heatmap Clusters"))
                        mhtDatasets["Heatmap Clusters"] = datasetNode;
                    else
                        mhtDatasets.Add("Heatmap Clusters", datasetNode);
                    break;
                    #endregion
                case "pvalues":
                    #region p-values
                    datasetNode.mTNode = new TreeNode("p-Values", 3, 4);
                    datasetNode.mstrDataText = "p-Values";
                    datasetNode.mstrMessage = "p-Values selected.";
                    datasetNode.mstrRdatasetName = "pvalues";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("p-Values"))
                        mhtDatasets["p-Values"] = datasetNode;
                    else
                        mhtDatasets.Add("p-Values", datasetNode);
                    break;
                    #endregion
                case "yimputed":
                    #region yimputed
                    datasetNode.mTNode = new TreeNode("Imputed Values", 1, 2);
                    datasetNode.mstrDataText = "Imputed Values";
                    datasetNode.mstrMessage = "Imputed Values selected.";
                    datasetNode.mstrRdatasetName = "yimputed";
                    datasetNode.mblAddDGridCtxtMnu = true;
                    datasetNode.mblAddTVCtxtMnu = true;
                    datasetNode.mblIsNumeric = true;
                    datasetNode.mblIsPlottable = true;
                    datasetNode.mblRollupPossible = true;
                    if (mhtDatasets.ContainsKey("Imputed Values"))
                        mhtDatasets["Imputed Values"] = datasetNode;
                    else
                        mhtDatasets.Add("Imputed Values", datasetNode);
                    break;
                    #endregion
                case "notused":
                    #region Unused data for ANOVA
                    datasetNode.mTNode = new TreeNode("Unused Data", 3, 4);
                    datasetNode.mstrDataText = "Unused Data";
                    datasetNode.mstrMessage = "Unused Data selected.";
                    datasetNode.mstrRdatasetName = "notused";
                    datasetNode.mstrParentNode = "p-Values";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = true;
                    datasetNode.mblIsPlottable = true;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Unused Data"))
                        mhtDatasets["Unused Data"] = datasetNode;
                    else
                        mhtDatasets.Add("Unused Data", datasetNode);
                    break;
                    #endregion
                case "foldChanges":
                    #region Fold Changes
                    datasetNode.mTNode = new TreeNode("Fold Changes", 3, 4);
                    datasetNode.mstrDataText = "Fold Changes";
                    datasetNode.mstrMessage = "Fold Changes selected.";
                    datasetNode.mstrRdatasetName = "foldChanges";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Fold Changes"))
                        mhtDatasets["Fold Changes"] = datasetNode;
                    else
                        mhtDatasets.Add("Fold Changes", datasetNode);
                    break;
                    #endregion
                case "patternData":
                    #region Pattern Search
                    datasetNode.mTNode = new TreeNode("Pattern Corr", 3, 4);
                    datasetNode.mstrDataText = "Pattern Corr";
                    datasetNode.mstrMessage = "Pattern Correlations selected.";
                    datasetNode.mstrRdatasetName = "patternData";
                    datasetNode.mblAddDGridCtxtMnu = false;
                    datasetNode.mblAddTVCtxtMnu = false;
                    datasetNode.mblIsNumeric = false;
                    datasetNode.mblIsPlottable = false;
                    datasetNode.mblRollupPossible = false;
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
                        datasetNode.mTNode = new TreeNode(nodeTxt, 1, 2);
                        datasetNode.mstrDataText = nodeTxt;
                        datasetNode.mstrMessage = "Filtered Data selected.";
                        datasetNode.mstrRdatasetName = dataTableNameInR;
                        datasetNode.mblAddDGridCtxtMnu = true;
                        datasetNode.mblAddTVCtxtMnu = false;
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

            var rcmd = "load(file=\"" + dataFilePath.Replace("\\", "/") + "\")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                vars = mRConnector.GetSymbolAsStrings("vars");
                mRConnector.EvaluateNoReturn("print(vars)");
            }
            catch (Exception ex)
            {
                LastSessionLoadError = ex.Message;
                if (!LastSessionLoadError.StartsWith("Value cannot be null", StringComparison.CurrentCultureIgnoreCase))
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
        /// Node Tag points to all the information related to theat node
        /// in a class of type 'clsDatasetTreeNode'
        /// </summary>
        /// <param name="mdataNode"></param>
        private void AddDataNode(clsDatasetTreeNode dataNodeToAdd)
        {
            TreeNode tn;
            if ((mtabControlData.Controls.Count == 0) &&
                (mhtDatasets.ContainsKey("Expressions") || 
                 mhtDatasets.ContainsKey("Protein Info") ||
                 mhtDatasets.ContainsKey("Factors")))
            {
                #region Add Controls on tab page
                var dataGridTab = new ucDataGridView();
                ctltabPage = new TabPage();
                mtabControlData.Controls.Add(ctltabPage);
                ctltabPage.Name = "ctltabPageData";
                ctltabPage.Controls.Add(dataGridTab);
                dataGridTab.Dock = DockStyle.Fill;
                #endregion
            }

            if (dataNodeToAdd.mDTable == null)
            {
                return;
            }

            //dataNodeToAdd.mDTable.rea
            if ((tn = GetNode(dataNodeToAdd.mstrDataText)) == null)
            {
                ctltabPage.TabIndex = 0;
                ctltabPage.Text = dataNodeToAdd.mstrDataText;
                ((ucDataGridView)ctltabPage.Controls[0]).SetDataSource = dataNodeToAdd.mDTable;

                if (dataNodeToAdd.mblAddDGridCtxtMnu)
                    ((ucDataGridView)ctltabPage.Controls[0]).CxMenu = mCntxtMnuGrid;
                if (dataNodeToAdd.mblAddTVCtxtMnu)
                    dataNodeToAdd.mTNode.ContextMenuStrip = mContextMenuTreeV;

                if (dataNodeToAdd.mstrParentNode.Equals("DAnTE"))
                    ctltreeView.Nodes[0].Nodes.Add(dataNodeToAdd.mTNode);
                else
                {
                    var parentNode = (mhtDatasets[dataNodeToAdd.mstrParentNode]).mTNode;
                    parentNode.Nodes.Add(dataNodeToAdd.mTNode);
                }
                ctltreeView.ExpandAll();
                ctltreeView.SelectedNode = dataNodeToAdd.mTNode;
                statusBarPanelMsg.Text = dataNodeToAdd.mstrMessage;
                statusBarPanelRowNum.Text = dataNodeToAdd.mDTable.Rows.Count + " Rows/" +
                                                 dataNodeToAdd.mDTable.Columns.Count + " Columns.";
                dataNodeToAdd.mTNode.Tag = dataNodeToAdd;
            }
            else
            {
                tn.Tag = dataNodeToAdd;
                ctltreeView.SelectedNode = tn;
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
            if (ctltabPage == null)
            {
                return;
            }

            ctltabPage.Text = selectedNodeTag.mstrDataText;
            ((ucDataGridView)ctltabPage.Controls[0]).SetDataSource = selectedNodeTag.mDTable;
            if (selectedNodeTag.mblAddDGridCtxtMnu)
                ((ucDataGridView)ctltabPage.Controls[0]).CxMenu = mCntxtMnuGrid;
            if (selectedNodeTag.mblAddTVCtxtMnu)
                tn.ContextMenuStrip = mContextMenuTreeV;
            statusBarPanelMsg.Text = selectedNodeTag.mstrMessage;
            statusBarPanelRowNum.Text = selectedNodeTag.mDTable.Rows.Count.ToString() + " Rows/" +
                                             selectedNodeTag.mDTable.Columns.Count.ToString() + " Columns.";
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
                if (datasetTable.mblIsNumeric)
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
                return (mhtDatasets[selected]).mstrRdatasetName;
            
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
            string rcmd = null;
            foreach (var item in mhtDatasets)
            {
                var datasetItem = item.Value;
                if (!string.IsNullOrWhiteSpace(datasetItem.mstrRProtDatasetName))
                    rcmd += '"' + datasetItem.mstrRProtDatasetName + '"' + ",";
                else
                    rcmd += '"' + datasetItem.mstrRdatasetName + '"' + ",";
            }
            return rcmd;
        }

        /// <summary>
        /// Variables with actual (abundance) data.
        /// Used in dataset re-arrangement.
        /// </summary>
        /// <returns></returns>
        private string NumericVars()
        {
            string rcmd = null;
            foreach (var item in mhtDatasets)
            {
                var datasetItem = item.Value;
                if (datasetItem.mblIsNumeric)
                    rcmd += '"' + datasetItem.mstrRdatasetName + '"' + ",";
            }
            return rcmd;
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
            var allNodes = ctltreeView.Nodes[0].Nodes;

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
        /// Deleting each datatable.
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
            if (mtabControlData.Controls.Count != 0)
            {
                var currNode = ctltreeView.SelectedNode;
                var strSelectedNode = currNode.Text;

                ctltreeView.Nodes.Remove(currNode);
                RemoveNodeTree(currNode);

                if (ctltreeView.Nodes[0].Nodes.Count > 0)
                {
                    var idx = ctltreeView.Nodes[0].Nodes.Count;
                    ctltreeView.SelectedNode = ctltreeView.Nodes[0].Nodes[idx - 1];
                }
                if (ctltreeView.Nodes[0].Nodes.Count == 0)
                {
                    mtabControlData.Controls.RemoveAt(0);
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