using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spark.Toolkit
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect args length");
                return;
            }
            
            string input = args[0];
            string output = args[1];

            if (!Directory.Exists(input))
            {
                Console.WriteLine($"Can't found {input} directory");
                return;
            }

            if (!Directory.Exists(output))
            {
                Console.WriteLine($"Can't found {output} directory");
                return;
            }

            MapDirectoryParser[] parsers =
            {
                new MapDirectoryParser()
            };

            IEnumerable<string> directories = Directory.EnumerateDirectories(input);
            foreach (string directory in directories)
            {
                string directoryName = Path.GetFileName(directory);
                IDirectoryParser parser = parsers.FirstOrDefault(x => x.Name == directoryName);
                if (parser == null)
                {
                    Console.WriteLine($"No parser for {directoryName}");
                    continue;
                }
                
                parser.Parse(input, output);
            }
        }
    }
}