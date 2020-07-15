using Spark.Packet.Extension;

namespace Spark.Packet.Notification
{
    [Packet("qnamli2")]
    public class QNamli2 : IPacket
    {
        public RaidInfo Raid { get; set; }

        public void Construct(string[] packet)
        {
            string identifier = packet[1];
            if (identifier == "#rl")
            {
                Raid = new RaidInfo
                {
                    Owner = packet[5]
                };
            }
        }

        public class RaidInfo
        {
            public string Owner { get; set; }
        }
    }
}