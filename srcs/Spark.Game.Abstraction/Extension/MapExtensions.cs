using System;
using NLog;
using Spark.Core;

namespace Spark.Game.Abstraction.Extension
{
    public static class MapExtensions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Random Random = new Random(DateTime.Now.GetHashCode());

        public static Vector2D GetRandomPosition(this IMap map, Vector2D point, int max = 1)
        {
            Vector2D output;
            int attempt = 0;
            do
            {
                output = new Vector2D(point.X + Random.Next(-max, max + 1), point.Y + Random.Next(-max, max + 1));
                attempt++;
            } 
            while (!map.IsWalkable(output) && attempt < max * 6);

            return attempt == (max * 6) ? point : output;
        }
    }
}