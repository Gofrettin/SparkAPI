using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using Spark.Database.Data;
using Spark.Database.Reader;
using TextReader = Spark.Database.Reader.TextReader;

namespace Spark.Toolkit.Parser
{
    public class MapParser : IParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonSerializer serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        public void Parse(DirectoryInfo input, DirectoryInfo output)
        {
            DirectoryInfo mapDirectory = input.GetDirectories().FirstOrDefault(x => x.Name == "Maps");
            if (mapDirectory == null)
            {
                Logger.Warn("Can't found Maps directory, skipping map parsing");
                return;
            }

            IEnumerable<FileInfo> mapFiles = mapDirectory.EnumerateFiles("*.bin");
            if (!mapFiles.Any())
            {
                Logger.Warn("Can't found any map file in Maps directory, skipping map parsing");
                return;
            }

            FileInfo mapIdData = input.GetFiles().FirstOrDefault(x => x.Name == "MapIDData.dat");
            if (mapIdData == null)
            {
                Logger.Warn("Can't found MapIDData.dat file, skipping map parsing");
                return;
            }

            TextContent content = TextReader.FromFile(mapIdData)
                .SkipLines(x => x.StartsWith("DATA"))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent(' ')
                .TrimLines()
                .GetContent();

            var mapNameKeys = new Dictionary<int, string>();
            foreach (TextLine line in content.Lines)
            {
                int firstMapId = line.GetValue<int>(0);
                int secondMapId = line.GetValue<int>(1);
                string nameKey = line.GetValue(4);

                for (int i = firstMapId; i <= secondMapId; i++)
                {
                    mapNameKeys[i] = nameKey;
                }
            }

            var maps = new Dictionary<int, MapData>();
            foreach (FileInfo mapFile in mapFiles)
            {
                int mapId = int.Parse(Path.GetFileNameWithoutExtension(mapFile.Name));
                maps[mapId] = new MapData
                {
                    NameKey = mapNameKeys.GetValueOrDefault(mapId, "UNDEFINED"),
                    Grid = File.ReadAllBytes(mapFile.FullName)
                };
            }

            using (StreamWriter file = File.CreateText(Path.Combine(output.FullName, "maps.json")))
            {
                serializer.Serialize(file, maps);
            }

            Logger.Info($"Successfully parsed {maps.Count} maps");
        }
    }
}