using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Battle;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class EntityBuffedEvent : IEvent
    {
        public IClient Client { get; }
        public ILivingEntity Entity { get; }
        public IBuff Buff { get; }
        
        public EntityBuffedEvent(IClient client, ILivingEntity entity, IBuff buff)
        {
            Client = client;
            Entity = entity;
            Buff = buff;
        }
    }
}