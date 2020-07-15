using Moq;
using NFluent;
using Spark.Core.Enum;
using Spark.Event.Entities.Player;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class CModeSpWearProcessorTest : ProcessorTest<CMode>
    {
        protected override CMode Packet { get; } = new CMode
        {
            EntityType = EntityType.Player,
            EntityId = 1234,
            MorphId = 27,
            MorphDesign = 0,
            MorphUpgrade = 0,
            MorphBonus = 0
        };

        public IPlayer Player { get; }

        public CModeSpWearProcessorTest()
        {
            Player = new Player(1234);
            
            Map.AddEntity(Player);
            Map.AddEntity(Client.Character);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Player.MorphId).IsEqualTo(27);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<SpecialistWearEvent>(s => s.Entity.Equals(Player) && s.SpecialistId == 27)), Times.Once);
        }
    }
    
    public class CModeSpUnwearProcessorTest : ProcessorTest<CMode>
    {
        protected override CMode Packet { get; } = new CMode
        {
            EntityType = EntityType.Player,
            EntityId = 1234,
            MorphId = 0,
            MorphDesign = 0,
            MorphUpgrade = 0,
            MorphBonus = 0
        };
        
        public IPlayer Player { get; }

        public CModeSpUnwearProcessorTest()
        {
            Player = new Player(1234);
            Map.AddEntity(Player);
            Map.AddEntity(Client.Character);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Player.MorphId).IsEqualTo(0);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<SpecialistUnwearEvent>(s => s.Entity.Equals(Player))), Times.Once);
        }
    }
}