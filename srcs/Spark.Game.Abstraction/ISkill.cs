using System;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction
{
    public interface ISkill : IEquatable<ISkill>
    {
        int SkillKey { get; }
        string Name { get; }
        short Range { get; }
        short ZoneRange { get; }
        int CastTime { get; }
        int Cooldown { get; }
        SkillCategory Category { get; }
        int MpCost { get; }
        int CastId { get; }
        SkillTarget Target { get; }
        HitType HitType { get; }
        SkillType SkillType { get; }

        bool IsOnCooldown { get; set; }
    }
}