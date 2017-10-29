/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using Polimi.DEIB.VahidJalili.GIFP;
using Polimi.DEIB.VahidJalili.IGenomics;
using Polimi.DEIB.VahidJalili.MSPC.Analyzer.Data;
using Polimi.DEIB.VahidJalili.MSPC.Warehouse;
using System;
using System.Collections.Generic;
using Polimi.DEIB.VahidJalili.XSquaredData;

namespace Polimi.DEIB.VahidJalili.MSPC.Analyzer
{
    public class Analyzer<Peak, Metadata>
        where Peak : IInterval<int, Metadata>, IComparable<Peak>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        #region .::.      Status Change        .::.

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnStatusValueChaned(value);
                }
            }
        }

        public event EventHandler<ValueEventArgs> statusChanged;
        private void OnStatusValueChaned(string value)
        {
            if (statusChanged != null)
                statusChanged(this, new ValueEventArgs(value));
        }

        #endregion

        private Processor<Peak, Metadata> processor { set; get; }

        public Analyzer()
        {
            Data<Peak, Metadata>.sampleKeys = new Dictionary<uint, string>();
        }
        public void AddSample(uint sampleKey, string fileName)
        {
            Data<Peak, Metadata>.sampleKeys.Add(sampleKey, fileName);
        }
        public void Run(AnalysisOptions options)
        {
            Options.replicateType = options.replicateType;
            Options.C = options.C;
            Options.tauS = options.tauS;
            Options.tauW = options.tauW;
            Options.gamma = options.gamma;
            Options.alpha = options.alpha;
            Options.multipleIntersections = options.multipleIntersections;

            Data<Peak, Metadata>.cachedChiSqrd = new List<double>();
            for (int i = 1; i <= Data<Peak, Metadata>.sampleKeys.Count; i++)
                Data<Peak, Metadata>.cachedChiSqrd.Add(Math.Round(ChiSquaredCache.ChiSqrdINVRTP(Options.gamma, (byte)(i * 2)), 3));

            Data<Peak, Metadata>.BuildSharedItems();

            processor = new Processor<Peak, Metadata>();

            #region .::.    Status      .::.
            int totalSteps = Data<Peak, Metadata>.sampleKeys.Count + 5;
            int stepCounter = 0;
            #endregion

            Console.WriteLine("");
            foreach (var sampleKey in Data<Peak, Metadata>.sampleKeys)
            {
                var sample = Samples<Peak, Metadata>.Data[sampleKey.Key];

                #region .::.    Status      .::.
                if (sampleKey.Value.Length > 36)
                    Console.Write("\r[" + (++stepCounter).ToString() + "\\" + totalSteps + "] processing sample: ..." + sampleKey.Value.Substring(sampleKey.Value.Length - 35, 35));
                else
                    Console.Write("\r[" + (++stepCounter).ToString() + "\\" + totalSteps + "] processing sample: " + sampleKey.Value);
                Console.WriteLine("");
                #endregion

                foreach (var chr in sample.intervals)
                    foreach (var strand in chr.Value)
                    {
                        int currentLineCursor = Console.CursorTop;
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write("\r .::. Processing {0}", chr.Key);

                        processor.Run(sampleKey.Key, chr.Key, strand.Key);
                    }
            }

            #region .::.    Status      .::.
            Console.WriteLine("\r[" + (++stepCounter).ToString() + "\\" + totalSteps + "] Purifying intermediate sets.");
            #endregion
            processor.IntermediateSetsPurification();

            #region .::.    Status      .::.
            Console.WriteLine("[" + (++stepCounter).ToString() + "\\" + totalSteps + "] Creating output set.");
            #endregion
            processor.CreateOuputSet();

            #region .::.    Status      .::.
            Console.WriteLine("[" + (++stepCounter).ToString() + "\\" + totalSteps + "] Performing Multiple testing correction.");
            #endregion
            processor.EstimateFalseDiscoveryRate();

            #region .::.    Status      .::.
            Console.WriteLine("[" + (++stepCounter).ToString() + "\\" + totalSteps + "] Creating combined output set.");
            #endregion
            processor.CreateCombinedOutputSet();
        }
        public Dictionary<uint, AnalysisResult<Peak, Metadata>> GetResults()
        {
            return Data.Data<Peak, Metadata>.analysisResults;
        }
        public Dictionary<string, SortedList<Warehouse.Interval<Peak, Metadata>, Peak>> GetMergedReplicates()
        {
            return Data.Data<Peak, Metadata>.mergedReplicates;
        }
        public Dictionary<uint, string> GetSamples()
        {
            return Data<Peak, Metadata>.sampleKeys;
        }
    }
}
