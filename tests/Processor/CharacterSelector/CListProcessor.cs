using System;
using NFluent;
using Spark.Core.Option;
using Spark.Packet.CharacterSelector;

namespace Spark.Tests.Processor.CharacterSelector
{
    public class CListProcessor : ProcessorTest<CList>
    {
        protected override CList Packet { get; }= new CList
        {
            Slot = 2,
            Name = "MyNameIs"
        };

        public CListProcessor()
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