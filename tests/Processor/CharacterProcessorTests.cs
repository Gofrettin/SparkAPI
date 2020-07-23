using System.Collections.Generic;
using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Event.Characters;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Characters;
using Spark.Packet.Entities;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class CharacterProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(At))]
        [EventTest(typeof(MapJoinEvent))]
        public void At_Without_Map_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                IMap currentMap = character.Map;
                
                context.Process(new At
                {
                    MapId = 2544,
                    Position = new Vector2D(24, 42),
                    Direction = Direction.South
                });

                Check.That(character.Map).IsNotNull();
                Check.That(character.Map.Id).IsEqualTo(2544);
                Check.That(character.Position).IsEqualTo(new Vector2D(24, 42));
                Check.That(character.Direction).IsEqualTo(Direction.South);

                context.Verify<MapJoinEvent>(x => x.Map.Equals(character.Map));
            }
        }
        
        [ProcessorTest(typeof(At))]
        [EventTest(typeof(MapJoinEvent))]
        [EventTest(typeof(MapLeaveEvent))]
        public void At_With_Map_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Map = TestFactory.CreateMap();
                
                ICharacter character = context.Character;
                IMap currentMap = character.Map;
                
                context.Process(new At
                {
                    MapId = 2544,
                    Position = new Vector2D(24, 42),
                    Direction = Direction.South
                });

                Check.That(character.Map).IsNotNull();
                Check.That(character.Map.Id).IsEqualTo(2544);
                Check.That(character.Position).IsEqualTo(new Vector2D(24, 42));
                Check.That(character.Direction).IsEqualTo(Direction.South);

                context.Verify<MapJoinEvent>(x => x.Map.Equals(character.Map));
                context.Verify<MapLeaveEvent>(x => x.Map.Equals(currentMap));
            }
        }

        [ProcessorTest(typeof(Lev))]
        public void Lev_Test()
        {
            using (GameContext context = CreateContext())
            {
                context.Process(new Lev
                {
                    Level = 80
                });

                Check.That(context.Character.Level).IsEqualTo(80);
            }
        }
        
        [ProcessorTest(typeof(CInfo))]
        [EventTest(typeof(CharacterInitializedEvent))]
        public void CInfo_Test()
        {
            using (GameContext context = CreateContext(false))
            {
                context.Process(new CInfo
                {
                    Name = "Isha",
                    Id = 123456
                });

                ICharacter character = context.Character;

                Check.That(character).IsNotNull();
                Check.That(character.Name).IsEqualTo("Isha");
                Check.That(character.Id).IsEqualTo(123456);

                context.Verify<CharacterInitializedEvent>(x => x.Character.Equals(character));
            }
        }

        [ProcessorTest(typeof(Ski))]
        public void Ski_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                context.Process(new Ski
                {
                    Skills = new HashSet<int>() { 240, 241, 242, 243 }
                });

                Check.That(character.Skills).CountIs(4);
                Check.That(character.Skills).HasElementThatMatches(x => x.SkillKey == 242);
            }
        }

        [ProcessorTest(typeof(Stat))]
        [EventTest(typeof(StatChangeEvent))]
        public void Stat_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                context.Process(new Stat
                {
                    Hp = 1000,
                    MaxHp = 2000,
                    Mp = 3000,
                    MaxMp = 4000
                });

                Check.That(character.Hp).IsEqualTo(1000);
                Check.That(character.MaxHp).IsEqualTo(2000);
                Check.That(character.Mp).IsEqualTo(3000);
                Check.That(character.MaxMp).IsEqualTo(4000);

                Check.That(character.HpPercentage).IsEqualTo(50);
                Check.That(character.MpPercentage).IsEqualTo(75);

                context.Verify<StatChangeEvent>(x => x.Character.Equals(character));
            }
        }
    }
}