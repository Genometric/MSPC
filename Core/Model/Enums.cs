// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.Core.Model
{
    public enum Attributes : byte
    {
        Background = 0,
        Weak = 1,
        Stringent = 2,
        Confirmed = 3,
        Discarded = 4,
        TruePositive = 5,
        FalsePositive = 6
    };

    public enum MultipleIntersections : byte
    {
        UseLowestPValue = 0,
        UseHighestPValue
    };

    public enum ReplicateType : byte
    {
        Technical = 0,
        Biological = 1
    };
}
