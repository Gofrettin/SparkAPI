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
}