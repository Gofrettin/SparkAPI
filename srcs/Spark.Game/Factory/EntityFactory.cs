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

        public EntityFactory(IDatabase database)
        {
            _database = database;
        }

        public IMonster CreateMonster(long entityId, int gameKey)
        {
            MonsterData data = _database.Monsters.GetValue(gameKey);
            if (data == null)
            {
                data = MonsterData.Undefined;
            }
            
            return new Monster(entityId, gameKey, data);
        }

        public INpc CreateNpc(long entityId, int gameKey)
        {
            MonsterData data = _database.Monsters.GetValue(gameKey);
            if (data == null)
            {
                data = MonsterData.Undefined;
            }
            
            return new Npc(entityId, gameKey, data);
        }

        public ICharacter CreateCharacter(long entityId, string name, IClient client)
        {
            return new Character(entityId, client)
            {
                Name = name
            };
        }

        public IMapObject CreateMapObject(long entityId, int gameKey)
        {
            return new MapObject(entityId, gameKey);
        }

        public IPlayer CreatePlayer(long entityId)
        {
            return new Player(entityId);
        }
    }
}