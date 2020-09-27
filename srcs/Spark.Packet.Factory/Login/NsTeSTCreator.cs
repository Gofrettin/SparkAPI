using System.Collections.Generic;
using System.Linq;
using System.Net;
using Spark.Core.Extension;
using Spark.Core.Server;
using Spark.Packet.Login;

namespace Spark.Packet.Factory.Login
{
    public class NsTeStCreator : PacketCreator<NsTeSt>
    {
        public override string Header { get; } = "NsTeST";
        
        public override NsTeSt Create(string[] content)
        {
            var packet = new NsTeSt
            {
                Name = content[1],
                RegionId = content[2].ToInt(),
                EncryptionKey = content[3].ToInt()
            };
            
            var servers = new List<WorldServer>();
            foreach (string server in content.Skip(4))
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

                servers.Add(new WorldServer(name, population, serverId, channelId, IPEndPoint.Parse($"{host}:{port}")));
            }

            packet.Servers = servers;

            return packet;
        }
    }
}