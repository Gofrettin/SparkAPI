using Spark.Core.Extension;
using Spark.Packet.Login;

namespace Spark.Packet.Factory.Login
{
    public class FailcCreator : PacketCreator<Failc>
    {
        public override string Header { get; } = "failc";
        
        public override Failc Create(string[] content)
        {
            return new Failc
            {
                Reason = content[0].ToByte()
            };
        }
    }
}