using NLog;
using Spark.Core.Enum;
using Spark.Event;
using Spark.Event.Notification;
using Spark.Game.Abstraction;
using Spark.Packet.Notification;

namespace Spark.Packet.Processor.Notification
{
    public class QNamli2Processor : PacketProcessor<QNamli2>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public QNamli2Processor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, QNamli2 packet)
        {
            if (packet.Raid != null)
            {
                _eventPipeline.Emit(new NotificationReceivedEvent(NotificationType.Raid, client, packet.Raid));
            }
        }
    }
}