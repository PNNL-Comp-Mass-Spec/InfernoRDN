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
        public const bool USE_THREADED_LOAD = false;

        #region File Menu items

        private static FileTypeExtension mMostRecentFileType = FileTypeExtension.Txt;

        private void menuItemLoad_Click(object sender, EventArgs e)
        {
            var originator = ((ToolStripMenuItem)sender).Text;

            if (originator.Contains("Expression"))
                OpenExpressionFileCheckExisting();
            else
            {
                if (originator.Contains("Protein"))
                {
                    OpenProteinsFile();
                }
                else if (originator.Contains("Factor"))
                {
                    OpenFactorsFile();
                }
            }
        }

        /// <summary>
        /// Show a file dialog to allow the user to choose an expressions file to load
        /// </summary>
        protected void OpenExpressionFileCheckExisting()
        {
            if (mhtDatasets.ContainsKey("Expressions"))
            {
                MessageBox.Show("Expressions are already loaded.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            mDataSetType = enmDataType.ESET;
            mFileDialogTitle = "Open Expressions";

            ShowOpenFileWindow(mMostRecentFileType);

            if (string.IsNullOrWhiteSpace(mLoadedFilename))
            {
                return;
            }

            mMostRecentFileType = GetFileType(mLoadedFilename, mMostRecentFileType);

            OpenExpressionFile(mLoadedFilename);
        }

        private void OpenExpressionFile(string filePath)
        {
            mLoadedFilename = filePath;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (USE_THREADED_LOAD)
                DataFileOpenThreaded(mLoadedFilename, "Loading Expressions ...");
            else
            {
                mProgressForm.Reset("Loading Expressions ...");
                mProgressForm.Show();
                var success = OpenFile(mLoadedFilename);

                HandleFileOpenCompleted(!success, success, string.Empty);
            }
        }

        /// <summary>
        /// Show a file dialog to allow the user to choose an protein info file to load
        /// </summary>
        protected void OpenProteinsFile()
        {
            if (mhtDatasets.ContainsKey("Protein Info"))
            {
                MessageBox.Show("Protein information is already loaded.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            mDataSetType = enmDataType.PROTINFO;
            mFileDialogTitle = "Open Protein information";

            ShowOpenFileWindow(mMostRecentFileType);

            if (mLoadedFilename != null)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (USE_THREADED_LOAD)
                    DataFileOpenThreaded(mLoadedFilename, "Loading Protein Information ...");
                else
                {
                    mProgressForm.Reset("Loading Protein Information ...");
                    mProgressForm.Show();
                    var success = OpenFile(mLoadedFilename);

                    HandleFileOpenCompleted(!success, success, string.Empty);
                }

            }
        }

        /// <summary>
        /// Show a file dialog to allow the user to choose a factors file to load
        /// </summary>
        protected void OpenFactorsFile()
        {
            if (mhtDatasets.ContainsKey("Factors"))
            {
                MessageBox.Show("Factor information is already loaded.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            mDataSetType = enmDataType.FACTORS;
            mFileDialogTitle = "Open Factor information";

            ShowOpenFileWindow(mMostRecentFileType);

            if (mLoadedFilename == null)
                return;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (USE_THREADED_LOAD)
                DataFileOpenThreaded(mLoadedFilename, "Loading Factor Information ...");
            else
            {
                mProgressForm.Reset("Loading Factors ...");
                mProgressForm.Show();
                var success = OpenFile(mLoadedFilename);

                HandleFileOpenCompleted(!success, success, string.Empty);
            }
        }

        private void DataFileOpenThreaded(string filePath, string message)
        {
            #region Threading

            m_BackgroundWorker.DoWork += m_BackgroundWorker_OpenFiles;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_FileOpenCompleted;

            #endregion

            m_BackgroundWorker.RunWorkerAsync(filePath);
            mProgressForm.Reset(message);
            mProgressForm.ShowDialog();

            #region Threading

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_OpenFiles;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_FileOpenCompleted;

            #endregion
        }

        private void SessionFileOpenNonThreaded(string sessionFilePath)
        {
            mLoadedFilename = sessionFilePath;

            Settings.Default.SessionFileName = sessionFilePath;
            Settings.Default.Save();

            mProgressForm.Reset("Loading Saved Session ...");
            mProgressForm.Show();

            var success = false;
            var cancelled = false;
            var errorMessage = string.Empty;

            try
            {
                success = OpenSession(sessionFilePath);
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                cancelled = true;
            }

            SessionFileOpenFinalize(success, cancelled, errorMessage);
        }

        private void SessionFileOpenThreaded(string sessionFilePath)
        {
            #region Threading

            m_BackgroundWorker.DoWork += m_BackgroundWorker_OpenSession;
            m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SessionOpenCompleted;

            #endregion

            mLoadedFilename = sessionFilePath;

            Settings.Default.SessionFileName = sessionFilePath;
            Settings.Default.Save();

            marrAnalysisObjects.Clear();
            mhtAnalysisObjects.Clear();

            m_BackgroundWorker.RunWorkerAsync(sessionFilePath);
            mProgressForm.Reset("Loading Saved Session ...");
            mProgressForm.ShowDialog();

            #region Threading

            m_BackgroundWorker.DoWork -= m_BackgroundWorker_OpenSession;
            m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SessionOpenCompleted;

            #endregion
        }

        private void SessionFileOpenFinalize(bool success, bool cancelled, string errorMessage)
        {
            mProgressForm.Hide();

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                // Error occured; show the message
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (cancelled)
            {
                // User cancelled the operation
                Console.WriteLine("Cancelled");
            }
            else
            {
                // Load succeeded (or an exception occurred)
                if (success)
                {
                    ctlTreeView.Nodes[0].Nodes.Clear();

                    foreach (var dataset in mhtDatasets)
                    {
                        AddDataNode(dataset.Value);
                    }

                    statusBarPanelMsg.Text = "Session opened successfully.";
                    if (string.IsNullOrEmpty(mLoadedFilename))
                        Title = "Main - " + Path.GetFileName(mLoadedFilename);
                }
                else
                {
                    var message = "Error loading session file";

                    if (LastSessionLoadError.StartsWith("Value cannot be null",
                                                        StringComparison.OrdinalIgnoreCase))
                    {
                        message +=
                            ". Try loading the file again -- in some cases the first load attempt fails, but the second load attempt succeeds.";
                    }

                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuItemMSMS_Click(object sender, EventArgs e)
        {
            if (mhtDatasets.ContainsKey("Expressions"))
            {
                MessageBox.Show("Expressions are already loaded.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                var msmsWizard = new frmMSMSWizard(mRConnector);
                if (msmsWizard.ShowDialog(this) == DialogResult.OK)
                {
                    var spectraData = msmsWizard.SpectralDT;
                    AddDataset2HashTable(spectraData);

                    if (spectraData.Rows.Count != 0)
                    {
                        AddDataNode(mhtDatasets["Expressions"]);
                    }
                    else
                    {
                        MessageBox.Show("No data matches your parameters", "No Data");
                    }
                }
            }
        }

        private void mnuSaveSession_Click(object sender, EventArgs e)
        {
            if (mDataTab.Controls.Count != 0)
            {
                #region Threading

                m_BackgroundWorker.DoWork += m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SaveSessionCompleted;

                #endregion

                var success = true;

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
                    mLoadedFilename = Settings.Default.SessionFileName;
                    if (string.IsNullOrWhiteSpace(mLoadedFilename))
                    {
                        mFileDialogTitle = "Select a file to save the session";
                        ShowSaveFileWindow("Inferno (DAnTE) files (*.dnt)|*.dnt|All files (*.*)|*.*");
                    }

                    if (mLoadedFilename != null)
                    {
                        Settings.Default.SessionFileName = mLoadedFilename;
                        Settings.Default.Save();

                        var FileName = mLoadedFilename.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf('c'));
                        rcmd = "save(list=" + rcmd2 + ",file=\"" + FileName + "\")";

                        m_BackgroundWorker.RunWorkerAsync(rcmd);
                        mProgressForm.Reset("Saving Session ...");
                        mProgressForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Error occurred. Most likely while talking to R", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                #region Threading

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SaveSessionCompleted;

                #endregion
            }
        }

        private void mnuSaveSessionAs_Click(object sender, EventArgs e)
        {
            if (mDataTab.Controls.Count != 0)
            {
                #region Threading

                m_BackgroundWorker.DoWork += m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SaveSessionCompleted;

                #endregion

                var success = true;

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

                    mFileDialogTitle = "Select a file to save the session";
                    ShowSaveFileWindow("Inferno (DAnTE) files (*.dnt)|*.dnt|All files (*.*)|*.*");

                    if (mLoadedFilename != null)
                    {
                        Settings.Default.SessionFileName = mLoadedFilename;
                        Settings.Default.Save();

                        var fileName = mLoadedFilename.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf('c'));
                        rcmd = "save(list=" + rcmd2 + ",file=\"" + fileName + "\")";

                        m_BackgroundWorker.RunWorkerAsync(rcmd);
                        mProgressForm.Reset("Saving Session ...");
                        mProgressForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Error occurred. Most likely while talking to R", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                #region Threading

                m_BackgroundWorker.DoWork -= m_BackgroundWorker_SaveSession;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SaveSessionCompleted;

                #endregion
            }
        }


        private void mnuOpenSession_Click(object sender, EventArgs e)
        {
            var doRun = true;

            if (mDataTab.Controls.Count != 0)
            {
                var res = MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
                                          "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                    doRun = false;
            }

            if (!doRun)
            {
                return;
            }

            ShowOpenFileWindow("Inferno (DAnTE) files (*.dnt)|*.dnt|All files (*.*)|*.*");

            if (!string.IsNullOrWhiteSpace(mLoadedFilename))
            {
                SessionFileOpenNonThreaded(mLoadedFilename);
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedNodeTag = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

                if (mDataTab.Controls.Count != 0)
                {
                    var saveDialog = new SaveFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv|Tab delimited TXT files (*.txt)|*.txt|All files (*.*)|*.*",
                        FilterIndex = 1,
                        RestoreDirectory = true
                    };

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    var outputFile = new FileInfo(saveDialog.FileName);

                    using (
                        var writer =
                            new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write,
                                                            FileShare.Read)))
                    {
                        CsvWriter.WriteToStream(writer, selectedNodeTag.mDTable, true, false,
                                                outputFile.Extension.ToLower().Equals(".txt"));
                        statusBarPanelMsg.Text = "File saved successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving the data: " + ex.Message);
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
                mSessionFile = null;
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
                                             MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                             MessageBoxDefaultButton.Button3);
                if (exitOK == DialogResult.Yes)
                {
                    mLoadedFilename = null;
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
            var selectedTable = (clsDatasetTreeNode)ctlTreeView.SelectedNode.Tag;

            if (!mhtDatasets.ContainsKey("Protein Info"))
            {
                MessageBox.Show("No protein information found!", "Insufficient data", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            mFileDialogTitle = "Select a file to save";
            ShowSaveFileWindow("CSV files (*.csv)|*.csv|Tab delimited txt files (*.txt)|*.txt|" + "All files (*.*)|*.*");
            if (string.IsNullOrWhiteSpace(mLoadedFilename))
            {
                return;
            }

            var outputFile = new FileInfo(mLoadedFilename);

            if (mhtDatasets.ContainsKey(selectedTable.DataText))
            {
                SaveTableWithProteinIDs(selectedTable, outputFile);
            }
            else
            {
                // Match not found; this is unexpected; export the data using R
                var rcmd = "SaveWithProts(Data=" + selectedTable.RDatasetName + ",filename=\"" +
                           mLoadedFilename.Replace("\\", "/") + "\")";
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

        private void SaveTableWithProteinIDs(clsDatasetTreeNode selectedTable, FileSystemInfo outputFile)
        {
            try
            {
                // Write out the current dataset, but with the the protein info included
                var currentDataset = mhtDatasets[selectedTable.DataText];
                var proteinDataset = mhtDatasets["Protein Info"];

                // Store the proteins in a dictionary so that we can quickly lookup the info
                // Keys are row_id, values are Lists of protein info (protein name and any associated metadata)
                var proteinList = new Dictionary<string, List<clsProteinInfo>>();
                var proteinTableColCount = proteinDataset.mDTable.Columns.Count;

                foreach (DataRow row in proteinDataset.mDTable.Rows)
                {
                    // Column 0 is Row_ID
                    // Column 1 is ProteinID
                    // There may also be additional protein metadata, starting at column index 2

                    var rowID = row[0].ToString();
                    var proteinID = row[1].ToString();
                    var proteinInfo = new clsProteinInfo(proteinID);

                    for (var i = 2; i < proteinTableColCount; i++)
                    {
                        proteinInfo.AppendMetadata(row[i].ToString());
                    }

                    if (string.IsNullOrWhiteSpace(rowID) || string.IsNullOrWhiteSpace(proteinID))
                        continue;

                    if (proteinList.TryGetValue(rowID, out var proteinsForRow))
                    {
                        proteinsForRow.Add(proteinInfo);
                    }
                    else
                    {
                        proteinsForRow = new List<clsProteinInfo>
                        {
                            proteinInfo
                        };
                        proteinList.Add(rowID, proteinsForRow);
                    }
                }

                const bool quoteAll = false;
                var tabDelimited = outputFile.Extension.ToLower() == ".txt";

                using (
                    var writer =
                        new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write,
                                                        FileShare.Read)))
                {
                    var dataTableColCount = currentDataset.mDTable.Columns.Count;
                    var rowData = new List<string>(dataTableColCount + proteinTableColCount);

                    for (var i = 0; i < dataTableColCount; i++)
                    {
                        rowData.Add(currentDataset.mDTable.Columns[i].Caption);

                        if (i != 0)
                            continue;

                        // Add the Protein column headers just after the first column
                        rowData.Add("ProteinID");

                        for (var j = 2; j < proteinTableColCount; j++)
                        {
                            rowData.Add(proteinDataset.mDTable.Columns[j].Caption);
                        }
                    }

                    CsvWriter.WriteRow(writer, rowData, quoteAll, tabDelimited);

                    foreach (DataRow row in currentDataset.mDTable.Rows)
                    {
                        var rowID = row[0].ToString();
                        if (!proteinList.TryGetValue(rowID, out var proteinsForRow))
                        {
                            proteinsForRow = new List<clsProteinInfo>
                            {
                                new clsProteinInfo("Undefined")
                            };
                        }

                        foreach (var proteinItem in proteinsForRow)
                        {
                            rowData.Clear();

                            for (var j = 0; j < dataTableColCount; j++)
                            {
                                var itemText = string.Empty;

                                if (row[j] != null)
                                {
                                    itemText = row[j].ToString();
                                }

                                rowData.Add(itemText);

                                if (j != 0)
                                    continue;

                                // Add the Protein info just after the first column
                                rowData.Add(proteinItem.ProteinName);
                                rowData.AddRange(proteinItem.Metadata);
                            }

                            CsvWriter.WriteRow(writer, rowData, quoteAll, tabDelimited);
                        }
                    }

                    statusBarPanelMsg.Text = "File saved successfully.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving the data: " + ex.Message);
            }
        }

        private void menuItemDeleteColumns_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion
    }
}