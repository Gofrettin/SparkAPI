using Spark.Packet.Notification;

namespace Spark.Packet.Factory.Notification
{
    public class QNamli2Creator : PacketCreator<QNamli2>
    {
        public override string Header { get; } = "qnamli2";
        
        public override QNamli2 Create(string[] content)
        {
            var packet = new QNamli2();
            
            string identifier = content[1];
            if (identifier == "#rl")
            {
                packet.Raid = new QNamli2.RaidInfo
                {
                    Owner = content[5]
                };
            }

            return packet;
        }
    }
}