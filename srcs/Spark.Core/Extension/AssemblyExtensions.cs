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

        public static IEnumerable<Type> BaseTypes(this Type type)
        {
            Type t = type;
            while (true)
            {
                t = t.BaseType;
                if (t == null)
                {
                    break;
                }

                yield return t;
            }
        }

        public static bool AnyBaseType(this Type type, Func<Type, bool> predicate)
        {
            return type.BaseTypes().Any(predicate);
        }

        public static bool IsParticularGeneric(this Type type, Type generic)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == generic;
        }
    }
}