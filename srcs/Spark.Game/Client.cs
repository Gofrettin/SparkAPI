using System;
using System.Collections.Generic;
using Spark.Core.Storage;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Network.Session;

namespace Spark.Game
{
    public sealed class Client : IClient
    {
        private ISession _session;

        public ISession Session
        {
            get => _session;
            set
            {
                if (_session != null)
                {
                    _session.PacketReceived -= ProcessPacket;
                }

                _session = value;
                _session.PacketReceived += ProcessPacket;
            }
        }
        
        public Guid Id { get; }
        public ICharacter Character { get; set; }
        
        public event Action<string> PacketReceived;
        
        public Dictionary<Type, object> Storage { get; }

        public Client(ISession session)
        {
            Id = Guid.NewGuid();
            Session = session;
            Storage = new Dictionary<Type, object>();
        }

        public void SendPacket(string packet)
        {
            Session.SendPacket(packet);
        }

        private void ProcessPacket(string packet)
        {
            PacketReceived?.Invoke(packet);
        }

        public T GetStorage<T>() where T : IStorage => (T)Storage.GetValueOrDefault(typeof(T));
        
        public void AddStorage<T>(T storage) where T : IStorage
        {
            Storage[typeof(T)] = storage;
        }
    }
}