using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        DAnTE.Purgatorio.clsRRollupPar mclsRRollupPar;
        DAnTE.Purgatorio.clsZRollupPar mclsZRollupPar;
        DAnTE.Purgatorio.clsQRollupPar mclsQRollupPar;

        #region Rollup Menu items

        /// <summary>
        /// Calculate log Expressions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void menuItemRRollup_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(mclsSelected, "RRollup"))
            {
                return;
            }

            string dataset = mclsSelected.mstrRdatasetName;
            
            #region Hook Threading events
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_RRollup);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_RRollupCompleted);
            #endregion

            mclsRRollupPar = new DAnTE.Purgatorio.clsRRollupPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText,
                OutFolder_pub = Settings.Default.WorkingFolder
            };

            frmRRollUpPar mfrmRefRollup = new frmRRollUpPar(mclsRRollupPar);

            if (mhtDatasets.ContainsKey("RRollup"))
            {
                MessageBox.Show("RRollup is already done.", "Nothing to do");
                return;
            }
            
            if (mfrmRefRollup.ShowDialog() == DialogResult.OK)
            {
                mclsRRollupPar = mfrmRefRollup.clsRRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsRRollupPar, "RRollup");
                    string rcmd = mclsRRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Message = "RRollup : Scaling Peptides and Rolling up to Proteins ...";
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_RRollup);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_RRollupCompleted);
            #endregion
        }


        private void menuItemZRollup_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(mclsSelected, "ZRollup"))
            {
                return;
            }

            string dataset = mclsSelected.mstrRdatasetName;            

            #region Hook Threading events
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_ZRollup);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_ZRollupCompleted);
            #endregion

            mclsZRollupPar = new DAnTE.Purgatorio.clsZRollupPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText,
                OutFolder_pub = Settings.Default.WorkingFolder
            };

            frmZRollupPar mfrmScaling = new frmZRollupPar(mclsZRollupPar);

            if (mhtDatasets.ContainsKey("ZRollup"))
            {
                MessageBox.Show("ZRollup is already done.", "Nothing to do");
                return;
            }
                
            if (mfrmScaling.ShowDialog() == DialogResult.OK)
            {
                mclsZRollupPar = mfrmScaling.clsZRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsZRollupPar, "ZRollup");
                    string rcmd = mclsZRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Message = "ZRollup: Scaling Peptides and Rolling up to Proteins ...";
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_ZRollup);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_ZRollupCompleted);
            #endregion
        }

        private void menuItemQRup_Click(object sender, EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!ValidateTables(mclsSelected, "QRollup"))
            {
                return;
            }

            string dataset = mclsSelected.mstrRdatasetName;
            
            #region Hook Threading events
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_QRollup);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_QRollupCompleted);
            #endregion

            mclsQRollupPar = new DAnTE.Purgatorio.clsQRollupPar
            {
                Rdataset = mclsSelected.mstrRdatasetName,
                DataSetName = mclsSelected.mstrDataText
            };

            frmQRollupPar mfrmQRup = new frmQRollupPar(mclsQRollupPar);

            if (mhtDatasets.ContainsKey("Protein (Q)rollup"))
            {
                MessageBox.Show("Protein (Q)rollup is already done.", "Nothing to do");
                return;
            }
                
            if (mfrmQRup.ShowDialog() == DialogResult.OK)
            {
                mclsQRollupPar = mfrmQRup.clsQRollupPar;

                if (dataset != null)
                {
                    Add2AnalysisHTable(mclsQRollupPar, "QRollup");
                    string rcmd = mclsQRollupPar.Rcmd;

                    m_BackgroundWorker.RunWorkerAsync(rcmd);
                    mfrmShowProgress.Message = "QRollup: Rolling up to Proteins ...";
                    mfrmShowProgress.ShowDialog();
                }
            }

            #region Unhook Threading events
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_QRollup);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_QRollupCompleted);
            #endregion
        }

        #endregion

        #region Private Methods
        private bool DoQRollup(string rcmd)
        {
            DataTable mDTQProteins = new DataTable();
            bool success = true;

            try
            {
                rConnector.EvaluateNoReturn(rcmd);
                rConnector.EvaluateNoReturn("qrollupP1 <- qrollupP[,-c(1,2)]"); // dataset with no peptide counts
                if (rConnector.GetTableFromRproteinMatrix("qrollupP"))
                {
                    mDTQProteins = rConnector.DataTable.Copy();
                    mDTQProteins.TableName = "qrollupP1";
                    mDTQProteins.Columns[0].ColumnName = "Protein";
                    rConnector.EvaluateNoReturn("cat(\"Proteins Q rolled up.\n\")");
                    AddDataset2HashTable(mDTQProteins);
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
            DataTable mDTZProteins = new DataTable();
            bool success = true;

            //rcmd = "pScaled2 <- scale.proteins(Eset,ProtInfo)";
            try
            {
                rConnector.EvaluateNoReturn(rcmd);
                rConnector.EvaluateNoReturn("sData2 <- pScaled2$sData"); // scaled data
                rConnector.EvaluateNoReturn("orData2 <- pScaled2$orData"); // scaled and outlier removed
                rConnector.EvaluateNoReturn("pData2 <- pScaled2$pData"); // protein abundances
                rConnector.EvaluateNoReturn("pData22 <- pData2[,-c(1,2)]"); // dataset with no peptide counts

                if (rConnector.GetTableFromRproteinMatrix("pData2"))
                {
                    mDTZProteins = rConnector.DataTable.Copy();
                    mDTZProteins.TableName = "pData22";
                    mDTZProteins.Columns[0].ColumnName = "Protein";
                    rConnector.EvaluateNoReturn("cat(\"Data scaling done.\n\")");
                    AddDataset2HashTable(mDTZProteins);
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
            bool success = true;
            DataTable mDTRProteins = new DataTable();
            DataTable mDTRScaled = new DataTable();
            DataTable mDTRORscaled = new DataTable();

            try
            {
                rConnector.EvaluateNoReturn(rcmd);
                rConnector.EvaluateNoReturn("sData1 <- pScaled1$sData"); //scaled data
                rConnector.EvaluateNoReturn("orData1 <- pScaled1$orData"); // scaled and outlier removed
                rConnector.EvaluateNoReturn("pData1 <- pScaled1$pData"); // protein abundances
                rConnector.EvaluateNoReturn("pData11 <- pData1[,-c(1,2)]"); // dataset with no peptide counts

                if (rConnector.GetTableFromRproteinMatrix("pData1"))
                {
                    mDTRProteins = rConnector.DataTable.Copy();
                    mDTRProteins.TableName = "pData11";
                    mDTRProteins.Columns[0].ColumnName = "Protein";

                    if (rConnector.GetTableFromRmatrix("sData1"))
                    {
                        mDTRScaled = rConnector.DataTable.Copy();
                        mDTRScaled.TableName = "sData1";
                    }
                    else
                        success = false;
                    if (rConnector.GetTableFromRmatrix("orData1"))
                    {
                        mDTRORscaled = rConnector.DataTable.Copy();
                        mDTRORscaled.TableName = "orData1";
                        rConnector.EvaluateNoReturn("cat(\"Data Ref scaling/outliers test done.\n\")");
                    }
                    else
                        success = false;
                }
                else
                    success = false;
                if (success)
                {
                    AddDataset2HashTable(mDTRProteins);
                    AddDataset2HashTable(mDTRScaled);
                    AddDataset2HashTable(mDTRORscaled);
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
                MessageBox.Show("'Protein Info' table not found; cannot rollup peptide data to the protein level", "Error");
                return false;
            }

            if (!mclsSelected.mblRollupPossible)
            {
                MessageBox.Show("Selected table does not have peptide data that can be rolled up to the protein level", "Error");
                return false;
            }

            return true;
        }

        #endregion
    }
}