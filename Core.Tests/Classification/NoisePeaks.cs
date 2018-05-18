using Genometric.GeUtilities.IntervalParsers;
using Genometric.GeUtilities.IntervalParsers.Model.Defaults;
using Genometric.MSPC;
using Genometric.MSPC.Core.Model;
using Genometric.MSPC.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Core.Tests.Base
{
    public class NoisePeaks
    {
        private ReadOnlyDictionary<uint, Result<ChIPSeqPeak>> GenerateAndProcessNoisePeaks()
        {
            var sA = new BED<ChIPSeqPeak>();
            sA.Add(new ChIPSeqPeak() { Left = 10, Right = 20, Value = 1e-2 }, "chr1", '*');

            var sB = new BED<ChIPSeqPeak>();
            sB.Add(new ChIPSeqPeak() { Left = 5, Right = 12, Value = 1e-4 }, "chr1", '*');

            var mspc = new MSPC<ChIPSeqPeak>();
            mspc.AddSample(0, sA);
            mspc.AddSample(1, sB);

            var config = new Config(ReplicateType.Biological, 1e-4, 1e-8, 1e-4, 2, 1F, MultipleIntersections.UseLowestPValue);

            // Act
            var res = mspc.Run(config);

            // TODO: this step should not be necessary; remove it after the Results class is updated.
            ///foreach (var rep in res)
            ///    rep.Value.ReadOverallStats();

            return res;
        }

        /// <summary>
        /// Asserts if noise peaks are correctly separated in R_j^b sets.
        /// </summary>
        [Fact]
        public void Separate()
        {
            // Arrange & Act
            var res = GenerateAndProcessNoisePeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].Get(Attributes.Background).Count == 1);
        }

        [Fact]
        public void NoisePeakAreNotConsideredDiscarded()
        {
            // Arrange & Act
            var res = GenerateAndProcessNoisePeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].Get(new Attributes[] { Attributes.Discarded }).Count == 0);
        }

        [Fact]
        public void NoisePeaksAreNotConsideredConfirmed()
        {
            // Arrange & Act
            var res = GenerateAndProcessNoisePeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].Get(new Attributes[] { Attributes.Confirmed }).Count == 0);
        }

        [Fact]
        public void NoisePeaksAreNotOutputed()
        {
            // Arrange & Act
            var res = GenerateAndProcessNoisePeaks();

            // Assert
            foreach (var s in res)
                Assert.True(s.Value.Chromosomes["chr1"].R_j__o.Count == 0);
        }

        // TODO check for all the other sets.
    }
}
