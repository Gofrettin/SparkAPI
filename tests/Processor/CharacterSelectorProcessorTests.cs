using NFluent;
using Spark.Core.Configuration;
using Spark.Event.Login;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;
using Spark.Packet.Processor.CharacterSelector;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class CharacterSelectorProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(CListEndProcessor))]
        public void CListEnd_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new CListEnd());

                // Nothing to check just processing to make sure everything is ok
            }
        }

        [ProcessorTest(typeof(CListProcessor))]
        public void CList_Test()
        {
            using (GameContext context = CreateContext())
            {
                IClient client = context.Client;

                client.AddConfiguration(new LoginConfiguration());

                context.Process(new CList
                {
                    Name = "MyNameIs",
                    Slot = 2
                });

                LoginConfiguration configuration = client.GetConfiguration<LoginConfiguration>();

                Check.That(configuration).IsNotNull();
                Check.That(configuration.SelectableCharacters).HasElementThatMatches(x => x.Name.Equals("MyNameIs") && x.Slot == 2);
            }
        }

        [ProcessorTest(typeof(OkProcessor))]
        [EventTest(typeof(GameStartEvent))]
        public void Ok_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Ok());
                context.IsEventEmitted<GameStartEvent>();
            }
        }
    }
}