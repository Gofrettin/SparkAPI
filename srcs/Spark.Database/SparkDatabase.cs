using System;
using System.IO;
using System.Reflection;
using NLog;
using Spark.Database.Data;

namespace Spark.Database
{
    public sealed class SparkDatabase : IDatabase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public string DatabaseDirectory { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "Database");
        
        public IRepository<MapData> Maps { get; }
        public IRepository<MonsterData> Monsters { get; }
        public IRepository<ItemData> Items { get; }
        public IRepository<SkillData> Skills { get; }

        public SparkDatabase()
        {
            Maps = new Repository<MapData>(Path.Combine(DatabaseDirectory, "maps.json"));
            Monsters = new Repository<MonsterData>(Path.Combine(DatabaseDirectory, "monsters.json"));
            Items = new Repository<ItemData>(Path.Combine(DatabaseDirectory, "items.json"));
            Skills = new Repository<SkillData>(Path.Combine(DatabaseDirectory, "skills.json"));
        }
        
        public void Load()
        {
            if (!Directory.Exists(DatabaseDirectory))
            {
                throw new IOException($"Can't load database missing {DatabaseDirectory} directory");
            }
            
            Logger.Info("Loading maps");
            Maps.Load();
            
            Logger.Info("Loading monsters");
            Monsters.Load();
            
            Logger.Info("Loading items");
            Items.Load();
            
            Logger.Info("Loading skills");
            Skills.Load();
        }
    }
}