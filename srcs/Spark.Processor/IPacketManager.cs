using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Spark.Game.Abstraction;
using Spark.Packet;

namespace Spark.Processor
{
    public interface IPacketManager
    {
        void Process(IClient client, IPacket packet);
    }

    public class PacketManager : IPacketManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<Type, IPacketProcessor> _processors;

        public PacketManager(IEnumerable<IPacketProcessor> processors)
        {
            _processors = processors.ToDictionary(x => x.PacketType, x => x);
        }

        public void Process(IClient client, IPacket packet)
        {
            IPacketProcessor processor = _processors.GetValueOrDefault(packet.GetType());
            if (processor == null)
            {
                Logger.Warn($"No packet processor for {packet.GetType().Name}");
                return;
            }

            Logger.Trace($"Processing packet {packet.GetType().Name} using {processor.GetType().Name}");
            try
            {
                processor.Process(client, packet);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}