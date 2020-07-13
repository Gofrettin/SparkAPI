using Spark.Packet.Battle;

namespace Spark.Tests.Packet.Battle
{
    public class SrPacketTest : PacketTest<Sr>
    {
        protected override string Packet { get; } = "sr 1";
        
        protected override Sr Excepted { get; } = new Sr
        {
            CastId = 1
        };
    }
}