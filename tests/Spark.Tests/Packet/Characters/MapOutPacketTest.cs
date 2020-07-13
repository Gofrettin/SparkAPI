using Spark.Packet.Characters;

namespace Spark.Tests.Packet.Characters
{
    public class MapOutPacketTest : PacketTest<MapOut>
    {
        protected override string Packet { get; } = "mapout";
        
        protected override MapOut Excepted { get; } = new MapOut();
    }
}