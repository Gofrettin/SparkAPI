using Spark.Game.Abstraction;

namespace Spark.Event.GameEvent.InstantCombat
{
    public class WaveComingEvent : IEvent
    {
        public WaveComingEvent(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }
    }
}