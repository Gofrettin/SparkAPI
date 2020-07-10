using System;
using System.Threading.Tasks;

namespace Spark.Event
{
    public interface IEventHandler
    {
        Type EventType { get; }
        void Handle(IEvent e);
    }

    public abstract class EventHandler<T> : IEventHandler where T : IEvent
    {
        public Type EventType { get; } = typeof(T);

        public void Handle(IEvent e) => Handle((T)e);

        protected abstract void Handle(T e);
    }
}