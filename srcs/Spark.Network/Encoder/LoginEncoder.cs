namespace Spark.Network.Encoder
{
    public class LoginEncoder : IEncoder
    {
        public byte[] Encode(string packet)
        {
            var bytes = new byte[packet.Length + 1];
            for (int i = 0; i < packet.Length; i++)
            {
                bytes[i] = (byte)((packet[i] ^ 0xC3) + 0xF);
            }

            bytes[^1] = 0xD8;
            return bytes;
        }
    }
}