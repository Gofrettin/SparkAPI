using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class ICNotifyEvent : IEvent
    {
        public IClient Client { get; }

        public ICNotifyEvent(IClient client)
        {
            Client = client;
        }
    }
}