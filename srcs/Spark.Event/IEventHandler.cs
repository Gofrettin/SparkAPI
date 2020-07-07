using System;
using System.Threading.Tasks;

namespace Spark.Event
{
    public interface IEventHandler
    {
        Type EventType { get; }
        Task Handle(IEvent e);
    }

    public abstract class EventHandler<T> : IEventHandler where T : IEvent
    {
        public Type EventType { get; } = typeof(T);

        public Task Handle(IEvent e) => Handle((T)e);

        protected abstract Task Handle(T e);
    }
}