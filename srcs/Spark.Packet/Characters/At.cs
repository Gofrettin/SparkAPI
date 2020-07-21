using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    public class At : IPacket
    {
        public long EntityId { get; set; }
        public int MapId { get; set; }
        public Vector2D Position { get; set; }
        public Direction Direction { get; set; }
    }
}