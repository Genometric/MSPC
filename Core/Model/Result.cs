// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    public class Result<I>
        where I : IChIPSeqPeak, new()
    {
        public Dictionary<string, Sets<I>> Chromosomes { set; get; }

        public Result()
        {
            Chromosomes = new Dictionary<string, Sets<I>>();
        }

        public void AddChromosome(string chr)
        {
            Chromosomes.Add(chr, new Sets<I>());
        }

        public uint Stats(Attributes type)
        {
            uint rtv = 0;
            foreach (var chr in Chromosomes)
                rtv += chr.Value.Stats[type];
            return rtv;
        }
    }
}
