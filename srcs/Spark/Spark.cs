using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Spark.Database;
using Spark.Event;
using Spark.Extension;
using Spark.Game;
using Spark.Game.Factory;
using Spark.Game.Factory.Impl;
using Spark.Gameforge;
using Spark.Network.Client;
using Spark.Network.Client.Impl;
using Spark.Packet;
using Spark.Processor;

namespace Spark
{
    public sealed class Spark : ISpark
    {
        internal Spark(IClientFactory clientFactory, IPacketManager packetManager, IEventPipeline eventPipeline, IGameDataProvider gameDataProvider)
        {
            ClientFactory = clientFactory;
            EventPipeline = eventPipeline;
            PacketManager = packetManager;
            GameDataProvider = gameDataProvider;
        }

        public IClientFactory ClientFactory { get; }
        public IEventPipeline EventPipeline { get; }
        public IPacketManager PacketManager { get; }
        public IGameDataProvider GameDataProvider { get; }

        public Task<IClient> CreateClient(IPEndPoint ip) => ClientFactory.CreateClient(ip);

        public Task<IClient> CreateClient(IPEndPoint ip, string name, int encryptionKey) => ClientFactory.CreateClient(ip, name, encryptionKey);

        public Task<IClient> CreateClient(Process process) => ClientFactory.CreateClient(process);

        public static ISpark CreateInstance()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddImplementingTypes<IPacketProcessor>();
            services.AddImplementingTypes<IEventHandler>();

            services.AddTransient<IGameforgeService, GameforgeService>();
            services.AddTransient<IClientFactory, ClientFactory>();
            services.AddTransient<IMapFactory, MapFactory>();

            services.AddSingleton<IPacketFactory, PacketFactory>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IEventPipeline, EventPipeline>();
            services.AddSingleton<IGameDataProvider, GameDataProvider>();

            services.AddSingleton<SparkConstructor>();

            SparkConstructor sparkConstructor = services.BuildServiceProvider().GetService<SparkConstructor>();

            return sparkConstructor.Construct();
        }
    }
}