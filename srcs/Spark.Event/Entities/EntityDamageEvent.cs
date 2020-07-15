using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityDamageEvent : IEvent
    {
        public EntityDamageEvent(IClient client, ILivingEntity caster, ILivingEntity target, int skillKey, int damage)
        {
            Client = client;
            Caster = caster;
            Target = target;
            SkillKey = skillKey;
            Damage = damage;
        }

        public ILivingEntity Caster { get; }
        public ILivingEntity Target { get; }
        public int SkillKey { get; }
        public int Damage { get; }

        public IClient Client { get; }
    }
}