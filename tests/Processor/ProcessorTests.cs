using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Spark.Core.Enum;
using Spark.Database;
using Spark.Database.Data;
using Spark.Event;
using Spark.Extension;
using Spark.Game;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Entities;
using Spark.Game.Factory;
using Spark.Network.Session;
using Spark.Processor;

namespace Spark.Tests.Processor
{
    public abstract class ProcessorTests
    {
        protected ProcessorTests()
        {
            IServiceCollection services = new ServiceCollection();

            EventPipelineMock = new Mock<IEventPipeline>();
            var dbMock = new Mock<IDatabase>();

            dbMock.Setup(x => x.Monsters.GetValue(It.IsAny<int>())).Returns(new MonsterData());
            dbMock.Setup(x => x.Maps.GetValue(It.IsAny<int>())).Returns(new MapData
            {
                Grid = new byte[999]
            });
            dbMock.Setup(x => x.Skills.GetValue(It.IsAny<int>())).Returns(new SkillData
            {
                Category = SkillCategory.Player
            });

            services.AddSingleton(dbMock.Object);

            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<ISkillFactory, SkillFactory>();
            services.AddTransient<IEntityFactory, EntityFactory>();
            services.AddTransient<ISessionFactory, SessionFactory>();
            services.AddSingleton<IEventPipeline>(EventPipelineMock.Object);

            services.AddImplementingTypes<IPacketProcessor>();

            IServiceProvider provider = services.BuildServiceProvider();

            PacketManager = new PacketManager();
            PacketManager.AddPacketProcessors(provider.GetServices<IPacketProcessor>());
        }

        private IPacketManager PacketManager { get; }
        private Mock<IEventPipeline> EventPipelineMock { get; }

        protected GameContext CreateContext(bool withCharacter = true)
        {
            var client = new Client(new Mock<ISession>().Object);
            return new GameContext(client, PacketManager, EventPipelineMock)
            {
                Character = withCharacter ? new Character(123456, client) : null
            };
        }
    }
}