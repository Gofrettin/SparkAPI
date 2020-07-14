using NFluent;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Battle;

namespace Spark.Tests.Processor.Battle
{
    public class SuProcessorTest : ProcessorTest<Su>
    {
        protected override Su Packet { get; } = new Su
        {
            CasterType = EntityType.Player,
            CasterId = 123456,
            TargetType = EntityType.Monster,
            TargetId = 999,
            SkillKey = 254,
            Damage = 1000,
            IsTargetAlive = false,
            HpPercentage = 0
        };
        
        public ILivingEntity Target { get; } = new Monster(999, 240, new MonsterData());

        public SuProcessorTest()
        {
            Map.AddEntity(Client.Character);
            Map.AddEntity(Target);
        }
        
        protected override void CheckResult()
        {
            Check.That(Target.HpPercentage).IsEqualTo(0);
            Check.That(Map.GetEntity(Target.EntityType, Target.Id)).IsNull();
        }
    }
}