using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityDeathEvent : EntityEvent
    {
        public EntityDeathEvent(IClient client, IEntity entity, IEntity killer) : base(client, entity) => Killer = killer;

        public IEntity Killer { get; }
    }
}