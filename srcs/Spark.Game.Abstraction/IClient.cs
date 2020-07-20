using System;
using Spark.Core.Option;
using Spark.Game.Abstraction.Entities;
using Spark.Network;

namespace Spark.Game.Abstraction
{
    public interface IClient : IEquatable<IClient>
    {
        Guid Id { get; }
        ICharacter Character { get; set; }
        INetwork Network { get; set; }

        event Action<string> PacketReceived;

        void SendPacket(string packet);

        T GetOption<T>() where T : IOption;
        void AddOption<T>(T storage) where T : IOption;
    }
}