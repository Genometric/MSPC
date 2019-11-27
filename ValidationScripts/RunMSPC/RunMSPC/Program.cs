using System.Collections.Generic;
using System.IO;

namespace RunMSPC
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchPath = @"C:\Users\Vahid\Desktop\MSPCDataWithEncodePeaks\MSPCDataWithEncodePeaks";
            List<double[]> thresholds = new List<double[]>
            {
                new double[] { 1E-01, 1E-02, 1E-02 },
                new double[] { 1E-01, 1E-03, 1E-02 },
                new double[] { 1E-01, 1E-03, 1E-03 },
                new double[] { 1E-01, 1E-04, 1E-03 },
                new double[] { 1E-01, 1E-04, 1E-04 },
                new double[] { 1E-02, 1E-04, 1E-04 },
                new double[] { 1E-02, 1E-05, 1E-02 },
                new double[] { 1E-02, 1E-05, 1E-03 },
                new double[] { 1E-02, 1E-05, 1E-04 },
                new double[] { 1E-03, 1E-05, 1E-05 },
                new double[] { 1E-03, 1E-06, 1E-05 },
                new double[] { 1E-03, 1E-06, 1E-06 },
                new double[] { 1E-03, 1E-07, 1E-05 },
                new double[] { 1E-03, 1E-07, 1E-06 },
                new double[] { 1E-03, 1E-07, 1E-07 },
                new double[] { 1E-03, 1E-08, 1E-04 },
                new double[] { 1E-03, 1E-08, 1E-05 },
                new double[] { 1E-03, 1E-08, 1E-06 },
                new double[] { 1E-03, 1E-08, 1E-07 },
                new double[] { 1E-03, 1E-08, 1E-08 },
                new double[] { 1E-04, 1E-05, 1E-05 },
                new double[] { 1E-04, 1E-06, 1E-05 },
                new double[] { 1E-04, 1E-06, 1E-06 },
                new double[] { 1E-04, 1E-07, 1E-07 },
                new double[] { 1E-04, 1E-08, 1E-08 },
                new double[] { 1E-05, 1E-06, 1E-06 },
                new double[] { 1E-05, 1E-07, 1E-07 },
                new double[] { 1E-05, 1E-08, 1E-08 }
            };
            foreach (var dir in Directory.GetDirectories(searchPath))
            {
                var input = new List<string>();
                foreach (var file in Directory.GetFiles(dir))
                    if (file.Contains("-filtered-rep1.bed") || file.Contains("-filtered-rep2.bed"))
                        input.Add(file);
                    else if (file.Contains(".DS_Store"))
                        File.Delete(file);

                int maxParallelInstancesCount = 5;
                int p = maxParallelInstancesCount;
                foreach (var threshold in thresholds)
                {
                    string command = "/C dotnet.exe C:\\Users\\Vahid\\Desktop\\latestV\\CLI.dll -p C:\\Users\\Vahid\\Desktop\\MSPCDataWithEncodePeaks\\MSPCDataWithEncodePeaks\\config.json";
                    foreach (var file in input)
                        command += " -i " + file;

                    command += " -r bio -c 1";
                    command += string.Format(" -w {0} -s {1} -g {2}", threshold[0], threshold[1], threshold[2]);
                    command += string.Format(" -o {0}\\w{1}_s{2}_g{3}", dir, threshold[0], threshold[1], threshold[2]);
                    var process = System.Diagnostics.Process.Start("CMD.exe", command);
                    p--;
                    if (p <= 0)
                    {
                        process.WaitForExit();
                        p = maxParallelInstancesCount;
                    }
                }
            }
        }
    }
}
