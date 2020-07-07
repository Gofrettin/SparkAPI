using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("mv")]
    public class Mv : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Position Position { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            Position = new Position(packet[2].ToShort(), packet[3].ToShort());
        }
    }
}