using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Processor.CharacterSelector
{
    public class OkProcessorTest : ProcessorTest<Ok>
    {
        protected override Ok Packet { get; } = new Ok();
        
        protected override void CheckOutput()
        {
            
        }
    }
}