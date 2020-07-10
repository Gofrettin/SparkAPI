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
    public sealed class Spark : ISpark
    {
        public Spark(IClientFactory clientFactory, IEventPipeline eventPipeline, IDatabase database, IPacketManager packetManager, IGameforgeService gameforgeService,
            IEnumerable<IEventHandler> builtInEventHandlers, IEnumerable<IPacketProcessor> builtInPacketProcessors)
        {
            if (Created)
            {
                throw new InvalidOperationException("Can't create multiple instance of Spark");
            }

            ClientFactory = clientFactory;
            EventPipeline = eventPipeline;
            PacketManager = packetManager;
            GameforgeService = gameforgeService;
            Database = database;

            BuiltInEventHandlers = builtInEventHandlers;
            BuildInPacketProcessors = builtInPacketProcessors;

            Created = true;
        }

        private static bool Created { get; set; }

        public IClientFactory ClientFactory { get; }
        public IEventPipeline EventPipeline { get; }
        public IPacketManager PacketManager { get; }
        public IDatabase Database { get; }
        public IGameforgeService GameforgeService { get; }

        public IEnumerable<IEventHandler> BuiltInEventHandlers { get; }
        public IEnumerable<IPacketProcessor> BuildInPacketProcessors { get; }

        public async Task<IClient> CreateRemoteClient(IPEndPoint ip, string token, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            IClient client = await ClientFactory.CreateClient(ip, serverSelector, characterSelector);

            client.SendPacket($"NoS0577 {token} {GameforgeService.InstallationId} 007C762C 20.9.3.3127 0 D0C4D9B41720BC5E00E1C6C7DC6B8B22");

            return client;
        }

        public Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> predicate) =>
            GameforgeService.GetSessionToken(email, password, locale, predicate);

        public void AddEventHandler<T>(T handler) where T : IEventHandler
        {
            EventPipeline.AddEventHandler(handler);
        }

        public void Dispose()
        {
        }

        public void Initialize()
        {
            Database.Load();

            EventPipeline.AddEventHandlers(BuiltInEventHandlers);
            PacketManager.AddPacketProcessors(BuildInPacketProcessors);
        }

        public static ISpark CreateInstance()
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
            services.AddSingleton<Spark>();

            Spark spark = services.BuildServiceProvider().GetService<Spark>();

            spark.Initialize();

            return spark;
        }
    }
}