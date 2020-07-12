using System;

namespace Spark.Core
{
    public readonly struct Position
    {
        public short X { get; }
        public short Y { get; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public Position GetDistance(Position position)
        {
            return new Position(Math.Abs((short)(position.X - X)), Math.Abs((short)(position.Y - Y)));
        }

        public override string ToString() => $"{X}/{Y}";
    }
}