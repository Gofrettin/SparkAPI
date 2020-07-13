using NFluent;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class CondProcessorTest : ProcessorTest<Cond>
    {
        protected override Cond Packet { get; } = new Cond
        {
            EntityType = EntityType.Player,
            EntityId = 123456,
            CanAttack = true,
            CanMove = true,
            Speed = 12
        };

        public CondProcessorTest()
        {
            Map.AddEntity(Client.Character);
        }

        protected override void CheckResult()
        {
            Check.That(Client.Character.Speed).IsEqualTo(12);
        }
    }
}