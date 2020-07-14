using NFluent;
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
        
        protected override void CheckResult()
        {
            Check.That(Client.Character.Name).IsEqualTo("Isha");
            Check.That(Client.Character.Id).IsEqualTo(123456);
        }
    }
}