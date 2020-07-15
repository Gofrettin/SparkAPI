using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("c_mode")]
    public class CMode : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public short MorphId { get; set; }
        public byte MorphUpgrade { get; set; }
        public short MorphDesign { get; set; }
        public byte MorphBonus { get; set; }
        public byte Size { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();

            MorphId = packet[2].ToShort();
            MorphUpgrade = packet[3].ToByte();
            MorphDesign = packet[4].ToShort();

            if (EntityType == EntityType.Player)
            {
                MorphBonus = packet[5].ToByte();
                Size = packet[6].ToByte();
            }
        }
    }
}