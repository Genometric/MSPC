/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Polimi.DEIB.VahidJalili.GIFP;
using Polimi.DEIB.VahidJalili.IGenomics;
using System;

namespace Polimi.DEIB.VahidJalili.MSPC.CLI
{
    internal class LoadSamples<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, new()
    {
        internal static ParsedChIPseqPeaks<int, Peak, Metadata> LoadSample(string arg)
        {
            BEDParser<Peak, Metadata> bedParser =
                new BEDParser<Peak, Metadata>(
                    source: arg,
                    species: Genomes.HomoSapiens,
                    assembly: Assemblies.hm19,
                    readOnlyValidChrs: false,
                    startOffset: 0,
                    chrColumn: 0,
                    leftEndColumn: 1,
                    rightEndColumn: 2,
                    nameColumn: 3,
                    valueColumn: 4,
                    strandColumn: -1,
                    defaultValue: 0.1,
                    pValueFormat: pValueFormat.minus1_Log10_pValue,
                    dropPeakIfInvalidValue: true);

            return bedParser.Parse();
        }
    }
}
