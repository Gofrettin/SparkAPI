using Spark.Packet.CharacterSelector;

namespace Spark.Packet.Factory.CharacterSelector
{
    public class OkCreator : PacketCreator<Ok>
    {
        public override string Header { get; } = "OK";
        
        public override Ok Create(string[] content)
        {
            return new Ok();
        }
    }
}