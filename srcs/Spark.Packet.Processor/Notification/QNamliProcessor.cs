using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Notification
{
    public class QNamliProcessor : PacketProcessor<QNamli>
    {
        private readonly IEventPipeline eventPipeline;

        public QNamliProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, QNamli packet)
        {
            if (packet.Request.Equals("#guri^506"))
            {
                eventPipeline.Emit(new NotificationReceivedEvent(NotificationType.InstantCombat, client));
            }
        }
    }
}