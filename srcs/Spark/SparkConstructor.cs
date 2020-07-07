using System.Collections.Generic;
using NLog;
using Spark.Event;
using Spark.Processor;

namespace Spark
{
    internal sealed class SparkConstructor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEnumerable<IEventHandler> _handlers;
        private readonly IEnumerable<IPacketProcessor> _processors;

        private readonly Spark _spark;

        public SparkConstructor(Spark spark, IEnumerable<IEventHandler> handlers, IEnumerable<IPacketProcessor> processors)
        {
            _spark = spark;
            _handlers = handlers;
            _processors = processors;
        }

        public Spark Construct()
        {
            Logger.Info("Initializing Spark");

            _spark.PacketManager.AddPacketProcessors(_processors);
            _spark.EventPipeline.AddEventHandlers(_handlers);

            _spark.GameDataProvider.EnsureCreated();

            return _spark;
        }
    }
}