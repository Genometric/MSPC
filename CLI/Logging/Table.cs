using System;
using System.Text;

namespace Genometric.MSPC.CLI.Logging
{
    internal class Table
    {
        private readonly int[] _columnsWidth;

        public Table(int[] columnsWidth)
        {
            _columnsWidth = columnsWidth;
        }

        public void AddHeader(
            out string renderedHeaders,
            out string renderedHeaderLines,
            params string[] headers)
        {
            var headerLines = new string[_columnsWidth.Length];
            for (int i = 0; i < headerLines.Length; i++)
                headerLines[i] = new string('-', _columnsWidth[i]);
            renderedHeaders = RenderRow(true, headers);
            renderedHeaderLines = RenderRow(true, headerLines);
        }

        public string GetRow(bool truncate = true, params string[] columns)
        {
            return RenderRow(truncate, columns);
        }

        /// <summary>
        /// Computes the number of characters required to render index
        /// column of a table without truncating the characters.
        /// 
        /// For instance, for 12345 possible rows, it computes 
        /// how many chars would require to fit "  12,345/12,345".
        /// </summary>
        /// <param name="rowCount">Sets the maximum number of rows 
        /// (excluding header) that will be rendered.</param>
        /// <returns></returns>
        public static int IdxColChars(int rowCount)
        {
            return (
                (int)(
                    Math.Floor(Math.Log10(rowCount)) +
                    Math.Floor(Math.Floor(Math.Log10(rowCount)) / 3) +
                    1) * 2) + 2;
        }

        public static string IdxColFormat(int rowNumber, int totalRows)
        {
            /// NOTE:
            /// Any changes to the following format, should also 
            /// be reflect in the IdxColChars method.
            return string.Format(
                "{0}/{1}",
                rowNumber.ToString("N0"),
                totalRows.ToString("N0"));
        }

        private string RenderRow(bool truncate = true, params string[] columns)
        {
            var row = new StringBuilder();
            if (truncate)
                for (int i = 0; i < columns.Length; i++)
                    row.Append(TruncateString(columns[i], _columnsWidth[i]) + "\t");
            else
                for (int i = 0; i < columns.Length; i++)
                    row.Append(columns[i] + "\t");
            return row.ToString();
        }

        private static string TruncateString(string value, int maxLength)
        {
            if (value.Length <= maxLength)
                return value.PadLeft(maxLength);
            else
                return "..." + value[^(maxLength - 3)..];
        }
    }
}
