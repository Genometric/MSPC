// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.Core.Model
{
    public class Config
    {
        public Config(ReplicateType replicateType, double tauW, double tauS, double gamma, byte C, float alpha, MultipleIntersections multipleIntersections)
        {
            ReplicateType = replicateType;
            TauW = tauW;
            TauS = tauS;
            Gamma = gamma;
            this.C = C;
            Alpha = alpha;
            MultipleIntersections = multipleIntersections;
        }
        /// <summary>
        /// Represents the replicate type of the input samples.
        /// </summary>
        public ReplicateType ReplicateType { get; }

        /// <summary>
        /// Only one peak out of multiple peaks of a sample intersecting
        /// with a peak on another sample will be chosen, if the value of
        /// this parameter is TRUE, the peak with lowest p-value will be 
        /// used; otherwise the peak with highest p-value will be used.
        /// </summary>
        public MultipleIntersections MultipleIntersections { get; }

        /// <summary>
        /// Represents the default value to be replace by p-value = 0
        /// of a peak when combining p-values.
        /// </summary>
        public const double default0PValue = double.Epsilon;

        /// <summary>
        /// Represents the Stringent p-value threshold.
        /// </summary>
        public double TauS { private set; get; }

        /// <summary>
        /// Represents the weak p-value threshold.
        /// </summary>
        public double TauW { get; }

        /// <summary>
        /// 
        /// </summary>
        public double Gamma { get; }

        /// <summary>
        /// 
        /// </summary>
        public byte C { get; }

        /// <summary>
        /// 
        /// </summary>
        public float Alpha { get; }
    }
}
