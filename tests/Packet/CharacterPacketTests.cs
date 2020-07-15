using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Characters;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class CharacterPacketTests : PacketTests
    {
        [PacketTest(typeof(At))]
        public void At_Test()
        {
            CreateAndCheckValues("at 123456 2544 24 42 2 0 53 1 -1", new At
            {
                EntityId = 123456,
                MapId = 2544,
                Position = new Vector2D(24, 42),
                Direction = Direction.South
            });
        }

        [PacketTest(typeof(CInfo))]
        public void CInfo_Test()
        {
            CreateAndCheckValues("c_info Isha - -1 2858.917 Family 123456 0 0 1 100 2 26 0 0 0 0 0 0 0", new CInfo
            {
                Name = "Isha",
                Id = 123456,
            });
        }

        [PacketTest(typeof(MapOut))]
        public void MapOut_Test()
        {
            CreateAndCheckValues("mapout", new MapOut());
        }

        [PacketTest(typeof(Ski))]
        public void Ski_Test()
        {
            CreateAndCheckValues("ski 240 241 240 241 242 243 244 245 247 248 249 250 251 252 254 256 236 694|4 704|0 279 280 281 282 283", new Ski
            {
                Skills = { 240, 241, 242, 243, 244, 245, 247, 248, 249, 250, 251, 252, 254, 256, 236, 694, 704, 279, 280, 281, 282, 283 }
            });
        }

        [PacketTest(typeof(Stat))]
        public void Stat_Test()
        {
            CreateAndCheckValues("stat 1000 2000 3000 4000", new Stat
            {
                Hp = 1000,
                MaxHp = 2000,
                Mp = 3000,
                MaxMp = 4000
            });
        }
    }
}