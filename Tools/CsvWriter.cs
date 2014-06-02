using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;

namespace DAnTE.Tools
{
	/// <summary>
	/// Summary description for CsvWriter.
	/// </summary>
	public class CsvWriter
	{
        public static string WriteToString(DataTable table, bool header, bool quoteall, bool tab)
		{
			StringWriter writer = new StringWriter();
			WriteToStream(writer, table, header, quoteall, tab);
			return writer.ToString();
		}

		public static void WriteToStream1(TextWriter stream, DataTable table, bool header, bool quoteall)
		{
			if (header)
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					WriteItem(stream, table.Columns[i].Caption, quoteall);
					if (i < table.Columns.Count - 1)
						stream.Write(',');
					else
						stream.Write("\r\n");
				}
			}
			foreach (DataRow row in table.Rows)
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					WriteItem(stream, row[i], quoteall);
					if (i < table.Columns.Count - 1)
						stream.Write(',');
					else
						stream.Write("\r\n");
				}
			}
		}

        public static void WriteToStream(TextWriter stream, DataTable table, bool header, 
            bool quoteall, bool tab)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                    {
                        if (tab)
                            stream.Write('\t');
                        else
                            stream.Write(',');
                    }
                    else
                    {
                        stream.Write("\r\n");
                    }
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                    {
                        if (tab)
                            stream.Write('\t');
                        else
                            stream.Write(',');
                    }
                    else
                        stream.Write("\r\n");
                }
            }
        }

        public static void WriteListViewToStream(TextWriter stream,
            System.Windows.Forms.ListView lstView, string mstrHeader, bool quoteall)
        {
            if (mstrHeader.Length > 0)
            {
                stream.Write("DataFile:" + mstrHeader);
                stream.Write("\r\n");
            }
            DateTime CurrTime = DateTime.Now;
            stream.Write("Time:" + 
                CurrTime.ToString("G", System.Globalization.CultureInfo.CreateSpecificCulture("en-us")));
            stream.Write("\r\n");
            stream.Write("\r\n");

            foreach (System.Windows.Forms.ListViewGroup lgrp in lstView.Groups)
            {
                WriteItem(stream, lgrp.Header, quoteall);
                stream.Write("\r\n");
                for (int i = 0; i < lgrp.Header.Length; i++)
                    stream.Write("-");
                stream.Write("\r\n");
                foreach (System.Windows.Forms.ListViewItem litem in lgrp.Items)
                {
                    for (int i = 0; i < lstView.Columns.Count; i++)
                    {
                        WriteItem(stream, litem.SubItems[i].Text, quoteall);
                        if (i < lstView.Columns.Count - 1)
                        {
                            stream.Write(':');
                        }
                        else
                            stream.Write("\r\n");
                    }
                }
                stream.Write("\r\n");
            }
        }

		private static void WriteItem(TextWriter stream, object item, bool quoteall)
		{
			if (item == null)
				return;
			string s = item.ToString();
			if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
				stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
			else
				stream.Write(s);
		}
	}
}
