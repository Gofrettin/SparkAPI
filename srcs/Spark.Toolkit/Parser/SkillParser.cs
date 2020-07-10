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
    public class SkillParser : IParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };
        
        public void Parse(DirectoryInfo input, DirectoryInfo output)
        {
            FileInfo skillDat = input.GetFiles().FirstOrDefault(x => x.Name.Equals("Skill.dat", StringComparison.InvariantCultureIgnoreCase));
            if (skillDat == null)
            {
                Logger.Warn("Can't found Skill.dat file, skipping skills parsing");
                return;
            }
            
            TextContent content = TextReader.FromFile(skillDat)
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();

            IEnumerable<TextRegion> regions = content.GetRegions("VNUM");
            
            var skills = new Dictionary<int, SkillData>();
            foreach (TextRegion region in regions)
            {
                int gameKey = region.GetLine("VNUM").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);
                
                skills[gameKey] = new SkillData
                {
                    NameKey = nameKey,
                };
            }
            
            using (StreamWriter file = File.CreateText(Path.Combine(output.FullName, "skills.json")))
            {
                _serializer.Serialize(file, skills);
            }
            
            Logger.Info($"Successfully parsed {skills.Count} skills");
        }
    }
}