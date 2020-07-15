using System;
using Spark.Database.Data;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Entities;
using Spark.Packet.Inventory;

namespace Spark.Tests
{
    public static class TestFactory
    {
        private static readonly Random Random = new Random();

        public static ISkill CreateSkill(Action<ISkill> setup = null)
        {
            int skillId = Random.Next(1, 200);
            int castId = Random.Next(1, 10);

            ISkill skill = new Skill(skillId, new SkillData
            {
                CastId = castId
            });

            setup?.Invoke(skill);

            return skill;
        }

        public static IMap CreateMap(params IEntity[] entities)
        {
            IMap map = new Map(Random.Next(1, 1000), new MapData
            {
                Grid = new byte[999],
                NameKey = "MyMap"
            });

            foreach (IEntity entity in entities)
            {
                map.AddEntity(entity);
            }

            return map;
        }

        public static IMonster CreateMonster(Action<IMonster> setup = null)
        {
            long monsterId = Random.Next(1, 999999);
            int monsterKey = Random.Next(1, 9999);
            IMonster monster = new Monster(monsterId, monsterKey, new MonsterData());

            setup?.Invoke(monster);
            return monster;
        }

        public static IPlayer CreatePlayer(Action<IPlayer> setup = null)
        {
            long playerId = Random.Next(1, 999999);
            IPlayer player = new Player(playerId);

            setup?.Invoke(player);
            return player;
        }

        public static ICharacter CreateCharacter(IClient client, Action<ICharacter> setup = null)
        {
            ICharacter character = new Character(123456, client);
            setup?.Invoke(character);
            return character;
        }
    }
}