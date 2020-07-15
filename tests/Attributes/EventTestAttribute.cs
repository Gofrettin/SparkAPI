using System;
using Xunit;

namespace Spark.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventTestAttribute : Attribute
    {
        public Type EventType { get; }

        public EventTestAttribute(Type eventType) => EventType = eventType;
    }
}