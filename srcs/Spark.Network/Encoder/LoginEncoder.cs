namespace Spark.Network.Encoder
{
    public class LoginEncoder : IEncoder
    {
        public byte[] Encode(string packet)
        {
            var bytes = new byte[packet.Length + 1];
            for (int i = 0; i < packet.Length; i++)
            {
                bytes[i] = (byte)((packet[i] ^ 195) + 15);
            }

            bytes[^1] = 0xD8;
            return bytes;
        }
    }
}