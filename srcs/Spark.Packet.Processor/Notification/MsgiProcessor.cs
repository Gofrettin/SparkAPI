using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.GameEvent.InstantCombat;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Notification
{
    public class MsgiProcessor : PacketProcessor<Msgi>
    {
        private readonly IEventPipeline _eventPipeline;

        public MsgiProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Msgi packet)
        {
            _eventPipeline.Emit(new ServerMessageReceivedEvent(client, packet.MessageId, packet.MessageType));
            
            if (packet.MessageType == MessageType.Classic)
            {
                if (packet.MessageId == 1287)
                {
                    _eventPipeline.Emit(new WaveComingEvent(client));    
                }
                
                if (packet.MessageId == 387)
                {
                    _eventPipeline.Emit(new ICStartEvent(client));
                }

                if (packet.MessageId == 384)
                {
                    _eventPipeline.Emit(new WaveStartSoonEvent(client));
                }
            }
        }
    }
}