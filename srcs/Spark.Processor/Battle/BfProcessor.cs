using System.Linq;
using NLog;
using Spark.Core.Extension;
using Spark.Event;
using Spark.Event.Entities;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Battle;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Battle;

namespace Spark.Processor.Battle
{
    public class BfProcessor : PacketProcessor<Bf>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly IBuffFactory _buffFactory;
        private readonly IEventPipeline _eventPipeline;

        public BfProcessor(IBuffFactory buffFactory, IEventPipeline eventPipeline)
        {
            _buffFactory = buffFactory;
            _eventPipeline = eventPipeline;
        }
        
        protected override void Process(IClient client, Bf packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Error("Character map is null");
                return;
            }

            ILivingEntity entity = map.GetEntity<ILivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Logger.Warn("Can't found entity in map");
                return;
            }

            if (packet.Duration != 0)
            {
                IBuff buff = _buffFactory.CreateBuff(packet.BuffId, packet.Duration);
                if (buff == null)
                {
                    Logger.Warn($"Failed to create buff {packet.BuffId}");
                    return;
                }
            
                entity.Buffs.Add(buff);
            
                _eventPipeline.Emit(new EntityBuffedEvent(client, entity, buff));
            
                Logger.Info($"Buff with id {packet.BuffId} successfully added to entity {entity.EntityType} with id {entity.Id}");
                return;
            }
            
            entity.Buffs.RemoveIf(x => x.Id == packet.BuffId);
            Logger.Info($"Buff with id {packet.BuffId} successfully removed from entity {entity.EntityType} with id {entity.Id}");
        }
    }
}