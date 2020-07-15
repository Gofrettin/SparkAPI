using System.Net;
using Spark.Core.Server;
using Spark.Packet.Login;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class LoginPacketTests : PacketTests
    {
        [PacketTest(typeof(Failc))]
        public void Failc_Test()
        {
            CreateAndCheckValues<Failc>("failc 1", new Failc
            {
                Reason = 1
            });
        }

        [PacketTest(typeof(NsTeST))]
        public void NsTeST_Test()
        {
            CreateAndCheckValues("NsTeST 2 MyNameIs 2 972 79.110.84.37:4014:0:2.5.Galaxie 79.110.84.250:4015:1:1.6.Cosmos -1:-1:-1:10000.10000.1", new NsTeST
            {
                Name = "MyNameIs",
                RegionId = 2,
                EncryptionKey = 972,
                Servers =
                {
                    new WorldServer("Galaxie", 0, 2, 5, IPEndPoint.Parse("79.110.84.37:4014")),
                    new WorldServer("Cosmos", 1, 1, 6, IPEndPoint.Parse("79.110.84.250:4015"))
                }
            });
        }
    }
}