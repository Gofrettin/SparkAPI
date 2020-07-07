using System.Collections.Generic;
using System.Linq;
using System.Net;
using Spark.Core.Server;
using Spark.Packet.Extension;

namespace Spark.Packet.Login
{
    [Packet("NsTeST")]
    public class NsTeST : IPacket
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public int EncryptionKey { get; set; }
        public List<WorldServer> Servers { get; } = new List<WorldServer>();

        public void Construct(string[] packet)
        {
            Name = packet[1];
            RegionId = packet[2].ToInt();
            EncryptionKey = packet[3].ToInt();

            foreach (string server in packet.Skip(4))
            {
                string[] serverInfo = server.Split(':');

                string host = serverInfo[0];
                int port = serverInfo[1].ToInt();
                int population = serverInfo[2].ToInt();

                string[] serverData = serverInfo[3].Split('.');

                int serverId = serverData[0].ToInt();
                int channelId = serverData[1].ToInt();
                string name = serverData[2];

                if (host == "-1")
                {
                    continue;
                }

                Servers.Add(new WorldServer(name, population, serverId, channelId, IPEndPoint.Parse($"{host}:{port}")));
            }
        }
    }
}