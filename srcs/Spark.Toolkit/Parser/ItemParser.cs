using System;
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
    public class ItemParser : IParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonSerializer serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        public void Parse(DirectoryInfo input, DirectoryInfo output)
        {
            FileInfo itemDat = input.GetFiles().FirstOrDefault(x => x.Name.Equals("Item.dat", StringComparison.InvariantCultureIgnoreCase));
            if (itemDat == null)
            {
                Logger.Warn("Can't found Item.dat file, skipping items parsing");
                return;
            }

            TextContent content = TextReader.FromFile(itemDat)
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();

            IEnumerable<TextRegion> regions = content.GetRegions("VNUM");

            var items = new Dictionary<int, ItemData>();
            foreach (TextRegion region in regions)
            {
                int gameKey = region.GetLine("VNUM").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);

                items[gameKey] = new ItemData
                {
                    NameKey = nameKey
                };
            }

            using (StreamWriter file = File.CreateText(Path.Combine(output.FullName, "items.json")))
            {
                serializer.Serialize(file, items);
            }

            Logger.Info($"Successfully parsed {items.Count} items");
        }
    }
}