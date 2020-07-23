namespace Spark.Packet.Notification
{
    public class QNamli2 : IPacket
    {
        public RaidInfo Raid { get; set; }

        public class RaidInfo
        {
            public string Owner { get; set; }
        }
    }
}