using System.Threading.Tasks;
using NLog;
using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class InProcessor : PacketProcessor<In>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public InProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override Task Process(IClient client, In packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Warn("Can't process in packet, character map is null");
                return Task.CompletedTask;
            }

            IEntity entity;
            switch (packet.EntityType)
            {
                case EntityType.Monster:
                    entity = new Monster(packet.EntityId, 0);
                    break;
                case EntityType.Npc:
                    entity = new Npc(packet.EntityId, 0);
                    break;
                case EntityType.Player:
                    entity = new Player(packet.EntityId);
                    break;
                case EntityType.MapObject:
                    entity = new MapObject(packet.EntityId, 0);
                    break;
                default:
                    Logger.Error($"Undefined switch clause for entity type {packet.EntityType}");
                    return Task.CompletedTask;
            }

            map.AddEntity(entity);
            _eventPipeline.Emit(new EntitySpawnEvent(map, entity));

            return Task.CompletedTask;
        }
    }
}