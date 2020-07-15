using System;

namespace Spark.Event
{
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