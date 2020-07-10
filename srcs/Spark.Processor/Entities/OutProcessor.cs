using System.Threading.Tasks;
using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class OutProcessor : PacketProcessor<Out>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public OutProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

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
                Logger.Warn($"Can't found entity {packet.EntityType} with id {packet.EntityId} in map");
                return;
            }

            map.RemoveEntity(entity);
            _eventPipeline.Emit(new EntityLeaveEvent(map, entity));

            return;
        }
    }
}