using Spark.Core.Enum;
using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class ChatMessageReceivedEvent : IEvent
    {
        public IClient Client { get; }
        public int MessageId { get; }
        public MessageColor Color { get; }

        public ChatMessageReceivedEvent(IClient client, int messageId, MessageColor color)
        {
            Client = client;
            MessageId = messageId;
            Color = color;
        }
    }
}