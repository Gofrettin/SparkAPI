using System;
using Spark.Game.Abstraction;
using Spark.Packet;

namespace Spark.Packet.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        void Process(IClient client, IPacket packet);
    }

    public abstract class PacketProcessor<TPacket> : IPacketProcessor where TPacket : IPacket
    {
        public Type PacketType { get; } = typeof(TPacket);

        public void Process(IClient client, IPacket packet) => Process(client, (TPacket)packet);

        protected abstract void Process(IClient client, TPacket packet);
    }
}