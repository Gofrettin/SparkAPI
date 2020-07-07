using System;
using System.Text;

namespace Spark.Gameforge.Extension
{
    public static class StringExtensions
    {
        public static string ToHex(this string value) =>
            BitConverter.ToString(Encoding.Default.GetBytes(value))
                .Replace("-", "")
                .ToUpperInvariant();
    }
}