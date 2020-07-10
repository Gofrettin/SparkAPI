using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using NLog;

namespace Spark.Database
{
    public interface IRepository<T>
    {
        string Path { get; }
        ReadOnlyDictionary<int, T> Values { get; }
        T GetValue(int id);
        void Load();
    }

    public sealed class Repository<T> : IRepository<T>
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };
        
        public Repository(string path) => Path = path;
        
        public string Path { get; }
        public ReadOnlyDictionary<int, T> Values { get; private set; }

        public T GetValue(int id) => Values.GetValueOrDefault(id);

        public void Load()
        {
            if (!File.Exists(Path))
            {
                throw new IOException($"Failed to load repository missing {Path} file");
            }


            using (StreamReader stream = File.OpenText(Path))
            {
                Values = (ReadOnlyDictionary<int, T>)_serializer.Deserialize(stream, typeof(ReadOnlyDictionary<int, T>));
            }

            if (Values == null)
            {
                Logger.Error("Failed to load values");
                return;
            }
            
            Logger.Info($"Loaded {Values.Count} values");
        }
    }
}