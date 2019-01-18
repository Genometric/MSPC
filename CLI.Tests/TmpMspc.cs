// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using Genometric.MSPC.CLI.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Genometric.MSPC.CLI.Tests
{
    public class TmpMspc : IDisposable
    {
        private List<string> _samples;

        public string SessionPath { private set; get; }

        public string Run(bool createSample = true, string template = null, string sessionPath = null)
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
                template = string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", _samples[0], _samples[1]);
            template += string.Format(" -o {0}", SessionPath);

            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Program.Main(template.Split(' '));
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            return output;
        }

        public List<string> FailRun(string template1 = null, string template2 = null)
        {
            string logFile;
            using (var o = new Orchestrator())
            {
                o.Orchestrate((template1 ?? "-i rep1 -i rep2 -r bio -s 1e-8 -w 1e-4").Split(' '));
                o.Orchestrate((template2 ?? "-r bio -s 1e-8 -w 1e-4").Split(' '));
                logFile = o.LogFile;
            }

            var messages = new List<string>();
            string line;
            using (var reader = new StreamReader(logFile))
                while ((line = reader.ReadLine()) != null)
                    messages.Add(line);
            return messages;
        }

        public string Run(IExporter<Peak> exporter)
        {
            SessionPath =
                "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff_", CultureInfo.InvariantCulture) +
                new Random().Next(100000, 999999).ToString();

            CreateTempSamples();
            var template = string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8", _samples[0], _samples[1]);

            string output;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var o = new Orchestrator(exporter);
                o.Orchestrate(template.Split(' '));
                output = sw.ToString();
            }

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            return output;
        }

        private void CreateTempSamples()
        {
            string rep1Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";
            string rep2Path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".bed";

            _samples = new List<string> { rep1Path, rep2Path };

            FileStream stream = File.Create(rep1Path);
            using (StreamWriter writter = new StreamWriter(stream))
            {
                writter.WriteLine("chr1\t10\t20\tmspc_peak_1\t3");
                writter.WriteLine("chr1\t25\t35\tmspc_peak_1\t5");
            }

            stream = File.Create(rep2Path);
            using (StreamWriter writter = new StreamWriter(stream))
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
            if (_samples != null)
                foreach (var sample in _samples)
                    File.Delete(sample);
            if (Directory.Exists(SessionPath))
                Directory.Delete(SessionPath, true);
        }
    }
}
