using Spark.Game.Abstraction;

namespace Spark.Event.Characters
{
    public class MapJoinEvent : IEvent
    {
        public MapJoinEvent(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }
    }
}