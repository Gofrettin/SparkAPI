using NFluent;
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
        
        protected override void CheckResult()
        {
            Check.That(Client.Character.Map).IsNull();
        }
    }
}