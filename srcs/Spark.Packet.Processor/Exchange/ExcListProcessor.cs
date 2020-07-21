using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Exchange;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Exchange;

namespace Spark.Packet.Processor.Exchange
{
    public class ExcListProcessor : PacketProcessor<ExcList>
    {
        private readonly IEventPipeline _eventPipeline;

        public ExcListProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, ExcList packet)
        {
            IMap map = client.Character.Map;
            IPlayer player = map.GetEntity<IPlayer>(EntityType.Player, packet.EntityId);

            if (player == null)
            {
                return;
            }

            if (packet.Gold == -1 && packet.BankGold == -1)
            {
                _eventPipeline.Emit(new ExchangeStartedEvent(client, player));
                return;
            }
            
            _eventPipeline.Emit(new ExchangeLockedEvent(client, player, packet.Gold, packet.BankGold));
        }
    }
}