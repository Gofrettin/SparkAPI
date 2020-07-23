using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Battle
{
    public class Su : IPacket
    {
        public EntityType CasterType { get; set; }
        public long CasterId { get; set; }
        public EntityType TargetType { get; set; }
        public long TargetId { get; set; }
        public int SkillKey { get; set; }
        public bool IsTargetAlive { get; set; }
        public int HpPercentage { get; set; }
        public int Damage { get; set; }
    }
}