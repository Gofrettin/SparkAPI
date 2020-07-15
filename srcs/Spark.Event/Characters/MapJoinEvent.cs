using Spark.Game.Abstraction;

namespace Spark.Event.Characters
{
    public class MapJoinEvent : IEvent
    {
        public MapJoinEvent(IClient client, IMap map)
        {
            Client = client;
            Map = map;
        }

        public IMap Map { get; }

        public IClient Client { get; }
    }
}