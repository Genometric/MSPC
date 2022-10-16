using System.Collections.Generic;

namespace Genometric.MSPC.Core.Model
{
    internal static class Messages
    {
        public enum Codes : byte
        {
            /// <summary>
            /// Default value; empty string.
            /// </summary>
            M000,

            /// <summary>
            /// X-squared is below chi-squared of Gamma.
            /// </summary>
            M001,

            /// <summary>
            /// Intersecting peaks count doesn't comply minimum C requirement.
            /// </summary>
            M002
        }

        public static string Decode(Codes code)
        {
            return _messages[code];
        }

        private static readonly Dictionary<Codes, string> _messages = new()
        {
            {Codes.M000, "" },
            { Codes.M001, "X-squared is below chi-squared of Gamma." },
            { Codes.M002, "Intersecting peaks count doesn't comply minimum C requirement." }
        };
    }
}
