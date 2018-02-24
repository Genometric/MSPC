
/** Copyright © 2014-2015 Vahid Jalili
* 
* This file is part of MSPC project.
* MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
* either version 3 of the License, or (at your option) any later version.
* MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
* PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
**/
namespace Genometric.MSPC.Model
{
    public class Config
    {
        public Config(ReplicateType replicateType, double tauW, double tauS, double gamma, byte C, float alpha, MultipleIntersections multipleIntersections)
        {
            this.ReplicateType = replicateType;
            this.TauW = tauW;
            this.TauS = tauS;
            this.Gamma = gamma;
            this.C = C;
            this.Alpha = alpha;
            this.MultipleIntersections = multipleIntersections;
        }
        /// <summary>
        /// Represents the replicate type of the input samples.
        /// </summary>
        public ReplicateType ReplicateType { private set; get; }

        /// <summary>
        /// Only one peak out of multiple peaks of a sample intersecting
        /// with a peak on another sample will be chosen, if the value of
        /// this parameter is TRUE, the peak with lowest p-value will be 
        /// used; otherwise the peak with highest p-value will be used.
        /// </summary>
        public MultipleIntersections MultipleIntersections { private set; get; }

        /// <summary>
        /// Represents the default value for maximum log of p-value. 
        /// p-value lower than this value will be truncated. 
        /// </summary>
        public const double defaultMaxLogOfPVvalue = -3000.0;

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
        public double TauW { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public double Gamma { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public byte C { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public float Alpha { private set; get; }
    }
}
