using Spark.Event;
using Spark.Event.Game.InstantCombat;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Chat;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Chat
{
    public class SayiProcessor : PacketProcessor<Sayi>
    {
        private readonly IEventPipeline eventPipeline;

        public SayiProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Sayi packet)
        {
            eventPipeline.Emit(new ChatMessageReceivedEvent(client, packet.MessageId, packet.Color));
            
            if (packet.MessageId == 2282)
            {
                eventPipeline.Emit(new InstantCombatRewardUnreceivedEvent(client));    
            }

            if (packet.MessageId == 2367)
            {
                eventPipeline.Emit(new InstantCombatRewardReceivedEvent(client));
            }
        }
    }
}