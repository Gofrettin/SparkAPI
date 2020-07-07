using NLog;
using Spark.Event;
using Spark.Event.Login;
using Spark.Game;
using Spark.Packet.Login;

namespace Spark.Processor.Login
{
    public class NsTeSTProcessor : PacketProcessor<NsTeST>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public NsTeSTProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, NsTeST packet)
        {
            Logger.Info($"Successfully connected with account {packet.Name}");
            _eventPipeline.Emit(new LoginCompleteEvent(packet.Name, packet.EncryptionKey, packet.Servers));
        }
    }
}