using System;

namespace Spark.Event
{
    public abstract class EventHandler<T> : IEventHandler where T : IEvent
    {
        public Type EventType { get; } = typeof(T);

        public void Handle(IEvent e)
        {
            Handle((T)e);
        }

        protected abstract void Handle(T e);
    }
}