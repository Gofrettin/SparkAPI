using Spark.Core.Enum;

namespace Spark.Packet.Entities
{
    public class CMode : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public short MorphId { get; set; }
        public byte MorphUpgrade { get; set; }
        public short MorphDesign { get; set; }
        public byte MorphBonus { get; set; }
        public byte Size { get; set; }
    }
}