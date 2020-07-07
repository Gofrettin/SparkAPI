using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Spark.Core.Extension
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetImplementingTypes<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);
        }
    }
}