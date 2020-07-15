using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Notification
{
    public class ExchangeRequestEvent : IEvent
    {
        public IClient Client { get; }
        public IPlayer Sender { get; }

        public ExchangeRequestEvent(IClient client, IPlayer sender)
        {
            Client = client;
            Sender = sender;
        }
    }
}