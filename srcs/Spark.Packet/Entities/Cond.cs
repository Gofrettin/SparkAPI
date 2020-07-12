using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("cond")]
    public class Cond : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public bool CanAttack { get; set; }
        public bool CanMove { get; set; }
        public byte Speed { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            CanAttack = packet[2].ToBool().Reverse();
            CanMove = packet[3].ToBool().Reverse();
            Speed = packet[4].ToByte();
        }
    }
}