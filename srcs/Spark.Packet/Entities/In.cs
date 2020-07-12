using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("in")]
    public class In : IPacket
    {
        public EntityType EntityType { get; set; }
        public string Name { get; set; }
        public int GameKey { get; set; }
        public long EntityId { get; set; }
        public Vector2D Position { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            if (EntityType == EntityType.Player)
            {
                Name = packet[1];
                EntityId = packet[3].ToLong();
                Position = new Vector2D(packet[4].ToShort(), packet[5].ToShort());
            }
            else if (EntityType == EntityType.Monster || EntityType == EntityType.Npc)
            {
                GameKey = packet[1].ToInt();
                EntityId = packet[2].ToLong();
                Position = new Vector2D(packet[3].ToShort(), packet[4].ToShort());
            }
        }
    }
}