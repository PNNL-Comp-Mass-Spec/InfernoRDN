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

        void m_BackgroundWorker_FileOpenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool mblFileOpenResult = false;

            mfrmShowProgress.Close();
            this.Focus();
            if (e.Error != null)
            {
                mblFileOpenResult = false;
                MessageBox.Show(e.Error.Message);
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
                        " Try again...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
