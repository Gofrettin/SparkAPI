using NLog;
using Spark.Event;
using Spark.Event.Login;
using Spark.Game.Abstraction;
using Spark.Packet.Login;

namespace Spark.Packet.Processor.Login
{
    public class FailcProcessor : PacketProcessor<Failc>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public FailcProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, Failc packet)
        {
            _eventPipeline.Emit(new LoginFailEvent(client, packet.Reason));
            
            client.Network.Close();
            Logger.Debug($"Failed to connect (reason: {packet.Reason})");
        }
    }
}