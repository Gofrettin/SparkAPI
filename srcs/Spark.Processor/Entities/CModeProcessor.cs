using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class CModeProcessor : PacketProcessor<CMode>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public CModeProcessor(IEventPipeline eventPipeline)
        {
            _eventPipeline = eventPipeline;
        }
        
        protected override void Process(IClient client, CMode packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                return;
            }

            IEntity entity = map.GetEntity(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Logger.Warn($"Can't found entity {packet.EntityType} with id {packet.EntityId} in map {map.Id}");
                return;
            }

            if (entity.Equals(client.Character))
            {
                return;
            }

            if (packet.SpecialistId > 0 && packet.SpecialistId <= 34)
            {
                _eventPipeline.Emit(new SpecialistWearEvent(client, entity, packet.SpecialistId));
                Logger.Info($"Entity {entity.EntityType} with id {entity.Id} wear SP {packet.SpecialistId}");
            }

            if (packet.SpecialistId == 0)
            {
                _eventPipeline.Emit(new SpecialistUnwearEvent(client, entity));
                Logger.Info($"Entity {entity.EntityType} with id {entity.Id} removed SP");
            }
        }
    }
}