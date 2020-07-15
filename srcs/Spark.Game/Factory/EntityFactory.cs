using Spark.Database;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Entities;

namespace Spark.Game.Factory
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IDatabase _database;

        public EntityFactory(IDatabase database) => _database = database;

        public IMonster CreateMonster(long entityId, int gameKey)
        {
            MonsterData data = _database.Monsters.GetValue(gameKey);
            if (data == null)
            {
                return default;
            }

            return new Monster(entityId, gameKey, data)
            {
                Name = string.Empty
            };
        }

        public INpc CreateNpc(long entityId, int gameKey)
        {
            MonsterData data = _database.Monsters.GetValue(gameKey);
            if (data == null)
            {
                return default;
            }

            return new Npc(entityId, gameKey, data)
            {
                Name = string.Empty
            };
        }

        public ICharacter CreateCharacter(long entityId, string name, IClient client) =>
            new Character(entityId, client)
            {
                Name = name
            };

        public IMapObject CreateMapObject(long entityId, int gameKey, int amount) =>
            new MapObject(entityId, gameKey, amount)
            {
                Name = string.Empty
            };

        public IPlayer CreatePlayer(long entityId, string name) =>
            new Player(entityId)
            {
                Name = name
            };
    }
}