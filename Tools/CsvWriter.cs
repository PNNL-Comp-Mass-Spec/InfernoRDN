using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DAnTE.Tools
{
    /// <summary>
    /// Summary description for CsvWriter.
    /// </summary>
    public static class CsvWriter
    {
        public static string WriteToString(DataTable table, bool header, bool quoteAll, bool tab)
        {
            var writer = new StringWriter();
            WriteToStream(writer, table, header, quoteAll, tab);
            return writer.ToString();
        }

        public static void WriteToStream1(TextWriter stream, DataTable table, bool header, bool quoteAll)
        {
            if (header)
            {
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteAll);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write(Environment.NewLine);
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteAll);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write(Environment.NewLine);
                }
            }
        }

        public static void WriteToStream(TextWriter stream, DataTable table, bool includeHeaderRow, bool quoteAll,
                                         bool tabDelimited)
        {
            if (includeHeaderRow)
            {
                WriteHeaderRow(stream, table, quoteAll, tabDelimited);
            }
            foreach (DataRow row in table.Rows)
            {
                WriteRow(stream, table.Columns.Count, row, quoteAll, tabDelimited);
            }
        }

        private static void WriteDelimiterOrLineFeed(TextWriter stream, int i, int columnCount, bool tabDelimited)
        {
            if (i < columnCount - 1)
            {
                if (tabDelimited)
                    stream.Write('\t');
                else
                    stream.Write(',');
            }
            else
            {
                stream.Write(Environment.NewLine);
            }
        }

        public static void WriteHeaderRow(TextWriter stream, DataTable table, bool quoteAll, bool tabDelimited)
        {
            var columnCount = table.Columns.Count;
            var rowData = new List<string>(columnCount);

            for (var i = 0; i < columnCount; i++)
            {
                rowData.Add(table.Columns[i].Caption);
            }

            WriteRow(stream, rowData, quoteAll, tabDelimited);
        }

        public static void WriteRow(TextWriter stream, int columnCount, DataRow row, bool quoteAll, bool tabDelimited)
        {
            var rowData = new List<string>(columnCount);

            for (var i = 0; i < columnCount; i++)
            {
                var itemText = string.Empty;

                if (row[i] != null)
                {
                    itemText = row[i].ToString();
                }

                rowData.Add(itemText);
            }

            WriteRow(stream, rowData, quoteAll, tabDelimited);
        }

        public static void WriteRow(TextWriter stream, List<string> rowData, bool quoteAll, bool tabDelimited)
        {
            for (var i = 0; i < rowData.Count; i++)
            {
                if (tabDelimited && !quoteAll)
                {
                    if (!string.IsNullOrWhiteSpace(rowData[i]))
                        stream.Write(rowData[i]);
                }
                else
                {
                    WriteItem(stream, rowData[i], quoteAll);
                }

                WriteDelimiterOrLineFeed(stream, i, rowData.Count, tabDelimited);
            }
        }

        public static void WriteListViewToStream(TextWriter stream, System.Windows.Forms.ListView lstView,
                                                 string mstrHeader, bool quoteAll)
        {
            if (mstrHeader.Length > 0)
            {
                stream.Write("DataFile:" + mstrHeader);
                stream.Write(Environment.NewLine);
            }
            var CurrTime = DateTime.Now;
            stream.Write("Time:" +
                         CurrTime.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture("en-us")));
            stream.Write(Environment.NewLine);
            stream.Write(Environment.NewLine);

            foreach (System.Windows.Forms.ListViewGroup lgrp in lstView.Groups)
            {
                WriteItem(stream, lgrp.Header, quoteAll);
                stream.Write(Environment.NewLine);
                for (var i = 0; i < lgrp.Header.Length; i++)
                    stream.Write("-");
                stream.Write(Environment.NewLine);
                foreach (System.Windows.Forms.ListViewItem litem in lgrp.Items)
                {
                    for (var i = 0; i < lstView.Columns.Count; i++)
                    {
                        WriteItem(stream, litem.SubItems[i].Text, quoteAll);
                        if (i < lstView.Columns.Count - 1)
                        {
                            stream.Write(':');
                        }
                        else
                            stream.Write(Environment.NewLine);
                    }
                }
                stream.Write(Environment.NewLine);
            }
        }

        private static void WriteItem(TextWriter stream, object item, bool quoteAll)
        {
            if (item == null)
                return;

            WriteItem(stream, item.ToString(), quoteAll);
        }

        public static void WriteItem(TextWriter stream, string itemText, bool quoteAll)
        {
            if (string.IsNullOrWhiteSpace(itemText))
                return;

            if (quoteAll || itemText.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + itemText.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(itemText);
        }
    }
}