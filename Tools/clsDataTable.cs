using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;


namespace DAnTE.Tools
{
    public class clsDataTable
    {

        private const int CSV_ERRORS_TO_SHOW = 4;

        private int mCsvErrors;

        #region Events

        /// <summary>
        /// Used for reporting errors
        /// </summary>
        public event EventHandler<MessageEventArgs> OnError;

        /// <summary>
        /// Used for reporting warnings
        /// </summary>
        public event EventHandler<MessageEventArgs> OnWarning;

        /// <summary>
        /// Used for reporting progress
        /// </summary>
        public event EventHandler<ProgressEventArgs> OnProgress;

        private void ReportError(string message)
        {
            Console.WriteLine(message);
            OnError?.Invoke(null, new MessageEventArgs(message));
        }

        private void ReportWarning(string message)
        {
            Console.WriteLine(message);
            OnWarning?.Invoke(null, new MessageEventArgs(message));
        }

        private void ReportProgress(float percentComplete)
        {
            var handler = OnProgress;
            handler?.Invoke(null, new ProgressEventArgs(percentComplete));
        }

        #endregion

        #region File loading methods

        [Obsolete("Unused")]
        public DataTable LoadFile2DataTable(string filePath)
        {

            var fileName = Path.GetFileName(filePath);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                ReportError("Unknown file type" + fExt);
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv":
                    return LoadDelimitedFileViaGenericParser(filePath, '\t');

                case ".txt":
                    return LoadDelimitedFileViaGenericParser(filePath, ',');

                case ".xls": //Excel files
                    var connectionString1 =
                        "Provider=Microsoft.Jet.OLEDB.4.0;" +
                        "Data Source=" + filePath + ";" +
                        "Extended Properties=Excel 8.0;";
                    return LoadExcelFile(filePath, connectionString1);

                case ".xlsx": // New Excel files
                    var connectionString2 = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" +
                                        filePath + ";" + "Extended Properties=Excel 12.0;";
                    return LoadExcelFile(filePath, connectionString2);

                default:
                    ReportError("Unknown File type" + fExt);
                    return null;
            }
        }

