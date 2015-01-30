using System;
using System.Collections;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
            EventHandler<ProgressEventArgs> handler = OnProgress;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        #endregion

        #region File loading methods
        public static DataTable LoadFile2DataTable(string FileName)
        {
            String sConnectionString = "";
            var mdtOut = new DataTable();
            DataTable mdtIn;

            string fileName = Path.GetFileName(FileName);
            string fExt = Path.GetExtension(fileName);

            switch (fExt)
            {
                case ".csv":// CSV files
                    using (var parser = new clsGenericParserAdapter())
                    {
                        parser.SetDataSource(FileName);
                        parser.ColumnDelimiter = ",".ToCharArray();
                        parser.FirstRowHasHeader = true;
                        parser.MaxBufferSize = 4096;
                        parser.TextQualifier = '\"';
                        mdtIn = parser.GetDataTable();
                        parser.Close();
                        mdtOut = ReplaceMissingStr(mdtIn);
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
                        mdtIn = parser.GetDataTable();
                        parser.Close();
                        mdtOut = ReplaceMissingStr(mdtIn);
                    }
                    break;
                case ".xls"://Excel files
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
                        //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                        //    FileName + ";" + "Extended Properties=Excel 8.0;";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        String[] excelSheets = new String[dt.Rows.Count];
                        int i = 0;

                        // Add the sheet name to the string array.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        string sheetCmd = "SELECT * FROM [" + excelSheets[0] +"]"; //read the first table
                        OleDbCommand objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                        objAdapter1.SelectCommand = objCmdSelect;
                        objAdapter1.Fill(mdtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                    finally
                    {
                        // Clean up.
                        if (objConn != null)
                        {
                            objConn.Close();
                            objConn.Dispose();
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Unknown File");
                    //fileOK = false;
                    mdtOut = null;
                    break;
            }
            return mdtOut;
        }

        public static DataTable LoadFile2DataTableJETOLEDB(string FileName)
        {
            string fileName = FileName;
            string filePath = FileName;
            string fExt;
            DataTable mdtOut = new DataTable();
            DataTable mdtIn = new DataTable();
            OleDbConnection objConn = null;
            DataTable dt = null;

            fileName = Path.GetFileName(FileName);
            filePath = Path.GetDirectoryName(FileName);
            fExt = Path.GetExtension(fileName);

            switch (fExt)
            {
                case ".csv":// CSV files
                case ".txt":
                    objConn = null;
                    dt = null;
                    try
                    {
                        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=" + filePath + ";" + 
                                @"Extended Properties=""text;HDR=Yes;FMT=Delimited""";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        
                        string sheetCmd = "SELECT * FROM [" + fileName + "]"; //read the table
                        OleDbCommand objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                        objAdapter1.SelectCommand = objCmdSelect;
                        objAdapter1.Fill(mdtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                    finally
                    {
                        // Clean up.
                        if (objConn != null)
                        {
                            objConn.Close();
                            objConn.Dispose();
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                        }
                    }
                    break;
                case ".xls"://Excel files
                    objConn = null;
                    dt = null;
                    try
                    {
                        String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                            FileName + ";" + "Extended Properties=Excel 8.0;";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        String[] excelSheets = new String[dt.Rows.Count];
                        int i = 0;

                        // Add the sheet name to the string array.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[i] = row["TABLE_NAME"].ToString();
                            i++;
                        }
                        string sheetCmd = "SELECT * FROM [" + excelSheets[0] + "]"; //read the first table
                        OleDbCommand objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                        objAdapter1.SelectCommand = objCmdSelect;
                        objAdapter1.Fill(mdtOut);
                        //mdtOut = clsDataTable.ClearNulls(mdtIn) ;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                    finally
                    {
                        // Clean up.
                        if (objConn != null)
                        {
                            objConn.Close();
                            objConn.Dispose();
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Unknown File");
                    //fileOK = false;
                    mdtOut = null;
                    break;
            }
            return mdtOut;
        }

        public static DataTable LoadFile2DataTableFastCSVReader(string FileName)
        {
            string fileName = FileName;
            string filePath = FileName;
            string fExt;
            String sConnectionString = "";
            DataTable mdtOut = new DataTable();
            DataTable mdtIn = new DataTable();

            fileName = Path.GetFileName(FileName);
            filePath = Path.GetDirectoryName(FileName);
            fExt = Path.GetExtension(fileName);

            switch (fExt)
            {
                case ".csv":// CSV files
                    using (CsvReader csv = new CsvReader(new System.IO.StreamReader(FileName), true))
                    {
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                        mdtOut.Load(csv);
                    } 
                    break;
                case ".txt":
                    using (CsvReader csv = new CsvReader(new System.IO.StreamReader(FileName), true, '\t'))
                    {
                        csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                        mdtOut.Load(csv);
                    } 
                    break;
                case ".xls"://Excel files
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
                    string mstrSheet = null;
                    try
                    {
                        //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" +
                        //    FileName + ";" + @"Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1;""";
                        objConn = new OleDbConnection(sConnectionString);
                        objConn.Open();
                        dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }
                        if (dt.Rows.Count == 1)
                            mstrSheet = (dt.Rows[0])["TABLE_NAME"].ToString();
                        else
                        {
                            ArrayList marrExcelSheets = new ArrayList();
                            int i = 0;

                            // Add the sheet name to the string array.
                            foreach (DataRow row in dt.Rows)
                            {
                                mstrSheet = row["TABLE_NAME"].ToString();
                                marrExcelSheets.Add(mstrSheet);
                                i++;
                            }
                            frmSelectExcelSheet mfrmSheets = new frmSelectExcelSheet();
                            mfrmSheets.PopulateListBox = marrExcelSheets;
                            if (mfrmSheets.ShowDialog() == DialogResult.OK)
                            {
                                i = mfrmSheets.SelectedSheet;
                                mstrSheet = marrExcelSheets[i].ToString();
                            }
                            else
                            {
                                mdtOut = null;
                                break;
                            }
                        }
                        string sheetCmd = "SELECT * FROM [" + mstrSheet + "]";
                        OleDbCommand objCmdSelect = new OleDbCommand(sheetCmd, objConn);
                        OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
                        objAdapter1.SelectCommand = objCmdSelect;
                        objAdapter1.Fill(mdtOut);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                    finally
                    {
                        // Clean up.
                        if (objConn != null)
                        {
                            objConn.Close();
                            objConn.Dispose();
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Unknown File");
                    //fileOK = false;
                    mdtOut = null;
                    break;
            }
            return mdtOut;
        }
        #endregion

        public static DataTable Array2DataTable(double[,] matrix, string[] rowNames, string[] colHeaders)
        {
            var mDataTable = new DataTable();

            DataColumn mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = "Row_ID";
            //mDataColumn.ReadOnly = true ;
            mDataTable.Columns.Add(mDataColumn);

            for (int i = 0; i < colHeaders.Length; i++)
            {
                mDataColumn = new DataColumn
                {
                    DataType = System.Type.GetType("System.String"),
                    ColumnName = colHeaders[i]
                };
                //mDataColumn.ReadOnly = true ;
                mDataTable.Columns.Add(mDataColumn);
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                DataRow mDataRow = mDataTable.NewRow();
                mDataRow[0] = rowNames[i];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j].ToString();
                }
                mDataTable.Rows.Add(mDataRow);
            }
            return ReplaceMissingStr(mDataTable);
        }

        public static string[] DataColumn2strArray(DataTable mTab, string Colmn)
        {
            string[] mStrArr = new string[mTab.Rows.Count];
            DataColumnCollection columns = mTab.Columns;
            int i = 0;
            foreach (DataColumn column in columns)
            {
                if (!Colmn.Equals(column.ColumnName))
                    i++;
                else
                    break;
            }
            for (int row = 0; row < mTab.Rows.Count; row++)
            {
                mStrArr[row] = mTab.Rows[row].ItemArray[i].ToString();
            }
            return mStrArr;
        }

        public static DataTable ReplaceMissingStr(DataTable dt)
        {
            string cell = null;
            int Nrows = dt.Rows.Count;
            //DataTable outDtable = new DataTable();

            for (int row = 0; row < Nrows; row++)
            {
                //DataRow workRow = dt.Rows[row] ;
                string[] obj;
                obj = new string[dt.Rows[row].ItemArray.Length];
                for (int col = 0; col < dt.Rows[row].ItemArray.Length; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col].ToString();
                    cell = dt.Rows[row].ItemArray[col].ToString();
                    if (cell.Equals("999999") || cell.Equals(" 9.999990e+05"))
                    {
                        string s = "";
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
            string cell = null;
            int Nrows = dt.Rows.Count;
            //DataTable outDtable = new DataTable();

            for (int row = 0; row < Nrows; row++)
            {
                //DataRow workRow = dt.Rows[row] ;
                Object[] obj = new object[dt.Rows[row].ItemArray.Length];
                //obj = new string[dt.Rows[row].ItemArray.Length];
                for (int col = 0; col < dt.Rows[row].ItemArray.Length; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col];
                    cell = dt.Rows[row].ItemArray[col].ToString();
                    if (cell.Equals("999999") || cell.Equals(" 9.999990e+05"))
                    {
                        obj[col] = System.DBNull.Value;
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
            int Nrows = dt.Rows.Count;
            DataTable outDtable = new DataTable();

            for (int row = 0; row < Nrows; row++)
            {
                //DataRow workRow = dt.Rows[row] ;
                object[] obj;
                obj = new object[dt.Rows[row].ItemArray.Length];
                for (int col = 0; col < dt.Rows[row].ItemArray.Length; col++)
                {
                    obj[col] = dt.Rows[row].ItemArray[col];
                    if (dt.Rows[row].ItemArray[col] == System.DBNull.Value)
                    {
                        string s = "";
                        obj[col] = (object)s;
                    }
                }
                dt.Rows[row].ItemArray = obj;
            }
            dt.AcceptChanges();
            return dt;
        }

        public static void RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

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

            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);
        }

        public static DataTable RemoveDuplicateRows2(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();            
            ArrayList duplicateList = new ArrayList();

            foreach (DataColumn dC in dTable.Columns)
            {
                dC.ReadOnly = false;
            }

            int rowCountLoaded = 0;
            int rowCountTotal = dTable.Rows.Count;

            foreach (DataRow thisRow in dTable.Rows)
            {
                if (!ValidRow(thisRow))
                    duplicateList.Add(thisRow);
                else
                    try
                    {
						if (hTable.ContainsKey(thisRow[colName]))
						{
							AddDuplicateRow(dTable, hTable, thisRow, duplicateList, colName);
						}
	                    else
	                    {
							hTable.Add(thisRow[colName], thisRow);    
	                    }
                        
                    }
                    catch (Exception ex)
                    {
						AddDuplicateRow(dTable, hTable, thisRow, duplicateList, colName);
                    }

                rowCountLoaded += 1;
                float percentComplete = rowCountLoaded / (float)rowCountTotal * 100;

                if (rowCountLoaded % 100 == 0)
                    OnProgressUpdate(new ProgressEventArgs(percentComplete));
            }

            dTable.AcceptChanges();

            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            return dTable;
        }

		private static void AddDuplicateRow(DataTable dTable, Hashtable hTable, DataRow thisRow, ArrayList duplicateList, string keyColName)
	    {
			duplicateList.Add(thisRow);
			var prevRow = (DataRow)hTable[thisRow[keyColName]];
			if (!RowsIdentical(thisRow, prevRow))
			{
				DataRow currentRow = addRows(prevRow, thisRow);
				hTable[thisRow[keyColName]] = currentRow;
			}
	    }

	    public static bool RowsIdentical(DataRow row1, DataRow row2)
        {
            for (int i = 0; i < row1.ItemArray.Length; i++)
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

        public static bool ValidRow(DataRow row) // not all empty
        {
            bool success = false;
            for (int i = 1; i < row.ItemArray.Length; i++)
            {
                if (!(row.ItemArray[i].Equals("")))
                {
                    success = true;
                    break;
                }

            }
            return success;
        }

        public static DataRow addRows(DataRow row1, DataRow row2)
        {
            for (int i = 1; i < row1.ItemArray.Length; i++)
            {
                double val1 = CDblSafe(row1.ItemArray[i]);
                double val2 = CDblSafe(row2.ItemArray[i]);
                double val3 = val1 + val2;
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
        /// <param name="KeyColumns">An array of DataColumns
        /// containing the columns to match for duplicates</param>
        public static void RemoveDuplicates(DataTable tbl, DataColumn[] keyColumns)
        {
            int rowNdx = 0;
            while (rowNdx < tbl.Rows.Count - 1)
            {
                DataRow[] dups = FindDups(tbl, rowNdx, keyColumns);
                if (dups.Length > 0)
                {
                    foreach (DataRow dup in dups)
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

        private static DataRow[] FindDups(DataTable tbl, int sourceNdx, DataColumn[] keyColumns)
        {
            ArrayList retVal = new ArrayList();
            DataRow sourceRow = tbl.Rows[sourceNdx];
            for (int i = sourceNdx + 1; i < tbl.Rows.Count; i++)
            {
                DataRow targetRow = tbl.Rows[i];
                if (IsDup(sourceRow, targetRow, keyColumns) || Is1stColumnEmpty(targetRow))
                {
                    retVal.Add(targetRow);
                }
            }
            return (DataRow[])retVal.ToArray(typeof(DataRow));
        }



        private static bool IsDup(DataRow sourceRow, DataRow targetRow, DataColumn[] keyColumns)
        {
            bool retVal = true;
            foreach (DataColumn column in keyColumns)
            {
                retVal = retVal && sourceRow[column].Equals(targetRow[column]);
                if (!retVal) break;
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
        /// Get the DataTable column names to an arraylist
        /// </summary>
        public static ArrayList DataTableColumns(DataTable dt, bool dataonly)
        {
            ArrayList marrCols = new ArrayList();
            int i = 0;
            foreach (DataColumn column in dt.Columns)
            {
                if (dataonly)
                {
                    //Ignore MassTag column
                    //if (i != 0 || !column.ColumnName.Equals("PepCount"))
                    if (i != 0)
                        marrCols.Add(column.ColumnName);
                    i++;
                }
                else
                    marrCols.Add(column.ColumnName);
            }
            return marrCols;
        }

        public static ArrayList DataTableColumns(DataTable dt, string dataset)
        {
            ArrayList marrCols = new ArrayList();
            int i = 0;
            bool prots = (dataset.Contains("pData") || dataset.Contains("qrollup"));

            foreach (DataColumn column in dt.Columns)
            {
                if (prots)
                {
                    //Ignore first two columns
                    if (i > 2)
                        marrCols.Add(column.ColumnName);
                    i++;
                }
                else
                {
                    //Ignore the first column
                    if (i != 0)
                        marrCols.Add(column.ColumnName);
                    i++;
                }
            }
            return marrCols;
        }

        public static ArrayList DataTableRows(DataTable dt)
        {
            ArrayList marrRows = new ArrayList();

            foreach (DataRow dRow in dt.Rows)
            {
                marrRows.Add(dRow.ItemArray[0].ToString());
            }
            return marrRows;
        }

    }
}
