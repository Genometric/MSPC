// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.Core
{
    public class Mspc : Mspc<PPeak>
    {
        public Mspc(bool trackSupportingRegions = false) : base(new PPeakConstructor(), trackSupportingRegions)
        { }
    }
}
