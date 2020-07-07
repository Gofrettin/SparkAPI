using System;
using Spark.Core.Storage;
using Spark.Game.Abstraction.Entities;
using Spark.Network.Session;

namespace Spark.Game.Abstraction
{
    public interface IClient
    {
        Guid Id { get; }
        ICharacter Character { get; set; }
        ISession Session { get; set; }
        
        event Action<string> PacketReceived;
        
        void SendPacket(string packet);

        T GetStorage<T>() where T : IStorage;
        void AddStorage<T>(T storage) where T : IStorage;
    }
}