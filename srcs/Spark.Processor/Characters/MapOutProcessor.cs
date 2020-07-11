using NLog;
using Spark.Event;
using Spark.Event.Characters;
using Spark.Game.Abstraction;
using Spark.Packet.Characters;

namespace Spark.Processor.Characters
{
    public class MapOutProcessor : PacketProcessor<MapOut>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public MapOutProcessor(IEventPipeline eventPipeline)
        {
            _eventPipeline = eventPipeline;
        }
        
        protected override void Process(IClient client, MapOut packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                Logger.Error("Received MapOut packet but map is null");
                return;
            }
            
            map.RemoveEntity(client.Character);
            _eventPipeline.Emit(new MapLeaveEvent(client, map));
        }
    }
}