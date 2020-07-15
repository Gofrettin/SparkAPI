using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class CModePacketTest : PacketTest<CMode>
    {
        protected override string Packet { get; } = "c_mode 1 123456 11 13 11 0 10 0";

        protected override CMode Excepted { get; } = new CMode
        {
            EntityType = EntityType.Player,
            EntityId = 123456,
            MorphId = 11,
            MorphUpgrade = 13,
            MorphDesign = 11,
            MorphBonus = 0,
            Size = 10
        };
    }
}