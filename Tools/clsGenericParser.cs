//	GenericParsing
//	Copyright (c) 2005 Andrew Rissing
//
//	Permission is hereby granted, free of charge, to any person obtaining a copy
//	of this software and associated documentation files (the "Software"), to deal
//	in the Software without restriction, including without limitation the rights
//	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//	of the Software, and to permit persons to whom the Software is furnished to do so,
//	subject to the following conditions:
//
//	The above copyright notice and this permission notice shall be included in all
//	copies or substantial portions of the Software.
//
//	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//	INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//	PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//	FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//	ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#region Using Directives

using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Collections.Specialized;

#endregion Using Directives

namespace DAnTE.Tools
{

    #region GenericParsing Classes

    /// <summary>
    /// The <see cref="clsGenericParser"/> class is designed to be a flexible and efficient manner
    /// of parsing various flat files formats.
    /// </summary>
    /// <threadsafety static="false" instance="false"/>
    [Obsolete("Unused")]
    public class clsGenericParser : IDisposable
    {
        #region Constants

        private const char NULL_CHAR = '\0';

        /// <summary>
        /// The current internal state of the parser.
        /// </summary>
        public enum ParserState
        {
            /// <summary>Indicates that the parser has no datasource and is not properly setup.</summary>
            NoDataSource = 0,

            /// <summary>Indicates that the parser is ready to begin parsing.</summary>
            Ready = 1,

            /// <summary>Indicates that the parser is currently parsing the datasource.</summary>
            Parsing = 2,

            /// <summary>Indicates that the parser has finished parsing the datasource.</summary>
            Finished = 3
        }

        private enum RowState
        {
            GetRowType = 0,
            CommentRow = 1,
            HeaderRow = 2,
            SkippedRow = 3,
            DataRow = 4
        }

        private enum BufferState
        {
            NoAction = 0,
            FetchData = 1,
            NoFetchableData = 2,
            NoDataLeft = 3
        }

        #region Default Values

        private const int DEFAULT_MAX_BUFFER_SIZE = 1024;
        private const int DEFAULT_MAX_ROWS = 0;
        private const int DEFAULT_SKIP_DATA_ROWS = 0;
        private const int DEFAULT_EXPECTED_COLUMN_COUNT = 0;
        private const bool DEFAULT_FIRST_ROW_HAS_HEADER = false;
        private const bool DEFAULT_TRIM_RESULTS = false;
        private const bool DEFAULT_FIXED_WIDTH = false;
        private const string DEFAULT_ROW_DELIMITER = ",";
        private const char DEFAULT_TEXT_QUALIFIER = '\"';
        private const char DEFAULT_ESCAPE_CHARACTER = NULL_CHAR;
        private const char DEFAULT_COMMENT_CHARACTER = '#';

        #endregion Default Values

        #region XmlConfig Constants

        private const string XML_ROOT_NODE = "clsGenericParser";
        private const string XML_COLUMN_WIDTH = "ColumnWidth";
        private const string XML_COLUMN_WIDTHS = "ColumnWidths";
        private const string XML_MAX_BUFFER_SIZE = "MaxBufferSize";
        private const string XML_MAX_ROWS = "MaxRows";
        private const string XML_SKIP_DATA_ROWS = "SkipDataRows";
        private const string XML_EXPECTED_COLUMN_COUNT = "ExpectedColumnCount";
        private const string XML_FIRST_ROW_HAS_HEADER = "FirstRowHasHeader";
        private const string XML_TRIM_RESULTS = "TrimResults";
        private const string XML_FIXED_WIDTH = "FixedWidth";
        private const string XML_ROW_DELIMITER = "RowDelimiter";
        private const string XML_COLUMN_DELIMITER = "ColumnDelimiter";
        private const string XML_TEXT_QUALIFIER = "TextQualifier";
        private const string XML_ESCAPE_CHARACTER = "EscapeCharacter";
        private const string XML_COMMENT_CHARACTER = "CommentCharacter";

        private const string XML_SAFE_STRING_DELIMITER = ",";

        #endregion XmlConfig Constants

        #endregion Constants

        #region Constructors

        /// <summary>
        ///   Constructs an instance of a <see cref="clsGenericParser"/> with the default settings.
        /// </summary>
        /// <remarks>
        ///   If you use this constructor, you must set the datasource
        ///   prior to using the parser (using <see cref="SetDataSource"/>), otherwise an
        ///   exception will be thrown.
        /// </remarks>
        [Obsolete("Unused")]
        protected clsGenericParser()
        {
            m_ParserState = ParserState.NoDataSource;
            m_txtReader = null;
            m_blnDisposed = false;

            m_iaColumnWidths = null;
            m_intMaxBufferSize = DEFAULT_MAX_BUFFER_SIZE;
            m_intMaxRows = DEFAULT_MAX_ROWS;
            m_intSkipDataRows = DEFAULT_SKIP_DATA_ROWS;
            m_intExpectedColumnCount = DEFAULT_EXPECTED_COLUMN_COUNT;
            m_blnFirstRowHasHeader = DEFAULT_FIRST_ROW_HAS_HEADER;
            m_blnTrimResults = DEFAULT_TRIM_RESULTS;
            m_blnFixedWidth = DEFAULT_FIXED_WIDTH;
            m_caRowDelimiter = Environment.NewLine.ToCharArray();
            m_caColumnDelimiter = DEFAULT_ROW_DELIMITER.ToCharArray();
            m_chTextQualifier = DEFAULT_TEXT_QUALIFIER;
            m_chEscapeCharacter = DEFAULT_ESCAPE_CHARACTER;
            m_chCommentCharacter = DEFAULT_COMMENT_CHARACTER;
        }

        /// <summary>
        ///   Constructs an instance of a <see cref="clsGenericParser"/> and sets the initial datasource
        ///   as the file referenced by the string passed in.
        /// </summary>
        /// <param name="strFileName">The file name to set as the initial datasource.</param>
        public clsGenericParser(string strFileName) : this()
        {
            SetDataSource(strFileName);
        }

        /// <summary>
        ///   Constructs an instance of a <see cref="clsGenericParser"/> and sets the initial datasource
        ///   as the <see cref="TextReader"/> passed in.
        /// </summary>
        /// <param name="txtReader">The <see cref="TextReader"/> containing the data to be parsed.</param>
        public clsGenericParser(TextReader txtReader) : this()
        {
            SetDataSource(txtReader);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///	  Indicates whether or not the instance has been disposed of.
        /// </summary>
        /// <value>
        ///   <para>
        ///     <see langword="true"/> - Indicates the instance has be disposed of.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Indicates the instance has not be disposed of.
        ///   </para>
        /// </value>
        [System.ComponentModel.Browsable(false)]
        public bool IsDisposed => m_blnDisposed;

        /// <summary>
        ///   <para>
        ///     An integer array indicating the number of spaces needed for each
        ///     column.
        ///   </para>
        ///   <para>
        ///     By setting this properly, you automatically set the <see cref="clsGenericParser"/>
        ///     for <see cref="FixedWidth"/> format and the <see cref="ExpectedColumnCount"/> is set.
        ///   </para>
        /// </summary>
        /// <value>An int[] containing the number of spaces for each column.</value>
        /// <remarks>If you have already started parsing, you cannot update this value.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Passing in an empty array or an
        /// array of values that have a number less than one.</exception>
        public int[] ColumnWidths
        {
            get
            {
                _CheckDiposed();

                return m_iaColumnWidths;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_iaColumnWidths = value;

                if (value == null)
                    m_blnFixedWidth = false;
                else
                {
                    if (m_iaColumnWidths.Length < 1)
                        throw new ArgumentOutOfRangeException(nameof(ColumnWidths),
                                                              "You cannot set the value of ColumnWidths to an empty array.");

                    // Make sure all of the ColumnWidths are valid.
                    foreach (var columnWidth in m_iaColumnWidths)
                    {
                        if (columnWidth < 1)
                            throw new ArgumentOutOfRangeException(nameof(ColumnWidths),
                                                                  "You cannot set the value of a ColumnWidth to a number less than one.");
                    }

                    m_blnFixedWidth = true;
                    m_intExpectedColumnCount = m_iaColumnWidths.Length;
                }
            }
        }

        /// <summary>
        ///   <para>
        ///     Determines the maximum size of the internal buffer used to cache the
        ///     data.  The <see cref="MaxBufferSize"/> must be atleast the size of one column of data,
        ///     plus the Max(column delimiter width, row delimiter width).
        ///   </para>
        ///   <para>
        ///     Maintaining the smallest number possible here improves memory usage, but
        ///     trades it off for higher CPU usage.
        ///   </para>
        ///   <para>
        ///     Default: 1024
        ///   </para>
        /// </summary>
        /// <value>The maximum size of the internal buffer to cache data from the
        /// datasource.</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Setting the value to something less than one.</exception>
        public int MaxBufferSize
        {
            get
            {
                _CheckDiposed();

                return m_intMaxBufferSize;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                if (value > 0)
                    m_intMaxBufferSize = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(MaxBufferSize), value,
                                                          "The value must be greater than 0.");
            }
        }

        /// <summary>
        ///   <para>
        ///     Indicates the maximum number of rows you wish to parse.
        ///   </para>
        ///   <para>
        ///     Setting the value to zero will cause all of the rows to be returned.
        ///   </para>
        ///  <para>
        ///    Default: 0
        ///  </para>
        /// </summary>
        /// <value>The maximum number of rows you wish to parse.</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        public int MaxRows
        {
            get
            {
                _CheckDiposed();

                return m_intMaxRows;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_intMaxRows = value;

                if (m_intMaxRows < 0)
                    m_intMaxRows = 0;
            }
        }

        /// <summary>
        ///   <para>
        ///     Indicates the number of rows of data you wish to ignore at the beginning of
        ///     the file. So, the header row (if present) and comment rows will not be taken
        ///     into account when determining the number of rows to skip.
        ///   </para>
        ///   <para>
        ///     Setting the value to zero will indicate you do not wish to ignore any rows.
        ///   </para>
        ///   <para>
        ///     Default: 0
        ///   </para>
        /// </summary>
        /// <value>The number of data rows to initially skip in the datasource</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        public int SkipDataRows
        {
            get
            {
                _CheckDiposed();

                return m_intSkipDataRows;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_intSkipDataRows = value;

                if (m_intSkipDataRows < 0)
                    m_intSkipDataRows = 0;
            }
        }

