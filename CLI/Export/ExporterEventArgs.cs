// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;

namespace Genometric.MSPC.CLI.Exporter
{
    public class ExporterEventArgs : EventArgs
    {
        public int status { get; set; }
        public ExporterEventArgs(int status)
        {
            this.status = status;
        }
    }
}
