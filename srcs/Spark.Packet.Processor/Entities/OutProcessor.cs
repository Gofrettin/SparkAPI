using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Packet.Processor.Entities
{
    public class OutProcessor : PacketProcessor<Out>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline eventPipeline;

        public OutProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Out packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Warn("Can't process out packet, character map is null");
                return;
            }

            IEntity entity = map.GetEntity(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }

            map.RemoveEntity(entity);
            eventPipeline.Emit(new EntityLeaveEvent(client, map, entity));
        }
    }
}