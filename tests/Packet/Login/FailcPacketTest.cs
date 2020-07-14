﻿﻿using Spark.Packet.Login;

namespace Spark.Tests.Packet.Login
{
    public class FailcPacketTest : PacketTest<Failc>
    {
        protected override string Packet { get; } = "failc 1";

        protected override Failc Excepted { get; } = new Failc
        {
            Reason = 1
        };
    }
}