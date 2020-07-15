using Spark.Packet.CharacterSelector;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class CharacterSelectorPacketTests : PacketTests
    {
        [PacketTest(typeof(CListEnd))]
        public void CListEnd_Test()
        {
            CreateAndCheckValues("clist_end", new CListEnd());
        }

        [PacketTest(typeof(CList))]
        public void CList_Test()
        {
            CreateAndCheckValues("clist 2 MyNameIs 0 1 0 9 0 0 3 0 -1.12.1.8.-1.-1.-1.-1.-1.-1 2  1 1 0.333.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1 0 0", new CList
            {
                Name = "MyNameIs",
                Slot = 2
            });
        }

        [PacketTest(typeof(Ok))]
        public void Ok_Test()
        {
            CreateAndCheckValues("OK", new Ok());
        }
    }
}