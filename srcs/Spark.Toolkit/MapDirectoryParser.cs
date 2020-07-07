using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Spark.Database.Data;
using Spark.Toolkit.Reader;
using TextReader = Spark.Toolkit.Reader.TextReader;

namespace Spark.Toolkit
{
    public class MapDirectoryParser : IDirectoryParser
    {
        public string Name { get; } = "Maps";
        
        public void Parse(string input, string output)
        {
            string outputDirectory = Path.Combine(output, "Maps");
            DirectoryInfo inputDirectory = Directory.CreateDirectory(Path.Combine(input, Name));
            IEnumerable<FileInfo> files = inputDirectory.EnumerateFiles();

            FileInfo info = files.FirstOrDefault(x => x.Name == "maps.data");
            if (info == null)
            {
                Console.WriteLine($"Can't found maps.data file in {inputDirectory.FullName}");
                return;
            }

            TextContent content = TextReader.FromFile(info.FullName)
                .SkipEmptyLines()
                .SkipLines(x => x.StartsWith("DATA"))
                .SkipCommentedLines("#")
                .TrimLines()
                .SplitLineContent(' ')
                .GetContent();
            
            var nameKeys = new Dictionary<int, string>();
            foreach (TextLine line in content.Lines)
            {
                int firstMapId = line.GetValue<int>(0);
                int secondMapId = line.GetValue<int>(1);
                string nameKey = line.GetValue(4);

                for (int i = firstMapId; i <= secondMapId; i++)
                {
                    nameKeys[i] = nameKey;
                }
            }
            
            var maps = new Dictionary<int, MapData>();
            foreach (FileInfo file in files.Where(x => x.Name != "maps.data"))
            {
                if (file.Extension != ".bin")
                {
                    Console.WriteLine($"Unsupported file {file.Name}");
                    continue;
                }

                int mapId = int.Parse(Path.GetFileNameWithoutExtension(file.Name));
                maps[mapId] = new MapData
                {
                    NameKey = nameKeys.GetValueOrDefault(mapId) ?? string.Empty,
                    Grid = File.ReadAllBytes(file.FullName)
                };
            }

            Directory.CreateDirectory(outputDirectory);
            
            foreach (KeyValuePair<int, MapData> map in maps)
            {
                File.WriteAllText(Path.Combine(outputDirectory, $"{map.Key}.json"), JsonConvert.SerializeObject(map.Value));
            }
        }
    }
}