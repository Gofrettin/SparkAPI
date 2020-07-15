using System;

namespace Spark.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventTestAttribute : Attribute
    {
        public EventTestAttribute(Type eventType) => EventType = eventType;
        public Type EventType { get; }
    }
}