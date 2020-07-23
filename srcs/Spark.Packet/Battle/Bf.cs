using Spark.Core.Enum;

namespace Spark.Packet.Battle
{
    public class Bf : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public long BuffId { get; set; }
        public long Duration { get; set; }
    }
}