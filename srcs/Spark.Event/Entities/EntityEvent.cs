using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public abstract class EntityEvent : IEvent
    {
        protected EntityEvent(IClient client, IEntity entity)
        {
            Client = client;
            Entity = entity;
        }

        public IEntity Entity { get; }
        public IClient Client { get; }
    }
}