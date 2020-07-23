using Spark.Packet.Notification;

namespace Spark.Packet.Factory.Notification
{
    public class QNamliCreator : PacketCreator<QNamli>
    {
        public override string Header { get; } = "qnamli";
        
        public override QNamli Create(string[] content)
        {
            return new QNamli
            {
                Request = content[1]
            };
        }
    }
}