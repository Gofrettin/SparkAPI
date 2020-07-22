using Spark.Game.Abstraction;

namespace Spark.Event.Login
{
    public class GameStartEvent : IEvent
    {
        public IClient Client { get; }

        public GameStartEvent(IClient client) => Client = client;
    }
}