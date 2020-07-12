using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityMoveEvent : EntityEvent
    {
        public EntityMoveEvent(IClient client, IEntity entity, Vector2D from, Vector2D to) : base(client, entity)
        {
            From = from;
            To = to;
        }
        
        public Vector2D From { get; }
        public Vector2D To { get; }
    }
}