using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Entities
{
    [Packet("mv")]
    public class Mv : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Vector2D Position { get; set; }
        public short Speed { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            Position = new Vector2D(packet[2].ToShort(), packet[3].ToShort());
            Speed = packet[4].ToShort();
        }
    }
}