        [Obsolete("Unused")]
        public DataTable LoadFile2DataTableJETOLEDB(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                ReportError("Unknown file type " + fExt);
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv": // CSV files
                case ".txt":
                    OleDbConnection objConn = null;
                    try
                    {
                        var sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                                "Data Source=" + filePath + ";" +
                                                @"Extended Properties=""text;HDR=Yes;FMT=Delimited""";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();

                        var sheetCmd = "SELECT * FROM [" + fileName + "]"; //read the table
                        var objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        var objAdapter1 = new OleDbDataAdapter
                        {
                            SelectCommand = objCmdSelect
                        };

                        var dtOut = new DataTable();
                        objAdapter1.Fill(dtOut);
                        return dtOut;
                    }
                    catch (Exception ex)
                    {
                        ReportError($"Error opening {filePath}: {ex.Message}");
                        return null;
                    }
                    finally
                    {
                        // Clean up.
                        if (objConn != null)
                        {
                            objConn.Close();
                            objConn.Dispose();
                        }
                    }

                case ".xls": //Excel files
                    try
                    {
                        var connectionString =
                            "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Data Source=" + filePath + ";" +
                            "Extended Properties=Excel 8.0;";
                        var dtOut = LoadExcelFile(filePath, connectionString);
                        return dtOut;
                    }
                    catch (Exception ex)
                    {
                        ReportError($"Error opening {filePath}: {ex.Message}");
                        return null;
                    }

                default:
                    Console.WriteLine("Unknown file type " + fExt);
                    return null;
            }

        }

        public DataTable LoadFile2DataTableFastCSVReader(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                ReportError("Unknown file type" + fExt);
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv": // CSV files
                    return LoadDelimitedFile(filePath, ',');

                case ".tsv":
                case ".txt":
                    return LoadDelimitedFile(filePath, '\t');

                case ".xls": //Excel files
                    var connectionString1 =
                        "Provider=Microsoft.Jet.OLEDB.4.0;" +
                        "Data Source=" + filePath + ";" +
                        "Extended Properties=Excel 8.0;";
                    return LoadExcelFile(filePath, connectionString1);

                case ".xlsx": // New Excel files
                    var connectionString2 =
                        "Provider=Microsoft.ACE.OLEDB.12.0;" +
                        "Data Source=" + filePath + ";" +
                        "Extended Properties=Excel 12.0;";
                    return LoadExcelFile(filePath, connectionString2);

                default:
                    ReportError("Unknown file type" + fExt);
                    return null;
            }
        }

        private DataTable LoadDelimitedFile(string filePath, char delimiter)
        {
            // Read the first line of the data file to make sure there are no duplicate column names
            using (var fileStream = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                if (!fileStream.EndOfStream)
                {
                    var headerLine = fileStream.ReadLine();
                    if (string.IsNullOrWhiteSpace(headerLine))
                    {
                        ReportError("Empty header line; cannot load data from " + filePath);
                        return null;
                    }

                    var headers = headerLine.Split(delimiter);
                    var uniqueHeaders = new SortedSet<string>();

                    foreach (var columnName in headers)
                    {
                        if (uniqueHeaders.Contains(columnName))
                        {
                            ReportError($"Duplicate column name in header line, column \"{columnName}\"; cannot load data from {filePath}");
                            return null;
                        }
                        uniqueHeaders.Add(columnName);
                    }

                }
            }

            using (var fileStream = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                using (var reader = new CsvReader(fileStream, true, delimiter))
                {
                    mCsvErrors = 0;
                    reader.ParseError += csv_ParseError;
                    reader.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;

                    try
                    {
                        var dtOut = new DataTable();
                        dtOut.Load(reader);

                        if (mCsvErrors > CSV_ERRORS_TO_SHOW)
                        {
                            MessageBox.Show(
                                $"{mCsvErrors} lines in the data file had an error",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        return dtOut;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Error reading data file {Path.GetFileName(filePath)}: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return null;
                    }

                }
            }
        }

        [Obsolete("Unused")]
        private DataTable LoadDelimitedFileViaGenericParser(string filePath, char delimiter)
        {
            var delimiters = new List<char> { delimiter };

            DataTable dtIn;

            using (var parser = new clsGenericParserAdapter())
            {
                parser.SetDataSource(filePath);
                parser.ColumnDelimiter = delimiters.ToArray();
                parser.FirstRowHasHeader = true;
                parser.MaxBufferSize = 4096;
                parser.TextQualifier = '\"';
                dtIn = parser.GetDataTable();
                parser.Close();
            }

            var dtOut = ReplaceMissingStr(dtIn);
            return dtOut;

        }

        private DataTable LoadExcelFile(string filePath, string connectionString)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            var dtOut = new DataTable();

            try
            {
                objConn = new OleDbConnection(connectionString);
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }

                string mstrSheet;
                if (dt.Rows.Count == 1)
                    mstrSheet = (dt.Rows[0])["TABLE_NAME"].ToString();
                else
                {
                    var arrExcelSheets = new List<string>();
                    var i = 0;

                    // Add the sheet name to the string array.
                    foreach (DataRow row in dt.Rows)
                    {
                        mstrSheet = row["TABLE_NAME"].ToString();
                        arrExcelSheets.Add(mstrSheet);
                        i++;
                    }
                    var frmSheets = new frmSelectExcelSheet
                    {
                        PopulateListBox = arrExcelSheets
                    };
                    if (frmSheets.ShowDialog() == DialogResult.OK)
                    {
                        i = frmSheets.SelectedSheet;
                        mstrSheet = arrExcelSheets[i];
                    }
                    else
                    {
                        return null;
                    }
                }

                var sheetCmd = "SELECT * FROM [" + mstrSheet + "]";
                var objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                var objAdapter1 = new OleDbDataAdapter
                {
                    SelectCommand = objCmdSelect
                };
                objAdapter1.Fill(dtOut);
            }
            catch (Exception ex)
            {
                ReportError($"Error opening {filePath}: {ex.Message}");
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                dt?.Dispose();
            }

            return dtOut;
        }

        private void csv_ParseError(object sender, ParseErrorEventArgs e)
        {
            mCsvErrors++;

            if (mCsvErrors > CSV_ERRORS_TO_SHOW)
                return;

            string errorMessage;
            var dataIndex = e.Error.Message.IndexOf("Current raw data", StringComparison.OrdinalIgnoreCase);

            if (dataIndex > 0)
                errorMessage = e.Error.Message.Substring(0, dataIndex);
            else if (e.Error.Message.Length > 250)
                errorMessage = e.Error.Message.Substring(0, 250);
            else
                errorMessage = e.Error.Message;

            if (errorMessage.StartsWith("The CSV"))
                MessageBox.Show("The data file" + errorMessage.Substring(7), "Reader Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show(errorMessage, "Reader Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        [Obsolete("Unused")]
        private DataTable Array2DataTable(double[,] matrix, IList<string> rowNames, string[] colHeaders)
        {
            var dataTable = new DataTable();

            var dataColumn = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "Row_ID"
            };
            dataTable.Columns.Add(dataColumn);

            foreach (var header in colHeaders)
            {
                dataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = header
                };
                dataTable.Columns.Add(dataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var dataRow = dataTable.NewRow();
                dataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    dataRow[j + 1] = matrix[i, j].ToString(CultureInfo.InvariantCulture);
                }
                dataTable.Rows.Add(dataRow);
            }
            return ReplaceMissingStr(dataTable);
        }

        public static string[] DataColumn2strArray(DataTable dataTable, string keyColumnName)
        {
            var stringArray = new string[dataTable.Rows.Count];
            var columns = dataTable.Columns;
            var i = 0;
            foreach (DataColumn column in columns)
            {
                if (!keyColumnName.Equals(column.ColumnName))
                    i++;
                else
                    break;
            }
            for (var row = 0; row < dataTable.Rows.Count; row++)
            {
                stringArray[row] = dataTable.Rows[row].ItemArray[i].ToString();
            }
            return stringArray;
        }

        [Obsolete("Unused")]
        private DataTable ReplaceMissingStr(DataTable dt)
        {
            var Nrows = dt.Rows.Count;

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                var obj = new object[colCount];
                for (var col = 0; col < colCount; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col].ToString();
                    var cell = obj[col].ToString();
                    if (cell.Equals("999999") || cell.Equals(" 9.999990e+05"))
                    {
                        var s = "";
                        obj[col] = s;
                    }
                }
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        [Obsolete("Unused")]
        private DataTable ReplaceMissing(DataTable dt)
        {
            var Nrows = dt.Rows.Count;

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                var obj = new object[colCount];

                for (var col = 0; col < colCount; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col];
                    var cell = obj[col].ToString();
                    if (cell.Equals("999999") || cell.Equals(" 9.999990e+05"))
                    {
                        obj[col] = DBNull.Value;
                    }
                }
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        [Obsolete("Unused")]
        private DataTable ClearNulls(DataTable dt)
        {
            var Nrows = dt.Rows.Count;

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                var obj = new object[colCount];
                for (var col = 0; col < colCount; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col];
                    if (dt.Rows[row].ItemArray[col] == DBNull.Value)
                    {
                        var s = "";
                        obj[col] = s;
                    }
                }
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// Check for and remove duplicate rows
        /// </summary>
        /// <param name="dTable"></param>
        /// <param name="keyColumn">Name of the column with data values that cannot be duplicated (e.g. protein name)</param>
        /// <returns>Updated data table</returns>
        /// <remarks>
        /// If two rows have the same key and identical data, the second row is removed
        /// If two rows have the same key but different data, the data values are summed
        /// </remarks>
        public DataTable RemoveDuplicateRows2(DataTable dTable, string keyColumn)
        {
            var uniqueKeys = new Dictionary<object, DataRow>();
            var duplicateList = new List<DataRow>();
            var emptyList = new List<DataRow>();

            foreach (DataColumn dC in dTable.Columns)
            {
                dC.ReadOnly = false;
            }

            var rowCountLoaded = 0;
            var rowCountTotal = dTable.Rows.Count;

            foreach (DataRow thisRow in dTable.Rows)
            {
                if (!ValidRow(thisRow))
                {
                    // Empty row; flag it for removal by adding to emptyList
                    emptyList.Add(thisRow);
                }
                else
                {
                    // Non-empty row
                    try
                    {
                        if (uniqueKeys.ContainsKey(thisRow[keyColumn]))
                        {
                            // Compare the new row to the master row
                            // If all values are identical, skip it
                            // Otherwise, sum the values
                            AddDuplicateRow(uniqueKeys, thisRow, duplicateList, keyColumn);
                        }
                        else
                        {
                            uniqueKeys.Add(thisRow[keyColumn], thisRow);
                        }
                    }
                    catch (Exception)
                    {
                        AddDuplicateRow(uniqueKeys, thisRow, duplicateList, keyColumn);
                    }
                }

                rowCountLoaded += 1;
                var percentComplete = rowCountLoaded / (float)rowCountTotal * 100;

                if (rowCountLoaded % 100 == 0)
                    ReportProgress(percentComplete);
            }

            if (duplicateList.Count > 0)
            {
                RemoveRows(dTable, duplicateList, keyColumn, "duplicate");
            }

            if (emptyList.Count > 0)
            {
                RemoveRows(dTable, emptyList, keyColumn, "empty");
            }

            return dTable;
        }

        private void RemoveRows(DataTable dTable, IEnumerable<DataRow> rowsToRemove, string keyColumn, string rowType)
        {
            var keysRemoved = new List<string>();

            // Remove empty rows and duplicate rows
            foreach (var item in rowsToRemove)
            {
                keysRemoved.Add(item[keyColumn].ToString());
                dTable.Rows.Remove(item);
            }

            // Log warning of the form
            // Removed 3 duplicate rows named: KeyName1, KeyName2, KeyName3
            // or
            // Removed 5 empty rows named: KeyName1, KeyName2, KeyName3

            if (keysRemoved.Count == 1)
            {
                ReportWarning(string.Format("Removed 1 {0} row named: {1}", rowType, keysRemoved.First()));
            }
            else if (keysRemoved.Count <= 10)
            {
                ReportWarning(string.Format("Removed {0} {1} rows named: {2}",
                                            keysRemoved.Count, rowType, string.Join(", ", keysRemoved)));
            }
            else
            {
                ReportWarning(string.Format("Removed {0} {1} rows named: {2} ...",
                                            keysRemoved.Count, rowType, string.Join(", ", keysRemoved.Take(10))));
            }
        }

        private void AddDuplicateRow(IDictionary<object, DataRow> uniqueKeys, DataRow thisRow,
                                     ICollection<DataRow> duplicateList, string keyColumn)
        {
            duplicateList.Add(thisRow);

            var masterRow = uniqueKeys[thisRow[keyColumn]];
            if (!RowsIdentical(thisRow, masterRow))
            {
                var updatedRow = AddRows(masterRow, thisRow);
                uniqueKeys[thisRow[keyColumn]] = updatedRow;
            }
        }

        private bool RowsIdentical(DataRow row1, DataRow row2)
        {
            for (var i = 0; i < row1.ItemArray.Length; i++)
            {
                var item1 = row1.ItemArray[i];
                var item2 = row2.ItemArray[i];

                if (item1 == null && item2 != null)
                    return false;

                if (item1 != null && item2 == null)
                    return false;

                if (item1 == null) continue;

                if (!item1.Equals(item2))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Make sure the row is not empty
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if at least one value in the row, otherwise false</returns>
        private bool ValidRow(DataRow row)
        {
            for (var i = 1; i < row.ItemArray.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(row.ItemArray[i].ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add values between two rows, starting with the second column
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        private DataRow AddRows(DataRow row1, DataRow row2)
        {
            for (var i = 1; i < row1.ItemArray.Length; i++)
            {
                var val1 = CDblSafe(row1.ItemArray[i], i);
                var val2 = CDblSafe(row2.ItemArray[i], i);
                var val3 = val1 + val2;
                if (Math.Abs(val3) > double.Epsilon)
                    row1[i] = val3.ToString(CultureInfo.InvariantCulture);
            }
            return row1;
        }

        private double CDblSafe(object item, int rowNumber)
        {
            if (item == null || item == DBNull.Value)
                return 0;

            if (item is double doubleValue)
                return doubleValue;

            if (item is float floatValue)
                return floatValue;

            if (item is int intValue)
                return intValue;

            try
            {
                if (clsUtilities.ParseDouble((string)item, out var value))
                    return value;
            }
            catch (Exception ex)
            {
                try
                {
                    if (clsUtilities.ParseDouble(item.ToString(), out var value))
                        return value;
                }
                catch (Exception ex2)
                {
                    ReportError(string.Format("Unable to convert item to a double, row {0}", rowNumber));
                }
            }

            return 0;
        }

        /// <summary>
        /// Removes duplicate rows from given DataTable
        /// </summary>
        /// <param name="tbl">Table to scan for duplicate rows</param>
        /// <param name="keyColumns">An array of DataColumns
        /// containing the columns to match for duplicates</param>
        [Obsolete("Unused")]
        private void RemoveDuplicates(DataTable tbl, DataColumn[] keyColumns)
        {
            var rowNdx = 0;
            while (rowNdx < tbl.Rows.Count - 1)
            {
                var dups = FindDups(tbl, rowNdx, keyColumns);
                if (dups.Count > 0)
                {
                    foreach (var dup in dups)
                    {
                        tbl.Rows.Remove(dup);
                    }
                }
                else
                {
                    rowNdx++;
                }
            }
        }

        [Obsolete("Unused")]
        private List<DataRow> FindDups(DataTable tbl, int sourceNdx, DataColumn[] keyColumns)
        {
            var retVal = new List<DataRow>();
            var sourceRow = tbl.Rows[sourceNdx];
            for (var i = sourceNdx + 1; i < tbl.Rows.Count; i++)
            {
                var targetRow = tbl.Rows[i];
                if (IsDup(sourceRow, targetRow, keyColumns) || Is1stColumnEmpty(targetRow))
                {
                    retVal.Add(targetRow);
                }
            }
            return retVal;
        }

        [Obsolete("Unused")]
        private bool IsDup(DataRow sourceRow, DataRow targetRow, DataColumn[] keyColumns)
        {
            var retVal = true;
            foreach (var column in keyColumns)
            {
                retVal = sourceRow[column].Equals(targetRow[column]);
                if (!retVal)
                    break;
            }
            return retVal;
        }

        private bool Is1stColumnEmpty(DataRow sourceRow)
        {
            return string.IsNullOrWhiteSpace(sourceRow[0].ToString());
        }

        /// <summary>
        /// Get the DataTable column names to an list
        /// </summary>
        public static List<string> DataTableColumns(DataTable dt, bool dataonly)
        {
            var columnNames = new List<string>();
            var i = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (dataonly)
                {
                    //Ignore MassTag column
                    //if (i != 0 || !column.ColumnName.Equals("PepCount"))
                    if (i != 0)
                        columnNames.Add(column.ColumnName);
                    i++;
                }
                else
                    columnNames.Add(column.ColumnName);
            }
            return columnNames;
        }

        public static List<string> DataTableColumns(DataTable dt, string dataset)
        {
            var columnNames = new List<string>();
            var i = 0;
            var prots = dataset.Contains("pData") || dataset.Contains("qrollup");

            foreach (DataColumn column in dt.Columns)
            {
                if (prots)
                {
                    // Ignore the first two columns
                    // May need to ignore more columns if protein metadata is present
                    if (i > 2)
                        columnNames.Add(column.ColumnName);
                    i++;
                }
                else
                {
                    //Ignore the first column
                    if (i != 0)
                        columnNames.Add(column.ColumnName);
                    i++;
                }
            }
            return columnNames;
        }

        /// <summary>
        /// Get a list of the first column value for each row
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> DataTableRows(DataTable dt)
        {
            var rowList = (from DataRow dRow in dt.Rows select dRow.ItemArray[0].ToString()).ToList();
            return rowList;
        }
    }
}