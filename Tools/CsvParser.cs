using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;

namespace DAnTE.Tools
{
	public class CsvParser
	{
		public static DataTable Parse(string data, bool headers)
		{
			return Parse(new StringReader(data), headers);
		}
               
		public static DataTable Parse(string data)
		{
			return Parse(new StringReader(data));
		}

		public static DataTable Parse(TextReader stream)
		{
			return Parse(stream, false);
		}

		public static DataTable Parse(TextReader stream, bool headers)
		{
			var table = new DataTable();
			var csv = new CsvStream(stream);
			var row = csv.GetNextRow();
			if (row == null)
				return null;

			if (headers)
			{
				foreach (var header in row)
				{
					if (!string.IsNullOrEmpty(header) && !table.Columns.Contains(header))
						table.Columns.Add(header, typeof(string));
					else
						table.Columns.Add(GetNextColumnHeader(table), typeof(string));
				}
				row = csv.GetNextRow();
			}

			while (row != null)
			{
				while (row.Count > table.Columns.Count)
					table.Columns.Add(GetNextColumnHeader(table), typeof(string));

				table.Rows.Add(row);
				row = csv.GetNextRow();
			}
			return table;
		}

		private static string GetNextColumnHeader(DataTable table)
		{
			var c = 1;
			while (true)
			{
				var h = "Column" + c++;
				if (!table.Columns.Contains(h))
					return h;
			}
		}

		private class CsvStream
		{
			private readonly TextReader stream;                 
                       
			public CsvStream(TextReader s)
			{
				stream = s;
			}

            public List<string> GetNextRow()
			{
				var row = new List<string>();
				while (true)
				{
					var item = GetNextItem();
					if (item == null)
						return row.Count == 0 ? null : row;

					row.Add(item);
				}
			}

			private bool EOS;
			private bool EOL;

			private string GetNextItem()
			{
				if (EOL)
				{
					// previous item was last in line, start new line
					EOL = false;
					return null;
				}

				var quoted = false;
				var predata = true;
				var postdata = false;
				var item = new StringBuilder();
                               
				while (true)
				{
					var c = GetNextChar(true);
					if (EOS)
						return item.Length > 0 ? item.ToString() : null;

					if ((postdata || !quoted) && c == ',')
						// end of item, return
						return item.ToString();
                                       
					if ((predata || postdata || !quoted) && (c == '\x0A' || c == '\x0D'))
					{
						// we are at the end of the line, eat newline characters and exit
						EOL = true;
						if (c == '\x0D' && GetNextChar(false) == '\x0A')
							// new line sequence is 0D0A
							GetNextChar(true);
						return item.ToString();
					}

					if (predata && c == ' ')
						// whitespace preceeding data, discard
						continue;

					if (predata && c == '"')
					{
						// quoted data is starting
						quoted = true;
						predata = false;
						continue;
					}

					if (predata)
					{
						// data is starting without quotes
						predata = false;
						item.Append(c);
						continue;
					}

					if (c == '"' && quoted)
					{
						if (GetNextChar(false) == '"')
							// double quotes within quoted string means add a quote       
							item.Append(GetNextChar(true));
						else
							// end-quote reached
							postdata = true;
						continue;
					}

					// all cases covered, character must be data
					item.Append(c);
				}
			}

			private readonly char[] buffer = new char[4096];
			private int pos;
			private int length;

			private char GetNextChar(bool eat)
			{
				if (pos >= length)
				{
					length = stream.ReadBlock(buffer, 0, buffer.Length);
					if (length == 0)
					{
						EOS = true;
						return '\0';
					}
					pos = 0;
				}
				if (eat)
					return buffer[pos++];
				else
					return buffer[pos];
			}
		}
	}
}
