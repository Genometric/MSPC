// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.Model
{
    public enum Attributes : byte
    {
        Stringent,
        Weak,
        Background,
        Confirmed,
        Discarded,
        TruePositive,
        FalsePositive
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
