using Genometric.GeUtilities.Intervals.Model;

namespace Genometric.MSPC.Core
{
    public class Mspc : Mspc<Peak>
    {
        public Mspc(
            bool trackSupportingRegions = false,
            int? maxDegreeOfParallelism = null) :
            base(
                new PeakConstructor(),
                trackSupportingRegions,
                maxDegreeOfParallelism: maxDegreeOfParallelism)
        { }
    }
}
