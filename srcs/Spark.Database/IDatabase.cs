using Spark.Database.Data;

namespace Spark.Database
{
    public interface IDatabase
    {
        string Directory { get; }

        IRepository<MapData> Maps { get; }
        IRepository<MonsterData> Monsters { get; }
        IRepository<ItemData> Items { get; }
        IRepository<SkillData> Skills { get; }

        void Load();
    }
}