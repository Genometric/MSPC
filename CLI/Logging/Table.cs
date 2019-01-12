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
        private static ILog log = LogManager.GetLogger("mspc", "log");

        public Table(int[] columnsWidth)
        {
            _columnsWidth = columnsWidth;
        }

        public void AddHeader(params string[] headers)
        {
            var headerLines = new string[_columnsWidth.Length];
            for (int i = 0; i < headerLines.Length; i++)
                headerLines[i] = new string('-', _columnsWidth[i]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(RenderRow(headers));
            Console.WriteLine(RenderRow(headerLines));
            Console.ResetColor();
        }

        public void AddRow(params string[] columns)
        {
            string row = RenderRow(columns);
            Console.WriteLine(row);
            log.Info(row);
        }

        private string RenderRow(params string[] columns)
        {
            var row = new StringBuilder();
            for (int i = 0; i < columns.Length; i++)
                if (columns[i].Length < _columnsWidth[i])
                    row.Append(columns[i].PadLeft(_columnsWidth[i]) + "\t");
                else
                    row.Append(TruncateString(columns[i], _columnsWidth[i]) + "\t");
            return row.ToString();
        }

        private string TruncateString(string value, int maxLength)
        {
            return
                value.Length <= maxLength ?
                new string(' ', maxLength - value.Length) + value :
                "..." + value.Substring(value.Length - maxLength - 3, maxLength);
        }
    }
}
