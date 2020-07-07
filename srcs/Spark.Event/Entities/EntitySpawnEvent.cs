using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntitySpawnEvent : IEvent
    {
        public EntitySpawnEvent(IMap map, IEntity entity)
        {
            Map = map;
            Entity = entity;
        }

        public IMap Map { get; }
        public IEntity Entity { get; }
    }
}