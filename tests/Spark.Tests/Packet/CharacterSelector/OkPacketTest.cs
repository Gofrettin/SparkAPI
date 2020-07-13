using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Packet.CharacterSelector
{
    public class OkPacketTest : PacketTest<Ok>
    {
        protected override string Packet { get; } = "OK";
        
        protected override Ok Excepted { get; } = new Ok();
    }
}