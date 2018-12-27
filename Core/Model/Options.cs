// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.Core.Model
{
    /// <summary>
    /// The configuration of MSPC process.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// The configuration of MSPC process.
        /// </summary>
        /// <param name="replicateType">Sets the replicate type of the input samples.</param>
        /// <param name="tauW">Sets weak p-value threshold.</param>
        /// <param name="tauS">Sets stringent p-value threshold.</param>
        /// <param name="gamma">Sets combined stringency threshold.</param>
        /// <param name="c">Sets the minimum number of samples having overlapping peaks with a given peak.</param>
        /// <param name="alpha">Sets Benjamini-Hochberg multiple testing correction threshold.</param>
        /// <param name="multipleIntersections">Sets if the peak with lowest or highest p-value 
        /// should be used when multiple peaks from a sample overlap with a given peak.</param>
        public Config(ReplicateType replicateType, double tauW, double tauS, double gamma, int c, float alpha, MultipleIntersections multipleIntersections)
        {
            ReplicateType = replicateType;
            TauW = tauW;
            TauS = tauS;
            Gamma = gamma;
            C = c;
            Alpha = alpha;
            MultipleIntersections = multipleIntersections;
        }
        /// <summary>
        /// Gets the replicate type of the input samples.
        /// </summary>
        public ReplicateType ReplicateType { get; }

        /// <summary>
        /// Gets if the peak with lowest or highest p-value 
        /// should be used when multiple peaks from a sample 
        /// overlap with a given peak.
        /// </summary>
        public MultipleIntersections MultipleIntersections { get; }

        /// <summary>
        /// Gets the value to replace zero p-values.
        /// </summary>
        public const double default0PValue = double.Epsilon;

        /// <summary>
        /// Gets stringent p-value threshold.
        /// </summary>
        public double TauS { get; }

        /// <summary>
        /// Gets weak p-value threshold.
        /// </summary>
        public double TauW { get; }

        /// <summary>
        /// Gets combined stringency threshold. Peaks with 
        /// combined p-value below this threshold are confirmed.
        /// </summary>
        public double Gamma { get; }

        /// <summary>
        /// Gets the minimum number of samples having overlapping 
        /// peaks with a given peak.
        /// </summary>
        /// <remarks>
        /// For example, given three replicates (rep1, rep2 and rep3),
        /// if C = 3, a peak on rep1 must overlap with at least 
        /// one peak from both rep2 and rep3 to combine their 
        /// p-values, otherwise the peak is discarded; 
        /// if C = 2, a peak on rep1 must overlap with at least 
        /// one peak from either rep2 or rep3 to combine their 
        /// p-values, otherwise the peak is discarded.
        /// </remarks>
        public int C { get; }

        /// <summary>
        /// Gets Benjamini-Hochberg multiple 
        /// testing correction threshold
        /// </summary>
        public float Alpha { get; }
    }
}
