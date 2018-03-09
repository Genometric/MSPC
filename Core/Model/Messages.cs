using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    internal static class Messages
    {
        public static string XsqrdBelowGamma { get { return "X-squared is below chi-squared of Gamma"; } }
        public static string MinimumCNotSatisfied { get { return "Intersecting peaks count doesn't comply minimum requirement"; } }
    }
}
