using System;
using Spark.Game.Abstraction.Entities;
using Spark.Network.Option;
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

        T GetStorage<T>() where T : IOption;
        void AddStorage<T>(T storage) where T : IOption;
    }
}