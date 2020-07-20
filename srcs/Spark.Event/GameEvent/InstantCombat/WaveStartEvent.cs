using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class WaveStartEvent : IEvent
    {
        public IClient Client { get; }

        public WaveStartEvent(IClient client) => Client = client;
    }
}