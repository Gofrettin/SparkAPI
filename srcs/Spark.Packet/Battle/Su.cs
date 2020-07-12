using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Battle
{
    [Packet("su")]
    public class Su : IPacket
    {
        public EntityType CasterType { get; set; }
        public long CasterId { get; set; }
        public EntityType TargetType { get; set; }
        public long TargetId { get; set; }
        public int SkillKey { get; set; }
        public bool IsTargetAlive { get; set; }
        public byte HpPercentage { get; set; }
        public int Damage { get; set; }
        
        public void Construct(string[] packet)
        {
            CasterType = packet[0].ToEnum<EntityType>();
            CasterId = packet[1].ToLong();
            TargetType = packet[2].ToEnum<EntityType>();
            TargetId = packet[3].ToLong();
            SkillKey = packet[4].ToInt();
            IsTargetAlive = packet[10].ToBool();
            HpPercentage = packet[11].ToByte();
            Damage = packet[12].ToInt();
        }
    }
}