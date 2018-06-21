// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;

namespace Genometric.MSPC.CLI.Exporter
{
    public class Exporter<P> : ExporterBase<P>
        where P : IChIPSeqPeak, new()
    {
        public Exporter()
        {
        }

        public int FileProgress
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
            FileProgressChanged?.Invoke(this, new ExporterEventArgs(value));
        }

        public int SampleProgress
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
            SampleProgressChanged?.Invoke(this, new ExporterEventArgs(value));
        }

        public void Export(
            Dictionary<uint, string> fileNames,
            ReadOnlyDictionary<uint, Result<P>> results,
            ReadOnlyDictionary<string, SortedList<P, P>> mergedReplicates,
            ExportOptions options)
        {
            includeBEDHeader = options.includeBEDHeader;
            sessionPath = options.sessionPath;

            if (!Directory.Exists(sessionPath))
                Directory.CreateDirectory(sessionPath);

            string date =
                DateTime.Now.Date.ToString("dd'_'MM'_'yyyy", CultureInfo.InvariantCulture) +
                "_h" + DateTime.Now.TimeOfDay.Hours.ToString() +
                "_m" + DateTime.Now.TimeOfDay.Minutes.ToString() +
                "_s" + DateTime.Now.TimeOfDay.Seconds.ToString() + "__";

            base.mergedReplicates = mergedReplicates;
            Export__MergedReps();

            foreach (var result in results)
            {
                int duplicationExtension = 0;
                FileProgress = 0;
                SampleProgress++;
                data = result.Value;

                samplePath = sessionPath + Path.DirectorySeparatorChar + date + Path.GetFileNameWithoutExtension(fileNames[result.Key]);
                while (Directory.Exists(samplePath))
                    samplePath = sessionPath + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(fileNames[result.Key]) + "_" + (duplicationExtension++).ToString();
                Directory.CreateDirectory(samplePath);

                if (options.Export_R_j__o_BED) { FileProgress++; Export__R_j__o_BED(); }
                if (options.Export_R_j__s_BED) { FileProgress++; Export__R_j__s_BED(); }
                if (options.Export_R_j__w_BED) { FileProgress++; Export__R_j__w_BED(); }
                if (options.Export_R_j__c_BED) { FileProgress++; Export__R_j__c_BED(); }
                if (options.Export_R_j__d_BED) { FileProgress++; Export__R_j__d_BED(); }
            }
        }
    }
}
