using Spark.Game.Abstraction;

namespace Spark.Event.Game.InstantCombat
{
    public class InstantCombatWaveComingEvent : IEvent
    {
        public InstantCombatWaveComingEvent(IClient client) => Client = client;

        public IClient Client { get; }
    }
}