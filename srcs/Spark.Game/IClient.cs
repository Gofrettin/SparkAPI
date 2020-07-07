using System;
using Spark.Game.Entities;

namespace Spark.Game
{
    public interface IClient
    {
        Guid Id { get; }

        Character Character { get; set; }

        event Action<string> PacketReceived;
        void SendPacket(string packet);
    }
}