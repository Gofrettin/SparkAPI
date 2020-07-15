using System.Net;
using Spark.Core.Server;
using Spark.Packet.Login;

namespace Spark.Tests.Processor.Login
{
    public class NsTeSTProcessorTest : ProcessorTest<NsTeST>
    {
        protected override NsTeST Packet { get; } = new NsTeST
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
        
        protected override void CheckOutput()
        {
            
        }
    }
}