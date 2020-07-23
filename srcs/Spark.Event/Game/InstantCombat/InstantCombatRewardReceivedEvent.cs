using Spark.Game.Abstraction;

namespace Spark.Event.Game.InstantCombat
{
    public class InstantCombatRewardReceivedEvent : IEvent
    {
        public IClient Client { get; }

        public InstantCombatRewardReceivedEvent(IClient client) => Client = client;
    }
}