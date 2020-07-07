using System;
using Spark.Game;
using Spark.Packet;

namespace Spark.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        void Handle(IClient client, IPacket packet);
    }

    public abstract class PacketProcessor<TPacket> : IPacketProcessor where TPacket : IPacket
    {
        public Type PacketType { get; } = typeof(TPacket);

        public void Handle(IClient client, IPacket packet)
        {
            Process(client, (TPacket)packet);
        }

        protected abstract void Process(IClient client, TPacket packet);
    }
}