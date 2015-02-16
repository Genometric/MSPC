/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Polimi.DEIB.VahidJalili.MSPC.Exporter
{
    public class Exporter<Peak, Metadata> : ExporterBase<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, new()
    {
        public bool Export_R_j__o { set; get; }

        public bool Export_R_j__s { set; get; }

        public bool Export_R_j__w { set; get; }

        public bool Export_R_j__v { set; get; }

        public bool Export_R_j__d { set; get; }

        public bool Export_R_j__o_Details { set; get; }

        public bool Export_R_j__v_Details { set; get; }

        public bool Export_R_j__d_Details { set; get; }

        public bool Export_Chromosomewide_stats { set; get; }

        public bool IncludeBEDHeader { set; get; }

        private Dictionary<UInt32, AnalysisResult<Peak, Metadata>> AnalysisResults { set; get; }

        public Session<Peak, Metadata> Session { set; get; }

        private Dictionary<UInt32, string> sampleKeys { set; get; }

        public void Export()
        {
            AnalysisResults = Session.analysisResults;
            sampleKeys = Session.samples;

            if (!Directory.Exists(sessionPath))
                Directory.CreateDirectory(sessionPath);

            includeBEDHeader = IncludeBEDHeader;

            string date =
                DateTime.Now.Date.ToString("dd'_'MM'_'yyyy", CultureInfo.InvariantCulture) +
                "_h" + DateTime.Now.TimeOfDay.Hours.ToString() +
                "_m" + DateTime.Now.TimeOfDay.Minutes.ToString() +
                "_s" + DateTime.Now.TimeOfDay.Seconds.ToString() + "__";

            //ExportOverview(AnalysisResults);

            foreach (var sampleKey in sampleKeys)
            {
                var data = AnalysisResults[sampleKey.Key];

                int DISP = 0; // Duplication Index for Session Path

                samplePath = sessionPath + Path.DirectorySeparatorChar + date + Path.GetFileNameWithoutExtension(data.FileName);

                while (Directory.Exists(samplePath))
                    samplePath = sessionPath + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(data.FileName) + "_" + (DISP++).ToString();

                Directory.CreateDirectory(samplePath);

                if (Export_R_j__o) Export__R_j__o(AnalysisResults[sampleKey.Key]);
                if (Export_R_j__s) Export__R_j__s(AnalysisResults[sampleKey.Key]);
                if (Export_R_j__w) Export__R_j__w(AnalysisResults[sampleKey.Key]);
                if (Export_R_j__v) Export__R_j__v(AnalysisResults[sampleKey.Key]);
                if (Export_R_j__d) Export__R_j__d(AnalysisResults[sampleKey.Key]);

                //if (Export_R_j__v_Details)
                //if (Export_R_j__d_Details)
                //if (Export_Chromosomewide_stats)
                /*if (Export_R_j__o_Details)
                {
                    Export_Output_set_in_Details("A_Output_set_True_Positives_Details", true);
                    Export_Output_set_in_Details("A_Output_set_False_Positives_Details", false);
                }*/
            }
        }
    }
}
