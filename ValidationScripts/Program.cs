string searchPath = @"C:\";
List<double[]> thresholds = new()
{
    new double[] { 1E-04, 1E-05, 1E-05 },
    new double[] { 1E-04, 1E-06, 1E-05 },
    new double[] { 1E-04, 1E-06, 1E-06 },
    new double[] { 1E-04, 1E-07, 1E-07 },
    new double[] { 1E-04, 1E-08, 1E-06 },
    new double[] { 1E-04, 1E-08, 1E-08 },
    new double[] { 1E-05, 1E-06, 1E-06 },
    new double[] { 1E-05, 1E-07, 1E-07 },
    new double[] { 1E-05, 1E-08, 1E-06 },
    new double[] { 1E-05, 1E-08, 1E-08 }
};

foreach (var dir in Directory.GetDirectories(searchPath))
{
    var input = new List<string>();
    foreach (var file in Directory.GetFiles(dir))
        if (file.Contains("rep1.bed") || file.Contains("rep2.bed"))
            input.Add(file);
        else if (file.Contains(".DS_Store"))
            File.Delete(file);

    int maxParallelInstancesCount = 5;
    int p = maxParallelInstancesCount;
    foreach (var threshold in thresholds)
    {
        string command = @"/C mspc.exe -p config.json";
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
