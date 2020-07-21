using NLog;
using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Notification
{
    public class Dlgi2Processor : PacketProcessor<Dlgi2>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventPipeline _eventPipeline;

        public Dlgi2Processor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Dlgi2 packet)
        {
            if (packet.ExchangeRequest != null)
            {
                IMap map = client.Character.Map;
                IPlayer player = map.GetEntity<IPlayer>(EntityType.Player, packet.ExchangeRequest.PlayerId);

                if (player == null)
                {
                    Logger.Warn($"Can't found player with id {packet.ExchangeRequest.PlayerId}");
                    return;
                }
                
                _eventPipeline.Emit(new ExchangeRequestEvent(client, player));
            }
        }
    }
}