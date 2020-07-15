using Moq;
using NFluent;
using Spark.Event.Characters;
using Spark.Game.Abstraction;
using Spark.Packet.Characters;

namespace Spark.Tests.Processor.Characters
{
    public class MapOutProcessorTest : ProcessorTest<MapOut>
    {
        protected override MapOut Packet { get; } = new MapOut();

        public MapOutProcessorTest()
        {
            Map.AddEntity(Client.Character);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Client.Character.Map).IsNull();
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<MapLeaveEvent>(s => s.Map.Equals(Map))), Times.Once);
        }
    }
}