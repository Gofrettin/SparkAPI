namespace Spark.Packet.Notification
{
    [Packet("qnamli")]
    public class QNamli : IPacket
    {
        public string Request { get; set; }
        
        public void Construct(string[] packet)
        {
            Request = packet[1];
        }
    }
}