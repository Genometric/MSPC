using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public class Result
    {
        private const string _delimiter = "\t";

        public string Version { set; get; } = "not_specified";

        public string ExperimentId { set; get; } = "not_specified";

        public int ReplicateCount { set; get; }

        public int IntervalCount { set; get; }

        /// <summary>
        /// Gets and sets the maximum amount of physical memory used, in byte. 
        /// </summary>
        public long PeakPhysicalMemoryUsage { set; get; }

        /// <summary>
        /// The maximum amount of virtual memory, in bytes, allocated for the process.
        /// </summary>
        public long PeakPagedMemoryUsage { set; get; }

        /// <summary>
        /// Gets and sets the maximum amount of memory, in bytes,
        /// allocated in the virtual memory paging file for the process.
        /// </summary>
        public long PeakVirtualMemoryUsage { set; get; }

        public Stopwatch Runtime { get; } = new();

        public static string GetHeader(string delimiter = _delimiter)
        {
            return string.Join(delimiter, new string[]
            {
                "mspc_version",
                "experiment_id",
                "replicate_count",
                "interval_count",
                "runtime_seconds",
                "peak_physical_memory_usage_bytes",
                "peak_paged_memory_usage_bytes",
                "peak_virtual_memory_usage_bytes"
            });
        }

        public string ToString(string delimiter = _delimiter)
        {
            return string.Join(delimiter, new string[]
            {
                Version,
                ExperimentId,
                ReplicateCount.ToString(),
                IntervalCount.ToString(),
                Runtime.Elapsed.TotalSeconds.ToString(),
                PeakPhysicalMemoryUsage.ToString(),
                PeakPagedMemoryUsage.ToString(),
                PeakVirtualMemoryUsage.ToString()
            });
        }
    }
}
