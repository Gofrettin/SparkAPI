using Spark.Core.Enum;
using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class ServerMessageReceivedEvent : IEvent
    {
        public IClient Client { get; }
        public int MessageId { get; }
        public MessageType MessageType { get; }

        public ServerMessageReceivedEvent(IClient client, int messageId, MessageType messageType)
        {
            Client = client;
            MessageId = messageId;
            MessageType = messageType;
        }
    }
}