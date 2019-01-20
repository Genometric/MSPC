// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Genometric.MSPC.Core.Model
{
    public static class Messages
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

        private static readonly Dictionary<Codes, string> _messages = new Dictionary<Codes, string>()
        {
            {Codes.M000, "" },
            { Codes.M001, "X-squared is below chi-squared of Gamma." },
            { Codes.M002, "Intersecting peaks count doesn't comply minimum C requirement." }
        };
    }
}
