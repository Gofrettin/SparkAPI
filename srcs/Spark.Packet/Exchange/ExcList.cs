using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Exchange
{
    [Packet("exc_list")]
    public class ExcList : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public int Gold { get; set; }
        public int BankGold { get; set; }
        
        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            Gold = packet[2].ToInt();
            BankGold = packet[3].ToInt();
        }
    }
}