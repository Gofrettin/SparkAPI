using System;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Spark.Core;
using Spark.Core.Server;
using Spark.Database;
using Spark.Database.Data;
using Spark.Event;
using Spark.Extension;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Entities;
using Spark.Game.Factory;
using Spark.Network.Session;
using Spark.Packet;
using Spark.Processor;
using Xunit;

namespace Spark.Tests.Processor
{
    public abstract class ProcessorTest<T> where T : IPacket
    {
        protected abstract T Packet { get; }
        
        protected IClient Client { get; }
        protected IMap Map { get; }
        
        private IPacketManager PacketManager { get; }

        protected ProcessorTest()
        {
            IServiceCollection services = new ServiceCollection();

            Client = new Client(new Mock<ISession>().Object);
            Client.Character = new Character(123456, Client);
            
            Map = new Map(1, new MapData
            {
                Grid = new byte[123]
            });

            var dbMock = new Mock<IDatabase>();
            
            dbMock.Setup(x => x.Monsters.GetValue(It.IsAny<int>())).Returns(new MonsterData());
            
            services.AddSingleton(dbMock.Object);
            
            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<ISkillFactory, SkillFactory>();
            services.AddTransient<IEntityFactory, EntityFactory>();
            services.AddTransient<ISessionFactory, SessionFactory>();
            services.AddSingleton<IEventPipeline, EventPipeline>();
            
            services.AddImplementingTypes<IPacketProcessor>();

            IServiceProvider provider = services.BuildServiceProvider();

            PacketManager = new PacketManager();
            PacketManager.AddPacketProcessors(provider.GetServices<IPacketProcessor>());
        }
        
        
        [Fact]
        public void Execute()
        {
            PacketManager.Process(Client, Packet);
            CheckResult();
        }
            
        protected abstract void CheckResult();
    }
}