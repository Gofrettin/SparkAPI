using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Entities
{
    public class Out : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }
}