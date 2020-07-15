using Spark.Game.Abstraction;

namespace Spark.Event.Characters
{
    public class MapLeaveEvent : IEvent
    {
        public MapLeaveEvent(IClient client, IMap map)
        {
            Client = client;
            Map = map;
        }

        public IMap Map { get; }

        public IClient Client { get; }
    }
}