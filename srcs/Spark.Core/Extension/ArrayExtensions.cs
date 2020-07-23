using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark.Core.Extension
{
    public static class ArrayExtensions
    {
        public static T[] Slice<T>(this T[] array, int startIndex, int length)
        {
            return array.Skip(startIndex).Take(length).ToArray();
        }

        public static bool RemoveIf<T>(this List<T> list, Predicate<T> predicate)
        {
            T value = list.FirstOrDefault(predicate.Invoke);
            if (value == null)
            {
                return false;
            }

            return list.Remove(value);
        }
    }
}