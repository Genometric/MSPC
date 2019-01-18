// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.Core.Model
{
    public enum Attributes : byte
    {
        Background = 2,
        Weak = 4,
        Stringent = 8,
        Confirmed = 16,
        Discarded = 32,
        TruePositive = 64,
        FalsePositive = 128
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
