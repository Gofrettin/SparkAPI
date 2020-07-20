using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Spark.Event;
using Spark.Game;
using Spark.Game.Entities;
using Spark.Network;
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
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IEventPipeline>(EventPipelineMock.Object);

            IServiceProvider provider = services.BuildServiceProvider();

            PacketManager = provider.GetService<IPacketManager>();
        }

        private IPacketManager PacketManager { get; }
        private Mock<IEventPipeline> EventPipelineMock { get; }

        protected GameContext CreateContext(bool withCharacter = true)
        {
            var client = new Client(new Mock<INetwork>().Object);
            return new GameContext(client, PacketManager, EventPipelineMock)
            {
                Character = withCharacter ? new Character(123456, client) : null
            };
        }
    }
}