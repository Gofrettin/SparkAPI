using Spark.Core.Enum;
using Spark.Packet.Battle;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Battle
{
    public class SuCreator : PacketCreator<Su>
    {
        public override string Header { get; } = "su";

        public override Su Create(string[] content)
        {
            return new Su
            {
                CasterType = content[0].ToEnum<EntityType>(),
                CasterId = content[1].ToLong(),
                TargetType = content[2].ToEnum<EntityType>(),
                TargetId = content[3].ToLong(),
                SkillKey = content[4].ToInt(),
                IsTargetAlive = content[10].ToBool(),
                HpPercentage = content[11].ToInt(),
                Damage = content[12].ToInt()
            };
        }
    }
}