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
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Polimi.DEIB.VahidJalili.MSPC.Warehouse
{
    public class Session<ER, Metadata> : INotifyPropertyChanged
        where ER : IInterval<int, Metadata>, IComparable<ER>, new()
        where Metadata : IChIPSeqPeak, IComparable<Metadata>, new()
    {
        public bool isCompleted { set; get; }
        public int index { set; get; }
        public Dictionary<uint, string> samples { set; get; }
        public DateTime startTime { set; get; }
        public DateTime endTime { set; get; }
        public string elapsedTime { set; get; }
        public AnalysisOptions options { set; get; }
        public Dictionary<UInt32, AnalysisResult<ER, Metadata>> analysisResults { set; get; }
        public Dictionary<string, SortedList<Interval<ER, Metadata>, ER>> mergedReplicates { set; get; }

        public string status
        {
            set
            {
                _status = value;
                NotifyPropertyChanged("status");
            }
            get { return _status; }
        }
        private string _status;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
