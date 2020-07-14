using Spark.Packet.Characters;

namespace Spark.Tests.Packet.Characters
{
    public class StatPacketTest : PacketTest<Stat>
    {
        protected override string Packet { get; } = "stat 1000 2000 3000 4000";

        protected override Stat Excepted { get; } = new Stat
        {
            Hp = 1000,
            MaxHp = 2000,
            Mp = 3000,
            MaxMp = 4000
        };
    }
}