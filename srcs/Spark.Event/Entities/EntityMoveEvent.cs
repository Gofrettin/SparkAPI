using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : IEvent
    {
        public EntityMoveEvent(IEntity entity) => Entity = entity;

        public IEntity Entity { get; }
    }
}