using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Battle
{
    [Packet("die")]
    public class Die : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
        }
    }
}