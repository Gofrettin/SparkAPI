using Spark.Game;
using Spark.Game.Abstraction;

namespace Spark.Event.Characters
{
    public class MapChangeEvent : IEvent
    {
        public MapChangeEvent(IClient client, IMap map)
        {
            Client = client;
            Map = map;
        }

        public IClient Client { get; }
        public IMap Map { get; }
    }
}