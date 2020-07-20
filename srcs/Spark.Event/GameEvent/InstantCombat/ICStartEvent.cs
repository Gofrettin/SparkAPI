using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class ICStartEvent : IEvent
    {
        public IClient Client { get; }

        public ICStartEvent(IClient client) => Client = client;
    }
}