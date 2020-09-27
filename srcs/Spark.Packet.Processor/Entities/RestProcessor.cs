using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Packet.Processor.Entities
{
    public class RestProcessor : PacketProcessor<Rest>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventPipeline eventPipeline;

        public RestProcessor(IEventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }
        
        protected override void Process(IClient client, Rest packet)
        {
            IMap map = client.Character.Map;
            ILivingEntity entity = map.GetEntity<ILivingEntity>(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                Logger.Debug($"Can't found entity {packet.EntityType} with id {packet.EntityId}");
                return;
            }

            entity.IsResting = packet.IsResting;
            eventPipeline.Emit(new EntityRestEvent(client, entity));
        }
    }
}