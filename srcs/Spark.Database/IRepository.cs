using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

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

            Values = JsonConvert.DeserializeObject<ReadOnlyDictionary<int, T>>(Path);
        }
    }
}