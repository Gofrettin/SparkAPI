using Spark.Core.Enum;
using Spark.Event.Game.InstantCombat;
using Spark.Packet.Chat;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor.Event
{
    public class InstantCombatEventTests : ProcessorTests
    {
        [EventTest(typeof(InstantCombatStartEvent))]
        public void InstantCombatStartEvent_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Msgi
                {
                    MessageType = MessageType.Classic,
                    MessageId = 387
                });
                
                context.IsEventEmitted<InstantCombatStartEvent>();
            }
        }

        [EventTest(typeof(InstantCombatWaveComingEvent))]
        public void InstantCombatWaveComingEvent_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Msgi
                {
                    MessageType = MessageType.Classic,
                    MessageId = 1287
                });
                
                context.IsEventEmitted<InstantCombatWaveComingEvent>();
            }
        }

        [EventTest(typeof(InstantCombatWaveStartSoonEvent))]
        public void InstantCombatWaveStartSoonEvent_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Msgi
                {
                    MessageType = MessageType.Classic,
                    MessageId = 384
                });
                
                context.IsEventEmitted<InstantCombatWaveComingEvent>();
            }
        }

        [EventTest(typeof(InstantCombatRewardReceivedEvent))]
        public void InstantCombatRewardReceivedEvent_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Sayi()
                {
                    Color = MessageColor.Yellow,
                    MessageId = 2367
                });
                
                context.IsEventEmitted<InstantCombatRewardReceivedEvent>();
            }
        }

        [EventTest(typeof(InstantCombatRewardUnreceivedEvent))]
        public void InstantCombatRewardUnreceivedEvent_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Sayi
                {
                    Color = MessageColor.Red,
                    MessageId = 2282
                });
                
                context.IsEventEmitted<InstantCombatRewardUnreceivedEvent>();
            }
        }
    }
}