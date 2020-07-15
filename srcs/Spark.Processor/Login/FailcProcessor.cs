using NLog;
using Spark.Game.Abstraction;
using Spark.Packet.Login;

namespace Spark.Processor.Login
{
    public class FailcProcessor : PacketProcessor<Failc>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override void Process(IClient client, Failc packet)
        {
            Logger.Info($"Failed to connect (reason: {packet.Reason})");
        }
    }
}