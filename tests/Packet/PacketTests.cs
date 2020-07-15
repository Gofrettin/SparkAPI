using NFluent;
using Spark.Packet;

namespace Spark.Tests.Packet
{
    public abstract class PacketTests
    {
        private IPacketFactory Factory { get; } = new PacketFactory();

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