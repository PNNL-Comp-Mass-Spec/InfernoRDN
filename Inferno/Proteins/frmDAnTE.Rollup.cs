using System;
using System.Data;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        Purgatorio.clsRRollupPar mclsRRollupPar;
        Purgatorio.clsZRollupPar mclsZRollupPar;
        Purgatorio.clsQRollupPar mclsQRollupPar;

        #region Rollup Menu items

        /// <summary>
        /// Calculate log Expressions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemRRollup_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(selectedNodeTag, "RRollup"))
            {
                return;
            }

            var dataset = selectedNodeTag.mstrRdatasetName;

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_RRollup;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_RRollupCompleted;

            #endregion

            mclsRRollupPar = new Purgatorio.clsRRollupPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText,
                OutFolder_pub = Settings.Default.WorkingFolder
            };

            var rRollupParams = new frmRRollUpPar(mclsRRollupPar);

            if (mhtDatasets.ContainsKey("RRollup"))
            {
                MessageBox.Show("RRollup is already done.", "Nothing to do");
                return;
            }

            if (rRollupParams.ShowDialog() == DialogResult.OK)
            {
                mclsRRollupPar = rRollupParams.clsRRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsRRollupPar, "RRollup");
                    var rcmd = mclsRRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Reset("RRollup : Scaling Peptides and Rolling up to Proteins ...");
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_RRollup;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_RRollupCompleted;

            #endregion
        }


        private void menuItemZRollup_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(selectedNodeTag, "ZRollup"))
            {
                return;
            }

            var dataset = selectedNodeTag.mstrRdatasetName;

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_ZRollup;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_ZRollupCompleted;

            #endregion

            mclsZRollupPar = new Purgatorio.clsZRollupPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText,
                OutFolder_pub = Settings.Default.WorkingFolder
            };

            var zRollupParams = new frmZRollupPar(mclsZRollupPar);

            if (mhtDatasets.ContainsKey("ZRollup"))
            {
                MessageBox.Show("ZRollup is already done.", "Nothing to do");
                return;
            }

            if (zRollupParams.ShowDialog() == DialogResult.OK)
            {
                mclsZRollupPar = zRollupParams.clsZRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsZRollupPar, "ZRollup");
                    var rcmd = mclsZRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Reset("ZRollup: Scaling Peptides and Rolling up to Proteins ...");
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_ZRollup;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_ZRollupCompleted;

            #endregion
        }

        private void menuItemQRup_Click(object sender, EventArgs e)
        {
            var selectedNodeTag = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(selectedNodeTag, "QRollup"))
            {
                return;
            }

            var dataset = selectedNodeTag.mstrRdatasetName;

            #region Hook Threading events

            m_BackgroundWorker.DoWork += m_BackgroundWorker_QRollup;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_QRollupCompleted;

            #endregion

            mclsQRollupPar = new Purgatorio.clsQRollupPar
            {
                Rdataset = selectedNodeTag.mstrRdatasetName,
                DataSetName = selectedNodeTag.mstrDataText
            };

            var qRollupParams = new frmQRollupPar(mclsQRollupPar);

            if (mhtDatasets.ContainsKey("Protein (Q)rollup"))
            {
                MessageBox.Show("Protein (Q)rollup is already done.", "Nothing to do", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            if (qRollupParams.ShowDialog() == DialogResult.OK)
            {
                mclsQRollupPar = qRollupParams.clsQRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsQRollupPar, "QRollup");
                    var rcmd = mclsQRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Reset("QRollup: Rolling up to Proteins ...");
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_QRollup;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_QRollupCompleted;

            #endregion
        }

        #endregion

        #region Private Methods

        private bool DoQRollup(string rcmd)
        {
            var success = true;

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("qrollupP1 <- qrollupP[,-c(1,2)]"); // dataset with no peptide counts
                if (mRConnector.GetTableFromRproteinMatrix("qrollupP"))
                {
                    var qRollupResults = mRConnector.DataTable.Copy();
                    qRollupResults.TableName = "qrollupP1";
                    qRollupResults.Columns[0].ColumnName = "Protein";
                    mRConnector.EvaluateNoReturn("cat(\"Proteins Q rolled up.\n\")");
                    AddDataset2HashTable(qRollupResults);
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

        private bool DoZRollup(string rcmd) //ZRollup
        {
            var success = true;

            //rcmd = "pScaled2 <- scale.proteins(Eset,ProtInfo)";
            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("sData2 <- pScaled2$sData"); // scaled data
                mRConnector.EvaluateNoReturn("orData2 <- pScaled2$orData"); // scaled and outlier removed
                mRConnector.EvaluateNoReturn("pData2 <- pScaled2$pData"); // protein abundances
                mRConnector.EvaluateNoReturn("pData22 <- pData2[,-c(1,2)]"); // dataset with no peptide counts

                if (mRConnector.GetTableFromRproteinMatrix("pData2"))
                {
                    var zRollupResults = mRConnector.DataTable.Copy();
                    zRollupResults.TableName = "pData22";
                    zRollupResults.Columns[0].ColumnName = "Protein";
                    mRConnector.EvaluateNoReturn("cat(\"Data scaling done.\n\")");
                    AddDataset2HashTable(zRollupResults);
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

        private bool DoRRollup(string rcmd) //RRollup
        {
            var success = true;
            var rRollupProteins = new DataTable();
            var scaledData = new DataTable();
            var scaledDataNoOutliers = new DataTable();

            try
            {
                mRConnector.EvaluateNoReturn(rcmd);
                mRConnector.EvaluateNoReturn("sData1 <- pScaled1$sData"); //scaled data
                mRConnector.EvaluateNoReturn("orData1 <- pScaled1$orData"); // scaled and outlier removed
                mRConnector.EvaluateNoReturn("pData1 <- pScaled1$pData"); // protein abundances
                mRConnector.EvaluateNoReturn("pData11 <- pData1[,-c(1,2)]"); // dataset with no peptide counts

                if (mRConnector.GetTableFromRproteinMatrix("pData1"))
                {
                    rRollupProteins = mRConnector.DataTable.Copy();
                    rRollupProteins.TableName = "pData11";
                    rRollupProteins.Columns[0].ColumnName = "Protein";

                    if (mRConnector.GetTableFromRmatrix("sData1"))
                    {
                        scaledData = mRConnector.DataTable.Copy();
                        scaledData.TableName = "sData1";
                    }
                    else
                        success = false;
                    if (mRConnector.GetTableFromRmatrix("orData1"))
                    {
                        scaledDataNoOutliers = mRConnector.DataTable.Copy();
                        scaledDataNoOutliers.TableName = "orData1";
                        mRConnector.EvaluateNoReturn("cat(\"Data Ref scaling/outliers test done.\n\")");
                    }
                    else
                        success = false;
                }
                else
                    success = false;
                if (success)
                {
                    AddDataset2HashTable(rRollupProteins);
                    AddDataset2HashTable(scaledData);
                    AddDataset2HashTable(scaledDataNoOutliers);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                success = false;
            }
            return success;
        }

        private bool ValidateTables(clsDatasetTreeNode mclsSelected, string rollupMode)
        {
            if (!ValidateExpressionsLoaded("use " + rollupMode))
            {
                return false;
            }

            if (!mhtDatasets.ContainsKey("Protein Info"))
            {
                MessageBox.Show("'Protein Info' table not found; cannot rollup peptide data to the protein level",
                                "Error");
                return false;
            }

            if (!mclsSelected.mblRollupPossible)
            {
                MessageBox.Show("Selected table does not have peptide data that can be rolled up to the protein level",
                                "Error");
                return false;
            }

            return true;
        }

        #endregion
    }
}