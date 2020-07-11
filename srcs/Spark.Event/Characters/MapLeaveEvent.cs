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

        public IClient Client { get; }
        public IMap Map { get; }
    }
}