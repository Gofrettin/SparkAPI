using Spark.Game;
using Spark.Game.Entities;

namespace Spark.Event.Entities
{
    public class EntityLeaveEvent : IEvent
    {
        public EntityLeaveEvent(Map map, Entity entity)
        {
            Map = map;
            Entity = entity;
        }

        public Map Map { get; }
        public Entity Entity { get; }
    }
}