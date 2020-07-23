using System.Collections.Generic;
using Spark.Core.Enum;
using Spark.Event.Notification;
using Spark.Packet.Chat;
using Spark.Packet.Processor.Chat;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class ChatProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(MsgiProcessor))]
        [EventTest(typeof(ServerMessageReceivedEvent))]
        public void Msgi_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Msgi
                {
                    MessageType = MessageType.Classic,
                    MessageId = 1898,
                    Parameters = new [] { 0, 0, 0, 0, 0 }
                });
                
                context.IsEventEmitted<ServerMessageReceivedEvent>(x => x.MessageType == MessageType.Classic && x.MessageId == 1898);
            }
        }

        [ProcessorTest(typeof(SayiProcessor))]
        [EventTest(typeof(ChatMessageReceivedEvent))]
        public void Sayi_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Sayi
                {
                    EntityType = EntityType.Player,
                    EntityId = 123456,
                    Color = MessageColor.Red,
                    MessageId = 123,
                    Parameters = new [] { 0, 0, 0, 0, 0 }
                });
                
                context.IsEventEmitted<ChatMessageReceivedEvent>(x => x.Color == MessageColor.Red && x.MessageId == 123);
            }
        }
    }
}