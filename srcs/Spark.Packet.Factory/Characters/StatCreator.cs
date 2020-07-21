using Spark.Packet.Characters;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Characters
{
    public class StatCreator : PacketCreator<Stat>
    {
        public override string Header { get; } = "stat";
        
        public override Stat Create(string[] content)
        {
            return new Stat
            {
                Hp = content[0].ToInt(),
                Mp = content[1].ToInt(),
                MaxHp = content[2].ToInt(),
                MaxMp = content[3].ToInt()
            };
        }
    }
}