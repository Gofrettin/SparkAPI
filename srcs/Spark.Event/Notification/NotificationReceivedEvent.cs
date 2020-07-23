using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Packet.Notification;

namespace Spark.Event.Notification
{
    public class NotificationReceivedEvent : IEvent
    {
        public NotificationType NotificationType { get; }
        public IClient Client { get; }
        public QNamli2.RaidInfo RaidInfo { get; }

        public NotificationReceivedEvent(NotificationType notificationType, IClient client)
        {
            NotificationType = notificationType;
            Client = client;
        }
        
        public NotificationReceivedEvent(NotificationType notificationType, IClient client, QNamli2.RaidInfo raidInfo)
        {
            NotificationType = notificationType;
            Client = client;
            RaidInfo = raidInfo;
        }
    }
}