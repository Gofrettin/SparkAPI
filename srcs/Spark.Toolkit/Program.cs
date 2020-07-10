using System;
using System.IO;
using NLog;
using Spark.Toolkit.Parser;

namespace Spark.Toolkit
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Logger.Error("Incorrect parameter length");
                return;
            }

            string path = args[0];
            if (!Directory.Exists(path))
            {
                Logger.Error($"Directory {path} doesn't exists");
                return;
            }
            
            var input = new DirectoryInfo(path);
            DirectoryInfo output = Directory.GetParent(path).CreateSubdirectory("Output");

            IParser[] parsers =
            {
                new MapParser(),
                new MonsterParser(),
                new SkillParser(),
                new ItemParser(), 
            };

            foreach (IParser parser in parsers)
            {
                parser.Parse(input, output);
            }

            Logger.Info("Parsing completed");
            Logger.Info($"Output can be found here : {output.FullName}");
        }
    }
}