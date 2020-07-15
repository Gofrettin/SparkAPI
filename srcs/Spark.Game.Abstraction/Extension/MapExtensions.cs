using System;
using Spark.Core;

namespace Spark.Game.Abstraction.Extension
{
    public static class MapExtensions
    {
        private static readonly Random Random = new Random(DateTime.Now.GetHashCode());

        public static Vector2D GetRandomPosition(this IMap map, Vector2D point, int max = 1)
        {
            Vector2D output;
            int attempt = 0;
            do
            {
                output = new Vector2D(point.X + Random.Next(-max, max), point.Y + Random.Next(-max, max));
                attempt++;
            } while (!map.IsWalkable(output) && attempt < max * 6);

            return attempt == (max * 6) ? point : output;
        }
    }
}