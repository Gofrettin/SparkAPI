using System;
using System.Collections.Generic;
using NLog;
using Spark.Game;
using Spark.Packet;

namespace Spark.Processor
{
    public interface IPacketManager
    {
        void Process(IClient client, IPacket packet);
        void AddPacketProcessor(IPacketProcessor processor);
        void AddPacketProcessors(IEnumerable<IPacketProcessor> processors);
    }

    public class PacketManager : IPacketManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<Type, IPacketProcessor> _processors;

        public PacketManager() => _processors = new Dictionary<Type, IPacketProcessor>();

        public void Process(IClient client, IPacket packet)
        {
            IPacketProcessor processor = _processors.GetValueOrDefault(packet.GetType());
            if (processor == null)
            {
                Logger.Warn($"No packet processor for {packet.GetType().Name}");
                return;
            }


            Logger.Debug($"Processing packet {packet.GetType().Name} using {processor.GetType().Name}");
            processor.Handle(client, packet);
        }

        public void AddPacketProcessor(IPacketProcessor processor)
        {
            _processors[processor.PacketType] = processor;
            Logger.Info($"Registered {processor.GetType().Name} for packet {processor.PacketType.Name}");
        }

        public void AddPacketProcessors(IEnumerable<IPacketProcessor> processors)
        {
            foreach (IPacketProcessor processor in processors)
            {
                AddPacketProcessor(processor);
            }
        }
    }
}