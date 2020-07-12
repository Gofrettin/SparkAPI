using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    public class Dir : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Direction Direction { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            Direction = packet[2].ToEnum<Direction>();
        }
    }
}