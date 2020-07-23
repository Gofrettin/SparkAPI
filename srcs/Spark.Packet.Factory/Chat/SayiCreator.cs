using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Chat;

namespace Spark.Packet.Factory.Chat
{
    public class SayiCreator : PacketCreator<Sayi>
    {
        public override string Header { get; } = "sayi";
        
        public override Sayi Create(string[] content)
        {
            return new Sayi
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                Color = content[2].ToEnum<MessageColor>(),
                MessageId = content[3].ToInt(),
                Parameters = content.Skip(4).Select(x => x.ToInt())
            };
        }
    }
}