        /// <summary>
        ///   Indicates the number of rows of data that have currently been parsed.
        /// </summary>
        /// <value>The number of rows of data that have been parsed.</value>
        /// <remarks>The DataRowNumber property is read-only.</remarks>
        public int DataRowNumber
        {
            get
            {
                _CheckDiposed();

                return m_intDataRowNumber;
            }
        }

        /// <summary>
        ///   Indicates how many rows in the file have been parsed.
        /// </summary>
        /// <value>The number of rows in the file that have been parsed.</value>
        /// <remarks>The <see cref="FileRowNumber"/> property is read-only and includes all
        /// rows possible (header, comment, and data).</remarks>
        public int FileRowNumber
        {
            get
            {
                _CheckDiposed();

                return m_intFileRowNumber;
            }
        }

        /// <summary>
        ///   <para>
        ///     Indicates the expected number of columns to find in the data.  If
        ///     the number of columns differs, an exception will be thrown.
        ///   </para>
        ///   <para>
        ///     By setting <see cref="ColumnWidths"/>, this property is automatically set.
        ///   </para>
        ///   <para>
        ///     Setting the value to zero will cause the <see cref="clsGenericParser"/> to ignore
        ///     the column count in case the number changes per row.
        ///   </para>
        ///   <para>
        ///     Default: 0
        ///   </para>
        /// </summary>
        /// <value>The number of columns expected per row of data.</value>
        /// <remarks>
        ///   If you have already started parsing, you cannot update this
        ///   value.
        /// </remarks>
        public int ExpectedColumnCount
        {
            get
            {
                _CheckDiposed();

                return m_intExpectedColumnCount;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_intExpectedColumnCount = value;

                // Make sure the ExpectedColumnCount matches the column width's
                // supplied.
                if (m_blnFixedWidth
                    && (m_iaColumnWidths != null)
                    && (m_iaColumnWidths.Length != m_intExpectedColumnCount))
                {
                    // Null it out to force the proper column width's to be supplied.
                    m_iaColumnWidths = null;
                    m_blnFixedWidth = false;
                }
                else if (m_intExpectedColumnCount < 0)
                    m_intExpectedColumnCount = 0;
            }
        }

        /// <summary>
        ///   <para>
        ///     Indicates whether or not the first row of data in the file contains
        ///     the header information.
        ///   </para>
        ///   <para>
        ///     Default: <see langword="false"/>
        ///   </para>
        /// </summary>
        /// <value>
        ///   <para>
        ///     <see langword="true"/> - Header found on first 'datarow'.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Header row does not exist.
        ///   </para>
        /// </value>
        /// <remarks>
        ///   If you have already started parsing, you cannot update this value.
        /// </remarks>
        public bool FirstRowHasHeader
        {
            get
            {
                _CheckDiposed();

                return m_blnFirstRowHasHeader;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_blnFirstRowHasHeader = value;
            }
        }

        /// <summary>
        ///   <para>
        ///     Indicates whether or not to trim the values for each column.
        ///   </para>
        ///   <para>
        ///     Default: <see langword="false"/>
        ///   </para>
        /// </summary>
        /// <value>
        ///   <para>
        ///     <see langword="true"/> - Indicates to trim the resulting strings.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Indicates to not trim the resulting strings.
        ///   </para>
        /// </value>
        /// <remarks>
        ///   <para>
        ///     If you have already started parsing, you cannot update this value.
        ///   </para>
        ///   <para>
        ///     Trimming only occurs on the strings if they are not text qualified.
        ///     So by placing values in quotes, it preserves all whitespace within
        ///     quotes.
        ///   </para>
        /// </remarks>
        public bool TrimResults
        {
            get
            {
                _CheckDiposed();

                return m_blnTrimResults;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_blnTrimResults = value;
            }
        }

        /// <summary>
        ///   Indicates that the data to be parsed is delimited into columns by
        ///   a fixed width of characters.
        /// </summary>
        /// <value>
        ///   <para>
        ///     <see langword="true"/> - Indicates the file's format is fixed width.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Indicates the file's format is delimited.
        ///   </para>
        /// </value>
        /// <remarks>
        ///   <para>
        ///     By setting <see cref="ColumnWidths"/>, this property is automatically set.
        ///   </para>
        ///   <para>
        ///     If you have already started parsing, you cannot update this value.
        ///   </para>
        /// </remarks>
        public bool FixedWidth
        {
            get
            {
                _CheckDiposed();

                return m_blnFixedWidth;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_blnFixedWidth = value;

                if (m_blnFixedWidth)
                    m_caColumnDelimiter = null;
                else
                    m_iaColumnWidths = null;
            }
        }

        /// <summary>
        ///   Retrieves the <see cref="ParserState"/> value indicating the current
        ///   internal state of the parser.
        /// </summary>
        /// <value>The <see cref="State"/> property is read-only and is used to return
        /// information about the internal state of the parser.</value>
        public ParserState State
        {
            get
            {
                _CheckDiposed();

                return m_ParserState;
            }
        }

        /// <summary>
        ///   <para>
        ///     Contains the character array used to match the end of a row of data.
        ///   </para>
        ///   <para>
        ///     Default: <see cref="System.Environment.NewLine"/>
        ///   </para>
        /// </summary>
        /// <value>It is the char[] of the row delimiter used in the data-source.</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        /// <exception cref="ArgumentNullException">Passing in <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Passing in an empty char[].</exception>
        public char[] RowDelimiter
        {
            get
            {
                _CheckDiposed();

                return m_caRowDelimiter;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                if (value == null)
                    throw new ArgumentNullException(nameof(RowDelimiter),
                                                    "You cannot set the value of RowDelimiter to null.");
                if (value.Length < 1)
                    throw new ArgumentException("You cannot set the value of RowDelimiter to an empty array.",
                                                nameof(RowDelimiter));
                m_caRowDelimiter = value;
            }
        }

        /// <summary>
        ///   <para>
        ///     Contains the char[] used to match the end of a column of data.
        ///   </para>
        ///   <para>
        ///     This is only meaningful when performing delimited parsing.
        ///   </para>
        ///   <para>
        ///     Default: ","
        ///   </para>
        /// </summary>
        /// <value>Contains the char's that are used to delimit a column.</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        /// <exception cref="ArgumentNullException">Passing in <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Passing in an empty char[].</exception>
        public char[] ColumnDelimiter
        {
            get
            {
                _CheckDiposed();

                return m_caColumnDelimiter;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                if (value == null)
                    throw new ArgumentNullException(nameof(ColumnDelimiter),
                                                    "You cannot set the value of ColumnDelimiter to null.");
                if (value.Length < 1)
                    throw new ArgumentException("You cannot set the value of ColumnDelimiter to an empty array.",
                                                nameof(ColumnDelimiter));
                m_caColumnDelimiter = value;
            }
        }

        /// <summary>
        ///   <para>
        ///     Contains the character that is used to enclose a string that would otherwise
        ///     be potentially trimmed (Ex. "  this  ").
        ///   </para>
        ///   <para>
        ///     Default: '\"'
        ///   </para>
        /// </summary>
        /// <value>The character used to enclose a string, so that row/column delimiters are
        /// ignored and whitespace is preserved.</value>
        /// <remarks>If you have already started parsing, you cannot update this
        /// value.</remarks>
        public char TextQualifier
        {
            get
            {
                _CheckDiposed();

                return m_chTextQualifier;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_chTextQualifier = value;
            }
        }

        /// <summary>
        ///   <para>
        ///     Contains the character that is used to escape a character (Ex. "\"This\"").
        ///   </para>
        ///   <para>
        ///     Upon parsing the file, the escaped characters will be stripped out, leaving
        ///     the desired character in place.  To produce the escaped character, use the
        ///     escaped character twice (Ex. \\).
        ///   </para>
        ///   <para>
        ///     Default: '\0'
        ///   </para>
        /// </summary>
        /// <value>The character used to escape row/column delimiters and the text qualifier.</value>
        /// <remarks>
        ///   <para>
        ///     If you have already started parsing, you cannot update this value.
        ///   </para>
        ///   <para>
        ///     Setting this to <see langword="null"/> causes a performance boost, if none of the values are
        ///     expected to require escaping.
        ///   </para>
        /// </remarks>
        public char EscapeCharacter
        {
            get
            {
                _CheckDiposed();

                return m_chEscapeCharacter;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_chEscapeCharacter = value;
            }
        }

        /// <summary>
        ///   <para>
        ///     Contains the character that is used to mark the beginning of a row that contains
        ///     purely comments and that should not be parsed.
        ///   </para>
        ///   <para>
        ///     Default: '#'
        ///   </para>
        /// </summary>
        /// <value>
        ///   The character used to indicate the current row is to be ignored as a comment.
        /// </value>
        /// <remarks>
        ///   If you have already started parsing, you cannot update this value.
        /// </remarks>
        public char CommentCharacter
        {
            get
            {
                _CheckDiposed();

                return m_chCommentCharacter;
            }
            set
            {
                _CheckDiposed();

                if (m_ParserState == ParserState.Parsing)
                    return;

                m_chCommentCharacter = value;
            }
        }

        /// <summary>
        ///   Accesses the data found in the current row of data by the column index.
        /// </summary>
        /// <value>The value of the column at the given index.</value>
        /// <param name="intColumnIndex">The index of the Column to retreive.</param>
        /// <remarks>
        ///   If the column is outside the bounds of the columns found or the column
        ///   does not possess a name, it will return <see langword="null"/>.
        /// </remarks>
        protected string this[int intColumnIndex]
        {
            get
            {
                _CheckDiposed();

                if ((intColumnIndex > -1) && (intColumnIndex < m_scData.Count))
                    return m_scData[intColumnIndex];

                return null;
            }
        }

        /// <summary>
        ///   Accesses the data found in the current row of data by the column name.
        /// </summary>
        /// <value>The value of the column with the given column name.</value>
        /// <param name="strColumnName">The name of the Column to retreive.</param>
        /// <remarks>
        ///   If the header has yet to be parsed (or no header exists), the property will
        ///   return <see langword="null"/>.
        /// </remarks>
        public string this[string strColumnName]
        {
            get
            {
                _CheckDiposed();

                return this[_GetColumnIndex(strColumnName)];
            }
        }

