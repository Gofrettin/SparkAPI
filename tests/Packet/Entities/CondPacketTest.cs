using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class CondPacketTest : PacketTest<Cond>
    {
        protected override string Packet { get; } = "cond 1 123456 0 0 12";

        protected override Cond Excepted { get; } = new Cond
        {
            EntityType = EntityType.Player,
            EntityId = 123456,
            CanAttack = true,
            CanMove = true,
            Speed = 12
        };
    }
}