using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Event.Entities;
using Spark.Event.Entities.Player;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;
using Spark.Packet.Processor.Entities;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class EntityProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(CModeProcessor))]
        [EventTest(typeof(SpecialistWearEvent))]
        public void CMode_Wear_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreatePlayer();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new CMode
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id,
                    MorphId = 11,
                    MorphUpgrade = 13,
                    MorphDesign = 11,
                    MorphBonus = 0,
                    Size = 10
                });

                Check.That(map.Entities).Contains(entity);
                Check.That(entity.MorphId).IsEqualTo(11);

                context.IsEventEmitted<SpecialistWearEvent>(x => x.Entity.Equals(entity) && x.SpecialistId == 11);
            }
        }

        [ProcessorTest(typeof(GpProcessor))]
        public void Gp_Test()
        {
            using (GameContext context = CreateContext())
            {
                IMap map = TestFactory.CreateMap(context.Character);
                
                context.Process(new Gp
                {
                    Position = new Vector2D(50, 50),
                    PortalId = 1,
                    PortalType = PortalType.MapPortal,
                    DestinationId = 2
                });

                Check.That(map.Portals).HasElementThatMatches(x => x.Id == 1 && x.PortalType == PortalType.MapPortal && x.DestinationId == 2 && x.Position.Equals(new Vector2D(50, 50)));
            }
        }

        [ProcessorTest(typeof(CModeProcessor))]
        [EventTest(typeof(SpecialistUnwearEvent))]
        public void CMode_Unwear_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreatePlayer();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new CMode
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id,
                    MorphId = 0,
                    MorphUpgrade = 13,
                    MorphDesign = 11,
                    MorphBonus = 0,
                    Size = 10
                });

                Check.That(map.Entities).Contains(entity);
                Check.That(entity.MorphId).IsEqualTo(0);

                context.IsEventEmitted<SpecialistUnwearEvent>(x => x.Entity.Equals(entity));
            }
        }

        [ProcessorTest(typeof(CondProcessor))]
        public void Cond_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreatePlayer();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new Cond
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id,
                    CanAttack = true,
                    CanMove = true,
                    Speed = 12
                });

                Check.That(map.Entities).Contains(entity);
                Check.That(entity.Speed).IsEqualTo(12);
            }
        }

        [ProcessorTest(typeof(DirProcessor))]
        public void Dir_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreatePlayer();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new Dir
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id,
                    Direction = Direction.North
                });

                Check.That(map.Entities).Contains(entity);
                Check.ThatEnum(entity.Direction).IsEqualTo(Direction.North);
            }
        }

        [ProcessorTest(typeof(InProcessor))]
        [EventTest(typeof(EntitySpawnEvent))]
        public void In_As_Npc_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                IMap map = TestFactory.CreateMap(character);

                context.Process(new In
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
                });

                INpc npc = map.GetEntity<INpc>(EntityType.Npc, 9326);

                Check.That(npc).IsNotNull();
                Check.That(npc.Map).IsNotNull();
                Check.That(npc.MonsterKey).IsEqualTo(3093);
                Check.That(npc.Position).IsEqualTo(new Vector2D(50, 28));
                Check.That(npc.Direction).IsEqualTo(Direction.South);
                Check.That(npc.HpPercentage).IsEqualTo(100);
                Check.That(npc.MpPercentage).IsEqualTo(100);
                Check.That(npc.Name).IsEmpty();

                context.IsEventEmitted<EntitySpawnEvent>(x => x.Entity.Id == 9326 && x.Entity.EntityType == EntityType.Npc && x.Map.Equals(map));
            }
        }

        [ProcessorTest(typeof(InProcessor))]
        [EventTest(typeof(EntitySpawnEvent))]
        public void In_As_Player_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                IMap map = TestFactory.CreateMap(character);

                context.Process(new In
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
                });

                IPlayer player = map.GetEntity<IPlayer>(EntityType.Player, 1204334);

                Check.That(player).IsNotNull();
                Check.That(player.Map).IsNotNull();
                Check.That(player.Position).IsEqualTo(new Vector2D(69, 44));
                Check.That(player.Direction).IsEqualTo(Direction.South);
                Check.That(player.HpPercentage).IsEqualTo(89);
                Check.That(player.MpPercentage).IsEqualTo(100);
                Check.That(player.Name).IsEqualTo("Makalash");
                Check.That(player.Class).IsEqualTo(Class.Archer);
                Check.That(player.Gender).IsEqualTo(Gender.Male);

                context.IsEventEmitted<EntitySpawnEvent>(x => x.Entity.Id == 1204334 && x.Entity.EntityType == EntityType.Player && x.Map.Equals(map));
            }
        }

        [ProcessorTest(typeof(InProcessor))]
        [EventTest(typeof(EntitySpawnEvent))]
        public void In_As_MapObject_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                IMap map = TestFactory.CreateMap(character);

                context.Process(new In
                {
                    EntityType = EntityType.MapObject,
                    GameKey = 1241,
                    EntityId = 708392,
                    Position = new Vector2D(17, 9),
                    MapObject = new In.MapObjectInfo
                    {
                        Amount = 80,
                        IsQuestRelative = false,
                        Owner = 0
                    }
                });

                IMapObject mapObject = map.GetEntity<IMapObject>(EntityType.MapObject, 708392);

                Check.That(mapObject).IsNotNull();
                Check.That(mapObject.Map).IsNotNull();
                Check.That(mapObject.ItemKey).IsEqualTo(1241);
                Check.That(mapObject.Position).IsEqualTo(new Vector2D(17, 9));
                Check.That(mapObject.Name).IsEmpty();
                Check.That(mapObject.Amount).IsEqualTo(80);

                context.IsEventEmitted<EntitySpawnEvent>(x => x.Entity.Id == 708392 && x.Entity.EntityType == EntityType.MapObject && x.Map.Equals(map));
            }
        }

        [ProcessorTest(typeof(MvProcessor))]
        [EventTest(typeof(EntityMoveEvent))]
        public void Mv_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreatePlayer();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new Mv
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id,
                    Position = new Vector2D(120, 143),
                    Speed = 10
                });

                Check.That(map.Entities).Contains(entity);
                Check.That(entity.Position).IsEqualTo(new Vector2D(120, 143));

                context.IsEventEmitted<EntityMoveEvent>(x => x.Entity.Equals(entity) && x.From.Equals(new Vector2D(0, 0)) && x.To.Equals(new Vector2D(120, 143)));
            }
        }

        [ProcessorTest(typeof(OutProcessor))]
        [EventTest(typeof(EntityLeaveEvent))]
        public void Out_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                ILivingEntity entity = TestFactory.CreateMonster();

                IMap map = TestFactory.CreateMap(character, entity);

                context.Process(new Out
                {
                    EntityType = entity.EntityType,
                    EntityId = entity.Id
                });

                Check.That(map.Entities).Not.Contains(entity);
                Check.That(entity.Map).IsNull();

                context.IsEventEmitted<EntityLeaveEvent>(x => x.Entity.Equals(entity) && x.Map.Equals(map));
            }
        }
    }
}