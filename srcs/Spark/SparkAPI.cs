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
        public SparkAPI(IClientFactory clientFactory, IEventPipeline eventPipeline, IDatabase database, IPacketManager packetManager, IGameforgeService gameforgeService,
            IEnumerable<IPacketProcessor> packetProcessors)
        {
            ClientFactory = clientFactory;
            EventPipeline = eventPipeline;
            PacketManager = packetManager;
            GameforgeService = gameforgeService;
            Database = database;

            PacketProcessors = packetProcessors;
        }

        public static ISpark Instance { get; } = CreateInstance();

        public IClientFactory ClientFactory { get; }
        public IPacketManager PacketManager { get; }
        public IDatabase Database { get; }

        public IEnumerable<IPacketProcessor> PacketProcessors { get; }
        public IEventPipeline EventPipeline { get; }
        public IGameforgeService GameforgeService { get; }

        public IClient CreateRemoteClient(IPEndPoint ip, string token, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            IClient client = ClientFactory.CreateClient(ip, serverSelector, characterSelector);

            client.SendPacket($"NoS0577 {token} {GameforgeService.InstallationId} 007C762C 20.9.3.3131 0 35A4AA7A09325A8BDEF01C188C7BF295");

            return client;
        }

        public Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> accountSelector) =>
            GameforgeService.GetSessionToken(email, password, locale, accountSelector);

        public void AddEventHandler(IEventHandler eventHandler)
        {
            EventPipeline.AddEventHandler(eventHandler);
        }

        public void AddEventHandler<T>(Action<T> handler) where T : IEvent
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

            services.AddTransient<IGameforgeService, GameforgeService>();
            services.AddTransient<IClientFactory, ClientFactory>();
            services.AddTransient<IMapFactory, MapFactory>();
            services.AddTransient<ISkillFactory, SkillFactory>();
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