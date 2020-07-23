using Spark.Core.Enum;
using Spark.Event.Notification;
using Spark.Packet.Notification;
using Spark.Packet.Processor.Notification;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class NotificationProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(QNamli2Processor))]
        [EventTest(typeof(NotificationReceivedEvent))]
        public void QNamli2_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new QNamli2
                {
                    Raid = new QNamli2.RaidInfo
                    {
                        Owner = "MyNameIs"
                    }
                });

                context.IsEventEmitted<NotificationReceivedEvent>(x => x.NotificationType == NotificationType.Raid);
            }
        }
        
        [ProcessorTest(typeof(QNamliProcessor))]
        [EventTest(typeof(NotificationReceivedEvent))]
        public void QNamli_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new QNamli
                {
                    Request = "#guri^506"
                });
                
                context.IsEventEmitted<NotificationReceivedEvent>(x => x.NotificationType == NotificationType.InstantCombat);
            }
        }
    }
}