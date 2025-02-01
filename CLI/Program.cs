using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Genometric.MSPC.CLI.Tests")]
namespace Genometric.MSPC.CLI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            using var orchestrator = new Orchestrator();
            Environment.ExitCode = orchestrator.Invoke(args);
        }
    }
}
