/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

using System.ComponentModel;

namespace Polimi.DEIB.VahidJalili.MSPC.Warehouse
{
    public class AnalysisOptions : INotifyPropertyChanged
    {
        public double gamma
        {
            set
            {
                _gamma = value;
                NotifyPropertyChanged("gamma");
            }
            get { return _gamma; }
        }
        private double _gamma;

        public double tauS
        {
            set
            {
                _tauS = value;
                NotifyPropertyChanged("tauS");
            }
            get { return _tauS; }
        }
        private double _tauS;

        public double tauW
        {
            set
            {
                _tauW = value;
                NotifyPropertyChanged("tauW");
            }
            get { return _tauW; }
        }
        private double _tauW;

        public ReplicateType replicateType
        {
            set
            {
                _replicateType = value;
                NotifyPropertyChanged("replicateType");
            }
            get { return _replicateType; }
        }
        private ReplicateType _replicateType;

        public float alpha
        {
            set
            {
                _alpha = value;
                NotifyPropertyChanged("alpha");
            }
            get { return _alpha; }
        }
        private float _alpha;

        public FDRProcedure fDRProcedure
        {
            set
            {
                _fDRProcedure = value;
                NotifyPropertyChanged("fDRProcedure");
            }
            get { return _fDRProcedure; }
        }
        private FDRProcedure _fDRProcedure;

        public byte C
        {
            set
            {
                _C = value;
                NotifyPropertyChanged("C");
            }
            get { return _C; }
        }
        private byte _C;

        public MultipleIntersections multipleIntersections
        {
            set
            {
                _multipleIntersections = value;
                NotifyPropertyChanged("multipleIntersections");
            }
            get { return _multipleIntersections; }
        }
        private MultipleIntersections _multipleIntersections;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
