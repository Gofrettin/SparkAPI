using NFluent;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Battle;
using Spark.Packet.Battle;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class BattleProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(Sr))]
        public void Sr_Test()
        {
            using (GameContext context = CreateContext())
            {
                ISkill firstSkill = TestFactory.CreateSkill(x => x.IsOnCooldown = true);
                ISkill secondSkill = TestFactory.CreateSkill(x => x.IsOnCooldown = true);

                context.Character.Skills = new[] { firstSkill, secondSkill };

                context.Process(new Sr
                {
                    CastId = firstSkill.CastId
                });

                Check.That(firstSkill.IsOnCooldown).IsFalse();
                Check.That(secondSkill.IsOnCooldown).IsTrue();
            }
        }

        [ProcessorTest(typeof(Su))]
        [EventTest(typeof(EntityDamageEvent))]
        public void Su_Non_Lethal_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity target = TestFactory.CreateMonster();

                IMap map = TestFactory.CreateMap(character, target);

                context.Process(new Su
                {
                    CasterType = character.EntityType,
                    CasterId = character.Id,
                    TargetType = target.EntityType,
                    TargetId = target.Id,
                    SkillKey = 254,
                    Damage = 1000,
                    IsTargetAlive = true,
                    HpPercentage = 34
                });

                Check.That(target.Map).IsNotNull();
                Check.That(map.Entities).Contains(target);
                Check.That(target.HpPercentage).IsEqualTo(34);

                context.Verify<EntityDamageEvent>(x => x.Caster.Equals(character) && x.Target.Equals(target) && x.Damage == 1000 && x.SkillKey == 254);
            }
        }

        [ProcessorTest(typeof(Die))]
        [EventTest(typeof(EntityDeathEvent))]
        public void Die_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity monster = TestFactory.CreateMonster();

                IMap map = TestFactory.CreateMap(character, monster);
                
                context.Process(new Die
                {
                    EntityType = monster.EntityType,
                    EntityId = monster.Id
                });

                Check.That(monster.Map).IsNull();
                Check.That(map.Entities).Not.Contains(monster);
                Check.That(monster.HpPercentage).IsEqualTo(0);
                
                context.Verify<EntityDeathEvent>(x => x.Entity.Equals(monster));
            }
        }

        [ProcessorTest(typeof(Su))]
        [EventTest(typeof(EntityDamageEvent))]
        [EventTest(typeof(EntityDeathEvent))]
        public void Su_Lethal_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity target = TestFactory.CreateMonster();

                IMap map = TestFactory.CreateMap(character, target);

                context.Process(new Su
                {
                    CasterType = character.EntityType,
                    CasterId = character.Id,
                    TargetType = target.EntityType,
                    TargetId = target.Id,
                    SkillKey = 254,
                    Damage = 1000,
                    IsTargetAlive = false,
                    HpPercentage = 0
                });

                Check.That(target.Map).IsNull();
                Check.That(map.Entities).Not.Contains(target);
                Check.That(target.HpPercentage).IsEqualTo(0);

                context.Verify<EntityDamageEvent>(x => x.Caster.Equals(character) && x.Target.Equals(target) && x.Damage == 1000 && x.SkillKey == 254);
                context.Verify<EntityDeathEvent>(x => x.Killer.Equals(character) && x.Entity.Equals(target));
            }
        }

        [ProcessorTest(typeof(Bf))]
        [EventTest(typeof(EntityReceiveBuffEvent))]
        public void Bf_Add_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity target = TestFactory.CreateMonster();

                IMap map = TestFactory.CreateMap(character, target);

                context.Process(new Bf
                {
                    EntityType = target.EntityType,
                    EntityId = target.Id,
                    BuffId = 150,
                    Duration = 1 * 600
                });

                Check.That(target.Buffs).HasElementThatMatches(x => x.Id == 150);

                context.Verify<EntityReceiveBuffEvent>(x => x.Entity.Equals(target) && x.Buff.Id == 150);
            }
        }
        
        [ProcessorTest(typeof(Bf))]
        [EventTest(typeof(EntityRemoveBuffEvent))]
        public void Bf_Remove_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity target = TestFactory.CreateMonster();

                target.Buffs.Add(new Buff(150, 600));
                
                TestFactory.CreateMap(character, target);

                context.Process(new Bf
                {
                    EntityType = target.EntityType,
                    EntityId = target.Id,
                    BuffId = 150,
                    Duration = 0
                });

                Check.That(target.Buffs).Not.HasElementThatMatches(x => x.Id == 150);

                context.Verify<EntityRemoveBuffEvent>(x => x.Entity.Equals(target) && x.Buff.Id == 150);
            }
        }
    }
}