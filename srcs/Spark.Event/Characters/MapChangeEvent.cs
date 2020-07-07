using Spark.Game;

namespace Spark.Event.Characters
{
    public class MapChangeEvent : IEvent
    {
        public MapChangeEvent(IClient client, Map map)
        {
            Client = client;
            Map = map;
        }

        public IClient Client { get; }
        public Map Map { get; }
    }
}