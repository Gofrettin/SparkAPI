using System;

namespace Spark.Core
{
    public readonly struct Position : IEquatable<Position>
    {
        public static readonly Position Origin = new Position(0, 0);
        
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

        public bool Equals(Position other) => X == other.X && Y == other.Y;
        public bool Equals(short x, short y) => Equals(new Position(x, y));

        public override string ToString() => $"{X}/{Y}";
    }
}