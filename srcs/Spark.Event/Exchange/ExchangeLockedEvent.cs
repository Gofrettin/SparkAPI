using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Exchange
{
    public class ExchangeLockedEvent : IEvent
    {
        public IClient Client { get; }
        public IPlayer Player { get; }
        
        public int Gold { get; }
        public int BankGold { get; }

        public ExchangeLockedEvent(IClient client, IPlayer player, int gold, int bankGold)
        {
            Client = client;
            Player = player;
            Gold = gold;
            BankGold = bankGold;
        }
    }
}