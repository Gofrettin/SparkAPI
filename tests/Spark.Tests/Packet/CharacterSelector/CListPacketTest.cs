using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Packet.CharacterSelector
{
    public class CListPacketTest : PacketTest<CList>
    {
        protected override string Packet { get; } = "clist 2 MyNameIs 0 1 0 9 0 0 3 0 -1.12.1.8.-1.-1.-1.-1.-1.-1 2  1 1 0.333.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1 0 0";

        protected override CList Excepted { get; } = new CList
        {
            Slot = 2,
            Name = "MyNameIs"
        };
    }
}