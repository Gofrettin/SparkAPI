using Spark.Game.Abstraction;

namespace Spark.Event.Game.InstantCombat
{
    public class InstantCombatWaveStartSoonEvent : IEvent
    {
        public IClient Client { get; }

        public InstantCombatWaveStartSoonEvent(IClient client) => Client = client;
    }
}