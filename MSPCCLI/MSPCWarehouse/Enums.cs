/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

namespace Polimi.DEIB.VahidJalili.MSPC.Warehouse
{
    public enum PeakClassificationType : byte
    {
        Stringent = 0,
        Weak = 1,
        WeakConfirmed = 2,
        WeakDiscarded = 3,
        StringentConfirmed = 4,
        StringentDiscarded = 5,
        TruePositive = 6,
        FalsePositive = 7,
        Input = 8,
        Output = 9,
        Background = 10,
        Confirmed = 11,
        Discarded = 12
    };

    public enum ERClassificationCategory : byte
    {
        First = 0,
        Second = 1,
        Third = 2,
        Fourth = 3
    };

    public enum FDRProcedure : byte
    {
        BenjaminiHochberg = 0
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
