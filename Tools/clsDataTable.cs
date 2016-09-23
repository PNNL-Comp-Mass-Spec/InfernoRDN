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
    public static class clsDataTable
    {
        #region Events

        /// <summary>
        /// Progress event handler.
        /// </summary>
        public static event EventHandler<ProgressEventArgs> OnProgress;

        private static void OnProgressUpdate(ProgressEventArgs e)
        {
            var handler = OnProgress;
            handler?.Invoke(null, e);
        }

        #endregion

        #region File loading methods

        public static DataTable LoadFile2DataTable(string FileName)
        {
            var sConnectionString = "";
            var dtOut = new DataTable();
            DataTable dtIn;

            var fileName = Path.GetFileName(FileName);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                Console.WriteLine("Unknown file type");
                //fileOK = false;
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv": // CSV files
                    using (var parser = new clsGenericParserAdapter())
                    {
                        parser.SetDataSource(FileName);
                        parser.ColumnDelimiter = ",".ToCharArray();
                        parser.FirstRowHasHeader = true;
                        parser.MaxBufferSize = 4096;
                        parser.TextQualifier = '\"';
                        dtIn = parser.GetDataTable();
                        parser.Close();
                        dtOut = ReplaceMissingStr(dtIn);
                    }
                    break;
                case ".txt":
                    using (var parser = new clsGenericParserAdapter())
                    {
                        parser.SetDataSource(FileName);
                        parser.ColumnDelimiter = "\t".ToCharArray();
                        parser.FirstRowHasHeader = true;
                        parser.MaxBufferSize = 4096;
                        parser.TextQualifier = '\"';
                        dtIn = parser.GetDataTable();
                        parser.Close();
                        dtOut = ReplaceMissingStr(dtIn);
                    }
                    break;
                case ".xls": //Excel files
                    sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                                        FileName + ";" + "Extended Properties=Excel 8.0;";
                    goto case "Excel";
                case ".xlsx": // New Excel files
                    sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" +
                                        FileName + ";" + "Extended Properties=Excel 12.0;";
                    goto case "Excel";
                case "Excel":
                    OleDbConnection objConn = null;
                    DataTable dt = null;
                    try
                    {
                        //string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                        //    FileName + ";" + "Extended Properties=Excel 8.0;";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        var excelSheets = new string[dt.Rows.Count];
                        var i = 0;

                        // Add the sheet name to the string array.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        var sheetCmd = "SELECT * FROM [" + excelSheets[0] + "]"; //read the first table
                        var objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        var objAdapter1 = new OleDbDataAdapter
                        {
                            SelectCommand = objCmdSelect
                        };
                        objAdapter1.Fill(dtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    break;
                default:
                    Console.WriteLine("Unknown File type");
                    //fileOK = false;
                    dtOut = null;
                    break;
            }
            return dtOut;
        }

        public static DataTable LoadFile2DataTableJETOLEDB(string FileName)
        {
            var dtOut = new DataTable();

            OleDbConnection objConn;

            var fileName = Path.GetFileName(FileName);
            var filePath = Path.GetDirectoryName(FileName);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                Console.WriteLine("Unknown file type");
                //fileOK = false;
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv": // CSV files
                case ".txt":
                    objConn = null;
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

                        objAdapter1.Fill(dtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    break;
                case ".xls": //Excel files
                    objConn = null;
                    DataTable dt = null;
                    try
                    {
                        var sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                                                FileName + ";" + "Extended Properties=Excel 8.0;";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        var excelSheets = new string[dt.Rows.Count];
                        var i = 0;

                        // Add the sheet name to the string array.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        var sheetCmd = "SELECT * FROM [" + excelSheets[0] + "]"; //read the first table
                        var objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        var objAdapter1 = new OleDbDataAdapter
                        {
                            SelectCommand = objCmdSelect
                        };

                        objAdapter1.Fill(dtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    break;
                default:
                    Console.WriteLine("Unknown File");
                    //fileOK = false;
                    dtOut = null;
                    break;
            }
            return dtOut;
        }

        public static DataTable LoadFile2DataTableFastCSVReader(string FileName)
        {
            var sConnectionString = "";
            var dtOut = new DataTable();

            var fileName = Path.GetFileName(FileName);
            var fExt = Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(fExt))
            {
                Console.WriteLine("Unknown file type");
                //fileOK = false;
                return null;
            }

            switch (fExt.ToLower())
            {
                case ".csv": // CSV files
                    using (
                        var csv =
                            new CsvReader(
                                new StreamReader(new FileStream(FileName, FileMode.Open, FileAccess.Read,
                                                                FileShare.ReadWrite)), true))
                    {
                        csv.ParseError += csv_ParseError;
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                        dtOut.Load(csv);
                    }
                    break;
                case ".txt":
                    using (
                        var csv =
                            new CsvReader(
                                new StreamReader(new FileStream(FileName, FileMode.Open, FileAccess.Read,
                                                                FileShare.ReadWrite)), true, '\t'))
                    {
                        csv.ParseError += csv_ParseError;
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                        dtOut.Load(csv);
                    }
                    break;
                case ".xls": //Excel files
                    sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                                        FileName + ";" + "Extended Properties=Excel 8.0;";
                    goto case "Excel";
                case ".xlsx": // New Excel files
                    sConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" +
                                        FileName + ";" + "Extended Properties=Excel 12.0;";
                    goto case "Excel";
                case "Excel":
                    OleDbConnection objConn = null;
                    DataTable dt = null;
                    try
                    {
                        //string sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                        //    FileName + ";" + @"Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1;""";
                        objConn = new OleDbConnection(sConnectionString);
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
                                dtOut = null;
                                break;
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
                        Console.WriteLine(ex.Message);
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
                    break;
                default:
                    Console.WriteLine("Unknown File");
                    //fileOK = false;
                    dtOut = null;
                    break;
            }
            return dtOut;
        }


        static void csv_ParseError(object sender, ParseErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message, "Reader Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        public static DataTable Array2DataTable(double[,] matrix, string[] rowNames, string[] colHeaders)
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

        public static DataTable ReplaceMissingStr(DataTable dt)
        {
            var Nrows = dt.Rows.Count;
            //DataTable outDtable = new DataTable();

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                //DataRow workRow = dt.Rows[row] ;
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
                //outDtable.Rows.Add(obj);
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        public static DataTable ReplaceMissing(DataTable dt)
        {
            var Nrows = dt.Rows.Count;
            //DataTable outDtable = new DataTable();

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                //DataRow workRow = dt.Rows[row] ;
                var obj = new object[colCount];
                //obj = new string[colCount];

                for (var col = 0; col < colCount; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col];
                    var cell = obj[col].ToString();
                    if (cell.Equals("999999") || cell.Equals(" 9.999990e+05"))
                    {
                        obj[col] = DBNull.Value;
                    }
                }
                //outDtable.Rows.Add(obj);
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        public static DataTable ClearNulls(DataTable dt)
        {
            var Nrows = dt.Rows.Count;

            for (var row = 0; row < Nrows; row++)
            {
                var colCount = dt.Rows[row].ItemArray.Length;

                //DataRow workRow = dt.Rows[row] ;
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

        public static void RemoveDuplicateRows(DataTable dTable, string colName)
        {
            var hTable = new Dictionary<object, string>();
            var duplicateList = new List<DataRow>();

            foreach (DataRow drow in dTable.Rows)
            {
                try
                {
                    hTable.Add(drow[colName], string.Empty);
                }
                catch
                {
                    duplicateList.Add(drow);
                }
            }

            foreach (var dRow in duplicateList)
                dTable.Rows.Remove(dRow);
        }

        public static DataTable RemoveDuplicateRows2(DataTable dTable, string colName)
        {
            var hTable = new Dictionary<object, DataRow>();
            var duplicateList = new List<DataRow>();

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
                    // Empty row; flag it for removal by adding to duplicateList
                    duplicateList.Add(thisRow);
                }
                else
                {
                    // Non-empty row
                    try
                    {
                        if (hTable.ContainsKey(thisRow[colName]))
                        {
                            AddDuplicateRow(hTable, thisRow, duplicateList, colName);
                        }
                        else
                        {
                            hTable.Add(thisRow[colName], thisRow);
                        }
                    }
                    catch (Exception)
                    {
                        AddDuplicateRow(hTable, thisRow, duplicateList, colName);
                    }
                }

                rowCountLoaded += 1;
                var percentComplete = rowCountLoaded / (float)rowCountTotal * 100;

                if (rowCountLoaded % 100 == 0)
                    OnProgressUpdate(new ProgressEventArgs(percentComplete));
            }

            dTable.AcceptChanges();

            // Remove empty rows and duplicate rows
            foreach (var dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            return dTable;
        }

        private static void AddDuplicateRow(IDictionary<object, DataRow> hTable, DataRow thisRow,
                                            ICollection<DataRow> duplicateList, string keyColName)
        {
            duplicateList.Add(thisRow);
            var prevRow = hTable[thisRow[keyColName]];
            if (!RowsIdentical(thisRow, prevRow))
            {
                var currentRow = addRows(prevRow, thisRow);
                hTable[thisRow[keyColName]] = currentRow;
            }
        }

        public static bool RowsIdentical(DataRow row1, DataRow row2)
        {
            for (var i = 0; i < row1.ItemArray.Length; i++)
            {
                var item1 = row1.ItemArray[i];
                var item2 = row2.ItemArray[i];

                if (item1 == null && item2 != null)
                    return false;

                if (item1 != null && item2 == null)
                    return false;

                if (item1 != null)
                {
                    if (!item1.Equals(item2))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Make sure the row is not empty
        /// </summary>
        /// <param name="row"></param>
        /// <returns>True if at least one value in the row, otherwise false</returns>
        public static bool ValidRow(DataRow row)
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

        public static DataRow addRows(DataRow row1, DataRow row2)
        {
            for (var i = 1; i < row1.ItemArray.Length; i++)
            {
                var val1 = CDblSafe(row1.ItemArray[i]);
                var val2 = CDblSafe(row2.ItemArray[i]);
                var val3 = val1 + val2;
                if (Math.Abs(val3) > double.Epsilon)
                    row1[i] = val3.ToString(CultureInfo.InvariantCulture);
            }
            return row1;
        }

        private static double CDblSafe(object item)
        {
            if (item == null || item == DBNull.Value)
                return 0;

            double value;
            if (double.TryParse((string)item, out value))
                return value;

            return 0;
        }

        //----------------------------------------------------------------------------
        /// <summary>
        /// Removes duplicate rows from given DataTable
        /// </summary>
        /// <param name="tbl">Table to scan for duplicate rows</param>
        /// <param name="keyColumns">An array of DataColumns
        /// containing the columns to match for duplicates</param>
        public static void RemoveDuplicates(DataTable tbl, DataColumn[] keyColumns)
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

        private static List<DataRow> FindDups(DataTable tbl, int sourceNdx, DataColumn[] keyColumns)
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


        private static bool IsDup(DataRow sourceRow, DataRow targetRow, DataColumn[] keyColumns)
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

        private static bool Is1stColumnEmpty(DataRow sourceRow)
        {
            if (sourceRow[0].ToString().Equals(""))
                return true;
            else
                return false;
        }

        //private static bool Is2ndColumnEmpty(DataRow sourceRow)
        //{
        //    if (sourceRow[1].ToString().Equals(""))
        //        return true;
        //    else
        //        return false;
        //}

        //public static void RemoveProtEmpty(DataTable tbl)
        //{
        //    DataRow[] blankRs = FindEmpty(tbl);
        //    if (blankRs.Length > 0)
        //    {
        //        foreach (DataRow dup in blankRs)
        //        {
        //            tbl.Rows.Remove(dup);
        //        }
        //    }
        //}

        //private static DataRow[] FindEmpty(DataTable tbl)
        //{
        //    ArrayList retVal = new ArrayList();
        //    for (int i = 0; i < tbl.Rows.Count; i++)
        //    {
        //        DataRow targetRow = tbl.Rows[i];
        //        if (Is1stColumnEmpty(targetRow) || Is2ndColumnEmpty(targetRow))
        //        {
        //            retVal.Add(targetRow);
        //        }
        //    }
        //    return (DataRow[])retVal.ToArray(typeof(DataRow));
        //}

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