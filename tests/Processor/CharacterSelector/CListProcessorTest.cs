using System;
using NFluent;
using Spark.Core.Option;
using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Processor.CharacterSelector
{
    public class CListProcessorTest : ProcessorTest<CList>
    {
        protected override CList Packet { get; }= new CList
        {
            Slot = 2,
            Name = "MyNameIs"
        };

        public CListProcessorTest()
        {
            Client.AddOption(new LoginOption());
        }
        
        protected override void CheckResult()
        {
            LoginOption option = Client.GetOption<LoginOption>();

            Check.That(option.SelectableCharacters).HasElementThatMatches(x => x.Name == Packet.Name && x.Slot == Packet.Slot);
        }
    }
}