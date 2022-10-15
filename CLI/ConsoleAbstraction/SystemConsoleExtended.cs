using System;
using System.CommandLine.IO;

namespace Genometric.MSPC.CLI.ConsoleAbstraction
{
    public class SystemConsoleExtended : SystemConsole, IConsoleExtended
    {
        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
