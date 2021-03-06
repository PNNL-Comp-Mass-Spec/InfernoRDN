using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DAnTE.Paradiso;
using DAnTE.Properties;
using DAnTE.Tools;

namespace DAnTE.Inferno
{
    partial class frmDAnTE
    {
        // Ignore Spelling: txt, Eset, Prot, Prots, csv, tsv, xls, xlsx, rcmd, colnames, rownames, fVals, nfVals

        private enum FileTypeExtension
        {
            Txt = 0,
            Tsv = 1,
            Csv = 2,
            Xls = 3,
            Xlsx = 4
        }

        private bool DeleteTempFile(string tempFile)
        {
            var ok = true;
            tempFile = tempFile.Replace("/", @"\");

            if (File.Exists(tempFile))
            {
                try
                {
                    mRConnector.EvaluateNoReturn("graphics.off()");
                    File.Delete(tempFile);
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
                if (string.Equals(enumName, fileExtension, StringComparison.OrdinalIgnoreCase))
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
            Image currentImage;
            using (var fs = new FileStream(tempFile, FileMode.Open,
                                           FileAccess.Read, FileShare.ReadWrite))
            {
                var img = Image.FromStream(fs);
                fs.Close();
                currentImage = img.Clone() as Image;
                img.Dispose();
                File.Delete(tempFile);
            }
            return currentImage;
        }

        /// <summary>
        /// Show the open file window to the user
        /// </summary>
        /// <param name="preferredFileType">Preferred file extension</param>
        private void ShowOpenFileWindow(FileTypeExtension preferredFileType)
        {
            var fileTypesUsed = new List<FileTypeExtension>
            {
                FileTypeExtension.Txt,
                FileTypeExtension.Tsv,
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

            ShowOpenFileWindow(filter);
        }

        /// <summary>
        /// Show the open file window to the user
        /// </summary>
        /// <param name="filter">Filter spec list, e.g. "CSV files (*.csv)|*.csv|Tab delimited txt files (*.txt)|*.txt|"</param>
        private void ShowOpenFileWindow(string filter)
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

            var fileDialog = new OpenFileDialog
            {
                Title = mFileDialogTitle
            };

            if (!string.IsNullOrWhiteSpace(workingFolder))
            {
                fileDialog.InitialDirectory = workingFolder;
            }
            else
            {
                fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            fileDialog.Filter = filter;
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                mLoadedFilename = fileDialog.FileName;

                var fiDataFile = new FileInfo(mLoadedFilename);
                if (!fiDataFile.Exists)
                {
                    return;
                }
                mLoadedFilename = fiDataFile.FullName;

                if (fiDataFile.Directory != null)
                    workingFolder = fiDataFile.Directory.FullName;

                Settings.Default.WorkingFolder = workingFolder;
                Settings.Default.DataFileName = fiDataFile.Name;
                Settings.Default.Save();

                RegistryAccess.SetStringRegistryValue(WORKING_FOLDER, workingFolder);
            }
            else
                mLoadedFilename = null;
        }

        private string GetFilterSpec(FileTypeExtension fileType)
        {
            switch (fileType)
            {
                case FileTypeExtension.Txt:
                    return "Tab delimited txt files (*.txt)|*.txt|";
                case FileTypeExtension.Tsv:
                    return "TSV files (*.tsv)|*.tsv|";
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

        private void ShowSaveFileWindow(string filter)
        {
            var workingFolder = Settings.Default.WorkingFolder;

            var fileDialog = new SaveFileDialog
            {
                Title = mFileDialogTitle,
                InitialDirectory = workingFolder ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = filter,
                FilterIndex = 1,
                RestoreDirectory = false
            };


            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                mLoadedFilename = fileDialog.FileName;
                workingFolder = Path.GetDirectoryName(mLoadedFilename);
                Settings.Default.WorkingFolder = workingFolder;
                Settings.Default.Save();
            }
            else
                mLoadedFilename = null;
        }

        /// <summary>
        /// Creates a new data table with the first column having unique rowIDs and
        /// multiple columns of data.
        /// Checks for and removes any columns with duplicate column names
        /// </summary>
        /// <param name="sourceDataTable"></param>
        /// <param name="keyColumnName">Column with the unique identifier for each row</param>
        /// <param name="dataCols">
        /// When loadingProteinToPeptideMapInfo is false, these are columns with data (one per dataset)
        /// When loadingProteinToPeptideMapInfo is true, this should be the protein name column, plus optionally protein metadata columns
        /// </param>
        /// <param name="loadingProteinToPeptideMapInfo">False when we first read the file; true when reading it again to load the protein to peptide mapping</param>
        /// <remarks>
        /// Multiple rows can have the same unique identifier only if there is an accession column (aka protein)
        /// Now two rows should have the same accession and unique identifier
        /// </remarks>
        /// <returns></returns>
        private DataTable ArrangeDataTable(
            DataTable sourceDataTable,
            string keyColumnName,
            IList<string> dataCols,
            bool loadingProteinToPeptideMapInfo)
        {
            var dtEset = sourceDataTable.Copy();
            var columns = dtEset.Columns;

            // Clone dataCols so that we can sort it
            var sortedDataColumns = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in dataCols)
            {
                sortedDataColumns.Add(item);
            }

            var columnsToRemove = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataColumn column in columns)
            {
                if (!string.IsNullOrWhiteSpace(keyColumnName) && column.ColumnName.Equals(keyColumnName))
                {
                    continue;
                }

                if (sortedDataColumns.Contains(column.ColumnName))
                    continue;

                if (!columnsToRemove.Contains(column.ColumnName))
                    columnsToRemove.Add(column.ColumnName);
            }

            // Remove columns that are not data columns or the key column
            foreach (var s in columnsToRemove)
            {
                dtEset.Columns.Remove(s);
            }

            if (loadingProteinToPeptideMapInfo)
            {
                // Assure that the protein name column is the first column
                var proteinColumnName = dataCols.First();
                if (dtEset.Columns[proteinColumnName].Ordinal > 0)
                {
                    dtEset.Columns[proteinColumnName].SetOrdinal(0);
                }
            }
            else
            {
                // Assure that the unique key column is the first column
                if (!string.IsNullOrWhiteSpace(keyColumnName) && dtEset.Columns[keyColumnName].Ordinal > 0)
                {
                    // Rearrange the data so that the key column is first
                    dtEset.Columns[keyColumnName].SetOrdinal(0);
                }
            }

            return dtEset;
        }

        /// <summary>
        /// Rearrange the protein info columns in a data table,
        /// then send it to R and 'clean' it.
        /// Finally get the 'cleaned' data back
        /// </summary>
        /// <param name="sourceDataTable"></param>
        /// <param name="proteinIdentifierColumn"></param>
        /// <param name="rowID"></param>
        /// <returns></returns>
        private DataTable LoadProtColumns(DataTable sourceDataTable, string proteinIdentifierColumn, string rowID,
                                          IReadOnlyCollection<string> metaDataColumns)
        {
            DataTable proteinDataTable;

            try
            {
                var dataColumns = new List<string> { proteinIdentifierColumn };
                if (metaDataColumns.Count > 0)
                {
                    dataColumns.AddRange(metaDataColumns);

                    // Table will have more than two columns: rowID and proteinIdentifierColumn, then metaDataColumns
                    // ProteinID, ProteinMetadata, and rowID (the key column name in the Eset table)
                    proteinDataTable = ArrangeDataTable(sourceDataTable, rowID, dataColumns, true);
                }
                else
                {
                    // Table will have two columns: rowID and proteinIdentifierColumn
                    proteinDataTable = ArrangeDataTable(sourceDataTable, rowID, dataColumns, true);
                }


                proteinDataTable.TableName = "ProtInfo";

                // Rename the first column from Protein Name to Row_ID
                proteinDataTable.Columns[0].ColumnName = "Row_ID";

                // Generate parallel lists of Protein names and MassTagIDs (aka Eset row identifiers)
                mProteins = clsDataTable.DataColumn2strArray(sourceDataTable, proteinIdentifierColumn);
                mMassTags = clsDataTable.DataColumn2strArray(sourceDataTable, rowID);

                try
                {
                    // Push the data into R, storing as table ProtInfo, with MassTagID in the first column and ProteinIdentifier in the second column
                    // Currently mDTProts is not used for this, instead mProteins and mMassTags are used

                    mRConnector.SetSymbolCharVector("proteinIdentifierColumn", mProteins);
                    mRConnector.SetSymbolCharVector("MassTags", mMassTags);

                    // Add any protein metadata
                    var rCmdAddon = new StringBuilder();

                    foreach (var proteinMetadataColName in metaDataColumns)
                    {
                        var metadataRows = clsDataTable.DataColumn2strArray(sourceDataTable, proteinMetadataColName);

                        var genericMetadataColName = GetCleanColumnName(proteinMetadataColName);

                        mRConnector.SetSymbolCharVector(genericMetadataColName, metadataRows);

                        rCmdAddon.Append("," + genericMetadataColName + "=" + genericMetadataColName);
                    }

                    var rcmd = "ProtInfo<-data.frame(Row_ID=MassTags,ProteinID=proteinIdentifierColumn" + rCmdAddon +
                               ")";
                    mRConnector.EvaluateNoReturn(rcmd);

                    if (mhtDatasets.ContainsKey("Expressions"))
                        rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo, checkEset=TRUE)";
                    else
                        rcmd = "ProtInfo<-remove.emptyProtInfo(ProtInfo)";

                    mRConnector.EvaluateNoReturn(rcmd);

                    // Change .rowNamesID from "Row_ID" to "RowID", otherwise this will conflict with the "Row_ID" used by Eset
                    clsRarray.rowNamesID = "RowID";
                    if (mRConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
                    {
                        // Change .rowNamesID from RowID (defined by R) to Row_ID (used in Eset)
                        clsRarray.rowNamesID = "Row_ID";
                        proteinDataTable = mRConnector.DataTable.Copy();

                        // mDTProts now has columns RowID, Row_ID, ProteinID
                        proteinDataTable.TableName = "ProtInfo";
                        if (proteinDataTable.Columns.CanRemove(proteinDataTable.Columns[0]))
                        {
                            // remove the line number column that comes from R row names, i.e. the RowID column
                            proteinDataTable.Columns.Remove(proteinDataTable.Columns[0]);

                            // Make sure the first column is now named Row_ID
                            if (!string.Equals(proteinDataTable.Columns[0].ColumnName, "Row_ID"))
                            {
                                proteinDataTable.Columns[0].ColumnName = "Row_ID";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Exception while talking to R");
                    proteinDataTable = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Exception while extracting data columns");
                proteinDataTable = null;
            }
            return proteinDataTable;
        }

        private DataTable GetProtInfoMatrix()
        {
            DataTable proteinInfoMatrix;

            clsRarray.rowNamesID = "RowID"; // Otherwise this will conflict with 'Row_ID'
            if (mRConnector.GetTableFromRProtInfoMatrix("ProtInfo"))
            {
                clsRarray.rowNamesID = "Row_ID";
                proteinInfoMatrix = mRConnector.DataTable.Copy();
                proteinInfoMatrix.TableName = "ProtInfo";
                if (proteinInfoMatrix.Columns.CanRemove(proteinInfoMatrix.Columns[0]))
                {
                    //remove the line number column which comes from R row names.
                    proteinInfoMatrix.Columns.Remove(proteinInfoMatrix.Columns[0]);
                    proteinInfoMatrix.Columns[0].ColumnName = "Row_ID";
                }
            }
            else
            {
                proteinInfoMatrix = null;
            }
            return proteinInfoMatrix;
        }

        /// <summary>
        /// Parses columnName to replace any characters that are not letters or numbers with underscores
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private string GetCleanColumnName(string columnName)
        {
            var reNotLetterOrNumber = new Regex("[^A-Z0-9]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var cleanName = reNotLetterOrNumber.Replace(columnName, "_");
            return cleanName;
        }

        [Obsolete("Unused", true)]
        private DataTable OpenFile_test(string filename)
        {
            //bool success = true;
            //bool FactorsValid = true;

            var dtSelectedEset1 = new DataTable();

            // check that the file exists before opening it
            if (!File.Exists(mLoadedFilename))
            {
                return dtSelectedEset1;
            }

            var fExt = Path.GetExtension(filename);
            if (!ValidExtension(fExt))
            {
                MessageBox.Show("File type not allowed.", "Error!");
                return null;
            }

            var dataLoader = new clsDataTable();
            dataLoader.OnError += clsDataTable_OnError;
            dataLoader.OnWarning += clsDataTable_OnWarning;
            dataLoader.OnProgress += clsDataTable_OnProgress;

            //FactorsValid = true;
            var loadedData = dataLoader.LoadFile2DataTableFastCSVReader(mLoadedFilename);
            if (loadedData == null)
            {
                return null;
            }
            loadedData.TableName = "AllEset";

            //Select columns
            var columnSelectionForm = new frmSelectColumns
            {
                PopulateListBox = clsDataTable.DataTableColumns(loadedData, false),
                Proteins = mhtDatasets.ContainsKey("Protein Info")
            };

            if (columnSelectionForm.ShowDialog() == DialogResult.OK)
            {
                var rowID = columnSelectionForm.RowIDColumn;
                var dataCols = columnSelectionForm.DataColumns.ToList();
                try
                {
                    dtSelectedEset1 = ArrangeDataTable(loadedData, rowID, dataCols, false); // create the expression set data table
                    dtSelectedEset1.TableName = "Eset";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                                    "Exception while extracting data columns");
                    //success = false;
                    return null;
                }
            }
            return dtSelectedEset1;
        }


        /// <summary>
        /// Main routine that opens data files (Expressions, Factors)
        /// in comma or tab delimited CSVs (and Excel as a future possibility).
        /// The main file load routine is in clsDataTable.LoadFile2DataTable(fileName)
        /// Duplicates are handled in C#
        /// </summary>
        private bool OpenFile(string filePath)
        {
            string rowID;
            string rcmd;
            var success = true;
            bool validFactors;

            // check that the file exists before opening it
            if (!File.Exists(filePath))
            {
                return false;
            }

            if (!string.Equals(mLoadedFilename, filePath))
            {
                mLoadedFilename = filePath;
            }

            var fExt = Path.GetExtension(filePath);
            if (string.IsNullOrWhiteSpace(fExt))
            {
                MessageBox.Show("File does not have an extension (like .csv or .txt); unable to determine file type",
                                "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ValidExtension(fExt))
            {
                MessageBox.Show("File type not allowed (must be csv, tsv, txt, xls, or xlsx)", "Error!");
                return false;
            }

            var dataLoader = new clsDataTable();
            dataLoader.OnError += clsDataTable_OnError;
            dataLoader.OnWarning += clsDataTable_OnWarning;
            dataLoader.OnProgress += clsDataTable_OnProgress;

            switch (mDataSetType)
            {
                case enmDataType.ESET:

                    #region Load Expressions

                    validFactors = true;

                    mProgressForm.Reset("Loading data");

                    var esetTable = dataLoader.LoadFile2DataTableFastCSVReader(mLoadedFilename);
                    if (esetTable == null)
                    {
                        string errorMessage;
                        if (string.IsNullOrWhiteSpace(mProgressForm.ErrorMessage))
                            errorMessage = "Unknown load error";
                        else
                            errorMessage = "Load error: " + mProgressForm.ErrorMessage;

                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (!string.IsNullOrWhiteSpace(mProgressForm.WarningMessage))
                    {
                        MessageBox.Show(mProgressForm.WarningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    esetTable.TableName = "AllEset";

                    // Select columns
                    var columnSelectionForm = new frmSelectColumns
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(esetTable, false),
                        Proteins = mhtDatasets.ContainsKey("Protein Info")
                    };

                    if (columnSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        rowID = columnSelectionForm.RowIDColumn; //mass tags
                        var dataCols = columnSelectionForm.DataColumns.ToList();
                        try
                        {
                            var filteredDataTable = ArrangeDataTable(esetTable, rowID, dataCols, false);

                            // Rename the first column from MassTagID (or whatever the user-supplied name is) to Row_ID
                            filteredDataTable.Columns[0].ColumnName = "Row_ID";

                            // Remove rows with no data or duplicate data
                            filteredDataTable = dataLoader.RemoveDuplicateRows2(
                                filteredDataTable,
                                filteredDataTable.Columns[0].ColumnName);

                            // Copy the data into R
                            filteredDataTable.TableName = "Eset";
                            success = mRConnector.SendTable2RmatrixNumeric("Eset", filteredDataTable);
                            if (mhtDatasets.ContainsKey("Factors"))
                            {
                                // Simplistic method looking for exact duplicates:
                                // rcmd = "FactorsValid<-identical(as.array(colnames(factors)),as.array(colnames(Eset)))";
                                // mRConnector.EvaluateNoReturn(rcmd);
                                // validFactors = mRConnector.GetSymbolAsBool("FactorsValid");

                                // Better method for comparing column names, including notifying the user of missing columns
                                validFactors = ValidateFactors();
                            }

                            if (!validFactors)
                            {
                                success = false;
                            }

                            if (!string.IsNullOrWhiteSpace(mProgressForm.ErrorMessage))
                            {
                                MessageBox.Show(mProgressForm.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            if (success)
                            {
                                AddDataset2HashTable(filteredDataTable);
                                mRConnector.EvaluateNoReturn("print(dim(Eset))");
                                mRConnector.EvaluateNoReturn("cat(\"Expressions loaded.\n\")");

                                if (!string.IsNullOrWhiteSpace(mProgressForm.WarningMessage))
                                {
                                    MessageBox.Show(mProgressForm.WarningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message,
                                            "Exception while extracting data columns");
                            return false;
                        }

                        if (columnSelectionForm.Proteins &&
                            !string.IsNullOrWhiteSpace(columnSelectionForm.ProteinIDColumn) && success)
                        {
                            // Load protein info then send to R
                            var proteinDataTable = LoadProtColumns(esetTable, columnSelectionForm.ProteinIDColumn, rowID,
                                                                   columnSelectionForm.ProteinMetadataColumns);
                            proteinDataTable.TableName = "ProtInfo";
                            AddDataset2HashTable(proteinDataTable);
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

                    var proteinInfoTable = dataLoader.LoadFile2DataTableFastCSVReader(mLoadedFilename);
                    if (proteinInfoTable == null)
                    {
                        return false;
                    }
                    var proteinSelectionForm = new frmSelectProtInfo
                    {
                        PopulateListBox = clsDataTable.DataTableColumns(proteinInfoTable, false)
                    };

                    if (proteinSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        rowID = proteinSelectionForm.RowIDColumn; //mass tags
                        var proteinIdentifierColumn = proteinSelectionForm.ProteinIDColumn;
                        var proteinDataTable = LoadProtColumns(proteinInfoTable, proteinIdentifierColumn, rowID,
                                                               proteinSelectionForm.ProteinMetadataColumns);
                        proteinDataTable.TableName = "ProtInfo";
                        AddDataset2HashTable(proteinDataTable);
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

                    validFactors = true;
                    var factorTable = dataLoader.LoadFile2DataTableFastCSVReader(mLoadedFilename);
                    if (factorTable == null)
                    {
                        return false;
                    }

                    if (factorTable.Rows.Count > frmDefFactors.MAX_LEVELS)
                    {
                        MessageBox.Show(
                            "Factors file has too many factors; max allowed is " + frmDefFactors.MAX_LEVELS + " factors",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (factorTable.Columns.Count == 0)
                    {
                        MessageBox.Show(
                            "Factors file is empty; nothing to load",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (mhtDatasets.ContainsKey("Expressions"))
                    {
                        // Check for extra sample names in the factorTable and remove them
                        // The various calls to R that utilize factors do not work properly when extra columns are present

                        mRConnector.EvaluateNoReturn("esetColNames <- colnames(Eset)");
                        var esetColNamesFromR = mRConnector.GetSymbolAsStrings("esetColNames");
                        var esetColNames = new SortedSet<string>();
                        foreach (var item in esetColNamesFromR.Distinct())
                            esetColNames.Add(item);

                        var extraFactorCols = new List<string>();
                        var factorColumnName = string.Empty;

                        foreach (var factorNameToFind in new List<string> { "Factor", "Factors" })
                        {
                            foreach (DataColumn factorCol in factorTable.Columns)
                            {
                                if (string.Equals(factorCol.ColumnName, factorNameToFind, StringComparison.OrdinalIgnoreCase))
                                {
                                    factorColumnName = factorNameToFind;
                                    break;
                                }
                            }
                        }

                        if (string.IsNullOrWhiteSpace(factorColumnName))
                        {
                            MessageBox.Show(
                                string.Format(
                                    "The first column of the factors file must be named Factor or Factors; your file has {0}",
                                    factorTable.Columns[0].ColumnName),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            return false;
                        }

                        // Assure that the factor column is the first column
                        if (factorTable.Columns[factorColumnName].Ordinal > 0)
                        {
                            factorTable.Columns[factorColumnName].SetOrdinal(0);
                        }

                        foreach (DataColumn factorCol in factorTable.Columns)
                        {
                            if (string.Equals(factorCol.ColumnName, factorColumnName, StringComparison.OrdinalIgnoreCase))
                                continue;

                            if (!esetColNames.Contains(factorCol.ColumnName))
                                extraFactorCols.Add(factorCol.ColumnName);
                        }

                        if (extraFactorCols.Count > 0)
                        {
                            foreach (var colToRemove in extraFactorCols)
                            {
                                factorTable.Columns.Remove(colToRemove);
                            }

                            if (extraFactorCols.Count == 1)
                            {
                                MessageBox.Show(
                                    string.Format(
                                        "Removed 1 unknown sample name from the factors file (did not match any expression column names): {0}",
                                        extraFactorCols.First()),
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    string.Format(
                                        "Removed {0} unknown sample names from the factors file (names did not match expression column names): {1}",
                                        extraFactorCols.Count,
                                        string.Join(", ", extraFactorCols)),
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    if (mRConnector.SendTable2RmatrixNonNumeric("factors", factorTable))
                    {
                        try
                        {
                            if (mhtDatasets.ContainsKey("Expressions"))
                            {
                                validFactors = ValidateFactors();
                            }

                            if (!validFactors)
                            {
                                success = false;
                            }
                            else
                            {
                                UpdateFactorInfoArray();
                                factorTable.Columns[0].ColumnName = "Factors";
                                factorTable.TableName = "factors";
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
                        AddDataset2HashTable(factorTable);
                    }

                    #endregion

                    break;
            }

            return success;
        }

        private void UpdateFactorInfoArray()
        {
            var factorValues = new List<string>();

            marrFactorInfo.Clear();
            try
            {
                mRConnector.EvaluateNoReturn("factorNames <- rownames(factors)");
                var factorNamesFromR = mRConnector.GetSymbolAsStrings("factorNames");
                var factorNames = factorNamesFromR;

                for (var numF = 0; numF < factorNames.Length; numF++)
                {
                    factorValues.Clear();
                    mRConnector.EvaluateNoReturn("fVals <- unique(factors[" +
                                                 Convert.ToString(numF + 1) + ",])");
                    mRConnector.EvaluateNoReturn("nfVals <- length(fVals)");

                    var factorValuesFromR = mRConnector.GetSymbolAsStrings("fVals");
                    var factorCountsFromR = mRConnector.GetSymbolAsNumbers("nfVals");

                    if ((int)factorCountsFromR[0] > 1)
                    {
                        //more than one factor value
                        factorValues.AddRange(factorValuesFromR);
                    }
                    else
                    {
                        factorValues.Add(factorValuesFromR[0]);
                    }

                    var currentFactorInfo = new clsFactorInfo
                    {
                        SetFvals = factorValues,
                        mstrFactor = factorNames[numF]
                    };

                    marrFactorInfo.Add(currentFactorInfo);
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
        /// <param name="sessionFilePath">File path</param>
        /// <remarks>Warns the user if data is already loaded and will get replaced</remarks>
        public void OpenSessionCheckExisting(string sessionFilePath, bool useThreadedLoad)
        {
            var doLoad = true;
            if (mDataTab.Controls.Count != 0)

            {
                doLoad = (MessageBox.Show("If you load a saved session, current data will be lost. Continue?",
                                          "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                          DialogResult.Yes);
            }

            if (!doLoad)
            {
                return;
            }

            if (useThreadedLoad)
                SessionFileOpenThreaded(sessionFilePath);
            else
                SessionFileOpenNonThreaded(sessionFilePath);
        }

        private string NumericVars2R()
        {
            var vars = "c("; // dummy to make vars an array of strings always

            vars += NumericVars();

            vars = vars.Substring(0, vars.LastIndexOf(','));
            vars = vars + ")";

            return vars;
        }

        void clsDataTable_OnError(object sender, MessageEventArgs e)
        {
            if (mProgressForm != null)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new EventHandler<MessageEventArgs>(clsDataTable_OnError), sender, e);
                    return;
                }

                mProgressForm.ErrorMessage = e.Message;
            }
        }

        void clsDataTable_OnWarning(object sender, MessageEventArgs e)
        {
            if (mProgressForm != null)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new EventHandler<MessageEventArgs>(clsDataTable_OnWarning), sender, e);
                    return;
                }

                mProgressForm.AppendWarningMessage(e.Message);
            }
        }

        void clsDataTable_OnProgress(object sender, ProgressEventArgs e)
        {
            if (mProgressForm != null)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new EventHandler<ProgressEventArgs>(clsDataTable_OnProgress), sender, e);
                    return;
                }

                mProgressForm.PercentComplete = (int)e.PercentComplete;
            }
        }

        private bool ValidExtension(string fExt)
        {
            if (string.IsNullOrWhiteSpace(fExt))
                return false;

            switch (fExt.ToLower())
            {
                case ".csv":
                case ".tsv":
                case ".txt":
                case ".xls":
                case ".xlsx":
                    return true;
            }

            return false;
        }

        private bool ValidateFactors()
        {
            // Make sure all of the column names in Eset are present in factors

            mRConnector.EvaluateNoReturn("factorColNames <- colnames(factors)");
            var factorColNamesFromR = mRConnector.GetSymbolAsStrings("factorColNames");
            var factorColNames = new SortedSet<string>();
            foreach (var factorCol in factorColNamesFromR.Distinct())
            {
                factorColNames.Add(factorCol);
            }

            mRConnector.EvaluateNoReturn("esetColNames <- colnames(Eset)");
            var esetColNames = mRConnector.GetSymbolAsStrings("esetColNames");

            var missingColNames = esetColNames.Where(esetCol => !factorColNames.Contains(esetCol)).ToList();

            if (missingColNames.Count == 0)
                return true;

            if (missingColNames.Count == 1)
            {
                MessageBox.Show(
                    string.Format("Error: factors were missing for data column: {0}", missingColNames.First()),
                    "Factors Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(
                    string.Format("Error: factors were missing for the following data columns: {0}",
                                  string.Join(", ", missingColNames)),
                    "Factors Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return false;
        }
    }
}