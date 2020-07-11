using System.Linq;

namespace Spark.Core.Extension
{
    public static class ArrayExtensions
    {
        public static T[] Slice<T>(this T[] array, int startIndex, int length) => array.Skip(startIndex).Take(length).ToArray();
    }
}