using Spark.Core;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : IEvent
    {
        public EntityMoveEvent(IEntity entity, Position from, Position to)
        {
            Entity = entity;
            From = from;
            To = to;
        }
        
        public IEntity Entity { get; }
        public Position From { get; }
        public Position To { get; }
    }
}