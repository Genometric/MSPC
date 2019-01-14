// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using System.IO;
using Xunit;

namespace Genometric.MSPC.CLI.Tests
{
    public class Logger
    {
        [Fact]
        public void LogFileIsMovedToSessionFolder()
        {
            // Arrange
            string[] files;

            // Act
            using (var tmpMspc = new TmpMspc())
            {
                tmpMspc.Run();
                files = Directory.GetFiles(tmpMspc.SessionPath);
            }

            // Assert
            Assert.Contains(files, x => { return x.Contains("EventsLog_"); });
        }
    }
}
