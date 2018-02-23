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
    public class SessionOptions
    {
        public double gamma { set; get; }
        public double tauS { set; get; }
        public double tauW { set; get; }
        public ReplicateType replicateType { set; get; }
        public float alpha { set; get; }
        public FDRProcedure fDRprocedure { set; get; }
        public byte C { set; get; }
        public MultipleIntersections multipleIntersections { set; get; }
    }
}
