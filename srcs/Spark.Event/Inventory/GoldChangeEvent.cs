using Spark.Game.Abstraction;

namespace Spark.Event.Inventory
{
    public class GoldChangeEvent : IEvent
    {
        public IClient Client { get; }
        public int Gold { get; }

        public GoldChangeEvent(IClient client, int gold)
        {
            Client = client;
            Gold = gold;
        }
    }
}