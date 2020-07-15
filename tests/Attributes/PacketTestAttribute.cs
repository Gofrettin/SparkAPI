using System;
using Xunit;

namespace Spark.Tests.Attributes
{
    public class PacketTestAttribute : FactAttribute
    {
        public PacketTestAttribute(Type packetType) => PacketType = packetType;

        public Type PacketType { get; }
    }
}