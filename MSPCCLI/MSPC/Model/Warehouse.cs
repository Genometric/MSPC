/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Genometric.IGenomics;
using System;
using System.Collections.Generic;

namespace Genometric.MSPC.Model
{
    public static class Samples<ER, Metadata>
        where ER : IInterval<int, Metadata>, IComparable<ER>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        //public static Dictionary<uint, ParsedChIPseqPeaks<int, ER, Metadata>> Data { set; get; }
    }

    public static class Sessions<ER, Metadata>
        where ER : IInterval<int, Metadata>, IComparable<ER>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public static Dictionary<string, Session<ER, Metadata>> Data { set; get; }

        public static string GetSessionTitle()
        {
            /*int c = 0;
            string counter = "";

            do counter = ++c < 10 ? "0" + c.ToString() : c.ToString();
            while (Sessions<ER, Metadata>.Data.ContainsKey("Session_" + counter));
            */
            return "Session_";// + counter;
        }
    }
}
