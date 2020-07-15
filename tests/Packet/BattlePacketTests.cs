using Spark.Core.Enum;
using Spark.Packet.Battle;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class BattlePacketTests : PacketTests
    {
        [PacketTest(typeof(Sr))]
        public void Sr_Test()
        {
            CreateAndCheckValues("sr 1", new Sr
            {
                CastId = 1
            });
        }

        [PacketTest(typeof(Su))]
        public void Su_Self_Test()
        {
            CreateAndCheckValues("su 1 123456 1 123456 254 350 24 281 0 0 1 100 0 -1 0", new Su
            {
                CasterType = EntityType.Player,
                CasterId = 123456,
                TargetType = EntityType.Player,
                TargetId = 123456,
                SkillKey = 254,
                Damage = 0,
                IsTargetAlive = true,
                HpPercentage = 100
            });
        }

        [PacketTest(typeof(Su))]
        public void Su_Other_Test()
        {
            CreateAndCheckValues("su 1 123456 3 2128 242 50 11 287 0 0 0 0 14955 3 0", new Su
            {
                CasterType = EntityType.Player,
                CasterId = 123456,
                TargetType = EntityType.Monster,
                TargetId = 2128,
                SkillKey = 242,
                Damage = 14955,
                IsTargetAlive = false,
                HpPercentage = 0
            });
        }

        [PacketTest(typeof(Bf))]
        public void Bf_Test()
        {
            CreateAndCheckValues("bf 1 123456 0.145.150 80", new Bf
            {
                EntityType = EntityType.Player,
                EntityId = 123456,
                BuffId = 145,
                Duration = 150
            });
        }
    }
}