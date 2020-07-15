using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("c_mode")]
    public class CMode : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public int SpecialistId { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            SpecialistId = packet[2].ToInt();
        }
    }
}