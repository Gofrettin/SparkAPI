using Spark.Packet.Extension;

namespace Spark.Packet.Battle
{
    [Packet("sr")]
    public class Sr : IPacket
    {
        public int CastId { get; set; }

        public void Construct(string[] packet)
        {
            CastId = packet[0].ToInt();
        }
    }
}