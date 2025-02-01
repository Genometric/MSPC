using System;
using System.Linq;

namespace Genometric.MSPC.CLI.Tests
{
    public static class Utilities
    {
        private static readonly Random _random = new Random();

        public static string GetRandomString(
            int length,
            string chars = "abcdefghijklmnopqrstuvwxyz")
        {
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
