/** Copyright © 2014-2015 Vahid Jalili
 * 
 * This file is part of MSPC project.
 * MSPC is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * MSPC is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A 
 * PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Foobar. If not, see http://www.gnu.org/licenses/.
 **/

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

        private static readonly Dictionary<Codes, string> _messages = new Dictionary<Codes, string>()
        {
            {Codes.M000, "" },
            { Codes.M001, "X-squared is below chi-squared of Gamma." },
            { Codes.M002, "Intersecting peaks count doesn't comply minimum C requirement." }
        };
    }
}
