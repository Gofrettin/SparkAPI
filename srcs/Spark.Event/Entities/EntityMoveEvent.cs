using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : EntityEvent
    {
        public EntityMoveEvent(IClient client, IEntity entity, Position from, Position to) : base(client, entity)
        {
            From = from;
            To = to;
        }
        
        public Position From { get; }
        public Position To { get; }
    }
}