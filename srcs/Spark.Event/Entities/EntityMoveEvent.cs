using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : EntityEvent
    {
        public EntityMoveEvent(IEntity entity, Vector2D from, Vector2D to)
        {
            From = from;
            To = to;
        }
        
        public IEntity Entity { get; }
        public Vector2D From { get; }
        public Vector2D To { get; }
    }
}