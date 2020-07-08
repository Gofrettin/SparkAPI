using System.Threading.Tasks;
using NLog;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class MvProcessor : PacketProcessor<Mv>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public MvProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override Task Process(IClient client, Mv packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Error("Can't process in packet, character map is null");
                return Task.CompletedTask;
            }

            IEntity entity = map.GetEntity(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Logger.Debug($"Can't found entity {packet.EntityType} with id {packet.EntityId} in map {map.Id}");
                return Task.CompletedTask;
            }

            entity.Position = packet.Position;

            _eventPipeline.Emit(new EntityMoveEvent(entity));
            Logger.Trace($"Entity {entity.EntityType} with id {entity.Id} moved to {entity.Position.X}:{entity.Position.Y}");

            return Task.CompletedTask;
        }
    }
}