// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using Genometric.MSPC.Core.Model;
using System.Collections.Generic;
using static Genometric.MSPC.Core.Model.Messages;

namespace Genometric.MSPC.Core.Interfaces
{
    public interface IPPeak : IPeak
    {
        double XSquared { set; get; }
        double RTP { get; }
        double AdjPValue { set; get; }
        string Reason { get; }
        int SupportingPeaksCount { set; get; }
        List<SupportingPeak<IPPeak>> SupportingPeaks { set;  get; }

        void SetReason(Codes code);

        void AddClassification(Attributes attribute);

        bool HasAttribute(Attributes attribute);

        bool Different(Attributes attribute);

        bool Different(byte attribute);
    }
}
