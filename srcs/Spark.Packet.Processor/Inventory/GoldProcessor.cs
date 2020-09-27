using Spark.Event;
using Spark.Event.Inventory;
using Spark.Game.Abstraction;
using Spark.Packet.Inventory;

namespace Spark.Packet.Processor.Inventory
{
    public class GoldProcessor : PacketProcessor<Gold>
    {
        private readonly IEventPipeline eventPipeline;

        public GoldProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Gold packet)
        {
            client.Character.Inventory.Gold = packet.Classic;
            eventPipeline.Emit(new GoldChangeEvent(client, packet.Classic));
        }
    }
}