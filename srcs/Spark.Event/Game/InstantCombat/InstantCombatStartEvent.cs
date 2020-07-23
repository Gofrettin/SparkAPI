using Spark.Game.Abstraction;

namespace Spark.Event.Game.InstantCombat
{
    public class InstantCombatStartEvent : IEvent
    {
        public IClient Client { get; }

        public InstantCombatStartEvent(IClient client) => Client = client;
    }
}