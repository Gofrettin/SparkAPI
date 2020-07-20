using System;

namespace Spark.Network
{
    public interface INetwork
    {
        event Action<string> PacketReceived;
        void SendPacket(string packet);

        void Close();
    }
}