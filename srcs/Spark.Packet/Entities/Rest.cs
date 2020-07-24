using Spark.Core.Enum;

namespace Spark.Packet.Entities
{
    public class Rest : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public bool IsResting { get; set; }
    }
}