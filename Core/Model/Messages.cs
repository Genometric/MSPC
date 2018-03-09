using System;
using System.Collections.Generic;
using System.Text;

namespace Genometric.MSPC.Core.Model
{
    internal static class Messages
    {
        public static string Decode(byte code)
        {
            return _messages[code];
        }

        private static Dictionary<byte, string> _messages = new Dictionary<byte, string>()
        {
            { 0, "X-squared is below chi-squared of Gamma" },
            { 1, "Intersecting peaks count doesn't comply minimum requirement" }
        };
    }
}
