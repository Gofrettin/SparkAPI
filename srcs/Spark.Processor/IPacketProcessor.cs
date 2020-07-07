using System;
using System.Threading.Tasks;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Packet;

namespace Spark.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }
        Task Process(IClient client, IPacket packet);
    }

    public abstract class PacketProcessor<TPacket> : IPacketProcessor where TPacket : IPacket
    {
        public Type PacketType { get; } = typeof(TPacket);

        public Task Process(IClient client, IPacket packet)
        {
            return Process(client, (TPacket)packet);
        }

        protected abstract Task Process(IClient client, TPacket packet);
    }
}