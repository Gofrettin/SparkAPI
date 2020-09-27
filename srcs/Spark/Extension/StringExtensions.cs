using System.Security.Cryptography;
using System.Text;

namespace Spark.Extension
{
    public static class StringExtensions
    {
        public static string ToMd5(this string value)
        {
            var md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            var sb = new StringBuilder();
        
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}