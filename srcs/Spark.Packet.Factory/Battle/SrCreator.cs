using Spark.Packet.Battle;
using Spark.Packet.Extension;

namespace Spark.Packet.Factory.Battle
{
    public class SrCreator : PacketCreator<Sr>
    {
        public override string Header { get; } = "sr";

        public override Sr Create(string[] content)
        {
            return new Sr
            {
                CastId = content[0].ToInt()
            };
        }
    }
}