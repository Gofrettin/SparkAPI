using System.Collections.Generic;
using NFluent;
using Spark.Core.Enum;
using Spark.Packet.Chat;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class ChatPacketTests : PacketTests
    {
        [PacketTest(typeof(Sayi))]
        public void Sayi_Test()
        {
            Sayi packet = CreateAndCheckValues("sayi 1 123456 10 1828 0 0 0 0 0", new Sayi
            {
                EntityType = EntityType.Player,
                EntityId = 123456,
                Color = MessageColor.Yellow,
                MessageId = 1828,
                Parameters = new List<int> { 0, 0, 0, 0, 0 }
            }, "Parameters");
            
            Check.That(packet.Parameters).ContainsOnlyElementsThatMatch(x => x == 0);
        }

        [PacketTest(typeof(Msgi))]
        public void Msgi_Test()
        {
            Msgi packet = CreateAndCheckValues("msgi 0 1828 0 0 0 0 0", new Msgi
            {
                MessageType = MessageType.Classic,
                MessageId = 1828,
                Parameters = new List<int> { 0, 0, 0, 0, 0 }
            }, "Parameters");

            Check.That(packet.Parameters).ContainsOnlyElementsThatMatch(x => x == 0);
        }
    }
}