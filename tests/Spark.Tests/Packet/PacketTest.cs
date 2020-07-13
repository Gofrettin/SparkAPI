using NFluent;
using Spark.Packet;
using Xunit;

// Required to avoid concurrency exception from NFluent.Helpers.ExceptionHelper.Constructors dictionary call...
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Spark.Tests.Packet
{
    public abstract class PacketTest<T>
    {
        protected abstract string Packet { get; }
        protected abstract T Excepted { get; }

        private IPacketFactory Factory { get; } = new PacketFactory();

        [Fact]
        public void Execute()
        {
            IPacket typedPacket = Factory.CreatePacket(Packet);

            Check.That(typedPacket).IsNotNull();
            Check.That(typedPacket).IsInstanceOf<T>();
            
            CheckPacket((T)typedPacket);
            
            Check.That(typedPacket).HasFieldsWithSameValues(Excepted);
        }

        protected virtual void CheckPacket(T packet)
        {
            
        }
    }
}