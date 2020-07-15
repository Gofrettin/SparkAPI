using Spark.Packet.Login;

namespace Spark.Tests.Processor.Login
{
    public class FailcProcessorTest : ProcessorTest<Failc>
    {
        protected override Failc Packet { get; } = new Failc
        {
            Reason = 1
        };
        
        protected override void CheckOutput()
        {
            
        }
    }
}