using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class MvPacketTest : PacketTest<Mv>
    {
        protected override string Packet { get; } = "mv 3 2102 24 143 4";

        protected override Mv Excepted { get; } = new Mv
        {
            EntityType = EntityType.Monster,
            EntityId = 2102,
            Position = new Vector2D(24, 143),
            Speed = 4
        };
    }
}