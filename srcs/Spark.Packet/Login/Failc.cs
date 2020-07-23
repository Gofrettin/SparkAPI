using Spark.Core.Extension;

namespace Spark.Packet.Login
{
    public class Failc : IPacket
    {
        public byte Reason { get; set; }
    }
}