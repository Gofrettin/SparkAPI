using System;
using Xunit;

namespace Spark.Tests.Attributes
{
    public class ProcessorTestAttribute : FactAttribute
    {
        public Type PacketType { get; }

        public ProcessorTestAttribute(Type packetType)
        {
            PacketType = packetType;
        }
    }
}