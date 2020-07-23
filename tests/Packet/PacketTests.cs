using System;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Spark.Extension;
using Spark.Packet;
using Spark.Packet.Factory;

namespace Spark.Tests.Packet
{
    public abstract class PacketTests
    {
        private IPacketFactory Factory { get; }

        protected PacketTests()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddImplementingTypes<IPacketCreator>();
            services.AddSingleton<IPacketFactory, PacketFactory>();

            IServiceProvider provider = services.BuildServiceProvider();
            Factory = provider.GetService<IPacketFactory>();
        }

        private T Create<T>(string packet) where T : IPacket
        {
            IPacket typedPacket = Factory.CreatePacket(packet);

            Check.That(typedPacket).IsInstanceOf<T>();

            return (T)typedPacket;
        }

        protected T CreateAndCheckValues<T>(string packet, T target) where T : IPacket
        {
            T typedPacket = Create<T>(packet);
            Check.That(typedPacket).HasFieldsWithSameValues(target);

            return typedPacket;
        }
    }
}