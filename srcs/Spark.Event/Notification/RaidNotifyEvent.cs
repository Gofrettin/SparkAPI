using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class RaidListNotifyEvent : IEvent
    {
        public RaidListNotifyEvent(IClient client, string owner)
        {
            Client = client;
            Owner = owner;
        }

        public string Owner { get; }
        public IClient Client { get; }
    }
}