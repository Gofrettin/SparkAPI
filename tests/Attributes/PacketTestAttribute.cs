using System;
using Xunit;

namespace Spark.Tests.Attributes
{
    public class PacketTestAttribute : FactAttribute
    {
        public Type PacketType { get; }

        public PacketTestAttribute(Type packetType)
        {
            PacketType = packetType;
        }
    }
}