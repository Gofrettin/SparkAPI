using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class RaidNotifyEvent : IEvent
    {
        public IClient Client { get; }
        public string Owner { get; }

        public RaidNotifyEvent(IClient client, string owner)
        {
            Client = client;
            Owner = owner;
        }
    }
}