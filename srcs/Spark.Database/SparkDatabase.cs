using System;
using System.IO;
using System.Reflection;
using NLog;
using Spark.Database.Data;

namespace Spark.Database
{
    public sealed class FileDatabase : IDatabase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public FileDatabase()
        {
            Directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "Database");

            Maps = new Repository<MapData>(Path.Combine(Directory, "maps.json"));
            Monsters = new Repository<MonsterData>(Path.Combine(Directory, "monsters.json"));
            Items = new Repository<ItemData>(Path.Combine(Directory, "items.json"));
            Skills = new Repository<SkillData>(Path.Combine(Directory, "skills.json"));
        }

        public string Directory { get; }

        public IRepository<MapData> Maps { get; }
        public IRepository<MonsterData> Monsters { get; }
        public IRepository<ItemData> Items { get; }
        public IRepository<SkillData> Skills { get; }

        public void Load()
        {
            Logger.Info("Loading database");
            
            if (!System.IO.Directory.Exists(Directory))
            {
                throw new IOException($"Can't load database missing {Directory} directory");
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