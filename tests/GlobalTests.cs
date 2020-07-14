using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Spark.Core.Extension;
using Spark.Packet;
using Spark.Tests.Packet;
using Spark.Tests.Processor;
using Xunit;

namespace Spark.Tests
{
    public class GlobalTests
    {
        public static readonly IEnumerable<Type> PacketTypes = typeof(IPacket).Assembly.GetImplementingTypes<IPacket>();
        
        public static readonly IEnumerable<Type> PacketTests = typeof(PacketTest<>).Assembly.GetTypes()
            .Where(x => x.BaseType != null && x.BaseType != typeof(object))
            .Select(x => x.BaseType)
            .Where(x => x.IsParticularGeneric(typeof(PacketTest<>)))
            .Select(x => x.GenericTypeArguments[0]);
        
        public static readonly IEnumerable<Type> ProcessorTests = typeof(ProcessorTest<>).Assembly.GetTypes()
            .Where(x => x.BaseType != null && x.BaseType != typeof(object))
            .Select(x => x.BaseType)
            .Where(x => x.IsParticularGeneric(typeof(ProcessorTest<>)))
            .Select(x => x.GenericTypeArguments[0]);
        
        
        [Fact]
        public void Packet_Have_Unit_Test()
        {
            foreach (Type packetType in PacketTypes)
            {
                Check.WithCustomMessage($"Missing packet test for {packetType.Name}").That(PacketTests).Contains(packetType);
            }
        }

        [Fact]
        public void Packet_Have_Processor_Unit_Test()
        {
            foreach (Type packetType in PacketTypes)
            {
                Check.WithCustomMessage($"Missing processor test for {packetType.Name}").That(ProcessorTests).Contains(packetType);
            }
        }
    }
}