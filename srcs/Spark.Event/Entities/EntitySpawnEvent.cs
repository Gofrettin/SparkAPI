using Spark.Game;
using Spark.Game.Entities;

namespace Spark.Event.Entities
{
    public class EntitySpawnEvent : IEvent
    {
        public EntitySpawnEvent(Map map, Entity entity)
        {
            Map = map;
            Entity = entity;
        }

        public Map Map { get; }
        public Entity Entity { get; }
    }
}