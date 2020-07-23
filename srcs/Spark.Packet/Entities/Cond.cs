using Spark.Core.Enum;

namespace Spark.Packet.Entities
{
    public class Cond : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public bool CanAttack { get; set; }
        public bool CanMove { get; set; }
        public short Speed { get; set; }
    }
}