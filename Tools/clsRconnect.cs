using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DAnTE.Tools
{
    /// <summary>
    /// Summary description for clsRconnect.
    /// </summary>
    public class clsRconnect
    {
        private RdnConnector _rdn = null;
        private string _rcmd;
        private clsRarray _mRarray; // R data.frame 

        public string[] RowNames { get; private set; }

        public string[] Vector { get; private set; }

        public DataTable DataTable { get; private set; }

        public string Message { get; private set; }

        public clsRconnect()
        {
            Message = null;
            _rcmd = "";
            _mRarray = new clsRarray();
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
            catch (Exception e)
            {
                var errmsg = "R Init failed: " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        #region R_Interface

        //public void SetCharacterOutputDevice(StatConnectorCommonLib.IStatConnectorCharacterDevice dev)
        //{
        //  _rdn.SetCharacterOutputDevice(dev);
        //}

        public void EvaluateNoReturn(string rcmd)
        {
            _rdn.EvaluateNoReturn(rcmd);
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

        public bool loadR(string table, string filename, bool stripwhite, bool header, string separator)
        {
            try
            {
                _rcmd = table + "<-loadfile('" + filename +
                  "',stripwhite=" + stripwhite.ToString().ToUpper() + ",header=" +
                  header.ToString().ToUpper() + ",separator='" + separator + "')";
                _rdn.EvaluateNoReturn(_rcmd);
                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRmatrix(string varName)
        {
            clsRarray mRarr;
            try
            {
                DataTable.Clear();
                _rcmd = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                mRarr = new clsRarray(matrix, rownames, colheaders);
                DataTable = RDoubleArray2DataTable(mRarr);

                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRvector(string varName)
        {
            clsRarray mRarr;
            try
            {
                _rcmd = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                var rowH = rownames as string[];
                var colH = colheaders[0];
                mRarr = new clsRarray(((double[,])matrix), rowH, colH);
                DataTable.Clear();
                DataTable = RDoubleVector2DataTable(mRarr);

                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRproteinMatrix(string varName)
        {
            clsRarray mRarr;
            try
            {
                _rcmd = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);

                var matrix = _rdn.GetSymbolAsNumberMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                mRarr = new clsRarray(matrix, rownames, colheaders);

                DataTable.Clear();
                DataTable = RProteinArray2DataTable(mRarr);

                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRmatrixNonNumeric(string varName)
        {
            clsRarray mRarr;
            try
            {
                _rcmd = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);

                var matrix = _rdn.GetSymbolAsStringMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                mRarr = new clsRarray(matrix, rownames, colheaders);

                DataTable = RstrArray2DataTable(mRarr);

                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetTableFromRProtInfoMatrix(string varName)
        {
            clsRarray mRarr;
            try
            {
                _rcmd = "X<-sendmatrix(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Headers<-colnames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);

                var matrix = _rdn.GetSymbolAsStringMatrix("X");
                var colheaders = _rdn.GetSymbolAsStrings("Headers");
                var rownames = _rdn.GetSymbolAsStrings("Rows");

                //mRarr = new clsRarray(matrix, rownames, colheaders);
                ////var m = ConvertZ(matrix);
                mRarr = new clsRarray(matrix, (string[])rownames, (string[])colheaders);

                DataTable = RproteinInfoArray2DataTable(mRarr);

                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool GetRowNamesFromRmatrix(string varName)
        {
            try
            {
                _rcmd = "Rows<-rownames(" + varName + ")";
                _rdn.EvaluateNoReturn(_rcmd);
                RowNames = _rdn.GetSymbolAsStrings("Rows");
                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
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
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool SendTable2RmatrixNumeric(string varName, DataTable mDtable)
        {
            var mRarrySend = DataTable2Rarray(mDtable);
            if (mRarrySend.matrix != null)
            {
                try
                {
                    _rdn.SetSymbolNumberMatrix("X", mRarrySend.matrix);
                    _rcmd = varName + "<- getmatrix(X)";
                    _rdn.EvaluateNoReturn(_rcmd);
                    _rdn.SetSymbolCharVector("colH", mRarrySend.colHeaders);
                    _rdn.SetSymbolCharVector("rowN", mRarrySend.rowNames);
                    _rcmd = "colnames(" + varName + ") <- colH";
                    _rdn.EvaluateNoReturn(_rcmd);
                    _rcmd = "rownames(" + varName + ") <- rowN";
                    _rdn.EvaluateNoReturn(_rcmd);
                    //rcmd = varName + "<- cleanmatrix(" + varName + ")"; // here's where the duplicates are removed
                    //rdcom.EvaluateNoReturn(rcmd);

                    return true;
                }
                catch (Exception e)
                {
                    var errmsg = _rcmd + " " + e.Message;
                    Console.WriteLine(errmsg);
                    Message = e.Message;
                    return false;
                }
            }
            else
                return false;
        }

        public bool SendTable2RmatrixNonNumeric(string varName, DataTable mDtable)
        {
            var mRarrySend = DataTable2RstrArray(mDtable);
            if (mRarrySend.mstrMatrix != null)
            {
                try
                {
                    _rdn.SetSymbolCharMatrix("X", mRarrySend.mstrMatrix);
                    //rcmd = varName + "<- getmatrix(X)";
                    _rcmd = varName + "<- X";
                    _rdn.EvaluateNoReturn(_rcmd);
                    _rdn.SetSymbolCharVector("colH", mRarrySend.colHeaders);
                    _rdn.SetSymbolCharVector("rowN", mRarrySend.rowNames);
                    _rcmd = "colnames(" + varName + ") <- colH";
                    _rdn.EvaluateNoReturn(_rcmd);
                    _rcmd = "rownames(" + varName + ") <- rowN";
                    _rdn.EvaluateNoReturn(_rcmd);

                    return true;
                }
                catch (Exception e)
                {
                    var errmsg = _rcmd + " " + e.Message;
                    Console.WriteLine(errmsg);
                    Message = e.Message;
                    return false;
                }
            }
            else
                return false;
        }

        public bool sourceRcmds(string filename)
        {
            try
            {
                _rcmd = "source(\"" + filename + "\")";
                _rdn.EvaluateNoReturn(_rcmd);
                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool loadRData(string filename)
        {
            try
            {
                _rcmd = "load(\"" + filename + "\")";
                _rdn.EvaluateNoReturn(_rcmd);
                return true;
            }
            catch (Exception e)
            {
                var errmsg = _rcmd + " " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }

        public bool closeR()
        {
            try
            {
                _rcmd = "graphics.off()";
                _rdn.EvaluateNoReturn(_rcmd);
                _rcmd = "rm(list=ls(all=TRUE))";
                _rdn.EvaluateNoReturn(_rcmd);
                _rdn.Close();
                return true;
            }
            catch (Exception e)
            {
                var errmsg = "R Close failed: " + e.Message;
                Console.WriteLine(errmsg);
                Message = e.Message;
                return false;
            }
        }



        private DataTable RDoubleArray2DataTable(clsRarray mRary)
        {
            var matrix = mRary.matrix;
            var rowNames = mRary.rowNames;
            var colHeaders = mRary.colHeaders;
            var mdatatable = new DataTable();
            DataColumn mDataColumn;
            DataRow mDataRow;

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = clsRarray.rowNamesID;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            for (var i = 0; i < colHeaders.Length; i++)
            {
                mDataColumn = new DataColumn();
                mDataColumn.DataType = System.Type.GetType("System.Double");
                mDataColumn.ColumnName = colHeaders[i];
                //mDataColumn.ReadOnly = true ;
                mdatatable.Columns.Add(mDataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                mDataRow = mdatatable.NewRow();
                mDataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j];
                }
                mdatatable.Rows.Add(mDataRow);
            }
            //return clsDataTable.ClearZeros(mdatatable);
            return clsDataTable.ReplaceMissing(mdatatable);
        }

        private DataTable RDoubleVector2DataTable(clsRarray mRary)
        {
            var matrix = mRary.matrix;
            var rowNames = mRary.rowNames;
            var colHeaders = mRary.colHs;
            var mdatatable = new DataTable();
            DataColumn mDataColumn;
            DataRow mDataRow;

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = clsRarray.rowNamesID;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.Double");
            mDataColumn.ColumnName = colHeaders;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                mDataRow = mdatatable.NewRow();
                mDataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j];
                }
                mdatatable.Rows.Add(mDataRow);
            }
            //return clsDataTable.ClearZeros(mdatatable);
            return clsDataTable.ReplaceMissing(mdatatable);
        }

        private DataTable RstrArray2DataTable(clsRarray mRary)
        {
            var matrix = mRary.mstrMatrix;
            var rowNames = mRary.rowNames;
            var colHeaders = mRary.colHeaders;
            var mdatatable = new DataTable();
            DataColumn mDataColumn;
            DataRow mDataRow;

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = clsRarray.rowNamesID;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            for (var i = 0; i < colHeaders.Length; i++)
            {
                mDataColumn = new DataColumn();
                mDataColumn.DataType = System.Type.GetType("System.String");
                mDataColumn.ColumnName = colHeaders[i];
                //mDataColumn.ReadOnly = true ;
                mdatatable.Columns.Add(mDataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                mDataRow = mdatatable.NewRow();
                mDataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j];
                }
                mdatatable.Rows.Add(mDataRow);
            }
            //return clsDataTable.ClearZeros(mdatatable);
            return mdatatable;
        }

        private DataTable RproteinInfoArray2DataTable(clsRarray mRary)
        {
            string[,] matrix;
            matrix = mRary.mstrMatrix;
            var rowNames = mRary.rowNames;
            var colHeaders = mRary.colHeaders;
            var mdatatable = new DataTable();
            DataColumn mDataColumn;
            DataRow mDataRow;

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = clsRarray.rowNamesID;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            for (var i = 0; i < colHeaders.Length; i++)
            {
                mDataColumn = new DataColumn();
                mDataColumn.DataType = System.Type.GetType("System.String");
                //if (i == 0)
                //    mDataColumn.DataType = System.Type.GetType("System.Double");
                //else
                //    mDataColumn.DataType = System.Type.GetType("System.String");
                mDataColumn.ColumnName = colHeaders[i];
                //mDataColumn.ReadOnly = true ;
                mdatatable.Columns.Add(mDataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                mDataRow = mdatatable.NewRow();
                mDataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j];
                }
                mdatatable.Rows.Add(mDataRow);
            }
            //return clsDataTable.ClearZeros(mdatatable);
            return mdatatable;
        }

        private DataTable RProteinArray2DataTable(clsRarray mRary)
        {
            var matrix = mRary.matrix;
            var rowNames = mRary.rowNames;
            var colHeaders = mRary.colHeaders;
            var mdatatable = new DataTable();
            DataColumn mDataColumn;
            DataRow mDataRow;

            mDataColumn = new DataColumn();
            mDataColumn.DataType = System.Type.GetType("System.String");
            mDataColumn.ColumnName = clsRarray.rowNamesID;
            //mDataColumn.ReadOnly = true ;
            mdatatable.Columns.Add(mDataColumn);

            for (var i = 0; i < colHeaders.Length; i++)
            {
                mDataColumn = new DataColumn();
                mDataColumn.DataType = System.Type.GetType("System.Double");
                mDataColumn.ColumnName = colHeaders[i];
                //mDataColumn.ReadOnly = true ;
                mdatatable.Columns.Add(mDataColumn);
            }
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                mDataRow = mdatatable.NewRow();
                mDataRow[0] = rowNames[i];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    mDataRow[j + 1] = matrix[i, j];
                }
                mdatatable.Rows.Add(mDataRow);
            }
            //return clsDataTable.ClearZeros(mdatatable);
            return clsDataTable.ReplaceMissing(mdatatable);
            //return mdatatable;
        }

        private clsRarray DataTable2Rarray(DataTable mTable)
        {
            var matrix = new double[mTable.Rows.Count, mTable.Columns.Count - 1];
            var rowNames = new string[mTable.Rows.Count];
            var colHeaders = new string[mTable.Columns.Count - 1];
            var mRary = new clsRarray();
            var typerror = false;

            //DataColumnCollection columnHeaders = mTable.Columns ;
            for (var col = 0; col < mTable.Columns.Count; col++)
            {
                if (typerror)
                    break;
                if (col > 0) //Start from 2nd column
                    colHeaders[col - 1] = mTable.Columns[col].ToString();
                for (var row = 0; row < mTable.Rows.Count; row++)
                {
                    if (typerror)
                        break;
                    if (col == 0) //Mass Tags
                        rowNames[row] = mTable.Rows[row].ItemArray[0].ToString();
                    else // numeric data
                    {
                        var cellValue = mTable.Rows[row].ItemArray[col].ToString();
                        if (cellValue.Length > 0) //not an empty cell
                            try
                            {
                                matrix[row, col - 1] = Convert.ToDouble(cellValue, NumberFormatInfo.InvariantInfo);
                            }
                            catch (FormatException ex)
                            {
                                MessageBox.Show("Invalid data type. Check for example, " +
                                    "if you have text strings mixed with numerical data.\n\nError:" +
                                    ex.Message, "File type error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                typerror = true;
                            }
                        else
                            matrix[row, col - 1] = 0;
                    }
                }
            }
            if (!typerror)
            {
                mRary.rowNames = rowNames;
                mRary.colHeaders = colHeaders;
                mRary.matrix = matrix;
            }
            else
            {
                _mRarray = null;
            }
            return mRary;
        }

        private clsRarray DataTable2RstrArray(DataTable mTable)
        {
            var matrix = new string[mTable.Rows.Count, mTable.Columns.Count - 1];
            var rowNames = new string[mTable.Rows.Count];
            var colHeaders = new string[mTable.Columns.Count - 1];
            string cellValue = null;
            var mRary = new clsRarray();
            var typerror = false;

            //DataColumnCollection columnHeaders = mTable.Columns ;
            for (var col = 0; col < mTable.Columns.Count; col++)
            {
                if (typerror)
                    break;
                if (col > 0) //Start from 2nd column
                    colHeaders[col - 1] = mTable.Columns[col].ToString();
                for (var row = 0; row < mTable.Rows.Count; row++)
                {
                    if (typerror)
                        break;
                    if (col == 0) //Mass Tags
                        rowNames[row] = mTable.Rows[row].ItemArray[0].ToString();
                    else // string data
                    {
                        cellValue = mTable.Rows[row].ItemArray[col].ToString();
                        if (cellValue.Length > 0) //not an empty cell
                            matrix[row, col - 1] = cellValue;
                        else
                            matrix[row, col - 1] = "NA";
                    }
                }
            }
            if (!typerror)
            {
                mRary.rowNames = rowNames;
                mRary.colHeaders = colHeaders;
                mRary.mstrMatrix = matrix;
            }
            else
            {
                _mRarray = null;
            }
            return mRary;
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
        public string colHs = null;
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
