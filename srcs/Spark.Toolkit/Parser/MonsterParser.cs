using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using Spark.Database.Data;
using Spark.Toolkit.Reader;
using TextReader = Spark.Toolkit.Reader.TextReader;

namespace Spark.Toolkit.Parser
{
    public class MonsterParser : IParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };
        
        public void Parse(DirectoryInfo input, DirectoryInfo output)
        {
            FileInfo monsterDat = input.GetFiles().FirstOrDefault(x => x.Name.Equals("Monster.dat", StringComparison.InvariantCultureIgnoreCase));
            if (monsterDat == null)
            {
                Logger.Warn("Can't found Monster.dat file, skipping monsters parsing");
                return;
            }
            
            TextContent content = TextReader.FromFile(monsterDat)
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();

            IEnumerable<TextRegion> regions = content.GetRegions("VNUM");
            
            var monsters = new Dictionary<int, MonsterData>();
            foreach (TextRegion region in regions)
            {
                int gameKey = region.GetLine("VNUM").GetValue<int>(1);
                int level = region.GetLine("LEVEL").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);
                
                monsters[gameKey] = new MonsterData
                {
                    NameKey = nameKey,
                    Level = level
                };
            }
            
            using (StreamWriter file = File.CreateText(Path.Combine(output.FullName, "monsters.json")))
            {
                _serializer.Serialize(file, monsters);
            }
            
            Logger.Info($"Successfully parsed {monsters.Count} monsters");
        }
    }
}