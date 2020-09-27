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
        private readonly IPacketFactory packetFactory;
        private readonly IPacketManager packetManager;
        private readonly INetworkFactory networkFactory;

        public ClientFactory(IPacketManager packetManager, IPacketFactory packetFactory, INetworkFactory networkFactory)
        {
            this.packetManager = packetManager;
            this.packetFactory = packetFactory;
            this.networkFactory = networkFactory;
        }

        public IClient CreateRemoteClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector)
        {
            INetwork network = networkFactory.CreateRemoteNetwork(ip);
            IClient client = new Client(network);

            client.AddConfiguration(new LoginConfiguration(serverSelector, characterSelector));

            client.PacketReceived += packet =>
            {
                IPacket typedPacket = packetFactory.CreatePacket(packet);
                if (typedPacket == null)
                {
                    return;
                }

                packetManager.Process(client, typedPacket);
            };

            return client;
        }

        public IClient CreateLocalClient(Process process)
        {
            return new Client(networkFactory.CreateLocalNetwork(process));
        }
    }
}