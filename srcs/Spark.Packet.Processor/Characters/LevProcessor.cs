using Spark.Game.Abstraction;
using Spark.Packet.Characters;

namespace Spark.Packet.Processor.Characters
{
    public class LevProcessor : PacketProcessor<Lev>
    {
        protected override void Process(IClient client, Lev packet)
        {
            client.Character.Level = packet.Level;
        }
    }
}