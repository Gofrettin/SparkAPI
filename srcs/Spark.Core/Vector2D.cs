using System;

namespace Spark.Core
{
    public readonly struct Vector2D
    {
        private static readonly double Sqrt = Math.Sqrt(2);
        
        public short X { get; }
        public short Y { get; }

        public Vector2D(short x, short y)
        {
            X = x;
            Y = y;
        }

        public Vector2D GetDistanceTo(Vector2D vector2D)
        {
            return new Vector2D(Math.Abs((short)(vector2D.X - X)), Math.Abs((short)(vector2D.Y - Y)));
        }
        
        public int GetDistance(Vector2D destination)
        {
            int x = Math.Abs(X - destination.X);
            int y = Math.Abs(Y - destination.Y);

            int min = Math.Min(x, y);
            int max = Math.Max(x, y);

            return (int)(min * Sqrt + max - min);
        }

        public override string ToString() => $"{X}/{Y}";
    }
}