using System;

namespace Spark.Event
{
    public class SimpleEventHandler<T> : IEventHandler where T : IEvent
    {
        private readonly Action<T> handler;

        public SimpleEventHandler(Action<T> handler) => this.handler = handler;

        public Type EventType { get; } = typeof(T);

        public void Handle(IEvent e)
        {
            handler.Invoke((T)e);
        }
    }
}