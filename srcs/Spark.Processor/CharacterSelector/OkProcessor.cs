using Spark.Game;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class OkProcessor : PacketProcessor<Ok>
    {
        protected override void Process(IClient client, Ok packet)
        {
            client.SendPacket("game_start");
        }
    }
}