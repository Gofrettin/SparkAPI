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
using Spark.Tests.Extension;

namespace Spark.Tests.Processor
{
    public abstract class ProcessorTests
    {
        protected ProcessorTests()
        {
            IServiceCollection services = new ServiceCollection();

            EventPipelineMock = new Mock<IEventPipeline>();

            services.AddDependencies();
            services.AddSingleton<IEventPipeline>(EventPipelineMock.Object);

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