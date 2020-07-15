using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using Spark.Core.Extension;
using Spark.Packet;
using Spark.Processor;
using Spark.Tests.Packet;
using Spark.Tests.Processor;
using Xunit;

namespace Spark.Tests
{
    public class GlobalTests
    {
        public static readonly IEnumerable<object[]> PacketTypes = typeof(IPacket).Assembly.GetImplementingTypes<IPacket>().Select(x => new []{x});
        
        public static readonly IEnumerable<Type> PacketProcessorsType = typeof(PacketProcessor<>).Assembly.GetTypes()
            .Where(x => x.BaseType != null && x.BaseType != typeof(object))
            .Select(x => x.BaseType)
            .Where(x => x.IsParticularGeneric(typeof(PacketProcessor<>)))
            .Select(x => x.GenericTypeArguments[0]);
        
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

        
        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void Packet_Have_Processor(Type type)
        {
            Check.WithCustomMessage($"Missing processor for {type.Name} packet").That(PacketProcessorsType).Contains(type);
        }
        
        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void Packet_Have_Test(Type type)
        {
            Check.WithCustomMessage($"Missing packet test for {type.Name} packet").That(PacketTests).Contains(type);
        }
        
        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void Packet_Have_Processor_Test(Type type)
        {
            Check.WithCustomMessage($"Missing processor test for {type.Name} packet").That(ProcessorTests).Contains(type);
        }
    }
}