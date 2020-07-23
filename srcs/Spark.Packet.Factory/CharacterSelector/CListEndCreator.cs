using Spark.Packet.CharacterSelector;

namespace Spark.Packet.Factory.CharacterSelector
{
    public class CListEndCreator : PacketCreator<CListEnd>
    {
        public override string Header { get; } = "clist_end";
        
        public override CListEnd Create(string[] content)
        {
            return new CListEnd();
        }
    }
}