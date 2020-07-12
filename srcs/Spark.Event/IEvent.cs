using Spark.Game.Abstraction;

namespace Spark.Event
{
    public interface IEvent
    {
        public IClient Client { get; }
    }
}