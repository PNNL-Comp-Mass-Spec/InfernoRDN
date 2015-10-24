using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using DAnTE.Tools;
using DAnTE.ExtraControls;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for Pre-Process --------------------------

        void m_BackgroundWorker_Log2Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Log2 Operation Canceled", "Error!", MessageBoxButtons.OK, 
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Log Expressions"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Log Expressions"]);
                }
                else
                    MessageBox.Show("Unknown error in data. Check if your file matches the format required",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_Log2(object sender, DoWorkEventArgs e)
        {
            DataTable mDTLogEset1 = new DataTable();

            string rcmd = (string)e.Argument;
            try
            {
                rConnector.rdcom.EvaluateNoReturn(rcmd);
                if (rConnector.GetTableFromRmatrix("logEset"))
                {
                    mDTLogEset1 = rConnector.mDataTable.Copy();
                    mDTLogEset1.TableName = "logEset";
                    rConnector.rdcom.EvaluateNoReturn("cat(\"Log Expressions calculated.\n\")");
                    //--------------------------------------
                    AddDataset2HashTable(mDTLogEset1);
                    e.Result = true;
                }
                else
                    e.Result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RDCOM failed: " + ex.Message, "Error!");
                e.Result = false;
                e.Cancel = true;
            }
        }

        void m_BackgroundWorker_LowessCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Loess Normalization Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("LOESS Data"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["LOESS Data"]);
                }
                else
                    MessageBox.Show("Loess failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_Lowess(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLowess((string)e.Argument);
        }

        void m_BackgroundWorker_LinRegCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Linear Regression Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Linear Regressed"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Linear Regressed"]);
                }
                else
                    MessageBox.Show("Linear Regression failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_LinReg(object sender, DoWorkEventArgs e)
        {
            e.Result = DoLinReg((string)e.Argument);
        }

        void m_BackgroundWorker_MeanCCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Mean Centring Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Mean Centered"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Mean Centered"]);
                }
                else
                    MessageBox.Show("Mean Centring failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_MeanC(object sender, DoWorkEventArgs e)
        {
            e.Result = DoMeanCenter((string)e.Argument);
        }

        void m_BackgroundWorker_QuantileCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Quantile Normalization Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Quantile Normalized"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Quantile Normalized"]);
                }
                else
                    MessageBox.Show("Quantile Normalization failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_Quantile(object sender, DoWorkEventArgs e)
        {
            e.Result = DoQuantileN((string)e.Argument);
        }

        void m_BackgroundWorker_MADCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("MAD Adjustment Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("MAD Adjusted"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["MAD Adjusted"]);
                }
                else
                    MessageBox.Show("MAD Adjustment failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_MAD(object sender, DoWorkEventArgs e)
        {
            e.Result = DoMADAdjustment((string)e.Argument);
        }

        void m_BackgroundWorker_ImputeCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Imputation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Imputed Data"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Imputed Data"]);
                }
                else
                    MessageBox.Show("Imputation failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_Impute(object sender, DoWorkEventArgs e)
        {
            e.Result = DoImpute((string)e.Argument);
        }

        void m_BackgroundWorker_MergeCCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Imputation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("Merged Data"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Merged Data"]);
                }
                else
                    MessageBox.Show("Imputation failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_MergeC(object sender, DoWorkEventArgs e)
        {
            e.Result = DoMergeColumns((string)e.Argument);
        }
        #endregion

        #region Threading events for Rollup

        void m_BackgroundWorker_RRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Ref. Scaling Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("RRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["RRollup"]);
                    if (mhtDatasets.Contains("ScaledData"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["ScaledData"]);
                    if (mhtDatasets.Contains("OutliersRemoved"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["OutliersRemoved"]);
                }
                else
                    MessageBox.Show("Ref. Scaling/Rolling up failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_RRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoRRollup((string)e.Argument);
        }


        void m_BackgroundWorker_ZRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Scaling Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("ZRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["ZRollup"]);
                }
                else
                    MessageBox.Show("Scaling/Rolling up failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_ZRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoZRollup((string)e.Argument);
        }

        void m_BackgroundWorker_QRollupCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("QRollup Operation Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("QRollup"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["QRollup"]);
                }
                else
                    MessageBox.Show("QRollup failed." + Environment.NewLine +
                        "Check if you have all data requirements.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_QRollup(object sender, DoWorkEventArgs e)
        {
            e.Result = DoQRollup((string)e.Argument);
        }

        #endregion

        #region Threading events for FileOpen --------------------------

        void m_BackgroundWorker_FileOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool mblFileOpenResult = false;

            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                mblFileOpenResult = false;
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                mblFileOpenResult = false;
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                mblFileOpenResult = (bool)e.Result;
                if (mblFileOpenResult)
                {
                    if (mhtDatasets.ContainsKey("Expressions") && (dataSetType == enmDataType.ESET))
                    {
                        if (mhtDatasets.ContainsKey("Protein Info"))
                        {
                            AddDataNode((clsDatasetTreeNode)mhtDatasets["Protein Info"]);
                        }
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Expressions"]);
                        if (mstrLoadedfileName != null)
                            this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
                        marrDataSetNames = clsDataTable.DataTableColumns(
                            ((clsDatasetTreeNode)mhtDatasets["Expressions"]).mDTable, true);
                    }
                    if (mhtDatasets.ContainsKey("Protein Info") && (dataSetType == enmDataType.PROTINFO))
                    {
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Protein Info"]);
                    }
                    if (mhtDatasets.ContainsKey("Factors") && (dataSetType == enmDataType.FACTORS))
                    {
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Factors"]);
                    }
                    Settings.Default.SessionFileName = null;
                    Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Either you cancelled the operation or an error ocurred." +
                        " Hint: check if the factor and dataset columns match.",
                        "Try again...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        void m_BackgroundWorker_OpenFiles(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = OpenFile((string)e.Argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File open failed: " + ex.Message, "Error!");
                e.Result = false;
                e.Cancel = true;
            }
        }
        #endregion

        #region Threading Events for Save/Open session

        void m_BackgroundWorker_SessionOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            //this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    ctltreeView.Nodes[0].Nodes.Clear();

                    IDictionaryEnumerator _enumerator = mhtDatasets.GetEnumerator();
                    while (_enumerator.MoveNext())
                    {
                        AddDataNode((clsDatasetTreeNode)_enumerator.Value);
                        
                    }
                                        
                    statusBarPanelMsg.Text = "Session opened successfully.";
                    if (mstrLoadedfileName != null)
                        this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
                }
                else
                    MessageBox.Show("Error in loading session.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_OpenSession(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = OpenSession((string)e.Argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File open failed: " + ex.Message, "Error!");
                e.Result = false;
                e.Cancel = true;
            }
        }

        void m_BackgroundWorker_SaveSessionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Hide();
            mfrmShowProgress.Close();
            //mfrmShowProgress.DialogResult = DialogResult.Cancel;
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                statusBarPanelMsg.Text = "Session saved successfully.";
            }
        }

        void m_BackgroundWorker_SaveSession(object sender, DoWorkEventArgs e)
        {
            string arg = (string)e.Argument;

            try
            {

                rConnector.rdcom.EvaluateNoReturn(arg);
                e.Result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RDCOM failed: " + ex.Message, "Error!");
                e.Result = false;
                e.Cancel = true;
            }
        }

        #endregion

        #region Threading events for plots --------------------------

        void m_BackgroundWorker_QQPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmQQplotDisplay mfrmQQplotDisplay = new frmQQplotDisplay(mclsQQPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmQQplotDisplay.Image = mclsPlotResult.mImage;
                mfrmQQplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmQQplotDisplay.DAnTEinstance = this;
                mfrmQQplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmQQplotDisplay.Title = "Q-Q Plot";
                mfrmQQplotDisplay.Show();
            }
        }

        void m_BackgroundWorker_HistPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmHistDisplay mfrmHistDisplay = new frmHistDisplay(mclsHistPar);
            //mfrmShowProgress.Hide();
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            //this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmHistDisplay.Image = mclsPlotResult.mImage;
                mfrmHistDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmHistDisplay.DAnTEinstance = this;
                mfrmHistDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmHistDisplay.Title = "Histograms";
                mfrmHistDisplay.Show();
            }
        }

        void m_BackgroundWorker_CorrPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmCorrDisplay mfrmCorrDisplay = new frmCorrDisplay(mclsCorrPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            //this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmCorrDisplay.Image = mclsPlotResult.mImage;
                mfrmCorrDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmCorrDisplay.DAnTEinstance = this;
                mfrmCorrDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmCorrDisplay.Title = "Correlations";
                mfrmCorrDisplay.Show();
            }
        }

        void m_BackgroundWorker_BoxPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmBoxPlotDisplay mfrmBoxPlotDisplay = new frmBoxPlotDisplay(mclsBoxPlotPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmBoxPlotDisplay.Image = mclsPlotResult.mImage;
                mfrmBoxPlotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmBoxPlotDisplay.DAnTEinstance = this;
                mfrmBoxPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmBoxPlotDisplay.Title = "Box Plots";
                mfrmBoxPlotDisplay.Show();
            }
        }

        void m_BackgroundWorker_MAplotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmMAplotDisplay mfrmMAplotDisplay = new frmMAplotDisplay(mclsMApar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmMAplotDisplay.Image = mclsPlotResult.mImage;
                mfrmMAplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmMAplotDisplay.DAnTEinstance = this;
                mfrmMAplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmMAplotDisplay.Title = "MA Plots";
                mfrmMAplotDisplay.Show();
            }
        }

        void m_BackgroundWorker_PCAPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataTable mDTpcaLoads = new DataTable();
            DataTable mDTplsLoads = new DataTable();
            string mstrType = "PCA";
            clsPlotResult mclsPlotResult;
            frmPCAPlotDisplay mfrmPCAPlotDisplay = new frmPCAPlotDisplay(mclsPCApar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmPCAPlotDisplay.Image = mclsPlotResult.mImage;
                mfrmPCAPlotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmPCAPlotDisplay.DAnTEinstance = this;
                mfrmPCAPlotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmPCAPlotDisplay.Title = "PCA/PLS Plot";
                mfrmPCAPlotDisplay.Show();
                try
                {
                    rConnector.rdcom.EvaluateNoReturn("Mode <- weights$Mode");
                    object pcmode = rConnector.rdcom.GetSymbol("Mode");
                    mstrType = (string)pcmode;

                    if (mstrType.Equals("PCA"))
                    {
                        rConnector.rdcom.EvaluateNoReturn("PCAweights <- weights$X");
                        if (rConnector.GetTableFromRmatrix("PCAweights"))
                        {
                            mDTpcaLoads = rConnector.mDataTable.Copy();
                            mDTpcaLoads.TableName = "PCAweights";
                            mDTpcaLoads.Columns[0].ColumnName = "ID";
                            rConnector.rdcom.EvaluateNoReturn("cat(\"PCA calculated.\n\")");
                            AddDataset2HashTable(mDTpcaLoads);
                            if (mhtDatasets.Contains("PCA Weights"))
                                AddDataNode((clsDatasetTreeNode)mhtDatasets["PCA Weights"]);
                        }
                    }
                    if (mstrType.Equals("PLS"))
                    {
                        rConnector.rdcom.EvaluateNoReturn("PLSweights <- weights$X");
                        if (rConnector.GetTableFromRmatrix("PLSweights"))
                        {
                            mDTplsLoads = rConnector.mDataTable.Copy();
                            mDTplsLoads.TableName = "PLSweights";
                            mDTplsLoads.Columns[0].ColumnName = "ID";
                            rConnector.rdcom.EvaluateNoReturn("cat(\"PLS calculated.\n\")");
                            AddDataset2HashTable(mDTplsLoads);
                            if (mhtDatasets.Contains("PLS Weights"))
                                AddDataNode((clsDatasetTreeNode)mhtDatasets["PLS Weights"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        void m_BackgroundWorker_VennCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmVennDisplay mfrmVennDisplay = new frmVennDisplay(mclsVennPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmVennDisplay.Image = mclsPlotResult.mImage;
                mfrmVennDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmVennDisplay.DAnTEinstance = this;
                mfrmVennDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmVennDisplay.Title = "Venn Diagram";
                mfrmVennDisplay.Show();
            }
        }

        void m_BackgroundWorker_HeatMapCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmHeatmapDisplay mfrmHmapDisplay = new frmHeatmapDisplay(mclsHeatmapPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Canceled");
            }
            else
            {
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmHmapDisplay.Image = mclsPlotResult.mImage;
                mfrmHmapDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmHmapDisplay.DAnTEinstance = this;
                mfrmHmapDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmHmapDisplay.Title = "Heatmap";
                mfrmHmapDisplay.Show();
                if (doClust)
                {
                    if (mhtDatasets.Contains("Heatmap Clusters"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Heatmap Clusters"]);
                }
            }
        }

        void m_BackgroundWorker_TamuQPlotCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            clsPlotResult mclsPlotResult;
            frmQQplotDisplay mfrmTamuQplotDisplay = new frmQQplotDisplay(mclsQQPar);
            mfrmShowProgress.Close();
            mfrmShowProgress.DialogResult = DialogResult.Cancel;
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                Console.WriteLine("Canceled");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                //mfrmPlot.Image = e.Result as Image;
                //DialogResult dres = mfrmPlot.ShowDialog();
                mclsPlotResult = (clsPlotResult)e.Result;
                mfrmTamuQplotDisplay.Image = mclsPlotResult.mImage;
                mfrmTamuQplotDisplay.PlotName = mclsPlotResult.mstrPlotName;
                mfrmTamuQplotDisplay.DAnTEinstance = this;
                mfrmTamuQplotDisplay.MdiParent = m_frmDAnTE.MdiParent;
                mfrmTamuQplotDisplay.Title = "Tamu-Q Plot";
                mfrmTamuQplotDisplay.Show();
            }
        }

        void m_BackgroundWorker_GeneratePlots(object sender, DoWorkEventArgs e)
        {
            clsRplotData arg = (clsRplotData)e.Argument;
            string rcmd = arg.mstrRcmd;
            string plotname = arg.mstrPlotName;
            clsPlotResult mclsPlotResult;

            try
            {

                rConnector.rdcom.EvaluateNoReturn(rcmd);
                mclsPlotResult = new clsPlotResult(LoadImage(tempFile), plotname);
                e.Result = mclsPlotResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RDCOM failed: " + ex.Message, "Error!");
                e.Result = null;
                e.Cancel = true;
                DeleteTempFile(tempFile);
            }
        }

        void m_BackgroundWorker_GenerateHeatmap(object sender, DoWorkEventArgs e)
        {
            DataTable mDTclusters = new DataTable();
            clsRplotData arg = (clsRplotData)e.Argument;
            string rcmd = arg.mstrRcmd;
            string plotname = arg.mstrPlotName;
            clsPlotResult mclsPlotResult;

            try
            {

                rConnector.rdcom.EvaluateNoReturn(rcmd);
                if (doClust)
                    if (rConnector.GetTableFromRvector("clusterResults"))
                    {
                        mDTclusters = rConnector.mDataTable.Copy();
                        mDTclusters.TableName = "clusterResults";
                        AddDataset2HashTable(mDTclusters);
                    }
                rConnector.rdcom.EvaluateNoReturn("cat(\"Heatmap done.\n\")");
                mclsPlotResult = new clsPlotResult(LoadImage(tempFile), plotname);
                e.Result = mclsPlotResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RDCOM failed: " + ex.Message, "Error!");
                e.Result = null;
                e.Cancel = true;
                DeleteTempFile(tempFile);
            }
        }

        #endregion

        #region Threading events for Statistics

        void m_BackgroundWorker_ANOVACompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("ANOVA Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("p-Values"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["p-Values"]);
                    if (mhtDatasets.Contains("Unused Data"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Unused Data"]);
                }
                else
                    MessageBox.Show("ANOVA failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void m_BackgroundWorker_TamuQCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("TamuQ Canceled", "Error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if ((bool)e.Result)
                {
                    if (mhtDatasets.Contains("p-Values"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["p-Values"]);
                    if (mhtDatasets.Contains("Imputed Values"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Imputed Values"]);
                    if (mhtDatasets.Contains("Unused Data"))
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Unused Data"]);
                }
                else
                    MessageBox.Show("TamuQ failed." + Environment.NewLine +
                        "Check if you have all data requirements and in correct format.", "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void m_BackgroundWorker_ANOVA(object sender, DoWorkEventArgs e)
        {
            e.Result = DoAnova((string)e.Argument);
        }

        void m_BackgroundWorker_TamuQ(object sender, DoWorkEventArgs e)
        {
            e.Result = DoTamuQ((string)e.Argument);
        }

        void m_BackgroundWorker_KW(object sender, DoWorkEventArgs e)
        {
            e.Result = DoKWtest((string)e.Argument);
        }

        void m_BackgroundWorker_Wilcox(object sender, DoWorkEventArgs e)
        {
            e.Result = DoWilcoxtest((string)e.Argument);
        }

        void m_BackgroundWorker_ShapiroWilks(object sender, DoWorkEventArgs e)
        {
            e.Result = DoShapiroWilkstest((string)e.Argument);
        }
        #endregion
    }

    #region Class for Plotting results
    class clsPlotResult
    {
        public Image mImage;
        public string mstrPlotName;

        public clsPlotResult()
        {
            mImage = null;
            mstrPlotName = null;
        }

        public clsPlotResult(Image image, string plot)
        {
            mImage = image;
            mstrPlotName = plot;
        }
    }

    class clsRplotData
    {
        public string mstrRcmd;
        public string mstrPlotName;

        public clsRplotData()
        {
            mstrPlotName = null;
            mstrRcmd = null;
        }

        public clsRplotData(string rcmd, string plotname)
        {
            mstrRcmd = rcmd;
            mstrPlotName = plotname;
        }
    }
    #endregion
}
