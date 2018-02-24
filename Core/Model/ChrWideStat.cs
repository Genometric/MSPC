/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using System;

namespace Genometric.MSPC.Model
{
    public class ChrWideStat
    {
        /// <summary>
        /// gets and sets the total peaks count on the chromosomes.
        /// </summary>
        public uint R_j__t_c { set; get; }

        /// <summary>
        /// gets and sets the total number of stringent peaks count on the chromosome.
        /// </summary>
        public uint R_j__s_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of total number of stringent peaks count on the chromosome.
        /// </summary>
        public string R_j__s_p { set; get; }

        /// <summary>
        /// gets and sets the total number of weak peaks count on the chromosome.
        /// </summary>
        public uint R_j__w_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of total number of weak peaks count on the chromosome.
        /// </summary>
        public string R_j__w_p { set; get; }

        /// <summary>
        /// gets and sets the total number of Confirmed peaks count on the chromosome.
        /// </summary>
        public uint R_j__c_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of total number of Confirmed peaks count on the chromosome.
        /// </summary>
        public string R_j__c_p { set; get; }

        /// <summary>
        /// gets and sets the total number of Discarded peaks count on the chromosome.
        /// </summary>
        public uint R_j__d_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of total number of Discarded peaks count on the chromosome.
        /// </summary>
        public string R_j__d_p { set; get; }

        /// <summary>
        /// gets and sets the total number of peaks count in Output set on the chromosome.
        /// </summary>
        public uint R_j__o_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of total number of peaks count in Output set on the chromosome.
        /// </summary>
        public string R_j__o_p { set; get; }

        /// <summary>
        /// gets and set the total number of True-Positive peaks count in Output set on the chromosome
        /// </summary>
        public uint R_j_TP_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of True-Positive peaks count in Output set on the chromosome.
        /// </summary>
        public string R_j_TP_p { set; get; }

        /// <summary>
        /// gets and set the total number of False-Positive peaks count in Output set on the chromosome
        /// </summary>
        public uint R_j_FP_c { set; get; }

        /// <summary>
        /// gets and sets the percentage of False-Positive peaks count in Output set on the chromosome.
        /// </summary>
        public string R_j_FP_p { set; get; }
    }
}
