using Spark.Packet.Extension;

namespace Spark.Packet.Login
{
    [Packet("failc")]
    public class Failc : IPacket
    {
        public byte Reason { get; set; }

        public void Construct(string[] packet)
        {
            Reason = packet[0].ToByte();
        }
    }
}