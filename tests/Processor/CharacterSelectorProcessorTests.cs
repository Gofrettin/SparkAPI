using NFluent;
using Spark.Core.Option;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class CharacterSelectorProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(CListEnd))]
        public void CListEnd_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new CListEnd());

                // Nothing to check just processing to make sure everything is ok
            }
        }

        [ProcessorTest(typeof(CList))]
        public void CList_Test()
        {
            using (GameContext context = CreateContext())
            {
                IClient client = context.Client;

                client.AddOption(new LoginOption());

                context.Process(new CList
                {
                    Name = "MyNameIs",
                    Slot = 2
                });

                LoginOption option = client.GetOption<LoginOption>();

                Check.That(option).IsNotNull();
                Check.That(option.SelectableCharacters).HasElementThatMatches(x => x.Name.Equals("MyNameIs") && x.Slot == 2);
            }
        }

        [ProcessorTest(typeof(Ok))]
        public void Ok_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Ok());

                // Nothing to check just processing to make sure everything is ok
            }
        }
    }
}