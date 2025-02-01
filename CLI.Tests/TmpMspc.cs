using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.CLI.CommandLineInterface;
using Genometric.MSPC.CLI.Interfaces;
using Genometric.MSPC.CLI.Tests.MockTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Genometric.MSPC.CLI.Tests
{
    // TODO: THIS MOCK CLASS SHOULD BE GENERALIZED TO SUPPORT MSPC ACCESS NEEDS
    // OF ALL THE CLI TESTS, HENCE SIMPLIFIE THOSE TESTS IMPLEMENTATION.

    public class TmpMspc : IDisposable
    {
        private List<string> _tmpSamples = null;
        public List<string> TmpSamples
        {
            get
            {
                if (_tmpSamples == null)
                    CreateTempSamples();
                return _tmpSamples;
            }
        }

        public string SessionPath { private set; get; }

        private static int SetExitCode_REMOVE_ME_AFTER_THE_BUG_SYSTEM_COMMANDLINE_IS_FIXED(
            int exitCode, int envExitCode)
        {
            return envExitCode != 0 ? envExitCode : exitCode;
        }

        public Result Run(
            string parserFilename,
            string rep1 = null,
            string rep2 = null,
            double tauW = 1e-4,
            double tauS = 1e-8)
        {
            string SessionPath =
                   "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                   new Random().Next(100000, 999999).ToString();

            var args = new StringBuilder();
            if (string.IsNullOrEmpty(rep1))
                rep1 = AddTempSample();
            args.Append(string.Format("-i {0} ", rep1));
            if (string.IsNullOrEmpty(rep2))
                rep2 = AddTempSample();
            args.Append(string.Format("-i {0} ", rep2));

            args.Append(string.Format("-w {0} ", tauW));
            args.Append(string.Format("-s {0} ", tauS));
            args.Append("-r bio ");

            if (parserFilename != null)
                args.Append(string.Format("-p {0} ", parserFilename));

            args.Append(string.Format("-o {0} ", SessionPath));

            int exitCode;
            string output;
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(args.ToString().Trim().Split(' '));
                exitCode = Environment.ExitCode;
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            exitCode = SetExitCode_REMOVE_ME_AFTER_THE_BUG_SYSTEM_COMMANDLINE_IS_FIXED(
                exitCode, Environment.ExitCode);

            return new Result(exitCode, output);
        }

        public Result Run(
            bool createSample = true,
            string template = null,
            string sessionPath = null,
            bool appendOutputOption = true,
            bool stranded = false)
        {
            if (createSample)
            {
                if (!stranded)
                    CreateTempSamples();
                else
                    CreateTempSamplesStranded();
            }

            if (template == null)
            {
                template = $"-i {TmpSamples[0]} -i {TmpSamples[1]} -r bio -w 1E-2 -s 1E-8";

                if (stranded)
                {
                    var parserConfigFilename = Main.GetFilename("parserConfig");
                    using (var writer = new StreamWriter(parserConfigFilename))
                        writer.WriteLine("{\"Strand\":5}");
                    template += $" -p {parserConfigFilename}";
                }
            }
            if (appendOutputOption)
            {
                if (sessionPath != null)
                    SessionPath = sessionPath;
                else
                    SessionPath =
                        "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                        new Random().Next(100000, 999999).ToString();

                template += string.Format(" -o {0}", SessionPath);
            }

            string output;
            int exitCode;
            var log = new List<string>();
            var console = new MockConsole();
            CliConfig config;

            string logFilename;
            using (var orchestrator = new Orchestrator(console))
            {
                exitCode = orchestrator.Invoke(template.Split(' '));
                output = console.GetStderr() + console.GetStdo();
                logFilename = orchestrator.LogFile;
                config = orchestrator.Config;
            }

            string line;
            if (logFilename != null)
            {
                using (var reader = new StreamReader(logFilename))
                {
                    while ((line = reader.ReadLine()) != null)
                        log.Add(line);
                }
            }

            return new Result(exitCode, output, log.AsReadOnly(), config);
        }

        // Do not make this static because multiple tests
        // running concurrently will have the same/combined output. 
        public Result FailRun(string template = null, string? template2 = null)
        {
            var _console = new MockConsole();
            using var o = new Orchestrator(_console);

            var exitCode = o.Invoke(
                (template ?? "-r bio -s 1e-8 -w 1e-4").Split(' '));

            if (template2 is not null)
                o.Invoke(template2.Split(' '));

            exitCode = SetExitCode_REMOVE_ME_AFTER_THE_BUG_SYSTEM_COMMANDLINE_IS_FIXED(
                exitCode, Environment.ExitCode);

            return new Result(exitCode, _console.GetStderr() + _console.GetStdo());
        }

        public Result Run(IExporter<Peak> exporter)
        {
            SessionPath =
                "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                new Random().Next(100000, 999999).ToString();

            var template = string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", TmpSamples[0], TmpSamples[1]);

            string output;
            var console = new MockConsole();
            var o = new Orchestrator(exporter, console);
            var exitCode = o.Invoke(template.Split(' '));
            exitCode = SetExitCode_REMOVE_ME_AFTER_THE_BUG_SYSTEM_COMMANDLINE_IS_FIXED(
                exitCode, Environment.ExitCode);

            return new Result(exitCode, console.GetStdo());
        }

        public string AddTempSample(int featureCount = 1)
        {
            string filename = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            FileStream stream = File.Create(filename);
            var rnd = new Random();
            using (var writter = new StreamWriter(stream))
                while (featureCount-- > 0)
                    writter.WriteLine(
                        string.Format(
                            "chr{0}\t{1}\t{2}\tmspc_peak_{3}\t{4}",
                            rnd.Next(),
                            rnd.Next(100, 1000),
                            rnd.Next(1001, 10000),
                            rnd.Next(),
                            rnd.NextDouble()));

            if (_tmpSamples == null)
                _tmpSamples = new List<string>();
            _tmpSamples.Add(filename);
            return filename;
        }

        private void CreateTempSamples()
        {
            string rep1Path = Path.Join(Environment.CurrentDirectory, Main.GetFilename("rep1"));
            string rep2Path = Path.Join(Environment.CurrentDirectory, Main.GetFilename("rep2"));

            _tmpSamples = new List<string> { rep1Path, rep2Path };

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t10\t20\tmspc_peak_1\t3");
                writter.WriteLine("chr1\t25\t35\tmspc_peak_1\t5");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t11\t18\tmspc_peak_2\t2");
                writter.WriteLine("chr1\t22\t28\tmspc_peak_2\t3");
                writter.WriteLine("chr1\t30\t40\tmspc_peak_2\t7");
            }
        }

        private void CreateTempSamplesStranded()
        {
            string rep1Path = Path.Join(Environment.CurrentDirectory, Main.GetFilename("rep1"));
            string rep2Path = Path.Join(Environment.CurrentDirectory, Main.GetFilename("rep2"));

            _tmpSamples = new List<string> { rep1Path, rep2Path };

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t10\t20\tmspc_peak_01\t3\t+");
                writter.WriteLine("chr1\t25\t35\tmspc_peak_02\t5\t+");
                writter.WriteLine("chr1\t25\t45\tmspc_peak_03\t5\t-");
                writter.WriteLine("chr1\t65\t90\tmspc_peak_04\t8\t-");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new(stream))
            {
                writter.WriteLine("chr1\t11\t18\tmspc_peak_05\t2\t+");
                writter.WriteLine("chr1\t22\t28\tmspc_peak_06\t3\t+");
                writter.WriteLine("chr1\t30\t40\tmspc_peak_07\t7\t+");
                writter.WriteLine("chr1\t50\t52\tmspc_peak_07\t8\t+");
                writter.WriteLine("chr1\t08\t55\tmspc_peak_08\t9\t-");
                writter.WriteLine("chr1\t60\t70\tmspc_peak_09\t2\t-");
                writter.WriteLine("chr1\t72\t80\tmspc_peak_10\t7\t-");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (TmpSamples != null)
                foreach (var sample in TmpSamples)
                    File.Delete(sample);
            if (Directory.Exists(SessionPath))
                Directory.Delete(SessionPath, true);
        }
    }
}
