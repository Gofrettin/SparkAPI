using System;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction;

namespace Spark.Game
{
    public class Skill : ISkill
    {
        public int SkillKey { get; }
        public string Name { get; }
        public short Range { get; }
        public short ZoneRange { get; }
        public int CastTime { get; }
        public int Cooldown { get; }
        public SkillCategory Category { get; }
        public int MpCost { get; }
        public int CastId { get; }
        public SkillTarget Target { get; }
        public HitType HitType { get; }
        
        public bool IsOnCooldown { get; set; }

        public Skill(int skillKey, SkillData data)
        {
            SkillKey = skillKey;
            Name = data.NameKey;
            Range = data.Range;
            ZoneRange = data.ZoneRange;
            CastTime = data.CastTime;
            Cooldown = data.Cooldown;
            Category = data.Category;
            MpCost = data.MpCost;
            CastId = data.CastId;
            Target = data.Target;
            HitType = data.HitType;
        }

        public bool Equals(ISkill other) => other != null && other.SkillKey == SkillKey;
    }
}