        /// <summary>
        ///   Returns the number of columns found in the current row
        ///   that was read.
        /// </summary>
        /// <value>The number of columns found in the current row.</value>
        /// <remarks>The <see cref="ColumnCount"/> property is read-only.</remarks>
        public int ColumnCount
        {
            get
            {
                _CheckDiposed();

                return m_scData.Count;
            }
        }

        #endregion Public Properties

        #region Public Methods

        #region Setting the Data Source

        /// <summary>
        ///   Sets the file as the datasource.
        /// </summary>
        /// <remarks>
        ///   If the parser is currently parsing a file, all data associated
        ///   with the previous file is lost and the parser is reset back to
        ///   its initial values.
        /// </remarks>
        /// <param name="strFileName">The <see cref="string"/> containing the name of the file
        /// to set as the data source.</param>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     clsGenericParser p = new clsGenericParser();
        ///     p.SetDataSource(@"C:\MyData.txt");
        ///   </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Supplying <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Supplying a filename to a file that does not exist.</exception>
        public void SetDataSource(string strFileName)
        {
            _CheckDiposed();

            if (strFileName == null)
                throw new ArgumentNullException(nameof(strFileName),
                                                "You cannot supply a null for the value of strFileName.");

            if (!File.Exists(strFileName))
                throw new ArgumentException(string.Format("File, {0}, does not exist.", strFileName),
                                            nameof(strFileName));

            m_ParserState = ParserState.Ready;
            m_txtReader = new StreamReader(strFileName);
        }

        /// <summary>
        ///   Sets the <see cref="TextReader"/> as the datasource.
        /// </summary>
        /// <param name="txtReader">The <see cref="TextReader"/> that contains the data to be parsed.</param>
        /// <remarks>
        ///   If the parser is currently parsing a file, all data associated with the
        ///   previous file is lost and the parser is reset back to its initial values.
        /// </remarks>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     clsGenericParser p = new clsGenericParser();
        ///     StreamReader srReader = new StreamReader(@"C:\MyData.txt");
        ///     p.SetDataSource(srReader);
        ///   </code>
        /// </example>
        /// <exception cref="ArgumentNullException">Supplying <see langword="null"/>.</exception>
        public void SetDataSource(TextReader txtReader)
        {
            _CheckDiposed();

            if (txtReader == null)
                throw new ArgumentNullException(nameof(txtReader),
                                                "You cannot supply a null for the value of txtReader.");

            m_ParserState = ParserState.Ready;
            m_txtReader = txtReader;
        }

        #endregion Setting the Data Source

        #region Parsing

        /// <summary>
        ///   <para>
        ///     Parses the data-source till it arrives at one row of data.
        ///   </para>
        /// </summary>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - Successfully parsed a new data row.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - No new data rows were found.
        ///   </para>
        /// </returns>
        /// <remarks>
        ///   <para>
        ///     If it finds a header, and its expecting a header row, it will not stop
        ///     at the row and continue on till it has found a row of data.
        ///   </para>
        ///   <para>
        ///     Internally, the header row is treated as a data row, but will not cause
        ///     the parser to stop after finding it.
        ///   </para>
        /// </remarks>
        /// <exception cref="ParserSetupException">Attempting to read without properly
        /// setting up the <see cref="clsGenericParser"/>.</exception>
        /// <exception cref="ParsingException">Thrown in the situations where the <see cref="clsGenericParser"/>
        /// cannot continue due to a conflict between the setup and the data being parsed.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     clsGenericParser p = new clsGenericParser();
        ///     p.SetDataSource(@"C:\MyData.txt");
        ///
        ///     while(p.Read())
        ///     {
        ///       // Put code here to retrieve results of the read.
        ///     }
        ///
        ///     p.Close();
        ///   </code>
        /// </example>
        public bool Read()
        {
            _CheckDiposed();

            // Setup some internal variables for the parsing.
            _InitializeParse();

            // Do we need to stop parsing rows.
            if (m_ParserState == ParserState.Finished)
                return false;

            // Read chunks of the data into the buffer, then parse each chunk for data.
            while (_ReadDataIntoBuffer())
            {
                while (m_intCurrentIndex < m_intCharactersInBuffer)
                {
                    // If we're in the state of parsing the row type, it means we're at the beginning of a row.
                    if (m_ParserRowState == RowState.GetRowType)
                    {
                        _ParseRowType();

                        // In the event that we read in a comment row, we need to skip
                        // the comment character, just in case they use it for something
                        // else in the file (row delimiter possibly).
                        if (m_ParserRowState == RowState.CommentRow)
                        {
                            ++m_intCurrentIndex;
                            continue;
                        }
                    }

                    ////////////////////////////////////////////////
                    // At this point, we're parsing character by  //
                    // character to find the end of a row/column. //
                    ////////////////////////////////////////////////

                    // If we're in comment, we want to bypass any special considerations and just
                    // find the end.
                    if (m_ParserRowState != RowState.CommentRow)
                    {
                        if (m_blnEscapeCharacterFound)
                        {
                            m_blnEscapeCharacterFound = false;
                            ++m_intCurrentIndex;
                            continue;
                        }

                        // Skip this character and then the next one, so that we ignore the escaped character.
                        if (m_caBuffer[m_intCurrentIndex] == m_chEscapeCharacter)
                        {
                            m_blnEscapeCharacterFound = true;
                            ++m_intCurrentIndex;
                            continue;
                        }

                        // Text qualifiers cause us to ignore row/column delimiters.
                        if (m_caBuffer[m_intCurrentIndex] == m_chTextQualifier)
                        {
                            m_blnInText = !m_blnInText;
                            ++m_intCurrentIndex;
                            continue;
                        }

                        // If we're still within text, so we don't care about the row/column delimiters.
                        if (m_blnInText)
                        {
                            ++m_intCurrentIndex;
                            continue;
                        }
                    }

                    if (_IsEndOfRow())
                    {
                        // Move back one character to get the last character in the column
                        // (ended with row delimiter).
                        if (!_IsEmptyRow(m_intCurrentIndex))
                        {
                            if ((m_ParserRowState == RowState.DataRow)
                                || (m_ParserRowState == RowState.SkippedRow))
                                ++m_intDataRowNumber;

                            if ((m_ParserRowState == RowState.DataRow)
                                || (m_ParserRowState == RowState.HeaderRow))
                                _ExtractColumn(m_intCurrentIndex - 1);
                        }

                        // Add the length of the RowDelimiter to the CurrentIndex to move us along.
                        m_intCurrentIndex += m_caRowDelimiter.Length;
                        m_intCurrentColumnStartIndex = m_intCurrentIndex;
                        m_blnEndOfRowFound = true;

                        // Ensure that we have some data, before trying to do something with it.
                        // This prevents problems with empty rows.
                        if (m_scData.Count > 0)
                        {
                            // Have we got a row that meets our expected number of columns.
                            if ((m_intExpectedColumnCount > 0) && (m_scData.Count != m_intExpectedColumnCount))
                                throw new ParsingException(
                                    string.Format(
                                        "Number of columns ({0}) differs from the expected column count ({1}).",
                                        m_scData.Count,
                                        m_intExpectedColumnCount),
                                    m_intFileRowNumber);

                            // If we were in a data row, we need to stop.
                            if (m_ParserRowState == RowState.DataRow)
                                break;
                            if (m_ParserRowState == RowState.HeaderRow)
                                _SetColumnNames();
                        }

                        m_ParserRowState = RowState.GetRowType;
                        continue;
                    }

                    if ((m_ParserRowState != RowState.CommentRow) && _IsEndOfColumn())
                    {
                        // Mark that this row has an end of column (ensures that we didn't find an empty row).
                        m_blnEndOfColumnFound = true;

                        // Move back one character to get the last character in the column
                        // (ended with column delimiter).
                        if ((m_ParserRowState == RowState.DataRow)
                            || (m_ParserRowState == RowState.HeaderRow))
                            _ExtractColumn(m_intCurrentIndex - 1);

                        // Add the length of the ColumnDelimiter to the CurrentIndex to move us along.
                        if (!m_blnFixedWidth)
                            m_intCurrentIndex += m_caColumnDelimiter.Length;

                        // Set the start of the column indice at the start of a new column.
                        m_intCurrentColumnStartIndex = m_intCurrentIndex;
                        continue;
                    }

                    ++m_intCurrentIndex;
                }

                // We found the end of a row..return normally.
                if (m_blnEndOfRowFound)
                    return (m_scData.Count > 0);
                //////////////////////////////////////////////////
                // At this point, the buffer has been expended. //
                //////////////////////////////////////////////////

                // We ran out of data, flush out the last column and return.
                if (m_BufferState == BufferState.NoFetchableData)
                {
                    m_BufferState = BufferState.NoDataLeft;
                    m_ParserState = ParserState.Finished;

                    if (!_IsEmptyRow(m_intCurrentIndex))
                    {
                        if ((m_ParserRowState == RowState.DataRow)
                            || (m_ParserRowState == RowState.SkippedRow))
                            ++m_intDataRowNumber;

                        // Move back one character to get the last character in the column (ended with EOF).
                        if (m_ParserRowState == RowState.DataRow)
                        {
                            // There's one column left to extract.
                            _ExtractColumn(m_intCurrentIndex - 1);
                            return (m_scData.Count > 0);
                        }
                    }

                    // There's nothing left to extract.
                    return false;
                }

                // Move the leftover data in the buffer to the front and start over.
                _CopyRemainingDataToFront(m_intCurrentColumnStartIndex);

                // Indicate that we need to fetch more data.
                m_BufferState = BufferState.FetchData;
            }

            m_ParserState = ParserState.Finished;
            return false;
        }

        #endregion Parsing

        #region Loading

        /// <summary>
        /// Loads the configuration of the <see cref="clsGenericParser"/> object from an <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="xrConfigXmlFile">The <see cref="XmlReader"/> containing the XmlConfig file to load configuration from.</param>
        /// <exception cref="ArgumentException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentException"/> could be thrown.</exception>
        /// <exception cref="ArgumentNullException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentNullException"/> could be thrown.</exception>
        /// <exception cref="ArgumentOutOfRangeException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentOutOfRangeException"/> could be thrown.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     FileStream fs = new FileStream(@"C:\MyData.txt", FileMode.Open);
        ///     XmlTextReader xmlTextReader = new XmlTextReader(fs);
        ///
        ///     clsGenericParser p = new clsGenericParser();
        ///     p.Load(xmlTextReader);
        ///   </code>
        /// </example>
        public void Load(XmlReader xrConfigXmlFile)
        {
            var xmlConfig = new XmlDocument();

            xmlConfig.Load(xrConfigXmlFile);

            Load(xmlConfig);
        }

