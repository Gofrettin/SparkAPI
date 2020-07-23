using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using NLog;
using Spark.Core.Extension;

namespace Spark.Database
{
    public sealed class Repository<T> : IRepository<T>
    {
        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Repository(string path) => Path = path;

        public string Path { get; }
        public ReadOnlyDictionary<int, T> Values { get; private set; }

        public T GetValue(int id)
        {
            return Values.GetValueOrDefault(id);
        }

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

            Logger.Debug($"Loaded {Values.Count} values");
        }
    }
}