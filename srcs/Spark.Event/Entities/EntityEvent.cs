using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public abstract class EntityEvent : IEvent
    {
        public IClient Client { get; }
        public IEntity Entity { get; }

        protected EntityEvent(IClient client, IEntity entity)
        {
            Client = client;
            Entity = entity;
        }
    }
}