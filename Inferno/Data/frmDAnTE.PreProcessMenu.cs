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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_Log2;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_Log2Completed;
            #endregion

            mclsLogPar = new Purgatorio.clsLogTransformPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DatasetName = mclsSelected.mstrDataText
            };

            var mfrmLog = new frmLogPar(mclsLogPar);

            var res = mfrmLog.ShowDialog();
            if (res == DialogResult.OK)
            {
                mclsLogPar = mfrmLog.clsLogPar;
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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
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

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_Lowess;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_LowessCompleted;

            #endregion

            mclsLoessPar = new Purgatorio.clsLoessPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            var mfrmLoessPar = new frmLOESSPar(mclsLoessPar)
            {
                DataSetName = mclsSelected.mstrDataText
            };

            var mclsFactors = mhtDatasets["Factors"];
            mfrmLoessPar.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);

            if (mfrmLoessPar.ShowDialog() == DialogResult.OK)
            {
                mclsLoessPar = mfrmLoessPar.clsLoessPar;

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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            var qnOK = false;


            if (!ValidateExpressionsLoaded("normalize"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            // check if there's enough complete data to do Quantile normalization
            var rcmd = "qnOK <- IsCompleteData(" + mclsSelected.mstrRdatasetName + ")";
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
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("mean center"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_MeanC;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MeanCCompleted;
            #endregion

            mclsMeanCPar = new Purgatorio.clsCentralTendencyPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            var mfrmMeanC = new frmMeanCenterPar(mclsMeanCPar)
            {
                DataSetName = mclsSelected.mstrDataText
            };

            if (mfrmMeanC.ShowDialog() == DialogResult.OK)
            {
                mclsMeanCPar = mfrmMeanC.clsCentrTendPar;

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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("apply MAD"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_MAD;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_MADCompleted;
            #endregion

            mclsMADPar = new Purgatorio.clsMADPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            var mfrmMad = new frmMADPar(mclsMADPar)
            {
                DataSetName = mclsSelected.mstrDataText
            };
            if (mhtDatasets.ContainsKey("Factors"))
            {
                var mclsFactors = mhtDatasets["Factors"];
                mfrmMad.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                mclsMADPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);
            }
            else
                mfrmMad.PopulateFactorComboBox = null;

            if (mfrmMad.ShowDialog() == DialogResult.OK)
            {
                mclsMADPar = mfrmMad.clsMADPar;

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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
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

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_LinReg;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_LinRegCompleted;

            #endregion

            mclsLinRegPar = new Purgatorio.clsLinRegrnPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            var mclsFactors = mhtDatasets["Factors"];
            mclsLinRegPar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);

            var mfrmLinReg = new frmLinRegPar(mclsLinRegPar);
            if (mfrmLinReg.ShowDialog() == DialogResult.OK)
            {
                mclsLinRegPar = mfrmLinReg.clsLinRegPar;

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
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateNodeIsSelected(mclsSelected))
            {
                return;
            }

            if (!ValidateExpressionsLoaded("impute"))
            {
                return;
            }

            if (!ValidateDataMatrixTableSelected(mclsSelected))
            {
                return;
            }

            #region Hook Threading events
            m_BackgroundWorker.DoWork += m_BackgroundWorker_Impute;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ImputeCompleted;
            #endregion

            mclsImputePar = new Purgatorio.clsImputePar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            var mfrmImpute = new frmImputePar(mclsImputePar)
            {
                DataSetName = mclsSelected.mstrDataText
            };

            if (mhtDatasets.ContainsKey("Factors"))
            {
                var mclsFactors = mhtDatasets["Factors"];
                mfrmImpute.PopulateFactorComboBox = clsDataTable.DataTableRows(mclsFactors.mDTable);
                mclsImputePar.marrFactors = clsDataTable.DataTableRows(mclsFactors.mDTable);
            }
            else
                mfrmImpute.PopulateFactorComboBox = null;

            if (mfrmImpute.ShowDialog() == DialogResult.OK)
            {
                mclsImputePar = mfrmImpute.clsImputePar;

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
                    var mDTMAD = mRConnector.DataTable.Copy();
                    mDTMAD.TableName = "madEset";
                    mRConnector.EvaluateNoReturn("cat(\"Data MAD Adjusted.\n\")");
                    AddDataset2HashTable(mDTMAD);
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
                    var mDTQuantile = mRConnector.DataTable.Copy();
                    mDTQuantile.TableName = "quaNormEset";
                    mRConnector.EvaluateNoReturn("cat(\"Data Quantile Normalized (Only complete data).\n\")");
                    AddDataset2HashTable(mDTQuantile);
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
                    var mDTLoess = mRConnector.DataTable.Copy();
                    mDTLoess.TableName = "loessData";
                    mRConnector.EvaluateNoReturn("cat(\"LOESS normalization done.\n\")");
                    AddDataset2HashTable(mDTLoess);
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
                    var mDTLinReg = mRConnector.DataTable.Copy();
                    mDTLinReg.TableName = "linregData";
                    mRConnector.EvaluateNoReturn("cat(\"Linear Regression done.\n\")");
                    AddDataset2HashTable(mDTLinReg);
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
                    var mDTImpute = mRConnector.DataTable.Copy();
                    mDTImpute.TableName = "imputedData";
                    mRConnector.EvaluateNoReturn("cat(\"Imputing done.\n\")");
                    AddDataset2HashTable(mDTImpute);
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
                    var mDTmerged = mRConnector.DataTable.Copy();
                    mDTmerged.TableName = "mergedData";
                    mRConnector.EvaluateNoReturn("cat(\"Merging done.\n\")");
                    AddDataset2HashTable(mDTmerged);
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