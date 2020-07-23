using Spark.Packet.Characters;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Characters
{
    public class LevCreator : PacketCreator<Lev>
    {
        public override string Header { get; } = "lev";
        
        public override Lev Create(string[] content)
        {
            return new Lev
            {
                Level = content[0].ToInt()
            };
        }
    }
}