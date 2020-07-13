using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class DirPacketTest : PacketTest<Dir>
    {
        protected override string Packet { get; } = "dir 1 123456 0";

        protected override Dir Excepted { get; } = new Dir
        {
            EntityType = EntityType.Player,
            EntityId = 123456,
            Direction = Direction.North
        };
    }
}