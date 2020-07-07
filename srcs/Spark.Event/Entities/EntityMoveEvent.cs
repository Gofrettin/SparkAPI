using Spark.Game.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : IEvent
    {
        public EntityMoveEvent(Entity entity) => Entity = entity;

        public Entity Entity { get; }
    }
}