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
        const bool USE_THREADED_LOAD = true;

        #region File Menu items

        private static FileTypeExtension mMostRecentFileType = FileTypeExtension.Txt;

        private void menuItemLoad_Click(object sender, System.EventArgs e)
        {
            string mstrOriginator = ((ToolStripMenuItem)sender).Text;

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
                            OpenFile(mstrLoadedfileName);
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
                        mMostRecentFileType = GetFileType(mstrLoadedfileName, mMostRecentFileType);
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
                        mMostRecentFileType = GetFileType(mstrLoadedfileName, mMostRecentFileType);
                        DataFileOpenThreaded(mstrLoadedfileName, "Loading Factor Information ...");
                    }
                }
            }
        }

        private void DataFileOpenThreaded(string filename, string message)
        {
            #region Threading
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_OpenFiles);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_FileOpenCompleted);
            #endregion

            m_BackgroundWorker.RunWorkerAsync(filename);
            mfrmShowProgress.Message = message;
            mfrmShowProgress.ShowDialog();

            #region Threading
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_OpenFiles);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_FileOpenCompleted);
            #endregion
        }

        #region Old code
        private void DataFileOpenThreaded_ORG(string mstrComingFrom)
        {
            #region Threading
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_OpenFiles);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_FileOpenCompleted);
            #endregion

            if (mstrComingFrom.Contains("Expression"))
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
                        //DataTable tmp1 = OpenFile_test(fileName);
                        //DataTable tmp2 = clsDataTable.RemoveDuplicateRows2(tmp1, "mass.tag.id");
                        m_BackgroundWorker.RunWorkerAsync(mstrLoadedfileName);
                        mfrmShowProgress.Message = "Loading Expressions ...";
                        mfrmShowProgress.ShowDialog();
                    }
                }
            }
            else if (mstrComingFrom.Contains("Protein"))
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
                        mMostRecentFileType = GetFileType(mstrLoadedfileName, mMostRecentFileType);
                        m_BackgroundWorker.RunWorkerAsync(mstrLoadedfileName);
                        mfrmShowProgress.Message = "Loading Protein Information ...";
                        mfrmShowProgress.ShowDialog();
                    }
                }
            }
            else if (mstrComingFrom.Contains("Factor"))
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
                        mMostRecentFileType = GetFileType(mstrLoadedfileName, mMostRecentFileType);
                        m_BackgroundWorker.RunWorkerAsync(mstrLoadedfileName);
                        mfrmShowProgress.Message = "Loading Factor Information ...";
                        mfrmShowProgress.ShowDialog();
                    }
                }
            }

            #region Threading
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_OpenFiles);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_FileOpenCompleted);
            #endregion
        }
        #endregion

        private void menuItemMSMS_Click(object sender, EventArgs e)
        {
            DataTable mDTspectral;
            if (mhtDatasets.ContainsKey("Expressions"))
                MessageBox.Show("Expressions are already loaded.");
            else
            {
                frmMSMSWizard msmsWizard = new frmMSMSWizard(rConnector);
                if (msmsWizard.ShowDialog(this) == DialogResult.OK)
                {
                    mDTspectral = msmsWizard.SpectralDT;
                    AddDataset2HashTable(mDTspectral);

                    if (!(mDTspectral.Rows.Count == 0))
                    {
                        AddDataNode((clsDatasetTreeNode)mhtDatasets["Expressions"]);
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
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_SaveSession);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_SaveSessionCompleted);
                #endregion

                bool success = true;
                string FileName = null;
                //SaveFileDialog saveFdlg = new SaveFileDialog();
                string rcmd1 = null, rcmd2 = null;
                string rcmd = "vars<-c(\"dummy\","; // dummy to make vars an array of strings always

                rcmd = rcmd + Vars2Save();

                rcmd = rcmd.Substring(0, rcmd.LastIndexOf(","));
                rcmd1 = rcmd + ")";

                try
                {
                    rConnector.EvaluateNoReturn("dummy<-0"); // create some dummy variable
                    rConnector.EvaluateNoReturn(rcmd1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("R.Net failed: " + ex.Message, "Error!");
                    success = false;
                }
                if (success)
                {
                    rcmd2 = rcmd + ",\"vars\")";
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

                        FileName = mstrLoadedfileName.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf("c"));
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
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_SaveSession);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_SaveSessionCompleted);
                #endregion
            }
        }

        private void mnuSaveSessionAs_Click(object sender, EventArgs e)
        {
            if (mtabControlData.Controls.Count != 0)
            {
                #region Threading
                m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_SaveSession);
                m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_SaveSessionCompleted);
                #endregion

                bool success = true;
                string FileName = null;
                //SaveFileDialog saveFdlg = new SaveFileDialog();
                string rcmd1 = null, rcmd2 = null;
                string rcmd = "vars<-c(\"dummy\","; // dummy to make vars an array of strings always

                rcmd = rcmd + Vars2Save();

                rcmd = rcmd.Substring(0, rcmd.LastIndexOf(","));
                rcmd1 = rcmd + ")";

                try
                {
                    rConnector.EvaluateNoReturn("dummy<-0"); // create some dummy variable
                    rConnector.EvaluateNoReturn(rcmd1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("R.Net failed: " + ex.Message, "Error!");
                    success = false;
                }
                if (success)
                {
                    rcmd2 = rcmd + ",\"vars\")";

                    mstrFldgTitle = "Select a file to save the session";
                    GetSaveFileName("DAnTE files (*.dnt)|*.dnt|All files (*.*)|*.*");

                    if (mstrLoadedfileName != null)
                    {
                        Settings.Default.SessionFileName = mstrLoadedfileName;
                        Settings.Default.Save();

                        FileName = mstrLoadedfileName.Replace("\\", "/");
                        rcmd2 = rcmd2.Substring(rcmd2.IndexOf("c"));
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
                m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_SaveSession);
                m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                    m_BackgroundWorker_SaveSessionCompleted);
                #endregion
            }
        }


        private void mnuOpenSession_Click(object sender, EventArgs e)
        {
            #region Threading
            m_BackgroundWorker.DoWork += new DoWorkEventHandler(m_BackgroundWorker_OpenSession);
            m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_SessionOpenCompleted);
            #endregion

            bool doRun = true;

            if (mtabControlData.Controls.Count != 0)
            {
                DialogResult res = MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
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
            m_BackgroundWorker.DoWork -= new DoWorkEventHandler(m_BackgroundWorker_OpenSession);
            m_BackgroundWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(
                m_BackgroundWorker_SessionOpenCompleted);
            #endregion
        }

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void menuItemSave_Click(object sender, System.EventArgs e)
        {
            clsDatasetTreeNode mclsSelected = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (mtabControlData.Controls.Count != 0)
            {
                SaveFileDialog saveFdlg = new SaveFileDialog();

                saveFdlg.Filter = "CSV files (*.csv)|*.csv|Tab delimited TXT files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFdlg.FilterIndex = 1;
                saveFdlg.RestoreDirectory = true;

                if (saveFdlg.ShowDialog() == DialogResult.OK)
                {
                    var outputFile = new FileInfo(saveFdlg.FileName);

                    using (var writer = new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    {
                        CsvWriter.WriteToStream(writer, mclsSelected.mDTable, true, false,
                            outputFile.Extension.ToLower().Equals(".txt"));
                        this.statusBarPanelMsg.Text = "File saved successfully.";
                    }
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // If the user canceled then exit.
            if (e.Cancel == true)
                return;

            if (OK2Exit())
            {
                sessionFile = null;
                e.Cancel = false;
                return;
            }
            else
            {
                e.Cancel = true;
                return;
            }

        } // End OnClosing()

        public bool OK2Exit()
        {
            DialogResult exitOK = DialogResult.Yes;
            if (mhtDatasets.Count > 0)
            {
                exitOK = MessageBox.Show("Are you sure?", "Confirm Close",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (exitOK == DialogResult.Yes)
                {
                    mstrLoadedfileName = null;
                    marrAnalysisObjects.Clear();
                    mhtAnalysisObjects.Clear();
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }

        private void mCtxtMenuSave_Click(object sender, EventArgs e)
        {
            var selectedTable = (clsDatasetTreeNode)ctltreeView.SelectedNode.Tag;

            if (!mhtDatasets.ContainsKey("Protein Info"))
            {
                MessageBox.Show("No protein information found!", "Insufficient data", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string rcmd = "SaveWithProts(Data=" + selectedTable.mstrRdatasetName + ",filename=\"" +
                   mstrLoadedfileName.Replace("\\", "/") + "\")";
                try
                {
                    rConnector.EvaluateNoReturn(rcmd);
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
            var currentDataset = (clsDatasetTreeNode)mhtDatasets[selectedTable.mstrDataText];
            var proteinDataset = (clsDatasetTreeNode)mhtDatasets["Protein Info"];

            // Store the proteins in a dictionary so that we can quickly lookup the info
            // Keys are row_id, values are Lists of protein names
            var proteinList = new Dictionary<string, List<string>>();

            foreach (DataRow row in proteinDataset.mDTable.Rows)
            {
                // Column 0 is Row_ID
                // Column 1 is ProteinID

                string rowID = row[0].ToString();
                string proteinID = row[1].ToString();

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
            bool tabDelimited = outputFile.Extension.ToLower() == ".txt";

            using (var writer = new StreamWriter(new FileStream(outputFile.FullName, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {

                int columnCount = currentDataset.mDTable.Columns.Count;
                var rowData = new List<string>(columnCount);

                for (int i = 0; i < columnCount; i++)
                {
                    rowData.Add(currentDataset.mDTable.Columns[i].Caption);

                    // Add the ProteinID just after the first column
                    if (i == 0)
                        rowData.Add("ProteinID");
                }

                CsvWriter.WriteRow(writer, rowData, quoteAll, tabDelimited);

                foreach (DataRow row in currentDataset.mDTable.Rows)
                {
                    string rowID = row[0].ToString();
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

                        for (int j = 0; j < columnCount; j++)
                        {
                            string itemText = string.Empty;

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

