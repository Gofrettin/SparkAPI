using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class WaveStartSoonEvent : IEvent
    {
        public IClient Client { get; }

        public WaveStartSoonEvent(IClient client) => Client = client;
    }
}