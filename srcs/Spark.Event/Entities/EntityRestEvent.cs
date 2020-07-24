using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityRestEvent : IEvent
    {
        public EntityRestEvent(IClient client, ILivingEntity entity)
        {
            Client = client;
            Entity = entity;
        }

        public IClient Client { get; }
        public ILivingEntity Entity { get; }
    }
}