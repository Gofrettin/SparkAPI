using System.Linq;
using Spark.Packet.Extension;

namespace Spark.Packet.Notification
{
    [Packet("dlgi2")]
    public class Dlgi2 : IPacket
    {
        public ExchangeRequestInfo ExchangeRequest { get; set; }
        
        public void Construct(string[] packet)
        {
            string request = packet[0];
            
            if (request.StartsWith("#req_exc"))
            {
                ExchangeRequest = new ExchangeRequestInfo
                {
                    PlayerId = request.Split('^').Last().ToLong()
                };
            }
        }

        public class ExchangeRequestInfo
        {
            public long PlayerId { get; set; }
        }
    }
}