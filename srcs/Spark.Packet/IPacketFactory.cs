using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;

namespace Spark.Packet
{
    public interface IPacketFactory
    {
        IPacket CreatePacket(string content);
    }

    public class PacketFactory : IPacketFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, Type> _packets;

        public PacketFactory()
        {
            _packets = new Dictionary<string, Type>();
            foreach (Type type in typeof(IPacket).Assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                {
                    continue;
                }

                if (!typeof(IPacket).IsAssignableFrom(type))
                {
                    continue;
                }

                PacketAttribute attribute = type.GetCustomAttribute<PacketAttribute>();
                if (attribute == null)
                {
                    Logger.Warn($"Missing PacketAttribute on {type.Name}");
                    continue;
                }

                _packets[attribute.Header] = type;
            }
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

            Type type = _packets.GetValueOrDefault(header);
            if (type == null)
            {
                return default;
            }

            var packet = (IPacket)Activator.CreateInstance(type);
            if (packet == null)
            {
                throw new InvalidOperationException($"Failed to create packet {type.Name}");
            }

            try
            {
                packet.Construct(packetContent);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Failed to construct packet {packet.GetType().Name}");
            }

            return packet;
        }
    }
}