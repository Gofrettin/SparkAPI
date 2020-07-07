using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Option;
using Spark.Core.Server;
using Spark.Network.Session;
using Spark.Packet;
using Spark.Processor;

namespace Spark.Client
{
    public class ClientFactory : IClientFactory
    {
        public IPacketFactory PacketFactory { get; }
        public IPacketManager PacketManager { get; }
        public ISessionFactory SessionFactory { get; }
        
        public ClientFactory(IPacketFactory packetFactory, IPacketManager packetManager, ISessionFactory sessionFactory)
        {
            PacketFactory = packetFactory;
            PacketManager = packetManager;
            SessionFactory = sessionFactory;
        }

        public async Task<Game.Client> CreateClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            ISession session = await SessionFactory.CreateSession(ip);
            var client = new Game.Client(session)
            {
                Options =
                {
                    [typeof(LoginOption)] = new LoginOption(serverSelector, characterSelector)
                }
            };

            client.PacketReceived += packet =>
            {
                IPacket typedPacket = PacketFactory.CreatePacket(packet);
                if (typedPacket == null)
                {
                    return;
                }
                
                PacketManager.Process(client, typedPacket);
            };

            return client;
        }
    }
}