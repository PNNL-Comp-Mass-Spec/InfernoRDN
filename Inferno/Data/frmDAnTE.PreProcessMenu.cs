using System;
using System.Windows.Forms;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        Purgatorio.clsLogTransformPar mclsLogPar;
        Purgatorio.clsLinRegrnPar mclsLinRegPar;
        Purgatorio.clsLoessPar mclsLoessPar;
        Purgatorio.clsCentralTendencyPar mclsMeanCPar;
        Purgatorio.clsQnormPar mclsQnormPar;
        Purgatorio.clsMADPar mclsMADPar;
        Purgatorio.clsImputePar mclsImputePar;

        #region Pre-processing Menu items

        /// <summary>
        /// Calculate log Expressions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemLogEset_Click(object sender, System.EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsLogPar = new Purgatorio.clsLogTransformPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DatasetName = selectedNodeTag.mstrDataText
            };

            var logTransformParams = new frmLogPar(mclsLogPar);

            var res = logTransformParams.ShowDialog();
            if (res == DialogResult.OK)
            {
                mclsLogPar = logTransformParams.clsLogPar;
                Add2AnalysisHTable(mclsLogPar, "LogTransform");

                var rcmd = mclsLogPar.Rcmd;

                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "Calculating Log Expressions ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Log2;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_Log2Completed;

            #endregion
        }

        private void menuItemLOESSNorm_Click(object sender, EventArgs e) //LOESS
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsLoessPar = new Purgatorio.clsLoessPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var loessParams = new frmLOESSPar(mclsLoessPar)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };

            var factorTable = mhtDatasets["Factors"];
            loessParams.PopulateFactorComboBox = clsDataTable.DataTableRows(factorTable.mDTable);

            if (loessParams.ShowDialog() == DialogResult.OK)
            {
                mclsLoessPar = loessParams.clsLoessPar;

                Add2AnalysisHTable(mclsLoessPar, "LOESS");

                var rcmd = mclsLoessPar.Rcmd;

                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "LOESS Normalizing Data ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Lowess;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_LowessCompleted;

            #endregion
        }

        private void mnuItemQnorm_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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
            var rcmd = "qnOK <- IsCompleteData(" + selectedNodeTag.mstrRdatasetName + ")";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

            mclsQnormPar = new Purgatorio.clsQnormPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            Add2AnalysisHTable(mclsQnormPar, "Quantile_Normalization");
            //rcmd = "quaNormEset <- quantileN(" + mclsSelected.mstrRdatasetName + ")";
            rcmd = mclsQnormPar.Rcmd;

            m_BackgroundWorker.RunWorkerAsync(rcmd);
            mfrmShowProgress.Message = "Quantile Normalizing ...";
            mfrmShowProgress.ShowDialog();

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Quantile;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_QuantileCompleted;

            #endregion
        }

        private void menuItemMeanC_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsMeanCPar = new Purgatorio.clsCentralTendencyPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var meanCenterParams = new frmMeanCenterPar(mclsMeanCPar)
            {
                DataSetName = selectedNodeTag.mstrDataText
            };

            if (meanCenterParams.ShowDialog() == DialogResult.OK)
            {
                mclsMeanCPar = meanCenterParams.clsCentrTendPar;

                Add2AnalysisHTable(mclsMeanCPar, "CentralTendency");

                var rcmd = mclsMeanCPar.Rcmd;
                m_BackgroundWorker.RunWorkerAsync(rcmd);

                if (mclsMeanCPar.mblUseMeanTend)
                    mfrmShowProgress.Message = "Mean Centering Data ...";
                else
                    mfrmShowProgress.Message = "Median Centering Data ...";

                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MeanC;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MeanCCompleted;

            #endregion
        }

        private void menuItemMAD_Click(object sender, EventArgs e) // MAD Adjustment
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsMADPar = new Purgatorio.clsMADPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var madParams = new frmMADPar(mclsMADPar)
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
                mclsMADPar = madParams.clsMADPar;

                Add2AnalysisHTable(mclsMADPar, "MedianAbsoluteDeviation_Adjustment");

                var rcmd = mclsMADPar.Rcmd;

                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "MAD Adjusting Data ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_MAD;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_MADCompleted;

            #endregion
        }

        private void menuItemLinReg_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsLinRegPar = new Purgatorio.clsLinRegrnPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var factorTable = mhtDatasets["Factors"];
            mclsLinRegPar.marrFactors = clsDataTable.DataTableRows(factorTable.mDTable);

            var linearRegressionParams = new frmLinRegPar(mclsLinRegPar);
            if (linearRegressionParams.ShowDialog() == DialogResult.OK)
            {
                mclsLinRegPar = linearRegressionParams.clsLinRegPar;

                Add2AnalysisHTable(mclsLinRegPar, "LinearRegression");

                var rcmd = mclsLinRegPar.Rcmd;

                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "Linear Regressing Data ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_LinReg;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_LinRegCompleted;

            #endregion
        }

        private void menuItemImpute_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

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

            mclsImputePar = new Purgatorio.clsImputePar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var imputeParams = new frmImputePar(mclsImputePar)
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
                mclsImputePar = imputeParams.clsImputePar;

                Add2AnalysisHTable(mclsImputePar, "Imputation");

                var rcmd = mclsImputePar.Rcmd;

                m_BackgroundWorker.RunWorkerAsync(rcmd);
                mfrmShowProgress.Message = "Imputing Data ...";
                mfrmShowProgress.ShowDialog();
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_Impute;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ImputeCompleted;

            #endregion
        }

        #endregion

        #region Private Methods

        private bool DoMADAdjustment(string rcmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoMeanCenter(string rcmd)
        {
            var success = true;

            try
            {
                string tableToFind;
                string statusMsg;

                if (mclsMeanCPar.mblUseMeanTend)
                {
                    tableToFind = "meanCEset";
                    statusMsg = "cat(\"Data mean centered.\n\")";
                }
                else
                {
                    tableToFind = "medianCEset";
                    statusMsg = "cat(\"Data median centered.\n\")";
                }

                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoQuantileN(string rcmd)
        {
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoLowess(string rcmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoLinReg(string rcmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoImpute(string rcmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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

        private bool DoMergeColumns(string rcmd)
        {
            var success = true;
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
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