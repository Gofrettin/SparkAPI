using System;

namespace Spark.Core
{
    public readonly struct Vector2D
    {
        public short X { get; }
        public short Y { get; }

        public Vector2D(short x, short y)
        {
            X = x;
            Y = y;
        }

        public Vector2D GetDistance(Vector2D vector2D)
        {
            return new Vector2D(Math.Abs((short)(vector2D.X - X)), Math.Abs((short)(vector2D.Y - Y)));
        }

        public override string ToString() => $"{X}/{Y}";
    }
}