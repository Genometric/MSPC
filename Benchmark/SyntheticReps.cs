namespace Genometric.MSPC.Benchmark
{
    internal static class SyntheticReps
    {
        public static List<string> Generate(List<string> replicates, int maxRepCount)
        {
            var extReps = new List<string>(replicates);
            if (extReps.Count > maxRepCount)
                return extReps;

            var i = 0;
            while (extReps.Count < maxRepCount)
            {
                extReps.Add(RandomAlter(
                    filename: replicates[i],
                    filenamePostfix: "_rnd_" + (extReps.Count + 1).ToString(),
                    rndSeed: (extReps.Count * 10) + replicates[i].Length));

                if (++i >= replicates.Count)
                    i = 0;
            }

            return extReps;
        }

        public static string RandomAlter(string filename, string filenamePostfix, int rndSeed = 0)
        {
            var rnd = new Random(rndSeed);
            var newFilename = Path.Join(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) +
                filenamePostfix + Path.GetExtension(filename));

            var valueMult = new int[] { 1, -1 };
            using (var reader = new StreamReader(filename))
            using (var writer = new StreamWriter(newFilename))
            {

                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var cols = line.Split("\t");
                    var start = int.Parse(cols[1]);
                    var end = int.Parse(cols[2]);
                    var value = double.Parse(cols[6]);
                    start += rnd.Next(0, end - start - 1);
                    end -= rnd.Next(0, end - start - 1);
                    value += valueMult[rnd.Next(0, 2)] * rnd.Next(0, (int)Math.Floor(value * 0.2));

                    if (end <= start || value < 0)
                        throw new InvalidOperationException(
                            "Invalid data generated, this should not have happend.");

                    cols[1] = start.ToString();
                    cols[2] = end.ToString();
                    cols[6] = value.ToString();

                    writer.WriteLine(string.Join("\t", cols));
                }
            }

            return newFilename;
        }
    }
}
