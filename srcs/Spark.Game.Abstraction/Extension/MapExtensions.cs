using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Entities;

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
                output = point.Randomize(max);
                attempt++;
            } 
            while (!map.IsWalkable(output) && attempt < max * 6);

            return attempt == (max * 6) ? point : output;
        }

        public static Vector2D FindTopDensityPosition(this IMap map)
        {
            int mapX = map.Height;
            int mapY = map.Width;

            int x = -1;
            int y = -1;
            double max = -1;

            IEnumerable<Vector2D> positions = map.Players.Select(s => s.Position);
            for (int cy = 0; cy < mapY; cy++)
            {
                for (int cx = 0; cx < mapX; cx++)
                {
                    double score = 0;
                    foreach (Vector2D vector in positions)
                    {
                        double d = vector.GetDistance(new Vector2D(cx, cy));
                        if (d == 0)
                        {
                            d = 0.5;
                        }

                        score += 1000 / d;
                    }

                    if (score > max || max == -1)
                    {
                        max = score;
                        x = cx;
                        y = cy;
                    }
                }
            }
            
            return new Vector2D(x, y);
        }
    }
}