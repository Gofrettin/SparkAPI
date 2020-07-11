using System;
using System.Net;
using System.Timers;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network.Session
{
    public class SessionFactory : ISessionFactory
    {
        public ISession CreateSession(IPEndPoint ip)
        {
            var session = new RemoteSession(new LoginEncoder(), new LoginDecoder());
            
            session.Connect(ip);

            return session;
        }

        public ISession CreateSession(IPEndPoint ip, int encryptionKey)
        {
            int packetId = new Random().Next(20000, 40000);

            var session = new RemoteSession(new WorldEncoder(encryptionKey), new WorldDecoder())
            {
                Modifiers = new Func<string, string>[]
                {
                    x => $"{packetId++} {x}",
                }
            };

            int keepAliveId = 0;
            var keepAlive = new Timer
            {
                Interval = 60000,
                Enabled = true,
            };
            keepAlive.Elapsed += (obj, e) =>
            {
                if (!session.Socket.Connected)
                {
                    keepAlive.Stop();
                }

                session.SendPacket($"pulse {keepAliveId++ * 60} 1");
            };

            session.Connect(ip);

            return session;
        }
    }
}