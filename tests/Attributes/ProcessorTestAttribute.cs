using System;
using Xunit;

namespace Spark.Tests.Attributes
{
    public class ProcessorTestAttribute : FactAttribute
    {
        public ProcessorTestAttribute(Type packetType) => PacketType = packetType;

        public Type PacketType { get; }
    }
}