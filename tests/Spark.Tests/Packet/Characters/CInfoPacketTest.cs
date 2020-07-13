using Spark.Packet.Characters;

namespace Spark.Tests.Packet.Characters
{
    public class CInfoPacketTest : PacketTest<CInfo>
    {
        protected override string Packet { get; } = "c_info Isha - -1 2858.917 Family 123456 0 0 1 100 2 26 0 0 0 0 0 0 0";

        protected override CInfo Excepted { get; } = new CInfo
        {
            Name = "Isha",
            Id = 123456,
        };
    }
}