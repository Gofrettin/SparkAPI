using Spark.Packet.Characters;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Characters
{
    public class CInfoCreator : PacketCreator<CInfo>
    {
        public override string Header { get; } = "c_info";

        public override CInfo Create(string[] content)
        {
            return new CInfo
            {
                Name = content[0],
                Id = content[5].ToLong()
            };
        }
    }
}