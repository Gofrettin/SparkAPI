using Spark.Event;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Notification
{
    public class QNamliProcessor : PacketProcessor<QNamli>
    {
        private readonly IEventPipeline _eventPipeline;

        public QNamliProcessor(IEventPipeline eventPipeline)
        {
            _eventPipeline = eventPipeline;
        }
        
        protected override void Process(IClient client, QNamli packet)
        {
            if (packet.Request.Equals("#guri^506"))
            {
                _eventPipeline.Emit(new ICNotifyEvent(client));
            }
        }
    }
}