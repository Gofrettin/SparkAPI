using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Processor.CharacterSelector
{
    public class CListEndProcessorTest : ProcessorTest<CListEnd>
    {
        protected override CListEnd Packet { get; } = new CListEnd();
        
        protected override void CheckOutput()
        {
            
        }
    }
}