using System.Diagnostics;

namespace Genometric.MSPC.Benchmark
{
    public class Result
    {
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
    }
}
