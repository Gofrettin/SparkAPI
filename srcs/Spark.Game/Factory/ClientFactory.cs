using System;
using System.Diagnostics;
using System.Net;
using Spark.Core;
using Spark.Core.Configuration;
using Spark.Core.Server;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Network;
using Spark.Packet;
using Spark.Packet.Factory;
using Spark.Packet.Processor;

namespace Spark.Game.Factory
{
    public class ClientFactory : IClientFactory
    {
        private readonly IPacketFactory _packetFactory;
        private readonly IPacketManager _packetManager;
        private readonly INetworkFactory _networkFactory;

        public ClientFactory(IPacketManager packetManager, IPacketFactory packetFactory, INetworkFactory networkFactory)
        {
            _packetManager = packetManager;
            _packetFactory = packetFactory;
            _networkFactory = networkFactory;
        }

        public IClient CreateRemoteClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            INetwork network = _networkFactory.CreateRemoteNetwork(ip);
            IClient client = new Client(network);

            client.AddConfiguration(new LoginConfiguration(serverSelector, characterSelector));

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

        public IClient CreateLocalClient(Process process)
        {
            return new Client(_networkFactory.CreateLocalNetwork(process));
        }
    }
}