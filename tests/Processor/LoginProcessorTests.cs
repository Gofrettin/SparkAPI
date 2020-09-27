using System.Collections.Generic;
using System.Net;
using NFluent;
using Spark.Core.Configuration;
using Spark.Core.Server;
using Spark.Event.Login;
using Spark.Packet.Login;
using Spark.Packet.Processor.Login;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class LoginProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(NsTeStProcessor))]
        public void NsTeST_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new NsTeSt
                {
                    Name = "MyNameIs",
                    RegionId = 2,
                    EncryptionKey = 972,
                    Servers = new List<WorldServer>()
                    {
                        new WorldServer("Galaxie", 0, 2, 5, IPEndPoint.Parse("79.110.84.37:4014")),
                        new WorldServer("Cosmos", 1, 1, 6, IPEndPoint.Parse("79.110.84.250:4015"))
                    }
                });
                
                // Nothing to check
            }
        }

        [ProcessorTest(typeof(FailcProcessor))]
        [EventTest(typeof(LoginFailEvent))]
        public void Failc_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Failc
                {
                    Reason = 1
                });

                context.IsEventEmitted<LoginFailEvent>(x => x.Reason == 1);
            }
        }
    }
}