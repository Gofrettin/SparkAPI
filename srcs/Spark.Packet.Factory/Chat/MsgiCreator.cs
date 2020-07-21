using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Chat;

namespace Spark.Packet.Factory.Chat
{
    public class MsgiCreator : PacketCreator<Msgi>
    {
        public override string Header { get; } = "msgi";
        
        public override Msgi Create(string[] content)
        {
            return new Msgi
            {
                MessageType = content[0].ToEnum<MessageType>(),
                MessageId = content[1].ToInt(),
                Parameters = content.Skip(2).Select(x => x.ToInt())
            };
        }
    }
}