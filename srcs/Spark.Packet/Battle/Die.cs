using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Battle
{
    public class Die : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }
}