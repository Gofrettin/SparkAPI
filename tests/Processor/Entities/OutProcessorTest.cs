using NFluent;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class OutProcessorTest : ProcessorTest<Out>
    {
        protected override Out Packet { get; }= new Out
        {
            EntityType = EntityType.Npc,
            EntityId = 123
        };
        
        public IEntity Entity { get; } = new Npc(123, 1, new MonsterData());

        public OutProcessorTest()
        {
            Map.AddEntity(Client.Character);
            Map.AddEntity(Entity);
        }
        
        protected override void CheckResult()
        {
            Check.That(Map.Entities).Not.Contains(Entity);
            Check.That(Map.Entities).Contains(Client.Character);
        }
    }
}