using System;
using System.CommandLine;

namespace Genometric.MSPC.CLI.ConsoleAbstraction
{
    public interface IConsoleExtended : IConsole
    {
        void SetForegroundColor(ConsoleColor color);
        void ResetColor();
    }
}
