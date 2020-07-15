using System;

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

    public class SimpleEventHandler<T> : IEventHandler where T : IEvent
    {
        private readonly Action<T> _handler;

        public SimpleEventHandler(Action<T> handler) => _handler = handler;

        public Type EventType { get; } = typeof(T);

        public void Handle(IEvent e)
        {
            _handler.Invoke((T)e);
        }
    }
}