using Spark.Core.Extension;
using Spark.Packet.Inventory;

namespace Spark.Packet.Factory.Inventory
{
    public class GoldCreator : PacketCreator<Gold>
    {
        public override string Header { get; } = "gold";
        
        public override Gold Create(string[] content)
        {
            return new Gold
            {
                Classic = content[0].ToInt(),
                Bank = content[1].ToInt()
            };
        }
    }
}