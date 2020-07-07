using System;
using System.IO;
using System.Reflection;
using NLog;
using Spark.Database.Data;

namespace Spark.Database
{
    public interface IGameDataProvider
    {
        MapData GetMapData(int mapId);

        void EnsureCreated();
    }

    public class GameDataProvider : IGameDataProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public GameDataProvider()
        {
            Folder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Failed to get assembly folder"), "Data");
            MapDataProvider = new MapDataProvider(Folder);
        }

        public string Folder { get; }
        public MapDataProvider MapDataProvider { get; }

        public MapData GetMapData(int mapId) => MapDataProvider.GetMapData(mapId);

        public void EnsureCreated()
        {
            Logger.Info("Checking if all directories & files exists");

            CheckDirectory(Folder);
            CheckDirectory(MapDataProvider.Folder);
        }

        private static bool CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new IOException($"Can't found {path}");
            }

            return true;
        }
    }
}