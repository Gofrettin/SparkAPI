using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using Spark.Database.Data;

namespace Spark.Database
{
    public class MapDataProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<int, MapData> _cache;

        public MapDataProvider(string dataFolder)
        {
            Folder = Path.Combine(dataFolder, "Maps");

            _cache = new Dictionary<int, MapData>();
        }

        public string Folder { get; }

        public MapData GetMapData(int mapId)
        {
            MapData mapData = _cache.GetValueOrDefault(mapId);
            if (mapData == null)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(Folder);
                string file = files.FirstOrDefault(x => Path.GetFileName(x) == $"{mapId}.json");

                if (file == null)
                {
                    Logger.Error($"Can't found map data file {mapId}.json");
                    return default;
                }

                mapData = JsonConvert.DeserializeObject<MapData>(File.ReadAllText(file));
                _cache[mapId] = mapData;
            }

            return mapData;
        }
    }
}