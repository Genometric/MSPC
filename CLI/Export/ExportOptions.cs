// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.MSPC.CLI.Exporter
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
