using Spark.Packet.Extension;

namespace Spark.Packet.Notification
{
    [Packet("qnamli2")]
    public class QNamli2 : IPacket
    {
        public RaidListInfo Raid { get; set; }
        
        public void Construct(string[] packet)
        {
            string id = packet[1];
            if (id == "#rl")
            {
                Raid = new RaidListInfo
                {
                    RaidId = packet[4].ToInt(),
                    Owner = packet[5]
                };
            }
        }

        public class RaidListInfo
        {
            public int RaidId { get; set; }
            public string Owner { get; set; }
        }
    }
}