using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Game.InstantCombat;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Chat;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Chat
{
    public class MsgiProcessor : PacketProcessor<Msgi>
    {
        private readonly IEventPipeline eventPipeline;

        public MsgiProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Msgi packet)
        {
            eventPipeline.Emit(new ServerMessageReceivedEvent(client, packet.MessageId, packet.MessageType));
            
            if (packet.MessageType == MessageType.Classic)
            {
                if (packet.MessageId == 1287)
                {
                    eventPipeline.Emit(new InstantCombatWaveComingEvent(client));    
                }
                
                if (packet.MessageId == 387)
                {
                    eventPipeline.Emit(new InstantCombatStartEvent(client));
                }

                if (packet.MessageId == 384)
                {
                    eventPipeline.Emit(new InstantCombatWaveStartSoonEvent(client));
                }
            }
        }
    }
}