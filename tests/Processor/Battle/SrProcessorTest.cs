using NFluent;
using Spark.Database.Data;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Packet.Battle;

namespace Spark.Tests.Processor.Battle
{
    public class SrProcessorTest : ProcessorTest<Sr>
    {
        protected override Sr Packet { get; } = new Sr
        {
            CastId = 1
        };
        
        public readonly ISkill Skill = new Skill(240, new SkillData { CastId = 1 })
        {
            IsOnCooldown = true
        };

        public SrProcessorTest()
        {
            Client.Character.Skills = new[] { Skill };
        }
        
        protected override void CheckResult()
        {
            Check.That(Skill.IsOnCooldown).IsFalse();
        }
    }
}