using System.Threading.Tasks;
using NLog;
using Spark.Event;
using Spark.Event.Characters;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Characters;

namespace Spark.Processor.Characters
{
    public class CMapProcessor : PacketProcessor<CMap>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEventPipeline _eventPipeline;

        private readonly IMapFactory _mapFactory;

        public CMapProcessor(IMapFactory mapFactory, IEventPipeline eventPipeline)
        {
            _mapFactory = mapFactory;
            _eventPipeline = eventPipeline;
        }

        protected override void Process(IClient client, CMap packet)
        {
            if (!packet.IsSourceMap)
            {
                return;
            }

            IMap map = _mapFactory.CreateMap(packet.MapId);
            map.AddEntity(client.Character);

            _eventPipeline.Emit(new MapChangeEvent(client, map));

            Logger.Info($"Map changed to {map.Id}");
        }
    }
}