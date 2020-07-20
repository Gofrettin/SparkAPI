using System;
using System.Collections.Generic;
using System.Text;

namespace Spark.Network.Decoder
{
    public class LoginDecoder : IDecoder
    {
        public IEnumerable<string> Decode(byte[] bytes, int size)
        {
            var packet = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                packet.Append(Convert.ToChar(bytes[i] - 15));
            }

            packet.Remove(packet.Length - 1, 1);
            return new[] { packet.ToString() };
        }
    }
}