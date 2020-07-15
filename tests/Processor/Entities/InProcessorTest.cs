using Moq;
using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Event.Entities;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class InNpcProcessorTest : ProcessorTest<In>
    {
        protected override In Packet { get; } = new In
        {
            EntityType = EntityType.Npc,
            EntityId = 9326,
            GameKey = 3093,
            Position = new Vector2D(50, 28),
            Direction = Direction.South,
            Npc = new In.NpcInfo
            {
                HpPercentage = 100,
                MpPercentage = 100,
                Faction = Faction.Neutral,
                Name = string.Empty,
                Owner = null
            }
        };

        public INpc Entity { get; } = new Npc(9326, 3093, new MonsterData());

        public InNpcProcessorTest()
        {
            Map.AddEntity(Client.Character);
        }

        protected override void CheckOutput()
        {
            INpc npc = Map.GetEntity<INpc>(Packet.EntityType, Packet.EntityId);

            Check.That(npc).IsNotNull();
            Check.That(npc.Map).IsNotNull();
            Check.That(npc.MonsterKey).IsEqualTo(Packet.GameKey);
            Check.That(npc.Position).IsEqualTo(Packet.Position);
            Check.That(npc.Direction).IsEqualTo(Packet.Direction);
            Check.That(npc.HpPercentage).IsEqualTo(Packet.Npc.HpPercentage);
            Check.That(npc.MpPercentage).IsEqualTo(Packet.Npc.MpPercentage);
        }

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<EntitySpawnEvent>(s => s.Entity.Equals(Entity) && s.Map.Equals(Client.Character.Map))), Times.Once);
        }
    }
    
    public class InPlayerProcessorTest : ProcessorTest<In>
    {
        protected override In Packet { get; } = new In
        {
            Name = "Makalash",
            EntityId = 1204334,
            Position = new Vector2D(69, 44),
            Direction = Direction.South,
            EntityType = EntityType.Player,
            Player = new In.PlayerInfo
            {
                HpPercentage = 89,
                MpPercentage = 100,
                Class = Class.Archer,
                Gender = Gender.Male
            }
        };
        
        public IPlayer Entity { get; } = new Player(1204334)
        {
            Name = "Makalash"
        };
        
        public InPlayerProcessorTest()
        {
            Map.AddEntity(Client.Character);
        }

        protected override void CheckOutput()
        {
            IPlayer player = Map.GetEntity<IPlayer>(Packet.EntityType, Packet.EntityId);

            Check.That(player).IsNotNull();
            Check.That(player.Position).IsEqualTo(Packet.Position);
            Check.That(player.Direction).IsEqualTo(Packet.Direction);
            Check.That(player.HpPercentage).IsEqualTo(Packet.Player.HpPercentage);
            Check.That(player.MpPercentage).IsEqualTo(Packet.Player.MpPercentage);
            Check.That(player.Class).IsEqualTo(Packet.Player.Class);
            Check.That(player.Gender).IsEqualTo(Packet.Player.Gender);
        }
        
        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<EntitySpawnEvent>(s => s.Entity.Equals(Entity) && s.Map.Equals(Client.Character.Map))), Times.Once);
        }
    }
}