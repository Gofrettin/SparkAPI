using Moq;
using NFluent;
using Spark.Event.Characters;
using Spark.Event.Notification;
using Spark.Packet.Characters;

namespace Spark.Tests.Processor.Characters
{
    public class CInfoProcessorTest : ProcessorTest<CInfo>
    {
        protected override CInfo Packet { get; } = new CInfo
        {
            Name = "Isha",
            Id = 123456,
        };

        public CInfoProcessorTest()
        {
            Client.Character = null;
        }
        
        protected override void CheckOutput()
        {
            Check.That(Client.Character.Name).IsEqualTo("Isha");
            Check.That(Client.Character.Id).IsEqualTo(123456);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<CharacterInitializedEvent>(s => s.Character.Equals(Client.Character))), Times.Once);
        }
    }
}