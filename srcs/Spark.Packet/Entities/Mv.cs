using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Entities
{
    public class Mv : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Vector2D Position { get; set; }
        public short Speed { get; set; }
    }
}