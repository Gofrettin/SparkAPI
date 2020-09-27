using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Fluent;
using Spark.Core;
using Spark.Core.Server;
using Spark.Database;
using Spark.Event;
using Spark.Extension;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Factory;
using Spark.Gameforge;
using Spark.Gameforge.Nostale;
using Spark.Packet.Factory;
using Spark.Packet.Processor;

namespace Spark
{
    public sealed class SparkApi : ISpark
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public SparkApi(IClientFactory clientFactory, IEventPipeline eventPipeline, IDatabase database, IPacketManager packetManager, IGameforgeService gameforgeService,
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

        public IClient CreateRemoteClient(IPEndPoint ip, string token, NostaleClientInfo clientInfo, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            IClient client = ClientFactory.CreateRemoteClient(ip, serverSelector, characterSelector);
            
            client.SendPacket($"NoS0577 {token} {GameforgeService.InstallationId} 007C762C 2{clientInfo.Version} 0 {(clientInfo.DxHash.ToUpper() + clientInfo.GlHash.ToUpper()).ToMd5()}");

            return client;
        }

        public Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> accountSelector)
        {
            return GameforgeService.GetSessionToken(email, password, locale, accountSelector);
        }

        public void AddEventHandler(IEventHandler eventHandler)
        {
            EventPipeline.AddEventHandler(eventHandler);
        }

        public void AddEventHandler<T>(Action<T> handler) where T : IEvent
        {
            EventPipeline.AddEventHandler(handler);
        }

        private static ISpark CreateInstance()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddImplementingTypes<IPacketCreator>();
            services.AddImplementingTypes<IPacketProcessor>();

            services.AddSingleton<IDatabase, FileDatabase>();
            
            services.AddGameFactories();
            
            services.AddSingleton<IPacketFactory, PacketFactory>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IEventPipeline, EventPipeline>();
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<IGameforgeService, GameforgeService>();
            
            services.AddSingleton<SparkApi>();

            SparkApi sparkApi = services.BuildServiceProvider().GetService<SparkApi>();
            
            sparkApi.Database.Load();

            return sparkApi;
        }
    }
}