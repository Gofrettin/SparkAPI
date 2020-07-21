using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Battle
{
    public class Die : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }
}