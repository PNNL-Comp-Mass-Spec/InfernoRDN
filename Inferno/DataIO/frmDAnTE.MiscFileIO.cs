using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DAnTE.Paradiso;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        private enum FileTypeExtension
        {
            Txt = 0,
            Csv = 1,
            Xls = 2,
            Xlsx = 3
        }

        private bool mProgressEventWired;

        private bool DeleteTempFile(string tempfile)
        {
            var ok = true;
            tempfile = tempfile.Replace("/", @"\");

            if (File.Exists(tempfile))
            {
                try
                {
                    mRConnector.EvaluateNoReturn("graphics.off()");
                    File.Delete(tempfile);
                }
                catch (Exception ex)
                {
                    ok = false;
                    Console.WriteLine(ex.Message);
                }
            }
            return ok;
        }

        private FileTypeExtension GetFileType(string filePath, FileTypeExtension defaultValue)
        {
            var fileExtension = Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileExtension))
                return defaultValue;

            if (fileExtension.StartsWith("."))
                fileExtension = fileExtension.Substring(1);

            foreach (var enumName in Enum.GetNames(typeof(FileTypeExtension)))
            {
                if (string.Equals(enumName, fileExtension, StringComparison.CurrentCultureIgnoreCase))
                {
                    var enumValue = (FileTypeExtension)Enum.Parse(typeof(FileTypeExtension), enumName);
                    return enumValue;
                }

            }

            return defaultValue;
        }

        /// <summary>
        /// Get the Image from file "tempFile" and delete the tempFile
        /// </summary>
        private Image LoadImage(string tempFile)
        {
            Image currImg;
            using (var fs = new FileStream(tempFile, FileMode.Open,
                                FileAccess.Read, FileShare.ReadWrite))
            {
                var img = Image.FromStream(fs);
                fs.Close();
                currImg = img.Clone() as Image;
                img.Dispose();
                File.Delete(tempFile);
            }
            return currImg;
        }

        private void GetOpenFileName(FileTypeExtension preferredFileType)
        {
            var fileTypesUsed = new List<FileTypeExtension>
            {
                FileTypeExtension.Txt,
                FileTypeExtension.Csv,
                FileTypeExtension.Xls,
                FileTypeExtension.Xlsx
            };

            var filter = GetFilterSpec(preferredFileType);

            foreach (var fileType in fileTypesUsed)
            {
                if (fileType != preferredFileType)
                    filter += GetFilterSpec(fileType);
            }

            filter += "All files (*.*)|*.*";

            GetOpenFileName(filter);
        }

        /// <summary>
        /// Fireup a file open dialog and get the file name to open.
        /// </summary>
        private void GetOpenFileName(string filter)
        {
            const string WORKING_FOLDER = "Working_Directory";

            var workingFolder = Settings.Default.WorkingFolder;

            if (string.IsNullOrWhiteSpace(workingFolder))
            {
                workingFolder = RegistryAccess.GetStringRegistryValue(WORKING_FOLDER, "");
            }

            if (!string.IsNullOrWhiteSpace(workingFolder))
            {
                try
                {
                    var diWorkingFolder = new DirectoryInfo(workingFolder);
                    while (!diWorkingFolder.Exists)
                    {
                        if (diWorkingFolder.Parent == null)
                        {
                            workingFolder = string.Empty;
                            break;
                        }
                        diWorkingFolder = diWorkingFolder.Parent;
                        workingFolder = diWorkingFolder.FullName;
                    }
                }
                catch
                {
                    workingFolder = string.Empty;
                }
            }

            var fdlg = new OpenFileDialog
            {
                Title = mstrFldgTitle
            };

            if (!string.IsNullOrWhiteSpace(workingFolder))
            {
                fdlg.InitialDirectory = workingFolder;
            }
            else
            {
                fdlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            fdlg.Filter = filter;
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = false;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                mstrLoadedfileName = fdlg.FileName;

                var fiDataFile = new FileInfo(mstrLoadedfileName);
                if (fiDataFile.Exists)
                {
                    mstrLoadedfileName = fiDataFile.FullName;

                    workingFolder = fiDataFile.Directory.FullName;
                    Settings.Default.WorkingFolder = workingFolder;
                    Settings.Default.DataFileName = fiDataFile.Name;
                    Settings.Default.Save();

                    RegistryAccess.SetStringRegistryValue(WORKING_FOLDER, workingFolder);

                }
            }
            else
                mstrLoadedfileName = null;
        }

        private string GetFilterSpec(FileTypeExtension fileType)
        {
            switch (fileType)
            {
                case FileTypeExtension.Txt:
                    return "Tab delimited txt files (*.txt)|*.txt|";
                case FileTypeExtension.Csv:
                    return "CSV files (*.csv)|*.csv|";
                case FileTypeExtension.Xls:
                    return "Excel files (*.xls)|*.xls|";
                case FileTypeExtension.Xlsx:
                    return "Excel 2007 files (*.xlsx)|*.xlsx|";
                default:
                    throw new ArgumentException("fileType", "Unrecognized value for fileType: " + fileType);
            }
        }

        private void GetSaveFileName(string filter)
        {
            var workingFolder = Settings.Default.WorkingFolder;

            var fdlg = new SaveFileDialog
            {
                Title = mstrFldgTitle
            };

            if (workingFolder != null)
                fdlg.InitialDirectory = workingFolder;
            else
                fdlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fdlg.Filter = filter;
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = false;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                mstrLoadedfileName = fdlg.FileName;
                workingFolder = Path.GetDirectoryName(mstrLoadedfileName);
                Settings.Default.WorkingFolder = workingFolder;
                Settings.Default.Save();
            }
            else
                mstrLoadedfileName = null;
        }

        /// <summary>
        /// Creates a new datatable with first column having unique rowIDs and
        /// multiple columns with data
        /// </summary>
        /// <param name="mDT"></param>
        /// <param name="RowID"></param>
        /// <param name="DataCols"></param>
        /// <returns></returns>
        private DataTable ArrangeDataTable(DataTable mDT, string RowID, string[] DataCols)
        {
            var dtEset = mDT.Copy();
            var columns = dtEset.Columns;
            Array.Sort(DataCols);
            var Cols2Rem = new string[dtEset.Columns.Count - DataCols.Length - 1];

            var z = 0;
            foreach (DataColumn column in columns)
            {
                if (Array.BinarySearch(DataCols, column.ColumnName) < 0 &&
                    !column.ColumnName.Equals(RowID))
                    Cols2Rem[z++] = column.ColumnName;
            }
            foreach (var s in Cols2Rem)
            {
                dtEset.Columns.Remove(s);
            }
            return dtEset;
        }

        /// <summary>
        /// Overloaded method of the above to create a table with only two columns.
        /// </summary>
        /// <param name="mDT"></param>
        /// <param name="RowID"></param>
        /// <param name="Cols"></param>
        /// <returns></returns>
        private DataTable ArrangeDataTable(DataTable mDT, string RowID, string Cols)
        {
            var dtEset = mDT.Copy();
            var columns = dtEset.Columns;
            var Cols2Rem = new string[dtEset.Columns.Count - 2];

            var z = 0;
            foreach (DataColumn column in columns)
            {
                if (!Cols.Equals(column.ColumnName) && !RowID.Equals(column.ColumnName))
                    Cols2Rem[z++] = column.ColumnName;
            }
            foreach (var s in Cols2Rem)
            {
                dtEset.Columns.Remove(s);
            }
            return dtEset;
        }


        /// <summary>
        /// Rearrange the protein info columns in a datatable,
        /// then send it to R and 'clean' it.
        /// Finally get the 'cleaned' data back
        /// </summary>
        /// <param name="mDT"></param>
        /// <param name="protIPI"></param>
        /// <param name="rowID"></param>
        /// <returns></returns>
        private DataTable LoadProtColumns(DataTable mDT, string protIPI, string rowID)
        {
            DataTable mDTProts;

            try
            {
                mDTProts = ArrangeDataTable(mDT, rowID, protIPI);
                mDTProts.TableName = "ProtInfo";
                mDTProts.Columns[0].ColumnName = "Row_ID";
                mstrArrProteins = clsDataTable.DataColumn2strArray(mDT, protIPI);
                mstrArrMassTags = clsDataTable.DataColumn2strArray(mDT, rowID);
                try
                {
                    mRConnector.SetSymbolCharVector("protIPI", mstrArrProteins);
                    mRConnector.SetSymbolCharVector("MassTags", mstrArrMassTags);
                    var rcmd = "ProtInfo<-data.frame(Row_ID=MassTags,ProteinID=protIPI)";
                    mRConnector.EvaluateNoReturn(rcmd);
                    if (mhtDatasets.ContainsKey("Expressions"))
                        rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo, checkEset=TRUE)";
                    else
                        rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo)";
                    mRConnector.EvaluateNoReturn(rcmd);

                    clsRarray.rowNamesID = "RowID"; // Otherwise this will conflict with 'Row_ID'
                    if (mRConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
                    {
                        clsRarray.rowNamesID = "Row_ID";
                        mDTProts = mRConnector.DataTable.Copy();
                        mDTProts.TableName = "ProtInfo";
                        if (mDTProts.Columns.CanRemove(mDTProts.Columns[0]))
                        {//remove the line number column which comes from R row names.
                            mDTProts.Columns.Remove(mDTProts.Columns[0]);
                            mDTProts.Columns[0].ColumnName = "Row_ID";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                    mDTProts = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "Exception while extracting data columns");
                mDTProts = null;
            }
            return mDTProts;
        }

        private DataTable GetProtInfoMatrix()
        {
            DataTable mDTProtInfo;

            clsRarray.rowNamesID = "RowID"; // Otherwise this will conflict with 'Row_ID'
            if (mRConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
            {
                clsRarray.rowNamesID = "Row_ID";
                mDTProtInfo = mRConnector.DataTable.Copy();
                mDTProtInfo.TableName = "ProtInfo";
                if (mDTProtInfo.Columns.CanRemove(mDTProtInfo.Columns[0]))
                {//remove the line number column which comes from R row names.
                    mDTProtInfo.Columns.Remove(mDTProtInfo.Columns[0]);
                    mDTProtInfo.Columns[0].ColumnName = "Row_ID";
                }
            }
            else
                mDTProtInfo = null;
            return mDTProtInfo;
        }

        private DataTable OpenFile_test(string filename)
        {
            //bool success = true;
            //bool FactorsValid = true;

            var dTselectedEset1 = new DataTable();

            // check that the file exists before opening it
            if (!File.Exists(mstrLoadedfileName))
            {
                return dTselectedEset1;
            }

            var fExt = Path.GetExtension(filename);
            if (!ValidExtension(fExt))
            {
                MessageBox.Show("Filetype not allowed.", "Error!");
                return null;
            }

            //FactorsValid = true;
            var mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
            if (mDTloaded == null)
            {
                return null;
            }
            mDTloaded.TableName = "AllEset";
            //Select columns
            var mfrmSelectCols = new frmSelectColumns
            {
                PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false),
                Proteins = mhtDatasets.ContainsKey("Protein Info")
            };

            if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
            {
                var rowID = mfrmSelectCols.RowIDColumn;
                var dataCols = mfrmSelectCols.DataColumns;
                try
                {
                    dTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols); // create the datatable
                    dTselectedEset1.TableName = "Eset";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                                    "Exception while extracting data columns");
                    //success = false;
                    return null;
                }
            }
            return dTselectedEset1;
        }


        /// <summary>
        /// Main routine that opens data files (Expressions, Factors)
        /// in comma or tab delimited CSVs (and Excel as a future possibility).
        /// The main file load routine is in clsDataTable.LoadFile2DataTable(fileName)
        /// Duplicates are handled in C#
        /// </summary>
        private bool OpenFile(string filename)
        {
            string rowID;
            string rcmd;
            var success = true;
            bool FactorsValid;

            // check that the file exists before opening it
            if (!File.Exists(mstrLoadedfileName))
            {
                return false;
            }

            var fExt = Path.GetExtension(filename);
            if (string.IsNullOrWhiteSpace(fExt))
            {
                MessageBox.Show("File does not have an extension (like .csv or .txt); unable to determine file type", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ValidExtension(fExt))
            {
                MessageBox.Show("Filetype not allowed (must be csv, txt, xls, or xlsx)", "Error!");
                return false;
            }

            switch (dataSetType)
            {
                case enmDataType.ESET:

                    #region Load Expressions

                    FactorsValid = true;

                    if (!mProgressEventWired)
                    {
                        clsDataTable.OnProgress += clsDataTable_OnProgress;
                        mProgressEventWired = true;
                    }

                    var mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTloaded == null)
                    {
                        return false;
                    }
                    mDTloaded.TableName = "AllEset";
                    //Select columns
                    var mfrmSelectCols = new frmSelectColumns
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false),
                        Proteins = mhtDatasets.ContainsKey("Protein Info")
                    };

                    if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
                    {
                        rowID = mfrmSelectCols.RowIDColumn; //mass tags
                        var dataCols = mfrmSelectCols.DataColumns;
                        try
                        {
                            var mDTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols);
                            mDTselectedEset1.Columns[0].ColumnName = "Row_ID";
                            mDTselectedEset1 = clsDataTable.RemoveDuplicateRows2(mDTselectedEset1,
                                                                                 mDTselectedEset1.Columns[0].ColumnName);
                            // handle duplicate rows
                            mDTselectedEset1.TableName = "Eset";
                            success = mRConnector.SendTable2RmatrixNumeric("Eset", mDTselectedEset1);
                            if (mhtDatasets.ContainsKey("Factors"))
                            {
                                rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
                                mRConnector.EvaluateNoReturn(rcmd);
                                FactorsValid = mRConnector.GetSymbolAsBool("FactorsValid");
                            }
                            if (!FactorsValid)
                            {
                                success = false;
                            }
                            if (success)
                            {
                                AddDataset2HashTable(mDTselectedEset1);
                                mRConnector.EvaluateNoReturn("print(dim(Eset))");
                                mRConnector.EvaluateNoReturn("cat(\"Expressions loaded.\n\")");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message,
                                            "Exception while extracting data columns");
                            return false;
                        }

                        if (mfrmSelectCols.Proteins && success) // Protein info needs to be loaded ?
                        {
                            // loads to mDataTableProtInfo and then sends to R
                            var mdtProts = LoadProtColumns(mDTloaded, mfrmSelectCols.ProteinIDColumn, rowID);
                            mdtProts.TableName = "ProtInfo";
                            AddDataset2HashTable(mdtProts);
                        }
                    }
                    else
                    {
                        success = false;
                    }

                    #endregion

                    break;
                case enmDataType.PROTINFO:

                    #region Load Protein Info

                    var mDTtmp = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTtmp == null)
                    {
                        return false;
                    }
                    var mfrmSelectProts = new frmSelectProtInfo
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(mDTtmp, false)
                    };

                    if (mfrmSelectProts.ShowDialog() == DialogResult.OK)
                    {
                        rowID = mfrmSelectProts.RowIDColumn; //mass tags
                        var protIPI = mfrmSelectProts.ProteinIDColumn;
                        var mdtProts = LoadProtColumns(mDTtmp, protIPI, rowID);
                        mdtProts.TableName = "ProtInfo";
                        AddDataset2HashTable(mdtProts);
                    }
                    else
                    {
                        success = false;
                    }

                    #endregion

                    break;
                case enmDataType.FACTORS:

                    // Factor files can be CSV files or .txt files
                    // The first row of the factor definitions file must have a column named Factor, then column names that match the names in the originally loaded Expressions table
                    // Each subsequent row of the factor file is a new factor name, then the factor value for each dataset
                    //
                    // Example factor file (.txt file should use a tab delimiter; CSV file would use commas)
                    //
                    // Factor       P10A  P10B  P11A  P11B  P12A  P12B
                    // Time         0     0     5     5     10    10
                    // Temperature  Hot   Cold  Hot   Cold  Hot   Cold

                    #region Load Factors

                    FactorsValid = true;
                    var mDTFactors = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTFactors == null)
                    {
                        return false;
                    }
                    if (mDTFactors.Rows.Count > frmDefFactors.MAX_LEVELS)
                    {
                        MessageBox.Show("Factors file has too many factors; max allowed is " + frmDefFactors.MAX_LEVELS + " factors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (mRConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors))
                    {
                        try
                        {
                            if (mhtDatasets.ContainsKey("Expressions"))
                            {
                                rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
                                mRConnector.EvaluateNoReturn(rcmd);
                                FactorsValid = mRConnector.GetSymbolAsBool("FactorsValid");
                            }
                            if (!FactorsValid)
                            {
                                success = false;
                            }
                            else
                            {
                                UpdateFactorInfoArray();
                                mDTFactors.Columns[0].ColumnName = "Factors";
                                mDTFactors.TableName = "factors";
                                mRConnector.EvaluateNoReturn("print(factors)");
                                mRConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                            success = false;
                        }
                    }
                    if (success)
                    {
                        AddDataset2HashTable(mDTFactors);
                    }

                    #endregion

                    break;
                default:
                    break;
            }
            return success;
        }

        /// <summary>
        /// Duplicates are handled in R
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool OpenFile2(string filename)
        {
            var success = true;

            // check that the file exists before opening it
            if (!File.Exists(mstrLoadedfileName))
            {
                return false;
            }

            var fExt = Path.GetExtension(filename);
            if (!ValidExtension(fExt))
            {
                MessageBox.Show("Filetype not allowed.", "Error!");
                return false;
            }

            string rowID;
            string rcmd;
            bool FactorsValid;
            switch (dataSetType)
            {
                case enmDataType.ESET:

                    #region Load Expressions

                    FactorsValid = true;
                    var mDTloaded = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTloaded == null)
                    {
                        return false;
                    }
                    mDTloaded.TableName = "AllEset";

                    //Select columns
                    var mfrmSelectCols = new frmSelectColumns
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(mDTloaded, false),
                        Proteins = mhtDatasets.ContainsKey("Protein Info")
                    };

                    if (mfrmSelectCols.ShowDialog() == DialogResult.OK)
                    {
                        rowID = mfrmSelectCols.RowIDColumn; //mass tags
                        var dataCols = mfrmSelectCols.DataColumns;
                        DataTable mDTselectedEset1;
                        try
                        {
                            mDTselectedEset1 = ArrangeDataTable(mDTloaded, rowID, dataCols); // create the datatable
                            mDTselectedEset1.TableName = "Eset";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message,
                                            "Exception while extracting data columns");
                            return false;
                        }

                        //clsRarray.rowNamesID = mDTselectedEset.Columns[0].ToString();
                        clsRarray.rowNamesID = "Row_ID";
                        if (mRConnector.SendTable2RmatrixNumeric("Eset", mDTselectedEset1))
                        // Duplicates are handled during the call 'SendTable2RmatrixNumeric'
                        {
                            try
                            {
                                if (mhtDatasets.ContainsKey("Factors"))
                                {
                                    rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
                                    mRConnector.EvaluateNoReturn(rcmd);
                                    FactorsValid = mRConnector.GetSymbolAsBool("FactorsValid");
                                }
                                if (!FactorsValid)
                                {
                                    success = false;
                                }
                                else
                                {
                                    mRConnector.GetTableFromRmatrix("Eset"); // Get the cleaned data matrix
                                    mDTselectedEset1 = mRConnector.DataTable.Copy();
                                    mDTselectedEset1.TableName = "Eset";
                                    mRConnector.EvaluateNoReturn("print(dim(Eset))");
                                    mRConnector.EvaluateNoReturn("cat(\"Expressions loaded.\n\")");
                                    AddDataset2HashTable(mDTselectedEset1);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                                success = false;
                            }
                        }
                        else
                        {
                            success = false;
                        }
                        if (mfrmSelectCols.Proteins && success) // Protein info needs to be loaded ?
                        {
                            // loads to mDataTableProtInfo and then sends to R
                            var mdtProts = LoadProtColumns(mDTloaded, mfrmSelectCols.ProteinIDColumn, rowID);
                            mdtProts.TableName = "ProtInfo";
                            AddDataset2HashTable(mdtProts);
                        }
                    }
                    else
                    {
                        success = false;
                    }

                    #endregion

                    break;
                case enmDataType.PROTINFO:

                    #region Load Protein Info

                    var mDTtmp = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTtmp == null)
                    {
                        return false;
                    }
                    var mfrmSelectProts = new frmSelectProtInfo
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(mDTtmp, false)
                    };

                    if (mfrmSelectProts.ShowDialog() == DialogResult.OK)
                    {
                        rowID = mfrmSelectProts.RowIDColumn; //mass tags
                        var protIPI = mfrmSelectProts.ProteinIDColumn;
                        var mdtProts = LoadProtColumns(mDTtmp, protIPI, rowID);
                        mdtProts.TableName = "ProtInfo";
                        AddDataset2HashTable(mdtProts);
                    }
                    else
                    {
                        success = false;
                    }

                    #endregion

                    break;
                case enmDataType.FACTORS:

                    #region Load Factors

                    FactorsValid = true;
                    var mDTFactors = clsDataTable.LoadFile2DataTableFastCSVReader(mstrLoadedfileName);
                    if (mDTFactors == null)
                    {
                        return false;
                    }
                    if (mDTFactors.Rows.Count > frmDefFactors.MAX_LEVELS)
                    {
                        MessageBox.Show("Factors file has too many factors; max allowed is " + frmDefFactors.MAX_LEVELS + " factors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    if (mRConnector.SendTable2RmatrixNonNumeric("factors", mDTFactors))
                    {
                        try
                        {
                            if (mhtDatasets.ContainsKey("Expressions"))
                            {
                                rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
                                mRConnector.EvaluateNoReturn(rcmd);
                                FactorsValid = mRConnector.GetSymbolAsBool("FactorsValid");
                            }
                            if (!FactorsValid)
                            {
                                success = false;
                            }
                            else
                            {
                                UpdateFactorInfoArray();
                                mDTFactors.Columns[0].ColumnName = "Factors";
                                mDTFactors.TableName = "factors";
                                mRConnector.EvaluateNoReturn("print(factors)");
                                mRConnector.EvaluateNoReturn("cat(\"Factors loaded.\n\")");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                            success = false;
                        }
                    }
                    if (success)
                    {
                        AddDataset2HashTable(mDTFactors);
                    }

                    #endregion

                    break;
                default:
                    break;
            }
            return success;
        }


        private void UpdateFactorInfoArray()
        {
            var marrFV = new List<string>();

            marrFactorInfo.Clear();
            try
            {
                mRConnector.EvaluateNoReturn("factorNames <- rownames(factors)");
                var factorNames = mRConnector.GetSymbolAsStrings("factorNames");
                var factors = factorNames;
                for (var numF = 0; numF < factors.Length; numF++)
                {
                    marrFV.Clear();
                    mRConnector.EvaluateNoReturn("fVals <- unique(factors[" +
                        Convert.ToString(numF + 1) + ",])");
                    mRConnector.EvaluateNoReturn("nfVals <- length(fVals)");

                    var factorValues = mRConnector.GetSymbolAsStrings("fVals");
                    var factorCounts = mRConnector.GetSymbolAsNumbers("nfVals");

                    if ((int)factorCounts[0] > 1)
                    {
                        //more than one factor value
                        marrFV.AddRange(factorValues);
                    }
                    else
                    {
                        marrFV.Add(factorValues[0]);
                    }

                    var currFactorInfo = new clsFactorInfo
                    {
                        SetFvals = marrFV,
                        mstrFactor = factors[numF]
                    };

                    marrFactorInfo.Add(currFactorInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
            }
        }

        /// <summary>
        /// Open an Inferno session file
        /// </summary>
        /// <param name="mstrFile">File path</param>
        public void OpenSessionThreaded(string mstrFile)
        {
            var doLoad = true;
            if (mtabControlData.Controls.Count != 0)
            
            {
                doLoad = (MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
                    "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
            }

            if (doLoad)
            {
                #region Threading
                m_BackgroundWorker.DoWork += m_BackgroundWorker_OpenSession;
                m_BackgroundWorker.RunWorkerCompleted += m_BackgroundWorker_SessionOpenCompleted;
                #endregion

                marrAnalysisObjects.Clear();
                mhtAnalysisObjects.Clear();

                m_BackgroundWorker.RunWorkerAsync(mstrFile);
                mfrmShowProgress.Message = "Loading Saved Session ...";
                mfrmShowProgress.ShowDialog();

                #region Threading
                m_BackgroundWorker.DoWork -= m_BackgroundWorker_OpenSession;
                m_BackgroundWorker.RunWorkerCompleted -= m_BackgroundWorker_SessionOpenCompleted;
                #endregion
            }
        }

        private string NumericVars2R()
        {
            var vars = "c("; // dummy to make vars an array of strings always

            vars += NumericVars();

            vars = vars.Substring(0, vars.LastIndexOf(','));
            vars = vars + ")";

            return vars;
        }

        void clsDataTable_OnProgress(object sender, ProgressEventArgs e)
        {
            if (mfrmShowProgress != null)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new EventHandler<ProgressEventArgs>(clsDataTable_OnProgress), sender, e);
                    return;
                }

                mfrmShowProgress.PercentComplete = (int)e.PercentComplete;
            }
        }

        private bool ValidExtension(string fExt)
        {
            if (string.IsNullOrWhiteSpace(fExt))
                return false;

            switch (fExt.ToLower())
            {
                case ".csv":
                case ".txt":
                case ".xls":
                case ".xlsx":
                    return true;
            }

            return false;
        }
    }
}