        /// <summary>
        /// Loads the configuration of the <see cref="clsGenericParser"/> object from an <see cref="TextReader"/>.
        /// </summary>
        /// <param name="trConfigXmlFile">The <see cref="TextReader"/> containing the XmlConfig file to load configuration from.</param>
        /// <exception cref="ArgumentException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentException"/> could be thrown.</exception>
        /// <exception cref="ArgumentNullException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentNullException"/> could be thrown.</exception>
        /// <exception cref="ArgumentOutOfRangeException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentOutOfRangeException"/> could be thrown.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     StreamReader sr = new StreamReader(@"C:\MyData.txt");
        ///
        ///     clsGenericParser p = new clsGenericParser();
        ///     p.Load(sr);
        ///   </code>
        /// </example>
        public void Load(TextReader trConfigXmlFile)
        {
            var xmlConfig = new XmlDocument();

            xmlConfig.Load(trConfigXmlFile);

            Load(xmlConfig);
        }

        /// <summary>
        /// Loads the configuration of the <see cref="clsGenericParser"/> object from an <see cref="Stream"/>.
        /// </summary>
        /// <param name="sConfigXmlFile">The <see cref="Stream"/> containing the XmlConfig file to load configuration from.</param>
        /// <exception cref="ArgumentException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentException"/> could be thrown.</exception>
        /// <exception cref="ArgumentNullException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentNullException"/> could be thrown.</exception>
        /// <exception cref="ArgumentOutOfRangeException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentOutOfRangeException"/> could be thrown.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     FileStream fs = new FileStream(@"C:\MyData.txt", FileMode.Open);
        ///
        ///     clsGenericParser p = new clsGenericParser();
        ///     p.Load(fs);
        ///   </code>
        /// </example>
        public void Load(Stream sConfigXmlFile)
        {
            var xmlConfig = new XmlDocument();

            xmlConfig.Load(sConfigXmlFile);

            Load(xmlConfig);
        }

        /// <summary>
        /// Loads the configuration of the <see cref="clsGenericParser"/> object from a file on the file system.
        /// </summary>
        /// <param name="strConfigXmlFile">The full path to the XmlConfig file on the file system.</param>
        /// <exception cref="ArgumentException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentException"/> could be thrown.</exception>
        /// <exception cref="ArgumentNullException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentNullException"/> could be thrown.</exception>
        /// <exception cref="ArgumentOutOfRangeException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentOutOfRangeException"/> could be thrown.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParser();
        ///     p.Load(@"C:\MyData.txt");
        ///   </code>
        /// </example>
        public void Load(string strConfigXmlFile)
        {
            var xmlConfig = new XmlDocument();

            xmlConfig.Load(strConfigXmlFile);

            Load(xmlConfig);
        }

        /// <summary>
        /// Loads the configuration of the <see cref="clsGenericParser"/> object from an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="xmlConfig">The <see cref="XmlDocument"/> object containing the configuration information.</param>
        /// <exception cref="ArgumentException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentException"/> could be thrown.</exception>
        /// <exception cref="ArgumentNullException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentNullException"/> could be thrown.</exception>
        /// <exception cref="ArgumentOutOfRangeException">In the event that the XmlConfig file contains a value that is invalid,
        /// an <see cref="ArgumentOutOfRangeException"/> could be thrown.</exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParser();
        ///     XmlDocument xmlConfig = new XmlDocument();
        ///     xmlConfig.Load(strConfigXmlFile);
        ///
        ///     p.Load(xmlConfig);
        ///   </code>
        /// </example>
        public virtual void Load(XmlDocument xmlConfig)
        {
            var alBuffer = new ArrayList();
            char[] caDelimiter;

            _CheckDiposed();

            if (xmlConfig == null)
                throw new NullReferenceException("Parameter xmlConfig cannot be null when calling Load in clsGenericParser");

            if (xmlConfig.DocumentElement == null)
                throw new NullReferenceException("Parameter xmlConfig hsa a null DocumentElement; error calling Load in clsGenericParser");

            ////////////////////////////////////////////////////////////////////
            // Access each element and load the contents of the configuration //
            // into the current GenericParser object.                         //
            ////////////////////////////////////////////////////////////////////

            var xmlElement = xmlConfig.DocumentElement[XML_COLUMN_WIDTHS];

            if (xmlElement != null && xmlElement.ChildNodes.Count > 0)
            {
                foreach (XmlElement xmlColumnWidth in xmlElement.ChildNodes)
                    if (xmlColumnWidth.Name == XML_COLUMN_WIDTH)
                        alBuffer.Add(Convert.ToInt32(xmlColumnWidth.InnerText));

                if (alBuffer.Count > 0)
                    ColumnWidths = (int[])alBuffer.ToArray(typeof(int));

                alBuffer.Clear();
            }

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_MAX_BUFFER_SIZE];

            if (xmlElement?.InnerText != null)
                MaxBufferSize = Convert.ToInt32(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_MAX_ROWS];

            if (xmlElement?.InnerText != null)
                MaxRows = Convert.ToInt32(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_SKIP_DATA_ROWS];

            if (xmlElement?.InnerText != null)
                SkipDataRows = Convert.ToInt32(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_EXPECTED_COLUMN_COUNT];

            if (xmlElement?.InnerText != null)
                ExpectedColumnCount = Convert.ToInt32(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_FIRST_ROW_HAS_HEADER];

            if (xmlElement?.InnerText != null)
                FirstRowHasHeader = Convert.ToBoolean(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_TRIM_RESULTS];

            if (xmlElement?.InnerText != null)
                TrimResults = Convert.ToBoolean(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_FIXED_WIDTH];

            if (xmlElement?.InnerText != null)
                FixedWidth = Convert.ToBoolean(xmlElement.InnerText);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_ROW_DELIMITER];

