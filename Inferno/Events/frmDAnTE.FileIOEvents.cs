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
        #region Threading events for FileOpen --------------------------

        private void m_BackgroundWorker_FileOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var errorMessage = string.Empty;
            var openCancelled = false;
            var openSuccess = false;

            if (e.Error != null)
            {
                errorMessage = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                openCancelled = true;
            }
            else
            {
                openSuccess = (bool)e.Result;
            }

            HandleFileOpenCompleted(openCancelled, openSuccess, errorMessage);

        }

        public void HandleFileOpenCompleted(bool openCancelled, bool openSuccess, string errorMessage)
        {
            mfrmShowProgress.Hide();

            this.Focus();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            if (openCancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                Console.WriteLine("Cancelled");
                return;
            }

            if (!openSuccess)
            {
                MessageBox.Show("Either you cancelled the operation or an error ocurred." +
                                " Hint: check if the factor and dataset columns match.",
                                " Try again...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mhtDatasets.ContainsKey("Expressions") && (dataSetType == enmDataType.ESET))
            {
                if (mhtDatasets.ContainsKey("Protein Info"))
                {
                    AddDataNode((clsDatasetTreeNode)mhtDatasets["Protein Info"]);
                }
                AddDataNode((clsDatasetTreeNode)mhtDatasets["Expressions"]);
                if (mstrLoadedfileName != null)
                {
                    this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
                }
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
    }
}
