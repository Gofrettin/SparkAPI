using System;
using System.Net;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Server;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Network.Option;
using Spark.Network.Session;
using Spark.Packet;
using Spark.Processor;

namespace Spark.Game.Factory
{
    public class ClientFactory : IClientFactory
    {
        private readonly IPacketFactory _packetFactory;
        private readonly IPacketManager _packetManager;
        private readonly ISessionFactory _sessionFactory;

        public ClientFactory(IPacketManager packetManager, IPacketFactory packetFactory, ISessionFactory sessionFactory)
        {
            _packetManager = packetManager;
            _packetFactory = packetFactory;
            _sessionFactory = sessionFactory;
        }

        public async Task<IClient> CreateClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            ISession session = await _sessionFactory.CreateSession(ip);
            IClient client = new Client(session);

            client.AddOption(new LoginOption(serverSelector, characterSelector));

            client.PacketReceived += packet =>
            {
                IPacket typedPacket = _packetFactory.CreatePacket(packet);
                if (typedPacket == null)
                {
                    return;
                }

                _packetManager.Process(client, typedPacket);
            };

            return client;
        }
    }
}