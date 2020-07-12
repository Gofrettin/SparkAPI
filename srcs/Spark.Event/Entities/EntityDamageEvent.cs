using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityDamageEvent : IEvent
    {
        public EntityDamageEvent(IClient client, IEntity caster, IEntity target, int skillKey, int damage)
        {
            Client = client;
            Caster = caster;
            Target = target;
            SkillKey = skillKey;
            Damage = damage;
        }

        public IClient Client { get; }
        public IEntity Caster { get; }
        public IEntity Target { get; }
        public int SkillKey { get; }
        public int Damage { get; }
    }
}