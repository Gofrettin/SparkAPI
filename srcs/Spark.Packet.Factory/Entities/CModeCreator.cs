using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class CModeCreator : PacketCreator<CMode>
    {
        public override string Header { get; } = "c_mode";
        
        public override CMode Create(string[] content)
        {
            var packet = new CMode
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                MorphId = content[2].ToShort(),
                MorphUpgrade = content[3].ToByte(),
                MorphDesign = content[4].ToShort(),
            };

            if (packet.EntityType == EntityType.Player)
            {
                packet.MorphBonus = content[5].ToByte();
                packet.Size = content[6].ToByte();
            }

            return packet;
        }
    }
}