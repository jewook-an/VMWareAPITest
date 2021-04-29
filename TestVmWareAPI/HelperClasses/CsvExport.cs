using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class CsvExport {
		/// <summary>
		/// To keep the ordered list of column names
		/// </summary>
		private readonly List<string> _fields = new List<string>();

		/// <summary>
		/// The list of rows
		/// </summary>
		private readonly List<Dictionary<string, object>> _rows = new List<Dictionary<string, object>>();

		/// <summary>
		/// The current row
		/// </summary>
		private Dictionary<string, object> _currentRow => this._rows[this._rows.Count - 1];

		/// <summary>
		/// The string used to separate columns in the output
		/// </summary>
		private readonly string _columnSeparator;

		/// <summary>
		/// Whether to include the preamble that declares which column separator is used in the output
		/// </summary>
		private readonly bool _includeColumnSeparatorDefinitionPreamble;

		/// <summary>
		/// Initializes a new instance of the <see cref="Jitbit.Utils.CsvExport"/> class.
		/// </summary>
		/// <param name="columnSeparator">
		/// The string used to separate columns in the output.
		/// By default this is a comma so that the generated output is a CSV file.
		/// </param>
		/// <param name="includeColumnSeparatorDefinitionPreamble">
		/// Whether to include the preamble that declares which column separator is used in the output.
		/// By default this is <c>true</c> so that Excel can open the generated CSV
		/// without asking the user to specify the delimiter used in the file.
		/// </param>
		public CsvExport(string columnSeparator = ",", bool includeColumnSeparatorDefinitionPreamble = true) {
			this._columnSeparator = columnSeparator;
			this._includeColumnSeparatorDefinitionPreamble = includeColumnSeparatorDefinitionPreamble;
		}

		/// <summary>
		/// Set a value on this column
		/// </summary>
		public object this[string field] {
			set {
				// Keep track of the field names, because the dictionary loses the ordering
				if (!this._fields.Contains(field))
					this._fields.Add(field);
				this._currentRow[field] = value;
			}
		}

		/// <summary>
		/// Call this before setting any fields on a row
		/// </summary>
		public void AddRow() => this._rows.Add(new Dictionary<string, object>());

		/// <summary>
		/// Add a list of typed objects, maps object properties to CsvFields
		/// </summary>
		public void AddRows<T>(IEnumerable<T> list) {
			if (list.Any()) {
				foreach (T obj in list) {
					this.AddRow();
					System.Reflection.PropertyInfo[] values = obj.GetType().GetProperties();
					foreach (System.Reflection.PropertyInfo value in values) {
						this[value.Name] = value.GetValue(obj, null);
					}
				}
			}
		}

		/// <summary>
		/// Converts a value to how it should output in a csv file
		/// If it has a comma, it needs surrounding with double quotes
		/// Eg Sydney, Australia -> "Sydney, Australia"
		/// Also if it contains any double quotes ("), then they need to be replaced with quad quotes[sic] ("")
		/// Eg "Dangerous Dan" McGrew -> """Dangerous Dan"" McGrew"
		/// </summary>
		/// <param name="columnSeparator">
		/// The string used to separate columns in the output.
		/// By default this is a comma so that the generated output is a CSV document.
		/// </param>
		public static string MakeValueCsvFriendly(object value, string columnSeparator = ",") {
			if (value == null)
				return "";

			if (value is INullable && ((INullable)value).IsNull)
				return "";

			if (value is DateTime) {
				if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
					return ((DateTime)value).ToString("yyyy-MM-dd");
				return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
			}

			string output = value.ToString().Trim();

			if (output.Length > 30000) //cropping value for stupid Excel
				output = output.Substring(0, 30000);

			if (output.Contains(columnSeparator) || output.Contains("\"") || output.Contains("\n") || output.Contains("\r"))
				output = '"' + output.Replace("\"", "\"\"") + '"';

			return output;
		}

		/// <summary>
		/// Outputs all rows as a CSV, returning one string at a time
		/// </summary>
		private IEnumerable<string> ExportToLines(bool includeHeader = false) {
			if (this._includeColumnSeparatorDefinitionPreamble)
				yield return "sep=" + this._columnSeparator;

			// The header
			if (includeHeader)
				yield return string.Join(this._columnSeparator, this._fields.Select(f => MakeValueCsvFriendly(f, this._columnSeparator)));

			// The rows
			foreach (Dictionary<string, object> row in this._rows) {
				foreach (string k in this._fields.Where(f => !row.ContainsKey(f))) {
					row[k] = null;
				}
				yield return string.Join(this._columnSeparator, this._fields.Select(field => MakeValueCsvFriendly(row[field], this._columnSeparator)));
			}
		}

		/// <summary>
		/// Output all rows as a CSV returning a string
		/// </summary>
		public string Export(bool includeHeader = false) {
			StringBuilder sb = new StringBuilder();

			foreach (string line in this.ExportToLines(includeHeader)) {
				sb.AppendLine(line);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Exports to a file
		/// </summary>
		public void ExportToFile(string path, bool includeHeader = false) => File.WriteAllLines(path, this.ExportToLines(includeHeader), Encoding.GetEncoding("ks_c_5601-1987"));

		/// <summary>
		/// Exports as raw UTF8 bytes
		/// </summary>
		public byte[] ExportToBytes(bool includeHeader = false) {
			byte[] data = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(this.Export(includeHeader));
			return Encoding.GetEncoding("ks_c_5601-1987").GetPreamble().Concat(data).ToArray();
		}
	}
}
