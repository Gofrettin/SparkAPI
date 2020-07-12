using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityLeaveEvent : EntityEvent
    {
        public EntityLeaveEvent(IClient client, IMap map, IEntity entity) : base(client, entity)
        {
            Map = map;
        }

        public IMap Map { get; }
    }
}