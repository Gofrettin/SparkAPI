using Spark.Event;
using Spark.Event.Exchange;
using Spark.Game.Abstraction;
using Spark.Packet.Exchange;

namespace Spark.Packet.Processor.Exchange
{
    public class ExcCloseProcessor : PacketProcessor<ExcClose>
    {
        private readonly IEventPipeline _eventPipeline;

        public ExcCloseProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, ExcClose packet)
        {
            _eventPipeline.Emit(new ExchangeCompletedEvent(client));
        }
    }
}