using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Battle
{
    [Packet("bf")]
    public class Bf : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public long BuffId { get; set; }
        public long Duration { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();

            string[] buffInfo = packet[2].Split('.');
            BuffId = buffInfo[1].ToLong();
            Duration = buffInfo[2].ToLong();
        }
    }
}