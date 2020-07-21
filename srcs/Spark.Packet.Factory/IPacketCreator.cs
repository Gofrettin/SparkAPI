using System;

namespace Spark.Packet.Factory
{
    public interface IPacketCreator
    {
        string Header { get; }
        IPacket Create(string[] content);
    }

    public abstract class PacketCreator<T> : IPacketCreator where T : IPacket
    {
        public abstract string Header { get; }
        
        IPacket IPacketCreator.Create(string[] content) => Create(content);

        public abstract T Create(string[] content);
    }
}