using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Packet.CharacterSelector
{
    public class CListEndPacketTest : PacketTest<CListEnd>
    {
        protected override string Packet { get; } = "clist_end";
        
        protected override CListEnd Excepted { get; } = new CListEnd();
    }
}