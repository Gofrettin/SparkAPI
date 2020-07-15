using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Exchange
{
    public class ExchangeStartedEvent : IEvent
    {
        public IClient Client { get; }
        public IPlayer Player { get; }
        
        public ExchangeStartedEvent(IClient client, IPlayer player)
        {
            Client = client;
            Player = player;
        }
    }
}