using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Battle;

namespace Spark.Packet.Processor.Battle
{
    public class DieProcessor : PacketProcessor<Die>
    {
        private readonly IEventPipeline eventPipeline;

        public DieProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Die packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                return;
            }

            ILivingEntity entity = map.GetEntity<ILivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }

            entity.HpPercentage = 0;
            map.RemoveEntity(entity);
            
            eventPipeline.Emit(new EntityDeathEvent(client, entity, entity));
        }
    }
}