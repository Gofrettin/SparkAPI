using Moq;
using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Event.Characters;
using Spark.Event.Entities;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class MvPacketTest : ProcessorTest<Mv>
    {
        protected override Mv Packet { get; } = new Mv
        {
            EntityType = EntityType.Monster,
            EntityId = 2102,
            Position = new Vector2D(24, 143),
            Speed = 4
        };
        
        public ILivingEntity Entity { get; } = new Monster(2102, 123, new MonsterData());

        public MvPacketTest()
        {
            Map.AddEntity(Client.Character);
            Map.AddEntity(Entity);
        }

        protected override void CheckOutput()
        {
            Check.That(Entity.Id).IsEqualTo(2102);
            Check.That(Entity.EntityType).IsEqualTo(EntityType.Monster);
            Check.That(Entity.Position).IsEqualTo(new Vector2D(24, 143));
            Check.That(Entity.Speed).IsEqualTo(4);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<EntityMoveEvent>(s => s.Entity.Equals(Entity) && s.To.Equals(Packet.Position))), Times.Once);
        }
    }
}