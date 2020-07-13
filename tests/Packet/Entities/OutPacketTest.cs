using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class OutPacketTest : PacketTest<Out>
    {
        protected override string Packet { get; } = "out 1 123456";

        protected override Out Excepted { get; } = new Out
        {
            EntityType = EntityType.Player,
            EntityId = 123456
        };
    }
}