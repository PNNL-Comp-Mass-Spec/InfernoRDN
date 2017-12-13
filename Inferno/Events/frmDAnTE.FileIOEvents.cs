using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using DAnTE.Tools;
using DAnTE.Properties;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        #region Threading events for FileOpen

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
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            if (mhtDatasets.ContainsKey("Expressions") && (mDataSetType == enmDataType.ESET))
            {
                if (mhtDatasets.ContainsKey("Protein Info"))
                {
                    AddDataNode(mhtDatasets["Protein Info"]);
                }

                AddDataNode(mhtDatasets["Expressions"]);
                if (!string.IsNullOrWhiteSpace(mstrLoadedfileName))
                {
                    this.Title = "Main - " + Path.GetFileName(mstrLoadedfileName);
                }
                mDataSetNames = clsDataTable.DataTableColumns(
                    (mhtDatasets["Expressions"]).mDTable, true);
            }

            if (mhtDatasets.ContainsKey("Protein Info") && (mDataSetType == enmDataType.PROTINFO))
            {
                AddDataNode(mhtDatasets["Protein Info"]);
            }

            if (mhtDatasets.ContainsKey("Factors") && (mDataSetType == enmDataType.FACTORS))
            {
                AddDataNode(mhtDatasets["Factors"]);
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