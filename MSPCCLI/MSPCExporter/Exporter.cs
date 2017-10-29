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
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public Exporter(Session<Peak, Metadata> session)
        {
            _session = session;
        }

        public int fileProgress
        {
            set
            {
                _fileProgress = value;
                OnFileProgressChanged(value);
            }
            get { return _fileProgress; }
        }
        private int _fileProgress;
        public event EventHandler<ExporterEventArgs> FileProgressChanged;
        private void OnFileProgressChanged(int value)
        {
            if (FileProgressChanged != null)
                FileProgressChanged(this, new ExporterEventArgs(value));
        }

        public int sampleProgress
        {
            set
            {
                _sampleProgress = value;
                OnSampleProgressChanged(value);
            }
            get { return _sampleProgress; }
        }
        private int _sampleProgress;
        public event EventHandler<ExporterEventArgs> SampleProgressChanged;
        private void OnSampleProgressChanged(int value)
        {
            if (SampleProgressChanged != null)
                SampleProgressChanged(this, new ExporterEventArgs(value));
        }


        private Dictionary<UInt32, AnalysisResult<Peak, Metadata>> _analysisResults { set; get; }
        private Session<Peak, Metadata> _session { set; get; }

        public void Export(ExportOptions options)
        {
            _analysisResults = _session.analysisResults;
            samples = _session.samples;
            includeBEDHeader = options.includeBEDHeader;
            sessionPath = options.sessionPath;

            if (!Directory.Exists(sessionPath))
                Directory.CreateDirectory(sessionPath);

            string date =
                DateTime.Now.Date.ToString("dd'_'MM'_'yyyy", CultureInfo.InvariantCulture) +
                "_h" + DateTime.Now.TimeOfDay.Hours.ToString() +
                "_m" + DateTime.Now.TimeOfDay.Minutes.ToString() +
                "_s" + DateTime.Now.TimeOfDay.Seconds.ToString() + "__";

            mergedReplicates = _session.mergedReplicates;
            Export__MergedReps();

            foreach (var sample in samples)
            {
                int duplicationExtension = 0;
                fileProgress = 0;
                sampleProgress++;
                data = _analysisResults[sample.Key];

                samplePath = sessionPath + Path.DirectorySeparatorChar + date + Path.GetFileNameWithoutExtension(data.FileName);
                while (Directory.Exists(samplePath))
                    samplePath = sessionPath + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(data.FileName) + "_" + (duplicationExtension++).ToString();
                Directory.CreateDirectory(samplePath);

                if (options.Export_R_j__o_BED) { fileProgress++; Export__R_j__o_BED(); }
                if (options.Export_R_j__s_BED) { fileProgress++; Export__R_j__s_BED(); }
                if (options.Export_R_j__w_BED) { fileProgress++; Export__R_j__w_BED(); }
                if (options.Export_R_j__c_BED) { fileProgress++; Export__R_j__c_BED(); }
                if (options.Export_R_j__d_BED) { fileProgress++; Export__R_j__d_BED(); }
            }
        }
    }
}
