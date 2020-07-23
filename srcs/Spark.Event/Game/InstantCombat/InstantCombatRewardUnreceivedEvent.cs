using Spark.Game.Abstraction;

namespace Spark.Event.Game.InstantCombat
{
    public class InstantCombatRewardUnreceivedEvent : IEvent
    {
        public IClient Client { get; }

        public InstantCombatRewardUnreceivedEvent(IClient client) => Client = client;
    }
}