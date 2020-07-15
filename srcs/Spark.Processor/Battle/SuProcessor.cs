using System.Linq;
using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Battle;

namespace Spark.Processor.Battle
{
    public class SuProcessor : PacketProcessor<Su>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventPipeline _eventPipeline;

        public SuProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Su packet)
        {
            IMap map = client.Character.Map;

            ILivingEntity caster = map.GetEntity<ILivingEntity>(packet.CasterType, packet.CasterId);
            ILivingEntity target = map.GetEntity<ILivingEntity>(packet.TargetType, packet.TargetId);

            if (caster is ICharacter character)
            {
                ISkill skill = character.Skills.FirstOrDefault(x => x.SkillKey == packet.SkillKey);
                if (skill != null)
                {
                    skill.IsOnCooldown = true;
                }
            }

            if (target == null || caster == null)
            {
                Logger.Warn("Can't found target or caster in map");
                return;
            }

            target.HpPercentage = packet.HpPercentage;

            _eventPipeline.Emit(new EntityDamageEvent(client, caster, target, packet.SkillKey, packet.Damage));

            if (packet.IsTargetAlive)
            {
                Logger.Info($"{target.EntityType} {target.Id} is alive");
                return;
            }

            Logger.Info($"Entity {target.EntityType} with id {target.Id} died");
            map.RemoveEntity(target);

            _eventPipeline.Emit(new EntityDeathEvent(client, target, caster));
        }
    }
}