using NFluent;
using Spark.Packet.Characters;

namespace Spark.Tests.Processor.Characters
{
    public class SkiProcessorTest : ProcessorTest<Ski>
    {
        protected override Ski Packet { get; } = new Ski
        {
            Skills = { 240, 241, 242, 243 }
        };
        
        protected override void CheckResult()
        {
            Check.That(Client.Character.Skills).CountIs(Packet.Skills.Count);
            Check.That(Client.Character.Skills).HasElementThatMatches(x => x.SkillKey == 240);
        }
    }
}