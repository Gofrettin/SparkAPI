using Spark.Event;
using Spark.Event.GameEvent.InstantCombat;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Chat;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Chat
{
    public class SayiProcessor : PacketProcessor<Sayi>
    {
        private readonly IEventPipeline _eventPipeline;

        public SayiProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Sayi packet)
        {
            _eventPipeline.Emit(new ChatMessageReceivedEvent(client, packet.MessageId, packet.Color));
            
            if (packet.MessageId == 2282)
            {
                _eventPipeline.Emit(new RewardUnreceivedEvent(client));    
            }

            if (packet.MessageId == 2367)
            {
                _eventPipeline.Emit(new RewardReceivedEvent(client));
            }
        }
    }
}