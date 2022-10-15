using Genometric.MSPC.CLI.ConsoleAbstraction;
using System;
using System.CommandLine.IO;
using System.IO;

namespace Genometric.MSPC.CLI.Tests.MockTypes
{
    // This class is mainly implemented based on the
    // System.CommandLine.IO.SystemConsole type. 
    internal class MockConsole : IConsoleExtended
    {
        private readonly StringWriter _stdout = new();
        private readonly StringWriter _stderr = new();

        public MockConsole()
        {
            Out = new StreamWriter(_stdout);
            Error = new StreamWriter(_stderr);
        }

        public string GetStdo()
        {
            return _stdout.ToString();
        }
        public string GetStderr()
        {
            return _stderr.ToString();
        }

        public IStandardStreamWriter Out {get;}

        public bool IsOutputRedirected => false;

        public IStandardStreamWriter Error { get; }

        public bool IsErrorRedirected => false;

        public bool IsInputRedirected => false;

        public void ResetColor()
        {
            // No need to change/rest the color on the mocked console.
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            // No need to change/rest the color on the mocked console.
        }

        private class StreamWriter : IStandardStreamWriter
        {
            private readonly StringWriter _writer;

            public StreamWriter(StringWriter writer) => _writer = writer;

            public void Write(string value) => _writer.Write(value);
        }
    }
}
