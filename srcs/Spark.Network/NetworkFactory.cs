using System;
using System.Diagnostics;
using System.Net;
using System.Timers;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network
{
    public class NetworkFactory : INetworkFactory
    {
        public INetwork CreateRemoteNetwork(IPEndPoint ip)
        {
            var network = new RemoteNetwork(new LoginEncoder(), new LoginDecoder(), 25);

            network.Connect(ip);

            return network;
        }

        public INetwork CreateRemoteNetwork(IPEndPoint ip, int encryptionKey)
        {
            int packetId = new Random().Next(20000, 40000);

            var session = new RemoteNetwork(new WorldEncoder(encryptionKey), new WorldDecoder(), 0xFF)
            {
                Modifiers = new Func<string, string>[]
                {
                    x => $"{packetId++} {x}"
                }
            };

            int keepAliveId = 0;
            var keepAlive = new Timer
            {
                Interval = 60000,
                Enabled = true
            };
            keepAlive.Elapsed += (obj, e) =>
            {
                if (!session.Client.Connected)
                {
                    keepAlive.Stop();
                }

                session.SendPacket($"pulse {keepAliveId++ * 60} 1");
            };

            session.Connect(ip);

            return session;
        }

        public INetwork CreateLocalNetwork(Process process)
        {
            throw new NotImplementedException();
        }
    }
}