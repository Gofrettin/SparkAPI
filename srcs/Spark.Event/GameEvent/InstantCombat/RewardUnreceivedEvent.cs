using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class RewardUnreceivedEvent : IEvent
    {
        public IClient Client { get; }

        public RewardUnreceivedEvent(IClient client)
        {
            Client = client;
        }
    }
}