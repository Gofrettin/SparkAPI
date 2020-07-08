using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Abstraction.Factory
{
    public interface IEntityFactory
    {
        IMonster CreateMonster(long entityId, int gameKey);
        INpc CreateNpc(long entityId, int gameKey);
        ICharacter CreateCharacter(long entityId, string name, IClient client);
        IMapObject CreateMapObject(long entityId, int gameKey);
        IPlayer CreatePlayer(long entityId);
    }
}