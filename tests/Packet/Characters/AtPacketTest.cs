using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Characters;

namespace Spark.Tests.Packet.Characters
{
    public class AtPacketTest : PacketTest<At>
    {
        protected override string Packet { get; } = "at 123456 2544 24 42 2 0 53 1 -1";

        protected override At Excepted { get; } = new At
        {
            EntityId = 123456,
            MapId = 2544,
            Position = new Vector2D(24, 42),
            Direction = Direction.South
        };
    }
}