using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        // When this is True, we get intermittent file load errors in OpenSession(), with rConnector reporting error "Value cannot be null"
        const bool USE_THREADED_LOAD = false;

        #region File Menu items

        private static FileTypeExtension mMostRecentFileType = FileTypeExtension.Txt;

        private void menuItemLoad_Click(object sender, EventArgs e)
        {
            var mstrOriginator = ((ToolStripMenuItem)sender).Text;

            if (mstrOriginator.Contains("Expression"))
            {
                if (mhtDatasets.ContainsKey("Expressions"))
                    MessageBox.Show("Expressions are already loaded.");
                else
                {
                    dataSetType = enmDataType.ESET;
                    mstrFldgTitle = "Open Expressions";

                    GetOpenFileName(mMostRecentFileType);
                    if (mstrLoadedfileName != null)
                    {
                        mMostRecentFileType = GetFileType(mstrLoadedfileName, mMostRecentFileType);

                        if (USE_THREADED_LOAD)
                            DataFileOpenThreaded(mstrLoadedfileName, "Loading Expressions ...");
                        else
                        {
                            mfrmShowProgress.Message = "Loading Expressions ...";
                            mfrmShowProgress.Show();
                            var success = OpenFile(mstrLoadedfileName);

                            this.HandleFileOpenCompleted(!success, success, string.Empty);
                        }

                    }
                }
            }
            else if (mstrOriginator.Contains("Protein"))
            {
                if (mhtDatasets.ContainsKey("Protein Info"))
                    MessageBox.Show("Protein information is already loaded.");
                else
                {
                    dataSetType = enmDataType.PROTINFO;
                    mstrFldgTitle = "Open Protein information";
                    GetOpenFileName(mMostRecentFileType);
                    if (mstrLoadedfileName != null)
                    {
                        DataFileOpenThreaded(mstrLoadedfileName, "Loading Protein Information ...");
                    }
                }
            }
            else if (mstrOriginator.Contains("Factor"))
            {
                if (mhtDatasets.ContainsKey("Factors"))
                    MessageBox.Show("Factor information is already loaded.");
                else
                {
                    dataSetType = enmDataType.FACTORS;
                    mstrFldgTitle = "Open Factor information";
                    GetOpenFileName(mMostRecentFileType);
                    if (mstrLoadedfileName != null)
                    {
                        DataFileOpenThreaded(mstrLoadedfileName, "Loading Factor Information ...");
                    }
                }
            }
        }

        private void DataFileOpenThreaded(string filename, string message)
        {
            #region Threading
            m_BackgroundWorker.DoWork += m_BackgroundWorker_OpenFiles;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_FileOpenCompleted;
            #endregion

            m_BackgroundWorker.RunWorkerAsync(filename);
            mfrmShowProgress.Message = message;
            mfrmShowProgress.ShowDialog();

            #region Threading
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_OpenFiles;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_FileOpenCompleted;
            #endregion
        }

        private void menuItemMSMS_Click(object sender, EventArgs e)
        {
            if (mhtDatasets.ContainsKey("Expressions"))
                MessageBox.Show("Expressions are already loaded.");
            else
            {
                var msmsWizard = new frmMSMSWizard(mRConnector);
                if (msmsWizard.ShowDialog(this) == DialogResult.OK)
                {
                    var mDTspectral = msmsWizard.SpectralDT;
                    AddDataset2HashTable(mDTspectral);

                    if (mDTspectral.Rows.Count != 0)
                    {
                        AddDataNode(mhtDatasets["Expressions"]);
                    }
                    else
                        MessageBox.Show("No data matches your parameters", "No Data");
                }
            }
        }

        private void mnuSaveSession_Click(object sender, EventArgs e)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Threading
                m_BackgroundWorker.DoWork += m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SaveSessionCompleted;
                #endregion

                var success = true;
                
                //SaveFileDialog saveFdlg = new SaveFileDialog();
                var rcmd = "vars<-c(\"dummy\","; // dummy to make vars an array of strings always

                rcmd = rcmd + Vars2Save();

                rcmd = rcmd.Substring(0, rcmd.LastIndexOf(','));
                var rcmd1 = rcmd + ")";

                try
                {
                    mRConnector.EvaluateNoReturn("dummy<-0"); // create some dummy variable
                    mRConnector.EvaluateNoReturn(rcmd1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("R.Net failed: " + ex.Message, "Error!");
                    success = false;
                }
                if (success)
                {
                    var rcmd2 = rcmd + ",\"vars\")";
                    mstrLoadedfileName = Settings.Default.SessionFileName;
                    if (string.IsNullOrWhiteSpace(mstrLoadedfileName))
                    {
                        mstrFldgTitle = "Select a file to save the session";
                        GetSaveFileName("DAnTE files (*.dnt)|*.dnt|All files (*.*)|*.*");
                    }

                    if (mstrLoadedfileName != null)
                    {
                        Settings.Default.SessionFileName = mstrLoadedfileName;
                        Settings.Default.Save();

                        var FileName = mstrLoadedfileName.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf('c'));
                        rcmd = "save(list=" + rcmd2 + ",file=\"" + FileName + "\")";

                        m_BackgroundWorker.RunWorkerAsync(rcmd);
                        mfrmShowProgress.Message = "Saving Session ...";
                        mfrmShowProgress.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Error ocurred. Most likely while talking to R", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                #region Threading
                m_BackgroundWorker.DoWork -= m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SaveSessionCompleted;
                #endregion
            }
        }

        private void mnuSaveSessionAs_Click(object sender, EventArgs e)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Threading
                m_BackgroundWorker.DoWork += m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SaveSessionCompleted;
                #endregion

                var success = true;
                //SaveFileDialog saveFdlg = new SaveFileDialog();
                var rcmd = "vars<-c(\"dummy\","; // dummy to make vars an array of strings always

                rcmd = rcmd + Vars2Save();

                rcmd = rcmd.Substring(0, rcmd.LastIndexOf(','));
                var rcmd1 = rcmd + ")";

                try
                {
                    mRConnector.EvaluateNoReturn("dummy<-0"); // create some dummy variable
                    mRConnector.EvaluateNoReturn(rcmd1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("R.Net failed: " + ex.Message, "Error!");
                    success = false;
                }
                if (success)
                {
                    var rcmd2 = rcmd + ",\"vars\")";

                    mstrFldgTitle = "Select a file to save the session";
                    GetSaveFileName("DAnTE files (*.dnt)|*.dnt|All files (*.*)|*.*");

                    if (mstrLoadedfileName != null)
                    {
                        Settings.Default.SessionFileName = mstrLoadedfileName;
                        Settings.Default.Save();

                        var fileName = mstrLoadedfileName.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf('c'));
                        rcmd = "save(list=" + rcmd2 + ",file=\"" + fileName + "\")";

                        m_BackgroundWorker.RunWorkerAsync(rcmd);
                        mfrmShowProgress.Message = "Saving Session ...";
                        mfrmShowProgress.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Error ocurred. Most likely while talking to R", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                #region Threading
                m_BackgroundWorker.DoWork -= m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SaveSessionCompleted;
                #endregion
            }
        }


        private void mnuOpenSession_Click(object sender, EventArgs e)
        {
            #region Threading
            m_BackgroundWorker.DoWork += m_BackgroundWorker_OpenSession;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SessionOpenCompleted;
            #endregion

            var doRun = true;

            if (mtabControlData.Controls.Count != 0)
            {
                var res = MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                    doRun = false;
            }

            if (doRun)
            {
                GetOpenFileName("DAnTE files (*.dnt)|*.dnt|All files (*.*)|*.*");

                if (mstrLoadedfileName != null)
                {
                    Settings.Default.SessionFileName = mstrLoadedfileName;
                    Settings.Default.Save();
                    marrAnalysisObjects.Clear();
                    mhtAnalysisObjects.Clear();
                    m_BackgroundWorker.RunWorkerAsync(mstrLoadedfileName);
                    mfrmShowProgress.Message = "Loading Saved Session ...";
                    mfrmShowProgress.ShowDialog();
                }
            }
            #region Threading
            m_BackgroundWorker.DoWork -= m_BackgroundWorker_OpenSession;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SessionOpenCompleted;
            #endregion
        }

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void menuItemSave_Click(object sender, System.EventArgs e)
        {
            var mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (mtabControlData.Controls.Count != 0)
            {
                var saveFdlg = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|Tab delimited TXT files (*.txt)|*.txt|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (saveFdlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var outputFile = new FileInfo(saveFdlg.FileName);

                using (var writer = new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Read)))
                {
                    CsvWriter.WriteToStream(writer, mclsSelected.mDTable, true, false,
                                            outputFile.Extension.ToLower().Equals(".txt"));
                    this.statusBarPanelMsg.Text = "File saved successfully.";
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // If the user canceled then exit.
            if (e.Cancel)
                return;

            if (OK2Exit())
            {
                sessionFile = null;
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }

        public bool OK2Exit()
        {
            if (mhtDatasets.Count > 0)
            {
                var exitOK = MessageBox.Show("Are you sure you want to exit?", "Confirm Close",
                                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (exitOK == DialogResult.Yes)
                {
                    mstrLoadedfileName = null;
                    marrAnalysisObjects.Clear();
                    mhtAnalysisObjects.Clear();
                    return true;
                }
                return false;
            }

            return true;
        }

        private void mCtxtMenuSave_Click(object sender, EventArgs e)
        {
            var selectedTable = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!mhtDatasets.ContainsKey("Protein Info"))
            {
                MessageBox.Show("No protein information found!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mstrFldgTitle = "Select a file to save";
            GetSaveFileName("CSV files (*.csv)|*.csv|Tab delimited txt files (*.txt)|*.txt|" + "All files (*.*)|*.*");
            if (string.IsNullOrWhiteSpace(mstrLoadedfileName))
            {
                return;
            }

            var outputFile = new FileInfo(mstrLoadedfileName);

            if (mhtDatasets.ContainsKey(selectedTable.mstrDataText))
            {
                SaveTableWithProteinIDs(selectedTable, outputFile);
            }
            else
            {
                // Match not found; this is unexpected; export the data using R
                var rcmd = "SaveWithProts(Data=" + selectedTable.mstrRdatasetName + ",filename=\"" + mstrLoadedfileName.Replace("\\", "/") + "\")";
                try
                {
                    mRConnector.EvaluateNoReturn(rcmd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("R.Net failed: " + ex.Message, "Error!");
                }
            }


        }

        private void SaveTableWithProteinIDs(clsDatasetTreeNode selectedTable, FileInfo outputFile)
        {
            // Write out the current dataset, but with the the protein info included
            var currentDataset = mhtDatasets[selectedTable.mstrDataText];
            var proteinDataset = mhtDatasets["Protein Info"];

            // Store the proteins in a dictionary so that we can quickly lookup the info
            // Keys are row_id, values are Lists of protein names
            var proteinList = new Dictionary<string, List<string>>();

            foreach (DataRow row in proteinDataset.mDTable.Rows)
            {
                // Column 0 is Row_ID
                // Column 1 is ProteinID

                var rowID = row[0].ToString();
                var proteinID = row[1].ToString();

                if (string.IsNullOrWhiteSpace(rowID) || string.IsNullOrWhiteSpace(proteinID))
                    continue;

                List<string> proteinsForRow;
                if (proteinList.TryGetValue(rowID, out proteinsForRow))
                {
                    proteinsForRow.Add(proteinID);
                }
                else
                {
                    proteinsForRow = new List<string>
                        {
                            proteinID
                        };
                    proteinList.Add(rowID, proteinsForRow);
                }
            }

            const bool quoteAll = false;
            var tabDelimited = outputFile.Extension.ToLower() == ".txt";

            using (var writer = new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {

                var columnCount = currentDataset.mDTable.Columns.Count;
                var rowData = new List<string>(columnCount);

                for (var i = 0; i < columnCount; i++)
                {
                    rowData.Add(currentDataset.mDTable.Columns[i].Caption);

                    // Add the ProteinID just after the first column
                    if (i == 0)
                        rowData.Add("ProteinID");
                }

                CsvWriter.WriteRow(writer, rowData, quoteAll, tabDelimited);

                foreach (DataRow row in currentDataset.mDTable.Rows)
                {
                    var rowID = row[0].ToString();
                    List<string> proteinsForRow;
                    if (!proteinList.TryGetValue(rowID, out proteinsForRow))
                    {
                        proteinsForRow = new List<string>
                            {
                                "Undefined"
                            };
                    }

                    foreach (var proteinID in proteinsForRow)
                    {
                        rowData.Clear();

                        for (var j = 0; j < columnCount; j++)
                        {
                            var itemText = string.Empty;

                            if (row[j] != null)
                            {
                                itemText = row[j].ToString();
                            }

                            rowData.Add(itemText);

                            // Add the ProteinID just after the first column
                            if (j == 0)
                                rowData.Add(proteinID);
                        }
                        CsvWriter.WriteRow(writer, rowData, quoteAll, tabDelimited);
                    }

                }

                this.statusBarPanelMsg.Text = "File saved successfully.";
            }

        }

        private void menuItemDeleteColumns_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented");
        }

        #endregion

    }
}

