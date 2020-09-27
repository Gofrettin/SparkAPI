using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Database.Reader;
using TextReader = Spark.Database.Reader.TextReader;

namespace Spark.Toolkit.Parser
{
    public class SkillParser : IParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly JsonSerializer serializer = new JsonSerializer
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
                TextLine vnumLine = region.GetLine(x => x.StartWith("VNUM"));
                TextLine nameLine = region.GetLine(x => x.StartWith("NAME"));
                TextLine typeLine = region.GetLine(x => x.StartWith("TYPE"));
                TextLine dataLine = region.GetLine(x => x.StartWith("DATA"));
                TextLine targetLine = region.GetLine(x => x.StartWith("TARGET"));

                int gameKey = vnumLine.GetValue<int>(1);

                skills[gameKey] = new SkillData
                {
                    NameKey = nameLine.GetValue(1),
                    Category = (SkillCategory)typeLine.GetValue<int>(1),
                    CastId = typeLine.GetValue<int>(2),
                    CastTime = dataLine.GetValue<int>(5),
                    Cooldown = dataLine.GetValue<int>(6),
                    MpCost = dataLine.GetValue<int>(7),
                    Target = (SkillTarget)targetLine.GetValue<int>(1),
                    HitType = (HitType)targetLine.GetValue<int>(2),
                    Range = targetLine.GetValue<short>(3),
                    ZoneRange = targetLine.GetValue<short>(4),
                    SkillType = (SkillType)targetLine.GetValue<int>(5)
                };
            }

            using (StreamWriter file = File.CreateText(Path.Combine(output.FullName, "skills.json")))
            {
                serializer.Serialize(file, skills);
            }

            Logger.Info($"Successfully parsed {skills.Count} skills");
        }
    }
}