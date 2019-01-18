// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using log4net;
using System;
using System.Text;

namespace Genometric.MSPC.CLI.Logging
{
    internal class Table
    {
        private readonly int[] _columnsWidth;
        private readonly ILog log;

        public Table(int[] columnsWidth, string repository, string name)
        {
            _columnsWidth = columnsWidth;
            log = LogManager.GetLogger(repository, name);
        }

        public void AddHeader(params string[] headers)
        {
            var headerLines = new string[_columnsWidth.Length];
            for (int i = 0; i < headerLines.Length; i++)
                headerLines[i] = new string('-', _columnsWidth[i]);
            Console.WriteLine(RenderRow(headers));
            Console.WriteLine(RenderRow(headerLines));
        }

        public void AddRow(params string[] columns)
        {
            string row = RenderRow(columns);
            Console.WriteLine(row);
            log.Info(row);
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
            return ((int)(Math.Floor(Math.Log10(rowCount)) + Math.Floor(Math.Floor(Math.Log10(rowCount)) / 3) + 1) * 2) + 2;
        }

        public static string IdxColFormat(int rowNumber, int totalRows)
        {
            /// NOTE:
            /// Any changes to the following format, should also 
            /// be reflect in the IdxColChars method.
            return string.Format("{0}/{1}", rowNumber.ToString("N0"), totalRows.ToString("N0"));
        }

        private string RenderRow(params string[] columns)
        {
            var row = new StringBuilder();
            for (int i = 0; i < columns.Length; i++)
                row.Append(TruncateString(columns[i], _columnsWidth[i]) + "\t");
            return row.ToString();
        }

        private string TruncateString(string value, int maxLength)
        {
            if (value.Length <= maxLength)
                return value.PadLeft(maxLength);
            else
                return "..." + value.Substring(value.Length - maxLength - 3, maxLength);
        }
    }
}
