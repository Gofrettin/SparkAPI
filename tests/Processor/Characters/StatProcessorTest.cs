using NFluent;
using Spark.Packet.Characters;

namespace Spark.Tests.Processor.Characters
{
    public class StatProcessorTest : ProcessorTest<Stat>
    {
        protected override Stat Packet { get; } = new Stat
        {
            Hp = 1000,
            Mp = 2000,
            MaxHp = 3000,
            MaxMp = 4000
        };
        
        protected override void CheckResult()
        {
            Check.That(Client.Character.Hp).IsEqualTo(1000);
            Check.That(Client.Character.Mp).IsEqualTo(2000);
            Check.That(Client.Character.MaxHp).IsEqualTo(3000);
            Check.That(Client.Character.MaxMp).IsEqualTo(4000);

            Check.That(Client.Character.HpPercentage).IsEqualTo(33);
            Check.That(Client.Character.MpPercentage).IsEqualTo(50);
        }
    }
}