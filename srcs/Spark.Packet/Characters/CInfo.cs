using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("c_info")]
    public class CInfo : IPacket
    {
        public string Name { get; set; }
        public long Id { get; set; }

        public void Construct(string[] packet)
        {
            Name = packet[0];
            Id = packet[5].ToLong();
        }
    }
}