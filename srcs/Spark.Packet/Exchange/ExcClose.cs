using Spark.Packet.Extension;

namespace Spark.Packet.Exchange
{
    [Packet("exc_close")]
    public class ExcClose : IPacket
    {
        public int Undefined { get; set; }
        
        public void Construct(string[] packet)
        {
            Undefined = packet[0].ToInt();
        }
    }
}