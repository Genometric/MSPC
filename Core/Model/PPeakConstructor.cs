// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;

namespace Genometric.MSPC.Core.Model
{
    public class PPeakConstructor : IPeakConstructor<PPeak>
    {
        public PPeak Construct(int left, int right, double value, string name = null, int summit = -1, string hashSeed = "")
        {
            return new PPeak(left, right, value, name, summit, hashSeed);
        }
    }
}
