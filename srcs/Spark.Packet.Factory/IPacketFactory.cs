using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;

namespace Spark.Packet.Factory
{
    public interface IPacketFactory
    {
        IPacket CreatePacket(string content);
    }

    public class PacketFactory : IPacketFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly Dictionary<string, IPacketCreator> creators;

        public PacketFactory(IEnumerable<IPacketCreator> creators)
        {
            this.creators = creators.ToDictionary(x => x.Header, x => x);
        }

        public IPacket CreatePacket(string content)
        {
            if (content == string.Empty)
            {
                return default;
            }

            string[] split = content.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string header = split[0];
            string[] packetContent = header.Length > 1 ? split.Skip(1).ToArray() : split;

            IPacketCreator creator = creators.GetValueOrDefault(header);
            if (creator == null)
            {
                return default;
            }

            return creator.Create(packetContent);
        }
    }
}