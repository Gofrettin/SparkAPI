using System.Net;
using Spark.Core.Server;
using Spark.Packet.Login;

namespace Spark.Tests.Packet.Login
{
    public class NsTeSTPacketTest : PacketTest<NsTeST>
    {
        protected override string Packet { get; } = "NsTeST 2 MyNameIs 2 972 79.110.84.37:4014:0:2.5.Galaxie 79.110.84.250:4015:1:1.6.Cosmos -1:-1:-1:10000.10000.1";
        
        protected override NsTeST Excepted { get; } = new NsTeST
        {
            Name = "MyNameIs",
            RegionId = 2,
            EncryptionKey = 972,
            Servers =
            {
                new WorldServer("Galaxie", 0, 2, 5, IPEndPoint.Parse("79.110.84.37:4014")),
                new WorldServer("Cosmos", 1, 1, 6, IPEndPoint.Parse("79.110.84.250:4015")),
            }
        };
    }
}