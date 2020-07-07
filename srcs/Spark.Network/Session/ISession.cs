using System;

namespace Spark.Network.Session
{
    public interface ISession
    {
        event Action<string> PacketReceived; 
        void SendPacket(string packet);
    }
}