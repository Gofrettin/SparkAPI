using Spark.Core.Enum;

namespace Spark.Database.Data
{
    public class SkillData
    {
        public string NameKey { get; set; }
        public short Range { get; set; }
        public short ZoneRange { get; set; }
        public int CastTime { get; set; }
        public int Cooldown { get; set; }
        public SkillCategory Category { get; set; }
        public int MpCost { get; set; }
        public int CastId { get; set; }
        public SkillTarget Target { get; set; }
        public HitType HitType { get; set; }
        public SkillType SkillType { get; set; }
    }
}