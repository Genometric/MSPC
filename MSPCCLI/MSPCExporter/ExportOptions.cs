/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

namespace Polimi.DEIB.VahidJalili.MSPC.Exporter
{
    public class ExportOptions
    {
        public ExportOptions(
            string sessionPath,
            bool includeBEDHeader,
            bool Export_R_j__o_BED,
            bool Export_R_j__s_BED,
            bool Export_R_j__w_BED,
            bool Export_R_j__c_BED,
            bool Export_R_j__d_BED,
            bool Export_Chromosomewide_stats)
        {
            this.sessionPath = sessionPath;
            this.includeBEDHeader = includeBEDHeader;
            this.Export_R_j__o_BED = Export_R_j__o_BED;
            this.Export_R_j__s_BED = Export_R_j__s_BED;
            this.Export_R_j__w_BED = Export_R_j__w_BED;
            this.Export_R_j__c_BED = Export_R_j__c_BED;
            this.Export_R_j__d_BED = Export_R_j__d_BED;
            this.Export_Chromosomewide_stats = Export_Chromosomewide_stats;
        }
        public bool Export_R_j__o_BED { private set; get; }
        public bool Export_R_j__s_BED { private set; get; }
        public bool Export_R_j__w_BED { private set; get; }
        public bool Export_R_j__c_BED { private set; get; }
        public bool Export_R_j__d_BED { private set; get; }
        public bool Export_Chromosomewide_stats { private set; get; }
        public bool includeBEDHeader { private set; get; }
        public string sessionPath { private set; get; }
    }
}
