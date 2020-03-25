using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace DAnTE.Tools
{
    /// <summary>
    /// Summary description for clsRconnect.
    /// </summary>
    public class clsRconnect
    {
        private RdnConnector _rdn;
        private string _rCommand;

        public string[] RowNames { get; private set; }

        public string[] Vector { get; private set; }

        public DataTable DataTable { get; private set; }

        public string Message { get; private set; }

        public clsRconnect()
        {
            Message = null;
            _rCommand = "";
            DataTable = new DataTable();
        }


        public bool initR()
        {
            Message = "";
            try
            {
                _rdn = new RdnConnector();
                _rdn.Init("R");
                return true;
            }
            catch (NullReferenceException)
            {
                var errMsg = @"Unable to connect to R. Confirm that R 3.x is installed by examining directory C:\Program Files\R or C:\Program Files (x86)\R";
                Console.WriteLine(errMsg);
                Message = errMsg;
                return false;
            }
            catch (Exception e)
            {
                var errMsg = "R Init failed: " + e.Message;
                Console.WriteLine(errMsg);
                Message = errMsg;
                return false;
            }
        }

        #region R_Interface

        //public void SetCharacterOutputDevice(StatConnectorCommonLib.IStatConnectorCharacterDevice dev)
        //{
        //  _rdn.SetCharacterOutputDevice(dev);
        //}

        public void EvaluateNoReturn(string rCommand)
        {
            _rdn.EvaluateNoReturn(rCommand);
        }

        public void SetSymbolCharVector(string name, string[] value)
        {
            _rdn.SetSymbolCharVector(name, value);
        }

        public bool GetSymbolAsBool(string name)
        {
            return _rdn.GetSymbolAsBool(name);
        }

        public string[] GetSymbolAsStrings(string name)
        {
            return _rdn.GetSymbolAsStrings(name);
        }

        public string[,] GetSymbolAsStringMatrix(string name)
        {
            return _rdn.GetSymbolAsStringMatrix(name);
        }

        public double[] GetSymbolAsNumbers(string name)
        {
            return _rdn.GetSymbolAsNumbers(name);
        }

        public double[,] GetSymbolAsNumberMatrix(string name)
        {
            return _rdn.GetSymbolAsNumberMatrix(name);
        }

        #endregion

        public bool loadR(string table, string filename, bool stripWhite, bool header, string separator)
        {
            try
            {
                _rCommand = table + "<-loadfile('" + filename +
                        "',stripWhite=" + stripWhite.ToString().ToUpper() + ",header=" +
                        header.ToString().ToUpper() + ",separator='" + separator + "')";
                _rdn.EvaluateNoReturn(_rCommand);
                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRmatrix(string varName)
        {
            try
            {
                DataTable.Clear();
                _rCommand = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rArray = new clsRarray(matrix, rownames, colheaders);
                DataTable = RDoubleArray2DataTable(rArray);

                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRvector(string varName)
        {
            try
            {
                _rCommand = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rowH = rownames;
                var colH = colheaders[0];
                var rArray = new clsRarray(matrix, rowH, colH);
                DataTable.Clear();
                DataTable = RDoubleVector2DataTable(rArray);

                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRproteinMatrix(string varName)
        {
            try
            {
                _rCommand = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rArray = new clsRarray(matrix, rownames, colheaders);

                DataTable.Clear();
                DataTable = RProteinArray2DataTable(rArray);

                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRmatrixNonNumeric(string varName)
        {
            try
            {
                _rCommand = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);

                var matrix = _rdn.GetSymbolAsStringMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rArray = new clsRarray(matrix, rownames, colheaders);

                DataTable = RstrArray2DataTable(rArray);

                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRProtInfoMatrix(string varName)
        {
            try
            {
                _rCommand = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);

                var matrix = _rdn.GetSymbolAsStringMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rArray = new clsRarray(matrix, rownames, colheaders);

                DataTable = RproteinInfoArray2DataTable(rArray);

                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetRowNamesFromRmatrix(string varName)
        {
            try
            {
                _rCommand = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rCommand);
                RowNames = _rdn.GetSymbolAsStrings("Rows");
                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetRstringVector(string varName)
        {
            try
            {
                Vector = _rdn.GetSymbolAsStrings(varName);
                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Store the data table in R; duplicate rows are removed
        /// </summary>
        /// <param name="varName"></param>
        /// <param name="dataTable"></param>
        /// <returns>True if success, otherwise false</returns>
        public bool SendTable2RmatrixNumeric(string varName, DataTable dataTable)
        {
            clsRarray rArray;

            try
            {
                rArray = DataTable2Rarray(dataTable, varName);
            }
            catch (Exception e)
            {
                var errMsg = string.Format("Error converting {0} using DataTable2Rarray: {1} ", varName, e.Message);
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }


            if (rArray.matrix != null)
            {
                try
                {
                    _rdn.SetSymbolNumberMatrix("X", rArray.matrix);
                    _rCommand = varName + "<- getmatrix(X)";
                    _rdn.EvaluateNoReturn(_rCommand);
                    _rdn.SetSymbolCharVector("colH", rArray.colHeaders);
                    _rdn.SetSymbolCharVector("rowN", rArray.rowNames);
                    _rCommand = "colnames(" + varName + ") <- colH";
                    _rdn.EvaluateNoReturn(_rCommand);
                    _rCommand = "rownames(" + varName + ") <- rowN";
                    _rdn.EvaluateNoReturn(_rCommand);
                    //rCommand = varName + "<- cleanmatrix(" + varName + ")"; // here's where the duplicates are removed
                    //rdcom.EvaluateNoReturn(rCommand);

                    return true;
                }
                catch (Exception e)
                {
                    var errMsg = _rCommand + " " + e.Message;
                    Console.WriteLine(errMsg);
                    Message = e.Message;
                    return false;
                }
            }

            return false;
        }

        public bool SendTable2RmatrixNonNumeric(string varName, DataTable mDtable)
        {
            var rArray = DataTable2RstrArray(mDtable);
            if (rArray.mstrMatrix != null)
            {
                try
                {
                    _rdn.SetSymbolCharMatrix("X", rArray.mstrMatrix);
                    //rCommand = varName + "<- getmatrix(X)";
                    _rCommand = varName + "<- X";
                    _rdn.EvaluateNoReturn(_rCommand);
                    _rdn.SetSymbolCharVector("colH", rArray.colHeaders);
                    _rdn.SetSymbolCharVector("rowN", rArray.rowNames);
                    _rCommand = "colnames(" + varName + ") <- colH";
                    _rdn.EvaluateNoReturn(_rCommand);
                    _rCommand = "rownames(" + varName + ") <- rowN";
                    _rdn.EvaluateNoReturn(_rCommand);

                    return true;
                }
                catch (Exception e)
                {
                    var errMsg = _rCommand + " " + e.Message;
                    Console.WriteLine(errMsg);
                    Message = e.Message;
                    return false;
                }
            }

            return false;
        }

        [Obsolete("Unused")]
        public bool sourceRcmds(string filename)
        {
            try
            {
                _rCommand = "source(\"" + filename + "\")";
                _rdn.EvaluateNoReturn(_rCommand);
                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool loadRData(string filename)
        {
            try
            {
                _rCommand = "load(\"" + filename + "\")";
                _rdn.EvaluateNoReturn(_rCommand);
                return true;
            }
            catch (Exception e)
            {
                var errMsg = _rCommand + " " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        public bool closeR()
        {
            try
            {
                _rCommand = "graphics.off()";
                _rdn.EvaluateNoReturn(_rCommand);
                _rCommand = "rm(list=ls(all=TRUE))";
                _rdn.EvaluateNoReturn(_rCommand);
                _rdn.Close();
                return true;
            }
            catch (Exception e)
            {
                var errMsg = "R Close failed: " + e.Message;
                Console.WriteLine(errMsg);
                Message = e.Message;
                return false;
            }
        }

        private static void CopyMatrixDataToTable(double[,] matrix, DataTable dataTable, IReadOnlyList<string> rowNames)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var dataRow = dataTable.NewRow();
                dataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    // Note: this functionality was moved from clsDataTable.ReplaceMissing because that function is very slow for large tables
                    // This procedure of looking for cells with a value of 999999 is due to this R code in MiscCommands.R
                    // See the comment above
                    //
                    // # Send matrix to the App. So, 'NA' should be replaced by '999999'
                    // sendmatrix <- function(x){
                    //   x[is.na(x)] <- 999999
                    //   x <- as.matrix(x)
                    //   return(x)
                    // }

                    if (Math.Abs(matrix[i, j] - 999999) < float.Epsilon)
                    {
                        dataRow[j + 1] = DBNull.Value;
                    }
                    else
                    {
                        dataRow[j + 1] = matrix[i, j];
                    }
                }
                dataTable.Rows.Add(dataRow);
            }
        }

        private DataTable RDoubleArray2DataTable(clsRarray rArray)
        {
            var matrix = rArray.matrix;
            var rowNames = rArray.rowNames;
            var colHeaders = rArray.colHeaders;
            var dataTable = new DataTable();

            var dataColumn1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = clsRarray.rowNamesID
            };
            dataTable.Columns.Add(dataColumn1);

            foreach (var columnName in colHeaders)
            {
                var dataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.Double"),
                    ColumnName = columnName
                };
                dataTable.Columns.Add(dataColumn);
            }

            CopyMatrixDataToTable(matrix, dataTable, rowNames);

            // return clsDataTable.ClearZeros(datatable);

            return dataTable;
        }

        private DataTable RDoubleVector2DataTable(clsRarray rArray)
        {
            var matrix = rArray.matrix;
            var rowNames = rArray.rowNames;
            var colHeaders = rArray.colHs;
            var dataTable = new DataTable();

            var dataColumn1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = clsRarray.rowNamesID
            };
            dataTable.Columns.Add(dataColumn1);

            var dataColumn2 = new DataColumn
            {
                DataType = Type.GetType("System.Double"),
                ColumnName = colHeaders
            };
            dataTable.Columns.Add(dataColumn2);

            CopyMatrixDataToTable(matrix, dataTable, rowNames);

            // return clsDataTable.ClearZeros(dataTable);

            return dataTable;
        }

        private DataTable RstrArray2DataTable(clsRarray rArray)
        {
            var matrix = rArray.mstrMatrix;
            var rowNames = rArray.rowNames;
            var colHeaders = rArray.colHeaders;
            var dataTable = new DataTable();

            var dataColumn1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = clsRarray.rowNamesID
            };
            dataTable.Columns.Add(dataColumn1);

            foreach (var columnName in colHeaders)
            {
                var dataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = columnName
                };
                dataTable.Columns.Add(dataColumn);
            }

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var dataRow = dataTable.NewRow();
                dataRow[0] = rowNames[i];

                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    dataRow[j + 1] = matrix[i, j];
                }
                dataTable.Rows.Add(dataRow);
            }
            //return clsDataTable.ClearZeros(datatable);
            return dataTable;
        }

        private DataTable RproteinInfoArray2DataTable(clsRarray rArray)
        {
            var matrix = rArray.mstrMatrix;
            var rowNames = rArray.rowNames;
            var colHeaders = rArray.colHeaders;
            var dataTable = new DataTable();

            var dataColumn1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = clsRarray.rowNamesID
            };
            dataTable.Columns.Add(dataColumn1);

            foreach (var columnName in colHeaders)
            {
                var dataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = columnName
                };

                dataTable.Columns.Add(dataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var dataRow = dataTable.NewRow();
                dataRow[0] = rowNames[i];

                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    dataRow[j + 1] = matrix[i, j];
                }
                dataTable.Rows.Add(dataRow);
            }
            //return clsDataTable.ClearZeros(datatable);
            return dataTable;
        }

        private DataTable RProteinArray2DataTable(clsRarray rArray)
        {
            var matrix = rArray.matrix;
            var rowNames = rArray.rowNames;
            var colHeaders = rArray.colHeaders;
            var dataTable = new DataTable();

            var dataColumn1 = new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = clsRarray.rowNamesID
            };
            dataTable.Columns.Add(dataColumn1);

            foreach (var columnName in colHeaders)
            {
                var dataColumn = new DataColumn
                {
                    DataType = Type.GetType("System.Double"),
                    ColumnName = columnName
                };
                dataTable.Columns.Add(dataColumn);
            }

            CopyMatrixDataToTable(matrix, dataTable, rowNames);

            // return clsDataTable.ClearZeros(datatable);

            return dataTable;
        }

        private clsRarray DataTable2Rarray(DataTable mTable, string varName)
        {
            var matrix = new double[mTable.Rows.Count, mTable.Columns.Count - 1];
            var rowNames = new string[mTable.Rows.Count];
            var colHeaders = new string[mTable.Columns.Count - 1];
            var rArray = new clsRarray();
            var typeError = false;

            //DataColumnCollection columnHeaders = mTable.Columns ;
            for (var col = 0; col < mTable.Columns.Count; col++)
            {
                if (col > 0)
                {
                    // Populate colHeaders with the user-supplied headers, starting with the second column
                    // The header for the first column is always Row_ID
                    colHeaders[col - 1] = mTable.Columns[col].ToString();
                }

                for (var row = 0; row < mTable.Rows.Count; row++)
                {
                    if (col == 0)
                    {
                        // Row identifier, typically the MassTagID (an integer), but can be a peptide sequence or any string
                        rowNames[row] = mTable.Rows[row].ItemArray[0].ToString();
                    }
                    else
                    {
                        // numeric data
                        var cellValue = mTable.Rows[row].ItemArray[col].ToString();
                        if (cellValue.Length > 0)
                        {
                            //not an empty cell
                            try
                            {
                                matrix[row, col - 1] = Convert.ToDouble(cellValue, NumberFormatInfo.InvariantInfo);
                            }
                            catch (FormatException)
                            {
                                var errMsg = string.Format("Invalid data, '{0}' is not numeric in dataset {1}",
                                                           cellValue, varName);
                                MessageBox.Show(errMsg, "Loading error", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                typeError = true;
                            }
                        }
                        else
                        {
                            // Empty cell
                            matrix[row, col - 1] = 0;
                        }
                    }

                    if (typeError)
                        break;
                }

                if (typeError)
                    break;
            }

            if (typeError)
                return rArray;

            rArray.rowNames = rowNames;
            rArray.colHeaders = colHeaders;
            rArray.matrix = matrix;

            return rArray;
        }

        private clsRarray DataTable2RstrArray(DataTable mTable)
        {
            var matrix = new string[mTable.Rows.Count, mTable.Columns.Count - 1];
            var rowNames = new string[mTable.Rows.Count];
            var colHeaders = new string[mTable.Columns.Count - 1];
            var rArray = new clsRarray();

            //DataColumnCollection columnHeaders = mTable.Columns ;
            for (var col = 0; col < mTable.Columns.Count; col++)
            {
                if (col > 0) //Start from 2nd column
                    colHeaders[col - 1] = mTable.Columns[col].ToString();
                for (var row = 0; row < mTable.Rows.Count; row++)
                {
                    if (col == 0)
                    {
                        //Mass Tags
                        rowNames[row] = mTable.Rows[row].ItemArray[0].ToString();
                    }
                    else
                    {
                        // string data
                        var cellValue = mTable.Rows[row].ItemArray[col].ToString();
                        if (cellValue.Length > 0) //not an empty cell
                            matrix[row, col - 1] = cellValue;
                        else
                            matrix[row, col - 1] = "NA";
                    }
                }
            }

            rArray.rowNames = rowNames;
            rArray.colHeaders = colHeaders;
            rArray.mstrMatrix = matrix;

            return rArray;
        }
    }

    /// <summary>
    /// R data matrix
    /// </summary>
    public class clsRarray
    {
        public double[,] matrix;
        public string[,] mstrMatrix;
        public string[] rowNames;
        public string[] colHeaders;
        public readonly string colHs;
        public static string rowNamesID = "Row_ID";

        public clsRarray()
        {
            matrix = null;
            mstrMatrix = null;
            rowNames = null;
            colHeaders = null;
        }

        public clsRarray(double[,] mat, string[] rows, string[] cols)
        {
            matrix = mat;
            mstrMatrix = null;
            rowNames = rows;
            colHeaders = cols;
        }

        public clsRarray(string[,] mat, string[] rows, string[] cols)
        {
            matrix = null;
            mstrMatrix = mat;
            rowNames = rows;
            colHeaders = cols;
        }

        public clsRarray(double[,] mat, string[] rows, string col)
        {
            matrix = mat;
            mstrMatrix = null;
            rowNames = rows;
            colHs = col;
        }
    }
}