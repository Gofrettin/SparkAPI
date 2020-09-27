using NLog;
using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Entities;

namespace Spark.Packet.Processor.Entities
{
    public class InProcessor : PacketProcessor<In>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityFactory entityFactory;

        private readonly IEventPipeline eventPipeline;

        public InProcessor(IEventPipeline eventPipeline, IEntityFactory entityFactory)
        {
            this.eventPipeline = eventPipeline;
            this.entityFactory = entityFactory;
        }

        protected override void Process(IClient client, In packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Warn("Can't process in packet, character map is null");
                return;
            }

            IEntity entity;
            switch (packet.EntityType)
            {
                case EntityType.Monster:
                    entity = entityFactory.CreateMonster(packet.EntityId, packet.GameKey);
                    break;
                case EntityType.Npc:
                    entity = entityFactory.CreateNpc(packet.EntityId, packet.GameKey);
                    break;
                case EntityType.Player:
                    entity = entityFactory.CreatePlayer(packet.EntityId, packet.Name);
                    break;
                case EntityType.MapObject:
                    entity = entityFactory.CreateMapObject(packet.EntityId, packet.GameKey, packet.MapObject.Amount);
                    break;
                default:
                    Logger.Error($"Undefined switch clause for entity type {packet.EntityType}");
                    return;
            }

            if (entity == null)
            {
                Logger.Error($"Failed to create entity {packet.EntityType} {packet.EntityId}");
                return;
            }
            
            entity.Position = packet.Position;
            if (entity is ILivingEntity livingEntity)
            {
                livingEntity.Direction = packet.Direction;

                if (entity is IPlayer player)
                {
                    player.HpPercentage = packet.Player.HpPercentage;
                    player.MpPercentage = packet.Player.MpPercentage;
                    player.Gender = packet.Player.Gender;
                    player.Class = packet.Player.Class;
                }
                else
                {
                    livingEntity.HpPercentage = packet.Npc.HpPercentage > 100 ? 100 : packet.Npc.HpPercentage;
                    livingEntity.MpPercentage = packet.Npc.MpPercentage > 100 ? 100 : packet.Npc.MpPercentage;
                }
            }

            map.AddEntity(entity);

            eventPipeline.Emit(new EntitySpawnEvent(client, map, entity));
        }
    }
}