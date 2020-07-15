using System;

namespace Spark.Event
{
    public interface IEventHandler
    {
        Type EventType { get; }
        void Handle(IEvent e);
    }
}