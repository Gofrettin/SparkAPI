using Spark.Packet.Notification;

namespace Spark.Tests.Packet.Notification
{
    public class QNamli2PacketTest : PacketTest<QNamli2>
    {
        protected override string Packet { get; } = "qnamli2 100 #rl 1647 10 1997 MyNameIs";

        protected override QNamli2 Excepted { get; } = new QNamli2
        {
            Raid = new QNamli2.RaidInfo
            {
                Owner = "MyNameIs"
            }
        };
    }
}