using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class RewardReceivedEvent : IEvent
    {
        public IClient Client { get; }

        public RewardReceivedEvent(IClient client) => Client = client;
    }
}