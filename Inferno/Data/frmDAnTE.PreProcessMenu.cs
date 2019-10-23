using System;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        Purgatorio.clsLogTransformPar mLogTransformParams;
        Purgatorio.clsLinRegrnPar mLinearRegressionParams;
        Purgatorio.clsLoessPar mLoessParams;
        Purgatorio.clsCentralTendencyPar mMeanCenteringParams;
        Purgatorio.clsQnormPar mQuantileNormalizationParams;
        Purgatorio.clsMADPar mMADParams;
        Purgatorio.clsImputePar mImputationParams;

        #region Pre-processing Menu items

        /// <summary>
        /// Calculate log Expressions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemLogEset_Click(object sender, System.EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Log2;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_Log2Completed;

            #endregion

            mLogTransformParams = new Purgatorio.clsLogTransformPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DatasetName = selectedNodeTag.mstrDataText
            };

            var logTransformParams = new frmLogPar(mLogTransformParams);

            var res = logTransformParams.ShowDialog();
            if (res == DialogResult.OK)
            {
                mLogTransformParams = logTransformParams.clsLogPar;
                Add2AnalysisHTable(mLogTransformParams, "LogTransform");

                var rCmd = mLogTransformParams.RCommand;

                m_BackgroundWorker.RunWorkerAsync(rCmd);
                mProgressForm.Reset("Calculating Log Expressions ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Log2;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_Log2Completed;

            #endregion
        }

        private void menuItemLOESSNorm_Click(object sender, EventArgs e) //LOESS
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("use LOESS"))
            {
                return;
            }

            if (!ValidateFactorsDefined("use LOESS (note that data should be log transformed)"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Lowess;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_LowessCompleted;

            #endregion

            mLoessParams = new Purgatorio.clsLoessPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var loessParams = new frmLOESSPar(mLoessParams)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };

            var factorTable = mhtDatasets["Factors"];
            loessParams.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);

            if (loessParams.ShowDialog() == DialogResult.OK)
            {
                mLoessParams = loessParams.clsLoessPar;

                Add2AnalysisHTable(mLoessParams, "LOESS");

                var rCmd = mLoessParams.RCommand;

                m_BackgroundWorker.RunWorkerAsync(rCmd);
                mProgressForm.Reset("LOESS Normalizing Data ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Lowess;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_LowessCompleted;

            #endregion
        }

        private void mnuItemQnorm_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            var qnOK = false;


            if (!ValidateExpressionsLoaded("normalize"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            // check if there's enough complete data to do Quantile normalization
            var rCmd = "qnOK <- IsCompleteData(" + selectedNodeTag.mstrRdatasetName + ")";
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                qnOK = mRConnector.GetSymbolAsBool("qnOK");
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
            }

            if (!qnOK)
            {
                MessageBox.Show("Not enough complete data for Quantile Normalization", "Too many missing",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Quantile;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_QuantileCompleted;

            #endregion

            mQuantileNormalizationParams = new Purgatorio.clsQnormPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            Add2AnalysisHTable(mQuantileNormalizationParams, "Quantile_Normalization");
            //rCmd = "quaNormEset <- quantileN(" + mclsSelected.mstrRdatasetName + ")";
            rCmd = mQuantileNormalizationParams.RCommand;

            m_BackgroundWorker.RunWorkerAsync(rCmd);
            mProgressForm.Reset("Quantile Normalizing ...");
            mProgressForm.ShowDialog();

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Quantile;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_QuantileCompleted;

            #endregion
        }

        private void menuItemMeanC_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("mean center"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_MeanC;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MeanCCompleted;

            #endregion

            mMeanCenteringParams = new Purgatorio.clsCentralTendencyPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var meanCenterParams = new frmMeanCenterPar(mMeanCenteringParams)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };

            if (meanCenterParams.ShowDialog() == DialogResult.OK)
            {
                mMeanCenteringParams = meanCenterParams.clsCentrTendPar;

                Add2AnalysisHTable(mMeanCenteringParams, "CentralTendency");

                var rCmd = mMeanCenteringParams.RCommand;
                m_BackgroundWorker.RunWorkerAsync(rCmd);

                string message;
                if (mMeanCenteringParams.mblUseMeanTend)
                    message = "Mean Centering Data ...";
                else
                    message = "Median Centering Data ...";

                mProgressForm.Reset(message);
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MeanC;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MeanCCompleted;

            #endregion
        }

        private void menuItemMAD_Click(object sender, EventArgs e) // MAD Adjustment
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("apply MAD"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_MAD;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MADCompleted;

            #endregion

            mMADParams = new Purgatorio.clsMADPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var madParams = new frmMADPar(mMADParams)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };
            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                madParams.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);
                clsDataTable.DataTableRows(factorTable.mDTable);
            }
            else
                madParams.PopulateFactorComboBox = null;

            if (madParams.ShowDialog() == DialogResult.OK)
            {
                mMADParams = madParams.clsMADPar;

                Add2AnalysisHTable(mMADParams, "MedianAbsoluteDeviation_Adjustment");

                var rCmd = mMADParams.RCommand;

                m_BackgroundWorker.RunWorkerAsync(rCmd);
                mProgressForm.Reset("MAD Adjusting Data ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MAD;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MADCompleted;

            #endregion
        }

        private void menuItemLinReg_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("apply linear regression"))
            {
                return;
            }

            if (!ValidateFactorsDefined("apply linear regression"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_LinReg;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_LinRegCompleted;

            #endregion

            mLinearRegressionParams = new Purgatorio.clsLinRegrnPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var factorTable = mhtDatasets["Factors"];
            mLinearRegressionParams.marrFactors = clsDataTable.DataTableRows(factorTable.mDTable);

            var linearRegressionParams = new frmLinRegPar(mLinearRegressionParams);
            if (linearRegressionParams.ShowDialog() == DialogResult.OK)
            {
                mLinearRegressionParams = linearRegressionParams.clsLinRegPar;

                Add2AnalysisHTable(mLinearRegressionParams, "LinearRegression");

                var rCmd = mLinearRegressionParams.RCommand;

                m_BackgroundWorker.RunWorkerAsync(rCmd);
                mProgressForm.Reset("Linear Regressing Data ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_LinReg;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_LinRegCompleted;

            #endregion
        }

        private void menuItemImpute_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(selectedNodeTag))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("impute"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(selectedNodeTag))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Impute;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ImputeCompleted;

            #endregion

            mImputationParams = new Purgatorio.clsImputePar
            {
                mRDataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var imputeParams = new frmImputePar(mImputationParams)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };

            if (mhtDatasets.ContainsKey("Factors"))
            {
                var factorTable = mhtDatasets["Factors"];
                imputeParams.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);
                clsDataTable.DataTableRows(factorTable.mDTable);
            }
            else
                imputeParams.PopulateFactorComboBox = null;

            if (imputeParams.ShowDialog() == DialogResult.OK)
            {
                mImputationParams = imputeParams.clsImputePar;

                Add2AnalysisHTable(mImputationParams, "Imputation");

                var rCmd = mImputationParams.RCommand;

                m_BackgroundWorker.RunWorkerAsync(rCmd);
                mProgressForm.Reset("Imputing Data ...");
                mProgressForm.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Impute;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ImputeCompleted;

            #endregion
        }

        #endregion

        #region Private Methods

        private bool DoMADAdjustment(string rCmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("madEset"))
                {
                    var madAdjustedData = mRConnector.DataTable.Copy();
                    madAdjustedData.TableName = "madEset";
                    mRConnector.EvaluateNoReturn("cat(\"Data MAD Adjusted.\n\")");
                    AddDataset2HashTable(madAdjustedData);
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

        private bool DoMeanCenter(string rCmd)
        {
            var success = true;

            try
            {
                string tableToFind;
                string statusMsg;

                if (mMeanCenteringParams.mblUseMeanTend)
                {
                    tableToFind = "meanCEset";
                    statusMsg = "cat(\"Data mean centered.\n\")";
                }
                else
                {
                    tableToFind = "medianCEset";
                    statusMsg = "cat(\"Data median centered.\n\")";
                }

                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix(tableToFind))
                {
                    // DataTable dtMeanOrMedianCentered = new DataTable();

                    var dtMeanOrMedianCentered = mRConnector.DataTable.Copy();
                    dtMeanOrMedianCentered.TableName = tableToFind;
                    mRConnector.EvaluateNoReturn(statusMsg);
                    AddDataset2HashTable(dtMeanOrMedianCentered);
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

        private bool DoQuantileN(string rCmd)
        {
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("quaNormEset"))
                {
                    var quantileNormalizedData = mRConnector.DataTable.Copy();
                    quantileNormalizedData.TableName = "quaNormEset";
                    mRConnector.EvaluateNoReturn("cat(\"Data Quantile Normalized (Only complete data).\n\")");
                    AddDataset2HashTable(quantileNormalizedData);
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

        private bool DoLowess(string rCmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("loessData"))
                {
                    var loessNormalizedData = mRConnector.DataTable.Copy();
                    loessNormalizedData.TableName = "loessData";
                    mRConnector.EvaluateNoReturn("cat(\"LOESS normalization done.\n\")");
                    AddDataset2HashTable(loessNormalizedData);
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

        private bool DoLinReg(string rCmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("linregData"))
                {
                    var linearRegressionData = mRConnector.DataTable.Copy();
                    linearRegressionData.TableName = "linregData";
                    mRConnector.EvaluateNoReturn("cat(\"Linear Regression done.\n\")");
                    AddDataset2HashTable(linearRegressionData);
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

        private bool DoImpute(string rCmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("imputedData"))
                {
                    var imputedData = mRConnector.DataTable.Copy();
                    imputedData.TableName = "imputedData";
                    mRConnector.EvaluateNoReturn("cat(\"Imputing done.\n\")");
                    AddDataset2HashTable(imputedData);
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

        private bool DoMergeColumns(string rCmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rCmd);
                if (mRConnector.GetTableFromRmatrix("mergedData"))
                {
                    var mergedColumnData = mRConnector.DataTable.Copy();
                    mergedColumnData.TableName = "mergedData";
                    mRConnector.EvaluateNoReturn("cat(\"Merging done.\n\")");
                    AddDataset2HashTable(mergedColumnData);
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