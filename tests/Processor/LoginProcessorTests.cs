﻿using System.Net;
using Spark.Core.Server;
using Spark.Packet.Login;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class LoginProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(NsTeST))]
        public void NsTeST_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new NsTeST
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

                // Nothing to check just processing to make sure everything is ok
            }
        }

        [ProcessorTest(typeof(Failc))]
        public void Failc_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Failc
                {
                    Reason = 1
                });

                // Nothing to check just processing to make sure everything is ok
            }
        }
    }
}