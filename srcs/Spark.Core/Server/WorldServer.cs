using System;
using System.Net;

namespace Spark.Core.Server
{
    public class WorldServer
    {
        public WorldServer(string name, int population, int serverId, int channelId, IPEndPoint ip)
        {
            Name = name;
            Population = population;
            ServerId = serverId;
            ChannelId = channelId;
            Ip = ip;
        }

        public string Name { get; }
        public int Population { get; }
        public int ServerId { get; }
        public int ChannelId { get; }
        public IPEndPoint Ip { get; }
    }
}