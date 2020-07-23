using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NFluent;
using Spark.Core.Extension;
using Spark.Event;
using Spark.Packet;
using Spark.Packet.Processor;
using Spark.Tests.Attributes;
using Spark.Tests.Packet;
using Spark.Tests.Processor;
using Xunit;

namespace Spark.Tests
{
    public class Tests
    {
        public static readonly IEnumerable<object[]> PacketTypes = typeof(IPacket).Assembly.GetImplementingTypes<IPacket>().Select(x => new[] { x });
        public static readonly IEnumerable<object[]> EventTypes = typeof(IEvent).Assembly.GetImplementingTypes<IEvent>().Select(x => new[] { x });

        public static readonly IEnumerable<Type> PacketProcessorsType = typeof(PacketProcessor<>).Assembly.GetTypes()
            .Where(x => x.BaseType != null && x.BaseType != typeof(object))
            .Select(x => x.BaseType)
            .Where(x => x.IsParticularGeneric(typeof(PacketProcessor<>)))
            .Select(x => x.GenericTypeArguments[0]);
        
        public static readonly IEnumerable<Type> PacketCreatorsType = typeof(PacketProcessor<>).Assembly.GetTypes()
            .Where(x => x.BaseType != null && x.BaseType != typeof(object))
            .Select(x => x.BaseType)
            .Where(x => x.IsParticularGeneric(typeof(PacketProcessor<>)))
            .Select(x => x.GenericTypeArguments[0]);

        public static readonly IEnumerable<Type> PacketTests = typeof(PacketTests).Assembly.GetTypes()
            .SelectMany(x => x.GetMethods())
            .Select(x => x.GetCustomAttribute<PacketTestAttribute>()?.PacketType)
            .Where(x => x != null);

        public static readonly IEnumerable<Type> ProcessorTests = typeof(ProcessorTests).Assembly.GetTypes()
            .SelectMany(x => x.GetMethods())
            .Select(x => x.GetCustomAttribute<ProcessorTestAttribute>()?.PacketType)
            .Where(x => x != null);

        public static readonly IEnumerable<Type> EventTests = typeof(ProcessorTests).Assembly.GetTypes()
            .SelectMany(x => x.GetMethods())
            .SelectMany(x => x.GetCustomAttributes<EventTestAttribute>())
            .Select(x => x.EventType);

        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void All_Packet_Have_Processor(Type type)
        {
            Check.WithCustomMessage($"Missing processor for {type.Name} packet").That(PacketProcessorsType).Contains(type);
        }

        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void All_Packet_Have_Test(Type type)
        {
            Check.WithCustomMessage($"Missing packet test for {type.Name} packet").That(PacketTests).Contains(type);
        }

        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void All_Packet_Processor_Have_Test(Type type)
        {
            Check.WithCustomMessage($"Missing processor test for {type.Name} packet").That(ProcessorTests).Contains(type);
        }

        [Theory]
        [MemberData(nameof(EventTypes))]
        public void All_Event_Have_Test(Type type)
        {
            Check.WithCustomMessage($"Missing event test for {type.Name} event").That(EventTests).Contains(type);
        }

        [Theory]
        [MemberData(nameof(PacketTypes))]
        public void All_Packet_Have_Creator(Type type)
        {
            Check.WithCustomMessage($"Missing PacketCreator for packet {type.Name}").That(PacketCreatorsType).Contains(type);
        }
    }
}