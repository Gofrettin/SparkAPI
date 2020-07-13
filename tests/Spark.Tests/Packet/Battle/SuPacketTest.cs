using Spark.Core.Enum;
using Spark.Packet.Battle;

namespace Spark.Tests.Packet.Battle
{
    public class SuSelfPacketTest : PacketTest<Su>
    {
        protected override string Packet { get; } = "su 1 123456 1 123456 254 350 24 281 0 0 1 100 0 -1 0";

        protected override Su Excepted { get; } = new Su
        {
            CasterType = EntityType.Player,
            CasterId = 123456,
            TargetType = EntityType.Player,
            TargetId = 123456,
            SkillKey = 254,
            Damage = 0,
            IsTargetAlive = true,
            HpPercentage = 100
        };
    }

    public class SuTargetPacketTest : PacketTest<Su>
    {
        protected override string Packet { get; } = "su 1 123456 3 2128 242 50 11 287 0 0 0 0 14955 3 0";

        protected override Su Excepted { get; } = new Su
        {
            CasterType = EntityType.Player,
            CasterId = 123456,
            TargetType = EntityType.Monster,
            TargetId = 2128,
            SkillKey = 242,
            Damage = 14955,
            IsTargetAlive = false,
            HpPercentage = 0
        };
    }
}