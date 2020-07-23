using System;
using System.Linq.Expressions;
using Moq;
using Spark.Event;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet;
using Spark.Packet.Processor;

namespace Spark.Tests
{
    public class GameContext : IDisposable
    {
        public GameContext(IClient client, IPacketManager packetManager, Mock<IEventPipeline> eventPipeline)
        {
            Client = client;
            PacketManager = packetManager;
            EventPipeline = eventPipeline;
        }

        public IClient Client { get; }

        public ICharacter Character
        {
            get => Client.Character;
            set => Client.Character = value;
        }

        public IMap Map
        {
            get => Character.Map;
            set => value.AddEntity(Character);
        }

        private IPacketManager PacketManager { get; }
        private Mock<IEventPipeline> EventPipeline { get; }

        public void Dispose()
        {
        }

        public void Process<T>(T packet) where T : IPacket
        {
            PacketManager.Process(Client, packet);
        }

        /// <summary>
        ///     Check if defined event is successfully called by event pipeline
        /// </summary>
        public void IsEventEmitted<T>(Expression<Func<T, bool>> check) where T : IEvent
        {
            EventPipeline.Verify(x => x.Emit(It.Is<T>(check)));
        }

        public void IsEventEmitted<T>() where T : IEvent
        {
            EventPipeline.Verify(x => x.Emit(It.IsAny<T>()));
        }
    }
}