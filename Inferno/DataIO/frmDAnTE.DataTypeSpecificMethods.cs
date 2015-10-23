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
    public enum enmDataType { ESET, PROTINFO, FACTORS };

    partial class frmDAnTE
    {
        public string LastSessionLoadError { get; private set; }

        private void AddDataset2HashTable(DataTable mDT)
        {
            var mstrRdataset = mDT.TableName;
            var mclsDataset = new clsDatasetTreeNode
            {
                mDTable = mDT
            };

            switch (mstrRdataset)
            {
                case "Eset":
                    #region Eset
                    mclsDataset.mTNode = new TreeNode("Expressions", 1, 2);
                    mclsDataset.mstrDataText = "Expressions";
                    mclsDataset.mstrMessage = "Expression values selected.";
                    mclsDataset.mstrRdatasetName = "Eset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Expressions"))
                        mhtDatasets["Expressions"] = mclsDataset;
                    else
                        mhtDatasets.Add("Expressions", mclsDataset);

                    if (!mhtDatasets.ContainsKey("Factors"))
                        DatasetFactorInfo(mDT, false);
                    break;
                    #endregion
                case "ProtInfo":
                    #region ProtInfo
                    mclsDataset.mTNode = new TreeNode("Protein Info", 3, 4);
                    mclsDataset.mstrDataText = "Protein Info";
                    mclsDataset.mstrMessage = "Protein Info selected.";
                    mclsDataset.mstrRdatasetName = "ProtInfo";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Protein Info"))
                        mhtDatasets["Protein Info"] = mclsDataset;
                    else
                        mhtDatasets.Add("Protein Info", mclsDataset);
                    break;
                    #endregion
                case "factors":
                    #region Factors
                    mclsDataset.mTNode = new TreeNode("Factors", 3, 4);
                    mclsDataset.mstrDataText = "Factors";
                    mclsDataset.mstrMessage = "Factors selected.";
                    mclsDataset.mstrRdatasetName = "factors";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Factors"))
                        mhtDatasets["Factors"] = mclsDataset;
                    else
                        mhtDatasets.Add("Factors", mclsDataset);
                    marrDatasetInfo.Clear();
                    DatasetFactorInfo(mDT, true);
                    break;
                    #endregion
                case "logEset":
                    #region logEset
                    mclsDataset.mTNode = new TreeNode("Log Expressions", 1, 2);
                    mclsDataset.mstrDataText = "Log Expressions";
                    mclsDataset.mstrMessage = "Log Expressions selected.";
                    mclsDataset.mstrRdatasetName = "logEset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Log Expressions"))
                        mhtDatasets["Log Expressions"] = mclsDataset;
                    else
                        mhtDatasets.Add("Log Expressions", mclsDataset);
                    break;
                    #endregion
                case "loessData":
                    #region loessData
                    mclsDataset.mTNode = new TreeNode("LOESS Data", 1, 2);
                    mclsDataset.mstrDataText = "LOESS Data";
                    mclsDataset.mstrMessage = "LOESS Data selected.";
                    mclsDataset.mstrRdatasetName = "loessData";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("LOESS Data"))
                        mhtDatasets["LOESS Data"] = mclsDataset;
                    else
                        mhtDatasets.Add("LOESS Data", mclsDataset);
                    break;
                    #endregion
                case "quaNormEset":
                    #region quaNormEset
                    mclsDataset.mTNode = new TreeNode("Quantile Normalized", 1, 2);
                    mclsDataset.mstrDataText = "Quantile Normalized";
                    mclsDataset.mstrMessage = "Quantile normalized data selected.";
                    mclsDataset.mstrRdatasetName = "quaNormEset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Quantile Normalized"))
                        mhtDatasets["Quantile Normalized"] = mclsDataset;
                    else
                        mhtDatasets.Add("Quantile Normalized", mclsDataset);
                    break;
                    #endregion
                case "meanCEset":
                    #region MeanCEset
                    mclsDataset.mTNode = new TreeNode("Mean Centered", 1, 2);
                    mclsDataset.mstrDataText = "Mean Centered";
                    mclsDataset.mstrMessage = "Mean centered data selected.";
                    mclsDataset.mstrRdatasetName = "meanCEset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Mean Centered"))
                        mhtDatasets["Mean Centered"] = mclsDataset;
                    else
                        mhtDatasets.Add("Mean Centered", mclsDataset);
                    break;
                    #endregion
                case "medianCEset":
                    #region MedianCEset
                    mclsDataset.mTNode = new TreeNode("Median Centered", 1, 2);
                    mclsDataset.mstrDataText = "Median Centered";
                    mclsDataset.mstrMessage = "Median centered data selected.";
                    mclsDataset.mstrRdatasetName = "medianCEset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Median Centered"))
                        mhtDatasets["Median Centered"] = mclsDataset;
                    else
                        mhtDatasets.Add("Median Centered", mclsDataset);
                    break;
                    #endregion
                case "madEset":
                    #region madEset
                    mclsDataset.mTNode = new TreeNode("MAD Adjusted", 1, 2);
                    mclsDataset.mstrDataText = "MAD Adjusted";
                    mclsDataset.mstrMessage = "MAD Adjusted data selected.";
                    mclsDataset.mstrRdatasetName = "madEset";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("MAD Adjusted"))
                        mhtDatasets["MAD Adjusted"] = mclsDataset;
                    else
                        mhtDatasets.Add("MAD Adjusted", mclsDataset);
                    break;
                    #endregion
                case "linregData":
                    #region linregData
                    mclsDataset.mTNode = new TreeNode("Linear Regressed", 1, 2);
                    mclsDataset.mstrDataText = "Linear Regressed";
                    mclsDataset.mstrMessage = "Linear Regressed Data selected.";
                    mclsDataset.mstrRdatasetName = "linregData";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Linear Regressed"))
                        mhtDatasets["Linear Regressed"] = mclsDataset;
                    else
                        mhtDatasets.Add("Linear Regressed", mclsDataset);
                    break;
                    #endregion
                case "imputedData":
                    #region imputedData
                    mclsDataset.mTNode = new TreeNode("Imputed Data", 1, 2);
                    mclsDataset.mstrDataText = "Imputed Data";
                    mclsDataset.mstrMessage = "Imputed Data selected.";
                    mclsDataset.mstrRdatasetName = "imputedData";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    if (mhtDatasets.ContainsKey("Imputed Data"))
                        mhtDatasets["Imputed Data"] = mclsDataset;
                    else
                        mhtDatasets.Add("Imputed Data", mclsDataset);
                    break;
                    #endregion
                case "mergedData":
                    #region mergedData
                    mclsDataset.mTNode = new TreeNode("Merged Data", 1, 2);
                    mclsDataset.mstrDataText = "Merged Data";
                    mclsDataset.mstrMessage = "Merged Data selected.";
                    mclsDataset.mstrRdatasetName = "mergedData";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Merged Data"))
                        mhtDatasets["Merged Data"] = mclsDataset;
                    else
                        mhtDatasets.Add("Merged Data", mclsDataset);
                    break;
                    #endregion
                case "pData11":
                    #region pData11 (RRollup)
                    mclsDataset.mTNode = new TreeNode("RRollup", 1, 2);
                    mclsDataset.mstrDataText = "RRollup";
                    mclsDataset.mstrMessage = "RRollup selected.";
                    mclsDataset.mstrRdatasetName = "pData11";
                    mclsDataset.mstrRProtDatasetName = @"pScaled1"",""pData1";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("RRollup"))
                        mhtDatasets["RRollup"] = mclsDataset;
                    else
                        mhtDatasets.Add("RRollup", mclsDataset);
                    break;
                    #endregion
                case "sData1":
                    #region sData1 (RRollup)
                    mclsDataset.mTNode = new TreeNode("ScaledData", 1, 2);
                    mclsDataset.mstrDataText = "ScaledData";
                    mclsDataset.mstrMessage = "Scaled data selected.";
                    mclsDataset.mstrRdatasetName = "sData1";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    mclsDataset.mstrParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("ScaledData"))
                        mhtDatasets["ScaledData"] = mclsDataset;
                    else
                        mhtDatasets.Add("ScaledData", mclsDataset);
                    break;
                    #endregion
                case "orData1":
                    #region orData1 (RRollup)
                    mclsDataset.mTNode = new TreeNode("OutliersRemoved", 1, 2);
                    mclsDataset.mstrDataText = "OutliersRemoved";
                    mclsDataset.mstrMessage = "Outliers removed data selected.";
                    mclsDataset.mstrRdatasetName = "orData1";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    mclsDataset.mstrParentNode = "RRollup";
                    if (mhtDatasets.ContainsKey("OutliersRemoved"))
                        mhtDatasets["OutliersRemoved"] = mclsDataset;
                    else
                        mhtDatasets.Add("OutliersRemoved", mclsDataset);
                    break;
                    #endregion
                case "pData22":
                    #region pData2 (ZRollup)
                    mclsDataset.mTNode = new TreeNode("ZRollup", 1, 2);
                    mclsDataset.mstrDataText = "ZRollup";
                    mclsDataset.mstrMessage = "ZRollup selected.";
                    mclsDataset.mstrRdatasetName = "pData22";
                    mclsDataset.mstrRProtDatasetName = @"pScaled2"",""pData2";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("ZRollup"))
                        mhtDatasets["ZRollup"] = mclsDataset;
                    else
                        mhtDatasets.Add("ZRollup", mclsDataset);
                    break;
                    #endregion
                case "qrollupP1":
                    #region QRollupP1
                    mclsDataset.mTNode = new TreeNode("QRollup", 1, 2);
                    mclsDataset.mstrDataText = "QRollup";
                    mclsDataset.mstrMessage = "QRollup selected.";
                    mclsDataset.mstrRdatasetName = "qrollupP1";
                    mclsDataset.mstrRProtDatasetName = "qrollupP";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("QRollup"))
                        mhtDatasets["QRollup"] = mclsDataset;
                    else
                        mhtDatasets.Add("QRollup", mclsDataset);
                    break;
                    #endregion
                case "PCAweights":
                    #region PCA weights
                    mclsDataset.mTNode = new TreeNode("PCA Weights", 3, 4);
                    mclsDataset.mstrDataText = "PCA Weights";
                    mclsDataset.mstrMessage = "PCA Weights selected.";
                    mclsDataset.mstrRdatasetName = "PCAweights";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("PCA Weights"))
                        mhtDatasets["PCA Weights"] = mclsDataset;
                    else
                        mhtDatasets.Add("PCA Weights", mclsDataset);
                    break;
                    #endregion
                case "PLSweights":
                    #region PCA weights
                    mclsDataset.mTNode = new TreeNode("PLS Weights", 3, 4);
                    mclsDataset.mstrDataText = "PLS Weights";
                    mclsDataset.mstrMessage = "PLS Weights selected.";
                    mclsDataset.mstrRdatasetName = "PLSweights";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("PLS Weights"))
                        mhtDatasets["PLS Weights"] = mclsDataset;
                    else
                        mhtDatasets.Add("PLS Weights", mclsDataset);
                    break;
                    #endregion
                case "clusterResults":
                    #region PCA weights
                    mclsDataset.mTNode = new TreeNode("Heatmap Clusters", 3, 4);
                    mclsDataset.mstrDataText = "Heatmap Clusters";
                    mclsDataset.mstrMessage = "Heatmap Clusters selected.";
                    mclsDataset.mstrRdatasetName = "clusterResults";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Heatmap Clusters"))
                        mhtDatasets["Heatmap Clusters"] = mclsDataset;
                    else
                        mhtDatasets.Add("Heatmap Clusters", mclsDataset);
                    break;
                    #endregion
                case "pvalues":
                    #region p-values
                    mclsDataset.mTNode = new TreeNode("p-Values", 3, 4);
                    mclsDataset.mstrDataText = "p-Values";
                    mclsDataset.mstrMessage = "p-Values selected.";
                    mclsDataset.mstrRdatasetName = "pvalues";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("p-Values"))
                        mhtDatasets["p-Values"] = mclsDataset;
                    else
                        mhtDatasets.Add("p-Values", mclsDataset);
                    break;
                    #endregion
                case "yimputed":
                    #region yimputed
                    mclsDataset.mTNode = new TreeNode("Imputed Values", 1, 2);
                    mclsDataset.mstrDataText = "Imputed Values";
                    mclsDataset.mstrMessage = "Imputed Values selected.";
                    mclsDataset.mstrRdatasetName = "yimputed";
                    mclsDataset.mblAddDGridCtxtMnu = true;
                    mclsDataset.mblAddTVCtxtMnu = true;
                    mclsDataset.mblIsNumeric = true;
                    mclsDataset.mblIsPlottable = true;
                    mclsDataset.mblRollupPossible = true;
                    if (mhtDatasets.ContainsKey("Imputed Values"))
                        mhtDatasets["Imputed Values"] = mclsDataset;
                    else
                        mhtDatasets.Add("Imputed Values", mclsDataset);
                    break;
                    #endregion
                case "notused":
                    #region Unused data for ANOVA
                    mclsDataset.mTNode = new TreeNode("Unused Data", 3, 4);
                    mclsDataset.mstrDataText = "Unused Data";
                    mclsDataset.mstrMessage = "Unused Data selected.";
                    mclsDataset.mstrRdatasetName = "notused";
                    mclsDataset.mstrParentNode = "p-Values";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = true;
                    mclsDataset.mblIsPlottable = true;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Unused Data"))
                        mhtDatasets["Unused Data"] = mclsDataset;
                    else
                        mhtDatasets.Add("Unused Data", mclsDataset);
                    break;
                    #endregion
                case "foldChanges":
                    #region Fold Changes
                    mclsDataset.mTNode = new TreeNode("Fold Changes", 3, 4);
                    mclsDataset.mstrDataText = "Fold Changes";
                    mclsDataset.mstrMessage = "Fold Changes selected.";
                    mclsDataset.mstrRdatasetName = "foldChanges";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Fold Changes"))
                        mhtDatasets["Fold Changes"] = mclsDataset;
                    else
                        mhtDatasets.Add("Fold Changes", mclsDataset);
                    break;
                    #endregion
                case "patternData":
                    #region Pattern Search
                    mclsDataset.mTNode = new TreeNode("Pattern Corr", 3, 4);
                    mclsDataset.mstrDataText = "Pattern Corr";
                    mclsDataset.mstrMessage = "Pattern Correlations selected.";
                    mclsDataset.mstrRdatasetName = "patternData";
                    mclsDataset.mblAddDGridCtxtMnu = false;
                    mclsDataset.mblAddTVCtxtMnu = false;
                    mclsDataset.mblIsNumeric = false;
                    mclsDataset.mblIsPlottable = false;
                    mclsDataset.mblRollupPossible = false;
                    if (mhtDatasets.ContainsKey("Pattern Corr"))
                        mhtDatasets["Pattern Corr"] = mclsDataset;
                    else
                        mhtDatasets.Add("Pattern Corr", mclsDataset);
                    break;
                    #endregion
                default:
                    #region All other tables (filteredData)
                    if (mstrRdataset.Contains("filteredData"))
                    {
                        var setNum = mstrRdataset.Substring(12);
                        var nodeTxt = "Filtered Data" + setNum;
                        mclsDataset.mTNode = new TreeNode(nodeTxt, 1, 2);
                        mclsDataset.mstrDataText = nodeTxt;
                        mclsDataset.mstrMessage = "Filtered Data selected.";
                        mclsDataset.mstrRdatasetName = mstrRdataset;
                        mclsDataset.mblAddDGridCtxtMnu = true;
                        mclsDataset.mblAddTVCtxtMnu = false;
                        if (mhtDatasets.ContainsKey(nodeTxt))
                            mhtDatasets[nodeTxt] = mclsDataset;
                        else
                            mhtDatasets.Add(nodeTxt, mclsDataset);
                    }
                    break;
                    #endregion
            }
        }

        /// <summary>
        /// Opens a previously save session in *.dnt file
        /// There are few special cases (reading a vector, non numeric etc).
        /// But most of the tables are in R matrices (default section in the switch statement).
        /// </summary>
        /// <param name="dataFilePath"></param>
        /// <returns></returns>
        private bool OpenSession(string dataFilePath)
        {
            object vars;
            var success = true;
            var mDTvar = new DataTable();

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
                MessageBox.Show("R access failed loading file " + fiDatafile.FullName + ": " + ex.Message, "Error!");
                return false;
            }

            var variables = (string[])vars;

            for (var i = 1; i < variables.Length; i++)
            {
                try
                {
                    Console.WriteLine("frmDAnTE.OpenSession:{0} -> {1}", "", variables[i]);
                    switch (variables[i])
                    {
                        case ("ProtInfo"):
                            #region ProtInfo
                            if ((mDTvar = GetProtInfoMatrix()) != null)
                                mDTvar.TableName = "ProtInfo";
                            else
                                success = false;
                            break;
                            #endregion
                        case ("factors"):
                            #region Factors
                            if (mRConnector.GetTableFromRmatrixNonNumeric("factors"))
                            {
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.Columns[0].ColumnName = "Factors";
                                mDTvar.TableName = "factors";
                                DatasetFactorInfo(mDTvar, true);
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
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("pData11 <- pData1[,-c(1,2)]");
                                mDTvar.TableName = "pData11";
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
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("pData22 <- pData2[,-c(1,2)]");
                                mDTvar.TableName = "pData22";
                            }
                            #endregion
                            break;
                        case ("qrollupP"):
                            #region Qrollup
                            if (mRConnector.GetTableFromRproteinMatrix("qrollupP"))
                            {
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.Columns[0].ColumnName = "Protein";
                                mRConnector.EvaluateNoReturn("qrollupP1 <- qrollupP[,-c(1,2)]");
                                mDTvar.TableName = "qrollupP1";
                            }
                            else
                                success = false;
                            #endregion
                            break;
                        case ("clusterResults"):
                            #region Cluster results
                            if (mRConnector.GetTableFromRvector("clusterResults"))
                            {
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.TableName = "clusterResults";
                            }
                            else
                                success = false;
                            #endregion
                            break;
                        case "pScaled1": // R list variable that holds RRollup information
                            mDTvar = null;
                            break;
                        case "pScaled2": // R list variable that holds ZRollup information
                            mDTvar = null;
                            break;
                        default:
                            #region Everything else
                            if (mRConnector.GetTableFromRmatrix(variables[i]))
                            {
                                mDTvar = mRConnector.DataTable.Copy();
                                mDTvar.TableName = variables[i];
                            }
                            else
                                success = false;
                            #endregion
                            break;
                    }
                    if (mDTvar != null)
                    {
                        AddDataset2HashTable(mDTvar);
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
        private void AddDataNode(clsDatasetTreeNode mdataNode)
        {
            TreeNode tn;
            if ((mtabControlData.Controls.Count == 0) &&
                (mhtDatasets.ContainsKey("Expressions") || 
                 mhtDatasets.ContainsKey("Protein Info") ||
                 mhtDatasets.ContainsKey("Factors")))
            {
                #region Add Controls on tab page
                var dataGridTab = new ucDataGridView();
                this.ctltabPage = new TabPage();
                this.mtabControlData.Controls.Add(this.ctltabPage);
                this.ctltabPage.Name = "ctltabPageData";
                this.ctltabPage.Controls.Add(dataGridTab);
                dataGridTab.Dock = System.Windows.Forms.DockStyle.Fill;
                #endregion
            }

            if (mdataNode.mDTable == null)
            {
                return;
            }

            //mdataNode.mDTable.rea
            if ((tn = GetNode(mdataNode.mstrDataText)) == null)
            {
                this.ctltabPage.TabIndex = 0;
                this.ctltabPage.Text = mdataNode.mstrDataText;
                ((ucDataGridView)this.ctltabPage.Controls[0]).SetDataSource = mdataNode.mDTable;

                if (mdataNode.mblAddDGridCtxtMnu)
                    ((ucDataGridView)this.ctltabPage.Controls[0]).CxMenu = mCntxtMnuGrid;
                if (mdataNode.mblAddTVCtxtMnu)
                    mdataNode.mTNode.ContextMenuStrip = mContextMenuTreeV;

                if (mdataNode.mstrParentNode.Equals("DAnTE"))
                    ctltreeView.Nodes[0].Nodes.Add(mdataNode.mTNode);
                else
                {
                    var mtnParent = (mhtDatasets[mdataNode.mstrParentNode]).mTNode;
                    mtnParent.Nodes.Add(mdataNode.mTNode);
                }
                ctltreeView.ExpandAll();
                ctltreeView.SelectedNode = mdataNode.mTNode;
                this.statusBarPanelMsg.Text = mdataNode.mstrMessage;
                this.statusBarPanelRowNum.Text = mdataNode.mDTable.Rows.Count + " Rows/" +
                                                 mdataNode.mDTable.Columns.Count + " Columns.";
                mdataNode.mTNode.Tag = mdataNode;
            }
            else
            {
                tn.Tag = mdataNode;
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

            var mdataNode = (clsDatasetTreeNode)tn.Tag;
            if (this.ctltabPage == null)
            {
                return;
            }

            this.ctltabPage.Text = mdataNode.mstrDataText;
            ((ucDataGridView)this.ctltabPage.Controls[0]).SetDataSource = mdataNode.mDTable;
            if (mdataNode.mblAddDGridCtxtMnu)
                ((ucDataGridView)this.ctltabPage.Controls[0]).CxMenu = mCntxtMnuGrid;
            if (mdataNode.mblAddTVCtxtMnu)
                tn.ContextMenuStrip = mContextMenuTreeV;
            this.statusBarPanelMsg.Text = mdataNode.mstrMessage;
            this.statusBarPanelRowNum.Text = mdataNode.mDTable.Rows.Count.ToString() + " Rows/" +
                                             mdataNode.mDTable.Columns.Count.ToString() + " Columns.";
            //mDTselected = mdataNode.mDTable;
        }


        /// <summary>
        /// Get all the datasets that can be used for numerical manipulations
        /// Exceptions are factors, ProtInfo etc.
        /// This is set in a bool variable in each node class
        /// </summary>
        /// <returns></returns>
        private List<string> AvailableDataSources()
        {
            var marrDataList = new List<string>();

            foreach (var dataset in mhtDatasets)
            {
                var mstrDataset = dataset.Key;
                var mclsDataset = dataset.Value;
                if (mclsDataset.mblIsNumeric)
                    marrDataList.Add(mstrDataset);
            }
            return marrDataList;
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
                var mclsData = item.Value;
                if (!mclsData.mstrRProtDatasetName.Equals(""))
                    rcmd += @"""" + mclsData.mstrRProtDatasetName + @""",";
                else
                    rcmd += @"""" + mclsData.mstrRdatasetName + @""",";
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
                var mclsData = item.Value;
                if (mclsData.mblIsNumeric)
                    rcmd += @"""" + mclsData.mstrRdatasetName + @""",";
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
                foreach (TreeNode t in tn.Nodes)
                {
                    mhtDatasets.Remove(t.Text);
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
                else if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode nd in node.Nodes)
                        if (nodeText.Equals(nd.Text))
                            return nd;
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

                this.ctltreeView.Nodes.Remove(currNode);
                RemoveNodeTree(currNode);

                if (this.ctltreeView.Nodes[0].Nodes.Count > 0)
                {
                    var idx = ctltreeView.Nodes[0].Nodes.Count;
                    ctltreeView.SelectedNode = ctltreeView.Nodes[0].Nodes[idx - 1];
                }
                if (this.ctltreeView.Nodes[0].Nodes.Count == 0)
                {
                    this.mtabControlData.Controls.RemoveAt(0);
                    this.Title = "Main";
                    Settings.Default.SessionFileName = null;
                    Settings.Default.Save();
                }
                this.statusBarPanelMsg.Text = strSelectedNode + " removed.";
                this.statusBarPanelRowNum.Text = "";
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
            var mclsAnalysis = new clsAnalysisObject(strKey, o);
            marrAnalysisObjects.Add(mclsAnalysis);
        }
    }
}