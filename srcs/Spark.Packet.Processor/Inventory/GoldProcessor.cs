using Spark.Event;
using Spark.Event.Inventory;
using Spark.Game.Abstraction;
using Spark.Packet.Inventory;

namespace Spark.Packet.Processor.Inventory
{
    public class GoldProcessor : PacketProcessor<Gold>
    {
        private readonly IEventPipeline _eventPipeline;

        public GoldProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Gold packet)
        {
            client.Character.Inventory.Gold = packet.Classic;
            _eventPipeline.Emit(new GoldChangeEvent(client, packet.Classic));
        }
    }
}