using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Entities
{
    public class Dir : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Direction Direction { get; set; }
    }
}