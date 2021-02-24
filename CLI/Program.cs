// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Genometric.MSPC.CLI.Tests")]
namespace Genometric.MSPC.CLI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Environment.ExitCode = 0;
            using (var orchestrator = new Orchestrator())
                orchestrator.Orchestrate(args);
        }
    }
}
