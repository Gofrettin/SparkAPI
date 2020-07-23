using Spark.Packet.CharacterSelector;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.CharacterSelector
{
    public class CListCreator : PacketCreator<CList>
    {
        public override string Header { get; } = "clist";
        
        public override CList Create(string[] content)
        {
            return new CList
            {
                Slot = content[0].ToInt(),
                Name = content[1]
            };
        }
    }
}