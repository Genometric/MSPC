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
    public enum Attributes : byte
    {
        Stringent = 0,
        Weak = 1,
        WeakConfirmed = 2,
        WeakDiscarded = 3,
        WeakDiscardedC = 4,
        WeakDiscardedT = 5,
        StringentConfirmed = 6,
        StringentDiscarded = 7,
        StringentDiscardedC = 8,
        StringentDiscardedT = 9,
        TruePositive = 10,
        FalsePositive = 11,
        Input = 12,
        Output = 13,
        Background = 14,
        Confirmed = 15,
        Discarded = 16
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
