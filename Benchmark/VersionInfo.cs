using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Genometric.MSPC.Benchmark
{
    internal class VersionInfo
    {
        private string _invocation = "mspc.exe";

        public string Args { set; get; } = "-r bio -w 1e-4 -s 1e-8";

        public string Version { get; }

        public ProcessStartInfo StartInfo
        {
            get
            {
                return new ProcessStartInfo(
                    _invocation,
                    Args + " -i " + string.Join(" -i ", InputFiles));
            }
        }

        public List<string> InputFiles { set; get; } = new();

        public Uri ReleaseUri { private set; get; }


        public VersionInfo(
            string version,
            string baseReleaseUri = "https://github.com/Genometric/MSPC/releases/")
        {
            Version = version;
            ReleaseUri = new Uri(baseReleaseUri);
            if (!TryRunVerSpecificConfig(version))
                throw new ArgumentException($"Unsupported version {version}.");

            (var success, var root) = TryLocalize().Result;
            if (!success)
                throw new ArgumentException($"No release for version {version} is found.");
            _invocation = Path.Join(root, _invocation);
        }

        private bool TryRunVerSpecificConfig(string version)
        {
            var pattern = new Regex(@"^v[4,5]\.\d+\.\d+$");
            if (pattern.IsMatch(version))
            {
                _invocation = "mspc.exe";
                ReleaseUri = new Uri(ReleaseUri, $"download/{version}/mspc.zip");
            }
            else
            {
                return false;
            }

            return true;
        }

        private async Task<(bool, string)> TryLocalize()
        {
            var dir = Path.Join(Path.GetTempPath(), "mspc", Version.ToLower().Replace(".", "_"));
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
            Directory.CreateDirectory(dir);
            var filename = Path.Join(dir, "mspc.zip");

            var client = new HttpClient();
            var response = await client.GetAsync(ReleaseUri);
            if (!response.IsSuccessStatusCode)
                return (false, string.Empty);

            using (var stream = new FileStream(filename, FileMode.CreateNew))
                await response.Content.CopyToAsync(stream);

            ZipFile.ExtractToDirectory(filename, dir);

            return (true, dir);
        }
    }
}
