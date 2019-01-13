// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Genometric.MSPC.CLI.Tests
{
    public class TmpMspc : IDisposable
    {
        private string _sessionPath;
        private List<string> _samples;

        public string Run(string template = null)
        {
            _sessionPath = "session_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff", CultureInfo.InvariantCulture);
            CreateTempSamples();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                if (template != null)
                    Program.Main(template.Split(' '));
                else
                    Program.Main(string.Format("-i {0} -i {1} -r bio -w 1E-2 -s 1E-8 -o {2}", _samples[0], _samples[1], _sessionPath).Split(' '));
                return sw.ToString();
            }
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
            foreach (var sample in _samples)
                File.Delete(sample);
            if (Directory.Exists(_sessionPath))
                Directory.Delete(_sessionPath, true);
        }
    }
}
