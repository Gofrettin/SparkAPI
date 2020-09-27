using Spark.Event;
using Spark.Event.Login;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;

namespace Spark.Packet.Processor.CharacterSelector
{
    public class OkProcessor : PacketProcessor<Ok>
    {
        private readonly IEventPipeline eventPipeline;

        public OkProcessor(IEventPipeline eventPipeline) => this.eventPipeline = eventPipeline;

        protected override void Process(IClient client, Ok packet)
        {
            client.SendPacket("game_start");
            eventPipeline.Emit(new GameStartEvent(client));
        }
    }
}