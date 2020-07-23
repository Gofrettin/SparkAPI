using System.Collections.Generic;
using NFluent;
using Spark.Core.Enum;
using Spark.Packet.Chat;
using Spark.Packet.Notification;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class NotificationPacketTests : PacketTests
    {
        [PacketTest(typeof(QNamli2))]
        public void QNamli2_Test()
        {
            CreateAndCheckValues("qnamli2 100 #rl 1647 10 1997 MyNameIs", new QNamli2
            {
                Raid = new QNamli2.RaidInfo
                {
                    Owner = "MyNameIs"
                }
            });
        }

        [PacketTest(typeof(QNamli))]
        public void QNamli_Test()
        {
            CreateAndCheckValues("qnamli 1 #guri^205", new QNamli
            {
                Request = "#guri^205"
            });
        }
    }
}