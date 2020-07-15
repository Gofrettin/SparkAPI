using Moq;
using NFluent;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Event.Entities;
using Spark.Event.Notification;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Battle;

namespace Spark.Tests.Processor.Battle
{
    public class SuWithoutDeathProcessorTest : ProcessorTest<Su>
    {
        protected override Su Packet { get; } = new Su
        {
            CasterType = EntityType.Player,
            CasterId = 123456,
            TargetType = EntityType.Monster,
            TargetId = 999,
            SkillKey = 254,
            Damage = 1000,
            IsTargetAlive = true,
            HpPercentage = 50
        };
        
        public ILivingEntity Target { get; } = new Monster(999, 240, new MonsterData());

        public SuWithoutDeathProcessorTest()
        {
            Map.AddEntity(Client.Character);
            Map.AddEntity(Target);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Target.Map).IsNotNull();
            Check.That(Target.HpPercentage).IsEqualTo(50);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<EntityDamageEvent>(s => 
                s.Caster.Equals(Client.Character) && 
                s.Target.Equals(Target) && s.Damage == 1000 && 
                s.SkillKey == 254)), Times.Once);
        }
    }
    
    public class SuWithDeathProcessorTest : ProcessorTest<Su>
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

        public SuWithDeathProcessorTest()
        {
            Map.AddEntity(Client.Character);
            Map.AddEntity(Target);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Target.Map).IsNull();
            Check.That(Target.HpPercentage).IsEqualTo(0);
        }
        
        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<EntityDamageEvent>(s => 
                s.Caster.Equals(Client.Character) && 
                s.Target.Equals(Target) && s.Damage == 1000 && 
                s.SkillKey == 254)), Times.Once);

            EventPipelineMock.Verify(x => x.Emit(It.Is<EntityDeathEvent>(s => s.Entity.Equals(Target) && s.Killer.Equals(Client.Character))), Times.Once);
        }
    }
}