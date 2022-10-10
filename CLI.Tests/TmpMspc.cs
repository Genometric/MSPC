// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.CLI.Interfaces;
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

        public string Run(string parserFilename, string rep1=null, string rep2=null, double tauW=1e-4, double tauS=1e-8)
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

            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(args.ToString().Trim().Split(' '));
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            return output;
        }

        public string Run(bool createSample = true, string template = null, string sessionPath = null, bool appendOutputOption = true)
        {
            if (sessionPath != null)
                SessionPath = sessionPath;
            else
                SessionPath =
                    "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                    new Random().Next(100000, 999999).ToString();

            if (createSample)
                CreateTempSamples();

            if (template == null)
                template = string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", TmpSamples[0], TmpSamples[1]);
            if (appendOutputOption)
                template += string.Format(" -o {0}", SessionPath);

            string output;
            using (var swOut = new StringWriter())
            using (var swErr = new StringWriter())
            {
                Console.SetOut(swOut);
                Console.SetError(swErr);
                Program.Main(template.Split(' '));
                output = swErr.ToString() + swOut.ToString();

                var standardOutput = new StreamWriter(Console.OpenStandardOutput())
                {
                    AutoFlush = true
                };
                Console.SetOut(standardOutput);

                var standardError = new StreamWriter(Console.OpenStandardError())
                {
                    AutoFlush = true
                };
                Console.SetError(standardError);
            }

           // Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
           // Console.SetError(new StreamWriter(Console.OpenStandardError()));
            


            return output;
        }

        // Do not make this static because multiple tests
        // running concurrently will have the same/combined output. 
        public string FailRun(string template1 = null, string template2 = null)
        {
            using var swOut = new StringWriter();
            using var swErr = new StringWriter();
            using var o = new Orchestrator();
            Console.SetOut(swOut);
            Console.SetError(swErr);

            Environment.ExitCode = o.Invoke(
                (template1 ?? $"-i rep1.bed -i rep2.bed -r bio -s 1e-8 -w 1e-4").Split(' '));

            Environment.ExitCode = o.Invoke(
                (template2 ?? "-r bio -s 1e-8 -w 1e-4").Split(' '));

            return swErr.ToString() + swOut.ToString();
        }

        public string Run(IExporter<Peak> exporter)
        {
            SessionPath =
                "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                new Random().Next(100000, 999999).ToString();

            var template = string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", TmpSamples[0], TmpSamples[1]);

            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var o = new Orchestrator(exporter);
                o.Invoke(template.Split(' '));
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            return output;
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
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

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
