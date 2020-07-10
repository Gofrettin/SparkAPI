using Spark.Database.Data;

namespace Spark.Database
{
    public interface IDatabase
    {
        IRepository<MapData> Maps { get; }
        IRepository<MonsterData> Monsters { get; }
        
        void Load();
    }
}