            if (!string.IsNullOrEmpty(xmlElement?.InnerText))
            {
                caDelimiter = _ConvertFromSafeString(xmlElement.InnerText);

                if (caDelimiter != null)
                    RowDelimiter = caDelimiter;
            }

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_COLUMN_DELIMITER];

            if (!string.IsNullOrEmpty(xmlElement?.InnerText))
            {
                caDelimiter = _ConvertFromSafeString(xmlElement.InnerText);

                if (caDelimiter != null)
                    ColumnDelimiter = caDelimiter;
            }

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_TEXT_QUALIFIER];

            if (!string.IsNullOrEmpty(xmlElement?.InnerText))
                TextQualifier = Convert.ToChar(Convert.ToInt32(xmlElement.InnerText));

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_ESCAPE_CHARACTER];

            if (!string.IsNullOrEmpty(xmlElement?.InnerText))
                EscapeCharacter = Convert.ToChar(Convert.ToInt32(xmlElement.InnerText));

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.DocumentElement[XML_COMMENT_CHARACTER];

            if (!string.IsNullOrEmpty(xmlElement?.InnerText))
                CommentCharacter = Convert.ToChar(Convert.ToInt32(xmlElement.InnerText));
        }

        #endregion Loading

        #region Saving

        /// <summary>
        /// Saves the configuration of the <see cref="clsGenericParser"/> object to a <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="xwXmlConfig">The XmlWriter to save the the <see cref="XmlDocument"/> to.</param>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     XmlTextWriter xwXmlConfig = new XmlTextWriter(@"C:\MyData.txt", Encoding.Default);
        ///     GenericParser p = new GenericParser();
        ///
        ///     p.Save(xwXmlConfig);
        ///   </code>
        /// </example>
        public void Save(XmlWriter xwXmlConfig)
        {
            var xmlConfig = Save();

            xmlConfig.Save(xwXmlConfig);
        }

        /// <summary>
        /// Saves the configuration of the <see cref="clsGenericParser"/> object to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="twXmlConfig">The TextWriter to save the <see cref="XmlDocument"/> to.</param>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     StringWriter sw = new StringWriter();
        ///     GenericParser p = new GenericParser();
        ///
        ///     p.Save(sw);
        ///   </code>
        /// </example>
        public void Save(TextWriter twXmlConfig)
        {
            var xmlConfig = Save();

            xmlConfig.Save(twXmlConfig);
        }

        /// <summary>
        /// Saves the configuration of the <see cref="clsGenericParser"/> object to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="sXmlConfig">The stream to save the <see cref="XmlDocument"/> to.</param>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     FileStream fs = new FileStream(@"C:\MyData.txt", FileMode.Create);
        ///
        ///     GenericParser p = new GenericParser();
        ///     p.Save(fs);
        ///   </code>
        /// </example>
        public void Save(Stream sXmlConfig)
        {
            var xmlConfig = Save();

            xmlConfig.Save(sXmlConfig);
        }

        /// <summary>
        /// Saves the configuration of the <see cref="clsGenericParser"/> object to a the file system.
        /// </summary>
        /// <param name="strConfigXmlFile">The file name to save the XmlConfig file.</param>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParser();
        ///     p.Load(@"C:\MyData.txt");
        ///   </code>
        /// </example>
        public void Save(string strConfigXmlFile)
        {
            var xmlConfig = Save();

            xmlConfig.Save(strConfigXmlFile);
        }

        /// <summary>
        /// Saves the configuration of the <see cref="clsGenericParser"/> object to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <returns>The <see cref="XmlDocument"/> containing the configuration information of the
        /// <see cref="clsGenericParser"/> object.</returns>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParser();
        ///     XmlDocument xmlConfig = p.Save();
        ///   </code>
        /// </example>
        public virtual XmlDocument Save()
        {
            var xmlConfig = new XmlDocument();
            XmlElement xmlElement;

            _CheckDiposed();

            // Create the XML declaration
            var xmlDeclaration = xmlConfig.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            var xmlRoot = xmlConfig.CreateElement(XML_ROOT_NODE);
            xmlConfig.InsertBefore(xmlDeclaration, xmlConfig.DocumentElement);
            xmlConfig.AppendChild(xmlRoot);

            ////////////////////////////////////////////////////////////////////
            // Save each of the pertinent configurable settings of the        //
            // GenericParser object into the XmlDocument.                     //
            ////////////////////////////////////////////////////////////////////

            if (m_iaColumnWidths != null)
            {
                xmlElement = xmlConfig.CreateElement(XML_COLUMN_WIDTHS);
                xmlRoot.AppendChild(xmlElement);

                // Create the column width elements underneath the column widths node.
                foreach (var intColumnWidth in m_iaColumnWidths)
                {
                    var xmlSubElement = xmlConfig.CreateElement(XML_COLUMN_WIDTH);
                    xmlSubElement.InnerText = intColumnWidth.ToString();
                    xmlElement.AppendChild(xmlSubElement);
                }
            }

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_MAX_BUFFER_SIZE);
            xmlElement.InnerText = m_intMaxBufferSize.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_MAX_ROWS);
            xmlElement.InnerText = m_intMaxRows.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_SKIP_DATA_ROWS);
            xmlElement.InnerText = m_intSkipDataRows.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_EXPECTED_COLUMN_COUNT);
            xmlElement.InnerText = m_intExpectedColumnCount.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_FIRST_ROW_HAS_HEADER);
            xmlElement.InnerText = m_blnFirstRowHasHeader.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_TRIM_RESULTS);
            xmlElement.InnerText = m_blnTrimResults.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_FIXED_WIDTH);
            xmlElement.InnerText = m_blnFixedWidth.ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_ROW_DELIMITER);
            xmlElement.InnerText = _ConvertToSafeString(m_caRowDelimiter);
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_COLUMN_DELIMITER);
            xmlElement.InnerText = _ConvertToSafeString(m_caColumnDelimiter);
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_TEXT_QUALIFIER);
            xmlElement.InnerText = Convert.ToInt32(m_chTextQualifier).ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_ESCAPE_CHARACTER);
            xmlElement.InnerText = Convert.ToInt32(m_chEscapeCharacter).ToString();
            xmlRoot.AppendChild(xmlElement);

            /////////////////////////////////////////////////////////////

            xmlElement = xmlConfig.CreateElement(XML_COMMENT_CHARACTER);
            xmlElement.InnerText = Convert.ToInt32(m_chCommentCharacter).ToString();
            xmlRoot.AppendChild(xmlElement);

            return xmlConfig;
        }

        #endregion Saving

        #region Miscellaneous

        /// <summary>
        ///   Releases the underlying resources of the <see cref="clsGenericParser"/>.
        /// </summary>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParser();
        ///     p.SetDataSource(@"C:\MyData.txt");
        ///
        ///     while(p.Read())
        ///     {
        ///       // Put code here to retrieve results of the read.
        ///     }
        ///
        ///     p.Close();
        ///   </code>
        /// </example>
        public void Close()
        {
            _CleanUpParser(false);
        }

        /// <summary>
        ///   Returns the index of the Column based on its Name.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     <see langword="null"/> column name is not a valid name for a column.
        ///   </para>
        ///   <para>
        ///     If the column is not found, the column index will be -1.
        ///   </para>
        /// </remarks>
        /// <param name="strColumnName">The name of the column you're looking for.</param>
        /// <returns>The index of the column with the name strColumnName.
        /// If none exists, -1 will be returned.</returns>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     int intID, intPrice;
        ///     bool blnGotIndices = false;
        ///     GenericParser p = new GenericParser();
        ///     p.SetDataSource(@"C:\MyData.txt");
        ///     p.FirstRowHasHeader = true;
        ///
        ///     while(p.Read())
        ///     {
        ///       if (!blnGotIndices)
        ///       {
        ///         blnGotIndices = true;
        ///         intID = p.GetColumnIndex("ID");
        ///         intPrice = p.GetColumnIndex("Price");
        ///       }
        ///
        ///       // Put code here to retrieve results of the read.
        ///     }
        ///   </code>
        /// </example>
        public int GetColumnIndex(string strColumnName)
        {
            _CheckDiposed();

            return _GetColumnIndex(strColumnName);
        }

        /// <summary>
        ///   Returns the name of the Column based on its ColumnIndex.
        /// </summary>
        /// <param name="intColumnIndex">The column index to return the name for.</param>
        /// <remarks>
        ///   If the column is not found or the index is outside the range
        ///   of possible columns, <see langword="null"/> will be returned.
        /// </remarks>
        /// <returns>The name of the column at the given ColumnIndex, if
        /// none exists <see langword="null"/> is returned.</returns>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     string strColumn1, strColumn2;
        ///     bool blnGotColumnNames = false;
        ///     GenericParser p = new GenericParser();
        ///     p.SetDataSource(@"C:\MyData.txt");
        ///     p.FirstRowHasHeader = true;
        ///
        ///     while(p.Read())
        ///     {
        ///       if (!blnGotColumnNames)
        ///       {
        ///         blnGotColumnNames = true;
        ///         strColumn1 = p.GetColumnIndex(0);
        ///         strColumn2 = p.GetColumnIndex(1);
        ///       }
        ///
        ///       // Put code here to retrieve results of the read.
        ///     }
        ///   </code>
        /// </example>
        public string GetColumnName(int intColumnIndex)
        {
            _CheckDiposed();

            return _GetColumnName(intColumnIndex);
        }

        #endregion Miscellaneous

        #endregion Public Methods

        #region Private Members

        #region Configuration Data

        private int[] m_iaColumnWidths;
        private int m_intMaxBufferSize;
        private int m_intMaxRows;
        private int m_intSkipDataRows;
        private int m_intExpectedColumnCount;
        private bool m_blnFirstRowHasHeader;
        private bool m_blnTrimResults;
        private bool m_blnFixedWidth;
        private char[] m_caRowDelimiter;
        private char[] m_caColumnDelimiter;
        private char m_chTextQualifier;
        private char m_chEscapeCharacter;
        private char m_chCommentCharacter;

        #endregion Configuration Data

        #region Parsing Variables

        private TextReader m_txtReader;

        private bool m_blnHeaderRowFound;
        private bool m_blnInText;
        private bool m_blnEndOfRowFound;
        private bool m_blnEndOfColumnFound;
        private bool m_blnEscapeCharacterFound;

        private int m_intDataRowNumber;
        private int m_intFileRowNumber;

        private int m_intCurrentIndex;
        private int m_intCurrentColumnStartIndex;

        private char[] m_caBuffer;
        private char[] m_chEscapeBuffer;

        private int m_intStartIndexOfNewData;
        private int m_intCharactersRead;
        private int m_intCharactersInBuffer;

        private ParserState m_ParserState;
        private RowState m_ParserRowState;
        private BufferState m_BufferState;

        private StringCollection m_scData;
        private StringCollection m_scColumnNames;

        #endregion Parsing Variables

        private bool m_blnDisposed;

        #endregion Private Members

        #region Private Methods

        /// <summary>
        ///   Initializes internal variables that are maintained for internal tracking
        ///   of state during parsing.
        /// </summary>
        /// <exception cref="ParserSetupException">
        ///   In the event that the <see cref="clsGenericParser"/> wasn't setup properly, this exception will be thrown.
        /// </exception>
        private void _InitializeParse()
        {
            switch (m_ParserState)
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////

                case ParserState.NoDataSource:
                    throw new ParserSetupException("You must supply a datasource before parsing.");

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                case ParserState.Ready:

                    // Peform a quick sanity check if we're doing FixedWidth.
                    if (m_blnFixedWidth && (m_iaColumnWidths == null))
                        throw new ParserSetupException(
                            "You must supply the appropriate column widths before attempting to parse fixed width data.");

                    m_ParserState = ParserState.Parsing;
                    m_ParserRowState = RowState.GetRowType;
                    m_BufferState = BufferState.FetchData;

                    m_blnInText = false;
                    m_blnEndOfRowFound = false;
                    m_blnEndOfColumnFound = false;
                    m_blnEscapeCharacterFound = false;

                    m_blnHeaderRowFound = false;
                    m_intStartIndexOfNewData = 0;
                    m_intDataRowNumber = 0;
                    m_intFileRowNumber = 0;

                    if (m_scData == null)
                        m_scData = new StringCollection();
                    else
                        m_scData.Clear();

                    if (m_scColumnNames == null)
                        m_scColumnNames = new StringCollection();
                    else
                        m_scColumnNames.Clear();

                    // I only allocate the following if it is null or is improperly sized.
                    if ((m_caBuffer == null) || (m_caBuffer.Length != m_intMaxBufferSize))
                        m_caBuffer = new char[m_intMaxBufferSize];

                    // Allocate the escape buffer in case where we have characters to unescape.
                    if ((m_chEscapeCharacter != NULL_CHAR)
                        && ((m_chEscapeBuffer == null) || (m_chEscapeBuffer.Length != m_intMaxBufferSize)))
                        m_chEscapeBuffer = new char[m_intMaxBufferSize];

                    break;

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                case ParserState.Parsing:

                    m_scData.Clear();

                    // Have we hit the max row count?
                    if ((m_intMaxRows > 0) && ((m_intDataRowNumber - m_intSkipDataRows) >= m_intMaxRows))
                        m_ParserState = ParserState.Finished;
                    else
                    {
                        m_ParserRowState = RowState.GetRowType;

                        m_blnInText = false;
                        m_blnEndOfRowFound = false;
                        m_blnEndOfColumnFound = false;
                        m_blnEscapeCharacterFound = false;
                    }

                    break;

                /////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

        /// <summary>
        ///   Removes all references to internally allocated resources.  Depending on
        ///   <paramref name="blnCompletely"/>, it will free up all of the internal resources
        ///   to prepare the instance for disposing.
        /// </summary>
        /// <param name="blnCompletely">
        ///   <para>
        ///     <see langword="true"/> - Clean-up the entire parser (used for disposing the instance).
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Clean-up the parser to all it to be reused later.
        ///   </para>
        /// </param>
        private void _CleanUpParser(bool blnCompletely)
        {
            m_ParserState = ParserState.Finished;

            m_txtReader?.Close();

            m_txtReader = null;
            m_caBuffer = null;
            m_chEscapeBuffer = null;
            m_scData = null;

            if (blnCompletely)
            {
                m_caColumnDelimiter = null;
                m_caRowDelimiter = null;
                m_iaColumnWidths = null;
                m_scColumnNames = null;
            }
        }

        /// <summary>
        ///   Examines the beginning of the row and the current state information
        ///   to determine how the parser will interpret the next line and updates
        ///   the internal RowState accordingly.
        /// </summary>
        private void _ParseRowType()
        {
            // Increment our file row counter to help with debugging in case of an error in syntax.
            ++m_intFileRowNumber;

            // Because we're at a new row, we need initialize the StartColumnIndex.
            m_intCurrentColumnStartIndex = m_intCurrentIndex;
            m_blnInText = false;
            m_blnEndOfRowFound = false;
            m_blnEndOfColumnFound = false;
            m_blnEscapeCharacterFound = false;

            // We need to verify if this is a comment row or not.
            if (m_caBuffer[m_intCurrentIndex] == m_chCommentCharacter)
                m_ParserRowState = RowState.CommentRow;
            else if (m_blnFirstRowHasHeader && !m_blnHeaderRowFound)
                m_ParserRowState = RowState.HeaderRow;
            else if (m_intDataRowNumber < m_intSkipDataRows)
                m_ParserRowState = RowState.SkippedRow;
            else
                m_ParserRowState = RowState.DataRow;
        }

        /// <summary>
        ///   Reads in the next block of data from the source into the internal buffer.
        ///   Also, it determines whether data still exists in the buffer that needs to
        ///   be parsed.
        /// </summary>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - Data exists that needs to be parsed.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - No data left.
        ///   </para>
        /// </returns>
        private bool _ReadDataIntoBuffer()
        {
            // Do we need to read in more data?
            if (m_BufferState == BufferState.FetchData)
            {
                m_intCharactersRead = m_txtReader.Read(m_caBuffer, m_intStartIndexOfNewData,
                                                       (m_intMaxBufferSize - m_intStartIndexOfNewData));
                m_intCharactersInBuffer = m_intCharactersRead + m_intStartIndexOfNewData;

                // If we didn't read in all that we could, then we're at the end of the data.
                if (m_intCharactersInBuffer < m_intMaxBufferSize)
                {
                    m_BufferState = BufferState.NoFetchableData;

                    // Since we don't have any more data to be read, close the reader off.
                    m_txtReader.Close();
                }
                else
                    m_BufferState = BufferState.NoAction;

                // Since we read in data, we need to reset the current index and start of a column back to the zero indice.
                m_intCurrentIndex = 0;
                m_intCurrentColumnStartIndex = 0;
                m_blnInText = false;
                m_blnEscapeCharacterFound = false;
            }

            // If we read in characters or old characters already exist in the buffer,
            // then we've got data left that needs to be parsed.
            return ((m_intCharactersRead > 0) || (m_intStartIndexOfNewData > 0));
        }

        /// <summary>
        ///   Takes the data parsed from the row and places it into the ColumnNames collection.
        /// </summary>
        private void _SetColumnNames()
        {
            // Move the strings in the data array into the string collection
            // because they are the header names.
            foreach (var columnName in m_scData)
                m_scColumnNames.Add(columnName);

            m_blnHeaderRowFound = true;

            // Clear out the data (the header names) from our data collection.
            m_scData.Clear();
        }

        /// <summary>
        ///   Determines if the current row has ended or not.
        /// </summary>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - End of the row.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Not the end of the row.
        ///   </para>
        /// </returns>
        private bool _IsEndOfRow()
        {
            if (m_BufferState == BufferState.FetchData)
                return false;

            return _MatchesDelimiter(m_caRowDelimiter);
        }

        /// <summary>
        ///   Determines if the current column has ended or not.
        /// </summary>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - End of the column.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - Not the end of the column.
        ///   </para>
        /// </returns>
        /// <exception cref="ParsingException">
        ///   If parsing a fixed width format and the number of columns found differs
        ///   what was expected, this exception will be thrown.
        /// </exception>
        private bool _IsEndOfColumn()
        {
            if (m_BufferState == BufferState.FetchData)
                return false;

            // Determine the end of the column based on the type of data being read.
            if (m_blnFixedWidth)
            {
                if (m_scData.Count >= m_iaColumnWidths.Length)
                    throw new ParsingException(
                        string.Format("Number of columns ({0}) differs from the expected column count ({1}).",
                                      m_scData.Count + 1,
                                      m_intExpectedColumnCount),
                        m_intFileRowNumber);

                return (m_intCurrentIndex >= (m_intCurrentColumnStartIndex + m_iaColumnWidths[m_scData.Count]));
            }

            return _MatchesDelimiter(m_caColumnDelimiter);
        }

        /// <summary>
        ///   Determines if the indice of the end of the row indicates an empty row.
        /// </summary>
        /// <param name="intEndOfRowIndex">The indice of the row delimiter marking the end of the row.</param>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - The row is an empty row.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - The row is not an empty row.
        ///   </para>
        /// </returns>
        private bool _IsEmptyRow(int intEndOfRowIndex)
        {
            return (!m_blnEndOfColumnFound && (intEndOfRowIndex <= m_intCurrentColumnStartIndex));
        }

        /// <summary>
        ///   Takes a range within the character buffer and extracts the desired
        ///   string from within it and places it into the DataArray.  If an escape
        ///   character has been set, the escape characters are stripped out and the
        ///   unescaped string is returned.
        /// </summary>
        /// <param name="intEndOfDataIndex">The index of the last character in the column.</param>
        /// <exception cref="ParsingException">
        ///   In the event that the <see cref="ExpectedColumnCount"/> is set to a value of greater
        ///   than zero (which is by default for a fixed width format) and the number of columns
        ///   found differs from what's expected, this exception will be thrown.
        /// </exception>
        private void _ExtractColumn(int intEndOfDataIndex)
        {
            string strUnescaped;
            var blnTrimResults = m_blnTrimResults;

            // Make sure we haven't exceeded our expected column count.
            if ((m_intExpectedColumnCount > 0) && (m_scData.Count > m_intExpectedColumnCount))
                throw new ParsingException(string.Format("Current column ({0}) exceeds ExpectedColumnCount of {1}.",
                                                         m_scData.Count + 1,
                                                         m_intExpectedColumnCount),
                                           m_intFileRowNumber);

            var intStartOfDataIndex = m_intCurrentColumnStartIndex;

            // Strip off any text qualifiers, if they are present.
            if ((m_chTextQualifier != NULL_CHAR)
                && (m_caBuffer[intStartOfDataIndex] == m_chTextQualifier)
                && (m_caBuffer[intEndOfDataIndex] == m_chTextQualifier))
            {
                blnTrimResults = false;
                ++intStartOfDataIndex;
                --intEndOfDataIndex;
            }

            // No escape character was set, so we do not need to look for it.
            if (m_chEscapeCharacter == NULL_CHAR)
                strUnescaped = _GenerateStringFromArray(m_caBuffer, intStartOfDataIndex, intEndOfDataIndex,
                                                        blnTrimResults);
            else
            {
                // An escape character was set, so we need to perform a check to make sure there
                // are no escape characters within the current block of data.
                int intIndex, intEscapeBufferIndex = -1;

                // Iterate over every character from the starting index to the end index (includes the end).
                for (intIndex = intStartOfDataIndex; intIndex <= intEndOfDataIndex; ++intIndex)
                {
                    if (m_caBuffer[intIndex] == m_chEscapeCharacter)
                    {
                        // Increment the current index, so that we skip over the escaped character.
                        // Though, make sure we don't go beyond the intEndIndex.
                        if (++intIndex > intEndOfDataIndex)
                            break;
                    }

                    // Insert the following character into the chUnescaped buffer, if we've found
                    // an escaped character yet.  Make sure though we're still in the proper range
                    // between the start/end indices.
                    m_chEscapeBuffer[++intEscapeBufferIndex] = m_caBuffer[intIndex];
                }

                // Return the unescaped characters from the m_chEscapeBuffer
                strUnescaped = _GenerateStringFromArray(m_chEscapeBuffer, 0, intEscapeBufferIndex, blnTrimResults);
            }

            // Only trim on non-textqualified strings.  Add the results to the string collection of data.
            m_scData.Add(strUnescaped);
        }

        /// <summary>
        ///   Generates a string from the character array passed in.  It performs the operations
        ///   of trimming the string in the character array, so that it doesn't have to allocate
        ///   a string and then reallocate a new 'trimmed' string.
        /// </summary>
        /// <param name="caArray">The character array containing our string to be extracted.</param>
        /// <param name="intStartOfDataIndex">The starting indice of the string to be extracted.</param>
        /// <param name="intEndOfDataIndex">The last character in the string to be extracted.</param>
        /// <param name="blnTrimResults">Whether or not the string needs to be trimmed.</param>
        /// <returns>The string that was generated from the character array between the two given indices.</returns>
        private string _GenerateStringFromArray(char[] caArray, int intStartOfDataIndex, int intEndOfDataIndex,
                                                bool blnTrimResults)
        {
            if (blnTrimResults)
            {
                // Move up the beginning indice if we have white-space.
                while ((intStartOfDataIndex <= intEndOfDataIndex) && char.IsWhiteSpace(caArray[intStartOfDataIndex]))
                    ++intStartOfDataIndex;

                // Move up the ending indice if we have white-space.
                while ((intStartOfDataIndex <= intEndOfDataIndex) && char.IsWhiteSpace(caArray[intEndOfDataIndex]))
                    --intEndOfDataIndex;
            }

            return new string(caArray, intStartOfDataIndex, intEndOfDataIndex - intStartOfDataIndex + 1);
        }

        /// <summary>
        ///   When the buffer has reached the end of its parsing and there are no more
        ///   complete columns to be parsed, the remaining data must be moved up to the
        ///   front of the buffer so that the next batch of data can be appended to
        ///   the end.
        /// </summary>
        /// <param name="intStartIndex">The index that starts the beginning of the data to be moved.</param>
        /// <exception cref="ParsingException">In the event that the entire buffer is full and a single
        /// column cannot be parsed from it, parsing can no longer continue.</exception>
        private void _CopyRemainingDataToFront(int intStartIndex)
        {
            int intIndex;

            if (intStartIndex == 0)
                throw new ParsingException(string.Format("Current column ({0}) exceeds MaxBufferSize of {1}.",
                                                         m_scData.Count,
                                                         m_intMaxBufferSize),
                                           m_intFileRowNumber);

            // Shift the value from the end of the buffer to the beginning.
            for (intIndex = 0; intStartIndex < m_intMaxBufferSize; ++intIndex, ++intStartIndex)
                m_caBuffer[intIndex] = m_caBuffer[intStartIndex];

            // Set the next position to begin placing data.
            m_intStartIndexOfNewData = intIndex;
        }

        /// <summary>
        ///   Determines if the sequence of characters starting at m_intCurrentIndex
        ///   matches the delimiter supplied.
        /// </summary>
        /// <param name="caDelimiter">The delimiter to compare against.</param>
        /// <returns>
        ///   <para>
        ///     <see langword="true"/> - The character sequence is a match.
        ///   </para>
        ///   <para>
        ///     <see langword="false"/> - The character sequence is not a match.
        ///   </para>
        /// </returns>
        private bool _MatchesDelimiter(IList<char> caDelimiter)
        {
            // Just check the first character to perform a quick check.
            if (m_caBuffer[m_intCurrentIndex] != caDelimiter[0])
                return false;

            // See if we could even match it based on available characters left in the buffer.
            if (m_intCurrentIndex + caDelimiter.Count > m_intCharactersInBuffer)
            {
                // Indicate we need to grab more data.
                if (m_BufferState == BufferState.NoAction)
                    m_BufferState = BufferState.FetchData;

                return false;
            }

            // Increment the intBufferIndex because we already checked the first character.
            var intBufferIndex = m_intCurrentIndex + 1;

            for (var intCheckIndex = 1; intCheckIndex < caDelimiter.Count; ++intCheckIndex, ++intBufferIndex)
            {
                if (m_caBuffer[intBufferIndex] != caDelimiter[intCheckIndex])
                    return false;
            }

            // No differences were found, so it was a match.
            return true;
        }

        /// <summary>
        ///   Used in the process of saving off GenericParser information into the
        ///   Xml. Due to the fact that some characters may be stripped when placed
        ///   into Xml, I've created this function to convert each character
        ///   down to its integer value and place them in a CSV format for storage
        ///   as a string.
        /// </summary>
        /// <param name="caArray">The char[] needing to be converted into
        ///  an Xml friendly string of CSV integers.</param>
        /// <returns>A string representation of the integer values of each of the
        /// characters in the character array.</returns>
        private string _ConvertToSafeString(char[] caArray)
        {
            var strResult = string.Empty;
            var blnFirstValue = true;

            if (caArray != null)
            {
                foreach (var c in caArray)
                {
                    strResult += string.Format("{0}{1}",
                                               (blnFirstValue) ? string.Empty : XML_SAFE_STRING_DELIMITER,
                                               Convert.ToInt32(c).ToString());

                    blnFirstValue = false;
                }
            }

            return strResult;
        }

        /// <summary>
        ///   Take a CSV array of integers and converts them back to an array of
        ///   characters.
        /// </summary>
        /// <param name="strSafeString">The CSV array of integers stored as a string.</param>
        /// <returns>The char[] of the decoded string.</returns>
        private char[] _ConvertFromSafeString(string strSafeString)
        {
            if (!string.IsNullOrEmpty(strSafeString))
            {
                var alChars = new ArrayList();
                var saValues = strSafeString.Split(XML_SAFE_STRING_DELIMITER.ToCharArray());

                foreach (var s in saValues)
                    alChars.Add(Convert.ToChar(Convert.ToInt32(s)));

                return ((char[])alChars.ToArray(typeof(char)));
            }

            return null;
        }

        /// <summary>
        ///   Returns the index of the Column based on its Name.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     <see langword="null"/> column name is not a valid name for a column.
        ///   </para>
        ///   <para>
        ///     If the column is not found, the column index will be -1.
        ///   </para>
        /// </remarks>
        /// <param name="strColumnName">The name of the column you're looking for.</param>
        /// <returns>The index of the column with the name strColumnName.
        /// If none exists, -1 will be returned.</returns>
        private int _GetColumnIndex(string strColumnName)
        {
            if (m_blnHeaderRowFound && (strColumnName != null))
            {
                for (var intColumnIndex = 0; intColumnIndex < m_scColumnNames.Count; ++intColumnIndex)
                {
                    if (m_scColumnNames[intColumnIndex] == strColumnName)
                        return intColumnIndex;
                }
            }

            return -1;
        }

        /// <summary>
        ///   Returns the name of the Column based on its ColumnIndex.
        /// </summary>
        /// <param name="intColumnIndex">The column index to return the name for.</param>
        /// <remarks>
        ///   If the column is not found or the index is outside the range
        ///   of possible columns, <see langword="null"/> will be returned.
        /// </remarks>
        /// <returns>The name of the column at the given ColumnIndex, if
        /// none exists <see langword="null"/> is returned.</returns>
        public string _GetColumnName(int intColumnIndex)
        {
            if (m_blnHeaderRowFound && (intColumnIndex > -1) && (intColumnIndex < m_scColumnNames.Count))
                return m_scColumnNames[intColumnIndex];

            return null;
        }

        #endregion Private Methods

        #region Events

        /// <summary>
        /// Occurs when this instance is diposed of.
        /// </summary>
        public event EventHandler Disposed;

        #endregion Events

        #region IDisposable Members

        /// <summary>
        ///   Releases all of the underlying resources used by this instance.
        /// </summary>
        /// <remarks>
        ///   Calls <see cref="_Dispose"/> with <paramref name="blnDisposing"/> set to <see langword="true"/>
        ///   to free unmanaged and managed resources.
        /// </remarks>
        public void Dispose()
        {
            _Dispose(true);

            // This object will be cleaned up by the Dispose method.
            //
            // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue
            // and prevent finalization code for this object from executing a second time.

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Verifies that the instance has not already been disposed of.  If it has,
        ///   then it throws an <see cref="System.ObjectDisposedException"/>.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">The instance has been disposed of.</exception>
        /// <remarks>
        ///   Any derived classes should call this method when executing code that should not be called
        ///   if the object has been disposed of.
        /// </remarks>
        protected void _CheckDiposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }

        /// <summary>
        /// Raises the <see cref="Disposed"/> Event.
        /// </summary>
        /// <param name="e">The event data about the OnDisposed Event being fired.</param>
        protected virtual void OnDisposed(EventArgs e)
        {
            Disposed?.Invoke(this, e);
        }

        /// <summary>
        ///   Releases the all unmanaged resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="blnDisposing">
        /// 	<see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected virtual void _Dispose(bool blnDisposing)
        {
            lock (this)
            {
                if (!IsDisposed)
                {
                    try
                    {
                        if (blnDisposing)
                        {
                            _CleanUpParser(true);
                        }
                    }
                    finally
                    {
                        m_blnDisposed = true;
                    }
                }
            }

            try
            {
                OnDisposed(EventArgs.Empty);
            }
            catch
            {
                /* Do nothing */
            }
        }

        /// <summary>
        ///   The finalizer for the GenericParser class to release all the underlying
        ///   resources.
        /// </summary>
        ~clsGenericParser()
        {
            _Dispose(false);
        }

        #endregion
    }

    /// <summary>
    ///   The <see cref="GenericParserAdapter"/> is used to modify the <see cref="clsGenericParser"/>
    ///   to allow it parse a file and place them into various formats.
    /// </summary>
    /// <threadsafety static="false" instance="false"/>
    public class clsGenericParserAdapter : clsGenericParser
    {
        #region Constants

        #region XmlConfig Constants

        private const string XML_INCLUDE_LINE_NUMBER = "IncludeFileLineNumber";

        #endregion XmlConfig Constants

        #region FileInfo Constants for DataSet

        private const string FILE_INFO_LINE = "FileLineNumber";

        #endregion FileInfo Constants for DataSet

        #endregion Constants

        #region Constructors

        /// <summary>
        ///   Constructs an instance of a <see cref="GenericParserAdapter"/>
        ///   with the default settings.
        /// </summary>
        /// <remarks>
        ///   If you use this constructor, you must set the datasource
        ///   prior to using the parser (using <see cref="GenericParser.SetDataSource"/>), otherwise an
        ///   exception will be thrown.
        /// </remarks>
        public clsGenericParserAdapter()
        {
            IncludeFileLineNumber = false;
        }

        /// <summary>
        ///   Constructs an instance of a <see cref="GenericParserAdapter"/> and sets
        ///   the initial datasource as the file referenced by the string passed in.
        /// </summary>
        /// <param name="strFileName">The file name to set as the initial datasource.</param>
        public clsGenericParserAdapter(string strFileName) : this()
        {
            SetDataSource(strFileName);
        }

        /// <summary>
        ///   Constructs an instance of a <see cref="GenericParserAdapter"/> and sets
        ///   the initial datasource as the <see cref="TextReader"/> passed in.
        /// </summary>
        /// <param name="txtReader">
        ///   The <see cref="TextReader"/> containing the data to be parsed.
        /// </param>
        public clsGenericParserAdapter(TextReader txtReader) : this()
        {
            SetDataSource(txtReader);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///   <para>
        ///     Indicates whether or not the <see cref="GenericParser.FileRowNumber"/> from where
        ///     the data was retrieved should be included as part of the result set.
        ///   </para>
        ///   <para>
        ///     Default: <see langword="false"/>
        ///   </para>
        /// </summary>
        public bool IncludeFileLineNumber
        {
            get
            {
                _CheckDiposed();

                return m_blnIncludeFileLineNumber;
            }
            set
            {
                _CheckDiposed();

                if (State == ParserState.Parsing)
                    return;

                m_blnIncludeFileLineNumber = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///   Generates an <see cref="XmlDocument"/> based on the data stored within
        ///   the entire data source after it was parsed.
        /// </summary>
        /// <returns>
        ///   The <see cref="XmlDocument"/> containing all of the data in the data
        ///   source.
        /// </returns>
        /// <exception cref="ParserSetupException">
        ///   Attempting to read without properly setting up the
        ///   <see cref="GenericParserAdapter"/>.
        /// </exception>
        /// <exception cref="ParsingException">
        ///   Thrown in the situations where the <see cref="GenericParserAdapter"/>
        ///   cannot continue due to a conflict between the setup and the data being
        ///   parsed.
        /// </exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParserAdapter(@"C:\MyData.txt");
        ///     XmlDocument xmlDoc = p.GetXml();
        ///     p.Close();
        ///   </code>
        /// </example>
        public XmlDocument GetXml()
        {
            XmlDocument xmlDocument = null;

            var dsData = GetDataSet();

            if (dsData != null)
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(dsData.GetXml());
            }

            return xmlDocument;
        }

        /// <summary>
        ///   Generates a <see cref="DataSet"/> based on the data stored within
        ///   the entire data source after it was parsed.
        /// </summary>
        /// <returns>
        ///   The <see cref="DataSet"/> containing all of the data in the
        ///   data source.
        /// </returns>
        /// <exception cref="ParserSetupException">
        ///   Attempting to read without properly setting up the
        ///   <see cref="GenericParserAdapter"/>.
        /// </exception>
        /// <exception cref="ParsingException">
        ///   Thrown in the situations where the <see cref="GenericParserAdapter"/>
        ///   cannot continue due to a conflict between the setup and the data being
        ///   parsed.
        /// </exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParserAdapter(@"C:\MyData.txt");
        ///     DataSet dsResults = p.GetDataSet();
        ///     p.Close();
        ///   </code>
        /// </example>
        public DataSet GetDataSet()
        {
            DataSet dsData = null;

            var dtData = GetDataTable();

            if (dtData != null)
            {
                dsData = new DataSet();
                dsData.Tables.Add(dtData);
            }

            return dsData;
        }

        /// <summary>
        ///   Generates a <see cref="DataTable"/> based on the data stored within
        ///   the entire data source after it was parsed.
        /// </summary>
        /// <returns>
        ///   The <see cref="DataTable"/> containing all of the data in the data
        ///   source.
        /// </returns>
        /// <exception cref="ParserSetupException">
        ///   Attempting to read without properly setting up the
        ///   <see cref="GenericParserAdapter"/>.
        /// </exception>
        /// <exception cref="ParsingException">
        ///   Thrown in the situations where the <see cref="GenericParserAdapter"/>
        ///   cannot continue due to a conflict between the setup and the data being
        ///   parsed.
        /// </exception>
        /// <example>
        ///   <code lang="C#" escaped="true">
        ///     GenericParser p = new GenericParserAdapter(@"C:\MyData.txt");
        ///     DataTable dtResults = p.GetDataTable();
        ///     p.Close();
        ///   </code>
        /// </example>
        public DataTable GetDataTable()
        {
            _CheckDiposed();

            var dtData = new DataTable();
            var intFoundColumns = 0;

            dtData.BeginLoadData();

            while (Read())
            {
                var intCurrentColumnCount = ColumnCount;

                // See if we have the appropriate number of columns.
                int intColumnIndex;
                if (intCurrentColumnCount > intFoundColumns)
                {
                    // Add in our column to store off the file line number.
                    if (IncludeFileLineNumber && (intFoundColumns < 1))
                        dtData.Columns.Add(FILE_INFO_LINE);

                    for (intColumnIndex = intFoundColumns; intColumnIndex < intCurrentColumnCount; ++intColumnIndex)
                    {
                        _AddColumnToTable(dtData, GetColumnName(intColumnIndex));
                        ++intFoundColumns;
                    }
                }

                var drRow = dtData.NewRow();

                if (IncludeFileLineNumber)
                    drRow[0] = FileRowNumber;

                // Now, add in the data retrieved from the current row.
                for (intColumnIndex = 0; intColumnIndex < intCurrentColumnCount; ++intColumnIndex)
                {
                    if (IncludeFileLineNumber)
                        drRow[intColumnIndex + 1] = this[intColumnIndex];
                    else
                        drRow[intColumnIndex] = this[intColumnIndex];
                }

                dtData.Rows.Add(drRow);
            }

            dtData.EndLoadData();

            return dtData;
        }

        #endregion Public Methods

        #region Private Members

        private bool m_blnIncludeFileLineNumber;

        #endregion Private Members

        #region Private Methods

        /// <summary>
        ///   Adds a column name to the given <see cref="DataTable"/>, such that
        ///   it ensures a unique column name.
        /// </summary>
        /// <param name="dtData">The <see cref="DataTable"/> to add the column to.</param>
        /// <param name="strColumnName">The desired column name to add.</param>
        private void _AddColumnToTable(DataTable dtData, string strColumnName)
        {
            if (strColumnName != null)
            {
                if (dtData.Columns[strColumnName] == null)
                    dtData.Columns.Add(strColumnName);
                else
                {
                    string strNewColumnName;
                    var intCount = 0;

                    // Looks like we need to generate a new column name.
                    do
                    {
                        strNewColumnName = string.Format("{0}{1}", strColumnName, ++intCount);
                    } while (dtData.Columns[strNewColumnName] != null);

                    dtData.Columns.Add(strNewColumnName);
                }
            }
            else
                dtData.Columns.Add();
        }

        #endregion Private Methods

        #region Overridden Methods

        /// <summary>
        ///   Loads the base <see cref="clsGenericParser"/> class from the
        ///   <see cref="XmlDocument"/> and then retrieves additional information
        ///    from the Xml that is specific to the <see cref="GenericParserAdapter"/>.
        /// </summary>
        /// <param name="xmlConfig">
        ///   The <see cref="XmlDocument"/> containing the configuration information.
        /// </param>
        public override void Load(XmlDocument xmlConfig)
        {
            // Load the base information for the GenericParser.
            base.Load(xmlConfig);

            /////////////////////////////////////////////
            // Load the rest of the information that's //
            // specific to the GenericParserAdapter.   //
            /////////////////////////////////////////////

            var xmlElement = xmlConfig.DocumentElement?[XML_INCLUDE_LINE_NUMBER];

            if (xmlElement?.InnerText != null)
                IncludeFileLineNumber = Convert.ToBoolean(xmlElement.InnerText);
        }

        /// <summary>
        ///   Saves the configuration of the <see cref="GenericParserAdapter"/>
        ///   to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <returns>
        ///   The <see cref="XmlDocument"/> that will store the configuration
        ///   information of the current setup of the <see cref="GenericParserAdapter"/>.
        /// </returns>
        public override XmlDocument Save()
        {
            var xmlConfig = base.Save();

            if (xmlConfig == null)
                throw new NullReferenceException("base.Save returned a null xmlConfig instance; cannot save the configuration in clsGenericParser");

            if (xmlConfig.DocumentElement == null)
                throw new NullReferenceException("xmlConfig.DocumentElement is null; cannot save the configuration in clsGenericParser");


            ///////////////////////////////////////////////////////////////
            // Take the document and insert the additional configuration //
            // specific to the GenericParserAdapter.                     //
            ///////////////////////////////////////////////////////////////

            var xmlElement = xmlConfig.CreateElement(XML_INCLUDE_LINE_NUMBER);
            xmlElement.InnerText = IncludeFileLineNumber.ToString();
            xmlConfig.DocumentElement?.AppendChild(xmlElement);

            return xmlConfig;
        }

        #endregion Overridden Methods
    }

    #endregion GenericParsing Classes

    #region GenericParser Exceptions

    /// <summary>
    ///   <see cref="ParserSetupException"/> is an exception class meant
    ///   for states where the parser has been improperly setup when attempting
    ///   to parse data.
    /// </summary>
    public class ParserSetupException : ApplicationException
    {
        #region Constructors

        /// <summary>
        ///   Creates a new <see cref="ParserSetupException"/> with default
        ///   values.
        /// </summary>
        public ParserSetupException()
        {
        }

        /// <summary>
        ///   Creates a new <see cref="ParserSetupException"/> with the
        ///   given string as the message.
        /// </summary>
        /// <param name="strMessage">
        ///   The message indicating the root cause of the error.
        /// </param>
        public ParserSetupException(string strMessage) : base(strMessage)
        {
        }

        #endregion Constructors
    }

    /// <summary>
    ///   <see cref="ParsingException"/> is an exception class meant for states where
    ///   the parser can no longer continue parsing due to the data found in the
    ///   data-source.
    /// </summary>
    public class ParsingException : ApplicationException
    {
        #region Constants

        private const string SERIALIZATION_FILE_ROW_NUMBER = "FileRowNumber";

        #endregion Constants

        #region Constructors

        /// <summary>
        ///   Creates a new <see cref="ParsingException"/> with default values.
        /// </summary>
        public ParsingException()
        {
        }

        /// <summary>
        ///   Creates a new <see cref="ParsingException"/> containing a message and the
        ///   file line number that the error occured.
        /// </summary>
        /// <param name="strMessage">
        ///   The message indicating the root cause of the error.
        /// </param>
        /// <param name="intFileRowNumber">The file line number the error occured on.</param>
        public ParsingException(string strMessage, int intFileRowNumber)
            : base(strMessage)
        {
            m_intFileRowNumber = intFileRowNumber;
        }

        /// <summary>
        ///   Creates a new <see cref="ParsingException"/> with seralized data.
        /// </summary>
        /// <param name="sInfo">
        ///   The <see cref="SerializationInfo"/> that contains information
        ///   about the exception.
        /// </param>
        /// <param name="sContext">
        ///   The <see cref="StreamingContext"/> that contains information
        ///   about the source/destination of the exception.
        /// </param>
        public ParsingException(SerializationInfo sInfo, StreamingContext sContext)
            : base(sInfo, sContext)
        {
            m_intFileRowNumber = sInfo.GetInt32(SERIALIZATION_FILE_ROW_NUMBER);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        ///   The line number in the file that the exception was thrown at.
        /// </summary>
        public int FileRowNumber => m_intFileRowNumber;

        #endregion Public Properties

        #region Private Members

        private readonly int m_intFileRowNumber;

        #endregion Private Members

        #region Overridden Methods

        /// <summary>
        ///   When overridden in a derived class, sets the <see cref="SerializationInfo"/>
        ///   with information about the exception.
        /// </summary>
        /// <param name="sInfo">
        ///   The <see cref="SerializationInfo"/> that holds the serialized object data
        ///   about the exception being thrown.
        /// </param>
        /// <param name="sContext">
        ///   The <see cref="StreamingContext"/> that contains contextual information about the source
        ///   or destination.
        /// </param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo sInfo, StreamingContext sContext)
        {
            base.GetObjectData(sInfo, sContext);

            sInfo.AddValue(SERIALIZATION_FILE_ROW_NUMBER, m_intFileRowNumber);
        }

        #endregion Overridden Methods
    }

    #endregion GenericParser Exceptions
}