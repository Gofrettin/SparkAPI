using Spark.Game.Abstraction;

namespace Spark.Event.Exchange
{
    public class ExchangeCompletedEvent : IEvent
    {
        public IClient Client { get; }

        public ExchangeCompletedEvent(IClient client) => Client = client;
    }
}