using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Spark.Core;
using Spark.Core.Server;
using Spark.Database;
using Spark.Event;
using Spark.Extension;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Factory;
using Spark.Gameforge;
using Spark.Network.Session;
using Spark.Packet;
using Spark.Processor;

namespace Spark
{
    public sealed class SparkAPI : ISpark
    {
        public static ISpark Instance { get; } = CreateInstance();
        
        public SparkAPI(IClientFactory clientFactory, IEventPipeline eventPipeline, IDatabase database, IPacketManager packetManager, IGameforgeService gameforgeService, IEnumerable<IPacketProcessor> packetProcessors)
        {
            ClientFactory = clientFactory;
            EventPipeline = eventPipeline;
            PacketManager = packetManager;
            GameforgeService = gameforgeService;
            Database = database;
            
            PacketProcessors = packetProcessors;
        }

        public IClientFactory ClientFactory { get; }
        public IEventPipeline EventPipeline { get; }
        public IPacketManager PacketManager { get; }
        public IDatabase Database { get; }
        public IGameforgeService GameforgeService { get; }
        
        public IEnumerable<IPacketProcessor> PacketProcessors { get; }

        public IClient CreateRemoteClient(IPEndPoint ip, string token, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            IClient client = ClientFactory.CreateClient(ip, serverSelector, characterSelector);

            client.SendPacket($"NoS0577 {token} {GameforgeService.InstallationId} 007C762C 20.9.3.3127 0 D0C4D9B41720BC5E00E1C6C7DC6B8B22");

            return client;
        }

        public Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> predicate) =>
            GameforgeService.GetSessionToken(email, password, locale, predicate);

        public void AddEventHandler<T>(T handler) where T : IEventHandler
        {
            EventPipeline.AddEventHandler(handler);
        }

        public void Initialize()
        {
            Database.Load();
            
            PacketManager.AddPacketProcessors(PacketProcessors);
        }

        private static ISpark CreateInstance()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddImplementingTypes<IPacketProcessor>();
            services.AddImplementingTypes<IEventHandler>();

            services.AddTransient<IGameforgeService, GameforgeService>();
            services.AddTransient<IClientFactory, ClientFactory>();
            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<IEntityFactory, EntityFactory>();
            services.AddTransient<ISessionFactory, SessionFactory>();

            services.AddSingleton<IPacketFactory, PacketFactory>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IEventPipeline, EventPipeline>();
            services.AddSingleton<IDatabase, SparkDatabase>();
            services.AddSingleton<SparkAPI>();

            SparkAPI sparkApi = services.BuildServiceProvider().GetService<SparkAPI>();

            sparkApi.Initialize();

            return sparkApi;
        }
    }
}